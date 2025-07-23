using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;
using Asteroids.Utilities;


namespace Asteroids
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Set console settings for the game.
            Console.CursorVisible = false;

            //Create and run the game.
            int width = Console.BufferWidth;
            int height = Console.BufferHeight;

            GameManager gameManager = new GameManager(width, height);
            gameManager.Run();

            //Reset console settings after the game is finished.
            Console.CursorVisible = true;

            //Pause the console so that it dones't exit after the program is finished.
            XConsole.PauseOnFinish();
        }
    }
}
