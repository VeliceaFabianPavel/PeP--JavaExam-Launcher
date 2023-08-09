using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO; // Added
using System.Windows.Forms;

namespace JavaExam
{
	public partial class Form1 : Form
	{
		private const string HostsFilePath = @"C:\Windows\System32\drivers\etc\hosts";
		private readonly List<string> BlockedWebsites = new List<string>
{
	"www.youtube.com"
};
		private void UnblockWebsites()
		{
			try
			{
				var hostsContent = File.ReadAllText(HostsFilePath);
				var lines = hostsContent.Split('\n');
				hostsContent = string.Join("\n", lines.Where(line => !BlockedWebsites.Any(website => line.Contains(website))));

				File.WriteAllText(HostsFilePath, hostsContent);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error unblocking websites: " + ex.Message);
			}
		}
		public Form1()
		{
			InitializeComponent();

		}
		private void button1_Click(object sender, EventArgs e)
		{
			string usersFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "users.txt");
			string tasksFile = @"C:\TaskWorker\TaskCreator\tasks.txt";
			string javaExamFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"JavaExam\JavaExam\src");
			string outputFile = @"C:\TaskWorker\TaskEvaluator\input.txt";

			string[] usersFileLines = File.ReadAllLines(usersFile);
			string tasksFileContent = File.ReadAllText(tasksFile);

			string fullName = usersFileLines[0].Substring(7) + " " + usersFileLines[1].Substring(7);

			string csvHeader = Regex.Match(tasksFileContent, @"<csv>\s*?(.*?)\s*?").Groups[1].Value;

			string inputContent = $@"Student Name: {fullName}
College: {usersFileLines[2].Substring(9)}
Profile: {usersFileLines[3].Substring(9)}
Group: {Regex.Match(tasksFileContent, @"Group:\s*(.*)").Groups[1].Value}

CSV File name: {Regex.Match(tasksFileContent, @"CSV file:\s*(.*)").Groups[1].Value}
CSV Header: {csvHeader}

Task 1: {Regex.Match(tasksFileContent, @"Task 1:\s*(.*)").Groups[1].Value}
Task 2: {Regex.Match(tasksFileContent, @"Task 2:\s*(.*)").Groups[1].Value}
Task 3: {Regex.Match(tasksFileContent, @"Task 3:\s*(.*)").Groups[1].Value}
Task 4: {Regex.Match(tasksFileContent, @"Task 4:\s*(.*)").Groups[1].Value}

";

			DirectoryInfo javaSrcFolder = new DirectoryInfo(javaExamFolder);

			foreach (FileInfo javaFile in javaSrcFolder.GetFiles("*.java"))
			{
				string className = javaFile.Name;
				string classContent = File.ReadAllText(javaFile.FullName);

				inputContent += $@"Class {className}:

{classContent}

{Environment.NewLine}{Environment.NewLine}";
			}

			File.WriteAllText(outputFile, inputContent);
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
		private void button2_Click(object sender, EventArgs e)
		{
			UnblockWebsites();
		}
	}
}
