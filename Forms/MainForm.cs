using ROAutomationToolkit.Models;
using ROAutomationToolkit.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROAutomationToolkit.Forms
{
    public partial class MainForm : Form
    {
        private readonly KeySenderService _keySenderService;
        private readonly IProfileService _profileService;
        private readonly SynchronizationContext _uiContext;

        public MainForm(
            KeySenderService keySenderService,
            IProfileService profileService)
        {
            InitializeComponent();
            _keySenderService = keySenderService;
            _profileService = profileService;
            _uiContext = SynchronizationContext.Current;

            _keySenderService.LogMessage += HandleLogMessage;
            _keySenderService.SendingStateChanged += HandleSendingStateChanged;

            this.FormClosing += MainFormClosing;
            this.Load += MainFormLoad;

            InitializeApp();
        }

        private void InitializeApp()
        {
            try
            {
                this.Icon = new Icon("Resources\\icon.ico");
            }
            catch {}
            
            LoadProfiles();
            LoadWindowsAsync();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            PostLogMessage("3. Clique em 'Ativar' para começar");
            PostLogMessage("2. Configure a tecla e o intervalo");
            PostLogMessage("1. Selecione uma janela do jogo na lista");
            PostLogMessage("Bem-vindo ao RO Automation Toolkit v1.3.0");
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_keySenderService.IsSending)
            {
                var result = MessageBox.Show(
                    "O programa está ativo. Deseja realmente sair?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            _keySenderService.StopSending();
        }

        private async void LoadWindowsAsync()
        {
            SetCursor(Cursors.WaitCursor);
            comboBoxRagexeWindows.Items.Clear();

            try
            {
                var windows = await Task.Run(() => 
                    WindowEnumerator.GetRagexeWindows()
                );
                
                SafeUIUpdate(() => 
                {
                    comboBoxRagexeWindows.Items.Clear();
                    if (windows != null)
                    {
                        comboBoxRagexeWindows.Items.AddRange(windows.ToArray());
                    }
                    
                    if (comboBoxRagexeWindows.Items.Count > 0)
                        comboBoxRagexeWindows.SelectedIndex = 0;
                });
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro ao listar janelas: {ex.Message}");
            }
            finally
            {
                SetCursor(Cursors.Default);
            }
        }

        private void LoadProfiles()
        {
            try
            {
                var profiles = _profileService.LoadProfiles();
                SafeUIUpdate(() => UpdateProfilesComboBox(profiles));
            }
            catch (ProfileServiceException ex)
            {
                PostLogMessage($"Erro ao carregar perfis: {ex.Message}");
            }
        }

        private void ButtonRagexeWindowsRefresh_Click(object sender, EventArgs e)
        {
            LoadWindowsAsync();
        }

        private void OnbuttonToggleKeySending(object sender, EventArgs e)
        {
            if (_keySenderService.IsSending)
            {
                _keySenderService.StopSending();
            }
            else
            {
                ValidateAndStartSending();
            }
        }

        private bool ValidateAndStartSending()
        {
            if (!int.TryParse(textBoxInterval.Text, out int interval) || interval < 100)
            {
                PostLogMessage("Intervalo inválido! Deve ser numérico e > 100ms");
                return false;
            }

            if (comboBoxRagexeWindows.SelectedItem is not WindowInfo selectedWindow)
            {
                PostLogMessage("Selecione uma janela do jogo antes de iniciar");
                return false;
            }

            try
            {
                _keySenderService.StartSending(
                    selectedWindow,
                    interval,
                    textBoxKeySelection.Text
                );
                return true;
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro ao iniciar: {ex.Message}");
                return false;
            }
        }

        private void CaptureSelectedKey(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            _keySenderService.SetKey(e.KeyValue);
            textBoxKeySelection.Text = e.KeyCode.ToString();
            PostLogMessage($"Tecla alterada: {e.KeyCode}");
        }
        
        private void HandleLogMessage(string message)
        {
            PostLogMessage(message);
        }

        private void PostLogMessage(string message)
        {
            SafeUIUpdate(() => 
            {
                try
                {
                    string timestamp = DateTime.Now.ToString("HH:mm:ss");
                    listBoxLog.Items.Insert(0, $"{timestamp} - {message}");
                    if (listBoxLog.Items.Count > 100) 
                        listBoxLog.Items.RemoveAt(listBoxLog.Items.Count - 1);
                }
                catch {}
            });
        }

        private void HandleSendingStateChanged(bool isSending)
        {
            SafeUIUpdate(() => 
            {
                try
                {
                    buttonToggleKeySending.Text = isSending ? "Desativar" : "Ativar";
                    buttonToggleKeySending.BackColor = isSending 
                        ? Color.LightCoral 
                        : Color.LightGreen;
                }
                catch {}
            });
        }

        private void ButtonSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxProfileName.Text))
            {
                PostLogMessage("Digite um nome para o perfil!");
                return;
            }

            if (!int.TryParse(textBoxInterval.Text, out int interval) || interval < 100)
            {
                PostLogMessage("Intervalo inválido! Deve ser numérico e > 100ms");
                return;
            }

            var profile = new Profile
            {
                Name = textBoxProfileName.Text.Trim(),
                Key = textBoxKeySelection.Text,
                KeyCode = _keySenderService.GetCurrentKeyCode(),
                Interval = interval
            };

            try
            {
                _profileService.AddOrUpdateProfile(profile);
                PostLogMessage($"Perfil salvo: {profile.Name}");

                var profiles = _profileService.LoadProfiles();
                SafeUIUpdate(() => 
                {
                    UpdateProfilesComboBox(profiles);

                    if (comboBoxProfiles.Items.Contains(profile.Name))
                    {
                        comboBoxProfiles.SelectedItem = profile.Name;
                    }
                    else
                    {
                        comboBoxProfiles.SelectedIndex = 0;
                    }
                });
            }
            catch (ProfileServiceException ex)
            {
                PostLogMessage($"Erro ao salvar perfil: {ex.Message}");
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro inesperado: {ex.Message}");
            }
        }

        private void UpdateProfilesComboBox(List<Profile> profiles)
        {
            try
            {
                var selectedItem = comboBoxProfiles.SelectedItem?.ToString();
                
                comboBoxProfiles.Items.Clear();
                comboBoxProfiles.Items.Add("-- Novo Perfil --");
                
                if (profiles != null && profiles.Any())
                {
                    foreach (var profile in profiles
                        .Where(p => p != null && !string.IsNullOrEmpty(p.Name))
                        .OrderBy(p => p.Name))
                    {
                        comboBoxProfiles.Items.Add(profile.Name);
                    }
                }
                
                if (!string.IsNullOrEmpty(selectedItem) && comboBoxProfiles.Items.Contains(selectedItem))
                {
                    comboBoxProfiles.SelectedItem = selectedItem;
                }
                else
                {
                    comboBoxProfiles.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro ao atualizar lista de perfis: {ex.Message}");
            }
        }

        private void ButtonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex <= 0) return;

            string profileName = comboBoxProfiles.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(profileName)) return;
            
            try
            {
                _profileService.DeleteProfile(profileName);
                PostLogMessage($"Perfil excluído: {profileName}");

                SafeUIUpdate(() => 
                {
                    try
                    {
                        var profiles = _profileService.LoadProfiles();
                        UpdateProfilesComboBox(profiles);
                    }
                    catch (Exception ex)
                    {
                        PostLogMessage($"Erro ao atualizar lista: {ex.Message}");
                    }
                });
            }
            catch (ProfileServiceException ex)
            {
                PostLogMessage($"Erro ao excluir perfil: {ex.Message}");
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro inesperado: {ex.Message}");
            }
        }

        private void ComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProfiles.SelectedIndex <= 0) 
            {
                textBoxProfileName.Text = "";
                textBoxKeySelection.Text = "F1";
                textBoxInterval.Text = "2000";
                return;
            }

            string selectedProfile = comboBoxProfiles.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedProfile)) return;
            
            try
            {
                var profile = _profileService.GetProfile(selectedProfile);
                if (profile != null)
                {
                    textBoxKeySelection.Text = profile.Key;
                    _keySenderService.SetKey(profile.KeyCode);
                    textBoxInterval.Text = profile.Interval.ToString();
                    textBoxProfileName.Text = profile.Name;
                    PostLogMessage($"Perfil carregado: {profile.Name}");
                }
            }
            catch (ProfileServiceException ex)
            {
                PostLogMessage($"Erro ao carregar perfil: {ex.Message}");
            }
            catch (Exception ex)
            {
                PostLogMessage($"Erro inesperado: {ex.Message}");
            }
        }

        private void SetCursor(Cursor cursor)
        {
            SafeUIUpdate(() => 
            {
                try
                {
                    Cursor = cursor;
                }
                catch {}
            });
        }
        
        private void SafeUIUpdate(Action action)
        {
            if (IsDisposed) return;
            
            _uiContext.Post(_ => 
            {
                try
                {
                    if (!IsDisposed)
                    {
                        action();
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na atualização da UI: {ex.Message}");
                }
            }, null);
        }
    }
}