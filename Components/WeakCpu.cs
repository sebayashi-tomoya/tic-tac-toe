using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Components;

internal class WeakCpu : IPlayer
{
    public string Name => "CPU";

    public CellValueType InputType => CellValueType.Cross;

    public CellState Place(IEnumerable<int> emptyCells)
    {
        // 空白セルからランダムに一つセルを抽出する
        var randomNum = new Random().Next(emptyCells.Count());
        return new CellState(emptyCells.ElementAt(randomNum), TicTacToe.Enums.CellValueType.Cross);
    }
}