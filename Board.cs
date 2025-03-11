using Chess.Squares;
using Chess.Pieces;

namespace Chess
{
    internal class Board
    {
        readonly Square[,] board = new Square[8, 8];
        public readonly int squareWidth = 5;
        public readonly int squareHeight = 3;
        public Board()
        {
            Initialize(board);
            Reset();
        }
        private void Initialize(Square[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j] = new Square(i, j);
        }
        public void Reset()
        {
            for (int i = 0; i < board.GetLength(1); i++)
            {
                board[1, i].Contents = new Pawn(ChessColor.White);
                board[6, i].Contents = new Pawn(ChessColor.Black);
            }

            board[0, 0].Contents = new Rook(ChessColor.White);
            board[0, 7].Contents = new Rook(ChessColor.White);
            board[0, 1].Contents = new Knight(ChessColor.White);
            board[0, 6].Contents = new Knight(ChessColor.White);
            board[0, 2].Contents = new Bishop(ChessColor.White);
            board[0, 5].Contents = new Bishop(ChessColor.White);
            board[0, 3].Contents = new Queen(ChessColor.White);
            board[0, 4].Contents = new King(ChessColor.White);

            board[7, 0].Contents = new Rook(ChessColor.Black);
            board[7, 7].Contents = new Rook(ChessColor.Black);
            board[7, 1].Contents = new Knight(ChessColor.Black);
            board[7, 6].Contents = new Knight(ChessColor.Black);
            board[7, 2].Contents = new Bishop(ChessColor.Black);
            board[7, 5].Contents = new Bishop(ChessColor.Black);
            board[7, 3].Contents = new Queen(ChessColor.Black);
            board[7, 4].Contents = new King(ChessColor.Black);
        }
        public void Clear()
        {
            Initialize(board);
        }

        public Square this[int rank, int file]
        {
            get
            {
                return board[rank, file];
            }
        }
        public Square this[Rank rank, Squares.File file]
        {
            get
            {
                return board[(int)rank, (int)file];
            }
        }
        public int GetLength(int dimension)
        {
            return board.GetLength(dimension);
        }
        public Piece? GetPiece(Square square)
        {
            return board[(int)square.Rank, (int)square.File].Contents;
        }
        public bool IsEmptySquare(Square square)
        {
            return GetPiece(square) == null;
        }
        public bool IsEnemyPiece(Square square, ChessColor color)
        {
            Piece? piece = GetPiece(square);
            return piece != null && piece.Color != color;
        }
        public bool IsEmptyOrEnemyPiece(Square square, ChessColor color)
        {
            Piece? piece = GetPiece(square);
            if (piece == null) return true;
            return piece.Color != color;
        }
        public void Print(int squareWidth, int squareHeight, ConsoleColor whiteColor, ConsoleColor blackColor, ChessColor playerColor)
        {
            (int left, int top) = Console.GetCursorPosition();
            if (playerColor == ChessColor.White)
            {
                for (int i = board.GetLength(0) - 1; i >= 0; i--)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(j * squareWidth + left, (board.GetLength(0) - 1 - i) * squareHeight + top);
                        board[i, j].Print(squareWidth, squareHeight, whiteColor, blackColor);
                    }
                }
            }
            else
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.SetCursorPosition(j * squareWidth + left, i * squareHeight + top);
                        board[i, j].Print(squareWidth, squareHeight, whiteColor, blackColor);
                    }
                }
            }
            Console.SetCursorPosition(left, top);
        }
        public void Print(ChessColor playerColor)
        {
            Print(squareWidth, squareHeight, ConsoleColor.White, ConsoleColor.Gray, playerColor);
        }

        public Square KingPosition(ChessColor color)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (board[i, j].Contents is King && board[i, j].Contents.Color == color)
                        return board[i, j];
            return board[0, 0];
        }
        public bool Move(Square start, Square end)
        {
            Piece? piece = GetPiece(start);
            if (piece == null) return false;

            board[(int)end.Rank, (int)end.File].Contents = piece;
            piece.PieceMoved = true;
            board[(int)start.Rank, (int)start.File].Contents = null;
            if (piece is King && Math.Abs(start.File - end.File) == 2)
            {
                if (start.File > end.File)
                    return Move(new Square(start.Rank, Squares.File.A), new Square(start.Rank, end.File + 1));
                else return Move(new Square(start.Rank, Squares.File.H), new Square(start.Rank, end.File - 1));
            }

            return true;
        }
    }
}
