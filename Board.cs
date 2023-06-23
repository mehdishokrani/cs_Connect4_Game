using System;

namespace FinalProject
{
    public class Board: IDisplayable, IGameObject, IMovable
    {
        public const int Rows = 6; // The number of rows on the game board
        public const int Columns = 7; // The number of columns on the game board

        private char[,] grid; // The 2D array representing the game board


        public Board()
        {
            grid = new char[Rows, Columns]; // Initialize the game board as a 2D char array with dimensions Rows x Columns
        }
        public void Initialize()
        {
            grid = new char[Rows, Columns]; // Initialize the game board as a 2D char array with dimensions Rows x Columns
        }


        public void Display()
        {
            // Display column numbers
            for (int col = 1; col <= Columns; col++)
            {
                Console.Write($"{col}   "); // Display the column number followed by three spaces
            }
            Console.Write(" <---These are column numbers.");
            Console.WriteLine(); // Move to the next line after displaying all column numbers

            // Display table
            for (int row = Rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < Columns; col++)
                {
                    // If the cell is empty, print a dot; otherwise, print the symbol
                    Console.Write(grid[row, col] == '\0' ? "." : grid[row, col].ToString());

                    Console.Write(" | "); // Print a vertical line (pipe symbol) to separate cells
                }

                Console.WriteLine(); // Move to the next line after displaying all cells in the row
                Console.WriteLine(new string('-', Columns * 4 - 1)); // Print a horizontal line between rows
            }

            Console.WriteLine(); // Print an empty line for better readability
        }

        public bool IsValidMove(int column)
        {
            return grid[Rows - 1, column] == '\0'; // Check if the top row of the column is empty
        }

        public void MakeMove(int column, char symbol)
        {
            for (int row = 0; row < Rows; row++)
            {
                if (grid[row, column] == '\0') // Check if the current cell is empty
                {
                    grid[row, column] = symbol; // Place the symbol in the current cell
                    break; // Exit the loop since the move has been made
                }
            }
        }

        public bool HasWinner(char symbol)
        {
            // Check if there is a winning sequence in any rows, columns, or diagonals
            return CheckRows(symbol) || CheckColumns(symbol) || CheckDiagonals(symbol);
        }


        private bool CheckRows(char symbol)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    if (grid[row, col] != '\0' && // Check if the current cell is not empty
                        grid[row, col] == grid[row, col + 1] && // Check if the current cell is the same as the next cell in the row
                        grid[row, col] == grid[row, col + 2] && // Check if the current cell is the same as the cell two positions ahead in the row
                        grid[row, col] == grid[row, col + 3]) // Check if the current cell is the same as the cell three positions ahead in the row
                    {
                        return true; // Return true if there is a winning sequence of symbols in the row
                    }
                }
            }

            return false; // Return false if no winning sequence is found in any row
        }


        private bool CheckColumns(char symbol)
        {
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row <= Rows - 4; row++)
                {
                    if (grid[row, col] != '\0' && // Check if the current cell is not empty
                        grid[row, col] == grid[row + 1, col] && // Check if the current cell is the same as the cell below it in the column
                        grid[row, col] == grid[row + 2, col] && // Check if the current cell is the same as the cell two positions below it in the column
                        grid[row, col] == grid[row + 3, col]) // Check if the current cell is the same as the cell three positions below it in the column
                    {
                        return true; // Return true if there is a winning sequence of symbols in the column
                    }
                }
            }

            return false; // Return false if no winning sequence is found in any column
        }

        private bool CheckDiagonals(char symbol)
        {
            // Check diagonals from bottom-left to top-right
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    if (grid[row, col] != '\0' && // Check if the current cell is not empty
                        grid[row, col] == grid[row + 1, col + 1] && // Check if the current cell is the same as the cell diagonally below and to the right
                        grid[row, col] == grid[row + 2, col + 2] && // Check if the current cell is the same as the cell two positions diagonally below and to the right
                        grid[row, col] == grid[row + 3, col + 3]) // Check if the current cell is the same as the cell three positions diagonally below and to the right
                    {
                        return true; // Return true if there is a winning sequence of symbols in the diagonal
                    }
                }
            }

            // Check diagonals from top-left to bottom-right
            for (int row = 3; row < Rows; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    if (grid[row, col] != '\0' && // Check if the current cell is not empty
                        grid[row, col] == grid[row - 1, col + 1] && // Check if the current cell is the same as the cell diagonally above and to the right
                        grid[row, col] == grid[row - 2, col + 2] && // Check if the current cell is the same as the cell two positions diagonally above and to the right
                        grid[row, col] == grid[row - 3, col + 3]) // Check if the current cell is the same as the cell three positions diagonally above and to the right
                    {
                        return true; // Return true if there is a winning sequence of symbols in the diagonal
                    }
                }
            }

            return false; // Return false if no winning sequence is found in any diagonal
        }


        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (grid[Rows - 1, col] == '\0') // Check if the top row of any column is empty
                {
                    return false; // If an empty slot is found, the board is not full
                }
            }

            return true; // Return true if all columns have a piece in the top row, indicating that the board is full
        }

    }
}
