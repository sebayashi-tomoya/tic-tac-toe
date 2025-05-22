using TicTacToe.Enums;
using TicTacToe.Interfaces;

internal class GameMaster
{
    private readonly Board Board;

    internal GameMaster()
    {
        this.Board = new Board();
    }

    internal void Start()
    {

        Console.WriteLine("○×ゲームへようこそ！");

        IPlayer firstPlayer = new Player();
        IPlayer secondPlayer = new WeakCpu();

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

        Console.Write("ゲームを開始します！Enterキーを押して下さい");
        _ = Console.ReadLine();

        // 初期盤面の表示
        this.Board.WriteBoard();

        while (true)
        {
            // 先攻プレイヤーのターン
            if (this.PlayTurn(firstPlayer)) break;
            // 後攻プレイヤーのターン
            if (this.PlayTurn(secondPlayer)) break;
        }

        Console.Clear();
        Console.WriteLine("ゲーム終了です！");
    }

    private bool PlayTurn(IPlayer player)
    {
        this.Board.SetState(player);
        this.Board.WriteBoard();
        return !Board.EmptyCells.Any();
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