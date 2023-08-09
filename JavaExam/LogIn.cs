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
			if(textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "" && textBox3.Text != "")
			{ 
			Users data = new Users
			{
				FName = textBox2.Text,
				LName = textBox1.Text,
				Faculty = "IESC",
				Spec = TrimStringFromCharacter(comboBox1.Text, '('),
				Year = "II",
				Group = textBox3.Text
			};
			FirstCheck firstCheck = new FirstCheck();
			firstCheck.Data = data;
			firstCheck.UpdateLabels();

			string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string filePath = Path.Combine(appDataFolder, "users.txt");

			try
			{
				File.WriteAllText(filePath, data.LName + "\n" + data.FName + "\n" + data.Faculty + "\n" + data.Spec + "\n" + data.Group);
			}
			catch (IOException ex)
			{
				MessageBox.Show("Error writing to file:");
				Console.WriteLine(ex.Message);
			}

			firstCheck.Show();
			Hide();
		}
			else
			{
				MessageBox.Show("Nu poti lasa spatii necompletate!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
    }
}
