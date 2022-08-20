using SnakesAndLadders.Models;
using SnakesAndLadders.Models.SpecialBoardSquares;

namespace SnakesAndLadders
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public Dictionary<int, IBoardSquare> BoardSquares { get; private set; }
        public Player? Winner { get; private set; }
        public Player? PlayerTurn { get; private set; }
        public bool GameStarted { get; private set; }
        public bool GameFinished { get; private set; }

        private readonly IDice dice;
        private int turn = 0;
        private readonly int boardSize = 100;
        
        public Game() : this(new Dice(), new BoardSquaresGenerator())
        {
        }

        internal Game(IDice dice, IBoardSquaresGenerator boardSquaresGenerator)
        {
            GameStarted = false;
            GameFinished = false;
            this.dice = dice;
            Players = new List<Player>();
            BoardSquares = boardSquaresGenerator.Generate(boardSize, 15);
        }

        public bool AddPlayer(string name)
        {
            if (!GameStarted)
            {
                Players.Add(new Player(name));
                return true;
            }
            return false;
        }

        public bool StartGame()
        {
            if (Players.Count >= 2)
            {
                GameStarted = true;
                PlayerTurn = Players[turn];
                return true;
            }
            return false;
        }

        public MovementResult RollDice()
        {
            if(PlayerTurn == null)
            {
                throw new InvalidOperationException();
            }

            MovementResult result = new(PlayerTurn);

            if (GameStarted && !GameFinished)
            {
                int diceValue = dice.Roll();
                result.DiceValue = diceValue;

                if(PlayerTurn.Position + diceValue <= boardSize)
                {
                    result.InitialPosition = PlayerTurn.Position;
                    PlayerTurn.Position += diceValue;

                    if (BoardSquares.ContainsKey(PlayerTurn.Position))
                    {
                        IBoardSquare specialBoardSquare = BoardSquares[PlayerTurn.Position];
                        PlayerTurn.Position = specialBoardSquare.FinalPosition;
                        result.BoardSquare = specialBoardSquare;
                    }
                }

                if (PlayerTurn.Position == boardSize)
                {
                    GameFinished = true;
                    Winner = PlayerTurn;
                }

                turn++;
                PlayerTurn = Players[turn % Players.Count];
            }
            return result;
        }
    }
}