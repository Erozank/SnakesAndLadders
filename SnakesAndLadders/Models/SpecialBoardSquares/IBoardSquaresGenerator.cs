using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesAndLadders.Models.SpecialBoardSquares
{
    internal interface IBoardSquaresGenerator
    {
        Dictionary<int, IBoardSquare> Generate(int boardSize, int specialSquares);
    }
}
