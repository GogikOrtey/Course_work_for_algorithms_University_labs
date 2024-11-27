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

namespace Coursework_07
{
	public partial class Form_03 : Form
	{
		public Form_03()
		{
			InitializeComponent();
			MyInitializations();
		}
		void MyInitializations()
		{
			this.BackColor = System.Drawing.SystemColors.Control;

			panel1.Hide();                  // Это панель с 4мя полями, для ввода данных

			MyTextBox_02_F = dateTimePicker1;      // Сразу переназначаю текст боксы из этих полей, на нужные мне (для удобства)
			MyTextBox_02_L = textBox2;
			MyTextBox_02_A = textBox3;

			StartSearch1.Hide();            // Это кнопки, которые появляются при старте поиска. Изначально они скрыты
			CloseSearch1.Hide();

			label2.Hide();                  // Это полупрозрачные тексты про неудавшийся поиск, и что элементов в таблице не осталось
			label10.Hide();
			dateTimePicker2.Hide();
			dateTimePicker1.Hide();
			textBox4.Hide();
			label14.Hide();

			MyHashTable = new HashTable();		// Создаю новый экземпляр Хеш-таблицы
			AvlTree_01 = Form_02.AvlTree_01;    // Беру ссылку на АВЛ-дерево, из 1го окна
			AvlTree_02 = new AVL_2();

			// Файл, который по умолчанию открывается
			comboMainWay = "D:/Рабочий стол/Учёба 3й курс 2022/Учёба 2022 2й курс/Структуры данных/0_Курсовая/InputFiles/MainData.gog2";

			//MyConsole myConsole = new MyConsole();
			//myConsole.Show();

			// %USERPROFILE%/Рабочий стол
			// %USERPROFILE%/Desktop
			// D:/Рабочий стол/_Учёба 2022/Структуры данных/0_Курсовая/InputFiles/

			openFileDialog1.InitialDirectory = "%USERPROFILE%/Рабочий стол/Учёба 3й курс 2022/Учёба 2022 2й курс/Структуры данных/0_Курсовая/InputFiles/";
			openFileDialog1.Filter = "gog files (*.gog2)|*.gog2";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.FileName = "TextFile_3 _2.gog2";

			counterForStartInit = 50;

			// Этот блок кода выполняется внизу, в таймере
			{
				//DataLoad();
				//MainPrintData();
				//AllInitial();

				//DebuggingWindow_2 = new DebuggingWindow_2();
				//MyHashTable.DebugPrint();    // Debug - отдельное окошко с белым фоном. Открывается по нажатию кнопки "Debug"
			}

			//MyConsole.INSn("Текст");  // MyConsole - это моя реализация стандартной консоли. Туда можно отправить любую строку
		}

		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//					  Объявление моих локальных переменных:						//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		public HashTable MyHashTable;
		public AVL_1 AvlTree_01;
		public AVL_2 AvlTree_02;

		public string comboMainWay;			// Путь + имя файла

		DebuggingWindow_2 DebuggingWindow_2;
		DebuggingWindow_3 DebuggingWindow_3;

		DateTimePicker MyTextBox_02_F;			// Дополнительные текст боксы, для введения данных (4 текстовых поля сверху окна)
		TextBox MyTextBox_02_L;
		TextBox MyTextBox_02_A;

		Button ButtEnterEdit_02;        // Кнопки, которые появляются при старте поиска
		Button ButtExitEdit_02;

		int StartWh = 90;               // С какой высоты от верхней границы окна мы начинаем выодить элементы таблицы

		int counterForStartInit = 0;


		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//								Локльные методы:								//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		// Из формата MyDate преобразую данные в строку
		MyDate StringToMyDate(DateTime str)
		{
			int d = str.Day;
			int m = str.Month;
			int g = str.Year;

			MyDate myDate = new MyDate(d, m, g);
			return myDate;
		}

		//-------------------------------//
		//			АВЛ_2 из ХТ	 		 //
		//-------------------------------//

		public void FormAVL()
		{
			// Чищу АВЛ
			AvlTree_02.MainAllDel();

			// Прямо прохожу ХТ, и всё добавляю в АВЛ
			for (int i = 0; i < HashTable.SizeHeshTable; i++)
			{
				if (MyHashTable.Empty[i] == 1)
				{
					AvlTree_02.MainAdd(MyHashTable.DataHT[i].Date, MyHashTable.DataHT[i].Text, MyHashTable.DataHT[i].Image, MyHashTable.DataHT[i].Login);
				}
			}

			AvlTree_02.DebugPrint(); // Обновляю окошко дебага
			//DebuggingWindow_3.WindowState = FormWindowState.Normal;
			//DebuggingWindow_3.Show();
		}

