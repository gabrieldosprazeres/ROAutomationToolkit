using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ROAutomationToolkit.Services;

namespace ROAutomationToolkit.Forms
{
    partial class MainForm
    {
        private IContainer components = null;
        private Label labelRagexeWindowSelector;
        private ComboBox comboBoxRagexeWindows;
        private Button buttonRagexeWindowsRefresh;
        private Label labeltextBoxKeySelection;
        private Label labelInterval;
        private TextBox textBoxInterval;
        private Button buttonToggleKeySending;
        private TextBox textBoxKeySelection;
        private ListBox listBoxLog;
        private Label labelListBoxLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelRagexeWindowSelector = new Label();
            this.comboBoxRagexeWindows = new ComboBox();
            this.buttonRagexeWindowsRefresh = new Button();
            this.labeltextBoxKeySelection = new Label();
            this.labelInterval = new Label();
            this.textBoxInterval = new TextBox();
            this.buttonToggleKeySending = new Button();
            this.textBoxKeySelection = new TextBox();
            this.listBoxLog = new ListBox();
            this.labelListBoxLog = new Label();
            this.SuspendLayout();
            // ===========================================
            // RagexeWindowSelector Label
            // ===========================================
            this.labelRagexeWindowSelector.AutoSize = true;
            this.labelRagexeWindowSelector.Location = new Point(12, 13);
            this.labelRagexeWindowSelector.Name = "labelRagexeWindowSelector";
            this.labelRagexeWindowSelector.Size = new Size(209, 16);
            this.labelRagexeWindowSelector.TabIndex = 1;
            this.labelRagexeWindowSelector.Text = "Selecione uma janela:";
            // ===========================================
            // RagexeWindow ComboBox
            // ===========================================
            this.comboBoxRagexeWindows.DropDownHeight = 300;
            this.comboBoxRagexeWindows.FormattingEnabled = true;
            this.comboBoxRagexeWindows.IntegralHeight = false;
            this.comboBoxRagexeWindows.Location = new Point(12, 32);
            this.comboBoxRagexeWindows.Name = "comboBoxRagexeWindows";
            this.comboBoxRagexeWindows.Size = new Size(317, 24);
            this.comboBoxRagexeWindows.TabIndex = 0;
            // ===========================================
            // RagexeWindowsRefresh Button
            // ===========================================
            this.buttonRagexeWindowsRefresh.Location = new Point(331, 30);
            this.buttonRagexeWindowsRefresh.Name = "buttonRagexeWindowsRefresh";
            this.buttonRagexeWindowsRefresh.Size = new Size(34, 28);
            this.buttonRagexeWindowsRefresh.TabIndex = 7;
            this.buttonRagexeWindowsRefresh.Text = "";
            this.buttonRagexeWindowsRefresh.UseVisualStyleBackColor = true;
            this.buttonRagexeWindowsRefresh.Click += new EventHandler(this.ButtonRagexeWindowsRefresh_Click);
            string imagePathRagexeWindow = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "refresh.png");
            this.buttonRagexeWindowsRefresh.Image = Image.FromFile(imagePathRagexeWindow);
            this.buttonRagexeWindowsRefresh.ImageAlign = ContentAlignment.MiddleCenter;
            this.buttonRagexeWindowsRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRagexeWindowsRefresh.Cursor = Cursors.Hand;
            // ===========================================
            // labeltextBoxKeySelection
            // ===========================================
            this.labeltextBoxKeySelection.AutoSize = true;
            this.labeltextBoxKeySelection.Location = new Point(12, 70);
            this.labeltextBoxKeySelection.Name = "labeltextBoxKeySelection";
            this.labeltextBoxKeySelection.Size = new Size(40, 16);
            this.labeltextBoxKeySelection.TabIndex = 2;
            this.labeltextBoxKeySelection.Text = "Tecla:";
            // ===========================================
            // textBoxKeySelection
            // ===========================================
            this.textBoxKeySelection.Location = new Point(12, 89);
            this.textBoxKeySelection.Name = "textBoxKeySelection";
            this.textBoxKeySelection.ReadOnly = true;
            this.textBoxKeySelection.Size = new Size(91, 22);
            this.textBoxKeySelection.TabIndex = 9;
            this.textBoxKeySelection.Text = "F1";
            this.textBoxKeySelection.KeyDown += new KeyEventHandler(this.CaptureSelectedKey);
            // ===========================================
            // labelInterval
            // ===========================================
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new Point(109, 70);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new Size(119, 16);
            this.labelInterval.TabIndex = 4;
            this.labelInterval.Text = "Intervalo (ms):";
            // ===========================================
            // textBoxInterval
            // ===========================================
            this.textBoxInterval.Location = new Point(111, 89);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new Size(91, 22);
            this.textBoxInterval.TabIndex = 5;
            this.textBoxInterval.Text = "2000";
            // ===========================================
            // buttonToggleKeySending Button
            // ===========================================
            this.buttonToggleKeySending.BackColor = Color.LightGreen;
            this.buttonToggleKeySending.Location = new Point(210, 86);
            this.buttonToggleKeySending.Name = "buttonToggleKeySending";
            this.buttonToggleKeySending.Size = new Size(156, 30);
            this.buttonToggleKeySending.TabIndex = 8;
            this.buttonToggleKeySending.Text = "Ativar";
            this.buttonToggleKeySending.UseVisualStyleBackColor = false;
            this.buttonToggleKeySending.Click += new EventHandler(this.OnbuttonToggleKeySending);
            // ===========================================
            // labelListBoxLog
            // ===========================================
            this.labelListBoxLog.AutoSize = true;
            this.labelListBoxLog.Location = new Point(12, 120);
            this.labelListBoxLog.Name = "labelListBoxLog";
            this.labelListBoxLog.Size = new Size(36, 16);
            this.labelListBoxLog.TabIndex = 11;
            this.labelListBoxLog.Text = "Log:";
            // ===========================================
            // listBoxLog
            // ===========================================
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Location = new Point(12, 139);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new Size(353, 156);
            this.listBoxLog.TabIndex = 10;
            // ===========================================
            // MainForm
            // ===========================================
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(375, 300);
            this.Controls.Add(this.labelRagexeWindowSelector);
            this.Controls.Add(this.comboBoxRagexeWindows);
            this.Controls.Add(this.buttonRagexeWindowsRefresh);
            this.Controls.Add(this.labelListBoxLog);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.textBoxKeySelection);
            this.Controls.Add(this.buttonToggleKeySending);
            this.Controls.Add(this.textBoxInterval);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.labeltextBoxKeySelection);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO Automation Toolkit v1.2.0";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
