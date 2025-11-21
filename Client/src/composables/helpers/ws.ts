import * as signalR from "@microsoft/signalr";

export const signalRConnection = new signalR.HubConnectionBuilder()
  .withUrl("/events", {
    accessTokenFactory: () => getCookie("access_token") || "",
  })
  .withAutomaticReconnect()
  .build();

signalRConnection.start().then(console.log).catch(console.error);
