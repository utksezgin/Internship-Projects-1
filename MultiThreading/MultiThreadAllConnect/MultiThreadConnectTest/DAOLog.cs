using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace MultiThreadConnectTest
{
    class DAOLog
    {
        SqlConnection sqlConnection1;
        SqlConnection sqlConnection2;

        public DAOLog()
        {
            try
            {
                sqlConnection1 = new SqlConnection();
                sqlConnection1.ConnectionString = "Data Source=BSS04; Initial Catalog=KrediHLK; Integrated Security= SSPI";
                sqlConnection1.Open();

                sqlConnection2 = new SqlConnection();
                sqlConnection2.ConnectionString = "Data Source=BSS04; Initial Catalog=KrediHLK; Integrated Security= SSPI";
                sqlConnection2.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public DataTable Select()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection1;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "truncate table kkThreadTest";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "select top 100 mbMustNo from kkMusteriBilgi(nolock) order by 1";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);

            return dataTable;
        }

        public void Insert(string request)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection2;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "insert kkThreadTest values(@request, null, getdate(), null)";
            sqlCommand.Parameters.Add(new SqlParameter("@request", request));
            sqlCommand.ExecuteNonQuery();
        }
    }
}
