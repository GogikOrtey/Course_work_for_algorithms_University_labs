using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_07
{
    public class AQ2 // Структура нужных нам данных
    {
        public string fio;
        public string login;
        public string gend;
        public string adress;

        public AQ2()
        {
            fio = "";
            login = "";
            gend = "";
            adress = "";
        }
    };

    public class HashTable_2
    {
        //public static void cout<Type>(Type Input)
        //{
        //    //Console.WriteLine(Input);
        //    Console.Write(Input);
        //}

        //public static void coutnn<Type>(Type Input)
        //{
        //    Console.Write(Input);
        //}

        public static void cout<Type>(Type Input)
        {
            //Console.WriteLine(Input);
            if (modOutput == 1) DebuggingWindow_4.PrintTree(Input.ToString() + "\n");
            else MyConsole.INS(Input.ToString() + "\n");
        }

        public static void coutnn<Type>(Type Input)
        {
            //Console.Write(Input);
            if (modOutput == 1) DebuggingWindow_4.PrintTree(Input.ToString());
            else MyConsole.INS(Input.ToString());
        }

        public static int modOutput;

        public static int SizeHeshTable = 16;     // Размер Хеш Таблицы
        public bool loudMode = false;       // Выводятся ли сообщения при выполнении программы
        public bool developerMode = false; // Сообщения для разработчиков

        AQ2[] DataHT;  // Массив данных ХТ
        int[] Empty;   // 0 -пусто, 1 занято, -1 удалено

        // string[] stringArray = new string[10];

        // Инициализируем пустой экземпляр класса
        public HashTable_2()
        {
            InitHashTable();
        }

        public void InitHashTable()
        {
            DataHT = new AQ2[SizeHeshTable];
            Empty = new int[SizeHeshTable];

            for (int i = 0; i < SizeHeshTable; i++)
            {
                Empty[i] = 0;

                DataHT[i] = new AQ2();

                DataHT[i].fio = " ";
                DataHT[i].login = " ";
                DataHT[i].gend = " ";
                DataHT[i].adress = " ";
            }
        }

        // Деструктор
        ~HashTable_2()
        {
            DataHT = null;
            Empty = null;
        }

        public void DelAllHT()
        {
            DataHT = null;
            Empty = null;
            InitHashTable();
        }

        // -----

        // Выводит данные в консоль
        public void coutM(AQ2 mass)
        {
            int f = 0;

            for (int i = 0; i < SizeHeshTable; i++)
            {
                //if (mass.d[i].ToString() != "")
                //{
                cout("[" + mass.fio[i] + ", " + mass.login[i] + ", " + mass.gend[i] + ", " + mass.adress[i] + "]" + "\n");
                f++;
                //}
            }

            if (f == 0)
            {
                cout("<Пусто>" + "\n");
            }
        }

        // Выводит в консоль только нужную нам строку данных
        public void coutQ(AQ2 mass)
        {
            cout("[" + mass.fio + ", " + mass.login + ", " + mass.gend + ", " + mass.adress + "]" + "\n");
        }

        // Выводит в консоль только нужную нам строку данных, без переноса каретки
        public void coutA(AQ2 mass)
        {
            coutnn("[" + mass.fio + ", " + mass.login + ", " + mass.gend + ", " + mass.adress + "]");
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

            return result;
        }

        // Первичная ХФ, середина квадрата
        public int hash1(string nkeyLogin)
        {
            int key = StringToNumber(nkeyLogin);  // Преобразую строку в число
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
            if (developerMode) cout("__ Hash1(" + nkeyLogin + ") = " + result + "\n");

            return result;
        }

        // Основная процедура хеширования. Использует первичную и вторичную ХФ
        // Вторичная ХФ, Открытая адресация (Квадратичный)
        public int GetFreeHash(string nkeyLogin)
        {
            int i, index;

            // Коэффициенты обязательно должны быть дробными, иначе всё уходит в бесконечный цикл
            float k1 = 0.36f;
            float k2 = 0.65f;

            int mainHash = hash1(nkeyLogin); // Получаем хеш ключа

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
                    if (DataHT[index].login == nkeyLogin) return index;

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
        public int MainFindHash(string nkeyLogin)
        {
            cout("\n");
            int index = GetFreeHash(nkeyLogin);

            if (loudMode) cout("Ищем в ХТ элемент с ключом |" + nkeyLogin + "|" + "\n");

            if (index != -1)
            {
                if (Empty[index] == 1 && DataHT[index].login == nkeyLogin) // Если нашли наш элемент в ХТ
                                                                           // Также проверяем, что эта строка с данными не является удалённой
                {
                    if (loudMode) cout("Нашли этот элемент, в ячейке с индексом " + index + "\n");

                    return index;
                }
            }

            if (loudMode) cout("Не нашли этот элемент, в нашей ХТ." + "\n");
            return -1; // Если не нашли элемента, возвращаем -1, вместо индекса
        }

        // Помечает данные как удалённые, если найдёт
        public void MainDelete(string nkeyLogin)
        {
            int position = MainFindHash(nkeyLogin);

            if (position != -1) // Если ячейка занята (поиск вернул нам индекс ячейки, а не -1)
            {
                Empty[position] = -1; // Помечаем ячейку как удалённую

                if (loudMode) cout("Успешно удалили элемент из " + position + " ячейки ХТ" + "\n");
            }
            else
            {
                if (loudMode) cout("Этого элемента нет в ХТ, или он уже был удалён" + "\n");
            }
        }

        public void MainAdd(string Fio, string Login, string Gend, string Adress)
        {
            AQ2 value = new AQ2();

            value.fio = Fio;
            value.login = Login;
            value.gend = Gend;
            value.adress = Adress;

            Add(value);
        }

        // Добавление элемента в хеш-таблицу
        public bool Add(AQ2 value)
        {
            string nkeyLogin = value.login;

            if (loudMode) cout("\n");
            if (loudMode) cout("Добавляем в ХТ строку ");
            if (loudMode) coutQ(value);

            int index = GetFreeHash(nkeyLogin);

            // Сначала проверяет, нет ли в нашей ХТ такой-же строки с данными
            if ((Empty[index] == 1) && (DataHT[index].login == nkeyLogin))
            {
                // Если есть:
                if (loudMode) cout("Не удалось добавить данные, т.к. они уже есть в ХТ: " + nkeyLogin + "\n");
                return false;
            }

            //Если нашли свободную ячейку для добавления
            if (index != -1)
            {
                // Записываем в неё нашу строку
                DataHT[index].login = nkeyLogin;
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
                if (DataHT[i].login != " ")
                {
                    coutnn("    h1 = " + hash1(DataHT[i].login));
                    cout("    h2 = " + GetFreeHash(DataHT[i].login));
                }
                else cout("");
            }
        }

        public void DebugPrint()
        {
            int mod = DebuggingWindow_4.ModOutput;
            modOutput = 1;
            DebuggingWindow_4.AllDel();
            DebuggingWindow_4.Mod = true;
            print();
            coutnn("\n");
            modOutput = 0;
            DebuggingWindow_4.Mod = false;
        }
    };
}
