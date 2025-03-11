using Chess.Squares;

namespace Chess.Pieces
{
    internal abstract class Piece
    {
        public readonly ChessColor Color;
        public readonly char Symbol;
        public bool PieceMoved = false;
        public Piece(ChessColor color, char whiteSybmol, char blackSymbol)
        {
            Color = color;
            Symbol = whiteSybmol;
            if (Color == ChessColor.Black)
                Symbol = blackSymbol;
        }
        public abstract IEnumerable<Square> LegalMoves(Square position, Board board);
        public static bool IsOppositeColor(Piece a, Piece b)
        {
            return !a.Color.Equals(b.Color);
        }
    }
}
