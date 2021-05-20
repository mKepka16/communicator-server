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
            ChatHistoryData historyData = dbManager.GetChatHistory(requestData.ChatId, requestData.ChatName, client.userData);

            Payload payload = new Payload("chatHistory", historyData.ToString());
            client.Write(payload.ToString());
        }
    }
}
