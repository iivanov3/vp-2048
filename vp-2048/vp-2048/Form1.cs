using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace vp_2048
{
    public partial class Form1 : Form
    {
        private Game game;
        private Graphics formGraphics;
        private Rules rules;
        private readonly Font drawFont;
        private readonly Dictionary<int, Color> tileColors;
        private readonly Dictionary<Keys, string> validKeys;

        public Form1()
        {
            InitializeComponent();
            Text = "2048";
            DoubleBuffered = true;

            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            drawFont = new Font("Arial", 25);

            tileColors = new Dictionary<int, Color>
            {
                { 0, Color.FromArgb(204, 192, 179) },
                { 2, Color.FromArgb(238, 228, 218) },
                { 4, Color.FromArgb(237, 224, 200) },
                { 8, Color.FromArgb(242, 177, 121) },
                { 16, Color.FromArgb(245, 149, 99) },
                { 32, Color.FromArgb(246, 124, 95) },
                { 64, Color.FromArgb(246, 94, 59) },
                { 128, Color.FromArgb(237, 207, 114) },
                { 256, Color.FromArgb(237, 204, 97) },
                { 512, Color.FromArgb(237, 200, 80) },
                { 1024, Color.FromArgb(237, 197, 63) },
                { 2048, Color.FromArgb(237, 194, 46) }
            };

            validKeys = new Dictionary<Keys, string>
            {
                { Keys.Up, "top" },
                { Keys.Down, "bottom" },
                { Keys.Left,"left" },
                { Keys.Right, "right" }
            };

            game = new Game();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!(game.IsLost || game.IsWon) && validKeys.ContainsKey(keyData))
            {
                game.handleMove(validKeys[keyData]);

                if (game.IsWon)
                {
                    MessageBox.Show(String.Format("You win.\nYour score is {0}.", game.getScore()), "Game over");
                }
                else if (game.IsLost)
                {
                    MessageBox.Show("You lose.", "Game over");
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen lines = new Pen(Color.Gray, 8);
            SolidBrush numbersBrush = new SolidBrush(Color.Black);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            formGraphics = CreateGraphics();
            for (int i = 0; i < game.BoardSize; i++)
            {
                for (int j = 0; j < game.BoardSize; j++)
                {
                    int val = game.getTileValue(i, j);
                    SolidBrush colorsBrush = new SolidBrush(tileColors[val]);
                    String drawString = game.getTileValue(i, j).ToString();
                    Rectangle r = new Rectangle(20 + 110 * j, 20 + 110 * i, 106, 106);

                    formGraphics.DrawRectangle(lines, r);
                    formGraphics.FillRectangle(colorsBrush, r);
                    if (val != 0)
                    {
                        formGraphics.DrawString(drawString, drawFont, numbersBrush, r, drawFormat);
                    }
                    colorsBrush.Dispose();
                }
            }

            label2.Text = "Score: " + game.getScore();

            lines.Dispose();
            numbersBrush.Dispose();
            formGraphics.Dispose();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            rules = new Rules();
            rules.Show();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            game = new Game();
            Invalidate(true);
        }
    }
}
