using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ROAutomationToolkit.Models;

namespace ROAutomationToolkit.Services
{
    public class KeySenderService
    {
        [DllImport("user32.dll")] 
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private const uint WM_KEYDOWN = 0x0100;
        private const uint WM_KEYUP = 0x0101;
        
        public event Action<string> LogMessage;
        public event Action<bool> SendingStateChanged;
        
        private IntPtr targetWindowHandle = IntPtr.Zero;
        private int keyCode = (int)Keys.F1;
        private int interval = 2000;
        public bool IsSending { get; private set; } = false;
        private Thread senderThread;

        public void StartSending(WindowInfo selectedWindow, int interval, string keyText)
        {
            if (selectedWindow == null)
            {
                LogMessage?.Invoke("Selecione uma janela do jogo primeiro!");
                return;
            }

            if (interval < 100)
            {
                LogMessage?.Invoke("Intervalo inválido! Use valores maiores que 100ms");
                return;
            }

            targetWindowHandle = selectedWindow.Handle;
            this.interval = interval;

            LogMessage?.Invoke($"Iniciado: Tecla {keyText} a cada {interval}ms");
            IsSending = true;
            SendingStateChanged?.Invoke(true);
            
            senderThread = new Thread(SendKeysLoop)
            {
                IsBackground = true
            };
            senderThread.Start();
        }

        private void SendKeysLoop()
        {
            while (IsSending)
            {
                DateTime sendTime = DateTime.Now;
                PostMessage(targetWindowHandle, WM_KEYDOWN, keyCode, 0);
                Thread.Sleep(50);
                PostMessage(targetWindowHandle, WM_KEYUP, keyCode, 0);

                LogMessage?.Invoke("Tecla enviada");
                Thread.Sleep(interval);
            }
        }

        public void StopSending()
        {
            if (IsSending)
            {
                IsSending = false;
                LogMessage?.Invoke("Envio interrompido");
                SendingStateChanged?.Invoke(false);
            }
        }
        
        public void SetKey(int keyValue)
        {
            keyCode = keyValue;
        }

        public int GetCurrentKeyCode()
        {
            return keyCode;
        }
    }
}