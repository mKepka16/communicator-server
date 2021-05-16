using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class LoginController
    {
        readonly LoginData loginData;
        readonly DbManager dbManager = new DbManager();
        readonly Client client;

        public LoginController(string data, Client client)
        {
            this.client = client;
            this.loginData = LoginData.Deserialize(data);
            bool isUserLoggedIn = TryLogInUser(out int id);
            if(isUserLoggedIn)
            {
                client.userData = new UserData(id, loginData.nick);
                Client.users.Add(client);

                Payload payload = new Payload("loginSuccess", "");
                this.client.Write(payload.ToString());
            }
            else
            {
                ErrorData errorData = new ErrorData("Zły nick lub hasło.");
                Payload payload = new Payload("loginFail", errorData.ToString());
                this.client.Write(payload.ToString());
            }
        }

        public bool TryLogInUser(out int id)
        {
            bool hasLoggedIn = dbManager.IsExist(loginData.nick, loginData.password, out id);

            return hasLoggedIn;
        }
    }
}
