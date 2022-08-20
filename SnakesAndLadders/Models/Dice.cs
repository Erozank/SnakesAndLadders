namespace SnakesAndLadders.Models
{
    internal class Dice : IDice
    {
        private readonly Random random;

        public Dice()
        {
            random = new Random();
        }

        public int Roll()
        {
            return random.Next(1, 7);
        }
    }
}
