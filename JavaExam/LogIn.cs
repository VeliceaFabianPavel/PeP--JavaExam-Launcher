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
			Users data = new Users
			{
				FName=textBox2.Text,
				LName=textBox1.Text,
				Faculty="IESC",
				Spec=TrimStringFromCharacter(comboBox1.Text,'('),
				Year="II",
				Group=textBox3.Text
			};
			FirstCheck firstCheck = new FirstCheck();
			firstCheck.Data= data;
			firstCheck.UpdateLabels();
			firstCheck.Show();
			Hide();
		}
	}
}
