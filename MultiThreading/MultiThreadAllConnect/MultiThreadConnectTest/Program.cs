using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Net.Sockets;
using System.Threading;

namespace MultiThreadConnectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(4, 4);
            ThreadPool.SetMaxThreads(16, 16);

            DAOLog daoLog = new DAOLog();
            DataTable dataTable = daoLog.Select();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string request = dataRow["mbMustNo"].ToString();
                daoLog.Insert(request);
                ThreadPool.QueueUserWorkItem(ThreadPoolFunction, request);
            }

            Console.Read();
        }

        static void ThreadPoolFunction(Object param)
        {
            string request = (string)param;
            request = request.PadRight(10);

            DateTime baslangic = DateTime.Now;

            TcpClient tcpClient = new TcpClient("127.0.0.1", 9999);
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] arrayByteSend = Encoding.UTF32.GetBytes(request);
            networkStream.Write(arrayByteSend, 0, 40);

            string response;
            byte[] arrayByteReceive = new byte[200];
            int bytesRead = networkStream.Read(arrayByteReceive, 0, 120);
            response = Encoding.UTF32.GetString(arrayByteReceive).Substring(0, 30);

            networkStream.Close();

            tcpClient.Close();

            DateTime bitis = DateTime.Now;

            Console.WriteLine(" ==> " + request);
            Console.WriteLine(" <== " + response);
        }

        
    }
}
