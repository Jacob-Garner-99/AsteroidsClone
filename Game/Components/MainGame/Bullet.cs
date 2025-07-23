using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.Components.MainGame
{
    /// <summary>
    /// Represents a bullet that the player shoots.
    /// </summary>
    internal class Bullet
    {
        #region Properties

        /// <summary>
        /// The sprite of the bullet.
        /// </summary>
        public Sprite Sprite { get; internal set; }


        /// <summary>
        /// Indicates if the bullet has been desteroyed or not.
        /// </summary>
        public bool Destroyed { get; set; } = false;


        /// <summary>
        /// The top left X position of the bullet and the sprite.
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
        /// The top left Y position of the bullet and the sprite.
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
        /// The bullet's movement speed on the X axis (the only direction they can move).
        /// </summary>
        /// <remarks>
        /// This value has been calcualted so that the asteroid will move 2 columns up
        /// at 60fps. At 30fps they would move 4 columns, etc.
        /// </remarks>
        public int Speed { get; set; } = 126;

        #endregion


        #region Variables



        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>Bullet</c> class with the specified sprite and 
        /// position.
        /// </summary>
        /// <param name="sprite">The sprite of the bullet.</param>
        /// <param name="x">The top left x position of the bullet.</param>
        /// <param name="y">The top left y position of the bullet.</param>
        public Bullet(Sprite sprite, int x, int y)
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
