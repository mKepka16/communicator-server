using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class NewChatData
    {
        public string ChatName { get; set; }
        public List<NickData> Nicks { get; set; }

        public NewChatData(string ChatName, List<NickData> Nicks)
        {
            this.ChatName = ChatName;
            this.Nicks = Nicks;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static NewChatData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<NewChatData>(toDeserialize);
        }
    }
}
