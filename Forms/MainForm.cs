using ROAutomationToolkit.Models;
using ROAutomationToolkit.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROAutomationToolkit.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadWindowsAsync();
            this.Icon = new Icon("Resources\\icon.ico");
        }

        private async void LoadWindowsAsync()
        {
            Cursor = Cursors.WaitCursor;
            comboBoxRagexeWindows.Items.Clear();

            var windows = await Task.Run(() => WindowEnumerator.GetRagexeWindows());
            comboBoxRagexeWindows.Items.AddRange(windows.ToArray());

            if (comboBoxRagexeWindows.Items.Count > 0)
                comboBoxRagexeWindows.SelectedIndex = 0;

            Cursor = Cursors.Default;
        }

        private void ButtonRagexeWindowsRefresh_Click(object sender, EventArgs e)
        {
            LoadWindowsAsync();
        }
    }
}
