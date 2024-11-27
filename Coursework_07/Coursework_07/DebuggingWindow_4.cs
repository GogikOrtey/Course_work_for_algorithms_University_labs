using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework_07
{
    public partial class DebuggingWindow_4 : Form
    {
        public DebuggingWindow_4()
        {
            InitializeComponent();
            Mylabel1 = this.label1;
            //Mylabel2 = this.panel1;
            MC = this;
            Mylabel2.Hide();

            //for (int i = 0; i < 200; i++)
            //{
            //    MyConsoleStr[i] = "";
            //}
        }

        static Label Mylabel1 = new Label();
        static Panel Mylabel2 = new Panel();

        public static DebuggingWindow_4 MC;

        public static bool Mod = false;

        public static int ModOutput = 0;

        //public static string[] MyConsoleStr = new string[200];

        public static void AllDel()
        {
            Mylabel1.Text = "";
            Mylabel2.Hide();
        }

        //static bool isEmpty = true;

        public static void PrintTree(string str)
        {
            if (Mod == false) Mylabel1.Text = "";

            Mylabel1.Text += str;

            //for (int i = 0; i < 200; i++)
            //{
            //    if (MyConsoleStr[i] == "")
            //        MyConsoleStr[i] += str;
            //}
            //isEmpty = false;

            Mylabel2.Hide();
        }

        private void DebuggingWindow_2_Load(object sender, EventArgs e)
        {

        }

        private void DebuggingWindow_4_Load(object sender, EventArgs e)
        {

        }
    }
}
