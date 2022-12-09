using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO.NET_PROJECT
{
    class Mysqlconn
    {
        private SqlConnection connection;
        private string queryString;
        private String source = "localhost";
        private String database = "ADONET";

        public Mysqlconn()
        {
            createConn();
        }
        public SqlConnection getConnection()
        {
            return connection;
        }
        public void upDateRecord(String Cin, String FirstName, String LastName)
        {
            queryString = "UPDATE citizens SET FirstName=@FirstName,LastName=@LastName where CIN=@CIN;";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@CIN", SqlDbType.VarChar);
            command.Parameters["@CIN"].Value = Cin;
            command.Parameters.Add("@FirstName", SqlDbType.VarChar);
            command.Parameters["@FirstName"].Value = FirstName;
            command.Parameters.Add("@LastName", SqlDbType.VarChar);
            command.Parameters["@LastName"].Value = LastName;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error");
                throw;
            }
        }
        public void insertRecord(String Cin, String FirstName, String LastName)
        {
            queryString = "INSERT INTO citizens VALUES(@CIN,@FirstName,@LastName);";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@CIN", SqlDbType.VarChar);
            command.Parameters["@CIN"].Value = Cin;
            command.Parameters.Add("@FirstName", SqlDbType.VarChar);
            command.Parameters["@FirstName"].Value = FirstName;
            command.Parameters.Add("@LastName", SqlDbType.VarChar);
            command.Parameters["@LastName"].Value = LastName;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error");
                throw;

            }
        }
        public SqlDataReader getAllRecords()
        {
            queryString = "select * from citizens";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader;

            try
            {
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error");
                throw;
            }
            return reader;
        }
        public int numberOfRecords()
        {
            queryString = "select count(*) from citizens";
            int numberOfRows;
            try
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                numberOfRows = (Int32)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error here");
                Console.WriteLine(ex.Message);
                throw;
            }
            return numberOfRows;
        }
        public void deleteRecord(String cin)
        {
            queryString = "DELETE citizens where CIN=@CIN;";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add("@CIN", SqlDbType.VarChar);
            command.Parameters["@CIN"].Value = cin;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
                Console.WriteLine(ex.Message);
                throw;

            }
        }
        private void createConn()
        {
            connection = new SqlConnection(@"Data source=" + source + ";initial catalog=" + database + ";integrated security =true;");
            connection.Open();

        }
    }
}
