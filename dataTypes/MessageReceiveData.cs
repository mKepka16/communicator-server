using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class MessageReceiveData
    {
        public string Content { get; set; }
        public int ChatId { get; set; }
        public string Author { get; set; }

        public MessageReceiveData(string Content, string Author, int ChatId)
        {
            this.Content = Content;
            this.ChatId = ChatId;
            this.Author = Author;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static MessageReceiveData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<MessageReceiveData>(toDeserialize);
        }
    }
}
