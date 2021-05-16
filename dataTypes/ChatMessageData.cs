using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class ChatMessageData
    {
        public string Content { get; set; }
        public string SenderNick { get; set; }
        public bool IsMyMessage { get; set; }

        public ChatMessageData(string Content, string SenderNick, string ClientNick)
        {
            this.Content = Content;
            this.SenderNick = SenderNick;
            IsMyMessage = ClientNick == SenderNick;
        }
    }
}
