using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace JavaExam
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
			InitializeComponent();
            this.pictureBox1.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("SplashLogo");
            this.pictureBox2.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("brandLogo");
        }
		

		public string connectionString = @"Server=tcp:licente.database.windows.net,1433;Initial Catalog=db;Persist Security Info=False;User ID=gabi;Password=Parola12;TrustServerCertificate=False;Connection Timeout=30;";


    private void button1_Click(object sender, EventArgs e)
		{

		}

		private void label11_Click(object sender, EventArgs e)
		{

		}

		private void label13_Click(object sender, EventArgs e)
		{

		}

		private void label12_Click(object sender, EventArgs e)
		{

		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
		}
		static string TrimStringFromCharacter(string input, char character)
		{
			int indexOfCharacter = input.IndexOf(character);
			return indexOfCharacter >= 0 ? input.Substring(0, indexOfCharacter).Trim() : input;
		}
		private void button1_Click_1(object sender, EventArgs e)
		{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Studenti WHERE Email=@Email AND Password=@Password";  // Adjust table name if needed

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", textBox1.Text);
                    // Ideally, hash the password and then compare. For now, assuming plain text:
                    command.Parameters.AddWithValue("@Password", textBox2.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Studenti loggedInStudent = new Studenti
                            {
                                StudnetId = (int)reader["StudnetId"],
                                ProctorId = (int)reader["ProctorId"],
                                Email = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Faculty = reader["Faculty"].ToString(),
                                Year = reader["Year"].ToString(),
                                Groupa = reader["Groupa"].ToString(),
                            };
                            GlobalUser.LoggedInUser = loggedInStudent;
                            FirstCheck fc = new FirstCheck();
                            fc.Show();
                            Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid login credentials.");
                        }
                    }
                }
            }
        }

		private void LogIn_Load(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
			Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
