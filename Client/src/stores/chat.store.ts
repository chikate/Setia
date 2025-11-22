import {
  HubConnectionBuilder,
  HubConnection,
  LogLevel,
} from "@microsoft/signalr";
import type { UserModel, MessageModel } from "@/composables/models";
import { getCookie } from "@/composables/helpers";

const authStore = new AuthApi();

export const useChatStore = defineStore("chat", {
  state: () => ({
    friends: [] as UserModel[],
    groups: [] as any[],
    requests: [] as any[],
    messages: {} as Record<string, MessageModel[]>,
    connection: null as HubConnection | null,
    isConnected: false,
    activeChat: null as { type: "user" | "group"; id: string } | null,
  }),

  actions: {
    async initialize() {
      if (!getCookie("access_token")) return;

      const config = new Configuration({
        accessToken: getCookie("access_token"),
      });
      this.friendsApi = new FriendsApi(config);
      this.chatApi = new ChatApi(config);

      await this.fetchFriends();
      await this.fetchGroups();
      await this.fetchRequests();

      await this.connectSignalR(getCookie("access_token"));
    },

    async connectSignalR(token: string) {
      if (this.connection) return;

      this.connection = new HubConnectionBuilder()
        .withUrl("/events", {
          accessTokenFactory: () => token,
        })
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Information)
        .build();

      this.connection.on("ReceiveMessage", (message: MessageModel) => {
        this.handleIncomingMessage(message);
      });

      this.connection.on(
        "MessageEdited",
        (messageId: string, newContent: string) => {
          this.handleMessageEdited(messageId, newContent);
        }
      );

      this.connection.on(
        "ReactionAdded",
        (messageId: string, userId: string, reaction: string) => {}
      );

      try {
        await this.connection.start();
        this.isConnected = true;
        this.groups.forEach((g) => this.connection?.invoke("JoinGroup", g.id));
      } catch (err) {}

      this.connection.onclose(() => {
        this.isConnected = false;
      });
    },

    handleIncomingMessage(message: MessageModel) {
      let targetId = "";

      if (message.groupId) {
        targetId = message.groupId;
      } else {
        const myId = JSON.parse(getCookie("user")).id;
        targetId = message.authorId === myId ? message.to! : message.authorId!;
      }

      if (!this.messages[targetId]) {
        this.messages[targetId] = [];
      }
      this.messages[targetId].push(message);
    },

    handleMessageEdited(messageId: string, newContent: string) {
      for (const key in this.messages) {
        const msg = this.messages[key].find((m) => m.id === messageId);
        if (msg) {
          msg.message = newContent;
          msg.isEdited = true;
          break;
        }
      }
    },

    async fetchFriends() {
      try {
        this.friends = await this.friendsApi.apiFriendsGet();
      } catch (e) {}
    },

    async fetchGroups() {
      try {
        const res = await fetch("/api/Chat/Group");
        if (res.ok) {
          this.groups = await res.json();
        }
      } catch (e) {}
    },

    async fetchRequests() {
      try {
        this.requests = await this.friendsApi.apiFriendsRequestsGet();
      } catch (e) {}
    },

    async sendRequest(userId: string) {
      await this.friendsApi.apiFriendsRequestUserIdPost({ userId });
    },

    async acceptRequest(requestId: string) {
      await this.friendsApi.apiFriendsAcceptRequestIdPost({ requestId });
      await this.fetchRequests();
      await this.fetchFriends();
    },

    async rejectRequest(requestId: string) {
      await this.friendsApi.apiFriendsRejectRequestIdPost({ requestId });
      await this.fetchRequests();
    },

    async loadChatHistory(id: string, type: "user" | "group") {
      this.activeChat = { type, id };

      if (!this.messages[id]) {
        let history = [];
        if (type === "user") {
          history = await this.chatApi.apiChatHistoryOtherUserIdGet({
            otherUserId: id,
          });
        } else {
          const res = await fetch(`/api/Chat/Group/${id}/History`);
          if (res.ok) history = await res.json();
        }
        this.messages[id] = history;
      }
    },

    async sendMessage(message: string, replyToId?: string) {
      if (!this.connection || !this.activeChat) return;

      const toUserId =
        this.activeChat.type === "user" ? this.activeChat.id : null;
      const groupId =
        this.activeChat.type === "group" ? this.activeChat.id : null;

      await this.connection.invoke(
        "SendMessage",
        toUserId,
        groupId,
        message,
        replyToId
      );
    },

    async createGroup(name: string) {
      const res = await fetch("/api/Chat/Group", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ name }),
      });
      if (res.ok) {
        const group = await res.json();
        this.groups.push(group);
        this.connection?.invoke("JoinGroup", group.id);
        return group;
      }
    },
  },
});
