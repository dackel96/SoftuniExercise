using System;
using System.Linq;

namespace _02._Super_Mario
{
    class Program
    {
        static void Main(string[] args)
        {
            int lives = int.Parse(Console.ReadLine());
            int rows = int.Parse(Console.ReadLine());
            char[][] castle = new char[rows][];
            int marioRow = default;
            int marioCol = default;
            for (int row = 0; row < castle.GetLength(0); row++)
            {
                var currRow = Console.ReadLine().ToCharArray();
                castle[row] = currRow;
                if (currRow.Contains('M'))
                {
                    marioRow = row;
                    marioCol = currRow.ToList().IndexOf('M');
                }
            }
            castle[marioRow][marioCol] = '-';
            while (true)
            {
                string[] cmd = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string direction = cmd[0];
                int row = int.Parse(cmd[1]);
                int col = int.Parse(cmd[2]);
                castle[row][col] = 'B';
                if (IsInRange(castle, marioRow - 1, marioCol) && direction == "W") //"W" up row-
                {
                    marioRow--;
                    lives--;
                }
                else if (IsInRange(castle, marioRow + 1, marioCol) && direction == "S") // "S" down row+
                {
                    marioRow++;
                    lives--;
                }
                else if (IsInRange(castle, marioRow, marioCol - 1) && direction == "A") // "A" left col-
                {
                    marioCol--;
                    lives--;
                }
                else if (IsInRange(castle, marioRow, marioCol + 1) && direction == "D") // "D" right col+
                {
                    marioCol++;
                    lives--;
                }
                else
                {
                    lives--;
                }
                if (castle[marioRow][marioCol] == 'B')
                {
                    lives -= 2;
                }
                else if (castle[marioRow][marioCol] == 'P')
                {
                    Console.WriteLine($"Mario has successfully saved the princess! Lives left: {lives}");
                    castle[marioRow][marioCol] = '-';
                    break;
                }
                if (lives <= 0)
                {
                    Console.WriteLine($"Mario died at {marioRow};{marioCol}.");
                    castle[marioRow][marioCol] = 'X';
                    break;
                }
                castle[marioRow][marioCol] = '-';
            }
            PrintTheMatrix(castle, rows);
        }

        private static void PrintTheMatrix(char[][] castle, int rows)
        {
            for (int row = 0; row < castle.Length; row++)
            {
                for (int col = 0; col < castle[row].Length; col++)
                {
                    Console.Write(castle[row][col]);
                }
                Console.WriteLine();
            }
        }

        private static bool IsInRange(char[][] board, int row, int col)
        {
            return row >= 0 && row < board.Length && col >= 0 && col < board[row].Length;
        }
    }

}
