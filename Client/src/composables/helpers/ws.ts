import * as signalR from "@microsoft/signalr";

export const signalRConnection = new signalR.HubConnectionBuilder()
  .withUrl("/events")
  .withAutomaticReconnect()
  .build();

signalRConnection.start().then(console.log).catch(console.error);
