// See https://aka.ms/new-console-template for more information

using System;
using System.IO;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

    class Game
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer;

        public Game()
        {
            InitializeBoard();
            currentPlayer = 'X';
        }

        public void Start()
        {
            bool gameWon = false;
            int turns = 0;

            while (turns < 9 && !gameWon)
            {
                DisplayBoard();
                PlayerMove();
                gameWon = CheckForWinner();
                if (gameWon)
                {
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    WriteResultToFile($"Player {currentPlayer} wins!");
                }
                else if (turns == 8)
                {
                    Console.WriteLine("The game is a draw!");
                    WriteResultToFile("The game is a draw!");
                }

                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                turns++;
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        private void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -----");
            }
        }

        private void PlayerMove()
        {
            int row, col;
            do
            {
                Console.WriteLine($"Player {currentPlayer}, enter your move (row and column): ");
                row = int.Parse(Console.ReadLine());
                col = int.Parse(Console.ReadLine());
            } while (row < 0 || row > 2 || col < 0 || col > 2 || board[row, col] != ' ');

            board[row, col] = currentPlayer;
        }

        private bool CheckForWinner()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer) ||
                    (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer))
                {
                    return true;
                }
            }

            // Check diagonals
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
            {
                return true;
            }

            return false;
        }

        private void WriteResultToFile(string result)
        {
            string filePath = "GameResult.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(result);
            }
        }
    }
}
