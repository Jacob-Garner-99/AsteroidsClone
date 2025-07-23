using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.GameScenes
{
    /// <summary>
    /// Represents the title screen game scene.
    /// </summary>
    internal class TitleScreen : IGameScene
    {
        #region Properties

        public GameManager GameManager { get; set; }

        #endregion


        #region Variables

        //Sprite variables.
        private ScreenSheet screenSheet;

        private Sprite titleSprite = Sprite.LoadFromFile(
            $"/Game/Assets/TitleScreen/Title.txt", y: 1);

        private Sprite optionsSprite = Sprite.LoadFromFile(
            $"/Game/Assets/TitleScreen/Options.txt");

        private Sprite earthSprite = Sprite.LoadFromFile(
            $"/Game/Assets/TitleScreen/Earth.txt");

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>TitleScreen</c> class.
        /// </summary>
        /// <param name="gameManager">A reference to the <c>GameManager</c> running 
        /// the game.</param>
        public TitleScreen(GameManager gameManager)
        {
            //Set up the game manager reference and screen sheet.
            this.GameManager = gameManager;
            this.screenSheet = new ScreenSheet(GameManager);

            //Set the positions of the sprites.
            titleSprite.Center(GameManager.BufferWidth);

            optionsSprite.Center(GameManager.BufferWidth);
            optionsSprite.Y = titleSprite.Y + titleSprite.Height + 2;

            earthSprite.Center(GameManager.BufferWidth);
            earthSprite.Y = GameManager.BufferHeight - (earthSprite.Height / 4);
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
            //Handle keyboard inputs from the user:
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                //Enter: start a new game.
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    MainGame mainGame = new MainGame(GameManager);
                    mainGame.EnterScene();
                }

                //Escape: exit the game.
                if (keyInfo.Key == ConsoleKey.Escape)
                    GameManager.Exit();
            }
        }


        public void Draw()
        {
            screenSheet.Clear();
            screenSheet.LoadSprite(titleSprite);
            screenSheet.LoadSprite(optionsSprite);
            screenSheet.LoadSprite(earthSprite);
            screenSheet.Draw();
        }

        #endregion


        #region Functions



        #endregion
    }
}
