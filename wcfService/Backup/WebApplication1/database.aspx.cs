using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ServiceReference1;
using System.Data;

namespace WebApplication1
{
    public partial class database : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public database()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("login.aspx");
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
                Service1Client client = new Service1Client();
                client.giris(id, lastName, firstName, age);
                client.Close();

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
                Service1Client client = new Service1Client();;
                DataTable kayit = client.getir(id);
                client.Close();

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
                Service1Client client = new Service1Client();
                client.guncelle(id, lastName, firstName, age);
                client.Close();

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
                Service1Client client = new Service1Client();
                client.sil(id, lastName, firstName, age);
                client.Close();

                lblHata.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnTemizle_Click(object sender, EventArgs e)
        {
            Response.Redirect("database.aspx");
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("login.aspx");
        }

        private void showError(string errMsg)
        {
            lblHata.Text = errMsg;
        }
    }
}