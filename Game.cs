using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;
using Chess.Squares;

namespace Chess
{
    internal class Game
    {
        Board board;
        bool[,] legalMoves = new bool[8, 8];
        public readonly int squareWidth = 5;
        public readonly int squareHeight = 3;
        readonly ConsoleColor whiteColor = ConsoleColor.White;
        readonly ConsoleColor blackColor = ConsoleColor.Gray;
        readonly ConsoleColor cursorColor = ConsoleColor.DarkYellow;
        readonly ConsoleColor legalMovesColor = ConsoleColor.Green;
        (int rank, int file) cursor = (0, 0);
        Square? selectedSquare = null;
        public int Turn { get; private set; } = 1;
        public ChessColor PlayerTurn
        {
            get
            {
                return (ChessColor)((Turn - 1) % 2);
            }
        }
        public Game()
        {
            board = new Board();
        }
        public Game(ConsoleColor whiteColor, ConsoleColor blackColor, ConsoleColor cursorColor, ConsoleColor legalMovesColor)
        {
            this.whiteColor = whiteColor;
            this.blackColor = blackColor;
            this.cursorColor = cursorColor;
            this.legalMovesColor = legalMovesColor;
            board = new Board();
        }

        public void Print()
        {
            Print(PlayerTurn);
        }
        public void Print(ChessColor playerColor)
        {
            Console.SetCursorPosition(0, 0);

            if (playerColor == ChessColor.White)
            {
                for (int i = board.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(j * squareWidth, (board.GetLength(0) - 1 - i) * squareHeight);
                        if (cursor.file == j && cursor.rank == i)
                            board[i, j].Print(squareWidth, squareHeight, cursorColor);
                        else if (legalMoves[i, j]) board[i, j].Print(squareWidth, squareHeight, legalMovesColor);
                        else board[i, j].Print(squareWidth, squareHeight, whiteColor, blackColor);
                    }
                }
            }
            else
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(j * squareWidth, i * squareHeight);
                        if (cursor.file == j && cursor.rank == i)
                            board[i, j].Print(squareWidth, squareHeight, cursorColor);
                        else if (legalMoves[i, j]) board[i, j].Print(squareWidth, squareHeight, legalMovesColor);
                        else board[i, j].Print(squareWidth, squareHeight, whiteColor, blackColor);
                    }
                }
            }
        }

        public void CursorAction()
        {
            bool validInput = false;
            while (!validInput)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        validInput = MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        validInput = MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        if (PlayerTurn == ChessColor.White) validInput = MoveDown();
                        else validInput = MoveUp();
                        break;
                    case ConsoleKey.UpArrow:
                        if (PlayerTurn == ChessColor.White) validInput = MoveUp();
                        else validInput = MoveDown();
                        break;
                    case ConsoleKey.Spacebar:
                        validInput = Action();
                        break;
                }
            }
        }

        public bool MoveLeft()
        {
            if (cursor.file <= (int)Squares.File.A)
                return false;
            cursor.file--;
            return true;
        }
        public bool MoveRight()
        {
            if (cursor.file >= (int)Squares.File.H)
                return false;
            cursor.file++;
            return true;
        }
        public bool MoveDown()
        {
            if (cursor.rank <= (int)Rank.Rank1)
                return false;
            cursor.rank--;
            return true;
        }
        public bool MoveUp()
        {
            if (cursor.rank >= (int)Rank.Rank8)
                return false;
            cursor.rank++;
            return true;
        }
        public bool Action()
        {
            if (board[cursor.rank, cursor.file].Contents == null && !legalMoves[cursor.rank, cursor.file] || selectedSquare == new Square(cursor.rank, cursor.file))
                return false;
            if (legalMoves[cursor.rank, cursor.file])
            {
                Move(selectedSquare, new Square(cursor.rank, cursor.file));
            }
            else if (board[cursor.rank, cursor.file].Contents?.Color == PlayerTurn)
            {
                legalMoves = new bool[8, 8];
                foreach (Square square in board[cursor.rank, cursor.file].Contents.LegalMoves(new Square(cursor.rank, cursor.file), board))
                {
                    Square[,] newBoard = new Square[8, 8];
                    for (int i = 0; i < board.GetLength(0); i++)
                    {
                        for (int j = 0; j < board.GetLength(1); j++)
                        {
                            newBoard[i, j] = new Square(i, j);
                            newBoard[i, j].Contents = board[i, j].Contents;
                        }
                    }
                    board[(int)square.Rank, (int)square.File].Contents = board[cursor.rank, cursor.file].Contents;
                    board[cursor.rank, cursor.file].Contents = null;

                    HashSet<Square> enemyLegalMoves = AllLegalMoves((ChessColor)(Turn % 2));

                    Square kingPosition = board.KingPosition(PlayerTurn);

                    for (int i = 0; i < board.GetLength(0); i++)
                        for (int j = 0; j < board.GetLength(1); j++)
                            board[i, j].Contents = newBoard[i, j].Contents;

                    if (enemyLegalMoves.Any(square => square.SameSquare(kingPosition)))
                        continue;

                    legalMoves[(int)square.Rank, (int)square.File] = true;
                }
                selectedSquare = board[cursor.rank, cursor.file];
            }
            return true;
        }
        public HashSet<Square> AllLegalMoves(ChessColor color)
        {
            HashSet<Square> enemyLegalMoves = new HashSet<Square>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (board[i, j].Contents != null && board[i, j].Contents.Color == color)
                        enemyLegalMoves.UnionWith(board[i, j].Contents.LegalMoves(new Square(i, j), board));

            return enemyLegalMoves;
        }
        public bool Move(Square start, Square end)
        {
            Piece? piece = board.GetPiece(start);
            if (piece == null) return false;

            legalMoves = new bool[8, 8];
            board[(int)end.Rank, (int)end.File].Contents = piece;
            board[(int)start.Rank, (int)start.File].Contents = null;
            if (piece is King && Math.Abs(start.File - end.File) == 2)
            {
                if (start.File > end.File)
                    return Move(new Square(start.Rank, Squares.File.A), new Square(start.Rank, end.File + 1));
                else return Move(new Square(start.Rank, Squares.File.H), new Square(start.Rank, end.File - 1));
            }
            else if (piece is Pawn && (end.Rank == Rank.Rank1 || end.Rank == Rank.Rank8))
            {
                board[(int)end.Rank, (int)end.File].Contents = Promotion(piece.Color);
            }
            piece.PieceMoved = true;
            Turn++;

            return true;
        }
        public Piece Promotion(ChessColor color)
        {
            Piece[] promotionPieces = { new Queen(color), new Rook(color), new Bishop(color), new Knight(color) };
            int cursor = 0;
            int left = 43;
            ConsoleKey key;
            do
            {
                for (int i = 0; i < promotionPieces.Length; i++)
                {
                    Console.SetCursorPosition(left, i * squareHeight + 6);
                    if (cursor == i)
                        Square.Print(squareWidth, squareHeight, cursorColor, promotionPieces[i].Symbol);
                    else
                        Square.Print(squareWidth, squareHeight, i % 2 == 0 ? whiteColor : blackColor, promotionPieces[i].Symbol);
                }
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursor > 0)
                            cursor--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursor < promotionPieces.Length - 1)
                            cursor++;
                        break;
                }
            }
            while (key != ConsoleKey.Spacebar);
            for (int i = 0; i < promotionPieces.Length; i++)
            {
                Console.SetCursorPosition(left, i * squareHeight + 6);
                Square.Print(squareWidth, squareHeight, ConsoleColor.Black);
            }
            return promotionPieces[cursor];
        }
    }
}
