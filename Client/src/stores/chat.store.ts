import { HubConnectionBuilder, HubConnection, LogLevel } from '@microsoft/signalr';
import type { UserModel, MessageModel } from '@/composables/models';

const authStore = new AuthApi();

export const useChatStore = defineStore('chat', {
    state: () => ({
        friends: [] as UserModel[],
        requests: [] as any[],
        messages: {} as Record<string, MessageModel[]>,
        connection: null as HubConnection | null,
        isConnected: false,
        activeChatUserId: null as string | null,
    }),

    actions: {
        async initialize() {
            if (!getCookie("access_token")) return;

            const config = new Configuration({ accessToken: getCookie("access_token") });
            this.friendsApi = new FriendsApi(config);
            this.chatApi = new ChatApi(config);

            await this.fetchFriends();
            await this.fetchRequests();

            await this.connectSignalR(getCookie("access_token"));
        },

        async connectSignalR(token: string) {
            if (this.connection) return;

            this.connection = new HubConnectionBuilder()
                .withUrl('/events', {
                    accessTokenFactory: () => token
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();

            this.connection.on('ReceiveMessage', (message: MessageModel) => {
                this.handleIncomingMessage(message);
            });

            try {
                await this.connection.start();
                this.isConnected = true;
                console.log('SignalR Connected');
            } catch (err) {
                console.error('SignalR Connection Error: ', err);
            }

            this.connection.onclose(() => {
                this.isConnected = false;
            });
        },

        handleIncomingMessage(message: MessageModel) {
            const otherId = message.authorId === this.activeChatUserId ? message.authorId : message.to;

            const myId = JSON.parse(getCookie("user")).id;

            let partnerId = '';
            if (message.authorId === myId) {
                partnerId = message.to;
            } else {
                partnerId = message.authorId!;
            }

            if (!this.messages[partnerId]) {
                this.messages[partnerId] = [];
            }
            this.messages[partnerId].push(message);
        },

        async fetchFriends() {
            try {
                this.friends = await this.friendsApi.apiFriendsGet();
            } catch (e) {
                console.error('Failed to fetch friends', e);
            }
        },

        async fetchRequests() {
            try {
                this.requests = await this.friendsApi.apiFriendsRequestsGet();
            } catch (e) {
                console.error('Failed to fetch requests', e);
            }
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

        async loadChatHistory(userId: string) {
            if (!this.messages[userId]) {
                const history = await this.chatApi.apiChatHistoryOtherUserIdGet({ otherUserId: userId });
                this.messages[userId] = history;
            }
            this.activeChatUserId = userId;
        },

        async sendMessage(toUserId: string, message: string) {
            if (!this.connection) return;
            await this.connection.invoke('SendMessage', toUserId, message);
        }
    }
});
