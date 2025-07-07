using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace ROAutomationToolkit
{
    partial class MainForm
    {
        private IContainer components = null;
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
            // ===========================================
            // MainForm
            // ===========================================
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(350, 300);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO Automation Toolkit v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}