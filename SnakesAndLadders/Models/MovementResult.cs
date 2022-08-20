using SnakesAndLadders.Models.SpecialBoardSquares;

namespace SnakesAndLadders.Models
{
    public class MovementResult
    {
        public int InitialPosition { get; set; }
        public int DiceValue { get; set; }
        public IBoardSquare? BoardSquare { get; set; }
        public Player Player { get; set; }

        public MovementResult(Player player)
        {
            Player = player;
        }
    }
}
