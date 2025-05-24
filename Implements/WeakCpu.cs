using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Implements;

/// <summary>
/// 初級CPU
/// </summary>
internal class WeakCpu : IPlayer
{
    /// <inheritdoc/>
    public string Name => "CPU";

    /// <inheritdoc/>
    public CellValueType InputType => CellValueType.Cross;

    /// <inheritdoc/>
    public CellState DecidePlacement(Board board)
    {
        // 空白セルからランダムに一つセルを抽出する
        var randomNum = new Random().Next(board.EmptyCells.Count());
        return new CellState(
            board.EmptyCells.ElementAt(randomNum),
            this.InputType);
    }
}