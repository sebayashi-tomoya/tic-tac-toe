using TicTacToe.Components;
using TicTacToe.Enums;

namespace TicTacToe.Interfaces;

internal interface IPlayer
{
    string Name { get; }
    CellValueType InputType { get; }
    CellState DecidePlacement(Board board);
}
