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
        private ToolTip _toolTip;
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
        private ComboBox comboBoxProfiles;
        private Label labelProfiles;
        private TextBox textBoxProfileName;
        private Button buttonSaveProfile;
        private Button buttonDeleteProfile;
        private TabControl tabControlMain;
        private TabPage tabPageAutoKey;
        private TabPage tabPageHotKey;

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
            this._toolTip = new ToolTip();
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
            this.comboBoxProfiles = new ComboBox();
            this.labelProfiles = new Label();
            this.textBoxProfileName = new TextBox();
            this.buttonSaveProfile = new Button();
            this.buttonDeleteProfile = new Button();
            this.tabControlMain = new TabControl();
            this.tabPageAutoKey = new TabPage();
            this.tabPageHotKey = new TabPage();
            
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();

            // ===========================================
            // ToolTip Configuration
            // ===========================================
            this._toolTip.SetToolTip(buttonSaveProfile, "Salvar perfil atual");
            this._toolTip.SetToolTip(buttonDeleteProfile, "Excluir perfil selecionado");
            this._toolTip.SetToolTip(buttonRagexeWindowsRefresh, "Atualizar lista de ragexe.exe");
            // ===========================================
            // TabControlMain Configuration
            // ===========================================
            this.tabControlMain.Controls.Add(this.tabPageAutoKey);
            this.tabControlMain.Controls.Add(this.tabPageHotKey);
            this.tabControlMain.Dock = DockStyle.None;
            this.tabControlMain.Location = new Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new Size(385, 330);
            this.tabControlMain.TabIndex = 0;
            // ===========================================
            // Aba tabPageAutoKey
            // ===========================================
            this.tabPageAutoKey.Location = new Point(4, 25);
            this.tabPageAutoKey.Name = "tabPageAutoKey";
            this.tabPageAutoKey.Padding = new Padding(3);
            this.tabPageAutoKey.Size = new Size(375, 325);
            this.tabPageAutoKey.TabIndex = 0;
            this.tabPageAutoKey.Text = "AutoKey";
            this.tabPageAutoKey.UseVisualStyleBackColor = true;
            // ===========================================
            // RagexeWindowSelector Label
            // ===========================================
            this.labelRagexeWindowSelector.AutoSize = true;
            this.labelRagexeWindowSelector.Location = new Point(5, 15);
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
            this.comboBoxRagexeWindows.Location = new Point(7, 32);
            this.comboBoxRagexeWindows.Name = "comboBoxRagexeWindows";
            this.comboBoxRagexeWindows.Size = new Size(317, 24);
            this.comboBoxRagexeWindows.TabIndex = 0;
            // ===========================================
            // RagexeWindowsRefresh Button
            // ===========================================
            this.buttonRagexeWindowsRefresh.Location = new Point(327, 30);
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
            this.labeltextBoxKeySelection.Location = new Point(5, 72);
            this.labeltextBoxKeySelection.Name = "labeltextBoxKeySelection";
            this.labeltextBoxKeySelection.Size = new Size(40, 16);
            this.labeltextBoxKeySelection.TabIndex = 2;
            this.labeltextBoxKeySelection.Text = "Tecla:";
            // ===========================================
            // textBoxKeySelection
            // ===========================================
            this.textBoxKeySelection.Location = new Point(7, 89);
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
            this.labelInterval.Location = new Point(104, 72);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new Size(119, 16);
            this.labelInterval.TabIndex = 4;
            this.labelInterval.Text = "Intervalo (ms):";
            // ===========================================
            // textBoxInterval
            // ===========================================
            this.textBoxInterval.Location = new Point(106, 89);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new Size(91, 22);
            this.textBoxInterval.TabIndex = 5;
            this.textBoxInterval.PlaceholderText = "ms (min: 100)";
            this.textBoxInterval.Text = "2000";
            // ===========================================
            // buttonToggleKeySending Button
            // ===========================================
            this.buttonToggleKeySending.BackColor = Color.LightGreen;
            this.buttonToggleKeySending.Location = new Point(205, 86);
            this.buttonToggleKeySending.Name = "buttonToggleKeySending";
            this.buttonToggleKeySending.Size = new Size(157, 30);
            this.buttonToggleKeySending.TabIndex = 8;
            this.buttonToggleKeySending.Text = "Ativar";
            this.buttonToggleKeySending.UseVisualStyleBackColor = false;
            this.buttonToggleKeySending.Click += new EventHandler(this.OnbuttonToggleKeySending);
            // ===========================================
            // labelProfiles
            // ===========================================
            this.labelProfiles.AutoSize = true;
            this.labelProfiles.Location = new Point(5, 122);
            this.labelProfiles.Name = "labelProfiles";
            this.labelProfiles.Size = new Size(51, 16);
            this.labelProfiles.TabIndex = 13;
            this.labelProfiles.Text = "Perfis:";
            // ===========================================
            // comboBoxProfiles
            // ===========================================
            this.comboBoxProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new Point(7, 139);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new Size(172, 24);
            this.comboBoxProfiles.TabIndex = 12;
            this.comboBoxProfiles.SelectedIndexChanged += new EventHandler(this.ComboBoxProfiles_SelectedIndexChanged);
            // ===========================================
            // textBoxProfileName
            // ===========================================
            this.textBoxProfileName.Location = new Point(185, 139);
            this.textBoxProfileName.Name = "textBoxProfileName";
            this.textBoxProfileName.Size = new Size(100, 22);
            this.textBoxProfileName.TabIndex = 14;
            this.textBoxProfileName.PlaceholderText = "Nome do perfil";
            this.textBoxProfileName.Text = "Novo Perfil";
            // ===========================================
            // buttonSaveProfile
            // ===========================================
            this.buttonSaveProfile.Location = new Point(291, 138);
            this.buttonSaveProfile.Name = "buttonSaveProfile";
            this.buttonSaveProfile.Size = new Size(34, 28);
            this.buttonSaveProfile.TabIndex = 15;
            this.buttonSaveProfile.UseVisualStyleBackColor = true;
            this.buttonSaveProfile.Click += new EventHandler(this.ButtonSaveProfile_Click);
            string imagePathSaveProfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "diskette.png");
            this.buttonSaveProfile.Image = Image.FromFile(imagePathSaveProfile);
            this.buttonSaveProfile.ImageAlign = ContentAlignment.MiddleCenter;
            this.buttonSaveProfile.FlatAppearance.BorderSize = 0;
            this.buttonSaveProfile.Cursor = Cursors.Hand;
            // ===========================================
            // buttonDeleteProfile
            // ===========================================
            this.buttonDeleteProfile.Location = new Point(328, 138);
            this.buttonDeleteProfile.Name = "buttonDeleteProfile";
            this.buttonDeleteProfile.Size = new Size(34, 28);
            this.buttonDeleteProfile.TabIndex = 16;
            this.buttonDeleteProfile.UseVisualStyleBackColor = true;
            this.buttonDeleteProfile.Click += new EventHandler(this.ButtonDeleteProfile_Click);
            string imagePathDeleteProfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "garbage.png");
            this.buttonDeleteProfile.Image = Image.FromFile(imagePathDeleteProfile);
            this.buttonDeleteProfile.ImageAlign = ContentAlignment.MiddleCenter;
            this.buttonDeleteProfile.FlatAppearance.BorderSize = 0;
            this.buttonDeleteProfile.Cursor = Cursors.Hand;
            // ===========================================
            // labelListBoxLog
            // ===========================================
            this.labelListBoxLog.AutoSize = true;
            this.labelListBoxLog.Location = new Point(5, 171);
            this.labelListBoxLog.Name = "labelListBoxLog";
            this.labelListBoxLog.Size = new Size(36, 16);
            this.labelListBoxLog.TabIndex = 11;
            this.labelListBoxLog.Text = "Log:";
            // ===========================================
            // listBoxLog
            // ===========================================
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Location = new Point(7, 189);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new Size(353, 106);
            this.listBoxLog.TabIndex = 10;
            // ===========================================
            // Aba tabPageHotKey
            // ===========================================
            this.tabPageHotKey.Location = new Point(4, 25);
            this.tabPageHotKey.Name = "tabPageHotKey";
            this.tabPageHotKey.Padding = new Padding(3);
            this.tabPageHotKey.Size = new Size(375, 325);
            this.tabPageHotKey.TabIndex = 1;
            this.tabPageHotKey.Text = "HotKey";
            this.tabPageHotKey.UseVisualStyleBackColor = true;
            // ===========================================
            // MainForm
            // ===========================================
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.tabPageAutoKey.Controls.Add(this.labelRagexeWindowSelector);
            this.tabPageAutoKey.Controls.Add(this.comboBoxRagexeWindows);
            this.tabPageAutoKey.Controls.Add(this.buttonRagexeWindowsRefresh);
            this.tabPageAutoKey.Controls.Add(this.labeltextBoxKeySelection);
            this.tabPageAutoKey.Controls.Add(this.labelInterval);
            this.tabPageAutoKey.Controls.Add(this.textBoxInterval);
            this.tabPageAutoKey.Controls.Add(this.buttonToggleKeySending);
            this.tabPageAutoKey.Controls.Add(this.textBoxKeySelection);
            this.tabPageAutoKey.Controls.Add(this.listBoxLog);
            this.tabPageAutoKey.Controls.Add(this.labelListBoxLog);
            this.tabPageAutoKey.Controls.Add(this.comboBoxProfiles);
            this.tabPageAutoKey.Controls.Add(this.labelProfiles);
            this.tabPageAutoKey.Controls.Add(this.textBoxProfileName);
            this.tabPageAutoKey.Controls.Add(this.buttonSaveProfile);
            this.tabPageAutoKey.Controls.Add(this.buttonDeleteProfile);
            this.Controls.Add(this.tabControlMain);
            this.ClientSize = new Size(375, 325);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO Automation Toolkit v1.4.0";
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}