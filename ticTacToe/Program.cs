using System;
using System.Windows;
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

        struct Position
        {
            public Byte X,Y;

            public Position(Byte x, Byte y) : this()
            {
                this.X = x;
                this.Y = y;
            }
        }

        #region Helper Methods
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

        //Error Code 255 = key pressed is out of range. please enter keys from 0-2
        static Byte getByteFromKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    return 0;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return 2;
                default:
                    //#TODO: refactor: maybe there's a better solution.
                    //return 255;
                    throw new IndexOutOfRangeException("please enter keys from 0-2");
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

        private static Position promptPosition(String player)
        {
            Console.WriteLine("{0} where do you want to place your sign?", player);
            Console.Write(Environment.NewLine + "x:");
            ConsoleKeyInfo x = Console.ReadKey();
            Console.Write(Environment.NewLine + "y:");
            ConsoleKeyInfo y = Console.ReadKey();
            Byte enteredX = getByteFromKey(x.Key);
            Byte enteredY = getByteFromKey(y.Key);
            Console.WriteLine("{0} set a sign at {1}/{2}", player, enteredX, enteredY);
            return new Position(enteredX, enteredY);
        }
        #endregion

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            Byte[,] grid = new Byte[3, 3];
            Boolean playerOnesTurn = true;
            Boolean noOneHasWonYet = true;
            drawInstructions();
            fill2dByteArray(grid, EMPTY);

            while (noOneHasWonYet)
            {
                Console.Clear();
                draw2dByteArray(grid);
                if (playerOnesTurn)
                {
                    
                    Position signToAdd = promptPosition("player1");
                    if (grid[signToAdd.X, signToAdd.Y] == EMPTY)
                    {
                        grid[signToAdd.X, signToAdd.Y] = CROSS;
                        playerOnesTurn = !playerOnesTurn;
                    }
                    else
                    {
                        Console.WriteLine("[{0}/{1}] already contains {2}, please choose an empty field", signToAdd.X, signToAdd.Y, grid[signToAdd.X, signToAdd.Y]);
                        Console.Read();
                    }
                }
                else
                {
                    Position signToAdd = promptPosition("player2");
                    if (grid[signToAdd.X, signToAdd.Y] == EMPTY)
                    {
                        grid[signToAdd.X, signToAdd.Y] = CIRCLE;
                        playerOnesTurn = !playerOnesTurn;
                    }
                    else
                    {
                        Console.WriteLine("[{0}/{1}] already contains {2}, please choose an empty field", signToAdd.X, signToAdd.Y, grid[signToAdd.X, signToAdd.Y]);
                        Console.Read();
                    }
                }
            }

            Console.Clear();
            Console.Read();
        }
    }
}
