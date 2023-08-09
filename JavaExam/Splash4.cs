using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaExam
{
    public partial class Splash4 : Form
    {
        private string pythonPath1 = "C:\\TaskWorker\\TaskEvaluator\\venv\\Scripts\\python.exe";
        private string scriptPath1 = "C:\\TaskWorker\\TaskEvaluator\\main.py";
        public Splash4()
        {
            InitializeComponent();
            this.Show();
            this.pictureBox2.Image = (System.Drawing.Bitmap)Properties.Resources.ResourceManager.GetObject("SplashLogo");
            RunScriptInBackground();
        }
        private async void RunScriptInBackground()
        {
            System.IO.Directory.SetCurrentDirectory("C:\\TaskWorker\\TaskEvaluator");
            await Task.Run(() => RunPythonScript(pythonPath1, scriptPath1));

            this.Invoke((Action)delegate
            {
                Hide(); // Close splash form
                Grade grade = new Grade();
                grade.Show();
            });
        }

        private void RunPythonScript(string pythonPath, string scriptPath)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = pythonPath;
                process.StartInfo.Arguments = $"\"{scriptPath}\"";  // Encase scriptPath in quotes in case of spaces in path
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        // Log or print the output for debugging
                        MessageBox.Show(e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        // Log or print the errors for debugging
                        MessageBox.Show("ERROR: " + e.Data);
                    }
                };

                process.Start();

                process.BeginOutputReadLine(); // Start async read of standard output
                process.BeginErrorReadLine();  // Start async read of standard error

                process.WaitForExit(); // Wait for the process to exit
            }
        }



    }
}
