using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaExam
{
	public partial class IntelliJVersionSelector : Form
	{
		public IntelliJVersionSelector()
		{
			InitializeComponent();
		}

		private void IntelliJVersionSelector_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				// Set the filter for .exe files
				openFileDialog.Filter = "Executable Files (*.exe)|*.exe";
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					// Get the selected file's full path
					string filePath = openFileDialog.FileName;

					// Display the full path in the label
					textBox1.Text = filePath;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string filePath = Path.Combine(appDataFolder, "IJPath.txt");
			string textToWrite = textBox1.Text;


			if (textBox1.Text!="" && textBox1.Text.EndsWith("idea64.exe")==true)
			{
				// Write to the .txt file
				try
				{
					File.WriteAllText(filePath, textToWrite);				
					ProctorLogin pl = new ProctorLogin();
					pl.Show();
					Hide();
				}
				catch (IOException ex)
				{
					MessageBox.Show("Error writing to file:");
					Console.WriteLine(ex.Message);
				}

			}
			else
			{
				MessageBox.Show("Invalid IntelliJ Path", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				textBox1.Text = "";
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

        private void IntelliJVersionSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
			Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
