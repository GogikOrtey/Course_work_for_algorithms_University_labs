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
    public partial class Otchet : Form
    {
        #region Как работает общий отчёт
        /*
            При нажатии кнопки "Сформировать отчёт", я:
                
                Перебираю все записи из 1й АВЛ, в 1м окне, ищу по ключу "Адрес"
                У каждой записи из 1й АВЛ, которая нам подошла по Адресу, есть Логин
                    Печатаю все записи из 1й АВЛ, в файл

                Загружаю все данные из ХТ во 2м окне, в 2ю АВЛ, по ключу "Login"
                Для каждой записи из 1й АВЛ, которая нам подошла по Адресу, ищу записи, с таким-же логином во 2й АВЛ
                    Печатаю их все также в файл                 
        */

        /*

        - Начало
        Создаю 2ю АВЛ, по ключу "Логин", из ХТ

        Ищу в 1й АВЛ по Адресу
            Нашёл совпадение, печатаю в файл
                Беру логин из этого совпадения, и ищу по нему во 2й АВЛ
                    Всё что нашёл, также печатаю в файл
        Ищу дальше, а 1й АВЛ по Адресу

        */
        #endregion

        public string outWayOutputTextFile;

        public Form_02 form_02;
        public Form_03 form_03;

        int isTextForSavingVisible = 0; // Виден ли текст о том, что файл успешно сохранён

        public Otchet()
        {
            InitializeComponent();
            MyInit();
        }

        void MyInit()
        {
            label3.Hide();
        }

        // Нажатие на кнопку "Сформировать отчёт"
        private void button5_Click(object sender, EventArgs e)
        {
            // Создать 2ю АВЛ из ХТ, по ключу "Логин"
            form_03.FormAVL();

            // Запускаю поиск в 1й АВЛ, по ключу "Адрес"
            form_02.SearchForAVL(textBox3.Text, dateTimePicker1);

            isTextForSavingVisible = 110;
            label3.Show();
        }

        // Нажатие на кнопку "?"
        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = "Программа ищет совпадение по ключу Адресу у пользователей, в таблице из 1го окна. Таких совпадений может быть несколько" +
                "\n Для каждого такого совпадения, ищутся все публикации пользователя (по ключу Login), из таблицы во 2м окне. Их также может быть несколько." +
                "\n Каждая найденная запись, из 1го и 2го окна выводится в выходной текстовый файл. ";            

            MessageBox.Show(s1, "Как это работает:");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isTextForSavingVisible > 0) isTextForSavingVisible--;
            else label3.Hide();
        }
    }
}
