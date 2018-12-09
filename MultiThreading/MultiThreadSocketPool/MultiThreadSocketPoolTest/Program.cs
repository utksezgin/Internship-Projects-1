using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace MultiThreadSocketPoolTest
{
    class Program
    {
        static SocketClient socketClient;

        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(16, 16);
            ThreadPool.SetMinThreads(32, 32);

            socketClient = new SocketClient(9999);

            for (int i = 0; i < 1000; i++)
                ThreadPool.QueueUserWorkItem(ThreadPoolFunction, i.ToString());
                
            Console.Read();
        }

        static void ThreadPoolFunction(object param)
        {
            string request = (string) param;

            socketClient.SendReceive(request);
        }
    }
}
