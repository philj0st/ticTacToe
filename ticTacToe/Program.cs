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
        const Byte EMPTY = 0;
        const Byte CROSS = 1;
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

        static Position promptPosition(String player)
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

        //return 0 if noone is victorious, else return it's playernumber
        //#TODO: method is too big .. extract some methods
        static Byte checkForVictory(Byte[,] array)
        {
            Byte rowTotal = 0;
            //horizontal row calculation
            for (int y = 0; y < array.GetLength(1); y++)
            {
                rowTotal = 0;
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    rowTotal += array[x, y];
                }
                if (rowTotal == 3*CROSS)
                {
                    //3 Crosses in a row
                    return 1;
                }
                else if (rowTotal == 3*CIRCLE)
                {
                    //3 Circles in a row
                    return 2;
                }
            }
            //vertical row calculation
            for (int x = 0; x < array.GetLength(0); x++)
            {
                rowTotal = 0;
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    rowTotal += array[x, y];
                }
                //#TODO: maybe make it more modular? by using (rowTotal == array.GetLength(1) * CROSS)
                if (rowTotal == 3 * CROSS)
                {
                    //3 Crosses in a row
                    return 1;
                }
                else if (rowTotal == 3 * CIRCLE)
                {
                    //3 Circles in a row
                    return 2;
                }
            }
            //diagonal row calculation
            //not scalable for bigger arrays
            //maybe this grade of modularity and scalability doesn't make sense for such a small ticTacToe since it'ss always gonna be 3 rows only
            rowTotal = 0;
            for (int i = 0; i < 3; i++)
            {
                rowTotal += array[i, i];
            }
            if (rowTotal == 3 * CROSS)
            {
                //3 Crosses in a row
                return 1;
            }
            else if (rowTotal == 3 * CIRCLE)
            {
                //3 Circles in a row
                return 2;
            }
            rowTotal = 0;
            rowTotal += array[0, 2];
            rowTotal += array[1, 1];
            rowTotal += array[2, 0];
            if (rowTotal == 3 * CROSS)
            {
                //3 Crosses in a row
                return 1;
            }
            else if (rowTotal == 3 * CIRCLE)
            {
                //3 Circles in a row
                return 2;
            }
            return 0;
        }
        #endregion

        static void Main(string[] args)
        {
            Byte turns = 0;
            Byte[,] grid = new Byte[3, 3];
            Boolean playerOnesTurn = true;
            Byte victoriuousPlayer = 0;
            drawInstructions();
            fill2dByteArray(grid, EMPTY);

            //exit the loop as soon as a player is victorious or it's a draw
            while (victoriuousPlayer == 0 && turns < 9)
            {
                Console.WriteLine(turns);
                //Console.Clear();
                draw2dByteArray(grid);
                if (playerOnesTurn)
                {
                    Position signToAdd;
                    try
                    {
                        signToAdd = promptPosition("player1 (X)");
                    }
                    catch (Exception)
                    {
                        //skip the current iteration if the user entered a false key
                        //#TODO: make error message persistent over Console.Clear() of a turn
                        continue;
                    }
                    if (grid[signToAdd.X, signToAdd.Y] == EMPTY)
                    {
                        grid[signToAdd.X, signToAdd.Y] = CROSS;
                        turns++;
                        victoriuousPlayer = checkForVictory(grid);
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
                    Position signToAdd;
                    try
                    {
                        signToAdd = promptPosition("player2 (O)");
                    }
                    catch (Exception)
                    {
                        //skip the current iteration if the user entered a false key
                        continue;
                    }
                    if (grid[signToAdd.X, signToAdd.Y] == EMPTY)
                    {
                        grid[signToAdd.X, signToAdd.Y] = CIRCLE;
                        turns++;
                        victoriuousPlayer = checkForVictory(grid);
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
            draw2dByteArray(grid);
            if (victoriuousPlayer != 0)
            {
                Console.WriteLine("player {0} is victorious", victoriuousPlayer);
            }
            else
            {
                Console.WriteLine("draw");
            }
            String tmp = Console.ReadLine();
        }
    }
}
