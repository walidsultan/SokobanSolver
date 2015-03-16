namespace SokobnaSolverEngine
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblKeyBoardhandler = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.tAnimate = new System.Windows.Forms.Timer(this.components);
            this.lblRecursiveSolution = new System.Windows.Forms.Label();
            this.lblUnrepeatedSolution = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStuckSolutions = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ofdSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.lblIsDirectPath = new System.Windows.Forms.Label();
            this.lblPathTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPushes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrentSolution = new System.Windows.Forms.Label();
            this.lblPossibleSolutions = new System.Windows.Forms.Label();
            this.pnlStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(32, 23);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(27, 13);
            this.lblMsg.TabIndex = 5;
            this.lblMsg.Text = "Msg";
            // 
            // lblKeyBoardhandler
            // 
            this.lblKeyBoardhandler.AutoSize = true;
            this.lblKeyBoardhandler.Location = new System.Drawing.Point(1388, 40);
            this.lblKeyBoardhandler.Name = "lblKeyBoardhandler";
            this.lblKeyBoardhandler.Size = new System.Drawing.Size(0, 13);
            this.lblKeyBoardhandler.TabIndex = 6;
            this.lblKeyBoardhandler.TextChanged += new System.EventHandler(this.lblKeyBoardhandler_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1431, 420);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Load Level";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.LoadLevel_Click);
            this.button1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress_1);
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(1431, 373);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 8;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Visible = false;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // tAnimate
            // 
            this.tAnimate.Enabled = true;
            this.tAnimate.Interval = 50;
            this.tAnimate.Tick += new System.EventHandler(this.tAnimate_Tick);
            // 
            // lblRecursiveSolution
            // 
            this.lblRecursiveSolution.AutoSize = true;
            this.lblRecursiveSolution.Location = new System.Drawing.Point(130, 1);
            this.lblRecursiveSolution.Name = "lblRecursiveSolution";
            this.lblRecursiveSolution.Size = new System.Drawing.Size(35, 13);
            this.lblRecursiveSolution.TabIndex = 9;
            this.lblRecursiveSolution.Text = "label1";
            // 
            // lblUnrepeatedSolution
            // 
            this.lblUnrepeatedSolution.AutoSize = true;
            this.lblUnrepeatedSolution.Location = new System.Drawing.Point(130, 31);
            this.lblUnrepeatedSolution.Name = "lblUnrepeatedSolution";
            this.lblUnrepeatedSolution.Size = new System.Drawing.Size(35, 13);
            this.lblUnrepeatedSolution.TabIndex = 10;
            this.lblUnrepeatedSolution.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "recursive Solutions :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Unrepeated Solutions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Stuck Solutions :";
            // 
            // lblStuckSolutions
            // 
            this.lblStuckSolutions.AutoSize = true;
            this.lblStuckSolutions.Location = new System.Drawing.Point(130, 59);
            this.lblStuckSolutions.Name = "lblStuckSolutions";
            this.lblStuckSolutions.Size = new System.Drawing.Size(35, 13);
            this.lblStuckSolutions.TabIndex = 14;
            this.lblStuckSolutions.Text = "label1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1388, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 13;
            // 
            // ofdSelectFile
            // 
            this.ofdSelectFile.Filter = "Level|*.gam";
            this.ofdSelectFile.InitialDirectory = "Levels";
            // 
            // lblIsDirectPath
            // 
            this.lblIsDirectPath.AutoSize = true;
            this.lblIsDirectPath.Location = new System.Drawing.Point(127, 157);
            this.lblIsDirectPath.Name = "lblIsDirectPath";
            this.lblIsDirectPath.Size = new System.Drawing.Size(35, 13);
            this.lblIsDirectPath.TabIndex = 16;
            this.lblIsDirectPath.Text = "label4";
            // 
            // lblPathTime
            // 
            this.lblPathTime.AutoSize = true;
            this.lblPathTime.Location = new System.Drawing.Point(127, 182);
            this.lblPathTime.Name = "lblPathTime";
            this.lblPathTime.Size = new System.Drawing.Size(35, 13);
            this.lblPathTime.TabIndex = 17;
            this.lblPathTime.Text = "label6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "IsDirectPath";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "PathTime";
            // 
            // lblPushes
            // 
            this.lblPushes.AutoSize = true;
            this.lblPushes.Location = new System.Drawing.Point(130, 85);
            this.lblPushes.Name = "lblPushes";
            this.lblPushes.Size = new System.Drawing.Size(35, 13);
            this.lblPushes.TabIndex = 20;
            this.lblPushes.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Pushes :";
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.Controls.Add(this.label7);
            this.pnlStatistics.Controls.Add(this.label9);
            this.pnlStatistics.Controls.Add(this.lblCurrentSolution);
            this.pnlStatistics.Controls.Add(this.lblPossibleSolutions);
            this.pnlStatistics.Controls.Add(this.label1);
            this.pnlStatistics.Controls.Add(this.label8);
            this.pnlStatistics.Controls.Add(this.lblRecursiveSolution);
            this.pnlStatistics.Controls.Add(this.lblPushes);
            this.pnlStatistics.Controls.Add(this.lblUnrepeatedSolution);
            this.pnlStatistics.Controls.Add(this.label6);
            this.pnlStatistics.Controls.Add(this.label2);
            this.pnlStatistics.Controls.Add(this.label4);
            this.pnlStatistics.Controls.Add(this.lblStuckSolutions);
            this.pnlStatistics.Controls.Add(this.lblPathTime);
            this.pnlStatistics.Controls.Add(this.label3);
            this.pnlStatistics.Controls.Add(this.lblIsDirectPath);
            this.pnlStatistics.Location = new System.Drawing.Point(473, 12);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Size = new System.Drawing.Size(182, 209);
            this.pnlStatistics.TabIndex = 22;
            this.pnlStatistics.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatistics_Paint);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Current Solutions :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Possible Solutions :";
            // 
            // lblCurrentSolution
            // 
            this.lblCurrentSolution.AutoSize = true;
            this.lblCurrentSolution.Location = new System.Drawing.Point(127, 133);
            this.lblCurrentSolution.Name = "lblCurrentSolution";
            this.lblCurrentSolution.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentSolution.TabIndex = 23;
            this.lblCurrentSolution.Text = "label6";
            // 
            // lblPossibleSolutions
            // 
            this.lblPossibleSolutions.AutoSize = true;
            this.lblPossibleSolutions.Location = new System.Drawing.Point(127, 108);
            this.lblPossibleSolutions.Name = "lblPossibleSolutions";
            this.lblPossibleSolutions.Size = new System.Drawing.Size(35, 13);
            this.lblPossibleSolutions.TabIndex = 22;
            this.lblPossibleSolutions.Text = "label4";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1177, 698);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblKeyBoardhandler);
            this.Controls.Add(this.lblMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sokoban Solver";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatistics.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label lblKeyBoardhandler;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Timer tAnimate;
        private System.Windows.Forms.Label lblRecursiveSolution;
        private System.Windows.Forms.Label lblUnrepeatedSolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStuckSolutions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog ofdSelectFile;
        private System.Windows.Forms.Label lblIsDirectPath;
        private System.Windows.Forms.Label lblPathTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPushes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurrentSolution;
        private System.Windows.Forms.Label lblPossibleSolutions;
    }
}

