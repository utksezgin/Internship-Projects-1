using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace MultiThreadSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTCPClient bsTcpClient = new BSTCPClient(9999);

            DAOLog daoLog = new DAOLog();
            DataTable dataTable = daoLog.Select();
            foreach(DataRow dataRow in dataTable.Rows)
            {
                string request = dataRow["mbMustNo"].ToString();
                daoLog.Insert(request);
                string response = bsTcpClient.SendReceive(request);

                Console.WriteLine("Request: " + request + ", Response: " + response);
            }
        }
    }
}
