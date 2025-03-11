using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Squares;

namespace Chess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(ChessColor color) : base(color, '♕', '♛') { }

        public override List<Square> LegalMoves(Square position, Board board)
        {
            return LegalMoves(position, board, Color);
        }
        public static List<Square> LegalMoves(Square position, Board board, ChessColor color)
        {
            List<Square> legalMoves = Rook.LegalMoves(position, board, color);
            legalMoves.AddRange(Bishop.LegalMoves(position, board, color));
            return legalMoves;
        }
    }
}
