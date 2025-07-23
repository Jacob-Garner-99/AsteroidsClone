using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text;
using System.IO;
using System;
using Asteroids.Game.GameScenes;
using Asteroids.Utilities;


namespace Asteroids.Game.Components.General
{
    /// <summary>
    /// Manages all aspects of the game, such as: the game scene stack, 
    /// the current game scene, whether or not the game is running, etc.
    /// </summary>
    /// <remarks>
    /// Only a single instance of <c>GameManager</c> should be instantiated.
    /// </remarks>
    internal class GameManager
    {
        #region Properties

        /// <summary>
        /// The <c>Console</c>'s buffer width.
        /// </summary>
        /// <remarks>
        /// <c>Console.BufferWidth</c> (which invokes various methods) is too slow for game 
        /// update/rendering purposes. Use this value instead.
        /// </remarks>
        public int BufferWidth { get; private set; }


        /// <summary>
        /// The <c>Console</c>'s buffer height.
        /// </summary>
        /// <remarks>
        /// <c>Console.BufferHeight</c> (which invokes various methods) is too slow for game 
        /// update/rendering purposes. Use this value instead.
        /// </remarks>
        public int BufferHeight { get; private set; }


        /// <summary>
        /// Indicates if the game is running.
        /// </summary>
        public bool Running { get; set; } = true;


        /// <summary>
        /// The set of game scenes being managed.
        /// </summary>
        public Stack<IGameScene> SceneStack { get; private set; } = new Stack<IGameScene>();


        /// <summary>
        /// The target FPS the game tries to run at.
        /// </summary>
        public int TargetFPS { get; set; } = 60;


        /// <summary>
        /// The player's highscore.
        /// </summary>
        /// <remarks>
        /// The highscore is loaded from/saved to a file so that it can persist between runs of 
        /// the game.
        /// </remarks>
        public int Highscore { get; set; }


        /// <summary>
        /// The player's current score.
        /// </summary>
        public int Score { get; set; }

        #endregion


        #region Variables

        //General time related variables.
        private const float milliseconds = 1000f;

        //Delta time variables.
        private Stopwatch gameWatch = new Stopwatch();
        private float deltaTime = 0f;

        //FPS variables.
        private float targetframeDuration = 0;

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>GameManager</c> class.
        /// </summary>
        /// <param name="consoleBufferWidth">The Console's <c>BufferWidth</c></param>
        /// <param name="consoleBufferHeight">The Console's <c>BufferHeight</c></param>
        public GameManager(int consoleBufferWidth, int consoleBufferHeight)
        {
            //Buffer width/height setup.
            this.BufferWidth = consoleBufferWidth;
            this.BufferHeight = consoleBufferHeight;

            //Set up and enter the starting game scene.
            TitleScreen titleScreen = new TitleScreen(this);
            titleScreen.EnterScene();

            //Delta time setup.
            gameWatch.Restart();

            //FPS setup.
            targetframeDuration = milliseconds / TargetFPS;

            //Load the player's highscore.
            string[] highscoreFileLines = File.ReadAllLines(
                $"{AppDomain.CurrentDomain.BaseDirectory}/Game/Data/Highscore.txt");

            Highscore = 
                highscoreFileLines.Length == 0 ?
                0 :
                int.Parse(highscoreFileLines[0]);
        }

        #endregion


        #region Functions

        /// <summary>
        /// The main game loop. Once invoked an indefinite loop starts where the active
        /// game scene is updated and drawn until the <c>Running</c> property is set to 
        /// false in order to end the game.
        /// </summary>
        /// <remarks>
        /// This method should only be invoked once.
        /// </remarks>
        public void Run()
        {
            while (Running)
            {
                //Calcualte delta time.
                deltaTime = gameWatch.ElapsedMilliseconds / milliseconds;
                gameWatch.Restart();

                //Update and render the game's next frame.
                SceneStack.Peek().Update(deltaTime);
                SceneStack.Peek().Draw();

                //Cap the game to the target FPS.
                float sleepTime = targetframeDuration - gameWatch.ElapsedMilliseconds;
                if (sleepTime > 0)
                    Thread.Sleep((int)sleepTime);
            }
        }


        /// <summary>
        /// Preforms any pre-exit actions (such as saving data) and then exits the game.
        /// </summary>
        public void Exit()
        {
            File.WriteAllText(
                $"{AppDomain.CurrentDomain.BaseDirectory}/Game/Data/Highscore.txt",
                $"{Highscore}");

            Running = false;
        }

        #endregion
    }
}
