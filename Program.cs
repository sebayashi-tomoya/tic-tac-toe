
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var master = new GameMaster();
            master.Start();
        }
    }

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

            // 難易度選択
            while (true)
            {
                Console.WriteLine("難易度を選んでください");
                Console.Write("【初級】0、【上級】1 : ");
                var selectedLevel = Console.ReadLine();

                if (GameMaster.ValidateSelectedLevel(selectedLevel, out int intLevel))
                {
                    Console.WriteLine($"{ConvertLevelToString(intLevel)}が選択されました");
                    break;
                }
                else
                {
                    Console.WriteLine("入力エラーです");
                    Console.WriteLine("0または1を入力してください");
                }
            }

            Console.WriteLine("ゲームを開始します！Enterキーを押して下さい");
            _ = Console.ReadLine();

            // 初期盤面の表示
            this.Board.WriteBoard();
        }

        /// <summary>
        /// レベル選択の適正チェック
        /// </summary>
        private static bool ValidateSelectedLevel(string? selectedLevel, out int intVal)
        {
            return int.TryParse(selectedLevel, out intVal) && (intVal == 0 || intVal == 1);
        }

        private string ConvertLevelToString(int intLevel)
        {
            return intLevel == 0 ? "初級" : "上級";
        }
    }

    internal enum State
    {
        Empty,
        Circle,
        Cross,
    }

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
                    var targetState = this.CellStates.First(x => x.CellNumber == cellNumber);
                    Console.Write($" {targetState.ConvertStateToSymbol()} ");
                }
                // 改行と行間のライン
                Console.WriteLine("|");
                Board.WriteRowLine();
            }
        }

        private void InitializeStates()
        {
            for (int i = 1; i <= this.CellCount; i++)
            {
                this.CellStates.Add(new CellState(i, State.Empty));
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
}
