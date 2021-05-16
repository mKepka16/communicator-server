using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class UserData
    {
        public string nick;
        public int id;
        

        public UserData(int id, string nick)
        {
            this.id = id;
            this.nick = nick;
        }
    }
}
