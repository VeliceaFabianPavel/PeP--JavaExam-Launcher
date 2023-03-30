using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace JavaExam
{

	public partial class Docker : Form
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

		private const uint SWP_NOZORDER = 0x0004;
		private const uint SWP_SHOWWINDOW = 0x0040;
		private TimeSpan timeLeft;
		public string windowName = "SunAwtFrame";
		public Docker()
		{
			InitializeComponent();
			this.TopMost = true;
			KillExplorer();
			this.StartPosition = FormStartPosition.Manual;
			this.Width = Screen.PrimaryScreen.WorkingArea.Width;
			this.Location = new System.Drawing.Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
			this.Resize += OnFormResize;
			this.Move += OnFormMove;
			this.FormClosed += OnFormClosed;
			BlockWebsites(); // Add this line
			DockAppToTop();
			timeLeft = TimeSpan.FromMinutes(45);
			label1.Text = timeLeft.ToString(@"hh\:mm\:ss");
			timer1.Start();
		}

		private void DockAppToTop()
		{
			IntPtr hWndApp = FindWindow(windowName, null);// For IntelliJ: SunAwtFrame
			if (hWndApp != IntPtr.Zero)
			{
				SetWindowPos(hWndApp, IntPtr.Zero, 0, 0, Screen.PrimaryScreen.WorkingArea.Width,
					Screen.PrimaryScreen.WorkingArea.Height - this.Height, SWP_NOZORDER | SWP_SHOWWINDOW);
			}
			else
			{
				MessageBox.Show("Eclipse IDE not found. Please open Eclipse before running this application.", "Error launching the Exam", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Application.Exit();
			}
		}

		private void OnFormMove(object sender, EventArgs e)
		{
			DockAppToTop();
		}

		private void OnFormResize(object sender, EventArgs e)
		{
			DockAppToTop();
		}
		private void KillExplorer()
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = "/C TASKKILL /F /IM explorer.exe",
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					UseShellExecute = false
				};

				Process.Start(psi);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error killing explorer.exe: " + ex.Message, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}
		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			UnblockWebsites();
			IntPtr hWndApp = FindWindow("SunAwtFrame", null);
			if (hWndApp != IntPtr.Zero)
			{
				SetWindowPos(hWndApp, IntPtr.Zero, 0, 0, Screen.PrimaryScreen.WorkingArea.Width,
					Screen.PrimaryScreen.WorkingArea.Height, SWP_NOZORDER | SWP_SHOWWINDOW);
			}

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{

		}

		private void button8_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}
		private void RestartExplorer()
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					Arguments = "/C START explorer.exe",
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					UseShellExecute = false
				};

				Process.Start(psi);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error restarting explorer.exe: " + ex.Message);
			}
		}
		private void button13_Click(object sender, EventArgs e)
		{

			try
			{
				string url = "https://www.google.com";
				ProcessStartInfo psi = new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true
				};
				System.Diagnostics.Process.Start(psi);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error opening the browser: " + ex.Message);
			}


		}
		private const string HostsFilePath = @"C:\Windows\System32\drivers\etc\hosts";
		private readonly List<string> BlockedWebsites = new List<string>
{
	"www.messenger.com",
	"www.instagram.com",
	"www.facebook.com",
	"web.whatsapp.com",
	"chat.openai.com",
	"www.telegram.com",
	"www.signal.com",
	"www.discord.com",
	"www.github.com",
	"www.reddit.com",
	"www.twitter.com",
	"web.snapchat.com"
};

		private void BlockWebsites()
		{
			try
			{
				var hostsContent = File.ReadAllText(HostsFilePath);

				foreach (var website in BlockedWebsites)
				{
					hostsContent += $"\n127.0.0.1 {website}";
				}

				File.WriteAllText(HostsFilePath, hostsContent);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error blocking websites: " + ex.Message);
			}
		}
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
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (timeLeft > TimeSpan.Zero)
			{
				timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(3));
				label1.Text = timeLeft.ToString(@"hh\:mm\:ss");
			}
			else
			{
				timer1.Stop();
				label1.Text = "00:00:00";
				MessageBox.Show("Time's up!");
			}
		}

		private void Docker_Load(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			RestartExplorer();
			Application.Exit();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you really sure you want to do this?\nThe timer will continue running\nTHIS OPERATION IS IRREVERSIBLE!", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				string sourceFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JavaTemp");
				string destinationFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JavaExam");
				DeleteFolder(destinationFolderPath);
				CopyDirectory(sourceFolderPath, destinationFolderPath);
				Splash2 splash2 = new Splash2();
				splash2.Show();
				//this.Hide();
			}
		}
		private static void DeleteFolder(string folderPath)
		{
			try
			{
				// Check if the folder exists
				if (Directory.Exists(folderPath))
				{
					// Delete the folder and all its contents
					Directory.Delete(folderPath, true);
					Console.WriteLine($"Folder deleted: {folderPath}");
				}
				else
				{
					Console.WriteLine($"Folder not found: {folderPath}");
				}
			}
			catch (IOException ioEx)
			{
				Console.WriteLine($"Error deleting folder: {ioEx.Message}");
			}
			catch (UnauthorizedAccessException uaEx)
			{
				Console.WriteLine($"Access denied: {uaEx.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
		private static void CopyDirectory(string sourcePath, string destinationPath)
		{
			// Check if the source folder exists
			if (!Directory.Exists(sourcePath))
			{
				Console.WriteLine($"Source folder not found: {sourcePath}");
				return;
			}

			// Create the destination folder if it doesn't exist
			if (!Directory.Exists(destinationPath))
			{
				Directory.CreateDirectory(destinationPath);
			}

			// Copy files in the source folder to the destination folder
			foreach (string filePath in Directory.GetFiles(sourcePath))
			{
				string fileName = Path.GetFileName(filePath);
				string destFilePath = Path.Combine(destinationPath, fileName);
				File.Copy(filePath, destFilePath, true);
			}

			// Recursively copy subfolders in the source folder to the destination folder
			foreach (string folderPath in Directory.GetDirectories(sourcePath))
			{
				string folderName = Path.GetFileName(folderPath);
				string destFolderPath = Path.Combine(destinationPath, folderName);
				CopyDirectory(folderPath, destFolderPath);
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			UnblockWebsites();
		}
	}

}