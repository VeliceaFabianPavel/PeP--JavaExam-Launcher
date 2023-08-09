using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Runtime.InteropServices;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Text.RegularExpressions;

namespace JavaExam
{
	public partial class Splash : Form
	{
		private string pythonPath = @"C:\TaskWorker\TaskCreator\venv\Scripts\python.exe";
		private string scriptPath = @"C:\TaskWorker\TaskCreator\TaskCreator.py";
		
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		private const int SW_MAXIMIZE = 3;

		private void MaximizeIntelliJEditorWindow()
		{
			IntPtr hWndIntelliJ = IntPtr.Zero;

			while (hWndIntelliJ == IntPtr.Zero)
			{
				hWndIntelliJ = FindWindow("SunAwtFrame", null);
				Thread.Sleep(500); // Wait for 0.5 seconds before checking again
			}

			ShowWindow(hWndIntelliJ, SW_MAXIMIZE);
		}

		public Splash()
		{
			InitializeComponent();
            this.Show(); // Show form
            this.pictureBox2.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("SplashLogo");
			RunScriptInBackground();
			OpenIntelliJWithProject();
		}
		private void ShowDockerFormAndCloseSplash()
		{
			Docker dockerForm = new Docker();
			dockerForm.Show();
			Hide();
		}
		private void OpenIntelliJWithProject()
		{	
			string projectPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "JavaExam");
			string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IJPath.txt");
			string textRead="";
			try
			{
				textRead = File.ReadAllText(filePath);
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}

			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = textRead,
				Arguments = $"\"{projectPath}\"",
				WindowStyle = ProcessWindowStyle.Maximized
			};
			Process.Start(psi);
			Thread maximizeThread = new Thread(MaximizeIntelliJEditorWindow);
			maximizeThread.Start();
		}

        private void RunScriptInBackground()
        {
            Task.Run(() =>
            {
                RunPythonScript(pythonPath, scriptPath);
                this.Invoke((Action)delegate {
                    Hide(); // Close splash form

                    Docker dockerForm = new Docker();
                    dockerForm.Show();
                });
            });
        }

        private Task<int> RunPythonScript(string pythonPath, string scriptPath)
		{
			TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

			using (Process process = new Process())
			{
				process.StartInfo.FileName = pythonPath;
				process.StartInfo.Arguments = scriptPath;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;

				process.EnableRaisingEvents = true;
				process.Exited += (sender, args) =>
				{
					tcs.SetResult(process.ExitCode);
					process.Dispose();
				};

				process.Start();

				string output = process.StandardOutput.ReadToEnd();
				string error = process.StandardError.ReadToEnd();
			}

			return tcs.Task;
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{

		}

		private void Splash_Load(object sender, EventArgs e)
		{

		}
	}
}