using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Implements;

internal class WeakCpu : IPlayer
{
    public string Name => "CPU";

    public CellValueType InputType => CellValueType.Cross;

    public CellState DecidePlacement(Board board)
    {
        // 空白セルからランダムに一つセルを抽出する
        var randomNum = new Random().Next(board.EmptyCells.Count());
        return new CellState(
            board.EmptyCells.ElementAt(randomNum),
            this.InputType);
    }
}