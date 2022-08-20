namespace SnakesAndLadders.Models.SpecialBoardSquares
{
    public class Ladder : IBoardSquare
    {
        public int InitialPosition { get; set; }
        public int FinalPosition { get; set; }

        public Ladder(int initialPosition, int finalPosition)
        {
            if (finalPosition <= initialPosition)
            {
                throw new ArgumentException("Final position should be greater than initial position");
            }

            InitialPosition = initialPosition;
            FinalPosition = finalPosition;
        }
    }
}
