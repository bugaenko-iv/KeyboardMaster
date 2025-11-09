using System;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Клавиатурный_тренажер_KeyboardMaster
{
    public partial class Form2Authorization : Form
    {
        bool isLeftMouseDown;
        Point startPoint;

        public Form2Authorization()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += Form1MainMenu_Shown;
            this.Activated += Form1MainMenu_Activated;
            this.Resize += Form1MainMenu_Resize;
            this.Size = new Size(1389, 795); // Установите фиксированный размер
            guna2TextBox1LoginAuth.MaxLength = 25;
            guna2TextBox2PasswordAuth.MaxLength = 25;
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
    }
}