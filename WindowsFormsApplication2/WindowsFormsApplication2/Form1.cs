using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TestDLL;

namespace WindowsFormsApplication2
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("ID cannot be empty.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("ID cannot be empty.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "")
            {
                MessageBox.Show("ID cannot be empty.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            if (txtID.Text == "")
            {
                MessageBox.Show("ID cannot be empty.");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtAge.Text = "";
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                this.Close();
        }
    }
}
