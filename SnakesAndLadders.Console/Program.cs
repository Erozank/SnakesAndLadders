using SnakesAndLadders;
using SnakesAndLadders.Models.SpecialBoardSquares;

Game game = new();

Console.WriteLine("Snakes and Ladders!");

while (!game.GameStarted)
{
    StartMenu(game);
}
PrintBoard(game);

if(game.PlayerTurn != null)
{
    while (!game.GameFinished)
    {
        PrintPlayersPositions(game);
        Console.WriteLine($"{game.PlayerTurn.Name} it's your turn");
        Console.WriteLine("Press enter to roll the dice");
        Console.ReadLine();
        var movementResult = game.RollDice();
        Console.WriteLine($"{movementResult.Player.Name} got a {movementResult.DiceValue}!");
        if (movementResult.BoardSquare != null)
        {
            string squareType = string.Empty;
            if (movementResult.BoardSquare is Snake)
            {
                squareType = "snake";
            }
            else if (movementResult.BoardSquare is Ladder)
            {
                squareType = "ladder";
            }
            Console.WriteLine($"There is a {squareType}! You go from {movementResult.BoardSquare.InitialPosition} to {movementResult.BoardSquare.FinalPosition}");
        }
    }
}

PrintPlayersPositions(game);
if(game.Winner != null)
{
    Console.WriteLine($"{game.Winner.Name} WINS!");
}

void PrintStartMenu()
{
    Console.WriteLine("1) Add player");
    Console.WriteLine("2) Start game");
}

void StartMenu(Game game)
{
    PrintStartMenu();
    string? option = Console.ReadLine();
    if (option == "1")
    {
        Console.WriteLine("Player name:");
        string? playerName = Console.ReadLine();
        if (!string.IsNullOrEmpty(playerName))
        {
            game.AddPlayer(playerName);
        }
        else
        {
            Console.WriteLine("Invalid name");
        }
    }
    else if (option == "2")
    {
        game.StartGame();
        if (!game.GameStarted)
        {
            Console.WriteLine("Add more players to play");
        }
    }
    else
    {
        Console.WriteLine("Invalid option");
    }
}

void PrintBoard(Game game)
{
    int boardNumber = 100;

    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            string boardSquare = $"| {boardNumber}";

            if (game.BoardSquares.ContainsKey(boardNumber))
            {
                IBoardSquare specialBoardSquare = game.BoardSquares[boardNumber];
                if (specialBoardSquare is Snake)
                {
                    boardSquare += $" S->{specialBoardSquare.FinalPosition}";
                }
                else if (specialBoardSquare is Ladder)
                {
                    boardSquare += $" L->{specialBoardSquare.FinalPosition}";
                }
            }

            int length = boardSquare.Length;
            string spaces = new(' ', 11 - length);
            boardSquare += spaces;

            if (i % 2 == 0)
            {
                boardNumber--;
            }
            else
            {
                boardNumber++;
            }

            Console.Write(boardSquare);
        }
        Console.WriteLine("|");
        if (i % 2 == 0)
        {
            boardNumber -= 9;
        }
        else
        {
            boardNumber -= 11;
        }
    }
}

void PrintPlayersPositions(Game game)
{
    foreach (var player in game.Players)
    {
        Console.WriteLine($"{player.Name}: {player.Position}");
    }
    Console.WriteLine();
}