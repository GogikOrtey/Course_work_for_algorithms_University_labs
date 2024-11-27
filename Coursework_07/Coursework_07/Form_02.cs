using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Coursework_07
{

	#region comment1
	/*
		Задачи:

		• Доделать отчёт
			• Распечатать его и сброшурировать

		• Сделать презентацию		
	*/
	#endregion

	#region Как работает общий отчёт
	/*

		Составление отчёта протестировать
		Пофиксить обратную связь таблиц
			При удалении пользователя 5 не удаляются записи пользователей с логинм 5	

		Во 2м окне сделать комбинированную ХТ: Не просто по Дате, а Дата + Логин
			Также во 2м окне добавить ещё одно поле для поиска "Логин"

		Добавить ХТ для 1го окна, по ключу "Логин" -> для проверки уникальности, при добвлении

		При запуске отчёта по 2м таблицам в 1м окне все данные добавляются в ХТ
			А во 2м окне в АВЛ
		В окне запуска отчёта ищем Адрес + Дата: 
			Проходим по 1й АВЛ, ищем совпадения по адресу
			Если нашли - берём логин этой записи из ХТ в 1м окне
			Передаём во 2е окно, ищем там в новой АВЛ по логину
				У всех записей которые найдём - проверяем дату.
			Если дата совпадает - выводим в выходной файл эту запись из 2го окна	
	*/
	#endregion

	public partial class Form_02 : Form
	{
		public Form_02()
		{
			InitializeComponent();
			MyInitializations();    // Моя дополнительная инициализация, перед началом работы с формой
		}

		void MyInitializations()
		{
			this.BackColor = System.Drawing.SystemColors.Control;

			panel1.Hide();                  // Это панель с 4мя полями, для ввода данных

			MyTextBox_02_F = textBox1;      // Сразу переназначаю текст боксы из этих полей, на нужные мне (для удобства)
			MyTextBox_02_L = textBox2;
			MyTextBox_02_A = textBox3;
			MyComboBox_02_Gend = comboBox1;

			StartSearch1.Hide();            // Это кнопки, которые появляются при старте поиска. Изначально они скрыты
			CloseSearch1.Hide();

			label2.Hide();                  // Это полупрозрачные тексты про неудавшийся поиск, и что элементов в таблице не осталось
			label10.Hide();
			textBox6.Hide();
			label14.Hide();

			AvlTree_01 = new AVL_1();
			hashTable_2 = new HashTable_2();

			comboMainWay = "D:/Рабочий стол/Учёба 3й курс 2022/Учёба 2022 2й курс/Структуры данных/0_Курсовая/InputFiles/Numbers.gog1";

			MyConsole myConsole = new MyConsole();
			myConsole.Show();

			// %USERPROFILE%/Рабочий стол
			// %USERPROFILE%/Desktop
			// D:/Рабочий стол/_Учёба 2022/Структуры данных/0_Курсовая/InputFiles/

			openFileDialog1.InitialDirectory = "%USERPROFILE%/Рабочий стол/Учёба 3й курс 2022/Учёба 2022 2й курс/Структуры данных/0_Курсовая/InputFiles/";
			openFileDialog1.Filter = "gog files (*.gog1)|*.gog1";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.FileName = "TextFile_2.gog1";

			#region ExamplesAddData

			/*
				Описание основных процедур:

				AvlTree_01.MainAdd("1", "2", "3", "4");         Добавляет в дерево элемент, с 4мя текстовыми полями
				AvlTree_01.MainDel("4");                        Находит и удаляет из дерева элемент с ключом "4" (ключ = адрес, это последний аргумент в конструкторе)
				AvlTree_01.MainPrint();                         Выводит дерево полностью
				AvlTree_01.MainSearch("1")                      Ищет в дереве элемент, по ключу. Возвращает указатель на найденную вершину, или null

				AvlTree_01.PrintNode(AvlTree_01.MainSearch("1"));       Например, вот так можно сразу найти и распечатать вершину
			*/

			//AvlTree_01.MainAdd("Орлов Георгий Александрович", "gogortey", "М", "ул.Державина 21");
			//AvlTree_01.MainAdd("Кравцов Максим Павлович", "maksim.krav", "М", "ул.Матрёшкина 43");
			//AvlTree_01.MainAdd("Исаева Варвара Ярославовна", "aveyasi", "Ж", "ул.Иваненко 19");
			//AvlTree_01.MainAdd("Поляков Ярослав Кириллович", "polyakov04", "М", "ул.Зайцев 44");
			//AvlTree_01.MainAdd("Окулова Мирослава Максимовна", "miroslava", "Ж", "ул.Вермеева 12");
			//AvlTree_01.MainPrint(2);

			//AvlTree_01.MainAdd("1", "1", "1", "1");
			//AvlTree_01.MainAdd("1", "2", "1", "1");
			//AvlTree_01.MainAdd("1", "3", "1", "1");
			//AvlTree_01.MainAdd("1", "4", "1", "1");
			//AvlTree_01.MainAdd("1", "5", "1", "1");

			//AvlTree_01.MainAdd("1", "6", "1", "2");
			//AvlTree_01.MainAdd("1", "7", "1", "3");
			//AvlTree_01.MainAdd("1", "8", "1", "4");
			//AvlTree_01.MainAdd("1", "9", "1", "5");
			//AvlTree_01.MainAdd("1", "10","1",  "6");

			#endregion

			DataLoad();

			MainPrintData();

			AllInitial();

			DebuggingWindow_1 = new DebuggingWindow_1();
			DebuggingWindow_4 = new DebuggingWindow_4();
			AvlTree_01.DebugPrint();    // Debug - отдельное окошко с белым фоном. Открывается по нажатию кнопки "Debug"

			//MyConsole.INSn("Текст");  // MyConsole - это моя реализация стандартной консоли. Туда можно отправить любую строку
		}

		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//					  Объявление моих локальных переменных:						//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		public static AVL_1 AvlTree_01;
		public HashTable_2 hashTable_2;

		public string comboMainWay;	// Путь + имя файла

		DebuggingWindow_1 DebuggingWindow_1; // Окно дебага, в нём вырисовывается дерево
		DebuggingWindow_4 DebuggingWindow_4;

		public Form_03 form_03;

		TextBox MyTextBox_02_F;		// Дополнительные текст боксы, для введения данных (4 текстовых поля сверху окна)
		TextBox MyTextBox_02_L;
		TextBox MyTextBox_02_A;
		ComboBox MyComboBox_02_Gend;

		Button ButtEnterEdit_02;	// Кнопки, которые появляются при старте поиска
		Button ButtExitEdit_02;

		int StartWh = 90;   // С какой высоты от верхней границы окна мы начинаем выодить элементы таблицы


		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//								Локльные методы:								//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		//-------------------------------//
		//		Отчёт из 2х таблиц 		 //
		//-------------------------------//

		StreamWriter OutOthFile;
		string OutOthAdress; // Ключ адреса из окошка отчёта
		MyDate othData;

		public void AddDataForOutputFile(string fio, string login, string gend, string adress)
		{
			if (adress == OutOthAdress)
			{
				form_03.SearchForAVL2(login, othData); // Ищу во 2м окне, по логину 

				//string str = fio + ", " + login + ", " + gend + ", " + adress + ", ";
				//OutOthFile.WriteLine(str); // Сохраняю запись в файл
			}
		}

		public void printDubles2(AVL_1.Node n)
		{
			if (n.key.next == null)
			{
				AddDataForOutputFile(n.key.fio, n.key.login, n.key.gend, n.key.adress);
			}
			else // Если не одна запись
			{
				AVL_1.Key_1 p1 = n.key;
				while (p1 != null)
				{
					AddDataForOutputFile(p1.fio, p1.login, p1.gend, p1.adress);
					p1 = p1.next;
				}
			}
		}

		// Обход дерева справа налево
		public void LKP2(AVL_1.Node n)
		{
			if (n != null)
			{
				if (ModDataPrint == 1) LKP2(n.right);
				else LKP2(n.left);

				printDubles2(n);
				sizeTree++;

				if (ModDataPrint == 1) LKP2(n.left);
				else LKP2(n.right);
			}
		}

		public void SearchForAVL(string Adress, DateTimePicker dateTimeP)
		{
			MyDate myDate = new MyDate(dateTimeP.Value.Day, dateTimeP.Value.Month, dateTimeP.Value.Year);
			OutOthAdress = Adress;
			othData = myDate;

			File.WriteAllText("Output.txt", String.Empty);  // Полностью чистим файл

			OutOthFile = new StreamWriter("Output.txt");    // Открываем его
			form_03.OutOthFile = OutOthFile; // Передаём 2му окну ссылку на этот файл

			LKP2(AvlTree_01.root);  // Через обход дерева обрабатываем все данные

			OutOthFile.Close();     // Закрываем
		}

		//-------------------------------//
		//		   Запись в файл 		 //
		//-------------------------------//

		// Тут я немного переписал уже готовые методы, именно под вывод данных в файл

		public void PrintData1(string fio, string login, string gend, string adress)
		{
			string str = fio + ", " + login + ", " + gend + ", " + adress + ", ";

			OutFile.WriteLine(str);
		}

		public void printDubles1(AVL_1.Node n)
		{
			if (n.key.next == null)
			{
				PrintData1(n.key.fio, n.key.login, n.key.gend, n.key.adress);
			}
			else // Если не одна запись
			{
				AVL_1.Key_1 p1 = n.key;
				while (p1 != null)
				{
					PrintData1(p1.fio, p1.login, p1.gend, p1.adress);
					p1 = p1.next;
				}
			}
		}

		// Обход дерева справа налево
		public void LKP1(AVL_1.Node n)
		{			
			if (n != null)
			{
				if (ModDataPrint == 1) LKP1(n.right);
				else LKP1(n.left);

				printDubles1(n);
				sizeTree++;

				if (ModDataPrint == 1) LKP1(n.left);
				else LKP1(n.right);
			}
		}

		StreamWriter OutFile;

		void SaveOutFile()
		{
			if (comboMainWay != "Empty.txt")
			{
				File.WriteAllText(comboMainWay, String.Empty);  // Полностью чистим файл

				OutFile = new StreamWriter(comboMainWay);       // Открываем его

				LKP1(AvlTree_01.root);  // Через обход дерева выводим все данные (построчно)

				OutFile.Close();    // Закрываем
			}
			else 
			{
				string s4 = "Открыт пустой файл, мы не можем его сохранить.";
				string s5 = "Для возможности сохранения файла, откройте другой файл";

				MessageBox.Show(s4 + " \n" + s5, "Ошибка!");
			}
        }

        //-------------------------------//
        //	    Считывание из файла		 //
        //-------------------------------//

        string[] InputFileStr; // Массив строк, в который записывается весь входной файл, при чтении 

		// Загружает данные из входного текстового файла в программу

		public void DataLoad()
		{
			if (File.Exists(comboMainWay) == false) comboMainWay = "Empty.txt";
			string fileName_Way = comboMainWay;
			AvlTree_01.MainAllDel();    // Удаляю все значения из дерева
			hashTable_2.DelAllHT();
			AllHide();					// Чищу окно полностью
			InputFileStr = File.ReadAllLines(fileName_Way);

			FullingInsideMassFromInputFile();
		}

		// Что делает: Заполняет внутренние массивы программы данными из входного файла
		// Когда и как используется: Используется только процедурой DataLoad()
		void FullingInsideMassFromInputFile()
		{
			int InpSize = InputFileStr.Length; // Получаю размер (кол-во строк входного файла)

			if (InpSize > 0)
			{

				string ArrLab_FIO_02;
				string ArrLab_Login_02;
				string ArrLab_Gend_02;
				string ArrLab_Adress_02;

				string str;
				char ch;
				int j;

				string inp;

				// В цикле посимвольно обрабатываю строки
				for (int i = 0; i < InpSize; i++)
				{
					inp = InputFileStr[i];

					str = "";
					ch = inp[0];
					j = 0;

					//inp = nputFileStr[i];

					// Иду посимвольно, считываю и записываю символы, пока не встречу запятую
					while (ch != ',')
					{
						str = str + ch;
						j++;
						ch = inp[j];
						//if (ch == ',') break;					
					}

					// Записываю значение в нужный массив
					ArrLab_FIO_02 = str;
					//label1.Text = "ФИО = " + str3;

					str = "";
					j += 2;
					ch = inp[j];

					// И т.д. - заполняю все массивы
					while (ch != ',')
					{
						str = str + ch;
						j++;
						ch = inp[j];
					}

					ArrLab_Login_02 = str;
					//label1.Text = "Логин = _" + str3 + "_ j = " + j.ToString();

					str = "";
					j += 2;
					ch = inp[j];

					while (ch != ',')
					{
						str = str + ch;
						j++;
						ch = inp[j];
					}

					ArrLab_Gend_02 = str;

					str = "";
					j += 2;
					ch = inp[j];

					while (ch != ',')
					{
						str = str + ch;
						j++;
						ch = inp[j];
					}

					ArrLab_Adress_02 = str;
					//label1.Text = "Адрес = _" + str3 + "_ j = " + j.ToString();

					AvlTree_01.MainAdd(ArrLab_FIO_02, ArrLab_Login_02, ArrLab_Gend_02, ArrLab_Adress_02);
					hashTable_2.MainAdd(ArrLab_FIO_02, ArrLab_Login_02, ArrLab_Gend_02, ArrLab_Adress_02);
				}

			}

			AvlTree_01.DebugPrint();
			hashTable_2.DebugPrint();

			// Вызываю основной метод отрисовки таблицы в окне
			MainPrintData();
		}		

		//-------------------------------//
		//		  Удаление записи		 //
		//-------------------------------//

		AVL_1.Node FoundNode;   // Переменная для хранения найденной поиском вершины. Нужна для удаления

		// Нажатие на кнопку удаления записи
		void DelElement(System.Object sender, System.EventArgs e)
		{
			string Name = ((Button)sender).Name;
			string loginDeleted = "";
			int numDel = GetNumOfName(Name);

			MyConsole.INSn("Удаляем элемент [" + FoundNode.key.fio + ", " + FoundNode.key.login + ", " +
			FoundNode.key.gend + ", " + FoundNode.key.adress + ", " + "]");

			if (FoundNode != null)
			{				
				if (FoundNode.key.next == null)
				{
					loginDeleted = FoundNode.key.login;
					AvlTree_01.MainDel(FoundNode.key.adress);
				}
				else // Если дубль
				{
					AVL_1.Key_1 p1 = FoundNode.key;

					AVL_1.Key_1 p1_prev = null;

					for (int i = 0; i < numDel; i++) 
					{
						p1_prev = p1;
						p1 = p1.next;
					}

					// Немного сложного кода, для корректного удаления из цепочки:
					if (p1.next == null) // Если элемент последний в цепочке 
					{
						loginDeleted = p1.login;
						AvlTree_01.MainDel(FoundNode.key.adress);						
					}
					else if (p1_prev == null) // Если первый
					{
						loginDeleted = p1.login;
						FoundNode.key = p1.next;
					}
					else if (p1.next != null) // Если посередине
					{
						loginDeleted = p1.login;
						p1_prev.next = p1.next;
						p1 = null;
					}			
				}

				if (loginDeleted != "")
				{
					MyConsole.INSn("##### - Удаляем из ХТ посты пользователя с логином " + loginDeleted);
					hashTable_2.MainDelete(loginDeleted);
					form_03.DelAllPostsForDeletedUser_LoginOnly(loginDeleted);
				}				
			}

			// И если поиск не активен, то отрисовываю таблицу как обычно
			if (isSearthModEnable == false)
			{
				MainPrintData();
			}
			else // А если поиск активен, то запускаю метод отрисовки значений поиском
			{
				StartSearthMyMetod();
			}

			AvlTree_01.DebugPrint();
			hashTable_2.DebugPrint();
		}

		//------------------------------//
		//		   	  Поиск				//
		//------------------------------//

		bool isSearthModEnable = false; // Переменная, которая показывет, активен ли режим поиска сейчас
										// Это нужно для корректного выведения результатов поиска

		// Обработка первого нажатия на кнопку поиска
		void OnInitTextBoxOmSearth()
		{
			// Скрывает другие кнопки, показывает нужные, а также поле для ввода данных поиска
			textBox6.Show();
			textBox6.Text = "";

			InsertNew.Hide();
			Search1.Hide();
			label14.Show();
			button1.Hide();

			StartSearch1.Show();
			CloseSearch1.Show();

			isSearthModEnable = true;
		}

		// Поиск в дереве, по ключу
		void SearchFoundDataShow(string Adress)
		{
			FoundNode = AvlTree_01.MainSearch(Adress); 
			// Записывае результат в отдельную переменную
			// Это понадобится, для удаления записи

			AllHide();

			if (FoundNode == null)
			{
				label2.Show();
				label2.Text = "Поиск не дал результатов";
				label2.BringToFront();
			}
			else 
			{				
				printDubles(FoundNode); // Выводим запись. Если есть дубли, выводим и их тоже
			}
		}

		// Проверки, перед запуском поиска
		void StartSearthMyMetod()
		{
			// Думаю, тут всё предельно ясно
			isSearthModEnable = true;
			label2.Hide();

			string str_A = textBox6.Text;

			if ((str_A != ""))
			{
				//MyConsole.INSn("Поиск может быть запущен");
				SearchFoundDataShow(str_A);
				MyConsole.INSn("Запускаем поиск. Ищем [" + str_A + "]");
			}
			else
			{
				string s4 = "Мы не можем найти пустую запись";
				string s5 = "Введите в каждое поле поиска как минимум один символ";

				MessageBox.Show(s4 + " \n" + s5, "Ошибка!");
			}
		}

		// Что делает: Скрывает поля поиска
		// Когда и как используется: Вызывается при нажатии на кнопку "-", завершающую поиск
		void CloseTextBoxSearch()
		{
			isSearthModEnable = false;

			// Скрываю и показываю нужные кнопки и поля
			StartSearch1.Hide();
			CloseSearch1.Hide();
			label14.Hide();

			button1.Show();

			textBox6.Hide();
			label2.Hide();

			MyTextBox_02_A.Text = "";

			InsertNew.Show();
			Search1.Show();

			MainPrintData();
		}

		//-------------------------------//
		//	  Добавление новой записи	 //
		//-------------------------------//

		// Обрабатывает нажатие на кнопку создания новой записи (+)
		void My_Func2_EditRecord()
		{
			this.ButtExitEdit_02.Click += new System.EventHandler(this.CloseTextBoxNewElement);

			panel1.Show();

			textBox1.Show();
			textBox2.Show();
			textBox3.Show();
			comboBox1.Show();

			ButtEnterEdit_02.Show();
			ButtExitEdit_02.Show();

			InsertNew.Hide();
			Search1.Hide();
		}

		// Добавление элемента через дерево
		void EnteredNewElement(string str_F, string str_L, string str_A, string str_Gend)
		{
			AvlTree_01.MainAdd(str_F, str_L, str_Gend, str_A);
			hashTable_2.MainAdd(str_F, str_L, str_Gend, str_A);
			MyConsole.INSn("Добавили в дерево элемент [" + str_F + ", " + str_L + ", " +
				str_Gend + ", " + str_A + ", " + "]");
			MainPrintData();
			AvlTree_01.DebugPrint();
			hashTable_2.DebugPrint();
		}

		// Проверки корректности данных, перед добавлением записи
		void EnterNewElement(System.Object sender, System.EventArgs e)
		{
			string str_F = MyTextBox_02_F.Text;
			string str_L = MyTextBox_02_L.Text;
			string str_A = MyTextBox_02_A.Text;
			string str_Gend = MyComboBox_02_Gend.Text;

			// Имя - 30
			// Логин - 20
			// Адрес - 25
			// Пол - 3
			// 123456789_

			if ((str_F != "") && (str_L != "") && (str_A != "") && (str_Gend != ""))
			{
				if (str_F.Length > 30)
				{
					string s1 = "Длинна ФИО превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else if (str_L.Length > 20)
				{
					string s1 = "Длинна логина превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else if (str_A.Length > 25)
				{
					string s1 = "Длинна адреса превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else if (str_Gend.Length > 3)
				{
					string s1 = "Длинна пола превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else
				{
					// Если такого элемента нет в ХТ - тогда добавляем
					if (hashTable_2.MainFindHash(str_L) == -1) //(AvlTree_01.SearchLogin(str_L) == false)
					{
						EnteredNewElement(str_F, str_L, str_A, str_Gend);
					}
					else
					{
						string s4 = "Пользователь с таким логином уже существует";
						string s5 = "Мы не можем создать такую запись";

						MessageBox.Show(s4 + " \n" + s5, "Ошибка!");
					}
				}
			}
			else
			{
				string s4 = "Мы не можем создать пустую запись";
				string s5 = "Введите в каждое поле как минимум один символ";

				MessageBox.Show(s4 + " \n" + s5, "Ошибка!");
			}
		}

		// Скрываю поле создания нового элемента
		void CloseTextBoxNewElement(System.Object sender, System.EventArgs e)
		{
			InsertNew.Show();
			Search1.Show();

			panel1.Hide();

			MyTextBox_02_F.Text = "";
			MyTextBox_02_L.Text = "";
			MyTextBox_02_A.Text = "";
			MyComboBox_02_Gend.Text = "";

			ButtExitEdit_02.Hide();
			ButtEnterEdit_02.Hide();
		}

        //------------------------------//
        //		    Мои методы			//
        //------------------------------//

        // Получаю номер объекта по его цифрам на конце его имени
        int GetNumOfName(string s)
        {
			// Вот и пригодились мои навыки по созданию игр
			// А в частности - итендефикация объектов, по их именам

			// У каждой кнопки на конце имени объекта есть её кодовый номер. Я его получаю

			char a1 = s[s.Length - 1];
			char a2 = s[s.Length - 2];
			char a3 = s[s.Length - 3];
			char a4 = s[s.Length - 4];

            int ret;

			int aa1, aa2, aa3;
			int.TryParse(a1.ToString(), out aa1);
			int.TryParse(a2.ToString(), out aa2);
			int.TryParse(a3.ToString(), out aa3);

			if (((a3 >= '0') && (a3 <= '9')) && ((a2 >= '0') && (a2 <= '9')) && ((a1 >= 0) && (a1 <= '9')))
            {
                ret = aa3 * 100 + aa2 * 10 + aa1;
            }
            else if (((a2 >= '0') && (a2 <= '9')) && ((a1 >= 0) && (a1 <= '9')))
            {
                ret = aa2 * 10 + aa1;
            }
            else //((a1 >= 0) && (a1 <= '9'))
            {
                ret = aa1;
            }

            return (ret);
        }

        //-------------------------------//
        //		   Инициализация		 //
        //-------------------------------//

        // Дополнительная стартовая инициализация
        void AllInitial()
		{
            // Инициализирую, и добавляю на форму кнопки добавления новой записи

            ButtExitEdit_02 = new Button();
			ButtExitEdit_02.Location = new System.Drawing.Point(8, 3);
			ButtExitEdit_02.Name = "ButtExitEdit_02";
			ButtExitEdit_02.AutoSize = true;
			ButtExitEdit_02.Size = new System.Drawing.Size(50, 24);
			ButtExitEdit_02.Text = "Закрыть";
			//ButtExitEdit_02.Font = new System.Drawing.Font(ButtExitEdit_02.Font.Name, 12, ButtExitEdit_02.Font.Style, ButtExitEdit_02.Font.Unit);
			ButtExitEdit_02.Hide();
			this.Controls.Add(ButtExitEdit_02);
			this.ButtExitEdit_02.Click += new System.EventHandler(this.CloseTextBoxNewElement);
			//ToolTip t2 = new ToolTip();
			//t2.SetToolTip(ButtExitEdit_02, "+ Добавить новую запись \n-  Не добавлять запись");
			ButtExitEdit_02.BringToFront();

			ButtEnterEdit_02 = new Button();
			ButtEnterEdit_02.Location = new System.Drawing.Point(8, 34);
			ButtEnterEdit_02.Name = "ButtEnterEdit_02";
			ButtEnterEdit_02.AutoSize = true;
			ButtEnterEdit_02.Size = new System.Drawing.Size(50, 24);
			ButtEnterEdit_02.Text = "Добавить";
			//ButtEnterEdit_02.Font = new System.Drawing.Font(ButtEnterEdit_02.Font.Name, 12, ButtEnterEdit_02.Font.Style, ButtEnterEdit_02.Font.Unit);
			ButtEnterEdit_02.Hide();
			this.Controls.Add(ButtEnterEdit_02);
			this.ButtEnterEdit_02.Click += new System.EventHandler(this.EnterNewElement);
			//ToolTip t1 = new ToolTip();
			//t1.SetToolTip(ButtEnterEdit_02, "+ Добавить новую запись \n-  Не добавлять запись");
			ButtEnterEdit_02.BringToFront();

			// -----

			// Добавляю всплывающие подсказки к 3м кнопкам сверху окна
			if (true)
			{
				ToolTip t1 = new ToolTip();
				t1.SetToolTip(InsertNew, "Добавить новую запись");

				ToolTip t2 = new ToolTip();
				t2.SetToolTip(CloseSearch1, "Закрыть поиск");

				ToolTip t3 = new ToolTip();
				t3.SetToolTip(Search1, "Запустить поиск");
			}
		}

		// Скрывает все записи на форме
		void AllHide()
		{
			if (myRoot != null)
			{
				MyList p = myRoot;
				i_Data = 0; // Для отслеживания высоты таблицы

				while (p.next != null)
				{
					p.Panel1.Hide();
					p = p.next;
				}
				p.Panel1.Hide();
			}
		}

		MyList myRoot; // Голова
		MyList myListNext; // Хвост

		public int sizeTree;

		// Вывожу все данные из дерева, в форму
		public void MainPrintData()
		{
			AllHide(); // Сначала скрываю, всё что есть на форме
			sizeTree = 0;

			if (AvlTree_01.root != null)
			{
				label10.Hide();
                //MyConsole.INSn("-----------");
                //AvlTree_01.MainPrint(2);
                LKP(AvlTree_01.root); // Проходим cнизу вверх, по дереву
			}
			else
			{
				label10.Show();
				label10.Text = "Таблица пуста";
				label10.BringToFront();
			}

			if (sizeTree > 32) 
			{
				string s1 = "Размер статической ХТ не может превышать" + HashTable.SizeHeshTable + ". Программа будет аварйино остановлена";
				MessageBox.Show(s1, "Фатальная ошибка!");
				Application.Exit();
			} 				
		}

		// Выводит и добавляет на форму кнопки удаления записей, при успешном поиске
		public void VisibleButtonDeleteElements(MyList myList)
		{
			Button myButton = new Button();
			int i = i_Data;

			myButton.Name = "NewButton_" + i.ToString();
			myButton.AutoSize = true;
			myButton.Size = new System.Drawing.Size(35, 24);
			myButton.Text = "Удалить";			

			//this.Controls.Add(myButton);

			myList.Panel1.Controls.Add(myButton);

			//myButton.Location = new System.Drawing.Point(600, StartWh + i + 3);
			myButton.Location = new System.Drawing.Point(600, i + 3);

			myButton.BringToFront();

			// Переопределяю для каждой созданной мной кнопки действие нажатия на неё, на единственный метод DelElement
			myButton.Click += new System.EventHandler(this.DelElement);
		}

		// Работает при выводе с дублями
		public void printDubles(AVL_1.Node n)
		{
			if (n.key.next == null)
			{
				PrintData(n.key.fio, n.key.login, n.key.gend, n.key.adress);
			}
			else // Если не одна запись
			{
				AVL_1.Key_1 p1 = n.key;
				while (p1 != null)
				{
					PrintData(p1.fio, p1.login, p1.gend, p1.adress);
					p1 = p1.next;
				}
			}
		}

		// Обход дерева справа налево
		public void LKP(AVL_1.Node n)
		{
			if (n != null)
			{
				if(ModDataPrint == 1) LKP(n.right);
				else LKP(n.left);

				printDubles(n);
				sizeTree++;

				if (ModDataPrint == 1) LKP(n.left);
				else LKP(n.right);
			}
		}

		int i_Data;

		// Храню все записи в односвязном списке
		// Когда нужно - прохожу его, и скрываю все записи, что бы вывести новые
		public partial class MyList
		{
			// Очень простой вложенный класс
			public MyList next;

			public Panel Panel1;
			public Label Label1;
			public Label Label2;
			public Label Label3;
			public Label Label4;

			public Button buttDel;

			public MyList() { }

			// У которого очень удобный внутренний конструктор
			public MyList(string fio, string login, string gend, string adress, int ii)
			{
				int currentSize = 10;

				Panel1 = new Panel();
				Label1 = new Label();
				Label2 = new Label();
				Label3 = new Label();
				Label4 = new Label();

				Label1.Text = fio;
				Label2.Text = login;
				Label3.Text = gend;
				Label4.Text = adress;

				Label1.Font = new System.Drawing.Font(Label1.Font.FontFamily, currentSize, Label1.Font.Style, Label1.Font.Unit);
				Label2.Font = new System.Drawing.Font(Label2.Font.FontFamily, currentSize, Label2.Font.Style, Label2.Font.Unit);
				Label3.Font = new System.Drawing.Font(Label3.Font.FontFamily, currentSize, Label3.Font.Style, Label3.Font.Unit);
				Label4.Font = new System.Drawing.Font(Label4.Font.FontFamily, currentSize, Label4.Font.Style, Label4.Font.Unit);

				Label1.Size = new System.Drawing.Size(250, 20);
				Label2.Size = new System.Drawing.Size(150, 20);
				Label3.Size = new System.Drawing.Size(30, 20);
				Label4.Size = new System.Drawing.Size(200, 20);

				Label1.BringToFront();
				Label2.BringToFront();
				Label3.BringToFront();
				Label4.BringToFront();

				Label1.Location = new System.Drawing.Point(3, 10);
				Label2.Location = new System.Drawing.Point(260, 10);
				Label3.Location = new System.Drawing.Point(440, 10);
				Label4.Location = new System.Drawing.Point(480, 10);

				//ArrBut_01[i].Location = new System.Drawing.Point(700, 5);
				Panel1.Location = new System.Drawing.Point(1, ii);
				Panel1.Size = new System.Drawing.Size(730, 33);

				Panel1.Controls.Add(Label1);
				Panel1.Controls.Add(Label2);
				Panel1.Controls.Add(Label3);
				Panel1.Controls.Add(Label4);

				Panel1.Show();

				next = null;
			}

			// Когда нужно получить панель, для вывода, я использую этот метод
			public Panel RetPanel()
			{
				return Panel1;
			}
		};

		// Вывожу данные на форму
		void PrintData(string fio, string login, string gend, string adress)
		{
			int ii = StartWh + i_Data * 30; // Шаг

			MyList myList = new MyList(fio, login, gend, adress, ii);

			Panel Panel1 = myList.RetPanel();

			Panel1.Show();

			this.Controls.Add(Panel1);

			if (myRoot == null)
			{
				myRoot = myList;
				myListNext = myRoot;
			}
			else
            {
				myListNext.next = myList;
				myListNext = myList;
			}

			if (isSearthModEnable == true)
			{
				VisibleButtonDeleteElements(myList);
			}

			i_Data++;
		}

		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//				      Внутреннее методы обработки действий:						//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		// Таймер
		private void timer1_Tick(object sender, EventArgs e)
		{
			// Таймер есть, но он выключен
		}

		// Обработка нажатия на кнопку создания новой записи (+)
		private void InsertNew_Click(object sender, EventArgs e)
		{
			My_Func2_EditRecord();
		}

		// Обработка нажатия на кнопку поиска (?)
		private void Settings_Click(object sender, EventArgs e)
		{
			// Обработка нажатия на кнопку поиска
			OnInitTextBoxOmSearth();
		}

		// Обработка нажатия на вторую кнопку поиска (?_2)
		private void StartSearch1_Click(object sender, EventArgs e)
		{
			// Обработка нажатия на вторую кнопку поиска
			StartSearthMyMetod();
		}

		// Обработка нажатия на кнопку закрытия поиска
		private void CloseSearch1_Click(object sender, EventArgs e)
		{
			// Обработка нажатия на кнопку закрытия поиска
			CloseTextBoxSearch();
		}

		// Обработка нажатия на кнопку "Выбрать файл"
		private void button4_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			AvlTree_01.DebugPrint();
			hashTable_2.DebugPrint();
		}

		// Открываем, или обновляем окно дебага, если оно уже открыто
		private void button10_Click(object sender, EventArgs e)
		{
			hashTable_2.DebugPrint();
			DebuggingWindow_4.WindowState = FormWindowState.Normal;
			DebuggingWindow_4.Show();

			AvlTree_01.DebugPrint();
			DebuggingWindow_1.WindowState = FormWindowState.Normal;
			DebuggingWindow_1.Show();

			MyConsole.INSn("Открыли/обновили окно дебага");
		}

		// Когда пользователь выбрал файл, и нажал ОК
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
			comboMainWay = openFileDialog1.FileName;
			if (isSearthModEnable) CloseTextBoxSearch();
			DataLoad();
			form_03.DataLoad();
		}

		int ModDataPrint = 0;

        private void button1_Click(object sender, EventArgs e)
        {
			if (ModDataPrint == 1)
			{
				ModDataPrint = 0;
				button1.Text = "a~z";
			}
			else
			{
				ModDataPrint = 1;
				button1.Text = "z~a";
			}

			if(isSearthModEnable == false) MainPrintData();
		}

		// При закрытии этой формы
        private void Form_02_FormClosed(object sender, FormClosedEventArgs e)
        {
			DebuggingWindow_1.WindowState = FormWindowState.Minimized;
			DebuggingWindow_4.WindowState = FormWindowState.Minimized;
		}

		// При нажатии на кнопку "Сохранить"
        private void button2_Click(object sender, EventArgs e)
        {
			SaveOutFile();
		}
    }
}
