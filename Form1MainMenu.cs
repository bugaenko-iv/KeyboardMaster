using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Клавиатурный_тренажер_KeyboardMaster
{
    public partial class Form1MainMenu : Form
    {
        bool isLeftMouseDown;
        Point startPoint;
        int countMistake = 0; // Кол-во ошибок в тексе при печатании
        int targetIndex = 0;
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
            this.ActiveControl = richTextBox1Typing;

            targetText = richTextBox1Typing.Text;
            richTextBox1Typing.Select(targetIndex, 1);
            richTextBox1Typing.SelectionColor = Color.White;

            label1NameUserOrAdmin.Text = Class1InfoAboutUserOrAdmin.nameUser;
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

    }
}