 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//DONE: moving to right works as it should(bugless).
//TODO: Transpose the board function so that shiftRight can be reused for other directions
namespace vp_2048
{
    class Game
    {
        private int[,] Board { get; set; }
        private int BoardSize { get; set; }
        public bool isWon { get; private set; }
        public bool isLost { get;private set; }
        private int Score { get; set; }

        private int generateRandomNumber(int scale) // scale from 1 to ...
        {
            Random randomGenerator = new Random();

            return randomGenerator.Next(0,scale);
        }

        public Game() // Definiraj pochetna sostojba na igrata
        {
            this.Score = 0;
            this.BoardSize = 4; // Let me be Lejzi
            this.isWon = false;
            this.isLost = false;
            this.Board = new int[4,4];
            for(int i=0;i<this.BoardSize;i++)
            {
                for(int j=0; j < this.BoardSize;j++)
                {
                    this.Board[i,j] = 0; // Init board;
                }
            }

            int randomCoordsX1, randomCoordsY1, randomCoordsX2, randomCoordsY2;
            randomCoordsX1 = generateRandomNumber(this.BoardSize);
            randomCoordsY1 = generateRandomNumber(this.BoardSize);
            randomCoordsX2 = generateRandomNumber(this.BoardSize);
            randomCoordsY2 = generateRandomNumber(this.BoardSize);
            while(randomCoordsX1 == randomCoordsX2 && randomCoordsY1 == randomCoordsY2) // ne mozheme 2 bloka vo 1 pole
            {
                randomCoordsX2 = generateRandomNumber(this.BoardSize);
                randomCoordsY2 = generateRandomNumber(this.BoardSize);
            }
            int firstBlockValue, secondBlockValue;
            int initialBlockProbability = generateRandomNumber(100);
            /*
             * Imame 20% blokot da bide 4ka i 80% da bide 2ka,
             * od nezavisnost na izborot na dvata bloka imame
             * 64% dvata bloka da se 2ka, 16% prviot da e 2ka vtoriot 4ka 16% prviot da e 4ka vtoriot 2ka i 4% dvata bloka da se 4ka
             */
            if (initialBlockProbability < 64)
            {
                firstBlockValue = 2;
                secondBlockValue = 2;
            }
            else if(initialBlockProbability < 80)
            {
                firstBlockValue = 2;
                secondBlockValue = 4;
            }
            else if(initialBlockProbability <96)
            {
                firstBlockValue = 4;
                secondBlockValue = 2;
            }else
            {
                firstBlockValue = 4;
                secondBlockValue = 4;
            }

            this.Board[randomCoordsX1,randomCoordsY1] = firstBlockValue;
            this.Board[randomCoordsX2,randomCoordsY2] = secondBlockValue;
           
            
        }
        private int[] shiftRight(int[] row) // zemi 1 red i podmesti mu gi decata udesno
        {
            for(int i=this.BoardSize-1; i>0;i--)
            {
                if (row[i] == 0)
                {
                    int t = i - 1;
                    while (t >= 0 && row[t] == 0) t--;
                    if (t < 0) break;
                    row[i] = row[t];
                    row[t] = 0;
                    i++;
                    continue;
                }
                for(int j=i-1;j>=0;j--)
                {
                    if (row[j] == 0)
                        continue;
                    if (row[j] == row[i])
                    {
                        row[i] *= 2;
                        this.Score += row[i];
                        row[j] = 0;
                        break;
                    }
                    if (row[j] != row[i])
                    {
                        row[i - 1] = row[j];
                        if(i-1 != j)
                        row[j] = 0;
                        break;
                    }

                }
            }
            return row;
        }
        private void rotateTable()
        {
            int[,] tempBoard = new int[this.BoardSize,this.BoardSize];
            for (int i = 0; i < this.BoardSize; i++)
            {
                for (int j = 0; j < this.BoardSize; j++)
                {
                    tempBoard[i,j] = this.Board[this.BoardSize-1-j,i];
                }
            }
            this.Board = tempBoard;
        }
        public int [,] getTable()
        {
            return this.Board;
        }
        public string getScore()
        {
            return this.Score.ToString();
        }
        private void addRandomTile()
        {
            int tileValue = generateRandomNumber(100) > 80 ? 4 : 2;
            int ranCoordsX, ranCoordsY;
            ranCoordsX = generateRandomNumber(this.BoardSize);
            ranCoordsY = generateRandomNumber(this.BoardSize);
            while(this.Board[ranCoordsX,ranCoordsY] != 0)
            {
                ranCoordsX = generateRandomNumber(this.BoardSize);
                ranCoordsY = generateRandomNumber(this.BoardSize);
            }

            this.Board[ranCoordsX, ranCoordsY] = tileValue;
        }
        private void checkWon()
        {
            for(int i=0;i<this.BoardSize;i++)
            {
                for(int j=0;j<this.BoardSize;j++)
                {
                    if (this.Board[i, j] == 2048)
                        this.isWon = true;
                }
            }
        }
        private bool isValid(int x, int y)
        {
            if (x >= 0 && x < this.BoardSize && y >= 0 && y < this.BoardSize)
                return true;

            return false;
        }
        private bool sameNeighbor(int x, int y)
        {
            bool result = false;
            if (isValid(x - 1, y))
            {
                result = result || this.Board[x - 1,y] == this.Board[x,y];
            }
            if (isValid(x + 1, y))
            {
                result = result || this.Board[x + 1, y] == this.Board[x, y];
            }
            if (isValid(x, y - 1))
            {
                result = result || this.Board[x, y - 1] == this.Board[x, y];
            }
            if (isValid(x, y + 1))
            {
                result = result || this.Board[x, y + 1] == this.Board[x, y];
            }

            return result;
        }
        private void checkLost()
        {
            int count = 0;
            for (int i = 0; i < this.BoardSize; i++)
            {
                for (int j = 0; j < this.BoardSize; j++)
                {
                    if (this.Board[i, j] != 0)
                        count += 1;
                }
            }
            if(count == this.BoardSize*this.BoardSize)
            {
                this.isLost = true;
                for (int i = 0; i < this.BoardSize; i++)
                {
                    for (int j = 0; j < this.BoardSize; j++)
                    {
                        if (sameNeighbor(i, j) == true)
                            this.isLost = false;
                    }
                }
            }
        }
        public void handleMove(string direction) // right, left, bottom, top
        {
            if(direction == "top")
            {
                this.rotateTable();
            }
            if(direction == "left")
            {
                this.rotateTable();
                this.rotateTable();
            }
            if(direction=="bottom")
            {
                this.rotateTable();
                this.rotateTable();
                this.rotateTable();
            }
            bool isValidMove = false;
            for (int i = 0; i < this.BoardSize; i++)
            {
                int[] temp = new int[this.BoardSize];
                for (int j = 0; j < this.BoardSize; j++)
                {
                    temp[j] = this.Board[i, j];
                }
                temp = shiftRight(temp);
                for (int j = 0; j < this.BoardSize; j++)
                {
                    if (this.Board[i, j] != temp[j]) isValidMove = true;
                    this.Board[i, j] = temp[j];
                }
            }

            if (direction == "bottom")
            {
                this.rotateTable();
            }
            if (direction == "left")
            {
                this.rotateTable();
                this.rotateTable();
            }
            if (direction == "top")
            {
                this.rotateTable();
                this.rotateTable();
                this.rotateTable();
            }
            //TODO: check if lost, add new random block every move, then check if won
            if (isValidMove)
            {
                this.addRandomTile();
                this.checkWon();
                this.checkLost();
            }
        }
    }
}
