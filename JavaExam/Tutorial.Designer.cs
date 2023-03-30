namespace JavaExam
{
	partial class Tutorial
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button3 = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(12, 36);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(778, 446);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(778, 446);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.btnPrevious);
			this.panel2.Controls.Add(this.btnNext);
			this.panel2.Location = new System.Drawing.Point(12, 491);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(778, 97);
			this.panel2.TabIndex = 1;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.button3.ForeColor = System.Drawing.Color.White;
			this.button3.Location = new System.Drawing.Point(457, 25);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(123, 46);
			this.button3.TabIndex = 2;
			this.button3.Text = "Start Exam";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnPrevious.ForeColor = System.Drawing.Color.White;
			this.btnPrevious.Location = new System.Drawing.Point(173, 25);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(149, 46);
			this.btnPrevious.TabIndex = 1;
			this.btnPrevious.Text = "Previous Page";
			this.btnPrevious.UseVisualStyleBackColor = false;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// btnNext
			// 
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnNext.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnNext.ForeColor = System.Drawing.Color.White;
			this.btnNext.Location = new System.Drawing.Point(457, 25);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(123, 46);
			this.btnNext.TabIndex = 0;
			this.btnNext.Text = "Next Page";
			this.btnNext.UseVisualStyleBackColor = false;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(351, 25);
			this.label1.TabIndex = 2;
			this.label1.Text = "Examen Programare în JAVA - TUTORIAL";
			// 
			// Tutorial
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Tutorial";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel panel1;
		private Panel panel2;
		private PictureBox pictureBox1;
		private Button button3;
		private Button btnPrevious;
		private Button btnNext;
		private Label label1;
	}
}