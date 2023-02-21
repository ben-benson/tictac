
namespace tictac
{

    public interface TicTacPlayer
    {

        public (int x, int y) play(TicTacSquare[,] board, WhiteOrBlack whiteOrBlack);

    }
}
