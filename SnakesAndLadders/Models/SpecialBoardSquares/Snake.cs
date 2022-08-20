namespace SnakesAndLadders.Models.SpecialBoardSquares
{
    public class Snake : IBoardSquare
    {
        public int InitialPosition { get; set; }
        public int FinalPosition { get; set; }

        public Snake(int initialPosition, int finalPosition)
        {
            if (initialPosition <= finalPosition)
            {
                throw new ArgumentException("Initial position should be greater than final position");
            }

            InitialPosition = initialPosition;
            FinalPosition = finalPosition;
        }
    }
}
