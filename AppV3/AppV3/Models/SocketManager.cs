using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppV3.Models
{
    class SocketManager //SINGLETON
    {
        
        public Socket socket { get; set; }
        private static SocketManager socketManager;

        //Private constructor
        private SocketManager() { }

        // Initializes the unique instance
        public static SocketManager GetInstance
        {
            get
            {
                if (socketManager == null)
                {
                    socketManager = new SocketManager();
                }
                return socketManager;
            }

        }
        public Socket Connect()
        {
            //Connect the server
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            socket.Listen(20);
            return socket;
        }
        public Socket AcceptConnection(Socket server)
        {
            //Accept client connection
            Socket client = server.Accept();
            return client;
        }
        public string ListenToNetwork(Socket client)
        {
            //Listen to the network to receive and send data
            byte[] buffer = new byte[1024];
            int iRx = client.Receive(buffer);
            Trace.WriteLine("Je suis le  buffer ;;;;;" + buffer);
            char[] chars = new char[iRx];

            Decoder d = Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
            string recv = new string(chars);
            Trace.WriteLine(recv);
            return recv;
        }
    }
}
