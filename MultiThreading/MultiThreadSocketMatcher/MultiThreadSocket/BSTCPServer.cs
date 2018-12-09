using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MultiThreadSocket
{
    class BSTCPServer
    {
        NetworkStream NetworkStream;

        public BSTCPServer(int port)
        {
            IPAddress ipAdress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ipAdress, port);
            tcpListener.Start();
            Console.WriteLine("Server started waiting for client.");

            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(16, 16);

            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            NetworkStream = tcpClient.GetStream();
            Console.WriteLine("Client " + tcpClient.Client.RemoteEndPoint.ToString() + " connected, waiting for message.");

            for (bool bExit = false; !bExit; )
            {
                try
                {
                    byte[] arrayByte = new byte[100];

                    int bytesRead = 0;
                    bytesRead = NetworkStream.Read(arrayByte, 0, 40);
                    if (bytesRead <= 0)
                        break;

                    string request = Encoding.UTF32.GetString(arrayByte);
                    request = request.Substring(0, 10);
                    ThreadPool.QueueUserWorkItem(ThreadPoolFunction, request);
                }
                catch (Exception ex)
                {
                    bExit = true;
                    Console.WriteLine(ex);
                }
            }
        }

        public void ThreadPoolFunction(object param)
        {
            string request = (string)param;

            DAOLog daoLog = new DAOLog();
            string response = daoLog.SelectByPK(request);
            daoLog.Update(request, response);
            daoLog.Close();

            response = request + response;
            byte[] arrayByte = Encoding.UTF32.GetBytes(response);
            NetworkStream.Write(arrayByte, 0, arrayByte.Length);

            Console.WriteLine(" ==> " + request);
            Console.WriteLine(" <== " + response);
        }
    }
}
