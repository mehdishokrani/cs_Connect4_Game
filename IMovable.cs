using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    // Declare an interface named "IMovable" to represent a movable game object.
    public interface IMovable
    {
        // Declare a method "IsValidMove" to check if a move is valid given a position.
        // It takes an integer position as input and returns a boolean indicating the validity of the move.
        bool IsValidMove(int position);
        // Declare a method "MakeMove" to make a move given a position and a symbol.
        // It takes an integer position and a character symbol as input and does not return a value.
        void MakeMove(int position, char symbol);
    }

}
