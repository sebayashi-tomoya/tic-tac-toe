using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Implements;

/// <summary>
/// 上級CPU
/// </summary>
internal class StrongCpu : IPlayer
{
    /// <inheritdoc/>
    public string Name => "CPU";

    /// <inheritdoc/>
    public CellValueType InputType => CellValueType.Cross;

    /// <inheritdoc/>
    public CellState DecidePlacement(Board board)
    {
        // リーチになっているセルがあれば優先的に埋める
        var reachCell = board.GetReachCellNum();
        if (reachCell is not null)
        {
            return new CellState((int)reachCell, this.InputType);
        }

        var a = (int)Math.Ceiling((double)board.CellCount / 2);

        // 真ん中が空いていれば優先的に埋める
        if (CellValueType.Empty.Equals(board.CenterCell?.ValueType))
        {
            return new CellState(board.CenterCell.CellNumber, this.InputType);
        }

        // 四隅が空いていれば優先的に埋める
        var emptyCorners = new List<int>();
        foreach (var cornerNum in board.CornerNumbers)
        {
            var target = board.CellStates.FirstOrDefault(x => x.CellNumber.Equals(cornerNum));
            if (target is not null && CellValueType.Empty.Equals(target.ValueType))
            {
                emptyCorners.Add(target.CellNumber);
            }
        }
        if (emptyCorners.Count > 0)
        {
            return new CellState(
                CreateRandomNum(emptyCorners),
                this.InputType);
        }

        // それ以外は空いてるセルからランダム
        return new CellState(
            CreateRandomNum(board.EmptyCells),
            this.InputType);
    }

    /// <summary>
    /// intのコレクションの数値の範囲でランダムに番号を生成
    /// </summary>
    private static int CreateRandomNum(IEnumerable<int> cellNumbers)
    {
        var randomNum = new Random().Next(cellNumbers.Count());
        return cellNumbers.ElementAt(randomNum);
    }
}