using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class LoginData
    {
        public string nick;
        public string password;

        public LoginData(string nick, string password)
        {
            this.nick = nick;
            this.password = password;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static LoginData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<LoginData>(toDeserialize);
        }
    }
}
