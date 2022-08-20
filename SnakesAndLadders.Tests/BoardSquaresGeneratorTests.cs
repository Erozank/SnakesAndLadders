using NUnit.Framework;
using SnakesAndLadders.Models.SpecialBoardSquares;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadders.Tests
{
    [TestFixture]
    internal class BoardSquaresGeneratorTests
    {
        [Test]
        public void Generator_ReturnsExpected_BoardSquares()
        {
            // Arrange
            BoardSquaresGenerator boardSquaresGenerator = new();
            int boardSize = 100;
            int numSquares = 15;

            // Action
            Dictionary<int, IBoardSquare> squares = boardSquaresGenerator.Generate(boardSize, numSquares);
            List<Snake> snakes = squares.Values.OfType<Snake>().ToList();
            List<Ladder> ladder = squares.Values.OfType<Ladder>().ToList();

            // Assert
            Assert.AreEqual(numSquares, squares.Count);
            Assert.AreEqual(snakes.Count, snakes.Where(s => s.InitialPosition > s.FinalPosition).Count());
            Assert.AreEqual(ladder.Count, ladder.Where(s => s.InitialPosition < s.FinalPosition).Count());
        }
    }
}
