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
     
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();

            this.updateTable();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game.isLost || game.isWon) 
                return base.ProcessCmdKey(ref msg, keyData);
            
            if (keyData == Keys.Left)
            {
                game.handleMove("left");
                this.updateTable();
                if (this.game.isWon)
                    MessageBox.Show("Won");
                else if (this.game.isLost)
                    MessageBox.Show("Lost");
                return true;
            }
            if (keyData == Keys.Right)
            {
                game.handleMove("right");
                this.updateTable();
                if (this.game.isWon)
                    MessageBox.Show("Won");
                else if (this.game.isLost)
                    MessageBox.Show("Lost");

                return true;
            }
            if (keyData == Keys.Up)
            {
                game.handleMove("top");
                this.updateTable();
                if (this.game.isWon)
                    MessageBox.Show("Won");
                else if (this.game.isLost)
                    MessageBox.Show("Lost");

                return true;
            }
            if (keyData == Keys.Down)
            {
                game.handleMove("bottom");
                this.updateTable();
                if (this.game.isWon)
                    MessageBox.Show("Won");
                else if (this.game.isLost)
                    MessageBox.Show("Lost");

                return true;
            }
           

            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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


            myBrush.Dispose();
            formGraphics.Dispose();
            solidBrush.Dispose();
            drawBrush.Dispose();

        }
    
        private void updateTable()
        {
            int[,] arr = new int[4, 4];
            arr = game.getTable();
            string arrTSr = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    arrTSr += arr[i, j].ToString() + " ";
                }
                arrTSr += "\n";
            }
            label1.Text = arrTSr;

            this.Invalidate();

            label2.Text = "Score: " + this.game.getScore();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.handleMove("right");
            this.updateTable();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            game.handleMove("top");
            this.updateTable();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            game.handleMove("left");
            this.updateTable();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            game.handleMove("bottom");
            this.updateTable();
        }
    }
}
