using TicTacToe.Enums;

namespace TicTacToe.Components;

internal class CellState
{
    internal int CellNumber;

    internal CellValueType ValueType;

    internal CellState(int cellNumber, CellValueType state)
    {
        this.CellNumber = cellNumber;
        this.ValueType = state;
    }

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