using Main.Modules.Auth;
using Main.Modules.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Main.Modules.Chat;

//public interface IChatService
//{
//    Task<bool> SendMessage(SSEClientManager clientManager, [FromBody] MessageModel messageData);
//}

//public class ChatService(ChatContext chatContext, ILogger<ChatService> logger, IConfiguration config, IAuthService auth) : IChatService
//{
//    public async Task<bool> SendMessage(SSEClientManager clientManager, [FromBody] MessageModel messageData)
//    {
//        messageData.AuthorId = (await auth.GetCurrentUser())?.Id;
//        messageData.AuthorData = await auth.GetCurrentUser();

//        // Step 1: Save the message to the database
//        await chatContext.Messages.AddAsync(messageData);
//        await chatContext.SaveChangesAsync();

//        // Step 2: Prepare the SSE message
//        var messageBytes = Encoding.UTF8.GetBytes($"chatMessage: {messageData}\n\n");

//        // Step 3: Broadcast to all connected clients
//        foreach (var client in clientManager.GetClients())
//            if (client.Body.CanWrite)
//            {
//                await client.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
//                await client.Body.FlushAsync();
//            }

//        return true;
//    }
//}