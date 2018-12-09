using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace sqlDLL
{
    public class SqlDLL
    {
        private String connString;
        private SqlConnection sqlConn;

        public SqlDLL()
        {
            try
            {
                connString = ConfigurationManager.ConnectionStrings["ConnStringName"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getir(string id)
        {
            try
            {
                using (sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlCom = new SqlCommand();
                    sqlCom.Connection = sqlConn;
                    sqlCom.CommandType = CommandType.StoredProcedure;
                    sqlCom.CommandText = "sp_SelectTestTable";
                    sqlCom.Parameters.Add(new SqlParameter("@ID", id));

                    SqlDataReader sqlDataReader = sqlCom.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(sqlDataReader); //Closes DataReader

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void giris(int id, string lastName, string firstName, int age)
        {
            try
            {
                using (sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlGirisCommand = new SqlCommand();
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@ID", id));
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@LastName", lastName));
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@Age", age));

                    sqlGirisCommand.Connection = sqlConn;
                    sqlGirisCommand.CommandType = CommandType.StoredProcedure;
                    sqlGirisCommand.CommandText = "sp_InsertTestTable";
                    
                    sqlGirisCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void guncelle(int id, string lastName, string firstName, int age)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlUpdateCommand = new SqlCommand();
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@ID", id));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@LastName", lastName));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@Age", age));

                    sqlUpdateCommand.Connection = sqlConn;
                    sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                    sqlUpdateCommand.CommandText = "sp_UpdateTestTable";
                    
                    sqlUpdateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void sil(int id, string lastName, string firstName, int age)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlDelete = new SqlCommand();
                    sqlDelete.Parameters.Add(new SqlParameter("@ID", id));
                    sqlDelete.Parameters.Add(new SqlParameter("@LastName", lastName));
                    sqlDelete.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    sqlDelete.Parameters.Add(new SqlParameter("@Age", age));

                    sqlDelete.Connection = sqlConn;
                    sqlDelete.CommandType = CommandType.StoredProcedure;
                    sqlDelete.CommandText = "sp_DeleteTestTable";

                    sqlDelete.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
