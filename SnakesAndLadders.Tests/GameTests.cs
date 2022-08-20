using Moq;
using NUnit.Framework;
using SnakesAndLadders.Models;
using SnakesAndLadders.Models.SpecialBoardSquares;
using System.Collections.Generic;

namespace SnakesAndLadders.Tests
{
    [TestFixture]
    internal class GameTests
    {
        [Test]
        public void StartAGame_PlayerInitialPositionShouldBe_1()
        {
            // Arrange
            Mock<IDice> dice = new();
            Mock<IBoardSquaresGenerator> boardSquaresGenerator = new();
            boardSquaresGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Dictionary<int, IBoardSquare>());

            Game game = new(dice.Object, boardSquaresGenerator.Object);
            game.AddPlayer("Pepe");
            game.AddPlayer("Manolo");

            // Action
            game.StartGame();

            // Assert
            Assert.AreEqual(2, game.Players.Count);
            Assert.AreEqual(1, game.Players[0].Position);
            Assert.AreEqual(1, game.Players[1].Position);
        }

        [TestCase(1, 3, 4)]
        [TestCase(4, 4, 8)]
        [TestCase(97, 4, 97)]
        public void RollDice_IncreasesPlayerPosition(int playerPosition, int diceValue, int finalPosition)
        {
            // Arrange
            Mock<IDice> dice = new();
            dice.Setup(x => x.Roll()).Returns(diceValue);
            Mock<IBoardSquaresGenerator> boardSquaresGenerator = new();
            boardSquaresGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Dictionary<int, IBoardSquare>());

            Game game = new(dice.Object, boardSquaresGenerator.Object);
            game.AddPlayer("Pepe");
            game.AddPlayer("Manolo");
            game.StartGame();

            Player? player = game.PlayerTurn;
            if (player != null)
            {
                player.Position = playerPosition;
            }

            // Action
            game.RollDice();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(finalPosition, player?.Position);
        }

        [Test]
        public void RollDice_IncreasesPlayerPositionToPosition100_PlayerWinsTheGame()
        {
            // Arrange
            Mock<IDice> dice = new();
            dice.Setup(x => x.Roll()).Returns(3);
            Mock<IBoardSquaresGenerator> boardSquaresGenerator = new();
            boardSquaresGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Dictionary<int, IBoardSquare>());

            Game game = new(dice.Object, boardSquaresGenerator.Object);
            game.AddPlayer("Pepe");
            game.AddPlayer("Manolo");
            game.StartGame();

            Player? player = game.PlayerTurn;
            if (player != null)
            {
                player.Position = 97;
            }

            // Action
            game.RollDice();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(100, player?.Position);
            Assert.AreEqual(true, game.GameFinished);
            Assert.AreEqual(player, game.Winner);
        }


        [Test]
        public void RollDice_PlayerStepsOnSnake_PlayerMovesToSnakeTail()
        {
            // Arrange
            Mock<IDice> dice = new();
            dice.Setup(x => x.Roll()).Returns(3);
            Mock<IBoardSquaresGenerator> boardSquaresGenerator = new();

            Snake snake = new(10, 3);
            Dictionary<int, IBoardSquare> boardSquares = new();
            boardSquares.Add(snake.InitialPosition, snake);

            boardSquaresGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(boardSquares);

            Game game = new(dice.Object, boardSquaresGenerator.Object);
            game.AddPlayer("Pepe");
            game.AddPlayer("Manolo");
            game.StartGame();

            Player? player = game.PlayerTurn;
            if (player != null)
            {
                player.Position = 7;
            }

            // Action
            game.RollDice();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(snake.FinalPosition, player?.Position);
        }

        [Test]
        public void RollDice_PlayerStepsOnLadder_PlayerMovesToTopOfTheLadder()
        {
            // Arrange
            Mock<IDice> dice = new();
            dice.Setup(x => x.Roll()).Returns(3);
            Mock<IBoardSquaresGenerator> boardSquaresGenerator = new();

            Ladder ladder = new(10, 25);
            Dictionary<int, IBoardSquare> boardSquares = new();
            boardSquares.Add(ladder.InitialPosition, ladder);

            boardSquaresGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(boardSquares);

            Game game = new(dice.Object, boardSquaresGenerator.Object);
            game.AddPlayer("Pepe");
            game.AddPlayer("Manolo");
            game.StartGame();

            Player? player = game.PlayerTurn;
            if (player != null)
                player.Position = 7;

            // Action
            game.RollDice();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(ladder.FinalPosition, player?.Position);
        }
    }
}
