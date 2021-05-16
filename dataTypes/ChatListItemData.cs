using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    public class ChatListItemData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ChatListItemData(int Id, string Name, int NumberOfMembers, string clientNick)
        {
            this.Id = Id;
            this.Name = Name;
            if(NumberOfMembers < 3)
            {
                string[] nicks = this.Name.Split(',');
                this.Name = nicks[0];
                if (nicks[0] == clientNick) this.Name = nicks[1];
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static ChatListItemData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<ChatListItemData>(toDeserialize);
        }
    }
}
