using System.Diagnostics;
using TicTacToe.Enums;

internal class Board
{
    /// <summary>
    /// 各セルの状態
    /// </summary>
    internal List<CellState> CellStates = [];

    internal Board()
    {
        this.InitializeStates();
    }

    internal void WriteBoard()
    {
        Console.Clear();

        // マス上辺の線
        Board.WriteRowLine();

        // 各セルの値
        for (int row = 1; row <= BoardSettings.BOARD_SIZE; row++)
        {
            for (int col = 1; col <= BoardSettings.BOARD_SIZE; col++)
            {
                Console.Write("|");

                // 行と列からセル番号を計算
                int cellNumber = (row - 1) * BoardSettings.BOARD_SIZE + col;
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
        for (int i = 1; i <= BoardSettings.CELL_COUNT; i++)
        {
            this.CellStates.Add(new CellState(i, CellValueType.Empty));
        }
    }

    private static void WriteRowLine()
    {
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }
}