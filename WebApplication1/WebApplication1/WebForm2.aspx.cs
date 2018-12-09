using System;
using System.Data;
using TestDLL;
using System.Web.Configuration;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        string strConn;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null)
                    Response.Redirect("WebForm3.aspx");

                strConn = WebConfigurationManager.ConnectionStrings["connStringName"].ConnectionString;
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                showError("ID cannot be empty");
                return;
            }

            int id = Convert.ToInt32(txtID.Text);
            string lastName = txtLastName.Text;
            string firstName = txtFirstName.Text;

            if (txtAge.Text == "")
                txtAge.Text = "0";
            int age = Convert.ToInt32(txtAge.Text);

            try
            {
                TestDLLClass testDLLClass = new TestDLLClass();
                testDLLClass.giris(id, lastName, firstName, age);
                lblHata.Text = "";
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        protected void btnGetir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                showError("ID cannot be empty");
                return;
            }

            string id = txtID.Text;

            try
            {
                TestDLLClass testDLLClass = new TestDLLClass();
                DataTable kayit = testDLLClass.getir(id);
                txtID.Text = (kayit.Rows[0]["ID"].ToString());
                txtLastName.Text = (kayit.Rows[0]["LastName"].ToString());
                txtFirstName.Text = (kayit.Rows[0]["FirstName"].ToString());
                txtAge.Text = (kayit.Rows[0]["Age"].ToString());
                lblHata.Text = "";
            }
            catch (Exception ex)
            {
                showError(ex.Message);
            }
        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                showError("ID cannot be empty");
                return;
            }

            int id = Convert.ToInt32(txtID.Text);
            string lastName = txtLastName.Text;
            string firstName = txtFirstName.Text;
            if (txtAge.Text == "")
                txtAge.Text = "0";
            int age = Convert.ToInt32(txtAge.Text);

            try
            {
                TestDLLClass testDLLClass = new TestDLLClass();
                testDLLClass.guncelle(id, lastName, firstName, age);
                lblHata.Text = "";
            }
            catch (Exception ex)
            {
                showError(ex.Message);
                return;
            }
        }

        protected void btnSil_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "")
            {
                showError("ID cannot be empty");
                return;
            }

            int id = Convert.ToInt32(txtID.Text);
            string lastName = txtLastName.Text;
            string firstName = txtFirstName.Text;
            if (txtAge.Text == "")
                txtAge.Text = "0";
            int age = Convert.ToInt32(txtAge.Text);

            try
            {
                TestDLLClass testDLLClass = new TestDLLClass();
                testDLLClass.sil(id);
                lblHata.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect("webform2.aspx");
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("webform3.aspx");
        }

        private void showError(string errMsg)
        {
            lblHata.Text = errMsg;
        }
    }
}