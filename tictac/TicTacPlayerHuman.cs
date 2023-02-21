
namespace tictac
{

    class TicTacPlayerHuman : TicTacPlayer
    {

        public (int x, int y) play(TicTacSquare[,] board, WhiteOrBlack whiteOrBlack)
        {
            Console.Write("    Enter Coordinate (row,col): ");
            string g = Console.ReadLine();

            int row = Int32.Parse(g.Substring(0, 1));
            int col = Int32.Parse(g.Substring(2, 1));

            Console.WriteLine("    Playing " + row + "," + col);

            return (row, col);
        }


    }

}

