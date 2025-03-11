using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess
{
    internal class GameSettings
    {
        int row = 0;
        int column = 0;
        Board displayBoard = new Board();
        ConsoleColor[] whiteColor = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor[] blackColor = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor[] cursorColor = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor[] legalMovesColor = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
        ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

        Dictionary<int, ConsoleColor> settings = new Dictionary<int, ConsoleColor>()
        {
            { 0, ConsoleColor.White },
            { 1, ConsoleColor.Gray },
            { 2, ConsoleColor.DarkYellow },
            { 3, ConsoleColor.Green },
        };

        public GameSettings()
        {
            displayBoard.Clear();
        }
        public void Print()
        {
            (int left, int top) = (5, 2);
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < settings.Count; i++)
            {
                for (int j = 0; j < colors.Length; j++)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(i * 6 + left, j + top);
                    if (settings[i] == colors[j])
                    {
                        Console.Write(">");
                    }

                    Console.Write("    ");
                    Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop);

                    if (column == i && row == j)
                        Console.Write("[");
                    Console.BackgroundColor = colors[j];
                    //Console.Write("░▒▓");
                    Console.Write("**");

                    if (column == i && row == j)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("]");
                    }
                }
            }

            Console.SetCursorPosition(24 + left, 5 + top);
            displayBoard.Print(2, 1, settings[0], settings[1], ChessColor.White);
            Console.SetCursorPosition(32 + left, 9 + top);
            Square.Print(2, 1, settings[3]);
            Console.SetCursorPosition(32 + left, 10 + top);
            Square.Print(2, 1, settings[3]);
            Console.SetCursorPosition(32 + left, 11 + top);
            Square.Print(2, 1, settings[2]);

            Console.ResetColor();
            Console.SetCursorPosition(32 + left, 15 + top);
            if (column == 4)
            {
                Console.Write("[Confirm]");
            }
            else Console.Write(" Confirm ");
        }
        public (ConsoleColor white, ConsoleColor black, ConsoleColor cursor, ConsoleColor legalMoves) SelectSettings()
        {
            bool confirm = false;
            while (!confirm)
            {
                Print();
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                    case ConsoleKey.Spacebar:
                        confirm = Confirm();
                        break;
                }
            }

            Console.Clear();
            return (settings[0], settings[1], settings[2], settings[3]);
        }
        public bool MoveLeft()
        {
            if (column <= 0)
                return false;
            column--;
            return true;
        }
        public bool MoveRight()
        {
            if (column >= settings.Count)
                return false;
            column++;
            return true;
        }
        public bool MoveUp()
        {
            if (row <= 0)
                return false;
            row--;
            return true;
        }
        public bool MoveDown()
        {
            if (row >= Enum.GetValues(typeof(ConsoleColor)).Length - 1)
                return false;
            row++;
            return true;
        }
        public bool Confirm()
        {
            if (column < 4)
            {
                settings[column] = colors[row];
                return false;
            }
            return true; 
        }
    }
}
