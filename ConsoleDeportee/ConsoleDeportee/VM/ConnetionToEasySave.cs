using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleDeportee.Models;

namespace ConsoleDeportee.VM
{
    class ConnetionToEasySave
    {
        public static Socket SeConnecter()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(localEndPoint);

            return server;
        }
        public static string EcouterReseau(Socket client)
        {

            byte[] buffer = new byte[1024];
            int iRx = client.Receive(buffer);
            char[] chars = new char[iRx];

            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
            System.String recv = new System.String(chars);
            return recv;
        }

        public static void Deconnecter(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
