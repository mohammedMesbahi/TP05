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
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data source=NORD\MSSQLSERVER01;initial catalog=ADONET; integrated security =true;");
        SqlCommand command;
        string queryString;
        SqlDataReader reader;
        private void Form1_Load(object sender, EventArgs e)
        {
            queryString = "select count(*) from citizens";
            command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                int numberOfRows = (Int32)command.ExecuteScalar();
                if (numberOfRows == 0)
                {
                    btnDelete.Enabled = false;
                    btnDisplayAll.Enabled = false;
                    btnUpDate.Enabled = false;
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("an Error accured");
            }
        }
        private bool IsinputsValid()
        {
            // Check for input in the Order ID text box.
            if (textBoxCin.Text == "")
            {
                MessageBox.Show("Please specify the CIN.");
                return false;
            }

            // Check for characters other than integers.
            else if (textBoxFirstName.Text == "")
            {
                // Show message and clear input.
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

        private void btnUpDate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("you clicked me");
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
                    queryString = "INSERT INTO citizens VALUES(@CIN,@FirstName,@LastName);";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@CIN", SqlDbType.VarChar);
                    command.Parameters["@CIN"].Value = textBoxCin.Text;
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters["@FirstName"].Value = textBoxFirstName.Text;
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters["@LastName"].Value = textBoxLastName.Text;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        queryString = "select * from citizens";
                        command = new SqlCommand(queryString, connection);
                        reader = command.ExecuteReader();
                        // Create a data table to hold the retrieved data.
                        DataTable dataTable = new DataTable();

                        // Load the data from SqlDataReader into the data table.
                        dataTable.Load(reader);

                        // Display the data from the data table in the data grid view.
                        this.dataGridView1.DataSource = dataTable;

                        // Close the SqlDataReader.
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("an Error accured");
                    }
                }


            }
            else
            {
                // Do something  

            }

        }
    }
}
