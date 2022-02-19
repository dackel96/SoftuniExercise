using System;

namespace __Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int nSize = int.Parse(Console.ReadLine());
            char[,] board = new char[nSize, nSize];
            for (int row = 0; row < board.GetLength(0); row++)
            {
                string rows = Console.ReadLine();
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = rows[col];
                }
            }

            int removedKs = 0;
            while (true)
            {
                int maxAttack = 0;
                int rowOfBadK = 0;
                int colOfBadK = 0;
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == '0')
                        {
                            continue;
                        }
                        int currAttack = 0;
                        //move K up and left
                        if (IsInRange(board, row - 2, col - 1) && board[row - 2, col - 1] == 'K')
                        {
                            currAttack++;
                        }
                        //move K up and right
                        if (IsInRange(board, row - 2, col + 1) && board[row - 2, col + 1] == 'K')
                        {
                            currAttack++;
                        }
                        //move K down and left
                        if (IsInRange(board, row + 2, col - 1) && board[row + 2, col - 1] == 'K')
                        {
                            currAttack++;
                        }
                        //move K down and right
                        if (IsInRange(board, row + 2, col + 1) && board[row + 2, col + 1] == 'K')
                        {
                            currAttack++;
                        }
                        //move K right and Up
                        if (IsInRange(board, row - 1, col + 2) && board[row - 1, col + 2] == 'K')
                        {
                            currAttack++;
                        }
                        //move K right and down
                        if (IsInRange(board, row + 1, col + 2) && board[row + 1, col + 2] == 'K')
                        {
                            currAttack++;
                        }
                        //move K left and up
                        if (IsInRange(board, row - 1, col - 2) && board[row - 1, col - 2] == 'K')
                        {
                            currAttack++;
                        }
                        //move K left and down
                        if (IsInRange(board, row + 1, col - 2) && board[row + 1, col - 2] == 'K')
                        {
                            currAttack++;
                        }
                        if (currAttack > maxAttack)
                        {
                            maxAttack = currAttack;
                            rowOfBadK = row;
                            colOfBadK = col;
                        }
                    }
                }
                if (maxAttack > 0)
                {
                    removedKs++;
                    board[rowOfBadK, colOfBadK] = '0';
                }
                else
                {
                    Console.WriteLine(removedKs);
                    break;
                }
            }
        }

        private static bool IsInRange(char[,] board, int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1);
        }
    }
}
