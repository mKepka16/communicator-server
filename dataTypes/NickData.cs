using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server.DataTypes
{
    class NickData
    {
        public string Nick { get; set; }

        public NickData(string Nick)
        {
            this.Nick = Nick;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static NickData Deserialize(string toDeserialize)
        {
            return JsonConvert.DeserializeObject<NickData>(toDeserialize);
        }
    }
}
