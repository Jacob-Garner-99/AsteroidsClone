using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.MainGame;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.GameScenes
{
    /// <summary>
    /// Represents the main game game scene.
    /// </summary>
    internal class MainGame : IGameScene
    {
        #region Properties

        public GameManager GameManager { get; set; }

        #endregion


        #region Variables

        //General game variables.
        ScreenSheet screenSheet;

        Random rnd = new Random();

        StringBuilder scoreText = new StringBuilder("Score: 0");
        int scoreXPosition = 0;

        bool gameOver = false;

        //Background sprite variables.
        Sprite earthSprite = Sprite.LoadFromFile(
            $"/Game/Assets/TitleScreen/Earth.txt");

        //Player variables.
        Player player;
        List<Bullet> playerBullets = new List<Bullet>();

        //Asteroid variables.
        List<Asteroid> asteroids = new List<Asteroid>();

        float spawnFrequencyTime = 3;
        float spawnTimer = 0;
        bool asteroidHit = false;

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>MainGame</c> class.
        /// </summary>
        /// <param name="gameManager">A reference to the <c>GameManager</c> running 
        /// the game.</param>
        public MainGame(GameManager gameManager)
        {
            //Set up the game manager reference and screen sheet.
            this.GameManager = gameManager;
            this.screenSheet = new ScreenSheet(GameManager);

            //Set up the score text's X position.
            scoreXPosition =
                (GameManager.BufferWidth / 2) - (scoreText.ToString().Length / 2);

            //Set up the earth sprite.
            earthSprite.Center(parentHeight: GameManager.BufferHeight);
            earthSprite.X = -(earthSprite.Width - earthSprite.Width / 3);

            //Set up the player.
            player = new Player(
                Sprite.LoadFromFile("/Game/Assets/MainGame/Spaceship.txt"), 
                0, 0);

            player.X = earthSprite.X + earthSprite.Width + 10;
            player.Y = player.Sprite.GetCenteredY(GameManager.BufferHeight);
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
            #region User input

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyState = Console.ReadKey(true);

                //W (up): move the player up.
                if (keyState.Key == ConsoleKey.W)
                {
                    if (player.Y > 0)
                        player.Y -= Math.Max(1, (int)(player.Speed * deltaTime));
                }

                //S (down): move the player down.
                if (keyState.Key == ConsoleKey.S)
                {
                    if (player.Y < GameManager.BufferHeight - player.Sprite.Height)
                        player.Y += Math.Max(1, (int)(player.Speed * deltaTime));
                }

                //Spacebar: shoot.
                if (keyState.Key == ConsoleKey.Spacebar)
                {
                    SpawnPlayerBullets();
                }
            }

            #endregion


            #region Bind the player to the screen

            if (player.Y < 0)
                player.Y = 0;

            if (player.Y + player.Sprite.Height > GameManager.BufferHeight)
                player.Y = GameManager.BufferHeight - player.Sprite.Height;

            #endregion


            #region Asteroid spawning

            spawnTimer += deltaTime;
            if (spawnTimer >= spawnFrequencyTime)
            {
                spawnTimer = 0;
                SpawnAsteroid();
            }

            #endregion


            #region Player bullet position updates

            foreach (Bullet bullet in playerBullets)
            {
                //Update the bullet's position.
                bullet.X += Math.Max(2, (int)(bullet.Speed * deltaTime));

                //Destroy the bullet if it has gone off screen.
                if (bullet.X + bullet.Sprite.Width > GameManager.BufferWidth)
                    bullet.Destroyed = true;
            }

            #endregion


            #region Asteroid position updates and collision checks

            foreach (Asteroid asteroid in asteroids)
            {
                //Update the asteroid's position.
                asteroid.X -= Math.Max(1, (int)(asteroid.Speed * deltaTime));

                //Check for collision with the player or the earth.
                if (Sprite.Collide(asteroid.Sprite, player.Sprite) || 
                    Sprite.Collide(asteroid.Sprite, earthSprite))
                {
                    gameOver = true;
                }

                //Check for collision with either of the player's two bullets.
                foreach (Bullet bullet in playerBullets)
                {
                    if (Sprite.Collide(asteroid.Sprite, bullet.Sprite))
                    {
                        /* We need to check and destory both player's bullets if they both hit 
                         * the asteroid. So we don't break after a collision detection. */
                        if (!asteroidHit)
                        {
                            //Mark the asteroid as destroyed.
                            asteroidHit = true;
                            asteroid.Desteroyed = true;

                            //Update the player's score and score text.
                            GameManager.Score +=
                                150 -
                                (asteroid.Sprite.X - (player.Sprite.X + player.Sprite.Width));

                            scoreText.Clear();
                            scoreText.Append($"Score: " + GameManager.Score.ToString("N0"));

                            scoreXPosition =
                                (GameManager.BufferWidth / 2) - (scoreText.ToString().Length / 2);
                        }

                        bullet.Destroyed = true;
                    }
                }
            }

            //Remove any destroyed asteroids and bullets.
            if (asteroidHit)
            {
                asteroidHit = false;

                asteroids = asteroids.Where(a => !a.Desteroyed).ToList();
                playerBullets = playerBullets.Where(b => !b.Destroyed).ToList();
            }

            if (gameOver)
            {
                //Update the player's highscore.
                bool newHighscore = GameManager.Score > GameManager.Highscore;
                if (newHighscore)
                    GameManager.Highscore = GameManager.Score;

                //Create and then show the game over scene.
                GameOverScene gameOverScene = new GameOverScene(GameManager, newHighscore);
                gameOverScene.EnterScene();

                /* If the user starts a new game from the game over scene: the game is re-started.
                 * If the user instead exits the game instead, the reset line is never executed.
                 * So I can do this hacky way of re-setting the game. 
                 * 
                 * I'm not actually sure why this code works. It seems to me like after popping 
                 * the game over scene form the game manager's scene stack: the main game's loop 
                 * should be re-run, the game is still over, so the game over scene should be 
                 * re-entered. My guess as to why this works is that somehow where the main game's
                 * loop is stopped as the game over scene is entered is saved. When the scene is
                 * exited the code picks up right back here (if the user started a new game). 
                 * This is just a learning project though. So it's fine. */
                Reset();
            }

            #endregion
        }


        public void Draw()
        {
            screenSheet.Clear();

            //Darw the player's score.
            screenSheet.LoadText(scoreText.ToString(), scoreXPosition, 0);

            //Draw the earth sprite.
            screenSheet.LoadSprite(earthSprite);

            //Draw the player's bullets.
            screenSheet.LoadSprite(player.Sprite);
            foreach (Bullet bullet in playerBullets)
                screenSheet.LoadSprite(bullet.Sprite);

            //Draw the asteroids.
            foreach (Asteroid asteroid in asteroids)
                screenSheet.LoadSprite(asteroid.Sprite);

            screenSheet.Draw();
        }

        #endregion


        #region Functions

        /// <summary>
        /// Resets the game so that the player can play again.
        /// </summary>
        private void Reset()
        {
            //Reset game varables.
            gameOver = false;
            GameManager.Score = 0;
            spawnTimer = 0;

            //Reset non-player entities.
            asteroids.Clear();
            playerBullets.Clear();

            //Reset the player's position.
            player.X = earthSprite.X + earthSprite.Width + 10;
            player.Y = player.Sprite.GetCenteredY(GameManager.BufferHeight);

            //Reset the score text.
            scoreText.Clear();
            scoreText.Append($"Score: " + GameManager.Score.ToString("N0"));
        }


        /// <summary>
        /// Spawns two player bullets located at each of the ships cannons.
        /// </summary>
        private void SpawnPlayerBullets()
        {
            playerBullets.Add(CreateBullet(4, 0));
            playerBullets.Add(CreateBullet(4, 2));
        }


        /// <summary>
        /// Creates a bullet at the player's position with an X and Y offset.
        /// </summary>
        /// <param name="xOffset">The X offset of the bullet from the player.</param>
        /// <param name="yOffset">The Y offset of the bullet from the player.</param>
        /// <returns></returns>
        private Bullet CreateBullet(int xOffset, int yOffset)
        {
            return new Bullet(
               Sprite.LoadFromFile($"/Game/Assets/MainGame/Bullet.txt"),
               player.X + xOffset, player.Y + yOffset);
        }


        /// <summary>
        /// Randomly spawns an asteroid.
        /// </summary>
        public void SpawnAsteroid()
        {
            //Create a random asteroid.
            int asteroidType = rnd.Next(1, 4);

            Asteroid asteroid = new Asteroid(
                Sprite.LoadFromFile($"/Game/Assets/MainGame/Asteroid{asteroidType}.txt"),
                0, 0);

            //Randomly place the asteroid.
            asteroid.X = GameManager.BufferWidth + asteroid.Sprite.Width;
            asteroid.Y = rnd.Next(0, GameManager.BufferHeight - asteroid.Sprite.Height);

            //Add the asteroid to the game.
            asteroids.Add(asteroid);
        }


        #endregion

    }
}
