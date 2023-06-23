using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace FinalProject
{
    public class ConnectFourGame : Game
    {
        private Board board; // Represents the game board
        private Player currentPlayer; // Represents the current player
        private bool isGameOver; // Indicates if the game is over

        public ConnectFourGame()
        {
            board = new Board(); // Create a new instance of the Board class to represent the game board
            // Create a new instance of the Player class for the current player, initializing with symbol 'X' and an empty name
            currentPlayer = new Player('X', "");
            isGameOver = false; // Set the initial game over state to false, indicating that the game is not yet over
        }

        // Override the abstract method "Start" from the base class "Game".
        // This method provides the implementation specific to the Connect Four game.
        public override void Start()
        {
            Console.WriteLine("****Connect Four Game****");
            Console.WriteLine();
            Console.WriteLine("Remember that, player name can only contain alphabetic characters, numbers, spaces, hyphens, or underscores." +
                            "\nAnd should start with a letter or number and, maximum 20 characters.");
            // Get the name of the first player
            Console.Write("Enter the name of the first player: ");
            string player1Name = (GetPlayerName("")).Trim();

            // Get the name of the second player
            Console.Write("Enter the name of the second player: ");
            string player2Name = (GetPlayerName(player1Name)).Trim();

            // Create Player objects for the first and second players
            Player player1 = new Player('X', player1Name);
            Player player2 = new Player('O', player2Name);

            // Set the current player to player1
            currentPlayer = player1;

            while (!isGameOver)
            {
                Console.Clear(); // Clear the console screen
                Console.WriteLine("****Connect Four Game****");
                Console.WriteLine($"Player 1: \'{player1.Name}\' has \"X\" symbol,");
                Console.WriteLine($"Player 2: \'{player2.Name}\' has \"O\" symbol.\n");
                board.Display(); // Display the game board
                Console.Write($"{currentPlayer.Name}, it is your turn.\nEnter the column number to drop your piece: ");

                // Get the column number input from the current player
                if (int.TryParse(Console.ReadLine(), out int column))
                {
                    if (column >= 1 && column <= Board.Columns)
                    {
                        column--; // Convert to zero-based index

                        // Check if the move is valid and make the move on the board
                        if (board.IsValidMove(column))
                        {
                            board.MakeMove(column, currentPlayer.Symbol);

                            // Check if the current player wins
                            if (board.HasWinner(currentPlayer.Symbol))
                            {
                                Console.Clear(); // Clear the console screen
                                board.Display();
                                Console.WriteLine($"\"{currentPlayer.Name}\" with player symbol \'{currentPlayer.Symbol}\' is the ***WINNER***");
                                isGameOver = true;
                            }
                            // Check if it's a draw
                            else if (board.IsBoardFull())
                            {
                                Console.Clear(); // Clear the console screen
                                board.Display();
                                Console.WriteLine($"It's a draw!\n{player1Name} and {player2Name} the game is over without winner.");
                                isGameOver = true;
                            }
                            else
                            {
                                // Switch the current player
                                currentPlayer = currentPlayer == player1 ? player2 : player1;
                            }
                        }
                        else
                        {
                            //This code block is executed when the entered move by the current player is invalid.
                            Console.WriteLine("Invalid move. Press Enter then write valid move.");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        //It displays a message indicating that the column number is invalid and prompts the player to press Enter before continuing.
                        Console.WriteLine("Invalid column number. Press Enter then write valid column number.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    //This code block is executed when the entered input is invalid (not a valid column number).
                    Console.WriteLine("Invalid input. Press Enter then write valid input.");
                    Console.ReadKey();
                }
            }
            //It displays the "Game over." message to indicate the end of the game.
            Console.WriteLine("Game over.");
        }
        private static string GetPlayerName(string opponentName)
        {
            while (true)
            {
                try
                {
                    // Read the player name from the console while check most exception cases
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input)) // Check if the input is empty or consists only of whitespace
                    {
                        throw new ArgumentException("Player name cannot be empty."); // Throw an exception with an error message
                    }

                    if (input.Length > 20) // Check if the input exceeds the maximum length of 20 characters
                    {
                        throw new ArgumentException("Player name cannot exceed 20 characters."); // Throw an exception with an error message
                    }

                    if (!IsValidPlayerName(input)) // Check if the input contains any invalid characters
                    {
                        // Throw an exception with an error message
                        throw new ArgumentException("Player name can only contain alphabetic characters, numbers, spaces, hyphens, or underscores." +
                            "\nAnd should start with a letter or number.");
                    }

                    // Check if the input is the same as the opponent's name (ignoring case)
                    if (input.Equals(opponentName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Throw an exception with an error message
                        throw new ArgumentException("Player name cannot be the same as the opponent's name. Please enter a different name.");
                    }

                    return FormatPlayerName(input); // Return the valid player name
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message); // Display the error message from the exception
                    Console.Write("Please enter a valid name: "); // Prompt the user to enter a valid name
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message); // Display a generic error message for any other exception
                    Console.Write("Please try again: "); // Prompt the user to try again
                }
            }
        }


        private static bool IsValidPlayerName(string name)
        {
            if (!char.IsLetterOrDigit(name[0])) // Check if the first character is not a letter or digit
            {
                return false; // Return false indicating an invalid player name
            }

            foreach (char c in name)
            {
                // Check if the character is not a letter, digit, whitespace, hyphen, or underscore
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) && c != '-' && c != '_')
                {
                    return false; // Return false indicating an invalid player name
                }
            }

            return true; // Return true indicating a valid player name
        }
        private static string FormatPlayerName(string name)
        {
            // Get the text info for the current culture
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            // Format the first character of the name to uppercase
            name = textInfo.ToTitleCase(name.ToLower());

            // Remove double spaces
            name = string.Join(" ", name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // Split the name into words using space as the separator
            string[] words = name.Split(' ');

            // Format each remaining word by making the first letter uppercase and the remaining letters lowercase
            for (int i = 1; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i].ToLower());
            }

            // Join the words back together with a space separator
            return string.Join(" ", words);
        }



    }
}
