using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.GameScenes
{
    /// <summary>
    /// Represents the game over game scene.
    /// </summary>
    internal class GameOverScene : IGameScene
    {
        #region Properties

        public GameManager GameManager { get; set; }

        #endregion


        #region Variables

        //Sprite variables.
        ScreenSheet screenSheet;
        Sprite scoreSprite;

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>GameOverScene</c> class.
        /// </summary>
        /// <param name="gameManager">A reference to the <c>GameManager</c> running 
        /// the game.</param>
        /// <param name="newHighscore">Ture if the user achieved a 
        /// highscore; otherwise, false.</param>
        public GameOverScene(GameManager gameManager, bool newHighscore)
        {
            //Set up the game manager reference and screen sheet.
            this.GameManager = gameManager;
            this.screenSheet = new ScreenSheet(GameManager);

            //Set up the score message.
            string newText =
                GameManager.Score > GameManager.Highscore || newHighscore ?
                "New! " :
                GameManager.Score == GameManager.Highscore && !newHighscore ?
                "Tied! " :
                string.Empty;

            string scoreText =
                $"{newText}High score: {GameManager.Highscore} | " +
                $"Your score: {GameManager.Score}\n\n" +
                $"(New Game (enter) | Exit (escape))";

            //Create the score message sprite.
            scoreSprite = new Sprite(scoreText, 0, 0);
            scoreSprite.Center(GameManager.BufferWidth, GameManager.BufferHeight);
        }

        #endregion


        #region Scene Functions

        public void EnterScene()
        {
            GameManager.SceneStack.Push(this);
        }


        public void ExitScene()
        {
            GameManager.SceneStack.Pop();
        }

        #endregion


        #region Game Functions

        public void Update(float deltaTime)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyState = Console.ReadKey(true);
                if (keyState.Key == ConsoleKey.Enter)
                    ExitScene();

                if (keyState.Key == ConsoleKey.Escape)
                    GameManager.Exit();
            }
        }


        public void Draw()
        {
            screenSheet.Clear();
            screenSheet.LoadSprite(scoreSprite);
            screenSheet.Draw();
        }

        #endregion


        #region Functions



        #endregion

    }
}
