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
	public partial class FirstCheck : Form
	{
		public Users Data { get; set; }
		public FirstCheck()
		{
			InitializeComponent();
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Checking checking = new Checking();
			checking.Show();
			Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Hide();
			LogIn logIn = new LogIn();
			logIn.Show();
		}
		public void UpdateLabels()
		{
			if (Data != null)
			{
				lblNume.Text = Data.LName;
				lblPrenume.Text = Data.FName;
				lblFacultate.Text = Data.Faculty;
				lblSpecializare.Text = Data.Spec;
				lblAn.Text = Data.Year;
				lblGrupa.Text = Data.Group;
			}
			else
			{
				lblNume.Text = "NO DATA RECEIVED";
				lblPrenume.Text = "NO DATA RECEIVED";
				lblSpecializare.Text = "NO DATA RECEIVED";
				lblFacultate.Text = "NO DATA RECEIVED";
				lblAn.Text = "NO DATA RECEIVED";
				lblGrupa.Text = "NO DATA RECEIVED";
			}
		}
		private void FirstCheck_Load(object sender, EventArgs e)
		{
			
		}
	}
}
