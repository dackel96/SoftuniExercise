using System;
using System.Linq;

namespace _08.BOMBS
{
    class Program
    {
        //you will receive indexes - coordinates to several cells separated by a single space,
        //in the following format: row1,column1 row2, column2  row3,column3… 
        //On those cells there are bombs.You have to proceed with every bomb,
        //one by one in the order they were given.When a bomb explodes deals damage equal to its integer value, to all the cells around it (in every direction and all diagonals).
        //One bomb can't explode more than once and after it does, its value becomes 0. When a cell’s value reaches 0 or below, it dies. Dead cells can't explode.
        //You must print the count of all alive cells and their sum.Afterward, print the matrix with all of its cells (including the dead ones). 

        static void Main(string[] args)
        {
            int matrixParameter = int.Parse(Console.ReadLine());
            int[,] matriX = new int[matrixParameter, matrixParameter];
            MatrixReader(matriX);
            //row1,column1  row2,column2  row3,column3… 
            int[] coordinates = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            for (int i = 1; i < coordinates.Length; i += 2)
            {
                int row = coordinates[i - 1];
                int col = coordinates[i];
                if (matriX[row, col] > 0)
                {
                    if (IsInRange(matriX, row - 1, col) && matriX[row - 1, col] > 0)
                    {
                        matriX[row - 1, col] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row - 1, col + 1) && matriX[row - 1, col + 1] > 0)
                    {
                        matriX[row - 1, col + 1] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row, col + 1) && matriX[row, col + 1] > 0)
                    {
                        matriX[row, col + 1] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row + 1, col + 1) && matriX[row + 1, col + 1] > 0)
                    {
                        matriX[row + 1, col + 1] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row + 1, col) && matriX[row + 1, col] > 0)
                    {
                        matriX[row + 1, col] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row + 1, col - 1) && matriX[row + 1, col - 1] > 0)
                    {
                        matriX[row + 1, col - 1] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row, col - 1) && matriX[row, col - 1] > 0)
                    {
                        matriX[row, col - 1] -= matriX[row, col];
                    }
                    if (IsInRange(matriX, row - 1, col - 1) && matriX[row - 1, col - 1] > 0)
                    {
                        matriX[row - 1, col - 1] -= matriX[row, col];
                    }
                    matriX[row, col] = 0;
                }
            }
            Console.WriteLine($"Alive cells: {MatrixCounter(matriX)}");
            Console.WriteLine($"Sum: {MatrixSum(matriX)}");
            MatrixWrite(matriX);
        }

        private static void MatrixWrite(int[,] matriX)
        {
            for (int row = 0; row < matriX.GetLength(0); row++)
            {
                for (int col = 0; col < matriX.GetLength(1); col++)
                {
                    Console.Write($"{matriX[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        private static int MatrixSum(int[,] matriX)
        {
            int sum = 0;
            for (int row = 0; row < matriX.GetLength(0); row++)
            {
                for (int col = 0; col < matriX.GetLength(1); col++)
                {
                    if (matriX[row, col] > 0)
                    {
                        sum += matriX[row, col];
                    }
                }
            }
            return sum;
        }

        private static int MatrixCounter(int[,] matriX)
        {
            int counter = 0;
            for (int row = 0; row < matriX.GetLength(0); row++)
            {
                for (int col = 0; col < matriX.GetLength(1); col++)
                {
                    if (matriX[row, col] > 0)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private static bool IsInRange(int[,] matriX, int row, int col)
        {
            return row >= 0 && row < matriX.GetLength(0) && col >= 0 && col < matriX.GetLength(1);
        }

        private static void MatrixReader(int[,] matriX)
        {
            for (int row = 0; row < matriX.GetLength(0); row++)
            {
                int[] rows = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < matriX.GetLength(1); col++)
                {
                    matriX[row, col] = rows[col];
                }
            }
        }
    }
}
