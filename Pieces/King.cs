using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        public King(ChessColor color) : base(color, '♔', '♚') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            List<Square> legalMoves  = LegalMoves(position, board, Color);

            if (!PieceMoved)
            {
                Piece? rookA = board.GetPiece(new Square(position.Rank, Squares.File.A));
                Piece? rookH = board.GetPiece(new Square(position.Rank, Squares.File.H));
                if (rookA != null && !rookA.PieceMoved && board.IsEmptySquare(new Square(position.Rank, Squares.File.B)) && board.IsEmptySquare(new Square(position.Rank, Squares.File.D)))
                    AddLegalMove(Squares.File.C, position.Rank, board, Color, legalMoves);
                if (rookH != null && !rookH.PieceMoved && board.IsEmptySquare(new Square(position.Rank, Squares.File.F)))
                    AddLegalMove(Squares.File.G, position.Rank, board, Color, legalMoves);
            }
            
            return legalMoves;
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = new List<Square>();

            AddLegalMove(position.File - 1, position.Rank + 1, board, color, legalMoves);
            AddLegalMove(position.File, position.Rank + 1, board, color, legalMoves);
            AddLegalMove(position.File + 1, position.Rank + 1, board, color, legalMoves);
            AddLegalMove(position.File - 1, position.Rank, board, color, legalMoves);
            AddLegalMove(position.File + 1, position.Rank, board, color, legalMoves);
            AddLegalMove(position.File - 1, position.Rank - 1, board, color, legalMoves);
            AddLegalMove(position.File, position.Rank - 1, board, color, legalMoves);
            AddLegalMove(position.File + 1, position.Rank - 1, board, color, legalMoves);

            return legalMoves;
        }

        private static void AddLegalMove(Squares.File file, Rank rank, Board board, ChessColor color, List<Square> legalMoves)
        {
            if (file >= Squares.File.A && file <= Squares.File.H && rank >= Rank.Rank1 && rank <= Rank.Rank8 &&
                board.IsEmptyOrEnemyPiece(new Square(rank, file), color))
            {
                legalMoves.Add(new Square(rank, file));
            }
        }
    }
}
