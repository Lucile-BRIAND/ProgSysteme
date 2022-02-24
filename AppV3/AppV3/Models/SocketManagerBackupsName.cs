using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppV3.Models
{
    class SocketManagerBackupsName //SINGLETON
    {
        public Socket socket { get; set; }
        private static SocketManagerBackupsName socketManager;

        //Private constructor
        private SocketManagerBackupsName() { }

        // Initializes the unique instance
        public static SocketManagerBackupsName GetInstance
        {
            get
            {
                if (socketManager == null)
                {
                    socketManager = new SocketManagerBackupsName();
                }
                return socketManager;
            }

        }
        public Socket Connect()
        {
            //Connect the server
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050));
            socket.Listen(20);
            return socket;
        }
        public Socket AcceptConnection(Socket server)
        {
            //Accept client connection
            Socket client = server.Accept();
            return client;
        }
        public void ListenToNetwork(Socket client)
        {
            //Listen to the network to receive and send data
            string data = null;
            IPEndPoint ipEndPoint = (IPEndPoint)client.LocalEndPoint;
            byte[] buffer = new byte[client.ReceiveBufferSize];

            int numberReceivedBytes = client.Receive(buffer); // Receive return the number of byte

            data += Encoding.ASCII.GetString(buffer, 0, numberReceivedBytes);

            byte[] message = Encoding.ASCII.GetBytes("Your message :" + data);
            client.Send(message);
        }
    }
}
