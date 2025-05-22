
namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var master = new GameMaster();
            Console.WriteLine("○×ゲームへようこそ！");
            master.SelectLevel();
            master.SetPlayers();
            master.Start();
        }
    }
}
