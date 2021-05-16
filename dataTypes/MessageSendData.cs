using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class MessageSendData
    {
        public string Content { get; set; }
        public int ChatId { get; set; }

        public MessageSendData(string Content, int ChatId)
        {
            this.Content = Content;
            this.ChatId = ChatId;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static MessageSendData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<MessageSendData>(toDeserialize);
        }
    }
}
