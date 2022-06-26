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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
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
