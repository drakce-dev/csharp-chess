using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class Knight : Piece
    {
        public Knight(ChessColor color) : base(color, '♘', '♞') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            return LegalMoves(position, board, Color);
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = new List<Square>();

            AddLegalMove(position.Rank + 2, position.File + 1, board, color, legalMoves);
            AddLegalMove(position.Rank + 2, position.File - 1, board, color, legalMoves);
            AddLegalMove(position.Rank - 2, position.File + 1, board, color, legalMoves);
            AddLegalMove(position.Rank - 2, position.File - 1, board, color, legalMoves);

            AddLegalMove(position.Rank + 1, position.File + 2, board, color, legalMoves);
            AddLegalMove(position.Rank + 1, position.File - 2, board, color, legalMoves);
            AddLegalMove(position.Rank - 1, position.File + 2, board, color, legalMoves);
            AddLegalMove(position.Rank - 1, position.File - 2, board, color, legalMoves);

            return legalMoves;
        }

        private static void AddLegalMove(Rank rank, Squares.File file, Board board, ChessColor color, List<Square> legalMoves)
        {
            if (rank >= Rank.Rank1 && rank <= Rank.Rank8 && file >= Squares.File.A && file <= Squares.File.H &&
                board.IsEmptyOrEnemyPiece(new Square(rank, file), color))
            {
                legalMoves.Add(new Square(rank, file));
            }
        }
    }
}
