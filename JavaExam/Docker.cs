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
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Intrinsics.X86;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Win32;
using GlobalHotKey;

namespace JavaExam
{

	public partial class Docker : Form
	{
		public string Overview="";
		public string Task1="";
		public string Task2="";
		public string Task3="";
		public string Task4= "";
		public string path = @"C:\TaskWorker\TaskCreator\tasks.txt";
		public string fileContent = File.ReadAllText(@"C:\TaskWorker\TaskCreator\tasks.txt");
		private const string registryPath = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
		public int selectedTask = 0;
		public bool com1=false;
		public bool com2=false;
		public bool com3=false;
		public bool com4=false;
		public bool mark1=false;
		public bool mark2=false;
		public bool mark3= false;
		public bool mark4 = false;
		public bool feed1 = false;
		public bool feed2 = false;
		public bool feed3 = false;
		public bool feed4 = false;
		public int numberFeedback = 0;
		public int numberCompleted = 0;
		public int numberMarked = 0;
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

		private const uint SWP_NOZORDER = 0x0004;
		private const uint SWP_SHOWWINDOW = 0x0040;
		private TimeSpan timeLeft;
		public string windowName = "SunAwtFrame";
		public bool LockExit = true;
		public Docker()
		{
			InitializeComponent();
			CreateCsvFile(fileContent);
			selectedTask = 0;
			LockKeys();
			//this.brandImage .Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("brandLogo");
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
			string pattern = @"Overview:(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern, RegexOptions.Singleline);

			if (match.Success)
			{
				Overview = match.Groups[1].Value.Trim();
				richTextBox1.Text = Overview;
			}
            string pattern2 = @"Domain:\s*(.*)";
            Match match2 = Regex.Match(File.ReadAllText(path), pattern2);

            if (match2.Success)
            {
                Overview = match2.Groups[1].Value.Trim();
                label4.Text = Overview;
            }

            timeLeft = TimeSpan.FromMinutes(60);
			label1.Text = timeLeft.ToString(@"hh\:mm\:ss");
			timer1.Start();
		}

		private void DockAppToTop()
		{
			IntPtr hWndApp = FindWindow("SunAwtFrame", null);// For IntelliJ: SunAwtFrame
			if (hWndApp != IntPtr.Zero)
			{
				SetWindowPos(hWndApp, IntPtr.Zero, 0, 0, Screen.PrimaryScreen.WorkingArea.Width,
					Screen.PrimaryScreen.WorkingArea.Height - this.Height, SWP_NOZORDER | SWP_SHOWWINDOW);
			}
			else
			{
				MessageBox.Show("IntelliJ not found. Please open IntelliJ before running this application.", "Error launching the Exam", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				Application.Exit();
			}
		}

		public void LockKeys()
		{
			using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath))
			{
				if (key != null)
				{
					key.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
				}
			}
		}

