using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Components;

internal class Board
{
    #region 定数

    /// <summary>盤面の一辺の長さ</summary>
    /// <remarks>正方形であることが前提のため縦横同じサイズとする</remarks>
    private static readonly int BOARD_SIZE = 3;

    #endregion

    #region フィールド、プロパティ

    /// <summary>各セルの状態</summary>
    internal List<CellState> CellStates = [];

    /// <summary>空白セルのセル番号リスト</summary>
    internal IEnumerable<int> EmptyCells => this.CellStates
        .Where(x => CellValueType.Empty.Equals(x.ValueType))
        .Select(x => x.CellNumber);

    /// <summary>全セルの数</summary>
    internal readonly int CellCount = BOARD_SIZE * BOARD_SIZE;

    /// <summary>四隅のセル番号リスト</summary>
    internal readonly int[] CornerNumbers =
    [
        1,
        BOARD_SIZE,
        (BOARD_SIZE -1) * BOARD_SIZE + 1,
        BOARD_SIZE * BOARD_SIZE,
    ];

    /// <summary>真ん中のセル番号</summary>
    internal CellState CenterCell => this.CellStates.First(x =>
        x.CellNumber.Equals((int)Math.Ceiling((double)this.CellCount / 2)));

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
        for (int row = 1; row <= BOARD_SIZE; row++)
        {
            for (int col = 1; col <= BOARD_SIZE; col++)
            {
                Console.Write("|");

                // 行と列からセル番号を計算
                int cellNumber = (row - 1) * BOARD_SIZE + col;
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
    /// セル状態の更新
    /// </summary>
    internal void SetState(CellState insertState)
    {
        this.CellStates.RemoveAll(x => x.CellNumber.Equals(insertState.CellNumber));
        this.CellStates.Add(insertState);
    }

    /// <summary>
    /// 勝者判定
    /// </summary>
    internal bool CheckWinner(IPlayer player)
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
            if (inputtedCells.Count() == BOARD_SIZE) return true;
        }

        return false;
    }

    internal int? GetReachCellNum()
    {
        foreach (var pattern in this.winPatterns)
        {
            List<CellState> winCellStates = [];
            foreach (var cellNum in pattern)
            {
                winCellStates.Add(
                    this.CellStates.First(x => x.CellNumber == cellNum));
            }

            // リーチならセル番号を返す
            var circle = winCellStates.Where(x => CellValueType.Circle.Equals(x.ValueType));
            var cross = winCellStates.Where(x => CellValueType.Cross.Equals(x.ValueType));

            var reachCount = BOARD_SIZE - 1;
            if (circle.Count() == reachCount || cross.Count() == reachCount)
            {
                var lastOneCell = winCellStates
                    .FirstOrDefault(x => CellValueType.Empty.Equals(x.ValueType));

                // 残り1セルが空であればセル番号を返す
                // 空でなければ検索を継続
                if (lastOneCell is not null) return lastOneCell.CellNumber;
            }
        }

        return null;
    }


    #endregion

    #region privateメソッド

    /// <summary>
    /// セル状態の初期化
    /// </summary>
    private void InitializeStates()
    {
        for (int i = 1; i <= CellCount; i++)
        {
            this.CellStates.Add(new CellState(i, CellValueType.Empty));
        }
    }

    /// <summary>
    /// 勝ちパターンの初期化
    /// </summary>
    private void InitializeWinPatterns()
    {
        // 横・縦のパターン
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            var rowWinPattern = new int[BOARD_SIZE];
            var colWinPattern = new int[BOARD_SIZE];

            for (int j = 0; j < BOARD_SIZE; j++)
            {
                rowWinPattern[j] = this.ConvertToCellNum(i, j);
                colWinPattern[j] = this.ConvertToCellNum(j, i);
            }
            this.winPatterns.Add(rowWinPattern);
            this.winPatterns.Add(colWinPattern);
        }

        // 左上から右下への斜め
        var mainDiagonal = new int[BOARD_SIZE];
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            mainDiagonal[i] = this.ConvertToCellNum(i, i);
        }
        this.winPatterns.Add(mainDiagonal);

        // 右上から左下への斜め
        var antiDiagonal = new int[BOARD_SIZE];
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            antiDiagonal[i] = this.ConvertToCellNum(i, BOARD_SIZE - 1 - i);
        }
        this.winPatterns.Add(antiDiagonal);
    }

    /// <summary>
    /// 2次元座標からセル番号に変換
    /// </summary>
    private int ConvertToCellNum(int row, int col)
    {
        return row * BOARD_SIZE + col + 1;
    }

    private static void WriteRowLine()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }

    #endregion
}