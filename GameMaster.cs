using TicTacToe.Components;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

internal class GameMaster
{
    private readonly Board Board;

    private IPlayer? firstPlayer;

    private IPlayer? secondPlayer;

    internal GameMaster()
    {
        this.Board = new Board();
    }

    internal void SelectLevel()
    {
        // 難易度選択
        while (true)
        {
            Console.WriteLine("難易度を選んでください");
            Console.Write("【初級】0、【上級】1 : ");

            if (ValidateSelectedLevel(Console.ReadLine(), out int intLevel))
            {
                Console.WriteLine($"{ConvertLevelToString(intLevel)}が選択されました");
                break;
            }
            else
            {
                Console.WriteLine("入力エラーです");
                Console.WriteLine("半角で0または1を入力してください");
            }
        }
    }

    internal void SetPlayers()
    {
        this.firstPlayer = new Player();
        this.secondPlayer = new WeakCpu();
    }

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
            result = this.Board.PlayTurn(firstPlayer);
            if (this.OnTurnFinished(result, firstPlayer)) break;

            // 後攻プレイヤーのターン
            result = this.Board.PlayTurn(secondPlayer);
            if (this.OnTurnFinished(result, secondPlayer)) break;
        }

        Console.WriteLine("ゲーム終了です！");
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
    /// レベル選択の適正チェック
    /// </summary>
    private static bool ValidateSelectedLevel(string? selectedLevel, out int intVal)
    {
        return int.TryParse(selectedLevel, out intVal) && (intVal == 0 || intVal == 1);
    }

    private static string ConvertLevelToString(int intLevel)
    {
        return intLevel == 0 ? "初級" : "上級";
    }
}