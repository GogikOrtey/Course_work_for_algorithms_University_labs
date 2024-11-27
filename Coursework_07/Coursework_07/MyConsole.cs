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
    public partial class MyConsole : Form
    {
        public MyConsole()
        {
            InitializeComponent();
			InitMyConsole();

			Mylabel1 = this.label1;
			MC = this;
			this.Hide();
		}

		static string[] MyConsoleStr = new string[200];

		static Label Mylabel1 = new Label();

		public static MyConsole MC;

		public static void ShowMyConsole()
		{
			MC.Show();
		}

		public static void MainShow()
		{
			MC.WindowState = FormWindowState.Normal;
		}

		void InitMyConsole()
		{
			for (int i = 0; i < 200; i++)
			{
				MyConsoleStr[i] = "";
			}
		}

		public static void INSn(string Str)
		{
			INS(Str + "\n");
		}

		public static void INS(string Str)
		{
			for (int i = 0; i < 200; i++)
			{
				if (MyConsoleStr[i] == "")
				{
					MyConsoleStr[i] = Str;
					break;
				}
			}
			PrintMyConsole();
		}

		static void PrintMyConsole()
		{
			string s = "";
			//int i = 0;

			for (int i = 0; i < 200; i++)
			{
				if (MyConsoleStr[i] != "")
				{
					s += MyConsoleStr[i];
					//s += "\n";
				}
				else break;
			}

			//while ((i < 200) && (MyConsole[i] != ""))
			//{
			//	s += MyConsole[i];
			//	s += "\n";
			//}

			//MessageBox.Show(s, "Вывожу консоль:");
			Mylabel1.Text = s;
		}

		private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void MyConsole_Load(object sender, EventArgs e)
        {

        }
    }
}
