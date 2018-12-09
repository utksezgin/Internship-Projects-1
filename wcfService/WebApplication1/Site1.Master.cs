using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            Response.Redirect("database.aspx");
        }

        protected void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void btnUyeGirisi_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void btnCikis_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Response.Redirect("home.aspx");
        }
    }
}