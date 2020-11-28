using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Power_Hammer_AutoClicker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP = 0x0004;
        private const int LEFTDOWN = 0x0002;
        public int intervals = 5;
        public bool ClicK = false;
        public int parsedValue;



        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread AC = new Thread(AutoClick);
            backgroundWorker1.RunWorkerAsync();

            AC.Start();
        }
        private void AutoClick()
        {
            while (true)
            {
                if (ClicK == true)
                {
                    mouse_event(dwFlags: LEFTUP, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: LEFTDOWN, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(intervals);
                }
                Thread.Sleep(2);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (checkBox1.Checked)
                {
                    if (GetAsyncKeyState(Keys.F6)< 0)
                    {
                        ClicK = false;
                    }else if (GetAsyncKeyState(Keys.F7)<0)
                    {
                        ClicK = true;
                    }
                    Thread.Sleep(1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out parsedValue))
            {
                MessageBox.Show("Please enter a number", "Unacceptable Character");
                return;
            }else
            {
                intervals = int.Parse(textBox1.Text);
                MessageBox.Show("Sucessfully Set Interval", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            Thread.Sleep(1);
        } 
        
        
    }
}
