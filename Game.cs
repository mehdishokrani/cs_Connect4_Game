using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    // Define an abstract class named "Game" that implements the "IGame" interface.
    public abstract class Game : IGame
    {
        // Declare an abstract method "Start".
        // Any class that derives from "Game" must provide an implementation for this method.
        public abstract void Start();
    }

}
