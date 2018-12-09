using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;

namespace MultiThreadSocketPoolTest
{
    class SocketClient
    {
        const int PORT = 9999;
        const int MAX_THREAD = 64;

        TcpClient[] arrayTcpClient;
        AutoResetEvent[] arrayEventTcpClient;

        public SocketClient(int port)
        {
            arrayTcpClient = new TcpClient[MAX_THREAD];
            arrayEventTcpClient = new AutoResetEvent[MAX_THREAD];

            for (int i = 0; i < MAX_THREAD; i++)
            {
                arrayTcpClient[i] = new TcpClient("127.0.0.1", PORT);
                arrayEventTcpClient[i] = new AutoResetEvent(true);
            }
        }

        public string SendReceive(string request)
        {
            request = request.PadRight(10);

            int availableTcpClientNumber = WaitHandle.WaitAny(arrayEventTcpClient, 2000);
            if (availableTcpClientNumber < 0 || availableTcpClientNumber >= MAX_THREAD)
            {
                Console.WriteLine("There is no TcpClient available!");

                return "";
            }

            NetworkStream networkStream = arrayTcpClient[availableTcpClientNumber].GetStream();

            byte[] arrayByteWrite = new byte[100];
            arrayByteWrite = Encoding.UTF32.GetBytes(request);
            networkStream.Write(arrayByteWrite, 0, 40);

            int bytesRead = 0;
            byte[] arrayByteRead = new byte[250];
            bytesRead = networkStream.Read(arrayByteRead, 0, 120);

            string response = Encoding.UTF32.GetString(arrayByteRead);
            response = response.Substring(0, 30);

            arrayEventTcpClient[availableTcpClientNumber].Set();

            Console.WriteLine("TcpClient Number: " + availableTcpClientNumber + ", Request: " + request + ", Response: " + response);

            return response;
        }

    }
}
