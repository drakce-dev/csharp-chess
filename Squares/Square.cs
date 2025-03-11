using System.Runtime.CompilerServices;
using Chess.Pieces;

namespace Chess.Squares
{
    internal class Square
    {
        public readonly Rank Rank;
        public readonly File File;
        public readonly ChessColor Color;
        public static ConsoleColor whiteColor = ConsoleColor.White;
        public static ConsoleColor blackColor = ConsoleColor.Gray;
        public Piece? Contents;
        public Square(Rank rank, File file)
        {
            Rank = rank;
            File = file;
            Color = (ChessColor)(((int)file + (int)rank) % 2);
            Contents = null;
        }
        public Square(int rank, int file)
        {
            Rank = (Rank)rank;
            File = (File)file;
            Color = (ChessColor)((file + rank + 1) % 2);
            Contents = null;
        }
        public Square(File file, Rank rank, Piece? content)
        {
            File = file;
            Rank = rank;
            Color = (ChessColor)(((int)file + (int)rank + 1) % 2);
            Contents = content;
        }

        public void Print(int width, int height)
        {
            if (Color == ChessColor.White)
                Print(width, height, whiteColor);
            else
                Print(width, height, blackColor);
        }
        public void Print(int width, int height, ConsoleColor color)
        {
            Print(width, height, color, Contents?.Symbol);
        }
        public void Print(int width, int height, ConsoleColor whiteColor, ConsoleColor blackColor)
        {
            if (Color == ChessColor.White)
                Print(width, height, whiteColor);
            else
                Print(width, height, blackColor);
        }
        public static void Print(int width, int height, ConsoleColor color, char? symbol = null)
        {
            Console.BackgroundColor = color;
            (int left, int top) = Console.GetCursorPosition();
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(left, top + i);
                if (i == height / 2 && symbol != null)
                    Console.Write(new string(' ', width / 2) + symbol + new string(' ', width / 2));
                else Console.Write(new string(' ', width));
            }
            Console.SetCursorPosition(left, top);
        }
        public bool SameSquare(Square square)
        {
            return square.File == File && square.Rank == Rank;
        }
    }
}
