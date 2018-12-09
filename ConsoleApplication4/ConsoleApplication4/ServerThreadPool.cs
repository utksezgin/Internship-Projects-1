using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Configuration;

namespace ConsoleApplication4
{
    class ServerThreadPool : Server
    {

        public ServerThreadPool()
        {
            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(16, 16);

            string serverIp = ConfigurationManager.AppSettings["serverIp"];
            IPAddress ipAddress = IPAddress.Parse(serverIp);

            int port = Int32.Parse(ConfigurationManager.AppSettings["serverPort"]);
            TcpListener tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();

            Console.WriteLine("Server started waiting for clients");
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(threadPoolTestFunction, tcpClient);
            }
        }

        
    }
}
