using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class CreateChatController
    {
        DbManager dbManager = new DbManager();

        public CreateChatController(string data, Client client)
        {
            NewChatData newChatData = NewChatData.Deserialize(data);
            newChatData.Nicks.Add(new NickData(client.userData.nick));

            bool isNameOccupied = dbManager.IsExist(newChatData);
            if(isNameOccupied)
            {
                ErrorData error = new ErrorData("Już posiadasz konwersacje z tym użytkownikiem");
                if (newChatData.Nicks.Count() > 2)
                    error.error = "Grupa o podanej nazwie już istnieje";

                Payload errorPayload = new Payload("groupExists", error.ToString());
                client.Write(errorPayload.ToString());
                return;
            }

            string chatName;
            if (newChatData.Nicks.Count() > 2)
                chatName = newChatData.ChatName;
            else
            {
                chatName = $"{newChatData.Nicks[0].Nick},{newChatData.Nicks[1].Nick}";
            }
            newChatData.ChatName = chatName;
            dbManager.CreateNewChat(newChatData);
            Payload payload = new Payload("groupDoesNotExist", "");
            client.Write(payload.ToString());

            List<ChatListItemData> chats = dbManager.GetUserChats(client.userData);
            ChatListData chatListData = new ChatListData(chats);
            client.Write(new Payload("chatsList", chatListData.ToString()).ToString());
        }
    }
}
