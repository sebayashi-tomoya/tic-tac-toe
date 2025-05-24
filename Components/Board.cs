using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Components;

internal class Board
{
    #region フィールド、プロパティ

    /// <summary>各セルの状態</summary>
    internal List<CellState> CellStates = [];

    /// <summary>空白セルのセル番号リスト</summary>
    private IEnumerable<int> EmptyCells => this.CellStates
        .Where(x => CellValueType.Empty.Equals(x.ValueType))
        .Select(x => x.CellNumber);

    /// <summary>勝ちパターン</summary>
    private readonly List<int[]> winPatterns = [];

    #endregion

    #region コンストラクタ

    internal Board()
    {
        this.InitializeStates();
        this.InitializeWinPatterns();
    }

    #endregion

    #region 外部公開メソッド

    /// <summary>
    /// 盤面を表示する
    /// </summary>
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

    /// <summary>
    /// 1ターンの処理
    /// </summary>
    internal TurnResult PlayTurn(IPlayer player)
    {
        this.SetState(player);
        this.WriteBoard();

        if (this.CheckWinner(player))
        {
            return TurnResult.Win;
        }
        if (!this.EmptyCells.Any())
        {
            return TurnResult.Draw;
        }

        return TurnResult.Continuation;
    }

    #endregion

    #region privateメソッド

    private void SetState(IPlayer player)
    {
        var insertState = player.Place(this.EmptyCells);
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

    private void InitializeWinPatterns()
    {
        // 横・縦のパターン
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            var rowWinPattern = new int[BoardSettings.BOARD_SIZE];
            var colWinPattern = new int[BoardSettings.BOARD_SIZE];

            for (int j = 0; j < BoardSettings.BOARD_SIZE; j++)
            {
                rowWinPattern[j] = this.ConvertToCellNum(i, j);
                colWinPattern[j] = this.ConvertToCellNum(j, i);
            }
            this.winPatterns.Add(rowWinPattern);
            this.winPatterns.Add(colWinPattern);
        }

        // 左上から右下への斜め
        var mainDiagonal = new int[BoardSettings.BOARD_SIZE];
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            mainDiagonal[i] = this.ConvertToCellNum(i, i);
        }
        this.winPatterns.Add(mainDiagonal);

        // 右上から左下への斜め
        var antiDiagonal = new int[BoardSettings.BOARD_SIZE];
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            antiDiagonal[i] = this.ConvertToCellNum(i, BoardSettings.BOARD_SIZE - 1 - i);
        }
        this.winPatterns.Add(antiDiagonal);
    }

    private bool CheckWinner(IPlayer player)
    {
        foreach (var pattern in this.winPatterns)
        {
            var targetCellStates = new List<CellState>();
            foreach (var cellNum in pattern)
            {
                targetCellStates.Add(
                    this.CellStates.First(x => x.CellNumber.Equals(cellNum))
                );
            }

            var inputtedCells = targetCellStates.Where(x => x.ValueType.Equals(player.InputType));
            if (inputtedCells.Count() == BoardSettings.BOARD_SIZE) return true;
        }

        return false;
    }

    /// <summary>
    /// 2次元座標からセル番号に変換
    /// </summary>
    private int ConvertToCellNum(int row, int col)
    {
        return row * BoardSettings.BOARD_SIZE + col + 1;
    }

    private static void WriteRowLine()
    {
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }

    #endregion
}