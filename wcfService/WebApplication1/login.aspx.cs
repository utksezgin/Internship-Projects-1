using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using WebApplication1.ServiceReference1;


namespace WebApplication1
{
    public partial class login : System.Web.UI.Page
    {
        string strConn;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                strConn = WebConfigurationManager.ConnectionStrings["connStringName"].ConnectionString;
            }
            catch (Exception ex)
            {
                lblHata.Text = ex.Message;
            }
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtPw.Text == "")
            {
                lblHata.Text = "Username or Password cannot be empty.";
                return;
            }

            string username = txtID.Text;
            string password = txtPw.Text;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(strConn))
                {
                    sqlConn.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "SELECT * FROM auth WHERE Username = @Username";
                    sqlCommand.Parameters.Add(new SqlParameter("@Username", username));
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    sqlReader.Read();
                    if (sqlReader["Password"].ToString() == encodePassword(password))
                    {
                        Session["UserName"] = username;
                        Response.Redirect("database.aspx");
                    }
                    else
                    {
                        lblHata.Text = "Unknown Username or Password";
                    }
                }
            }
            catch (Exception ex)
            {
                lblHata.Text = ex.Message;
            }
        }

        protected void btnYeni_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "" || txtPw.Text == "")
            {
                lblHata.Text = "Username or Password cannot be empty.";
                return;
            }

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(strConn))
                {
                    sqlConn.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConn;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "INSERT INTO auth (Username, Password) VALUES (@Username, @Password)";
                    sqlCommand.Parameters.Add(new SqlParameter("@Username", txtID.Text));
                    sqlCommand.Parameters.Add(new SqlParameter("@Password", encodePassword(txtPw.Text)));
                    sqlCommand.ExecuteNonQuery();
                    lblHata.Text = "New user created";
                }
            }
            catch (Exception ex)
            {
                lblHata.Text = ex.Message;
            }
        }
        private string encodePassword(string str)
        {
            SHA256Managed sha = new SHA256Managed();

            byte[] passwordByte = Encoding.ASCII.GetBytes(str);
            byte[] hashByte = sha.ComputeHash(passwordByte);
            string passwordChar = Convert.ToBase64String(hashByte);

            return passwordChar;
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("home.aspx");
        }
    }
}