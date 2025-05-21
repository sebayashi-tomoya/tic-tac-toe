using TicTacToe.Enums;

internal class CellState
{
    internal int CellNumber;

    internal CellValueType State;

    internal CellState(int cellNumber, CellValueType state)
    {
        this.CellNumber = cellNumber;
        this.State = state;
    }

    internal string GetSymbol()
    {
        return this.State switch
        {
            CellValueType.Empty => this.CellNumber.ToString(),
            CellValueType.Circle => "○",
            CellValueType.Cross => "×",
            _ => string.Empty
        };
    }
}