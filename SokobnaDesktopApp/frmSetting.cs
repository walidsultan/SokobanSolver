using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Sokoban.DataTypes;
using System.Configuration;
namespace SokobnaSolverEngine
{
    public partial class frmSetting : Form
    {
        public delegate void SettingChangedHandler();

        public event SettingChangedHandler SettingsChanged;

        public frmSetting()
        {
            InitializeComponent();

            LoadSettings();

        }

        private void LoadSettings()
        {
            txtSpeed.Text = Settings.Speed.ToString();
            txtZoomFactor.Text = Settings.ZoomFactor .ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            XMLHandler.WriteSetting("Speed", txtSpeed.Text);
            XMLHandler.WriteSetting("ZoomFactor", txtZoomFactor.Text);

            Settings.Speed = int.Parse(txtSpeed.Text);
            Settings.ZoomFactor = int.Parse(txtZoomFactor.Text);
            
            OnSettingsChanged();



            this.Close();

        }

        protected void OnSettingsChanged()
        {
            if (SettingsChanged  != null)
            {
                SettingsChanged();
            }
        }

    }
}
