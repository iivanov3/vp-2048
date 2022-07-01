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
        private readonly Dictionary<int, Color> tileColors;
        private readonly Dictionary<Keys, string> validKeys;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            rules = new Rules();
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            tileColors = new Dictionary<int, Color>();
            tileColors[0] = Color.FromArgb(204, 192, 179);
            tileColors[2] = Color.FromArgb(238, 228, 218);
            tileColors[4] = Color.FromArgb(237, 224, 200);
            tileColors[8] = Color.FromArgb(242, 177, 121);
            tileColors[16] = Color.FromArgb(245, 149, 99);
            tileColors[32] = Color.FromArgb(246, 124, 95);
            tileColors[64] = Color.FromArgb(246, 94, 59);
            tileColors[128] = Color.FromArgb(237, 207, 114);
            tileColors[256] = Color.FromArgb(237, 204, 97);
            tileColors[512] = Color.FromArgb(237, 200, 80);
            tileColors[1024] = Color.FromArgb(237, 197, 63);
            tileColors[2048] = Color.FromArgb(237, 194, 46);

            validKeys = new Dictionary<Keys, string>();
            validKeys[Keys.Up] = "top";
            validKeys[Keys.Down] = "bottom";
            validKeys[Keys.Left] = "left";
            validKeys[Keys.Right] = "right";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game.isLost || game.isWon)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (validKeys.ContainsKey(keyData))
            {
                game.handleMove(validKeys[keyData]);

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
                    int val = game.getTileValue(j, i);
                    SolidBrush solidBrush = new SolidBrush(tileColors[val]);
                    String drawString = game.getTileValue(j, i).ToString();
                    Rectangle r = new Rectangle(10 + 101 * i, 10 + 101 * j, 100, 100);

                    formGraphics.DrawRectangle(myBrush, r);
                    formGraphics.FillRectangle(solidBrush, r);
                    if (val != 0)
                    {
                        formGraphics.DrawString(drawString, drawFont, drawBrush, r, drawFormat);
                    }
                    solidBrush.Dispose();
                }
            }

            label2.Text = "Score: " + game.getScore();

            myBrush.Dispose();
            formGraphics.Dispose();
            drawBrush.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            game = new Game();
            Invalidate(true);
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
