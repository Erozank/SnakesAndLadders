namespace SnakesAndLadders.Models.SpecialBoardSquares
{
    internal class BoardSquaresGenerator : IBoardSquaresGenerator
    {
        public Dictionary<int, IBoardSquare> Generate(int boardSize, int specialSquares)
        {
            Dictionary<int, IBoardSquare> boardSquares = new();
            List<int> availableSquares = GetAvailableSquares(boardSize);

            for (int i = 0; i < specialSquares; i++)
            {
                int initialPosition = GetAvailableSquare(availableSquares);
                int finalPosition = GetAvailableSquare(availableSquares);

                IBoardSquare boardSquare = GenerateBoardSquare(initialPosition, finalPosition);
                boardSquares.Add(initialPosition, boardSquare);
            }

            return boardSquares;
        }

        private List<int> GetAvailableSquares(int boardSize)
        {
            List<int> availableSquares = new();
            for (int i = 2; i < boardSize; i++)
            {
                availableSquares.Add(i);
            }

            return availableSquares;
        }

        private int GetAvailableSquare(List<int> availableSquares)
        {
            Random random = new();
            int index = random.Next(availableSquares.Count);
            int availableSquare = availableSquares[index];
            availableSquares.RemoveAt(index);

            return availableSquare;
        }

        private IBoardSquare GenerateBoardSquare(int initialPosition, int finalPosition)
        {
            IBoardSquare specialBoardSquare;

            if (initialPosition < finalPosition)
            {
                specialBoardSquare = new Ladder(initialPosition, finalPosition);
            }
            else
            {
                specialBoardSquare = new Snake(initialPosition, finalPosition);
            }

            return specialBoardSquare;
        }
    }
}
