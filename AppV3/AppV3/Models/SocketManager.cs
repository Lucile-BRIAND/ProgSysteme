using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AppV3.Models
{
    class SocketManager
    {
        
        public Socket socket { get; set; }
        private static SocketManager socketManager;
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
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            socket.Listen(20);
            return socket;
        }
        public Socket AcceptConnection(Socket server)
        {
            Socket client = server.Accept();
            return client;
        }
        public void ListenToNetwork(Socket client)
        {
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
