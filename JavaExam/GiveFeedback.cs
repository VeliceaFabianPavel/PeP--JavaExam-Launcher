using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaExam
{
	public partial class GiveFeedback : Form
	{
		private int currentTaskIndex;
		private bool[] taskStates;
		private string[] feedbacks;

		public GiveFeedback()
		{
			InitializeComponent();
			LoadTaskStates();
			currentTaskIndex = -1;
			feedbacks = new string[taskStates.Length];
			DisplayNextTask();
		}
		private void LoadTaskStates()
		{
			string taskStateFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "taskState.txt");

			if (File.Exists(taskStateFile))
			{
				string[] lines = File.ReadAllLines(taskStateFile);
				taskStates = lines.Select(x => bool.Parse(x)).ToArray();
			}
			else
			{
				MessageBox.Show("Task state file not found.");
				taskStates = new bool[0];
			}
		}

		private void DisplayNextTask()
		{
			while (++currentTaskIndex < taskStates.Length)
			{
				if (taskStates[currentTaskIndex])
				{
					taskLabel.Text = $"Vă rugăm să specificați Feedback-ul pentru task-ul {currentTaskIndex + 1}";
					lblSpec.Text = $"Conținut Task {currentTaskIndex + 1}";

					string pattern = $@"Task {currentTaskIndex + 1}:\s*(.*)";
					string path = @"C:\TaskWorker\TaskCreator\tasks.txt"; // Replace with the path to your task file

					Match match = Regex.Match(File.ReadAllText(path), pattern);

					if (match.Success)
					{
						taskContent.Text = match.Groups[1].Value.Trim();
					}

					return;
				}
			}

			SaveFeedbacks();
			MoveToNextForm();
		}

		private void SaveFeedbacks()
		{
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string javaExamFolder = Path.Combine(desktopPath, "JavaExam");

			if (!Directory.Exists(javaExamFolder))
			{
				Directory.CreateDirectory(javaExamFolder);
			}

			for (int i = 0; i < feedbacks.Length; i++)
			{
				if (!string.IsNullOrWhiteSpace(feedbacks[i]))
				{
					File.WriteAllText(Path.Combine(javaExamFolder, $"feedback{i + 1}.txt"), feedbacks[i]);
				}
			}
		}

		private void MoveToNextForm()
		{
			ty thankYou = new ty();
			thankYou.Show();
			Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (currentTaskIndex >= 0 && currentTaskIndex < taskStates.Length)
			{
				feedbacks[currentTaskIndex] = richTextBox1.Text;
				richTextBox1.Clear();
			}

			DisplayNextTask();
		}

		private void taskContent_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
