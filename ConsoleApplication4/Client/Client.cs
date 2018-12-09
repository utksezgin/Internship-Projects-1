using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Data;
using System.Configuration;


namespace Client
{
    class Client
    {
        public Client()
        {
            try
            {
                //Setting min and max thread counts
                ThreadPool.SetMinThreads(4, 4);
                ThreadPool.SetMaxThreads(16, 16);

                //Creating SQLTest object and reading requests from the table.
                SQLTest sqlTest = new SQLTest();
                DataTable dataTable = sqlTest.select();

                //For each request in the table
                foreach (DataRow dr in dataTable.Rows)
                {
                    string request = dr["ID"].ToString();
                    //Insert request into new table
                    sqlTest.Insert(request);
                    //Call an available thread to execute the function.
                    ThreadPool.QueueUserWorkItem(threadPoolTestFunction, request);
                }
                //After all the requests processed, close the sql connection.
                sqlTest.close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void threadPoolTestFunction(Object param)
        {
            try
            {
                string request = (string)param;

                string serverIP = ConfigurationManager.AppSettings["serverIP"];
                int port = Int32.Parse(ConfigurationManager.AppSettings["serverPort"]);

                //Creating a client and a network stream
                TcpClient tcpClient = new TcpClient(serverIP, port);
                NetworkStream networkStream = tcpClient.GetStream();

                Console.WriteLine(" ==> " + request);
                //Sending request to server
                byte[] sendByteArray = Encoding.UTF8.GetBytes(request);
                networkStream.Write(sendByteArray, 0, sendByteArray.Length);

                //Reading response from Server
                string response;
                byte[] receivedByteArray = new byte[256];
                int bytesRead = networkStream.Read(receivedByteArray, 0, receivedByteArray.Length);

                response = Encoding.UTF8.GetString(receivedByteArray, 0, bytesRead);

                //Closing client and network.
                networkStream.Close();
                networkStream.Dispose();
                tcpClient.Close();
               
                Console.WriteLine(" <== " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
   }
}
