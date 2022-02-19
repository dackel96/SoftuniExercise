using System;
using System.Linq;

namespace _2X2_Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowCol = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            char[,] matriX = new char[rowCol[0], rowCol[1]];
            for (int row = 0; row < matriX.GetLength(0); row++)
            {
                char[] rows = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                for (int col = 0; col < matriX.GetLength(1); col++)
                {
                    matriX[row, col] = rows[col];
                }
            }
            int nSquares = 0;
            for (int row = 0; row < matriX.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matriX.GetLength(1) - 1; col++)
                {
                    char currSymbol = matriX[row, col];

                    char topRight = matriX[row, col + 1];
                    char bottomLeft = matriX[row + 1, col];
                    char bottomRight = matriX[row + 1, col + 1];
                    if (currSymbol == topRight && currSymbol == bottomLeft && currSymbol == bottomRight)
                    {
                        nSquares++;
                    }
                }
            }
            Console.WriteLine(nSquares);
        }
    }
}
