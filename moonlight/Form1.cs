using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using CeleryAPI;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace moonlight
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public Form1()
        {
            InitializeComponent();
        }

        private async void execute_Click(object sender, EventArgs e)
        {
            try
            {
                string script = "window.getEditorContent();";
                var result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                string editorContent = System.Text.Json.JsonSerializer.Deserialize<string>(result); // Deserialize JSON result
                CeleryAPI.ByfronPlayer.execute(editorContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void inject_Click(object sender, EventArgs e)
        {
            CeleryAPI.ByfronPlayer.Inject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://moonlight.dawg.tf";
            Process.Start(url);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri(Path.Combine(Application.StartupPath, @"Monaco\index.html"));
        }


        private async void InitializeWebView()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        private void minimizeButton_Click(object sender, System.EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void maximizeButton_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
