using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTCPServer bsTcpServer = new BSTCPServer(9999);
        }
    }
}
