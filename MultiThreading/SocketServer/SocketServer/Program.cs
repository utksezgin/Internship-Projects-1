using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Server
            IPAddress ipAdress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ipAdress, 9999);
            tcpListener.Start();
            Console.WriteLine("Server started waiting for client.");

            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] arrayByteRead = new byte[100];

            int bytesRead = 0;
            bytesRead = networkStream.Read(arrayByteRead, 0, 40);
            if (bytesRead <= 0)
                return;

            string request = Encoding.UTF32.GetString(arrayByteRead);
            request = request.Substring(0, 10);

            string response = "Hello " + request ;

            response = response.PadRight(30);
            byte[] arrayByteWrite = Encoding.UTF32.GetBytes(response);
            networkStream.Write(arrayByteWrite, 0, arrayByteWrite.Length);

            networkStream.Close();

            Console.WriteLine(" ==> " + request);
            Console.WriteLine(" <== " + response);

            Console.Read();
        }
    }
}
