using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Client
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

        public DataTable select()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "truncate table testTableResponse";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "SELECT * from testTable";
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(dataReader);
            return dt;
            
        }

        public void Insert(string request)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "INSERT INTO testTableResponse values(@Request, null)";
            sqlCommand.Parameters.Add(new SqlParameter("@Request", request));
            sqlCommand.ExecuteNonQuery();
        }

        public void close()
        {
            sqlConnection.Close();
        }

    }
}
