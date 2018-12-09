using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;

namespace MultiThreadSocketTest
{
    class ThreadParamSt
    {
        public string request;
        public string response;
        public AutoResetEvent anEvent;
    }

    class BSTCPClient
    {
        NetworkStream networkStream;

        List<ThreadParamSt> arrayThreadList = new List<ThreadParamSt>(100);

        public BSTCPClient(int port)
        {
            for (bool bExit = false; !bExit; )
            {
                try
                {
                    TcpClient tcpClient = new TcpClient("127.0.0.1", port);
                    networkStream = tcpClient.GetStream();
                    bExit = true;

                    Thread t = new Thread(Reader);
                    t.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                }
            }
        }

        public string SendReceive(string request)
        {
            request = request.PadRight(10);

            ThreadParamSt threadParam = new ThreadParamSt();
            threadParam.request = request;
            threadParam.response = "";
            threadParam.anEvent = new AutoResetEvent(false);

            lock (arrayThreadList)
            {
                if (arrayThreadList.Count < 100)
                    arrayThreadList.Add(threadParam);
            }

            byte[] arrayByte = Encoding.UTF32.GetBytes(request);
            networkStream.Write(arrayByte, 0, arrayByte.Length);

            threadParam.anEvent.WaitOne();

            string response = threadParam.response;

            lock (arrayThreadList)
            {
                arrayThreadList.Remove(threadParam);
            }

            return response;
        }

        void Reader()
        {
            for (bool bExit = false; !bExit; )
            {
                try
                {
                    byte[] arrayByte = new byte[250];

                    int bytesRead = 0;
                    bytesRead = networkStream.Read(arrayByte, 0, 160);
                    if (bytesRead <= 0)
                        break;

                    string response = Encoding.UTF32.GetString(arrayByte);
                    response = response.Substring(0, 40);

                    Matcher(response);
                }
                catch (Exception ex)
                {
                    bExit = true;
                    Console.WriteLine(ex);
                }
            }
        }

        void Matcher(string response)
        {
            lock (arrayThreadList)
            {
                foreach (ThreadParamSt threadParam in arrayThreadList)
                {
                    if (threadParam.request == response.Substring(0, 10))
                    {
                        threadParam.response = response.Substring(10, 30);
                        threadParam.anEvent.Set();
                        break;
                    }
                }
            }
        }
    }
}
