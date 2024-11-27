using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework_07
{
    public class AQ // Структура нужных нам данных
    {
        public string Text, Image, Login;
        public MyDate Date;

        public AQ(MyDate Date_v, string Text_v, string Image_v, string Login_v)
        {
            Date = Date_v;
            Text = Text_v;
            Image = Image_v;
            Login = Login_v;
        }

        public AQ()
        {
            Date    = new MyDate(0,0,0);
            Text    = "";
            Image   = "";
            Login   = "";
        }
    };

    public class HashTable
    {
        public static void cout<Type>(Type Input)
        {
            if (modOutput == 1) DebuggingWindow_2.PrintTree(Input.ToString() + "\n");
            else MyConsole.INS(Input.ToString() + "\n");
        }

        public static void coutnn<Type>(Type Input)
        {
            if (modOutput == 1) DebuggingWindow_2.PrintTree(Input.ToString());
            else MyConsole.INS(Input.ToString());
        }

        public static int modOutput;

        public static int SizeHeshTable = 16;     // Размер Хеш Таблицы
        public bool loudMode = false;       // Выводятся ли сообщения при выполнении программы
        public bool developerMode = false; // Сообщения для разработчиков

        public AQ[] DataHT;  // Массив данных ХТ
        public int[] Empty;   // 0 -пусто, 1 занято, -1 удалено

        public int countFullingHT = 0;

        // string[] stringArray = new string[10];

        // Инициализируем пустой экземпляр класса
        public HashTable()
        {
            InitHashTable();
        }

        public void InitHashTable()
        {
            DataHT = new AQ[SizeHeshTable];
            Empty = new int[SizeHeshTable];

            countFullingHT = 0;

            for (int i = 0; i < SizeHeshTable; i++)
            {
                Empty[i] = 0;

                DataHT[i] = new AQ();

                DataHT[i].Date = new MyDate(0,0,0);
                DataHT[i].Text = "";
                DataHT[i].Image = "";
                DataHT[i].Login = "";
            }
        }

        // Деструктор
        ~HashTable() 
        {
            DataHT = null;
            Empty = null;
        }

        public void DelAllHT()
        {
            countFullingHT = 0;
            DataHT = null;
            Empty = null;
            InitHashTable();
        }

        // -----

        // Выводит данные в консоль
        public void coutM(AQ mass)
        {
            int f = 0;

            for (int i = 0; i < SizeHeshTable; i++)
            {
                //if (mass.d[i].ToString() != "")
                //{
                cout("[" + mass.Date.RetStringForPrint()[i] + ", " + mass.Text[i] + ", " + mass.Image[i] + ", " + mass.Login[i] + "]" + "\n");
                f++;
                //}
            }

            if (f == 0)
            {
                cout("<Пусто>" + "\n");
            }
        }

        // Выводит в консоль только нужную нам строку данных
        public void coutQ(AQ mass)
        {
            cout("[" + mass.Date.RetStringForPrint() + ", " + mass.Text + ", " + mass.Image + ", " + mass.Login + "]" + "\n");
        }

        // Выводит в консоль только нужную нам строку данных, без переноса каретки
        public void coutA(AQ mass)
        {
            coutnn("[" + mass.Date.RetStringForPrint() + ", " + mass.Text + ", " + mass.Image + ", " + mass.Login + "]");
        }

        // Выводит русскими буквами: свободно, занято, или удалено
        public string isEmpty(int n)
        {
            if (n == 0) return "свободно";
            if (n == 1) return "занято";
            if (n == -1) return "удалено";
            return "/";
        }

        // Преобразовывает строку в числовую сумму всех её символов
        public int StringToNumber(string str)
        {
            int size = str.Length;
            int result = 0;

            for (int i = 0; i < size; i++)
            {
                result += (int)str[i]; // Явное преобразование char в int
            }

            //cout("res(" + str + ") = " + result);

            return result;
        }

        // Первичная ХФ, середина квадрата
        public int hash1(string nkeyData, string login)
        {
            int key = StringToNumber(nkeyData);  // Преобразую строку в число
            int keyDate = StringToNumber(login);

            key = key + keyDate; // Собираю комбинированный ключ. Вроде правильно

            MyConsole.INSn("$$$Key (" + nkeyData + ") = " + key.ToString());
            //cout( "key = " + key + "\n");
            int key2 = key * key;               // Возвожу в квадрат
                                                //cout( "key2 = " + key2 + "\n");
            int rang = -1;  // Ранг - это колличество цифр в числе (например 100 - тут 3 цифры)
            int y = 10;     // Десятичный множитель
            int i = 1;      // Число проходов

            // Вычисляем ранг квадрата
            while (rang == -1)
            {
                if (key2 < y)
                {
                    rang = i;
                    break;
                }

                i++;
                y = (int)Math.Pow(10, i);
            }

            //cout( "rang(key2) = " + rang + "\n");

            int result = key2;
            bool isCh = false;

            // Проверяем на чётность кол-во цифр в числе
            if ((rang % 2) == 1)
            {
                isCh = false;
                // Если число состоит из нечётного кол-ва цифр, мы выводим среднюю
            }
            else
            {
                isCh = true;
                rang--;
                // Если из чётного - выводим 2 из середины
            }

            // До половины укорачиваем число справа
            for (int ii = 0; ii < (rang / 2); ii++)
                result /= 10;

            // Если число состояло из чётного кол-ва цифр, то берём 2 последних цифры    
            if (isCh)
                result = result % 100;
            else
                result = result % 10; // Если нет - то одну последнюю

            result = result % SizeHeshTable; // Приводим число к размерности ХТ
            if (developerMode) cout("__ Hash1(" + nkeyData + ") = " + result + "\n");

            return result;
        }

        // Основная процедура хеширования. Использует первичную и вторичную ХФ
        // Вторичная ХФ, Открытая адресация (Квадратичный)
        public int GetFreeHash(string nkeyData, string login)
        {
            int i, index;

            // Коэффициенты обязательно должны быть дробными, иначе всё уходит в бесконечный цикл
            float k1 = 0.36f;
            float k2 = 0.65f;

            int mainHash = hash1(nkeyData, login); // Получаем хеш ключа

            // Создаём массив размерности ХТ, для учёта ячеек, в которые мы уже пытались положить эту строку,
            // Если предыдущая строка по хешу уже была занята.
            // Такой массив даёт нам гарантию того, что мы проверим все ячейки ХТ, при поиске свободного места
            int[] processed = new int[SizeHeshTable];
            int busy = 0;

            for (i = 0; i < SizeHeshTable; i++)
            {
                processed[i] = 0;
            }

            i = 0;

            while (true)
            {
                // Первичная ХФ - при i = 0, вторичная - при i > 0
                index = (int)(mainHash + (k1 * i) + (k2 * i * i)) % SizeHeshTable;

                if (developerMode) if (i != 0) cout("{Ячейка занята, используем вторичную ХФ}: hash2(" + mainHash + ", i = " + i + ") = " + index + "\n");

                if (Empty[index] == 0)
                {
                    if (loudMode) cout("Нашли свободное место в ХТ за " + i + 1 + " итераций, с индексом " + index + "\n");
                    return index;
                }
                else
                {
                    // Если при хеше мы нашли строку с такими-же данными, то возвращаем индекс этой строки
                    if (DataHT[index].Date.RetStringForPrint() == nkeyData) return index;

                    // Дальше, ячейка занята, указываем это в массиве processed
                    if (processed[index] == 0)
                    {
                        processed[index] = 1;
                        busy++; // Увеличиваем кол-во занятых ячеек

                        if (busy >= SizeHeshTable)
                        {
                            // Если кол-во занятых ячеек >= размеру ХТ, то мы не нашли свободного места
                            if (developerMode) cout("__ Не нашли места в ХТ, busy = " + busy + " i = " + i + "\n");
                            return -1; // Вместо индекса возвращаем -1
                        }
                    }
                }

                i++;
            }
            return -1;
        }

        // Ищет строку по ключу в нашей ХТ
        public int MainFindHash(string nkeyData, string login)
        {
            cout("\n");
            int index = GetFreeHash(nkeyData, login);

            if (loudMode) cout("Ищем в ХТ элемент с ключом |" + nkeyData + "|" + "\n");

            if (index != -1)
            {
                if (Empty[index] == 1 && DataHT[index].Date.RetStringForPrint() == nkeyData) // Если нашли наш элемент в ХТ
                                                                           // Также проверяем, что эта строка с данными не является удалённой
                {
                    if (loudMode) cout("Нашли этот элемент, в ячейке с индексом " + index + "\n");

                    return index;
                }
            }

            if (loudMode) cout("Не нашли этот элемент, в нашей ХТ." + "\n");
            return -1; // Если не нашли элемента, возвращаем -1, вместо индекса
        }

        public AQ MainFind(MyDate nkeyData, string login)
        {
            MyConsole.INSn("hash(" + nkeyData.RetStringForPrint() + ") = " + MainFindHash(nkeyData.RetStringForPrint(), login));

            int a = 0;

            int resHashCode = MainFindHash(nkeyData.RetStringForPrint(), login);

            if (resHashCode != -1)
            {
                return (DataHT[resHashCode]);
            }
            else
            {
                return new AQ(); // Если не нашёл, то возвращаю пустую запись
            }
        }

        // Помечает данные как удалённые, если найдёт
        public void MainDelete(MyDate nkeyData, string login)
        {
            int position = MainFindHash(nkeyData.RetStringForPrint(), login);

            if (position != -1) // Если ячейка занята (поиск вернул нам индекс ячейки, а не -1)
            {
                Empty[position] = -1; // Помечаем ячейку как удалённую

                if (loudMode) cout("Успешно удалили элемент из " + position + " ячейки ХТ" + "\n");
                countFullingHT--;
            }
            else
            {
                if (loudMode) cout("Этого элемента нет в ХТ, или он уже был удалён" + "\n");
            }
        }

        // Удаляем все записи, с этим логином
        public void MainDeleteForLogin(string Login)
        {
            bool a = false;
            for (int i = 0; i < SizeHeshTable; i++)
            {
                if (DataHT[i].Login == Login)
                {
                    if (Empty[i] == 1)
                    {
                        if (a == false)
                        {
                            a = true;
                            string s1 = "Все публикации пользователя с логином " + Login + " будут также удалены.";
                            MessageBox.Show(s1, "Внимание!");
                        }
                        Empty[i] = -1;
                        countFullingHT--;
                    }
                }
            }
        }

        public void MainAdd(MyDate Date1, string Text1, string Image1, string Login1)
        {
            AQ value = new AQ();

            value.Date = Date1;
            value.Login = Login1;
            value.Text = Text1;
            value.Image = Image1;

            Add(value);
        }

        void FatalErrorForOverflow()
        {
            string s1 = "Переполнение Хеш-таблицы. Программа будет экстренно остановлена";
            MessageBox.Show(s1, "Внимание! Фатальная ошибка:");
            Environment.Exit(99);
        }

        // Добавление элемента в хеш-таблицу
        public bool Add(AQ value)
        {
            if (countFullingHT >= SizeHeshTable)
            {
                FatalErrorForOverflow();
            }

            MyDate nkeyData = value.Date;

            if (loudMode) cout("\n");
            if (loudMode) cout("Добавляем в ХТ строку ");
            if (loudMode) coutQ(value);

            int index = GetFreeHash(nkeyData.RetStringForPrint(), value.Login);

            // Сначала проверяет, нет ли в нашей ХТ такой-же строки с данными
            if ((Empty[index] == 1) && (DataHT[index].Date == nkeyData))
            {
                // Если есть:
                if (loudMode) cout("Не удалось добавить данные, т.к. они уже есть в ХТ: " + nkeyData + "\n");
                return false;
            }

            //Если нашли свободную ячейку для добавления
            if (index != -1)
            {
                countFullingHT++;
                // Записываем в неё нашу строку
                DataHT[index].Date = nkeyData;
                DataHT[index] = value;
                Empty[index] = 1;
                return true;
            }
            else
            {
                // Если не нашли свободного места
                if (loudMode) cout("Нет места для этого элемента" + "\n");
                return false;
            }
        }

        // Выводим всю Хеш Таблицу
        public void print()
        {
            cout("\n");
            cout("Выводим всю Хеш Таблицу:" + "\n");

            for (int i = 0; i < SizeHeshTable; i++)
            {
                // Чуть сложного кода для красивого вывода
                coutnn(i + ":  ");
                if (i < 10) coutnn(" ");
                coutA(DataHT[i]);
                coutnn("  <" + isEmpty(Empty[i]) + ">");
                if (DataHT[i].Login != "")
                {
                    coutnn("    h1 = " + hash1(DataHT[i].Date.RetStringForPrint(), DataHT[i].Login));
                    cout("    h2 = " + GetFreeHash(DataHT[i].Date.RetStringForPrint(), DataHT[i].Login));
                }
                else cout("");
            }
        }

        public void DebugPrint()
        {
            int mod = DebuggingWindow_2.ModOutput;
            modOutput = 1;
            DebuggingWindow_2.AllDel();
            DebuggingWindow_2.Mod = true;
            print();
            coutnn("\n");
            modOutput = 0;
            DebuggingWindow_2.Mod = false;
        }
    };
}
