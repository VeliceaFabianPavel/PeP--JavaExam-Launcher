using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaExam
{
	public partial class CSV : Form
	{
        public CSV()
        {
            InitializeComponent();

            // Extract CSV content
            string csvContentPattern = @"<csv>(.*?)<\/csv>";
            Match contentMatch = Regex.Match(File.ReadAllText(@"C:\TaskWorker\TaskCreator\tasks.txt"), csvContentPattern, RegexOptions.Singleline);
            string csvContent = contentMatch.Groups[1].Value.Trim();

            // Split into lines
            string[] lines = csvContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Make first line bold
            lines[0] = "" + lines[0] + "";

            // Join lines back together
            csvContent = string.Join(Environment.NewLine, lines);

            // Load into rich text box  
            richTextBox1.Text = csvContent;
            richTextBox1.ReadOnly = true; // Prevent editing
            richTextBox1.ZoomFactor = 1.0f; // Prevent zooming
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}

        private void CSV_Load(object sender, EventArgs e)
        {

        }
    }
}
