using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace tictac
{

    public class TicTacSession
    {
        Random random = new Random();
        TicTacPlayer playerOne = new TicTacPlayerHuman();
        TicTacPlayer playerTwo = new TicTacPlayerHuman();
        string playerOneName = "Player One";
        string playerTwoName = "Player Two";
        int matchGames = 1;
        int playedGames = 0;
        int playerOneWins = 0;
        int playerTwoWins = 0;
        int ties = 0;
        TicTacSquare[,] board = createNewBoard();


        public TicTacSession()
        {
        }

        public void setPlayerOne(TicTacPlayer p, string name)
        {
            this.playerOne = p;
            this.playerOneName = name;
        }

        public void setPlayerTwo(TicTacPlayer p, string name)
        {
            this.playerTwo = p;
            this.playerTwoName = name;
        }

        public void setMatchGames(int g)
        {
            this.matchGames = g;
        }

        public int getPlayerOneWins()
        {
            return this.playerOneWins;
        }

        public int getPlayerTwoWins()
        {
            return this.playerTwoWins;
        }

        public bool isMatchOver()
        {
            if (this.playedGames < this.matchGames)
            {
                return false;
            }
            return true;
        }


        public void playMatch()
        {

            while (this.playedGames < matchGames)
            {
                if (this.playedGames % 2 == 0)
                {
                    playGame(1);
                }
                else
                {
                    playGame(2);
                }

            }

            // match over, show stats for the match
            printMatchStats();
        }


        private void playGame(int startingPlayer)
        {
            // assign color to player
            WhiteOrBlack playerOneColor = WhiteOrBlack.white;
            WhiteOrBlack playerTwoColor = WhiteOrBlack.black;

            if (startingPlayer == 2)
            {
                playerOneColor = WhiteOrBlack.black;
                playerTwoColor = WhiteOrBlack.white;
            }


            int player = 2;
            WhiteOrBlack playerColor = playerTwoColor;
            string playerName;


            while (true)
            {
                // alternate player
                if (player == 2)
                {
                    player = 1;
                    playerColor = playerOneColor;
                    playerName = this.playerOneName;
                }
                else
                {
                    player = 2;
                    playerColor = playerTwoColor;
                    playerName = this.playerTwoName;
                }

                Console.WriteLine("Player " + playerName + " moves:");

                int row = -1;
                int col = -1;

                if (player == 1)
                {
                    (row, col) = this.playerOne.play(this.board, playerOneColor);
                    if (!markBoard(row, col, playerOneColor))
                    {
                        Console.WriteLine("    invalid play, you lose!");
                        if (player == 1)
                        {
                            this.playerTwoWins++;
                        }
                        else
                        {
                            this.playerOneWins++;
                        }
                        break;
                    }
                }
                else
                {
                    (row, col) = this.playerTwo.play(this.board, playerTwoColor);
                    if (!markBoard(row, col, playerTwoColor))
                    {
                        Console.WriteLine("    invalid play, you lose!");
                        if (player == 1)
                        {
                            this.playerTwoWins++;
                        }
                        else
                        {
                            this.playerOneWins++;
                        }
                        break;
                    }
                }



                printBoard();
                Console.WriteLine();


                // GAME OVER?
                TicTacGameStatus status = getBoardStatus();
                if (status == TicTacGameStatus.white)
                {
                    if (playerOneColor == WhiteOrBlack.white)
                    {
                        playerOneWins++;
                    }
                    else
                    {
                        playerTwoWins++;
                    }
                    break;
                }
                else if (status == TicTacGameStatus.black)
                {
                    if (playerOneColor == WhiteOrBlack.black)
                    {
                        playerOneWins++;
                    }
                    else
                    {
                        playerTwoWins++;
                    }
                    break;
                }
                else if (status == TicTacGameStatus.tie)
                {
                    ties++;
                    break;
                }

                // no one has won, continue loop
            }


            // tally win
            this.playedGames++;
        }



        public bool markBoard(int row, int col, WhiteOrBlack color)
        {
            int rows = this.board.GetLength(0);
            int cols = this.board.GetLength(1);
            if (row < 0 || row >= rows)
            {
                return false;
            }
            if (col < 0 || col >= cols)
            {
                return false;
            }

            TicTacSquare t = board[row, col];
            if (t != TicTacSquare.empty)
            {
                return false;
            }

            if (color == WhiteOrBlack.white)
            {
                this.board[row, col] = TicTacSquare.white;
            }
            else
            {
                this.board[row, col] = TicTacSquare.black;
            }

            return true;
        }



        private TicTacGameStatus getBoardStatus()
        {

            // rows
            for (int row = 0; row < 3; row++)
            {
                if (board[row, 0] == TicTacSquare.white && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
                {
                    return TicTacGameStatus.white;
                }
                if (board[row, 0] == TicTacSquare.black && board[row, 0] == board[row, 1] && board[row, 1] == board[row, 2])
                {
                    return TicTacGameStatus.black;
                }
            }

            // cols
            for (int col = 0; col < 3; col++)
            {
                if (board[0, col] == TicTacSquare.white && board[0, col] == board[1, col] && board[1, col] == board[2, col])
                {
                    return TicTacGameStatus.white;
                }
                if (board[0, col] == TicTacSquare.black && board[0, col] == board[1, col] && board[1, col] == board[2, col])
                {
                    return TicTacGameStatus.black;
                }
            }

            // diagonal top left
            if (board[0, 0] == TicTacSquare.white && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return TicTacGameStatus.white;
            }
            if (board[0, 0] == TicTacSquare.black && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return TicTacGameStatus.black;
            }

            // diagonal top right
            if (board[0, 2] == TicTacSquare.white && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return TicTacGameStatus.white;
            }
            if (board[0, 2] == TicTacSquare.black && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return TicTacGameStatus.black;
            }


            // tie
            bool hasEmptySquare = false;
            foreach (TicTacSquare t in board)
            {
                if (t == TicTacSquare.empty)
                {
                    hasEmptySquare = true;
                }
            }

            if (!hasEmptySquare)
            {
                return TicTacGameStatus.tie;
            }

            // TODO: what if no move can win?


            // open game
            return TicTacGameStatus.open;
        }


        public void printBoard()
        {
            for (int row = 0; row < this.board.GetLength(0); row++)
            {
                for (int col = 0; col < this.board.GetLength(1); col++)
                {
                    TicTacSquare t = this.board[row, col];
                    Console.Write("    " + t);
                }
                Console.WriteLine();
            }
        }


        public static TicTacSquare[,] createNewBoard()
        {
            TicTacSquare[,] t = new TicTacSquare[3, 3]
            {
                {  TicTacSquare.empty, TicTacSquare.empty, TicTacSquare.empty },
                {  TicTacSquare.empty, TicTacSquare.empty, TicTacSquare.empty },
                {  TicTacSquare.empty, TicTacSquare.empty, TicTacSquare.empty }
            };

            return t;
        }


        public void printMatchStats()
        {
            Console.WriteLine();
            Console.WriteLine("MATCH STATISTICS:");
            Console.WriteLine("Match " + this.playerOneName + " VS " + this.playerTwoName);
            Console.WriteLine("Played " + this.playedGames + " out of " + this.matchGames + " games");
            Console.WriteLine("Player One (" + this.playerOneName + ") wins: " + this.playerOneWins);
            Console.WriteLine("Player Two (" + this.playerTwoName + ") wins: " + this.playerTwoWins);

            if (isMatchOver())
            {
                if (this.playerOneWins > this.playerTwoWins)
                {
                    // player one won
                    Console.WriteLine(this.playerOneName + " Wins!");
                }
                else if (this.playerOneWins > this.playerTwoWins)
                {
                    // player two won
                    Console.WriteLine(this.playerTwoName + " Wins!");
                }
                else
                {
                    // tie
                    Console.WriteLine("It's a tie!");
                }
            }

        }

    }
}

