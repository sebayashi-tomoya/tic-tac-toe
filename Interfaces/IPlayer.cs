using TicTacToe.Components;
using TicTacToe.Enums;

namespace TicTacToe.Interfaces;

/// <summary>
/// プレイヤーのインターフェース
/// </summary>
internal interface IPlayer
{
    /// <summary>プレイヤー名</summary>
    string Name { get; }

    /// <summary>入力する文字のタイプ</summary>
    CellValueType InputType { get; }

    /// <summary>配置位置を決める</summary>
    CellState DecidePlacement(Board board);
}
