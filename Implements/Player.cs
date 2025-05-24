using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Implements;

internal class Player : IPlayer
{
    public string Name => "あなた";

    public CellValueType InputType => CellValueType.Circle;

    public CellState DecidePlacement(Board board)
    {
        while (true)
        {
            var startNum = board.CellCount - (board.CellCount - 1);
            Console.WriteLine($"{startNum} ~ {board.CellCount} のどこに置きますか？");

            if (ValidateInputCell(Console.ReadLine(), board, out int inputNum))
            {
                return new CellState(inputNum, this.InputType);
            }
            else
            {
                Console.WriteLine("入力エラーです");
                Console.WriteLine("半角で0~9を入力してください");
            }
        }
    }

    private static bool ValidateInputCell(string? inputNum, Board board, out int cellNum)
    {
        return int.TryParse(inputNum, out cellNum)
            && board.EmptyCells.Contains(cellNum)
            && cellNum <= board.CellCount
            && cellNum > 0;
    }
}