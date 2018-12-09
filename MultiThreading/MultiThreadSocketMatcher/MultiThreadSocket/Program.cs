using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTCPServer tcpServer = new BSTCPServer(9999);
        }
    }
}
