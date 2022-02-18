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
        public static List<JobModel> EcouterReseau(Socket client)
        {
            string message = Console.ReadLine();
            client.Send(Encoding.UTF8.GetBytes(message));
            List<JobModel> jobModelList = new List<JobModel>();
            return jobModelList;
        }

        public static void Deconnecter(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
