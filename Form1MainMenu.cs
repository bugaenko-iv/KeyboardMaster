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
        string labelText;
        string typingText;
        int countMistake = 0;

        public Form1MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += Form1MainMenu_Shown;
            this.Activated += Form1MainMenu_Activated;
            this.Resize += Form1MainMenu_Resize;
            this.Size = new Size(1389, 795); // Установите фиксированный размер
            guna2TextBox1Typing.Focus();
        }

        private void Form1MainMenu_Load(object sender, EventArgs e)
        {
            labelText = label1Text.Text;
            this.ActiveControl = guna2TextBox1Typing;
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


        #region Печать с клавиатуры и проверка

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            typingText = guna2TextBox1Typing.Text;

            if (!string.IsNullOrEmpty(guna2TextBox1Typing.Text))
            {
                for (int i = 0; i < typingText.Length; i++) 
                { 
                    if (typingText[i].ToString() != labelText[i].ToString())
                    {
                        countMistake++;
                    }
                }
            }

            label1.Text = countMistake.ToString();
        }

        #endregion
    }
}