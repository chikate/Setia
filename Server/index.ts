import WebSocket from "ws";
const server = new WebSocket.Server({ port: "3000" });

server.on("connection", (socket) => {
  socket.on("message", (message) => {
    console.log(message);
    socket.send(`Roger that! ${message}`);
  });
});