		//-------------------------------//
		//		Отчёт из 2х таблиц 		 //
		//-------------------------------//

		public StreamWriter OutOthFile; // Беру из 1го окна
		string otLogin; // Ключ логина из 1го окна
		MyDate othData;

		public void AddDataForOutputFile(MyDate Date, string Text, string Image, string Login)
		{
			if (Login == otLogin) // Если в нашем дереве мы нашли запись с логином, полученным из 1го окна
			{
				if (Date == othData) // Если в этой записи дата совпадает с указанной в окне отчёта
				{
					string str = Date.RetStringForPrint() + ", " + Text + ", " + Image + ", " + Login + ", ";
					OutOthFile.WriteLine(str); // Тогда мы мохраняем эту запись в файл
				}
			}
		}

		public void printDubles2(AVL_2.Node n)
		{
			if (n.key.next == null)
			{
				AddDataForOutputFile(n.key.Date, n.key.Text, n.key.Image, n.key.Login);
			}
			else // Если не одна запись
			{
				AVL_2.Key_1 p1 = n.key;
				while (p1 != null)
				{
					AddDataForOutputFile(p1.Date, p1.Text, p1.Image, p1.Login);
					p1 = p1.next;
				}
			}
		}

		// Обход дерева справа налево
		public void LKP2(AVL_2.Node n)
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

		public void SearchForAVL2(string Login, MyDate myDate)
		{
			otLogin = Login;
			othData = myDate;

			// Поиск в нашей новой АВЛ, по логину
			LKP2(AvlTree_02.root);
		}

		//-------------------------------//
		//		   Запись в файл 		 //
		//-------------------------------//		

		void SaveOutFile()
		{
			if (comboMainWay != "Empty.txt")
			{
				File.WriteAllText(comboMainWay, String.Empty); // Полностью чистим файл

				StreamWriter OutFile = new StreamWriter(comboMainWay);

				AQ Res;

				for (int i = 0; i < HashTable.SizeHeshTable; i++)
				{
					Res = MyHashTable.DataHT[i];

					int indHT = MyHashTable.MainFindHash(Res.Date.RetStringForPrint(), Res.Login);
					if (indHT != -1)
					{
						if (MyHashTable.Empty[indHT] == 1)
						{
							string str = Res.Date.RetStringForPrint() + ", " + Res.Text + ", " + Res.Image + ", " + Res.Login + ", ";

							OutFile.WriteLine(str);
						}
					}
				}

				OutFile.Close();
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
			MyHashTable.DelAllHT();  // Удаляю все значения из дерева
			AvlTree_02.MainAllDel();
			AllHide();                  // Чищу таблицу полностью
			InputFileStr = File.ReadAllLines(fileName_Way);

            if (InputFileStr.Length > HashTable.SizeHeshTable)
            {
                string s1 = "Размер статической ХТ не может превышать" + HashTable.SizeHeshTable + ". Программа будет аварйино остановлена";
                MessageBox.Show(s1, "Фатальная ошибка!");
                Application.Exit();
            }

            FullingInsideMassFromInputFile();
		}

		bool inNotAllRecordInsert; // == true, если при считывании данных из файла, были добавлены не все публикации (из-за отсутсвия пользователей)

		// Что делает: Заполняет внутренние массивы программы данными из входного файла
		// Когда и как используется: Используется только процедурой DataLoad()
		void FullingInsideMassFromInputFile()
		{
			inNotAllRecordInsert = false;

			int InpSize = InputFileStr.Length; // Получаю размер (кол-во строк входного файла)

			MyDate ArrLab_Date_02;
			string d, m, g;
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

				d = "";
				ch = inp[0];
				j = 0;

				// Иду посимвольно, считываю и записываю символы, пока не встречу запятую
				while (ch != '.')
				{
					d = d + ch;
					j++;
					ch = inp[j];
				}

				m = "";
				j += 1;
				ch = inp[j];

				// И т.д. - заполняю все массивы
				while (ch != '.')
				{
					m = m + ch;
					j++;
					ch = inp[j];
				}

				g = "";
				j += 1;
				ch = inp[j];

				// И т.д. - заполняю все массивы
				while (ch != ',')
				{
					g = g + ch;
					j++;
					ch = inp[j];
				}

				int dd, mm, gg;

				int.TryParse(d, out dd);
				int.TryParse(m, out mm);
				int.TryParse(g, out gg);

				ArrLab_Date_02 = new MyDate(dd, mm, gg);

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

                // Добавляем записи, только к существующим пользователям
                if (AvlTree_01.SearchLogin(ArrLab_Adress_02) == true) //if (HT.MainFindHash(ArrLab_Adress_02) != -1) //////// !!!!! !!!!! !!!!! !!!!!! !!!!!!
                {
                    MyHashTable.MainAdd(ArrLab_Date_02, ArrLab_Login_02, ArrLab_Gend_02, ArrLab_Adress_02);
					FormAVL(); //AvlTree_02.MainAdd(ArrLab_Date_02, ArrLab_Login_02, ArrLab_Gend_02, ArrLab_Adress_02);

				}
                else inNotAllRecordInsert = true;

                //MyHashTable.MainAdd(ArrLab_Date_02, ArrLab_Login_02, ArrLab_Gend_02, ArrLab_Adress_02);
            }

			if (inNotAllRecordInsert == true)
			{
				string s4 = "Нам удалось загрузить в программу из входного файла не все записи";
				string s5 = "Так как некоторых пользователей не было обнаружено";

				MessageBox.Show(s4 + " \n" + s5, "Внимание!");
			}

			MyHashTable.DebugPrint();
			AvlTree_02.DebugPrint();

			// Вызываю основной метод отрисовки таблицы в окне
			MainPrintData();
		}


