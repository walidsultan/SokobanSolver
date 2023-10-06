namespace SokobnaSolverEngine
{
    partial class frmProcessing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcessing));
            this.pbProcessing = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessing)).BeginInit();
            this.SuspendLayout();
            // 
            // pbProcessing
            // 
            this.pbProcessing.Image = ((System.Drawing.Image)(resources.GetObject("pbProcessing.Image")));
            this.pbProcessing.Location = new System.Drawing.Point(3, 2);
            this.pbProcessing.Name = "pbProcessing";
            this.pbProcessing.Size = new System.Drawing.Size(214, 138);
            this.pbProcessing.TabIndex = 0;
            this.pbProcessing.TabStop = false;
            this.pbProcessing.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // frmProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 138);
            this.Controls.Add(this.pbProcessing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcessing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing";
            this.Load += new System.EventHandler(this.frmProcessing_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcessing_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbProcessing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbProcessing;


    }
}