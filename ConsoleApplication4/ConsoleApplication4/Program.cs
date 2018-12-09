using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApplication4
{
    class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                ServerMultiThreads serverMultiThreads = new ServerMultiThreads();
                //ServerManualThreadPool serverManuelThreadPool = new ServerManualThreadPool(3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