		//-------------------------------//
		//		  Удаление записи		 //
		//-------------------------------//

		AQ FoundNode;   // Переменная для хранения найденной поиском вершины. Нужна для удаления

		// Нажатие на кнопку удаления записи
		void DelElement(System.Object sender, System.EventArgs e)
		{
			MyConsole.INSn("Удаляем элемент [" + FoundNode.Date.RetStringForPrint() + ", " + FoundNode.Text + ", " +
			FoundNode.Image + ", " + FoundNode.Login + ", " + "]");

			if (FoundNode != null)
			{
				MyHashTable.MainDelete(FoundNode.Date, FoundNode.Login);
				FormAVL(); //AvlTree_02.MainDel(FoundNode.Login);
			}

			// Если поиск не активен, то отрисовываю таблицу как обычно
			if (isSearthModEnable == false)
			{
				MainPrintData();
			}
			else // А если поиск активен, то запускаю метод отрисовки значений поиском
			{
				StartSearthMyMetod();
			}

			MyHashTable.DebugPrint();
			AvlTree_02.DebugPrint();
		}

		// Удаление всех постов пользователя, при его удалении в другом окне
		public void DelAllPostsForDeletedUser_LoginOnly(string Login) 
		{
			MyHashTable.MainDeleteForLogin(Login);
			FormAVL();
			MainPrintData();
			MyHashTable.DebugPrint();
		}

		//------------------------------//
		//		   	  Поиск				//
		//------------------------------//

		bool isSearthModEnable = false; // Переменная, которая показывет, активен ли режим поиска сейчас
										// Это нужно для корректного выведения результатов поиска

		// Обработка первого нажатия на кнопку поиска
		void OnInitTextBoxOmSearth()
		{
			// Скрывает другие кнопки, показывает нужные, а так-же поле для ввода данных поиска
			dateTimePicker1.Show();
			textBox4.Show();

			InsertNew.Hide();
			Search1.Hide();
			label14.Show();

			StartSearch1.Show();
			CloseSearch1.Show();

			isSearthModEnable = true;
		}

		// Поиск, по ключу
		void SearchFoundDataShow(MyDate myDate, string login)
		{
			FoundNode = MyHashTable.MainFind(myDate, login);
			// Записывае результат в отдельную переменную
			// Это понадобится, для удаления записи

			AllHide();

			if (FoundNode.Login == "")
			{
				label2.Show();
				label2.Text = "Поиск не дал результатов";
				label2.BringToFront();
			}
			else
			{
				// Выводим запись. Если есть дубли, выводим и их тоже
				PrintData(FoundNode.Date, FoundNode.Text, FoundNode.Image, FoundNode.Login);
			}
		}

