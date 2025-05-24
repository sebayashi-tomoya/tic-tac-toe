using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Components;

internal class Player : IPlayer
{
    public CellState Place(IEnumerable<int> emptyCells)
    {
        while (true)
        {
            var startNum = BoardSettings.CELL_COUNT - (BoardSettings.CELL_COUNT - 1);
            Console.WriteLine($"{startNum} ~ {BoardSettings.CELL_COUNT} のどこに置きますか？");

            if (ValidateInputCell(Console.ReadLine(), out int inputNum))
            {
                return new CellState(inputNum, CellValueType.Circle);
            }
            else
            {
                Console.WriteLine("入力エラーです");
                Console.WriteLine("半角で0~9を入力してください");
            }
        }
    }

    private static bool ValidateInputCell(string? inputNum, out int cellNum)
    {
        return int.TryParse(inputNum, out cellNum) && cellNum <= BoardSettings.CELL_COUNT && cellNum > 0;
    }
}