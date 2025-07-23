using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.Components.MainGame
{
    /// <summary>
    /// Represents an asteroid in the game.
    /// </summary>
    internal class Asteroid
    {
        #region Properties

        /// <summary>
        /// The sprite of the asteroid.
        /// </summary>
        public Sprite Sprite { get; internal set; }


        /// <summary>
        /// Indicates if the asteroid has been desteroyed or not.
        /// </summary>
        public bool Desteroyed { get; set; } = false;


        /// <summary>
        /// The top left X position of the asteroid and the sprite.
        /// </summary>
        public int X
        {
            get { return x; }

            set
            {
                x = value;
                Sprite.X = value;
            }
        }
        private int x;


        /// <summary>
        /// The top left Y position of the asteroid and the sprite.
        /// </summary>
        public int Y
        {
            get { return y; }

            set
            {
                y = value;
                Sprite.Y = value;
            }
        }
        private int y;

        /// <summary>
        /// The asteroid's movement speed on the X axis (the only direction they can move).
        /// </summary>
        /// <remarks>
        /// This value has been calcualted so that the asteroid will move 1 column down
        /// at 60fps. At 30fps they would move 2 columns, etc.
        /// </remarks>
        public int Speed { get; set; } = 63;

        #endregion


        #region Variables



        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>Asteroid</c> class with the specified sprite and 
        /// position. </summary>
        /// <param name="sprite">The sprite of the asteroid.</param>
        /// <param name="x">The top left x position of the asteroid.</param>
        /// <param name="y">The top left y position of the asteroid.</param>
        public Asteroid(Sprite sprite, int x, int y)
        {
            this.Sprite = sprite;

            this.X = x;
            this.Y = y;
        }

        #endregion


        #region Functions



        #endregion
    }
}
