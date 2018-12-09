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
    class ServerMultiThreads : Server
    {
        public ServerMultiThreads()
        {
            string serverIp = ConfigurationManager.AppSettings["serverIp"];
            IPAddress ipAddress = IPAddress.Parse(serverIp);

            int port = Int32.Parse(ConfigurationManager.AppSettings["serverPort"]);
            TcpListener tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();

            Console.WriteLine("Server started waiting for clients");
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Thread tcpThread = new Thread(threadPoolTestFunction);
                tcpThread.Start(tcpClient);
            }  
        }


    }
}
