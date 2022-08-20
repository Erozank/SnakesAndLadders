namespace SnakesAndLadders.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Position { get; set; }

        public Player(string name)
        {
            Name = name;
            Position = 1;
        }
    }
}
