using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Implements;
using TicTacToe.Interfaces;

internal class GameMaster
{
    #region フィールド、プロパティ

    private readonly Board Board;

    private CpuLevel selectedLevel;

    private IPlayer? firstPlayer;

    private IPlayer? secondPlayer;

    #endregion

    #region コンストラクタ

    internal GameMaster()
    {
        this.Board = new Board();
    }

    #endregion

    #region 外部公開メソッド

    /// <summary>
    /// 難易度の選択
    /// </summary>
    internal void SelectLevel()
    {
        // 難易度選択
        while (true)
        {
            Console.WriteLine("難易度を選んでください");
            Console.Write("【初級】0、【上級】1 : ");

            if (ValidateSelectedInt(Console.ReadLine(), out int intLevel))
            {
                var strLevel = CpuLevel.Weak.Equals(this.selectedLevel) ? "初級" : "上級";
                this.selectedLevel = (CpuLevel)intLevel;
                Console.Clear();
                Console.WriteLine($"{strLevel}が選択されました");
                break;
            }
            else
            {
                WriteErrorMessage();
            }
        }
    }

    /// <summary>
    /// 先攻後攻の選択
    /// </summary>
    internal void SetPlayers()
    {
        Console.WriteLine("先攻・後攻を選んで下さい");
        Console.Write("【先攻】0、【後攻】1 : ");

        bool isUserFirst;
        while (true)
        {
            if (ValidateSelectedInt(Console.ReadLine(), out int intTurn))
            {
                var strTurn = intTurn == 0 ? "先攻" : "後攻";
                isUserFirst = intTurn == 0;
                Console.WriteLine($"{strTurn}が選択されました");
                break;
            }
            else
            {
                WriteErrorMessage();
            }
        }

        IPlayer cpu = CpuLevel.Weak.Equals(this.selectedLevel) ?
            new WeakCpu() : new StrongCpu();
        if (isUserFirst)
        {
            this.firstPlayer = new Player();
            this.secondPlayer = cpu;
        }
        else
        {
            this.firstPlayer = cpu;
            this.secondPlayer = new Player();
        }
    }

    /// <summary>
    /// ゲームのメイン処理
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    internal void Start()
    {
        if (this.firstPlayer is null || this.secondPlayer is null)
        {
            throw new InvalidOperationException();
        }

        Console.WriteLine("ゲームを開始します!");
        Console.Write("Enterキーを押して下さい");
        _ = Console.ReadLine();

        // 初期盤面の表示
        this.Board.WriteBoard();

        TurnResult result = TurnResult.Continuation;
        while (true)
        {
            // 先攻プレイヤーのターン
            result = this.PlayTurn(firstPlayer);
            if (this.OnTurnFinished(result, firstPlayer)) break;

            // 後攻プレイヤーのターン
            result = this.PlayTurn(secondPlayer);
            if (this.OnTurnFinished(result, secondPlayer)) break;
        }

        Console.WriteLine("ゲーム終了です！");
    }

    #endregion

    #region privateメソッド

    /// <summary>
    /// 1ターン中の処理
    /// </summary>
    private TurnResult PlayTurn(IPlayer player)
    {
        var insertState = player.DecidePlacement(this.Board);
        this.Board.SetState(insertState);
        this.Board.WriteBoard();

        if (this.Board.CheckWinner(player))
        {
            return TurnResult.Win;
        }
        if (!this.Board.EmptyCells.Any())
        {
            return TurnResult.Draw;
        }

        return TurnResult.Continuation;
    }

    /// <summary>
    /// ターン終了後の処理
    /// </summary>
    /// <returns>引き分けまたは勝者がいればtrueを返す</returns>
    private bool OnTurnFinished(TurnResult result, IPlayer player)
    {
        if (TurnResult.Win.Equals(result))
        {
            Console.WriteLine($"{player.Name}の勝ちです");
            return true;
        }
        if (TurnResult.Draw.Equals(result))
        {
            Console.WriteLine("引き分けです");
            return true;
        }

        return false;
    }

    /// <summary>
    /// 0 or 1 の場合の入力値チェック
    /// </summary>
    private static bool ValidateSelectedInt(string? selectedLevel, out int intVal)
    {
        return int.TryParse(selectedLevel, out intVal) && (intVal == 0 || intVal == 1);
    }

    /// <summary>
    /// 入力エラー時の出力
    /// </summary>
    private static void WriteErrorMessage()
    {
        Console.WriteLine("入力エラーです");
        Console.WriteLine("半角で0または1を入力してください");
    }

    #endregion
}