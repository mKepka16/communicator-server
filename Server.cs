using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace communicator_server
{
    class Server
    {
        private static TcpListener tcpListener;
        private static Int16 port = 200;
        private static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        static void Main(string[] args)
        {
            //AskForIp();
            //AskForPort();
            InitServer();
            ListenForClients();

            Console.ReadLine();
        }

        static void AskForIp()
        {
            Console.Write("IP Address: ");
            string userResponse = Console.ReadLine();
            bool parseSucceeded = IPAddress.TryParse(userResponse, out IPAddress ipAddress);
            if (!parseSucceeded)
            {
                Console.WriteLine("Incorrect IP address format");
                Console.WriteLine("Server is running on default ip: 127.0.0.1");
                //AskForIp();
                return;
            }
            Server.ipAddress = ipAddress;
        }

        static void AskForPort()
        {
            Console.Write("Port: ");
            string userResponse = Console.ReadLine();
            if (!Int16.TryParse(userResponse, out port))
            {
                Console.WriteLine("Wrong port");
                AskForPort();
            }
        }

        static void InitServer()
        {
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();
            Console.WriteLine("Server is working");
        }

        static void ListenForClients()
        {
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("client connected");
                new Client(tcpClient);
            }
        }
    }
}
