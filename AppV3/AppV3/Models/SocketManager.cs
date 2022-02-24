using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public string EcouterReseau(Socket client)
        {

            byte[] buffer = new byte[1024];
            int iRx = client.Receive(buffer);
            Trace.WriteLine("Je suis le  buffer ;;;;;" + buffer);
            char[] chars = new char[iRx];

            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
            System.String recv = new System.String(chars);
            Trace.WriteLine(recv);
            return recv;
        }
    }
}
