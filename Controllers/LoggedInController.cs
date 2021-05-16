using communicator_server.DataTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class LoggedInController
    {
        readonly DbManager dbManager = new DbManager();
        readonly Client client;

        public LoggedInController(Client client)
        {
            this.client = client;
            UserData data = this.client.userData;
            Console.WriteLine($"User: {data.nick}, id: {data.id} is logged in.");

            List<ChatListItemData> chats = dbManager.GetUserChats(client.userData);
            ChatListData chatListData = new ChatListData(chats);
            client.Write(new Payload("chatsList", chatListData.ToString()).ToString());
        }
    }
}
