using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace MultiThreadConnect
{
    class BSTCPServer
    {
        public BSTCPServer(int port)
        {
            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(16, 16);

            IPAddress ipAdress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ipAdress, port);
            tcpListener.Start();
            Console.WriteLine("Server started waiting for client.");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(ThreadPoolFunction, tcpClient);
            }
        }

        public void ThreadPoolFunction(object param)
        {
            TcpClient tcpClient = (TcpClient)param;
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] arrayByteRead = new byte[100];

            int bytesRead = 0;
            bytesRead = networkStream.Read(arrayByteRead, 0, 40);
            if (bytesRead <= 0)
                return;

            string request = Encoding.UTF32.GetString(arrayByteRead);
            request = request.Substring(0, 10);

            DAOLog daoLog = new DAOLog();
            string response = daoLog.SelectByPK(request);
            daoLog.Update(request, response);
            daoLog.Close();

            response = request + response;

            response = response.PadRight(30);
            byte[] arrayByteWrite = Encoding.UTF32.GetBytes(response);
            networkStream.Write(arrayByteWrite, 0, arrayByteWrite.Length);

            networkStream.Close();

            Console.WriteLine(" ==> " + request);
            Console.WriteLine(" <== " + response);
        }
    }
}
