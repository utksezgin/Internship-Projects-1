using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace wcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private string connString;
        private SqlConnection sqlConn;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string test(string id)
        {
            return id;
        }

        public Service1()
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
        public void giris(int id, string lastname, string firstname, int age)
        {
            try
            {
                using (sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlGirisCommand = new SqlCommand();
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@ID", id));
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@LastName", lastname));
                    sqlGirisCommand.Parameters.Add(new SqlParameter("@FirstName", firstname));
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
        public void guncelle(int id, string lastname, string firstname, int age)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlUpdateCommand = new SqlCommand();
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@ID", id));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@LastName", lastname));
                    sqlUpdateCommand.Parameters.Add(new SqlParameter("@FirstName", firstname));
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
        public void sil(int id, string lastname, string firstname, int age)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();

                    SqlCommand sqlDelete = new SqlCommand();
                    sqlDelete.Parameters.Add(new SqlParameter("@ID", id));
                    sqlDelete.Parameters.Add(new SqlParameter("@LastName", lastname));
                    sqlDelete.Parameters.Add(new SqlParameter("@FirstName", lastname));
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
