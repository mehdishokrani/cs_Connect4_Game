using System;

namespace FinalProject
{

    class Program
    {
        static void Main(string[] args)
        {
            
            
            bool continueGame = true; // Flag to control whether to continue playing or not
            while (continueGame)
            {
                // Create a new instance of the ConnectFourGame class
                ConnectFourGame game = new ConnectFourGame();
                // Start the Connect Four game
                game.Start(); // Start the game

                Console.WriteLine("Do you want to play again?\n'Y' for \"Yes\" and 'N' for \"No\"");
                string input = string.Empty; // Variable to store user input
                bool isValidInput = false; // Flag to check if the input is valid

                while (!isValidInput)
                {
                    try
                    {
                        // Read user input from the console and convert it to uppercase
                        input = Console.ReadLine()?.ToUpper();

                        // Check for valid input
                        if (string.IsNullOrEmpty(input) || input.Length != 1 || (input[0] != 'Y' && input[0] != 'N'))
                        {
                            throw new ArgumentException("Invalid input. Please enter 'Y' for \"Yes\" or 'N' for \"No\".");
                        }

                        isValidInput = true; // Set isValidInput to true if input is valid
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message); // Display the error message from the exception
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message); // Display a generic error message for any other exception
                    }
                }

                // Check if the user wants to continue playing
                if (input[0] == 'N')
                {
                    continueGame = false; // Set continueGame to false to exit the loop
                }
            }
        }

    }
}
