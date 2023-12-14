using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLife
{
    public class GameLogic
    {
        private bool[,] gameField;
        private readonly int rows;
        private readonly int cols;
        public uint currentGen { get; private set; }


        public GameLogic(int rows, int cols, int density)
        {
            this.rows = rows;
            this.cols = cols;
            gameField = new bool[cols, rows];
            Random rnd = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    gameField[x, y] = rnd.Next(density) == 0;
                }
            }
        }
        public bool[,] GetCurrentGen()
        {
            var result = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = gameField[x, y];
                }
            }

            return result;
        }
        public void Generations()
        {
            bool[,] newField = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var counterNearby = CountNearby(x, y);
                    var haveLife = gameField[x, y];
                    if (!haveLife && counterNearby == 3)
                    {
                        newField[x, y] = true;
                    }
                    else if (haveLife && (counterNearby < 2 || counterNearby > 3)) // condition to live
                    {
                        newField[x, y] = false;
                    }
                    else
                    {
                        newField[x, y] = gameField[x, y];
                    }
                }
            }
            gameField = newField;
            currentGen++;
        }
        private int CountNearby(int x, int y)
        {
            int total = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;  // begin from this  var col = x + i;
                    var row = (y + j + rows) % rows;  // looking for other side of map

                    bool isSelf = col == x && row == y;
                    var haveLife = gameField[col, row];
                    if (haveLife && !isSelf)
                    {
                        total++;
                    }

                    // checking around
                    //
                    // * - empty, # - have neighbor
                    //
                    //          * * *
                    //          * # *
                    //          * * *
                    //          - death
                    //
                    //          # * *
                    //          * # *
                    //          # # *
                    //          - live
                }
            }
            return total;
        }
        private bool validateCellPos(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }
        private void updateCell(int x, int y, bool state)
        {
            if (validateCellPos(x, y))
            {
                gameField[x, y] = state;
            }
        }
        public void addCell(int x, int y)
        {
            updateCell(x, y, true);
        }
        public void removeCell(int x, int y)
        {
            updateCell(x, y, false);
        }
    }
}
