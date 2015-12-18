using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ticTacToe
{
    class Program
    {
        const Byte EMPTY = 1;
        const Byte CROSS = 2;
        const Byte CIRCLE = 4;

        static void fill2dByteArray(Byte[,] array, Byte value)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    array[x, y] = value;
                }
            }
        }

        static void draw2dByteArray(Byte[,] array)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                String line = "";
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    switch (array[x,y])
                    {
                        case EMPTY:
                            line += "-";
                            break;
                        case CROSS:
                            line += "X";
                            break;
                        case CIRCLE:
                            line += "O";
                            break;
                    }
                }
                Console.WriteLine("["+line+"]");
            }
        }

        static void drawInstructions()
        {
            Console.WriteLine("Welcome to ticTacToe");
            Console.WriteLine("please place crosses and circles with x and y coordiantes between 0 and 2");
            Console.WriteLine("  012{0}0[--X]{0}1[-XO]{0}2[XO-]",Environment.NewLine);
            Console.WriteLine("press Enter to start");
            Console.Read();
        }


        static void Main(string[] args)
        {
            Byte[,] grid = new Byte[3, 3];
            Boolean playerOnesTurn = true;
            Boolean noOneHasWonYet = true;
            drawInstructions();
            
            while (noOneHasWonYet)
            {
                //Console.Clear();
                if (playerOnesTurn)
                {
                    Console.WriteLine("Player 1 please place a Cross");
                    Console.Write(Environment.NewLine + "x:");
                    ConsoleKeyInfo x = Console.ReadKey();
                    Console.Write(Environment.NewLine + "y:");
                    ConsoleKeyInfo y = Console.ReadKey();
                    playerOnesTurn = !playerOnesTurn;
                }
                else
                {
                    playerOnesTurn = !playerOnesTurn;
                }
            }
            
            Console.Clear();
            fill2dByteArray(grid, EMPTY);
            grid[1, 1] = CROSS;
            grid[2, 1] = CIRCLE;
            draw2dByteArray(grid);
            Console.Read();
        }
    }
}
