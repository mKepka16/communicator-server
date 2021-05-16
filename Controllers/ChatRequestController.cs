using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class ChatRequestController
    {
        Client client;
        readonly DbManager dbManager = new DbManager();

        public ChatRequestController(string data, Client client)
        {
            this.client = client;
            ChatRequestData requestData = ChatRequestData.Deserialize(data);
            Console.WriteLine(requestData.ChatId);
            Console.WriteLine(requestData.ChatName);
            ChatHistoryData historyData = dbManager.GetChatHistory(requestData.ChatId, requestData.ChatName, client.userData);
            foreach (ChatMessageData message in historyData.ChatHistory)
            {
                Console.WriteLine(message.SenderNick);
                Console.WriteLine(message.Content);
                Console.WriteLine(message.IsMyMessage);
            }

            Payload payload = new Payload("chatHistory", historyData.ToString());
            client.Write(payload.ToString());
        }
    }
}
