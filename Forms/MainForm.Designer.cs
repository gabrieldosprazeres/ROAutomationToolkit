using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ROAutomationToolkit.Forms
{
    partial class MainForm
    {
        private IContainer components = null;
        private Label labelRagexeWindowSelector;
        private ComboBox comboBoxRagexeWindows;
        private Button buttonRagexeWindowsRefresh;

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
            // MainForm
            // ===========================================
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(375, 300);
            this.Controls.Add(this.labelRagexeWindowSelector);
            this.Controls.Add(this.comboBoxRagexeWindows);
            this.Controls.Add(this.buttonRagexeWindowsRefresh);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO Automation Toolkit v1.1.0";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
