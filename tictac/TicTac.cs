
namespace tictac
{

    class TicTac
    {
        public static void Main(String[] args)
        {

            TicTacSession session = new TicTacSession();

            Console.WriteLine("Let's play Tic Tac Toe!");


            TicTacPlayer p1 = new TicTacPlayerHuman();
            TicTacPlayer p2 = new TicTacPlayerHuman();

            session.setPlayerOne(p1, "Bob");
            session.setPlayerTwo(p2, "Sally");
            session.playMatch();
        }
    }

}