		public void UnlockKeys()
		{
			using (RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath))
			{
				if (key != null)
				{
					key.SetValue("DisableTaskMgr", 0, RegistryValueKind.DWord);
				}
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
			selectedTask = 2;
			string pattern = @"Task 2:\s*(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern);

			if (match.Success)
			{
				Task2 = match.Groups[1].Value.Trim();
				richTextBox1.Text = Task2;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			if (selectedTask == 0)
				button3.BackColor = Color.White;
			if (selectedTask == 1 && com1 == false)
			{
				button4.BackColor = Color.Lime;
				com1 = true;
				numberCompleted++;
			}
			else if (selectedTask == 1 && com1 == true)
			{
				button4.BackColor = Color.White;
				com1 = false;
				numberCompleted--;
			}

			if (selectedTask == 2 && com2 == false)
			{
				button5.BackColor = Color.Lime;
				com2 = true;
				numberCompleted++;
			}
			else if (selectedTask == 2 && com2 == true)
			{
				button5.BackColor = Color.White;
				com2 = false;
				numberCompleted--;
			}

			if (selectedTask == 3 && com3 == false)
			{
				button6.BackColor = Color.Lime;
				com3 = true;
				numberCompleted++;
			}
			else if (selectedTask == 3 && com3 == true)
			{
				button6.BackColor = Color.White;
				com3 = false;
				numberCompleted--;
			}

			if (selectedTask == 4 && com4 == false)
			{
				button7.BackColor = Color.Lime;
				com4 = true;
				numberCompleted++;
			}
			else if (selectedTask == 4 && com4 == true)
			{
				button7.BackColor = Color.White;
				com4 = false;
				numberCompleted--;
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (mark1 == true || mark2 == true || mark3 == true || mark4 == true)
			{
				if (MessageBox.Show("There are tasks marked for review. Are you sure do you want to continue submitting the project?\nThere is no way back", "IMPORTANT INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (feed1 == true || feed2 == true || feed3 == true || feed4 == true)
					{
						string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
						string filePath = Path.Combine(appDataFolder, "taskState.txt");
						try
						{
							File.WriteAllText(filePath, feed1 + "\n" + feed2 + "\n" + feed3 + "\n" + feed4);
							GiveFeedback gf = new GiveFeedback();
							gf.Show();
							UnlockKeys();
							LockExit = false;
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
						ty ps = new ty();
						ps.Show();
						UnlockKeys();
						LockExit = false;
						Hide();

					}
				}
			}
			else if((mark1==false && mark2==false && mark3==false && mark4==false) && (feed1 == true || feed2 == true || feed3 == true || feed4 == true))
			{
				if (MessageBox.Show("Are you sure do you want to continue submitting the project?\nThere is no way back", "IMPORTANT INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
					string filePath = Path.Combine(appDataFolder, "taskState.txt");
					try
					{
						File.WriteAllText(filePath, feed1 + "\n" + feed2 + "\n" + feed3 + "\n" + feed4);
						GiveFeedback gf = new GiveFeedback();
						UnlockKeys();
						gf.Show();
						LockExit = false;
						Hide();
					}
					catch (IOException ex)
					{
						MessageBox.Show("Error writing to file:");
						Console.WriteLine(ex.Message);
					}
				}
			}
			else
			{
				ty ps = new ty();
				ps.Show();
				UnlockKeys();
				LockExit = false;
				Hide();
			}
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
	"www.github.com",
	"www.tinder.com",
	"www.viber.com",
	"web.snapchat.com",
	"www.youtube.com",
	"platform.openai.com",
	""
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
				timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
				label1.Text = timeLeft.ToString(@"hh\:mm\:ss");
			}
			else
			{
				timer1.Stop();
				label1.Text = "00:00:00";
				MessageBox.Show("Timpul alocat desfasurarii examenului a expirat!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
				
					if (mark1 == true || mark2 == true || mark3 == true || mark4 == true)
					{
						if (MessageBox.Show("There are tasks marked for review. Are you sure do you want to continue submitting the project?\nThere is no way back", "IMPORTANT INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							if (feed1 == true || feed2 == true || feed3 == true || feed4 == true)
							{
								string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
								string filePath = Path.Combine(appDataFolder, "taskState.txt");
								try
								{
									File.WriteAllText(filePath, feed1 + "\n" + feed2 + "\n" + feed3 + "\n" + feed4);
									GiveFeedback gf = new GiveFeedback();
									gf.Show();
									UnlockKeys();
									LockExit = false;
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
								ty ps = new ty();
								ps.Show();
								UnlockKeys();
								LockExit = false;
								Hide();

							}
						}
					}
					else if ((mark1 == false && mark2 == false && mark3 == false && mark4 == false) && (feed1 == true || feed2 == true || feed3 == true || feed4 == true))
						{
					if (MessageBox.Show("Are you sure do you want to continue submitting the project?\nThere is no way back", "IMPORTANT INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
						string filePath = Path.Combine(appDataFolder, "taskState.txt");
						try
						{
							File.WriteAllText(filePath, feed1 + "\n" + feed2 + "\n" + feed3 + "\n" + feed4);
							GiveFeedback gf = new GiveFeedback();
							UnlockKeys();
							gf.Show();
							LockExit = false;
							Hide();
						}
						catch (IOException ex)
						{
							MessageBox.Show("Error writing to file:");
							Console.WriteLine(ex.Message);
						}
					}
				}
				else
				{
					ty ps = new ty();
					ps.Show();
					UnlockKeys();
					LockExit = false;
					Hide();
				}
			}
		}

		private void Docker_Load(object sender, EventArgs e)
		{
			
		}




		protected override void OnFormClosing(FormClosingEventArgs e)
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

				string DesktopFolder=Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string javaexamfolder = Path.Combine(DesktopFolder, "JavaExam");
				string javatempfolder = Path.Combine(DesktopFolder, "JavaTemp");

				DeleteFolder(javaexamfolder+"\\out");
				DeleteFolder(javaexamfolder+"\\JavaExam\\src");
				CreateFolder(javaexamfolder+"\\JavaExam\\src");
				CopyFile(javatempfolder + "\\Main.java", javaexamfolder +"\\JavaExam\\src\\Main.java");
				com1 = false;
				com2 = false;
				com3 = false;
				com4 = false;
				mark1 = false;
				mark2 = false;
				mark3 = false;
				mark4 = false;
				feed1 = false;
				feed2 = false;
				feed3 = false;
				feed4 = false;
				button4.BackColor = Color.White;
				button5.BackColor = Color.White;
				button6.BackColor = Color.White;
				button7.BackColor = Color.White;
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
		private static void CopyFile(string sourceFilePath, string destinationFilePath)
		{
			try
			{
				File.Copy(sourceFilePath, destinationFilePath);
				Console.WriteLine("File copied successfully!");
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error copying file:");
				Console.WriteLine(ex.Message);
			}
		}
		private static void CreateFolder (string folderName)
		{
			try
			{
				Directory.CreateDirectory(folderName);
				Console.WriteLine("New folder created successfully!");
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error creating new folder:");
				Console.WriteLine(ex.Message);
			}
		}
		private void button11_Click(object sender, EventArgs e)
		{
			string csvFileNamePattern = @"CSV file:\s*(.*)";
			Match fileNameMatch = Regex.Match(fileContent, csvFileNamePattern);
			string csvFileName = fileNameMatch.Groups[1].Value.Trim();
			CSV csv = new CSV();
			csv.Text= csvFileName;
			csv.Show();
		}

		private void pictureBox1_Click_1(object sender, EventArgs e)
		{

		}

		private void brandImage_Click(object sender, EventArgs e)
		{
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Control | Keys.Shift | Keys.F8))
			{
				UnblockWebsites();
				return true;
			}
			if (keyData == (Keys.Control | Keys.Shift | Keys.F7))
			{
				BlockWebsites();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}
		static void CreateCsvFile(string inputText)
		{
			// Extract the CSV file name
			string csvFileNamePattern = @"CSV file:\s*(.*)";
			Match fileNameMatch = Regex.Match(inputText, csvFileNamePattern);
			string csvFileName = fileNameMatch.Groups[1].Value.Trim();

			// Extract the CSV content between <csv> and </csv>
			string csvContentPattern = @"<csv>(.*?)<\/csv>";
			Match contentMatch = Regex.Match(inputText, csvContentPattern, RegexOptions.Singleline);
			string csvContent = contentMatch.Groups[1].Value.Trim();

			// Save the CSV file to the user's Desktop in the "JavaExam\JavaExam" folder
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string outputDirectory = Path.Combine(desktopPath, "JavaExam", "JavaExam");
			
			string outputFilePath = Path.Combine(outputDirectory, csvFileName);	
			File.WriteAllText(outputFilePath, csvContent);
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			selectedTask = 0;
			string pattern = @"Overview:(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern, RegexOptions.Singleline);

			if (match.Success)
			{
				Overview = match.Groups[1].Value.Trim();
				richTextBox1.Text = Overview;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			selectedTask = 1;
			string pattern = @"Task 1:\s*(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern);

			if (match.Success)
			{
				Task1 = match.Groups[1].Value.Trim();
				richTextBox1.Text = Task1;
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			selectedTask = 3;
			string pattern = @"Task 3:\s*(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern);

			if (match.Success)
			{
				Task3 = match.Groups[1].Value.Trim();
				richTextBox1.Text = Task3;
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			selectedTask = 4;
			string pattern = @"Task 4:\s*(.*)";
			Match match = Regex.Match(File.ReadAllText(path), pattern);

			if (match.Success)
			{
				Task4 = match.Groups[1].Value.Trim();
				richTextBox1.Text = Task4;
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (selectedTask == 0)
				button3.BackColor = Color.White;
			if (selectedTask == 1 && mark1 == false)
			{
				button4.BackColor = Color.Red;
				mark1 = true;
				numberMarked++;
			}
			else if (selectedTask == 1 && mark1 == true)
			{
				button4.BackColor = Color.White;
				mark1 = false;
				numberMarked--;
			}

			if (selectedTask == 2 && mark2 == false)
			{
				button5.BackColor = Color.Red;
				mark2 = true;
				numberMarked++;
			}
			else if (selectedTask == 2 && mark2 == true)
			{
				button5.BackColor = Color.White;
				mark2 = false;
				numberMarked--;
			}

			if (selectedTask == 3 && mark3 == false)
			{
				button6.BackColor = Color.Red;
				mark3 = true;
				numberMarked++;
			}
			else if (selectedTask == 3 && com3 == true)
			{
				button6.BackColor = Color.White;
				mark3 = false;
				numberMarked--;
			}

			if (selectedTask == 4 && com4 == false)
			{
				button7.BackColor = Color.Red;
				mark4 = true;
				numberMarked++;
			}
			else if (selectedTask == 4 && com4 == true)
			{
				button7.BackColor = Color.White;
				mark4 = false;
				numberMarked--;
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			if (selectedTask == 0)
				button3.BackColor = Color.White;
			if (selectedTask == 1 && feed1 == false)
			{
				button4.BackColor = Color.Cyan;
				feed1 = true;
				numberFeedback++;
			}
			else if (selectedTask == 1 && feed1 == true)
			{
				button4.BackColor = Color.White;
				feed1 = false;
				numberFeedback--;
			}

			if (selectedTask == 2 && feed2 == false)
			{
				button5.BackColor = Color.Cyan;
				feed2 = true;
				numberFeedback++;
			}
			else if (selectedTask == 2 && feed2 == true)
			{
				button5.BackColor = Color.White;
				feed2 = false;
				numberFeedback--;
			}

			if (selectedTask == 3 && feed3 == false)
			{
				button6.BackColor = Color.Cyan;
				feed3 = true;
				numberFeedback++;
			}
			else if (selectedTask == 3 && feed3 == true)
			{
				button6.BackColor = Color.White;
				feed3 = false;
				numberFeedback--;
			}

			if (selectedTask == 4 && com4 == false)
			{
				button7.BackColor = Color.Cyan;
				mark4 = true;
				numberMarked++;
			}
			else if (selectedTask == 4 && com4 == true)
			{
				button7.BackColor = Color.White;
				mark4 = false;
				numberMarked--;
			}
		}

		private void Docker_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = LockExit;
			base.OnClosing(e);
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}

}