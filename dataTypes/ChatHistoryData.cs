using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class ChatHistoryData
    {
        public List<ChatMessageData> ChatHistory { get; set; }
        public string Name { get; set; }

        public ChatHistoryData(List<ChatMessageData> ChatHistory, string Name)
        {
            this.ChatHistory = ChatHistory;
            this.Name = Name;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static ChatHistoryData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<ChatHistoryData>(toDeserialize);
        }
    }
}
