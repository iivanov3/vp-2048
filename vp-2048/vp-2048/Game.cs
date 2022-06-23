﻿using System;
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
        private bool isWon { get; set; }
        private bool isLost { get; set; }
        private int Score { get; set; }

        private int generateRandomNumber(int scale) // scale from 1 to ....
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
                        row[j] = 0;
                        break;
                    }

                }
            }
            return row;
        }
        public void printBoard()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(this.Board[i,j]);
                }
                Console.WriteLine();
            }
        }
        public int [,] getTable()
        {
            return this.Board;
        }
        public void handleMove(string direction) // right, left, bottom, top
        {
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
                    this.Board[i, j] = temp[j];
                }
            }

        }
    }
}
