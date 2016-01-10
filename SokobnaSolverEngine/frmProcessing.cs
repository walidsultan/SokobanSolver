using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Sokoban.SokobanSolvingLogic;
using System.Threading;

namespace SokobnaSolverEngine
{
    public partial class frmProcessing : Form
    {
        CancellationTokenSource _CancellationToken;
        public frmProcessing(CancellationTokenSource cancellationToken)
        {
            this._CancellationToken = cancellationToken;
            InitializeComponent();
        }

        private void frmProcessing_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Opacity = .99;
            
        }

        private void frmProcessing_Load(object sender, EventArgs e)
        {
           // ShowProcessing();
            ToolTip AssociatedTip = new ToolTip();
            AssociatedTip.SetToolTip(pbProcessing, "Double Click to Stop.");
            
        }

   
        private void ShowProcessing()
        {
            PictureBox Processing = new PictureBox();
            Processing.Left = 0;
            Processing.Top = 0;
            Processing.SizeMode = PictureBoxSizeMode.AutoSize;
            Processing.ImageLocation = Application.StartupPath + "\\images\\processing.gif";
            Processing.Name = "ProcessingImage";
            ToolTip AssociatedTip = new ToolTip();
            AssociatedTip.SetToolTip(Processing, "Double Click to Stop.");
            Processing.MouseDoubleClick  += new MouseEventHandler (ImageProcesseingDCHandler);
            this.Controls.Add(Processing);
        }

        private void ImageProcesseingDCHandler(object sender, EventArgs e)
        {
            
            
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {//Stop Processing thread
            this._CancellationToken.Cancel();
            this.Close();
        }

        public void SetCancellationToken(CancellationTokenSource cancellationToken) {
            this._CancellationToken = cancellationToken;
        }

    }
}
