namespace SnakesAndLadders.Models.SpecialBoardSquares
{
    public interface IBoardSquare
    {
        int InitialPosition { get; set; }
        int FinalPosition { get; set; }
    }
}
