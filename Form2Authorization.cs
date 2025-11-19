using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Клавиатурный_тренажер_KeyboardMaster
{
    public partial class Form2Authorization : Form
    {
        string connectionString = "server = localhost; user = root; password = aris; database = KeyboardMaster";

        bool isLeftMouseDown;
        Point startPoint;

        public Form2Authorization()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += Form1MainMenu_Shown;
            this.Activated += Form1MainMenu_Activated;
            this.Resize += Form1MainMenu_Resize;
            this.Size = new Size(1389, 795); // Фиксированный размер формы
            guna2TextBox1LoginAuth.MaxLength = 25;
            guna2TextBox2PasswordAuth.MaxLength = 25;
        }

        private void Form2Authorization_Load(object sender, EventArgs e)
        {
            guna2PictureBox1HidePassword.Location = new Point(253, 133); // Для авторизации
            guna2PictureBox2HidePassword.Location = new Point(253, 133); // Для регистрации
            panel2ForRegistr.Location = new Point(526, 228); // Для панели регистрации
            panel3ForRestorePassword.Location = new Point(526, 228); // Для панели восстановления пароля
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


        #region Переход на главную форму

        private void guna2PictureBox1Logo_Click(object sender, EventArgs e)
        {
            Form1MainMenu form1MainMenu = new Form1MainMenu();
            this.Hide();
            form1MainMenu.Show();
        }

        #endregion


        #region Активация кнопки авторизации. Поля "имя пользователя и пароль"       

        private void guna2TextBox1LoginAuth_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox1LoginAuth.Text) && !string.IsNullOrEmpty(guna2TextBox2PasswordAuth.Text))
            {
                guna2Button1Auth.Enabled = true;
            }
            else
            {
                guna2Button1Auth.Enabled = false;
            }
        }

        private void guna2TextBox2PasswordAuth_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox1LoginAuth.Text) && !string.IsNullOrEmpty(guna2TextBox2PasswordAuth.Text))
            {
                guna2Button1Auth.Enabled = true;
            }
            else
            {
                guna2Button1Auth.Enabled = false;
            }
        }

        #endregion


        #region Активация кнопки регистрации. Поля "имя пользователя, пароль и ключевое слово "  

        private void guna2TextBox3LoginRegistr_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox3LoginRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox4PasswordRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox5KeywordRegistr.Text))
            {
                guna2Button2Registr.Enabled = true;
            }
            else
            {
                guna2Button2Registr.Enabled = false;
            }
        }

        private void guna2TextBox4PasswordRegistr_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox3LoginRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox4PasswordRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox5KeywordRegistr.Text))
            {
                guna2Button2Registr.Enabled = true;
            }
            else
            {
                guna2Button2Registr.Enabled = false;
            }
        }

        private void guna2TextBox5KeywordRegistr_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox3LoginRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox4PasswordRegistr.Text) && !string.IsNullOrEmpty(guna2TextBox5KeywordRegistr.Text))
            {
                guna2Button2Registr.Enabled = true;
            }
            else
            {
                guna2Button2Registr.Enabled = false;
            }
        }

        #endregion


        #region Активация кнопки восстановления пароля. Поля "имя пользователя и ключевое слово"

        private void guna2TextBox6LoginRestore_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox6LoginRestore.Text) && !string.IsNullOrEmpty(guna2TextBox7KeywordRestore.Text))
            {
                guna2Button3Restore.Enabled = true;
            }
            else
            {
                guna2Button3Restore.Enabled = false;
            }
        }

        private void guna2TextBox7KeywordRestore_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(guna2TextBox6LoginRestore.Text) && !string.IsNullOrEmpty(guna2TextBox7KeywordRestore.Text))
            {
                guna2Button3Restore.Enabled = true;
            }
            else
            {
                guna2Button3Restore.Enabled = false;
            }
        }

        #endregion


        #region Показать/Скрыть пароль для авторизации  

        private void guna2PictureBox1ShowPassword_Click(object sender, EventArgs e)
        {
            guna2PictureBox1ShowPassword.Visible = false;
            guna2PictureBox1HidePassword.Visible = true;

            guna2TextBox2PasswordAuth.UseSystemPasswordChar = false;
        }

        private void guna2PictureBox1HidePassword_Click(object sender, EventArgs e)
        {
            guna2PictureBox1HidePassword.Visible = false;
            guna2PictureBox1ShowPassword.Visible = true;

            guna2TextBox2PasswordAuth.UseSystemPasswordChar = true;
        }


        #endregion


        #region Показать/Скрыть пароль для регистрации      

        private void guna2PictureBox2ShowPassword_Click(object sender, EventArgs e)
        {
            guna2PictureBox2ShowPassword.Visible = false;
            guna2PictureBox2HidePassword.Visible = true;

            guna2TextBox4PasswordRegistr.UseSystemPasswordChar = false;
        }

        private void guna2PictureBox2HidePassword_Click(object sender, EventArgs e)
        {
            guna2PictureBox2HidePassword.Visible = false;
            guna2PictureBox2ShowPassword.Visible = true;

            guna2TextBox4PasswordRegistr.UseSystemPasswordChar = true;
        }

        #endregion


        #region Регистрация нового пользователя

        private void guna2Button2Registr_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string addNewUserQuery = "insert into users (login, password, keyword) values (@login, @password, @keyword)";
                    MySqlCommand commandAddNewUser = new MySqlCommand(addNewUserQuery, connection);
                    commandAddNewUser.Parameters.AddWithValue("@login", guna2TextBox3LoginRegistr.Text);
                    commandAddNewUser.Parameters.AddWithValue("@password", guna2TextBox4PasswordRegistr.Text);
                    commandAddNewUser.Parameters.AddWithValue("@keyword", guna2TextBox5KeywordRegistr.Text);
                    commandAddNewUser.ExecuteNonQuery();
                    MessageBox.Show("Регистрация прошла успешно");

                    Form1MainMenu form1MainMenu = new Form1MainMenu();
                    this.Hide();
                    form1MainMenu.Show();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Ошибка при создании нового пользователя: " + ex.Message);
                }
            }
        }

        #endregion


        #region Авторизация пользователя

        private void guna2Button1Auth_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string lookForUserQuery = "select login, password from users where login = @login and password = @password";
                MySqlCommand commandLookForUser = new MySqlCommand(lookForUserQuery, connection);
                commandLookForUser.Parameters.AddWithValue("@login", guna2TextBox1LoginAuth.Text);
                commandLookForUser.Parameters.AddWithValue("@password", guna2TextBox2PasswordAuth.Text);
                object dataFromBDuser = commandLookForUser.ExecuteScalar();

                string lookForAdminQuery = "select login, password from admin where login = @login and password = @password";
                MySqlCommand commandlookForAdmin = new MySqlCommand(lookForAdminQuery, connection);
                commandlookForAdmin.Parameters.AddWithValue("@login", guna2TextBox1LoginAuth.Text);
                commandlookForAdmin.Parameters.AddWithValue("@password", guna2TextBox2PasswordAuth.Text);
                object dataFromBDadmin = commandlookForAdmin.ExecuteScalar();

                if (dataFromBDuser != null)
                {
                    MessageBox.Show("Авторизация прошла успешно");
                }
                else if (dataFromBDadmin != null)
                {
                    MessageBox.Show("Режим аминистратор");
                }
                else
                {
                    MessageBox.Show("Ошибка, данных не существует");
                }
            }
        }

        #endregion


        #region Восстановление пароля  

        private void guna2Button3Restore_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string getUserPassword = "select password from users where @login = login and @keyword = keyword";
                MySqlCommand commandGetUserPassword = new MySqlCommand(getUserPassword, connection);
                commandGetUserPassword.Parameters.AddWithValue("@login", guna2TextBox6LoginRestore.Text);
                commandGetUserPassword.Parameters.AddWithValue("@keyword", guna2TextBox7KeywordRestore.Text);
                object userPassword = commandGetUserPassword.ExecuteScalar();

                if (userPassword != null)
                {
                    panel3ForRestorePassword.Visible = false;
                    panel1ForAuth.Visible = true;

                    guna2TextBox2PasswordAuth.Text = userPassword.ToString();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        #endregion


        #region Смена панелей авторизации, регистрации и восстановления пароля

        private void label1RestorePassword_Click(object sender, EventArgs e)
        {
            panel1ForAuth.Visible = false;
            panel3ForRestorePassword.Visible = true;
        }

        private void label1CreateNewAcc_Click(object sender, EventArgs e)
        {
            panel1ForAuth.Visible = false;
            panel2ForRegistr.Visible = true;
        }

        private void label1EnterInAcc_Click(object sender, EventArgs e)
        {
            panel2ForRegistr.Visible = false;
            panel1ForAuth.Visible = true;
        }

        private void label2EnterInAcc_Click(object sender, EventArgs e)
        {
            panel3ForRestorePassword.Visible = false;
            panel1ForAuth.Visible = true;
        }

        #endregion


        #region Анимация label при навидении мышью

        private void label1RestorePassword_MouseEnter(object sender, EventArgs e)
        {
            label1RestorePassword.ForeColor = Color.White;
        }

        private void label1RestorePassword_MouseLeave(object sender, EventArgs e)
        {
            label1RestorePassword.ForeColor = Color.DarkGray;
        }

        private void label1CreateNewAcc_MouseEnter(object sender, EventArgs e)
        {
            label1CreateNewAcc.ForeColor = Color.White;
        }

        private void label1CreateNewAcc_MouseLeave(object sender, EventArgs e)
        {
            label1CreateNewAcc.ForeColor = Color.DarkGray;
        }

        private void label1EnterInAcc_MouseEnter(object sender, EventArgs e)
        {
            label1EnterInAcc.ForeColor = Color.White;
        }

        private void label1EnterInAcc_MouseLeave(object sender, EventArgs e)
        {
            label1EnterInAcc.ForeColor = Color.DarkGray;
        }

        private void label2EnterInAcc_MouseEnter(object sender, EventArgs e)
        {
            label2EnterInAcc.ForeColor = Color.White;
        }

        private void label2EnterInAcc_MouseLeave(object sender, EventArgs e)
        {
            label2EnterInAcc.ForeColor = Color.DarkGray;
        }

        #endregion

    }
}