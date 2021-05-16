using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class Payload
    {
        public string action;
        public string data;

        public Payload(string action, string data)
        {
            this.action = action;
            this.data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static Payload Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<Payload>(toDeserialize);
        }
    }
}
