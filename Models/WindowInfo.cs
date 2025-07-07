using System;

namespace ROAutomationToolkit.Models
{
    public class WindowInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
        public uint PID { get; set; }
        public string ProcessName { get; set; }
        public DateTime CreationTime { get; set; }

        public override string ToString()
        {
            return $"{Title} (PID: {PID})";
        }
    }
}
