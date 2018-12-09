using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace ConsoleApplication4
{
    class ServerManualThreadPool
    {
        /***   Parallel Arrays   ***/
        Thread[] threadPool; //Array of threads that the class can use.
        AutoResetEvent[] threadAvailability; // Array of events that used as the availability of threads.
        AutoResetEvent[] threadStart; //Array of events that used for signaling threads to start the loop again.
        TcpClient[] threadClients; //Array of TcpClient. 1 element for each thread to access.
        /***************************/
        AutoResetEvent variableLock;  //Event to lock threadClients variable to prevent race condition.

        public ServerManualThreadPool(int maxThread)
        {
            threadPool = new Thread[maxThread];
            threadAvailability = new AutoResetEvent[maxThread];
            threadStart = new AutoResetEvent[maxThread];
            threadClients = new TcpClient[maxThread];
            variableLock = new AutoResetEvent(true);

            for (int i = 0; i < maxThread; ++i)
            {
                threadPool[i] = new Thread(threadPoolTestFunction);
                //All threads start as available at the beginning.
                threadAvailability[i] = new AutoResetEvent(true);
                //All threads will wait for signal from the main thread to start.
                threadStart[i] = new AutoResetEvent(false);
                //All threads start initially and wait for signal.
                threadPool[i].Start(i);
            }

            //Server Configuration
            string serverIp = ConfigurationManager.AppSettings["serverIp"];
            IPAddress ipAddress = IPAddress.Parse(serverIp);
            int port = Int32.Parse(ConfigurationManager.AppSettings["serverPort"]);
            TcpListener tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();


            Console.WriteLine("Server started waiting for clients...");
            while (true)
            {
                //Waiting for an available thread.
                int availableThreadIndex = WaitHandle.WaitAny(threadAvailability, -1);
                if (availableThreadIndex < 0 || availableThreadIndex > maxThread)
                {
                    Console.WriteLine("There's no available thread");
                    return;
                }
                Console.WriteLine("Available Thread No: " + availableThreadIndex);

                //Waiting for a client.
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                Console.WriteLine("Client connected!");

                //Assigning new client to the available thread.
                variableLock.WaitOne();
                threadClients[availableThreadIndex] = tcpClient;
                variableLock.Set();

                //Signaling thread to start.
                threadStart[availableThreadIndex].Set();
            }
        }


        public void threadPoolTestFunction(Object param)
        {
            try
            {
                string request;
                string response;
                SqlConnection sqlConnection;

                TcpClient tcpClient;
                int threadIndex = (int)param;

                //Sql Connection
                string connString = ConfigurationManager.ConnectionStrings["ConnStringName"].ConnectionString;
                using (sqlConnection = new SqlConnection(connString))
                {
                    sqlConnection.Open();
                    SQLTest sqlTest = new SQLTest(sqlConnection);

                    while (true)
                    {
                        Console.WriteLine("Thread " + threadIndex + " waiting for signal to start");
                        //Waiting for signal to start.
                        threadStart[threadIndex].WaitOne();

                        //Getting the current assigned client.
                        variableLock.WaitOne();
                        tcpClient = threadClients[threadIndex];
                        variableLock.Set();

                        NetworkStream networkStream = tcpClient.GetStream();

                        //Reading request from the cilent.
                        byte[] receivedByteArray = new byte[200];
                        int bytesRead = 0;
                        bytesRead = networkStream.Read(receivedByteArray, 0, receivedByteArray.Length);
                        if (bytesRead <= 0)
                            return;

                        request = Encoding.UTF8.GetString(receivedByteArray, 0, bytesRead);
                        Console.WriteLine(" ==> " + request);

                        //Updating database with the response
                        response = sqlTest.select(request);
                        sqlTest.update(request, response);

                        Thread.Sleep(10000);

                        //Sending response back to the client.
                        byte[] sendByteArray = Encoding.UTF8.GetBytes(response);
                        networkStream.Write(sendByteArray, 0, sendByteArray.Length);

                        //Closing network
                        networkStream.Flush();
                        networkStream.Close();


                        variableLock.WaitOne();
                        //Resetting event so the thread waits for the new client.
                        threadStart[threadIndex].Reset();
                        //Thread is available again.
                        threadAvailability[threadIndex].Set();
                        variableLock.Set();

                        Console.WriteLine("Thread " + threadIndex + " completed the task");
                        Console.WriteLine(" <== " + response);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
