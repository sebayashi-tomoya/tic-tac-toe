using TicTacToe.Components;
using TicTacToe.Enums;

namespace TicTacToe.Interfaces;

internal interface IPlayer
{
    string Name { get; }
    CellValueType InputType { get; }
    CellState Place(IEnumerable<int> emptyCells);
}
