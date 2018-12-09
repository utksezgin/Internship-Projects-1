using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{

    class testClass
    {
        //Connection String
        private String connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStringName"].ConnectionString;
        //SqlConnection
        private SqlConnection sqlConn;
        //Form Object
        private frmTest form;

        public testClass(frmTest frm)
        {
            this.form = frm;
        }

        public void cikis()
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                form.Close();
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
                    DataTable returnData = new DataTable();
                    returnData.Load(sqlDataReader); //Closes DataReader
                    return returnData;
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
                    sqlGirisCommand.CommandType = CommandType.StoredProcedure;
                    sqlGirisCommand.CommandText = "sp_InsertTestTable";
                    sqlGirisCommand.Connection = sqlConn;
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
                    sqlUpdateCommand.CommandType = CommandType.StoredProcedure;
                    sqlUpdateCommand.CommandText = "sp_UpdateTestTable";
                    sqlUpdateCommand.Connection = sqlConn;
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

        public DataRow temizle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("LastName");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("Age");
            DataRow newRow = dt.NewRow();
            newRow["ID"] = "";
            newRow["LastName"] = "";
            newRow["FirstName"] = "";
            newRow["Age"] = "";
            return newRow;
        }
    }
}
