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
    class Server
    {
        public void threadPoolTestFunction(Object param)
        {
            try
            {
                TcpClient tcpClient = (TcpClient)param;
                NetworkStream networkStream = tcpClient.GetStream();

                byte[] receivedByteArray = new byte[200];
                //Reading request from the stream
                int bytesRead = 0;
                bytesRead = networkStream.Read(receivedByteArray, 0, receivedByteArray.Length);
                if (bytesRead <= 0)
                    return;
                //Decoding request
                string request = Encoding.UTF8.GetString(receivedByteArray, 0, bytesRead);

                //Getting the request from the database and the updating the database.
                SQLTest sqlTest = new SQLTest();
                string response = sqlTest.select(request);
                sqlTest.update(request, response);
                sqlTest.close();

                //Sending the response back to the client.
                byte[] sendByteArray = Encoding.UTF8.GetBytes(response);
                networkStream.Write(sendByteArray, 0, sendByteArray.Length);

                //Closing network stream
                networkStream.Flush();
                networkStream.Close();

                Console.WriteLine(" ==> " + request);
                Console.WriteLine(" <== " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
