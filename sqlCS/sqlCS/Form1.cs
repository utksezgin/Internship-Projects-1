using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace sqlCS
{
    public partial class Form1 : Form
    {
        //Connection String
        private String conString = "Server=BSS02;Database=BPersoEgitim;Trusted_Connection=True;";
        private DataTable dataTable;
        private SqlDataAdapter dataAdapter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSource1 = new BindingSource();
            //Binding DataGridView's Data Source to BindingSource
            SQLData.DataSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getData("SELECT * FROM testTable");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            //Updates the Dataabase
            try
            {
                int updateReturn = dataAdapter.Update((DataTable)bindingSource1.DataSource);
                //If there were any changes.
                if(updateReturn > 0)
                    MessageBox.Show("Changes are successfully saved into the database");
                else
                    MessageBox.Show("There were no changes made on the table");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }
        //Gets data from the database table. **Table must have a primary key to work**
        private void getData(String selectQuery)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(conString);
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand(selectQuery, sqlCon);
                sqlCom.CommandType = CommandType.Text;
                dataAdapter = new SqlDataAdapter(sqlCom);
                //Creating a command builder
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                //Creating a new data table
                dataTable = new DataTable();
                //Filling the table from Database
                dataAdapter.Fill(dataTable);
                //Binding dataTable to bindingSource1
                bindingSource1.DataSource = dataTable;
                SQLData.DataSource = dataTable;

                //Resizes the table.
                SQLData.AutoResizeColumns();
                SQLData.AutoResizeRows();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
//Table Name: testTable