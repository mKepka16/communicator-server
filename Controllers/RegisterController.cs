using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.Controllers
{
    class RegisterController
    {
        readonly RegisterData registerData;
        readonly DbManager dbManager = new DbManager();
        readonly Client client;

        public RegisterController(string data, Client client)
        {
            this.client = client;
            this.registerData = RegisterData.Deserialize(data);
            bool isUserRegistered = TryRegisterUser();
            if(isUserRegistered)
            {
                Payload payload = new Payload("registerSuccess", "");
                this.client.Write(payload.ToString());
            }
            else
            {
                ErrorData errorData = new ErrorData("Użytkownik o podanym nicku już istnieje");
                Payload payload = new Payload("registerFail", errorData.ToString());
                this.client.Write(payload.ToString());
            }
        }

        public bool TryRegisterUser()
        {
            if (dbManager.IsExist(registerData.nick)) return false;
            dbManager.InsertUser(registerData);
            return true;
        }
    }
}
