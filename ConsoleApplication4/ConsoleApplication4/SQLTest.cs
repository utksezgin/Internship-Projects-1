using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ConsoleApplication4
{
    class SQLTest
    {
        SqlConnection sqlConnection;

        public SQLTest()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnStringName"].ConnectionString;
            sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
        }

        public SQLTest(SqlConnection connection)
        {
            sqlConnection = connection;
        }

        

        public string select(string request)
        {
            string response;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT FirstName from testTable where ID = @Request";
            sqlCommand.Parameters.Add(new SqlParameter("@Request", request));

            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();
            response = sqlReader["FirstName"].ToString();
            sqlReader.Close();


            return response;
        }

        public void update(string request, string response)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "Update testTableResponse set Response = @Response where Request = @Request";
            sqlCommand.Parameters.Add(new SqlParameter("@Response", response));
            sqlCommand.Parameters.Add(new SqlParameter("@Request", request));
            sqlCommand.ExecuteNonQuery();

        }

        public void close()
        {
            sqlConnection.Close();
        }
    }
}
