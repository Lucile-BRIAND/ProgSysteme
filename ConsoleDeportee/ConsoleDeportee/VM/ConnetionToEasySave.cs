using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ConsoleDeportee.Models;

namespace ConsoleDeportee.VM
{
    class ConnetionToEasySave
    {
        public Socket InitPourcentage()
        {
            Socket connection = SeConnecterPourcentage();
            //EcouterReseau(connection);
            return connection;
        }
        public Socket InitNomSauvegarde()
        {
            Socket connection = SeConnecterNomSauvegarde();
            //EcouterReseau(connection);
            return connection;
        }
        public static Socket SeConnecterPourcentage()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));

            return server;
        }
        public static Socket SeConnecterNomSauvegarde()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050 ));

            return server;
        }
        public static Socket SeConnecterSendIndex()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9090));
            server.Listen(20);
            return server;
        }
        public static string EcouterReseau(Socket client)
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
        public static string EcouterReseauBackupsName(Socket client)
        {

            byte[] buffer = new byte[1024];
            try {
                int iRx = client.Receive(buffer);
                Trace.WriteLine("Je suis le buffer ;;;;;" + buffer);
                char[] chars = new char[iRx];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);
                Trace.WriteLine(recv);
                return recv;
            } catch
            {
                return "";
            } 
        }
        public static void Deconnecter(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
