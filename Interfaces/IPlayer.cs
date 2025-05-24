using TicTacToe.Components;

namespace TicTacToe.Interfaces;

internal interface IPlayer
{
    CellState Place(IEnumerable<int> emptyCells);
}
