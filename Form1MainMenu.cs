using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Клавиатурный_тренажер_KeyboardMaster
{
    public partial class Form1MainMenu : Form
    {
        bool isLeftMouseDown;
        Point startPoint;
        int countMistake = 0; // Кол-во ошибок в тексте при печатании
        int targetIndex;
        string[] arrayText = {"и", "в", "не", "он", "на", "я", "что", "тот", "быть", "с", "а", "весь", "это", "как", "она", "по", "но", "они", "к", "у", "ты", "из", "мы", "за", "вы", "так", "же", "от", "сказать", "этот", "который", "мочь", "человек", "о", "один", "еще", "бы", "такой", "только", "себя", "свое", "какой", "когда", "уже", "для", "вот", "кто", "да", "говорить", "год", "знать", "можно", "статья", "если", "очень", "ну", "вот", "потом", "дело", "жизнь", "первый", "день", "тут", "во", "ничто", "очень", "со", "хотеть", "ли", "при", "голова", "надо", "без", "видеть", "идти", "теперь", "тоже", "стоять", "друг", "дом", "теперь", "можно", "после", "здесь", "думать", "место", "лицо", "друг", "жить", "делать", "через", "общий", "знать", "новый", "два", "видеть", "идти", "один", "под", "где", "потом", "делать", "два", "при", "мой", "идти", "хотеть", "жить", "работа", "рука", "раз", "слово", "солнце", "море", "ветер", "небо", "земля", "свет", "трава", "звезда", "дождь", "огонь", "река", "утро", "вечер", "ночь", "звук", "цвет", "вкус", "запах", "чувство", "радость", "печаль", "надежда", "вера", "любовь", "смерть", "время", "путь", "сила", "мир", "красота", "правда", "ложь", "ум", "душа", "тело", "сердце", "мысль", "давать", "нога", "книга", "писать", "читать", "учить", "студент", "учитель", "класс", "урок", "задание", "ответ", "вопрос", "причина", "следствие", "результат", "процесс", "метод", "способ", "пример", "правило", "исключение", "структура", "элемент", "функция", "задача", "решение", "проблема", "сложность", "легкость", "быстро", "медленно", "громко", "тихо", "высоко", "низко", "далеко", "близко", "внутри", "наружи", "слева", "справа", "вперед", "назад", "вверх", "вниз", "всегда", "никогда", "часто", "редко", "иногда", "обычно", "везде", "начинать", "получать", "делать", "смотреть", "думать", "ждать", "искать", "находить", "создавать", "использовать", "понимать", "чувствовать", "помнить", "забывать", "менять", "оставаться", "появляться", "исчезать", "расти", "падать", "лежать", "стоять", "сидеть", "бежать", "лететь", "плыть", "идти", "ехать", "возвращаться", "приходить", "уходить", "заходить", "выходить", "входить", "лежать", "сидеть", "стоять", "висеть", "лежать", "стоять", "сидеть", "смотреть", "слушать", "говорить", "писать", "читать", "учить", "работать", "отдыхать", "спать", "есть", "пить", "дышать", "жить", "умирать", "рождаться", "расти", "стареть", "выздоравливать", "улыбаться", "плакать", "смеяться", "грустить", "злиться", "радоваться", "удивляться", "бояться", "надеяться", "верить", "любить", "ненавидеть", "просить", "давать", "брать", "отдавать", "принимать", "отправлять", "получать", "делать", "создавать", "использовать", "применять", "строить", "разрушать", "открывать", "закрывать", "начинать", "заканчивать", "продолжать", "останавливаться", "двигаться", "спешить", "литьмед", "торопиться", "опаздывать", "успевать", "готовить", "варить", "жарить", "печь", "резать", "чистить", "мыть", "сушить", "гладить", "стирать", "убирать", "ремонтировать", "чинить"};
        string targetText;

        public Form1MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += Form1MainMenu_Shown;
            this.Activated += Form1MainMenu_Activated;
            this.Resize += Form1MainMenu_Resize;
            this.Size = new Size(1389, 795); // Фиксированный размер формы
        }

        private void Form1MainMenu_Load(object sender, EventArgs e)
        {
            label1NameUserOrAdmin.Text = Class1InfoAboutUserOrAdmin.nameUser;
            generationTargetText();
        }

        private void Form1MainMenu_Click(object sender, EventArgs e)
        {
            guna2Panel1SpeedMenu.Visible = false;
        }

        #region Граница формы

        private void Form1MainMenu_Shown(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовываем форму при открытии
        }

        private void Form1MainMenu_Activated(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовываем форму при активации
        }

        private void Form1MainMenu_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовываем форму при изменении размера
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.Gray, 2)) // Цвет и толщина границы
            {
                e.Graphics.DrawRectangle(borderPen, 2, 2, this.Width - 4, this.Height - 4); // Отступы для границы
            }
        }

        #endregion


        #region Перемещение формы мышью  

        private void panel1ControlForm1_MouseDown(object sender, MouseEventArgs e)
        {
            isLeftMouseDown = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel1ControlForm1_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftMouseDown = false;
        }

        private void panel1ControlForm1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isLeftMouseDown)
            {
                Point deltaPos = new Point(e.X - startPoint.X, e.Y - startPoint.Y);
                this.Location = new Point(this.Location.X + deltaPos.X, this.Location.Y + deltaPos.Y);
            }
        }

        #endregion


        #region Управление формой кнопками 

        private void guna2Button1ExitApll_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Закрытие программы
        }

        private void guna2Button1CollapseApll_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // Сворачивание программы
        }

        #endregion


        #region Печать с клавиатуры, изменение цвета символов и проверка текста  

        private void richTextBox1Typing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (targetIndex < targetText.Length)
            {
                if (e.KeyChar == targetText[targetIndex])
                {
                    richTextBox1Typing.Select(targetIndex + 1, 1);
                    richTextBox1Typing.SelectionColor = Color.White;

                    targetIndex++;
                }
                else
                {
                    richTextBox1Typing.Select(targetIndex, 1);
                    richTextBox1Typing.SelectionBackColor = Color.FromArgb(250, 128, 114);

                    countMistake++;
                }
            }
        }

        #endregion


        #region Метод случайной генерации текста для печати 

        public void generationTargetText()
        {
            targetIndex = 0;
            targetText = null;

            Random random = new Random();
            int countIteration = 0;

            while (countIteration < arrayText.Length)
            {
                int indexSymbol = random.Next(0, arrayText.Length);
                targetText += arrayText[indexSymbol] + " ";

                countIteration++;
            }

            richTextBox1Typing.Text = targetText;

            this.ActiveControl = richTextBox1Typing;
            richTextBox1Typing.Select(targetIndex, 1);
            richTextBox1Typing.SelectionColor = Color.White;
        }

        #endregion


        #region Изменение иконки и имя пользователя     

        private void guna2PictureBox1UserLogo_MouseEnter(object sender, EventArgs e)
        {
            if (Class1InfoAboutUserOrAdmin.nameUser == null)
            {
                guna2PictureBox1UserLogo.Image = Properties.Resources.user_logo_white;
            }
            else if (Class1InfoAboutUserOrAdmin.nameUser != null)
            {
                label1NameUserOrAdmin.ForeColor = Color.White;
                guna2PictureBox1UserLogo.Image = Properties.Resources.user_logo_white;

                guna2Panel1SpeedMenu.Visible = true;
            }
        }

        private void guna2PictureBox1UserLogo_MouseLeave(object sender, EventArgs e)
        {
            if (Class1InfoAboutUserOrAdmin.nameUser == null)
            {
                guna2PictureBox1UserLogo.Image = Properties.Resources.user_logo_grey;
            }
            else if (Class1InfoAboutUserOrAdmin.nameUser != null)
            {
                label1NameUserOrAdmin.ForeColor = Color.DarkGray;
                guna2PictureBox1UserLogo.Image = Properties.Resources.user_logo_grey;
            }
        }

        #endregion


        #region Подключение к бд и переход на форму авторизации     

        private void guna2PictureBox1UserLogo_Click(object sender, EventArgs e)
        {
            if (Class1InfoAboutUserOrAdmin.nameUser == null)
            {
                string connectionString = "server = localhost; user = root; password = aris; database = KeyboardMaster";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        Form2Authorization form2Authorization = new Form2Authorization();
                        this.Hide();
                        form2Authorization.Show();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
                    }
                }
            }
        }


        #endregion


        #region Работа кнопок быстрого меню  

        private void guna2Button2EnterProfile_Click(object sender, EventArgs e)
        {
            Form3UserProfile form3UserProfile = new Form3UserProfile();
            this.Hide();
            form3UserProfile.Show();
        }

        private void guna2Button3LeaveAcc_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }




        #endregion


        #region Обновление текста для печати и анимация кнопки

        private void button1UpdateTargetText_Click(object sender, EventArgs e)
        {
            generationTargetText();
        }

        private void button1UpdateTargetText_MouseEnter(object sender, EventArgs e)
        {
            button1UpdateTargetText.Image = Properties.Resources.update_white;
        }

        private void button1UpdateTargetText_MouseLeave(object sender, EventArgs e)
        {
            button1UpdateTargetText.Image = Properties.Resources.update_gray;
        }

        #endregion


        #region Активация курсора на нулевом символе  

        private void richTextBox1Typing_Click(object sender, EventArgs e)
        {
            this.ActiveControl = richTextBox1Typing;
            richTextBox1Typing.Select(targetIndex, 1);
            richTextBox1Typing.SelectionColor = Color.White;
        }

        private void richTextBox1Typing_DoubleClick(object sender, EventArgs e)
        {
            this.ActiveControl = richTextBox1Typing;
            richTextBox1Typing.Select(targetIndex, 1);
            richTextBox1Typing.SelectionColor = Color.White;
        }

        #endregion


    }
}