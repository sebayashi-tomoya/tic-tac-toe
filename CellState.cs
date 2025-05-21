using TicTacToe.Enums;

internal class CellState
{
    internal int CellNumber;

    internal State State;

    internal CellState(int cellNumber, State state)
    {
        this.CellNumber = cellNumber;
        this.State = state;
    }

    internal string ConvertStateToSymbol()
    {
        return this.State switch
        {
            State.Empty => this.CellNumber.ToString(),
            State.Circle => "○",
            State.Cross => "×",
            _ => string.Empty
        };
    }
}