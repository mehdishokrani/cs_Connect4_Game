using System;

namespace FinalProject
{
    public class Player
    {
        public char Symbol { get; private set; } // The symbol ('X' or 'O') representing the player
        public string Name { get; private set; } // The name of the player

        public Player(char symbol, string name)
        {
            Symbol = symbol; // Set the player's symbol
            Name = name; // Set the player's name
        }
    }


}
