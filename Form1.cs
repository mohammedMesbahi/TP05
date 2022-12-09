using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ADO.NET_PROJECT
{
    public partial class Form1 : Form
    {
        Mysqlconn mysqlconn = new Mysqlconn();
        SqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reload();
        }
        

        private void btnUpDate_Click(object sender, EventArgs e)
        {
            string message = "Do you want to continue?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == DialogResult.Yes)
            {
                if (IsCinValid())
                {
                    mysqlconn.upDateRecord(textBoxCin.Text, textBoxFirstName.Text, textBoxLastName.Text);
                    reader = mysqlconn.getAllRecords();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    this.dataGridView1.DataSource = dataTable;
                    reader.Close();
                    clearInputs();
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string message = "Do you want to continue?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (IsinputsValid())
                {

                    mysqlconn.insertRecord(textBoxCin.Text, textBoxFirstName.Text, textBoxLastName.Text);
                    reader = mysqlconn.getAllRecords();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    this.dataGridView1.DataSource = dataTable;
                    reader.Close();
                    clearInputs();
                    reload();
                }


            }
        }

        private void reload()
        {

            int numberOfRows = mysqlconn.numberOfRecords();
            if (numberOfRows == 0)
            {
                btnDelete.Enabled = false;
                btnDisplayAll.Enabled = false;
                btnUpDate.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnDisplayAll.Enabled = true;
                btnUpDate.Enabled = true;
            }
        }

        private void btnDisplayAll_Click(object sender, EventArgs e)
        {
            reader = mysqlconn.getAllRecords();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            this.dataGridView1.DataSource = dataTable;
            reader.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "Do you want to continue?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == DialogResult.Yes)
            {
                if (IsCinValid())
                {
                    mysqlconn.deleteRecord(textBoxCin.Text);
                    reader = mysqlconn.getAllRecords();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    this.dataGridView1.DataSource = dataTable;
                    reader.Close();
                    clearInputs();
                    reload();
                }
            }
        }
        public void clearInputs()
        {
            textBoxCin.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
        }
        private bool IsinputsValid()
        {
            if (textBoxCin.Text == "")
            {
                MessageBox.Show("Please specify the CIN.");
                return false;
            }
            else if (textBoxFirstName.Text == "")
            {
                MessageBox.Show("Please specify the First name");
                return false;
            }
            else if (textBoxLastName.Text == "")
            {
                MessageBox.Show("Please specify the Last name");
                return false;
            }
            else
                return true;
        }
        private bool IsCinValid()
        {
            if (textBoxCin.Text == "")
            {
                MessageBox.Show("Please specify the CIN.");
                return false;
            }
            else return true;
        }
    }
}
