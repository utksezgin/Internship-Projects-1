using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Client
            string request = "Mutlu";

            TcpClient tcpClient = new TcpClient("127.0.0.1", 9999);
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] arrayByteSend = Encoding.UTF32.GetBytes(request);
            networkStream.Write(arrayByteSend, 0, request.Length);

            string response;
            byte[] arrayByteReceive = new byte[200];
            int bytesRead = networkStream.Read(arrayByteReceive, 0, 120);
            response = Encoding.UTF32.GetString(arrayByteReceive);

            networkStream.Close();

            tcpClient.Close();

            Console.WriteLine(" ==> " + request);
            Console.WriteLine(" <== " + response);
        }
    }
}
