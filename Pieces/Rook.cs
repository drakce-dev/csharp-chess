using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(ChessColor color) : base(color, '♖', '♜') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            return LegalMoves(position, board, Color);
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = new List<Square>();

            for (Rank rank = position.Rank + 1; rank <= Rank.Rank8; rank++)
            {
                Square square = new Square(rank, position.File);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for (Rank rank = position.Rank - 1; rank >= Rank.Rank1; rank--)
            {
                Square square = new Square(rank, position.File);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for (Squares.File file = position.File + 1; file <= Squares.File.H; file++)
            {
                Square square = new Square(position.Rank, file);
                Piece? piece = board.GetPiece(square);
                if (piece == null) legalMoves.Add(square);
                else
                {
                    if (piece.Color != color)
                        legalMoves.Add(square);
                    break;
                }
            }

            for (Squares.File file = position.File - 1; file >= Squares.File.A; file--)
            {
                Square square = new Square(position.Rank, file);
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
