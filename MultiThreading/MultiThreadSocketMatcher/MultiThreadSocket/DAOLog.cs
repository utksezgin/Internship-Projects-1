using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace MultiThreadSocket
{
    class DAOLog
    {
        SqlConnection sqlConnection;

        public DAOLog()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=BSS04; Initial Catalog=KrediHLK; Integrated Security= SSPI";
            sqlConnection.Open();
        }

        public string SelectByPK(string request)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "select mbEmbossAdi from kkMusteriBilgi(nolock) where mbMustNo = @MustNo";
            sqlCommand.Parameters.Add(new SqlParameter("@MustNo", request));

            SqlDataReader dr = sqlCommand.ExecuteReader();
            dr.Read();
            string response = dr["mbEmbossAdi"].ToString().PadRight(30);
            dr.Close();

            return response;
        }

        public void Update(string request, string response)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "update kkThreadTest set response = @response, bitis = getdate() where request = @request";
            sqlCommand.Parameters.Add(new SqlParameter("@request", request));
            sqlCommand.Parameters.Add(new SqlParameter("@response", response));
            sqlCommand.ExecuteNonQuery();
        }

        public void Close()
        {
            sqlConnection.Close();
        }
    }
}
