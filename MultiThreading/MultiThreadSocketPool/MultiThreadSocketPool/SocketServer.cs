using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using System.Threading;

namespace MultiThreadSocketPool
{
    class SocketServer
    {
        const int MAX_THREAD = 64;
        Thread[] arrayThread;

        TcpListener tcpListener;

        public SocketServer(int port)
        {
            IPAddress ipAdress = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(ipAdress, port);
            tcpListener.Start();

            arrayThread = new Thread[MAX_THREAD];
            for (int i = 0; i < MAX_THREAD; i++)
            {
                arrayThread[i] = new Thread(ThreadFunction);
                arrayThread[i].Start(i);
            }
        }

        void ThreadFunction(object param)
        {
            int threadNumber = (int)param;

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream networkStream = tcpClient.GetStream();

                for (bool bExit = false; !bExit; )
                {
                    try
                    {
                        byte[] arrayByteRead = new byte[100];

                        int bytesRead = 0;
                        bytesRead = networkStream.Read(arrayByteRead, 0, 40);
                        if (bytesRead <= 0)
                            break;

                        string request = Encoding.UTF32.GetString(arrayByteRead);
                        request = request.Replace('\0', ' ');
                        request = request.Substring(0, 10);

                        Console.WriteLine(" ==> " + request);

                        byte[] arrayByteWrite = new byte[250];

                        string response = "Welcome " + request;
                        response = response.PadRight(30);

                        arrayByteWrite = Encoding.UTF32.GetBytes(response);
                        networkStream.Write(arrayByteWrite, 0, 120);

                        Console.WriteLine(" <== " + response);
                    }
                    catch (Exception ex)
                    {
                        bExit = true;
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
