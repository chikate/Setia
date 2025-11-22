import * as signalR from "@microsoft/signalr";
import { getCookie } from "./helpers";

export const signalRConnection = new signalR.HubConnectionBuilder()
  .withUrl("/events", {
    accessTokenFactory: () => getCookie("access_token") || "",
  })
  .withAutomaticReconnect()
  .build();
