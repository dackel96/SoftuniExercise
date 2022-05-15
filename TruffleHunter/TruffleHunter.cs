using System;
using System.Text;

namespace Truffle_Hunter
{
    class TruffleHunter
    {
        static void Main(string[] args)
        {
            // 'B' => black truffle // 'S' => summer truffle // 'W' white truffle
            int blackTruffle = 0;
            int summerTruffle = 0;
            int whiteTruffle = 0;
            int eatenTruffles = 0;
            //On the first line, the size of the forest is given, which will be a matrix with a square shape.
            //Then for each row, you will receive the truffles.All of the empty positions will be marked with a '-'(dash).
            int matrixSize = int.Parse(Console.ReadLine());
            char[,] forest = new char[matrixSize, matrixSize];

            for (int row = 0; row < forest.GetLength(0); row++)
            {
                string[] rows = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < forest.GetLength(1); col++)
                {
                    if (char.Parse(rows[col]) != '-')
                    {
                        forest[row, col] = char.Parse(rows[col].ToUpper());
                    }
                    else
                    {
                        forest[row, col] = char.Parse(rows[col]);
                    }
                }
            }

            // Then you will start receiving commands. Here are the possible ones you can receive:
            // "Collect {row} {col}" -
            // Peter goes to the given place in the forest and collect the truffle if it exists.When he collects a truffle, the cell’s should be change to a dash('-').
            // "Wild_Boar {row} {col} {direction}" -
            // a wild boar appeirs in the given coordinates(they will be always valid) in the forest and it goes all the way in that direction.
            // Which means the wild boar, goes from the given cell to the last cell in the given direction.
            // It eats the given cell, skips the next, and eats the next one, if there is a truffle there, and so on until it reaches the last cell in the given direction.
            // Mark the eaten cells with a dash('-').There are four possible directions:
            // "up", "down", "left", and "right"
            // "Stop the hunt" – the program stops and the result is printed.

            string commandInput = Console.ReadLine();
            while (commandInput != "Stop the hunt")
            {
                string[] cmdSplit = commandInput
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (cmdSplit[0].ToLower() == "collect")
                {
                    int row = int.Parse(cmdSplit[1]);
                    int col = int.Parse(cmdSplit[2]);
                    if (forest[row, col] != '-')
                    {
                        if (forest[row, col] == 'B')
                        {
                            forest[row, col] = '-';
                            blackTruffle++;
                        }
                        else if (forest[row, col] == 'S')
                        {
                            forest[row, col] = '-';
                            summerTruffle++;
                        }
                        else if (forest[row, col] == 'W')
                        {
                            forest[row, col] = '-';
                            whiteTruffle++;
                        }
                    }
                }
                else if (cmdSplit[0].ToLower() == "wild_boar")
                {
                    int row = int.Parse(cmdSplit[1]);
                    int col = int.Parse(cmdSplit[2]);
                    string direction = cmdSplit[3].ToLower();
                    if (direction == "up")
                    {
                        bool onOff = true;
                        for (int rows = row; rows >= 0; rows--)
                        {
                            if (onOff)
                            {
                                if (forest[rows, col] != '-')
                                {
                                    eatenTruffles++;
                                    forest[rows, col] = '-';
                                }
                                onOff = false;
                            }
                            else
                            {
                                onOff = true;
                            }
                        }
                    }
                    else if (direction == "left")
                    {
                        bool onOff = true;
                        for (int cols = col; cols >= 0; cols--)
                        {
                            if (onOff)
                            {
                                if (forest[row, cols] != '-')
                                {
                                    eatenTruffles++;
                                    forest[row, cols] = '-';
                                }
                                onOff = false;
                            }
                            else
                            {
                                onOff = true;
                            }
                        }
                    }
                    else if (direction == "down")
                    {
                        bool onOff = true;
                        for (int rows = row; rows < matrixSize; rows++)
                        {
                            if (onOff)
                            {
                                if (forest[rows, col] != '-')
                                {
                                    eatenTruffles++;
                                    forest[rows, col] = '-';
                                }
                                onOff = false;
                            }
                            else
                            {
                                onOff = true;
                            }
                        }
                    }
                    else if (direction == "right")
                    {
                        bool onOff = true;
                        for (int cols = col; cols < matrixSize; cols++)
                        {
                            if (onOff)
                            {
                                if (forest[row, cols] != '-')
                                {
                                    eatenTruffles++;
                                    forest[row, cols] = '-';
                                }
                                onOff = false;
                            }
                            else
                            {
                                onOff = true;
                            }
                        }
                    }
                }
                commandInput = Console.ReadLine();
            }
            Console.WriteLine($"Peter manages to harvest {blackTruffle} black, {summerTruffle} summer, and {whiteTruffle} white truffles.");
            Console.WriteLine($"The wild boar has eaten {eatenTruffles} truffles.");
            for (int row = 0; row < matrixSize; row++)
            {
                for (int col = 0; col < matrixSize; col++)
                {
                    Console.Write($"{forest[row, col]} ");
                }
                Console.WriteLine();
            }
        }
        private static bool IsInMatriX(char[,] matriX, int row, int col)
        {
            return row >= 0 && row < matriX.GetLength(0) && col >= 0 && col < matriX.GetLength(1);
        }
    }
}
