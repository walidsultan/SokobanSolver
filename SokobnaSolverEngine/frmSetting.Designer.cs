namespace SokobnaSolverEngine
{
    partial class frmSetting
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
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblZoomFactor = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtZoomFactor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(72, 119);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(172, 119);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(28, 27);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(119, 13);
            this.lblSpeed.TabIndex = 2;
            this.lblSpeed.Text = "Animation step Speed : ";
            // 
            // lblZoomFactor
            // 
            this.lblZoomFactor.AutoSize = true;
            this.lblZoomFactor.Location = new System.Drawing.Point(74, 60);
            this.lblZoomFactor.Name = "lblZoomFactor";
            this.lblZoomFactor.Size = new System.Drawing.Size(73, 13);
            this.lblZoomFactor.TabIndex = 3;
            this.lblZoomFactor.Text = "Zoom Factor :";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(153, 27);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(52, 20);
            this.txtSpeed.TabIndex = 4;
            // 
            // txtZoomFactor
            // 
            this.txtZoomFactor.Location = new System.Drawing.Point(153, 60);
            this.txtZoomFactor.Name = "txtZoomFactor";
            this.txtZoomFactor.Size = new System.Drawing.Size(52, 20);
            this.txtZoomFactor.TabIndex = 5;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 183);
            this.Controls.Add(this.txtZoomFactor);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.lblZoomFactor);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblZoomFactor;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtZoomFactor;
    }
}