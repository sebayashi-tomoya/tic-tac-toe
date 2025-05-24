using TicTacToe.Enums;

namespace TicTacToe.Components;

/// <summary>
/// セルの状態を管理
/// </summary>
internal class CellState
{
    /// <summary>セルのインデックス</summary>
    /// <remarks>1始まり</remarks>
    internal int CellNumber;

    /// <summary>セルに入力されている値</summary>
    internal CellValueType ValueType;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    internal CellState(int cellNumber, CellValueType state)
    {
        this.CellNumber = cellNumber;
        this.ValueType = state;
    }

    /// <summary>
    /// 設定されている値を画面表示用の文字列で取得する
    /// </summary>
    /// <returns></returns>
    internal string GetSymbol()
    {
        return this.ValueType switch
        {
            CellValueType.Empty => this.CellNumber.ToString(),
            CellValueType.Circle => "○",
            CellValueType.Cross => "×",
            _ => string.Empty
        };
    }
}