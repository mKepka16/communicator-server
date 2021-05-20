using communicator_server.Controllers;
using communicator_server.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server
{
    class Client
    {
        public static List<Client> clients = new List<Client>();
        public static List<Client> users = new List<Client>();
        public BinaryReader binaryReader;
        public BinaryWriter binaryWriter;
        public TcpClient tcpClient;
        public NetworkStream networkStream;
        public UserData userData;

        public Client(TcpClient client)
        {
            clients.Add(this);

            tcpClient = client;
            networkStream = tcpClient.GetStream();
            binaryReader = new BinaryReader(networkStream);
            binaryWriter = new BinaryWriter(networkStream);

            Task.Run(() => Read());
        }

        public void Read()
        {
            try
            {
                string readData;
                while (true)
                {
                    readData = binaryReader.ReadString();
                    Console.WriteLine("From client: " + readData);
                    Console.WriteLine();
                    Payload payload = Payload.Deserialize(readData);
                    PayloadDistributor.Distribute(payload, this); 
                }
            }
            catch
            {
                Disconnect();
            }
        }

        public void Write(string data)
        {
            try
            {
                Console.WriteLine("To client: " + data);
                Console.WriteLine();
                binaryWriter.Write(data);
                binaryWriter.Flush();
            }
            catch
            {
                Console.WriteLine("Message has not been sent");
            }
        }

        public void Disconnect()
        {
            Console.WriteLine($"{userData.nick} disconnected");
            clients.Remove(this);
            if (users.Contains(this)) users.Remove(this);
            tcpClient.Close();
        }
    }
}
