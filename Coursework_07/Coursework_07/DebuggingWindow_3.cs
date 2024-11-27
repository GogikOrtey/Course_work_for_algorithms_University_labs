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
    public partial class DebuggingWindow_3 : Form
    {
        public DebuggingWindow_3()
        {
            InitializeComponent();
            Mylabel1 = this.label1;
            Mylabel2 = this.panel1;
            MC = this;
            Mylabel2.Hide();

            //for (int i = 0; i < 200; i++)
            //{
            //    MyConsoleStr[i] = "";
            //}
        }

        static Label Mylabel1 = new Label();
        static Panel Mylabel2 = new Panel();

        public static DebuggingWindow_3 MC;

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ModOutput == 0)
            {
                ModOutput = 1;
                this.button1.Text = "All Data";
            }
            else
            {
                ModOutput = 0;
                this.button1.Text = "Adress only";
            }
            //DebugPrint(); // Не могу так сделать, т.к. здесь не инициализирован клас дерева
            Mylabel2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
