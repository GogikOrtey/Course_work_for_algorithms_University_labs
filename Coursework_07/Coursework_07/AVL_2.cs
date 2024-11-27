using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Coursework_07
{

    /*
    class Program
    {
        public static Random Random = new Random();

        public static void cout<Type>(Type Input)
        {
            Console.WriteLine(Input);
        }

        public static void coutnn<Type>(Type Input)
        {
            Console.Write(Input);
        }

        static void Main(string[] args)
        {
            //setlocale(LC_ALL, "Rus");

            //cout("Привет");
            //Span((unsigned)DateTime(null));

            AVLTree AvlTree_01 = new AVLTree();

            
                Описание основных процедур:

                AvlTree_01.MainAdd("1", "2", "3", "4");         Добавляет в дерево элемент, с 4мя текстовыми полями
                AvlTree_01.MainDel("4");                        Находит и удаляет из дерева элемент с ключом "4" (ключ = адрес, это последний аргумент в конструкторе)
                AvlTree_01.MainPrint();                         Выводит дерево полностью
                AvlTree_01.MainSearch("1")                      Ищет в дереве элемент, по ключу. Возвращает указатель на найденную вершину, или null

                AvlTree_01.PrintNode(AvlTree_01.MainSearch("1"));       Например, вот так можно сразу найти и распечатать вершину
            

            // Устанавливайте эту переменную true, только если хотите тестировать дерево числовыми значениями
            // При работе со строками, в которых есть символы не цифры - обязательно поставьте эту переменную в false
            AvlTree_01.TestingNumberKeysMod = true;

            AvlTree_01.MainPrint();

            {
                string str;
                int Rand;

                // Рандомный тестировщик
                for (int i = 0; i < 20; i++)
                {
                    //cout + "i =  " + i + endl;
                    if (true) //(i < 12) // ((rand() % 2 + 1) == 1)
                    {
                        Rand = Random.Next(1, 25);

                        str = Rand.ToString();
                        cout("Добавляем " + str);

                        AvlTree_01.MainAdd("1", "1", "1", str);

                        cout("");
                        AvlTree_01.MainPrint();
                        cout("----------------");
                        cout("");
                    }
                    else
                    {
                        Rand = Random.Next(1, 25);
                        str = Rand.ToString();

                        if (AvlTree_01.MainSearch(str) != null)
                        {
                            cout("Удаляем " + Rand);
                            AvlTree_01.MainDel(str);

                            cout("");
                            AvlTree_01.MainPrint();
                            cout("----------------");
                            cout("");
                        }
                    }
                }
            }


            {
                //AvlTree_01.MainAdd("1", "1", "1", "1");
                //AvlTree_01.MainPrint(0);
                //cout("----------------");
                //AvlTree_01.MainAdd("1", "1", "1", "2");
                //AvlTree_01.MainPrint(0);
                //cout("----------------");
                //AvlTree_01.MainAdd("1", "1", "1", "3");
                //AvlTree_01.MainPrint(0);
                //cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "4");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "5");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "6");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "7");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "7");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                ////AvlTree_01.MainAdd("1", "1", "1", "8");
                ////AvlTree_01.MainPrint(0);
                ////cout("----------------");
                //AvlTree_01.MainDel("1");
                //AvlTree_01.MainPrint(0);
            }

            cout("----------------");
            AvlTree_01.PrintMinToMax();
            cout("----------------");
            AvlTree_01.PrintMaxToMin();

            cout("Ищем 1: ");
            AvlTree_01.PrintNode(AvlTree_01.MainSearch("1"), 1);
        }
    }
    */

    /*
    public class MyDate
    {
        int dd, mm, gg;

        public MyDate() { }
        public MyDate(int d, int m, int g)
        {
            dd = d;
            mm = m;
            gg = g;
        }

        public string RetStringForPrint()
        {
            string str = dd + "." + mm + "." + gg;
            return str;
        }


        //public static MyDate operator +(MyDate counter1, MyDate counter2)
        //{
        //    return new MyDate
        //    {
        //        //Value counter1.Value + counter2.Value 
        //
        //        dd = counter1.dd + counter2.dd
        //
        //    };
        //}

        public static bool operator >(MyDate c1, MyDate c2)
        {
            if (c1.gg > c2.gg) return true;
            else if (c1.gg == c2.gg)
            {
                if (c1.mm > c2.mm) return true;
                else if (c1.mm == c2.mm)
                {
                    if (c1.dd > c2.dd) return true;
                }
            }
            return false;
        }
        public static bool operator <(MyDate c1, MyDate c2)
        {
            if ((c1 == c2) || (c1 > c2)) return false;
            else return true;

            //if (c1.gg < c2.gg) return true;
            //else if (c1.mm < c2.mm) return true;
            //else if (c1.dd < c2.dd) return true;
            //else return false;
        }


        public static bool operator ==(MyDate c1, MyDate c2)
        {
            if ((c1.gg == c2.gg) && (c1.mm == c2.mm) && (c1.dd == c2.dd)) return true;
            else return false;
        }
        public static bool operator !=(MyDate c1, MyDate c2)
        {
            if (c1 == c2) return false;
            else return true;

            //if ((c1.gg == c2.gg) && (c1.mm == c2.mm) && (c1.dd == c2.dd)) return false;
            //else return true;
        }
    };
    */

    public class AVL_2
    {
        /*
            При удалении замена на min справа
        */

        //Form_02 Form_02;
        //MyConsole MyConsole;

        public static void cout<Type>(Type Input)
        {
            //Console.WriteLine(Input);
            if (modOutput == 1) DebuggingWindow_3.PrintTree(Input.ToString() + "\n");
            else MyConsole.INS(Input.ToString() + "\n");
        }

        public static void coutnn<Type>(Type Input)
        {
            //Console.Write(Input);
            if (modOutput == 1) DebuggingWindow_3.PrintTree(Input.ToString());
            else MyConsole.INS(Input.ToString());
        }

        public static int modOutput; // Переключает потоки вывода между консолью, и окнами дебага

        // Класс записи
        public class Key_1
        {
            // В дереве повторяющиеся записываются в цепочку, поэтому есть указатель на следующий элемент
            public Key_1 next;
            // Данные хранящиеся в файле
            public string Text, Image, Login;
            public MyDate Date;

            // Конструктор
            public Key_1(MyDate Date_v, string Text_v, string Image_v, string Login_v)
            {
                next = null;
                Date = Date_v;
                Text = Text_v;
                Image = Image_v;
                Login = Login_v;
            }
        };

        // Класс вершины
        public class Node
        {
            public Key_1 key;
            public Node left;
            public Node right;
            public int height;

            // Конструктор. Так же записывает данные в переменные
            public Node(MyDate Date_v, string Text_v, string Image_v, string Login_v)
            {
                left = null;
                right = null;
                height = 1;
                key = new Key_1(Date_v, Text_v, Image_v, Login_v);
            }
        };

        //Указатель на основной корень дерева
        public Node root;

        // Устанавливайте эту переменную true, только если хотите тестировать дерево числовыми значениями (в строковом виде)
        // При работе со строками, в которых есть символы не цифры - обязательно поставьте эту переменную в false
        public bool TestingNumberKeysMod = false;

        public Node GetRoot()
        {
            return root;
        }

        //Конструктор
        public AVL_2()
        {
            root = null;
        }

        //Деструктор
        ~AVL_2()
        {
            if (root != null) Cleaning(root);
        }

        public void MainAllDel()
        {
            Cleaning(root);
            root = null;
        }

        //Очистка дерева
        public void Cleaning(Node tree)
        {
            if (tree != null)
            {
                Cleaning(tree.left);
                Cleaning(tree.right);
                tree.key = null;
                tree = null;
            }
        }

        public int height(Node N)
        {
            if (N == null)
                return 0;
            return N.height;
        }

        public int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        // Возвращает true, если a>b
        bool a_big_b(string a, string b)
        {
            int f = String.Compare(a, b);
            if (f > 0)
            {
                //cout(a + ">" + b);
                return true;
            }
            else
            {
                //cout(a + "<" + b);
                return false;
            }
        }

        public Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;
            x.right = y;
            y.left = T2;
            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;
            return x;
        }

        public Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;
            y.left = x;
            x.right = T2;
            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;
            return y;
        }

        public int getBalanceFactor(Node N)
        {
            if (N == null)
                return 0;
            return height(N.left) - height(N.right);
        }

        //OnlyTestNumbersMod
        public string NumbersToLiterals(string Inp)
        {
            //char A2 = new char[Inp.length() + 1];
            //strcpy(A2, Inp.c_str());
            //int adrInp = atoi(A2); // Преобразовываем входную строку в число
            int adrInp;

            int.TryParse(Inp, out adrInp);

            if (adrInp == 0)
            {
                cout("На вход было подано число 0, либо строка, содержащая символы, кроме цифр."
                    + "Такие входные данные при тестировании недопустимы");

                Environment.Exit(9);
            }

            if ((adrInp < 0) || (adrInp > 25))
            {
                cout("На вход было подано слишком больше число."
                    + "При тестировании числа должны лежать в диапазоне от 1 до 25 включительно");
                Environment.Exit(9);
            }

            adrInp--;

            int nR = (adrInp % 25) + 97;

            char c3 = (char)nR;

            //String str1(1, c3);
            string str1 = c3.ToString();

            return str1;
        }

        //OnlyTestNumbersMod
        public string LiteralsToNumbers(string Inp)
        {
            char Inp2 = Inp[0];
            int Inp3 = (int)Inp2;

            Inp3++;

            Inp3 -= 97;

            string str1 = Inp3.ToString();

            return str1;
        }

        //void CheckAttentionAddFromTree(string Adress)
        //{
        //    int adrInp;
        //    if (int.TryParse(Adress, out adrInp))
        //    {
        //        if (adrInp >= 10)
        //        {
        //            string s1 = "Во входных данных в строке [Адрес] было встречено число >= 10. \n" +
        //                "Напоминаю, что при вставки такого элемента в дерево сравнение " +
        //                "происходит посимвольно (не как число, а как строка) \n" +
        //                "\n Тогда 1>10";
        //            MessageBox.Show(s1, "Будьте внимательны!");
        //        }
        //    }
        //}

        // Основная процедура вставки
        public bool MainAdd(MyDate Date, string Text, string Image, string Login)
        {
            //if (TestingNumberKeysMod == true)
            //{
            //    Adress = NumbersToLiterals(Adress);
            //}

            if (root == null)
            {
                root = new Node(Date, Text, Image, Login);
            }
            else
            {
                root = insertNode(root, Date, Text, Image, Login);
            }
            return true;
        }

        // Вставка элемента в дерево
        public Node insertNode(Node node, MyDate Date, string Text, string Image, string Login)
        {
            if (node == null)
            {
                Node newNode = new Node(Date, Text, Image, Login);
                return (newNode);
            }

            Node Sear = InsideSearch(root, Login);

            if (Sear == null)
            {
                if (string.Compare(Login, node.key.Login) < 0)
                    node.left = insertNode(node.left, Date, Text, Image, Login);
                else if (string.Compare(Login, node.key.Login) > 0)
                    node.right = insertNode(node.right, Date, Text, Image, Login);
                else
                    return node;
            }
            else
            {
                Key_1 vertex = new Key_1(Date, Text, Image, Login);

                if (Sear.key.next == null)
                {
                    Sear.key.next = vertex;
                }
                else
                {
                    Key_1 buff;
                    buff = Sear.key;
                    while (buff.next != null) buff = buff.next;
                    buff.next = vertex;
                }
                return root;
            }

            node.height = 1 + max(height(node.left),
                height(node.right));
            int balanceFactor = getBalanceFactor(node);
            if (balanceFactor > 1)
            {
                if (string.Compare(Login, node.left.key.Login) < 0)
                {
                    return rightRotate(node);
                }
                else if (string.Compare(Login, node.left.key.Login) > 0)
                {
                    node.left = leftRotate(node.left);
                    return rightRotate(node);
                }
            }
            if (balanceFactor < -1)
            {
                if (string.Compare(Login, node.right.key.Login) > 0)
                {
                    return leftRotate(node);
                }
                else if (string.Compare(Login, node.right.key.Login) < 0)
                {
                    node.right = rightRotate(node.right);
                    return leftRotate(node);
                }
            }
            return node;
        }

        public Node nodeWithMimumValue(Node node)
        {
            Node current = node;
            while (current.left != null)
                current = current.left;
            return current;
        }

        // Основная процедура удаления
        public void MainDel(string Login)
        {
            //if (TestingNumberKeysMod == true)
            //{
            //    Adress = NumbersToLiterals(Adress);
            //}

            root = deleteNode(root, Login, false);
        }

        // Удаление по ключу
        public Node deleteNode(Node root, string Login, bool isDontReturnDubleNode)
        {
            if (root == null)
                return root;

            if (Login == root.key.Login)
            {
                if ((root.key.next == null) || (isDontReturnDubleNode == true))
                {
                    if ((root.left == null) || (root.right == null))
                    {
                        Node temp = (root.left != null) ? root.left : root.right;
                        if (temp == null)
                        {
                            temp = root;
                            root = null;
                        }
                        else
                            root = temp;
                    }
                    else
                    {
                        Node temp = nodeWithMimumValue(root.right);
                        root.key = temp.key;
                        root.right = deleteNode(root.right, temp.key.Login, true);
                    }
                }
                else
                {
                    Key_1 p1 = root.key;
                    while (p1.next.next != null)
                        p1 = p1.next;
                    p1.next = null;                    
                }
            }
            else if (string.Compare(Login, root.key.Login) < 0)
                root.left = deleteNode(root.left, Login, isDontReturnDubleNode);
            else //if (a_big_b(Adress, root.key.adress))
                root.right = deleteNode(root.right, Login, isDontReturnDubleNode);

            if (root == null)
                return root;

            root.height = 1 + max(height(root.left),
                height(root.right));
            int balanceFactor = getBalanceFactor(root);
            if (balanceFactor > 1)
            {
                if (getBalanceFactor(root.left) >= 0)
                {
                    return rightRotate(root);
                }
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }
            if (balanceFactor < -1)
            {
                if (getBalanceFactor(root.right) <= 0)
                {
                    return leftRotate(root);
                }
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }
            return root;
        }

        public Node ResSearch; // Переменная, для хранения результатов поиска

        // Поиск по ключу
        public bool SearchAdressOnly(Node node, string Login)
        {
            if ((node != null) && (node.key != null))
            {
                if (Login == node.key.Login)
                {
                    ResSearch = node;
                    return true;
                }
                else if (string.Compare(Login, node.key.Login) > 0)
                    SearchAdressOnly(node.right, Login);
                else
                    SearchAdressOnly(node.left, Login);
            }

            return false;
        }

        // Нужна для внутреннего поиска, для встваки дублей. Используется в процедуре вставки
        public Node InsideSearch(Node root1, string Login)
        {
            ResSearch = null;

            SearchAdressOnly(root1, Login);

            return (ResSearch);
        }

        // Основная процедура поиска
        public Node MainSearch(string Login)
        {
            ResSearch = null;

            //if (TestingNumberKeysMod == true)
            //{
            //    Adress = NumbersToLiterals(Adress);
            //}

            SearchAdressOnly(this.root, Login);

            return (ResSearch);
        }

        // Вывожу данные вершины
        public void CoutNode(Key_1 p, int mod)
        {
            string str = p.Login;

            //if (TestingNumberKeysMod == true)
            //    str = LiteralsToNumbers(p.adress);

            if (mod == 0) coutnn(str);
            else coutnn("" + str + ", " + p.Text
                + ", " + p.Image + ", " + p.Login);
        }

        // Оюрабатываю вывод, если в одной вершине несколько дублей
        public void PrintNode(Node p, int mod)
        {
            if (p != null)
            {
                if (p.key.next == null)
                {
                    CoutNode(p.key, mod);
                    coutnn("\n");
                }
                else // Если не одна запись
                {
                    Key_1 p1 = p.key;
                    while (p1 != null)
                    {
                        CoutNode(p1, mod);
                        if (p1.next != null) coutnn(" | ");
                        p1 = p1.next;
                    }
                    coutnn("\n");
                }
            }
            else
                cout("[Пустая запись]");
        }

        // Процедура вывода дерева (слева направо)
        public void Print1(Node p, int indent, int mod)
        {
            /*
                mod: Как выводить дерево
                0) Только ключи (адрес), в т.ч. и повторяющиеся - в строку, через |
                1) Все записи полностью
            */

            if ((p != null) && (p.key != null))
            {
                if (p.right != null)
                {
                    Print1(p.right, indent + 4, mod);
                }
                if (indent != 0)
                {
                    for (int i = 0; i < indent; i++)
                    {
                        coutnn("  ");
                    }
                }
                if (p.right != null)
                {
                    coutnn(" /\n");
                    for (int i = 0; i < indent; i++)
                    {
                        coutnn("  ");
                    }
                }

                PrintNode(p, mod);

                if (p.left != null)
                {
                    for (int i = 0; i < indent; i++)
                    {
                        coutnn("  ");
                    }
                    coutnn(" \\\n");
                    Print1(p.left, indent + 4, mod);
                }
            }
        }

        // Основная процедура вывода дерева
        public void MainPrint(int mod)
        {
            if (root == null)
            {
                cout("[Дерево пусто]");
            }
            Print1(root, 0, mod);
            coutnn("\n");
        }

        public void DebugPrint()
        {
            int mod = DebuggingWindow_3.ModOutput;
            modOutput = 1;
            DebuggingWindow_3.AllDel();
            DebuggingWindow_3.Mod = true;
            if (root == null)
            {
                cout("[Дерево пусто]");
            }
            Print1(root, 0, mod);
            coutnn("\n");
            modOutput = 0;
            DebuggingWindow_3.Mod = false;
        }

        // Процедура без аргумента
        public void MainPrint()
        {
            if (root == null)
            {
                cout("[Дерево пусто]");
            }
            Print1(root, 0, 0);
            coutnn("\n");
        }

        public void PrintMaxToMin()
        {
            PKL(root);
        }

        // Вывод от max до min
        public void PKL(Node n)
        {
            if (n != null)
            {
                PKL(n.right);
                PrintNode(n, 0);
                PKL(n.left);
            }
        }

        public void PrintMinToMax()
        {
            LKP(root);
        }

        // Вывод от min до max
        public void LKP(Node n)
        {
            if (n != null)
            {
                LKP(n.left);
                PrintNode(n, 0);
                LKP(n.right);
            }
        }
    };
}
