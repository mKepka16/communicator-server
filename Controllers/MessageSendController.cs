using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class MessageSendController
    {
        DbManager dbManager = new DbManager();
        
        public MessageSendController(string data, Client client)
        {
            MessageSendData message = MessageSendData.Deserialize(data);
            dbManager.AddMessageToChat(message, client.userData);
            List<string> nicks = dbManager.GetNicksInChat(message.ChatId, client.userData);
            IEnumerable<Client> clientsInGroup = Client.users.Where(cl => nicks.Contains(cl.userData.nick));
            Console.WriteLine("Active clients in group: ");
            foreach (Client cl in clientsInGroup)
            {
                MessageReceiveData messageReceiveData = new MessageReceiveData(message.Content, client.userData.nick, message.ChatId);
                Payload payload = new Payload("messageReceive", messageReceiveData.ToString());
                cl.Write(payload.ToString());
            }
        }
    }
}
