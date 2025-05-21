using System.Diagnostics;
using TicTacToe.Enums;

internal class Board
{
    private static readonly int VERTICAL_COUNT = 3;
    private static readonly int HORIZONTAL_COUNT = 3;

    /// <summary>
    /// 各セルの状態
    /// </summary>
    internal List<CellState> CellStates = [];

    /// <summary>
    /// 全セルの数
    /// </summary>
    private readonly int CellCount;

    internal Board()
    {
        this.CellCount = VERTICAL_COUNT * HORIZONTAL_COUNT;
        this.InitializeStates();
    }

    internal void WriteBoard()
    {
        Console.Clear();

        // マス上辺の線
        Board.WriteRowLine();

        // 各セルの値
        for (int row = 1; row <= VERTICAL_COUNT; row++)
        {
            for (int col = 1; col <= HORIZONTAL_COUNT; col++)
            {
                Console.Write("|");

                // 行と列からセル番号を計算
                int cellNumber = (row - 1) * HORIZONTAL_COUNT + col;
                // セル番号に該当するStateを表示用文字に変換
                var targetState = this.CellStates.First(x => x.CellNumber == cellNumber);
                Console.Write($" {targetState.GetSymbol()} ");
            }
            // 改行と行間のライン
            Console.WriteLine("|");
            Board.WriteRowLine();
        }
    }

    internal void SetState(CellState insertState)
    {
        this.CellStates.RemoveAll(x => x.CellNumber.Equals(insertState.CellNumber));
        this.CellStates.Add(insertState);
    }

    private void InitializeStates()
    {
        for (int i = 1; i <= this.CellCount; i++)
        {
            this.CellStates.Add(new CellState(i, CellValueType.Empty));
        }
    }

    private static void WriteRowLine()
    {
        for (int i = 0; i < HORIZONTAL_COUNT; i++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }
}