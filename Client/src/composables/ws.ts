import * as signalR from "@microsoft/signalr";

export const signalRConn = new signalR.HubConnectionBuilder()
  .withUrl("/event")
  .withAutomaticReconnect()
  .build();

export const auditConn = new signalR.HubConnectionBuilder()
  .withUrl("/events/audit")
  .withAutomaticReconnect()
  .build();
