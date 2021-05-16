using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class ChatRequestData
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; }

        public ChatRequestData(int ChatId, string ChatName)
        {
            this.ChatId = ChatId;
            this.ChatName = ChatName;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static ChatRequestData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<ChatRequestData>(toDeserialize);
        }
    }
}
