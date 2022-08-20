using NUnit.Framework;
using SnakesAndLadders.Models;

namespace SnakesAndLadders.Tests
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        [Repeat(25)]
        public void Roll_Returns_NumberBetwen1And6()
        {
            // Arrange
            Dice dice = new();

            // Action
            int value = dice.Roll();

            // Assert
            Assert.GreaterOrEqual(value, 1);
            Assert.LessOrEqual(value, 6);
        }
    }
}