using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(ChessColor color) : base(color, '♗', '♝') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            return LegalMoves(position, board, Color);
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = new List<Square>();

            for ((Rank rank, Squares.File file) = (position.Rank + 1, position.File + 1); rank <= Rank.Rank8 && file <= Squares.File.H; rank++, file++)
            {
                Square square = new Square(rank, file);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for ((Rank rank, Squares.File file) = (position.Rank - 1, position.File + 1); rank >= Rank.Rank1 && file <= Squares.File.H; rank--, file++)
            {
                Square square = new Square(rank, file);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for ((Rank rank, Squares.File file) = (position.Rank + 1, position.File - 1); rank <= Rank.Rank8 && file >= Squares.File.A; rank++, file--)
            {
                Square square = new Square(rank, file);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for ((Rank rank, Squares.File file) = (position.Rank - 1, position.File - 1); rank >= Rank.Rank1 && file >= Squares.File.A; rank--, file--)
            {
                Square square = new Square(rank, file);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            return legalMoves;
        }
    }
}
