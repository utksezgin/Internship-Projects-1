using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Client
{
    class Client
    {
        private static Socket ConnectSocket(String server, int port)
        {
            Socket socket = null;
            IPHostEntry hostEntry = Dns.GetHostEntry(server);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipEndPoint = new IPEndPoint(address, port);
                Socket tempSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                tempSocket.Connect(ipEndPoint);
                if (tempSocket.Connected)
                {
                    socket = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return socket;
        }

        public void sendString(string message, string address, int port)
        {
            try
            {

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(address, port);

                Console.WriteLine("Client Connected");

                Stream stream = tcpClient.GetStream();

                byte[] stringByte = Encoding.UTF8.GetBytes(message);
                stream.Write(stringByte, 0, stringByte.Length);

                Console.WriteLine("Message sent.");



                /*socket.Shutdown(SocketShutdown.Both);
                socket.Close();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
