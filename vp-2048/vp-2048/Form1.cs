using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vp_2048
{
    public partial class Form1 : Form
    {
        private Game game;
        private Font drawFont = new Font("Arial", 25);
        private Graphics formGraphics;
        private Rules rules;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            rules = new Rules();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game.isLost || game.isWon)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            bool changed = false;
            if (keyData == Keys.Left)
            {
                game.handleMove("left");
                changed = true;
            }
            else if (keyData == Keys.Right)
            {
                game.handleMove("right");
                changed = true;
            }
            else if (keyData == Keys.Up)
            {
                game.handleMove("top");
                changed = true;
            }
            else if (keyData == Keys.Down)
            {
                game.handleMove("bottom");
                changed = true;
            }
            
            if (changed)
            {
                if (this.game.isWon)
                {
                    MessageBox.Show(String.Format("You win.\nYour score is {0}.", game.getScore()), "Game over");
                }
                else if (this.game.isLost)
                {
                    MessageBox.Show("You lose.", "Game over");
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen myBrush = new Pen(Color.Yellow);
            SolidBrush solidBrush = new SolidBrush(Color.Orange);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            formGraphics = this.CreateGraphics();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    String drawString = game.getTileValue(j, i).ToString();
                    Rectangle r = new Rectangle(10 + 101 * i, 10 + 101 * j, 100, 100);

                    formGraphics.DrawRectangle(myBrush, r);
                    formGraphics.FillRectangle(solidBrush, r);
                    formGraphics.DrawString(drawString, drawFont, drawBrush, r, drawFormat);
                }
            }

            label2.Text = "Score: " + game.getScore();

            myBrush.Dispose();
            formGraphics.Dispose();
            solidBrush.Dispose();
            drawBrush.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            game = new Game();
            Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            rules.Show();
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
