using System;

namespace vp_2048
{
    class Game
    {
        public int[,] Board { get; set; }
        public int BoardSize { get; set; }
        public bool IsWon { get; set; }
        public bool IsLost { get; set; }
        public int Score { get; set; }
        public static Random random = new Random();

        private int generateRandomNumber(int scale)
        {
            return random.Next(0, scale);
        }

        public Game()
        {
            Score = 0;
            BoardSize = 4; 
            IsWon = false;
            IsLost = false;
            Board = new int[4,4];

            int randomCoordsX1 = generateRandomNumber(BoardSize);
            int randomCoordsY1 = generateRandomNumber(BoardSize);
            int randomCoordsX2 = generateRandomNumber(BoardSize);
            int randomCoordsY2 = generateRandomNumber(BoardSize);

            while(randomCoordsX1 == randomCoordsX2 && randomCoordsY1 == randomCoordsY2)
            {
                randomCoordsX2 = generateRandomNumber(BoardSize);
                randomCoordsY2 = generateRandomNumber(BoardSize);
            }

            int firstBlockValue, secondBlockValue;
            int initialBlockProbability = generateRandomNumber(100);

            if (initialBlockProbability < 64)
            {
                firstBlockValue = 2;
                secondBlockValue = 2;
            }
            else if (initialBlockProbability < 80)
            {
                firstBlockValue = 2;
                secondBlockValue = 4;
            }
            else if (initialBlockProbability < 96)
            {
                firstBlockValue = 4;
                secondBlockValue = 2;
            }
            else
            {
                firstBlockValue = 4;
                secondBlockValue = 4;
            }

            Board[randomCoordsX1, randomCoordsY1] = firstBlockValue;
            Board[randomCoordsX2, randomCoordsY2] = secondBlockValue;
        }
        private int[] shiftRight(int[] row)
        {
            for (int i = BoardSize - 1; i > 0; i--)
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
                for (int j = i - 1; j >= 0; j--)
                {
                    if (row[j] == 0)
                        continue;
                    if (row[j] == row[i])
                    {
                        row[i] *= 2;
                        Score += row[i];
                        row[j] = 0;
                        break;
                    }
                    if (row[j] != row[i])
                    {
                        row[i - 1] = row[j];
                        if (i - 1 != j)
                            row[j] = 0;
                        break;
                    }

                }
            }
            return row;
        }
        private void rotateTable()
        {
            int[,] tempBoard = new int[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    tempBoard[i, j] = Board[BoardSize - 1 - j, i];
                }
            }
            Board = tempBoard;
        }

        public int getTileValue(int x, int y)
        {
            return Board[x, y];
        }
        public string getScore()
        {
            return Score.ToString();
        }
        private void addRandomTile()
        {
            int tileValue = generateRandomNumber(100) > 80 ? 4 : 2;
            int ranCoordsX = generateRandomNumber(BoardSize);
            int ranCoordsY = generateRandomNumber(BoardSize);

            while (Board[ranCoordsX, ranCoordsY] != 0)
            {
                ranCoordsX = generateRandomNumber(BoardSize);
                ranCoordsY = generateRandomNumber(BoardSize);
            }

            Board[ranCoordsX, ranCoordsY] = tileValue;
        }
        private void checkWon()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] == 2048)
                        IsWon = true;
                }
            }
        }
        private bool isValid(int x, int y)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
                return true;
            return false;
        }
        private bool sameNeighbor(int x, int y)
        {
            bool result = false;

            if (isValid(x - 1, y))
            {
                result = result || Board[x - 1, y] == Board[x, y];
            }
            if (isValid(x + 1, y))
            {
                result = result || Board[x + 1, y] == Board[x, y];
            }
            if (isValid(x, y - 1))
            {
                result = result || Board[x, y - 1] == Board[x, y];
            }
            if (isValid(x, y + 1))
            {
                result = result || Board[x, y + 1] == Board[x, y];
            }

            return result;
        }
        private void checkLost()
        {
            int count = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] != 0)
                    {
                        count++;
                    }
                }
            }

            if (count == BoardSize * BoardSize)
            {
                IsLost = true;
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (sameNeighbor(i, j))
                        {
                            IsLost = false;
                        }
                    }
                }
            }
        }
        public void handleMove(string direction)
        {
            if (direction == "top")
            {
                rotateTable();
            }
            else if (direction == "left")
            {
                rotateTable();
                rotateTable();
            }
            else if (direction == "bottom")
            {
                rotateTable();
                rotateTable();
                rotateTable();
            }

            bool isValidMove = false;
            for (int i = 0; i < BoardSize; i++)
            {
                int[] temp = new int[BoardSize];
                for (int j = 0; j < BoardSize; j++)
                {
                    temp[j] = Board[i, j];
                }

                temp = shiftRight(temp);
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i, j] != temp[j])
                    {
                        isValidMove = true;
                    }
                    Board[i, j] = temp[j];
                }
            }

            if (direction == "bottom")
            {
                rotateTable();
            }
            else if (direction == "left")
            {
                rotateTable();
                rotateTable();
            }
            else if (direction == "top")
            {
                rotateTable();
                rotateTable();
                rotateTable();
            }

            if (isValidMove)
            {
                addRandomTile();
                checkWon();
                checkLost();
            }
        }
    }
}
