using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class Pawn : Piece
    {
        // public new readonly ChessColor Color;
        public Pawn(ChessColor color) : base(color, '♙', '♟') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            return LegalMoves(position, board, Color);
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = new List<Square>();
            if (color == ChessColor.White)
            {
                if (position.Rank != Rank.Rank8)
                {
                    if (board.IsEmptySquare(new Square(position.Rank + 1, position.File)))
                    {
                        legalMoves.Add(new Square(position.Rank + 1, position.File));
                        if (position.Rank == Rank.Rank2 && board.IsEmptySquare(new Square(position.Rank + 2, position.File)))
                            legalMoves.Add(new Square(position.Rank + 2, position.File));
                    }

                    if (position.File != Squares.File.A &&
                        board.IsEnemyPiece(new Square(position.Rank + 1, position.File - 1), color))
                        legalMoves.Add(new Square(position.Rank + 1, position.File - 1));

                    if (position.File != Squares.File.H &&
                        board.IsEnemyPiece(new Square(position.Rank + 1, position.File + 1), color))
                        legalMoves.Add(new Square(position.Rank + 1, position.File + 1));
                }
            }
            else if (position.Rank != Rank.Rank1)
            {
                if (board.IsEmptySquare(new Square(position.Rank - 1, position.File)))
                {
                    legalMoves.Add(new Square(position.Rank - 1, position.File));
                    if (position.Rank == Rank.Rank7 && board.IsEmptySquare(new Square(position.Rank - 2, position.File)))
                        legalMoves.Add(new Square(position.Rank - 2, position.File));
                }

                if (position.File != Squares.File.A &&
                        board.IsEnemyPiece(new Square(position.Rank - 1, position.File - 1), color))
                    legalMoves.Add(new Square(position.Rank - 1, position.File - 1));

                if (position.File != Squares.File.H &&
                    board.IsEnemyPiece(new Square(position.Rank - 1, position.File + 1), color))
                    legalMoves.Add(new Square(position.Rank - 1, position.File + 1));
            }

            return legalMoves;
        }
    }
}
