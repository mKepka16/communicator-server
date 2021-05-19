using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class UserExistenceController
    {
        DbManager dbManager = new DbManager();

        public UserExistenceController(string data, Client client)
        {
            NickData nick = NickData.Deserialize(data);
            bool isExist = dbManager.IsExist(nick.Nick);
            Payload payload;
            
            if(isExist && nick.Nick.ToLower() == client.userData.nick.ToLower())
            {
                ErrorData error = new ErrorData("Nie możesz dodać samego siebie.");
                payload = new Payload("userDoesNotExist", error.ToString());
            }
            else if(isExist)
                payload = new Payload("userExists", "");
            else
                payload = new Payload("userDoesNotExist", "");
            client.Write(payload.ToString());
        }
    }
}