		// Проверки, перед запуском поиска
		void StartSearthMyMetod()
		{
			isSearthModEnable = true;
			label2.Hide();

			MyDate str_A = new MyDate(dateTimePicker1.Value.Day, dateTimePicker1.Value.Month, dateTimePicker1.Value.Year);
			string nLogin = textBox4.Text;

			if (nLogin != "")
			{
				SearchFoundDataShow(str_A, nLogin);
				MyConsole.INSn("Запускаем поиск. Ищем [" + str_A + "]");
			}
			else 
			{
				string s1 = "В поле Логин ничего не введено. Мы не можем найти запись, по пустому ключу";
				MessageBox.Show(s1, "Ошибка!");
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

			dateTimePicker1.Hide();
			textBox4.Hide();
			label2.Hide();

			MyTextBox_02_A.Text = "";
			//textBox4.Text = "";

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

			dateTimePicker2.Show();
			textBox2.Show();
			textBox3.Show();
			textBox1.Show();

			ButtEnterEdit_02.Show();
			ButtExitEdit_02.Show();

			InsertNew.Hide();
			Search1.Hide();
		}

		// Добавление элемента через ХТ
		void EnteredNewElement(MyDate Date, string Text, string Image, string Login)
		{
			MyHashTable.MainAdd(Date, Text, Image, Login);
			FormAVL(); //AvlTree_02.MainAdd(Date, Text, Image, Login);
			MyConsole.INSn("Добавили в ХТ элемент [" + Date.RetStringForPrint() + ", " + Text + ", " +
				Image + ", " + Login + ", " + "]");
			MainPrintData();
			MyHashTable.DebugPrint();
			AvlTree_02.DebugPrint();
		}

		// Проверки корректности данных, перед добавлением записи
		void EnterNewElement(System.Object sender, System.EventArgs e)
		{
			MyDate str_Date = StringToMyDate(dateTimePicker2.Value);

			string str_Text = textBox2.Text;    //MyTextBox_02_F.Text;
			string str_Image = textBox1.Text;   //MyTextBox_02_L.Text;
			string str_Login = textBox3.Text;   //MyTextBox_02_A.Text;			

			// Текст - 20
			// Image - 12
			// Логин - 20

			// 123456789_

			if ((str_Text != "") && (str_Image != "") && (str_Login != ""))
			{
				if (str_Text.Length > 20)
				{
					string s1 = "Длинна текста записи превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else if (str_Image.Length > 12)
				{
					string s1 = "Длинна имнени изображения превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");

				}
				else if (str_Login.Length > 20)
				{
					string s1 = "Длинна логина превышает допустимую";
					MessageBox.Show(s1, "Ошибка!");
				}
				else
				{
					// Если существует пользователь, с таким логином
					if (AvlTree_01.SearchLogin(str_Login) == true)
					{
						int hash = MyHashTable.MainFindHash(str_Date.RetStringForPrint(), str_Login);

						if (hash == -1)
						{
							// Если записи, с такой датой нет в нашей ХТ
							EnteredNewElement(str_Date, str_Text, str_Image, str_Login);
						}
						else
						{
							if ((MyHashTable.DataHT[hash].Date == str_Date) && (MyHashTable.DataHT[hash].Login == str_Login))
							{
								string s4 = "Публикация, с такой датой и логином уже есть нашей программе";
								string s5 = "Мы не можем создать ещё одну запись, с такой-же датой и логином";

								MessageBox.Show(s4 + " \n" + s5, "Ошибка!");
							}
							else
							{
								// Если есть, но она например, удалённая
								EnteredNewElement(str_Date, str_Text, str_Image, str_Login);
							}
						}
					}
					else
					{
						string s4 = "Пользователя с таким логином не существует";
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
			textBox1.Text = "";
			dateTimePicker2.Hide();

			ButtExitEdit_02.Hide();
			ButtEnterEdit_02.Hide();
		}

		//-------------------------------//
		//			   Вывод 			 //
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

			AQ Res;

			for (int i = 0; i < HashTable.SizeHeshTable; i++)
			{
				Res = MyHashTable.DataHT[i];
				PrintData(Res.Date, Res.Text, Res.Image, Res.Login);
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
			myButton.Location = new System.Drawing.Point(650, i + 3);

			myButton.BringToFront();

			// Переопределяю для каждой созданной мной кнопки действие нажатия на неё, на единственный метод DelElement
			myButton.Click += new System.EventHandler(this.DelElement);
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
			public MyList(MyDate Date, string Text, string Image, string Login, int ii)
			{
				int currentSize = 10;

				Panel1 = new Panel();
				Label1 = new Label();
				Label2 = new Label();
				Label3 = new Label();
				Label4 = new Label();

				Label1.Text = Date.RetStringForPrint();
				Label2.Text = Text;
				Label3.Text = Image;
				Label4.Text = Login;

				Label1.Font = new System.Drawing.Font(Label1.Font.FontFamily, currentSize, Label1.Font.Style, Label1.Font.Unit);
				Label2.Font = new System.Drawing.Font(Label2.Font.FontFamily, currentSize, Label2.Font.Style, Label2.Font.Unit);
				Label3.Font = new System.Drawing.Font(Label3.Font.FontFamily, currentSize, Label3.Font.Style, Label3.Font.Unit);
				Label4.Font = new System.Drawing.Font(Label4.Font.FontFamily, currentSize, Label4.Font.Style, Label4.Font.Unit);

				Label1.Size = new System.Drawing.Size(80, 20);
				Label2.Size = new System.Drawing.Size(220, 20);
				Label3.Size = new System.Drawing.Size(120, 20);
				Label4.Size = new System.Drawing.Size(300, 20);

				Label1.BringToFront();
				Label2.BringToFront();
				Label3.BringToFront();
				Label4.BringToFront();

				Label1.Location = new System.Drawing.Point(3, 10);
				Label2.Location = new System.Drawing.Point(124, 10);
				Label3.Location = new System.Drawing.Point(354, 10);
				Label4.Location = new System.Drawing.Point(482, 10);

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
		void PrintData(MyDate Date, string Text, string Image, string Login)
		{
			int indHT = MyHashTable.MainFindHash(Date.RetStringForPrint(), Login);
			if (indHT != -1)
			{
				if (MyHashTable.Empty[indHT] == 1) // Проверяю, что бы данные были "заняты" в этой ячейке
				{
					int ii = StartWh + i_Data * 30; // Шаг

					MyList myList = new MyList(Date, Text, Image, Login, ii);

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
			}
		}

		//////////////////////////////////////////////////////////////////////////////////
		//																				//
		//				      Внутреннее методы обработки действий:						//
		//																				//
		//////////////////////////////////////////////////////////////////////////////////

		// Таймер
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (counterForStartInit>2)
			{
				counterForStartInit--;
			}
			else if (counterForStartInit == 2)
			{
				counterForStartInit = 1;

				DebuggingWindow_2 = new DebuggingWindow_2();
				DebuggingWindow_3 = new DebuggingWindow_3();

				DataLoad();
				MainPrintData();
				AllInitial();

				AvlTree_02.DebugPrint();    // Debug - отдельное окошко с белым фоном. Открывается по нажатию кнопки "Debug"
				MyHashTable.DebugPrint();    // Debug - отдельное окошко с белым фоном. Открывается по нажатию кнопки "Debug"				
			}
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
			AvlTree_02.DebugPrint();
			MyHashTable.DebugPrint();
		}

		// Открываем, или обновляем окно дебага, если оно уже открыто
		private void button10_Click(object sender, EventArgs e)
		{
			AvlTree_02.DebugPrint();
			DebuggingWindow_3.WindowState = FormWindowState.Normal;
			DebuggingWindow_3.Show();

			MyHashTable.DebugPrint();
			DebuggingWindow_2.WindowState = FormWindowState.Normal;
			DebuggingWindow_2.Show();

			MyConsole.INSn("Открыли/обновили окно дебага");
		}

		// Когда пользователь выбрал файл, и нажал ОК
		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			comboMainWay = openFileDialog1.FileName;
			if (isSearthModEnable) CloseTextBoxSearch();
			DataLoad();
		}

		int ModDataPrint = 0;

		// При закрытии этой формы
		private void Form_02_FormClosed(object sender, FormClosedEventArgs e)
		{
			DebuggingWindow_2.WindowState = FormWindowState.Minimized;
			DebuggingWindow_3.WindowState = FormWindowState.Minimized;
			//DebuggingWindow_2.WindowState = FormWindowState.Minimized;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SaveOutFile();
		}
    }
}