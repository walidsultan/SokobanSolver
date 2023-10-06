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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            lblMsg = new System.Windows.Forms.Label();
            lblKeyBoardhandler = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            btnSolve = new System.Windows.Forms.Button();
            tAnimate = new System.Windows.Forms.Timer(components);
            lblRecursiveSolution = new System.Windows.Forms.Label();
            lblUnrepeatedSolution = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            lblStuckSolutions = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ofdSelectFile = new System.Windows.Forms.OpenFileDialog();
            lblIsDirectPath = new System.Windows.Forms.Label();
            lblPathTime = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            lblPushes = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            pnlStatistics = new System.Windows.Forms.Panel();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            lblCurrentSolution = new System.Windows.Forms.Label();
            lblPossibleSolutions = new System.Windows.Forms.Label();
            Menu = new System.Windows.Forms.MenuStrip();
            pnlStatistics.SuspendLayout();
            SuspendLayout();
            // 
            // lblMsg
            // 
            lblMsg.AutoSize = true;
            lblMsg.Location = new System.Drawing.Point(43, 35);
            lblMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new System.Drawing.Size(37, 20);
            lblMsg.TabIndex = 5;
            lblMsg.Text = "Msg";
            // 
            // lblKeyBoardhandler
            // 
            lblKeyBoardhandler.AutoSize = true;
            lblKeyBoardhandler.Location = new System.Drawing.Point(1851, 62);
            lblKeyBoardhandler.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblKeyBoardhandler.Name = "lblKeyBoardhandler";
            lblKeyBoardhandler.Size = new System.Drawing.Size(0, 20);
            lblKeyBoardhandler.TabIndex = 6;
            lblKeyBoardhandler.TextChanged += lblKeyBoardhandler_TextChanged;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(1908, 646);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(100, 35);
            button1.TabIndex = 7;
            button1.Text = "Load Level";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += LoadLevel_Click;
            button1.KeyPress += button1_KeyPress_1;
            // 
            // btnSolve
            // 
            btnSolve.Location = new System.Drawing.Point(1908, 574);
            btnSolve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSolve.Name = "btnSolve";
            btnSolve.Size = new System.Drawing.Size(100, 35);
            btnSolve.TabIndex = 8;
            btnSolve.Text = "Solve";
            btnSolve.UseVisualStyleBackColor = true;
            btnSolve.Visible = false;
            btnSolve.Click += btnSolve_Click;
            // 
            // tAnimate
            // 
            tAnimate.Enabled = true;
            tAnimate.Interval = 50;
            tAnimate.Tick += tAnimate_Tick;
            // 
            // lblRecursiveSolution
            // 
            lblRecursiveSolution.AutoSize = true;
            lblRecursiveSolution.Location = new System.Drawing.Point(173, 2);
            lblRecursiveSolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblRecursiveSolution.Name = "lblRecursiveSolution";
            lblRecursiveSolution.Size = new System.Drawing.Size(50, 20);
            lblRecursiveSolution.TabIndex = 9;
            lblRecursiveSolution.Text = "label1";
            // 
            // lblUnrepeatedSolution
            // 
            lblUnrepeatedSolution.AutoSize = true;
            lblUnrepeatedSolution.Location = new System.Drawing.Point(173, 48);
            lblUnrepeatedSolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblUnrepeatedSolution.Name = "lblUnrepeatedSolution";
            lblUnrepeatedSolution.Size = new System.Drawing.Size(50, 20);
            lblUnrepeatedSolution.TabIndex = 10;
            lblUnrepeatedSolution.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 0);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(139, 20);
            label1.TabIndex = 11;
            label1.Text = "recursive Solutions :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(4, 48);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(152, 20);
            label2.TabIndex = 12;
            label2.Text = "Unrepeated Solutions";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(4, 91);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(116, 20);
            label3.TabIndex = 15;
            label3.Text = "Stuck Solutions :";
            // 
            // lblStuckSolutions
            // 
            lblStuckSolutions.AutoSize = true;
            lblStuckSolutions.Location = new System.Drawing.Point(173, 91);
            lblStuckSolutions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStuckSolutions.Name = "lblStuckSolutions";
            lblStuckSolutions.Size = new System.Drawing.Size(50, 20);
            lblStuckSolutions.TabIndex = 14;
            lblStuckSolutions.Text = "label1";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(1851, 105);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(0, 20);
            label5.TabIndex = 13;
            // 
            // ofdSelectFile
            // 
            ofdSelectFile.Filter = "Level|*.gam";
            ofdSelectFile.InitialDirectory = "Levels";
            // 
            // lblIsDirectPath
            // 
            lblIsDirectPath.AutoSize = true;
            lblIsDirectPath.Location = new System.Drawing.Point(169, 242);
            lblIsDirectPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblIsDirectPath.Name = "lblIsDirectPath";
            lblIsDirectPath.Size = new System.Drawing.Size(50, 20);
            lblIsDirectPath.TabIndex = 16;
            lblIsDirectPath.Text = "label4";
            // 
            // lblPathTime
            // 
            lblPathTime.AutoSize = true;
            lblPathTime.Location = new System.Drawing.Point(169, 280);
            lblPathTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPathTime.Name = "lblPathTime";
            lblPathTime.Size = new System.Drawing.Size(50, 20);
            lblPathTime.TabIndex = 17;
            lblPathTime.Text = "label6";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(4, 240);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(87, 20);
            label4.TabIndex = 18;
            label4.Text = "IsDirectPath";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(4, 278);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(70, 20);
            label6.TabIndex = 19;
            label6.Text = "PathTime";
            // 
            // lblPushes
            // 
            lblPushes.AutoSize = true;
            lblPushes.Location = new System.Drawing.Point(173, 131);
            lblPushes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPushes.Name = "lblPushes";
            lblPushes.Size = new System.Drawing.Size(50, 20);
            lblPushes.TabIndex = 20;
            lblPushes.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(4, 131);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(60, 20);
            label8.TabIndex = 21;
            label8.Text = "Pushes :";
            // 
            // pnlStatistics
            // 
            pnlStatistics.Controls.Add(label7);
            pnlStatistics.Controls.Add(label9);
            pnlStatistics.Controls.Add(lblCurrentSolution);
            pnlStatistics.Controls.Add(lblPossibleSolutions);
            pnlStatistics.Controls.Add(label1);
            pnlStatistics.Controls.Add(label8);
            pnlStatistics.Controls.Add(lblRecursiveSolution);
            pnlStatistics.Controls.Add(lblPushes);
            pnlStatistics.Controls.Add(lblUnrepeatedSolution);
            pnlStatistics.Controls.Add(label6);
            pnlStatistics.Controls.Add(label2);
            pnlStatistics.Controls.Add(label4);
            pnlStatistics.Controls.Add(lblStuckSolutions);
            pnlStatistics.Controls.Add(lblPathTime);
            pnlStatistics.Controls.Add(label3);
            pnlStatistics.Controls.Add(lblIsDirectPath);
            pnlStatistics.Location = new System.Drawing.Point(631, 18);
            pnlStatistics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pnlStatistics.Name = "pnlStatistics";
            pnlStatistics.Size = new System.Drawing.Size(243, 322);
            pnlStatistics.TabIndex = 22;
            pnlStatistics.Paint += pnlStatistics_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(4, 203);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(129, 20);
            label7.TabIndex = 25;
            label7.Text = "Current Solutions :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(4, 165);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(134, 20);
            label9.TabIndex = 24;
            label9.Text = "Possible Solutions :";
            // 
            // lblCurrentSolution
            // 
            lblCurrentSolution.AutoSize = true;
            lblCurrentSolution.Location = new System.Drawing.Point(169, 205);
            lblCurrentSolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCurrentSolution.Name = "lblCurrentSolution";
            lblCurrentSolution.Size = new System.Drawing.Size(50, 20);
            lblCurrentSolution.TabIndex = 23;
            lblCurrentSolution.Text = "label6";
            // 
            // lblPossibleSolutions
            // 
            lblPossibleSolutions.AutoSize = true;
            lblPossibleSolutions.Location = new System.Drawing.Point(169, 166);
            lblPossibleSolutions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPossibleSolutions.Name = "lblPossibleSolutions";
            lblPossibleSolutions.Size = new System.Drawing.Size(50, 20);
            lblPossibleSolutions.TabIndex = 22;
            lblPossibleSolutions.Text = "label4";
            // 
            // Menu
            // 
            Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            Menu.Location = new System.Drawing.Point(0, 0);
            Menu.Name = "Menu";
            Menu.Size = new System.Drawing.Size(1569, 28);
            Menu.TabIndex = 23;
            Menu.Text = "menu";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Silver;
            ClientSize = new System.Drawing.Size(1569, 1074);
            Controls.Add(pnlStatistics);
            Controls.Add(label5);
            Controls.Add(btnSolve);
            Controls.Add(button1);
            Controls.Add(lblKeyBoardhandler);
            Controls.Add(lblMsg);
            Controls.Add(Menu);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = Menu;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "frmMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sokoban Solver";
            FormClosing += Form1_FormClosing;
            Load += frmMain_Load;
            KeyDown += Form1_KeyDown;
            Resize += frmMain_Resize;
            pnlStatistics.ResumeLayout(false);
            pnlStatistics.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.MenuStrip Menu;
    }
}

