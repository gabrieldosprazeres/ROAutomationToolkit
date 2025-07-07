using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ROAutomationToolkit.Models;

namespace ROAutomationToolkit.Services
{
    public static class WindowEnumerator
    {
        [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")] private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")] private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public static List<WindowInfo> GetRagexeWindows()
        {
            var windows = new List<WindowInfo>();
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                if (IsWindowVisible(hWnd) && GetWindowTextLength(hWnd) > 0)
                {
                    var builder = new StringBuilder(GetWindowTextLength(hWnd) + 1);
                    GetWindowText(hWnd, builder, builder.Capacity);
                    string title = builder.ToString();

                    GetWindowThreadProcessId(hWnd, out uint pid);
                    string processName = GetProcessName(pid);
                    if (!processName.Equals("Ragexe", StringComparison.OrdinalIgnoreCase)) return true;

                    DateTime creationTime = DateTime.MinValue;
                    try { creationTime = Process.GetProcessById((int)pid).StartTime; } catch { }

                    windows.Add(new WindowInfo
                    {
                        Handle = hWnd,
                        Title = title,
                        PID = pid,
                        ProcessName = processName,
                        CreationTime = creationTime
                    });
                }
                return true;
            }, IntPtr.Zero);

            return windows.OrderBy(w => w.CreationTime).ToList();
        }

        private static string GetProcessName(uint pid)
        {
            try { return Process.GetProcessById((int)pid).ProcessName; }
            catch { return "Unknown"; }
        }
    }
}