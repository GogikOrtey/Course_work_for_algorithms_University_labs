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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MyConsole.INS("Вас приветствует Master MyConsole\n");
        }

        Form_02 form_02 = new Form_02();
        Form_03 form_03 = new Form_03();

        Otchet otchet = new Otchet();

        //public static Form_03 form_03A = form_03;

        //DebuggingWindow_1 DebuggingWindow_1 = new DebuggingWindow_1();

        //MyConsole MyConsole;

        // 1я форма
        private void button1_Click_1(object sender, EventArgs e)
        {
            form_02.Show();
            form_02.form_03 = form_03;
            MyConsole.INS("Режим пассивной агрессии активирован\n");
            //DebuggingWindow_1.WindowState = FormWindowState.Normal;
        }

        // 2я форма
        private void button2_Click(object sender, EventArgs e)
        {
            form_03.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyConsole.MainShow();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string s4 = "• Нельзя сначала закрывать окна, а потом пытаться их открыть";
            string s5 = "• Содержимое файлов при загрузке не проверяется. Учитывается только их разрешение";
            string s6 = "• ХТ макисмально может содержать только " + HashTable.SizeHeshTable + " записи, т.к. у меня не динамическая реализация";

            MessageBox.Show(s4 + " \n" + s5 + " \n" + s6, "Важно!");
        }

        // Кнопка "Сформировать отчёт"
        private void button5_Click(object sender, EventArgs e)
        {
            otchet.form_02 = form_02;
            otchet.form_03 = form_03;

            otchet.Show();
        }

        // При нажатии на кнопку "New"
        private void button6_Click(object sender, EventArgs e)
        {
            // Чищу все файлики
            form_02.comboMainWay = "Empty.txt";
            form_02.DataLoad();
            form_03.comboMainWay = "Empty.txt";
            form_03.DataLoad();
        }
    }
}
