using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiThreadSocketPool
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer sockServer = new SocketServer(9999);
        }
    }
}
