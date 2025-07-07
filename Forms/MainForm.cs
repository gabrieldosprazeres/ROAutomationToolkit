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
        private readonly KeySenderService keySenderService;

        public MainForm()
        {
            InitializeComponent();
            keySenderService = new KeySenderService();
            keySenderService.LogMessage += HandleLogMessage;
            keySenderService.SendingStateChanged += HandleSendingStateChanged;
            LoadWindowsAsync();
            this.Icon = new Icon("Resources\\icon.ico");
            this.Load += MainFormLoad;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            HandleLogMessage("3. Clique em 'Ativar' para começar");
            HandleLogMessage("2. Configure a tecla e o intervalo");
            HandleLogMessage("1. Selecione uma janela do jogo na lista");
            HandleLogMessage("Bem-vindo ao RO Automation Toolkit v1.1.0");
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

        private void OnbuttonToggleKeySending(object sender, EventArgs e)
        {
            if (keySenderService.isSending)
            {
                keySenderService.StopSending();
            }
            else
            {
                if (!ValidateAndStartSending()) return;
            }
        }

        private bool ValidateAndStartSending()
        {
            if (!int.TryParse(textBoxInterval.Text, out int interval) || interval < 100)
            {
                HandleLogMessage("Intervalo inválido! O valor deve ser numérico e maior que 100ms");
                return false;
            }

            if (comboBoxRagexeWindows.SelectedItem == null)
            {
                HandleLogMessage("Selecione uma janela do jogo antes de iniciar");
                return false;
            }

            keySenderService.StartSending(
                (WindowInfo)comboBoxRagexeWindows.SelectedItem,
                interval,
                textBoxKeySelection.Text
            );

            return true;
        }

        private void CaptureSelectedKey(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            keySenderService.SetKey(e.KeyValue);
            textBoxKeySelection.Text = e.KeyCode.ToString();
            HandleLogMessage($"Tecla alterada para: {e.KeyCode}");
        }
        
        private void HandleLogMessage(string message)
        {
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(new Action<string>(HandleLogMessage), message);
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            listBoxLog.Items.Insert(0, $"{timestamp} - {message}");
            if (listBoxLog.Items.Count > 100) listBoxLog.Items.RemoveAt(listBoxLog.Items.Count - 1);
        }

        private void HandleSendingStateChanged(bool isSending)
        {
            if (buttonToggleKeySending.InvokeRequired)
            {
                buttonToggleKeySending.Invoke(new Action<bool>(HandleSendingStateChanged), isSending);
                return;
            }
            
            buttonToggleKeySending.Text = isSending ? "Desativar" : "Ativar";
            buttonToggleKeySending.BackColor = isSending ? Color.LightCoral : Color.LightGreen;
        }
    }
}
