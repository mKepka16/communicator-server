using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class ChatListData
    {
        public List<ChatListItemData> ChatList { get; set; }

        public ChatListData(List<ChatListItemData> ChatList)
        {
            this.ChatList = ChatList;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static ChatListData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<ChatListData>(toDeserialize);
        }
    }
}
