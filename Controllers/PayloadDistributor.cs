using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class PayloadDistributor
    {
        public static void Distribute(Payload payload, Client client)
        {

            switch(payload.action)
            {
                case "register":
                    new RegisterController(payload.data, client);
                    break;
                case "login":
                    new LoginController(payload.data, client);
                    break;
                case "loggedIn":
                    new LoggedInController(client);
                    break;
                case "chatRequest":
                    new ChatRequestController(payload.data, client);
                    break;
                case "messageSend":
                    new MessageSendController(payload.data, client);
                    break;
            }
        }
    }
}
