using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZulLabel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.AppSettings["str"];
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM tbl_login WHERE Username = @Username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Username", "admin");

                        // Execute the query and get the result
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                           // Console.WriteLine("User with username 'admin' exists.");
                        }
                        else
                        {
                            using (SqlCommand insertCommand = new SqlCommand("INSERT INTO tbl_login (Username, Password) VALUES (@Username, @Password)", connection))
                            {
                                // Add parameters to the insert query
                                insertCommand.Parameters.AddWithValue("@Username", "admin");
                                insertCommand.Parameters.AddWithValue("@Password", "pass");

                                // Execute the insert query
                                int rowsAffected = insertCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    //Console.WriteLine("User with username 'admin' created successfully.");
                                }
                                else
                                {
                                    //Console.WriteLine("Failed to create user with username 'admin'.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text;
            string password = txtpassword.Text;

            if (AuthenticateUser(username, password))
            {
                // Authentication successful, open the MainForm
                PrintForm mainForm = new PrintForm();
               

                // Optionally, you can hide the login form
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM tbl_login WHERE Username = @Username AND Password = @Password", connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
