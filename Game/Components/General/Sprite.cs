using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO;
using System;


namespace Asteroids.Game.Components.General
{
    /// <summary>
    /// Represents a game sprite composed of text (ASCII characters).
    /// </summary>
    internal class Sprite
    {
        #region Properties

        /// <summary>
        /// The X position of the sprite's top left corner.
        /// </summary>
        public int X {  get; set; }


        /// <summary>
        /// The Y position of the sprite's top left corner.
        /// </summary>
        public int Y { get; set; }


        /// <summary>
        /// The width of the sprite.
        /// </summary>
        public int Width { get; internal set; }


        /// <summary>
        /// The height of the sprite.
        /// </summary>
        public int Height 
        { 
            get
            {
                return SpriteLines.Length;
            }
        }


        /// <summary>
        /// The sprite "image" itself.
        /// </summary>
        public string Texture 
        {
            get 
            { 
                return texture; 
            }

            set
            {
                texture = value;

                SpriteLines = texture.Split('\n');
                Width = CalculateSpriteWidth();
            }
        }
        private string texture;


        /// <summary>
        /// The individual lines of text that compose the texture.
        /// </summary>
        public string[] SpriteLines { get; internal set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Creates a new instance of the <c>Sprite</c> class with the specified texture and
        /// position. </summary>
        /// <param name="texture">The "image" of the sprite.</param>
        /// <param name="x">The X position of the sprite's top left corner.</param>
        /// <param name="y">The Y position of the sprite's top left corner.</param>
        public Sprite(string texture, int x, int y)
        {
            //Set the position of the sprite.
            this.X = x;
            this.Y = y;

            //Store the texture and calculate the sprite's width and height.
            this.Texture = texture;
            this.SpriteLines = texture.Split('\n');
            this.Width = CalculateSpriteWidth();
        }

        #endregion


        #region Functions

        /// <summary>
        /// Calculates the width of the sprite (used for when/if the texture is updated).
        /// </summary>
        /// <returns>The width of the sprite.</returns>
        private int CalculateSpriteWidth()
        {
            int longestWidth = 0;
            foreach (string line in this.SpriteLines)
            {
                if (line.Length > longestWidth)
                    longestWidth = line.Length;
            }

            return longestWidth;
        }


        /// <summary>
        /// Centers the sprite on a parent width and/or height.
        /// </summary>
        /// <param name="parentWidth">(Optional) the parent width to center on.</param>
        /// <param name="parentHeight">(Optional) the parent height to center on.</param>
        /// <remarks>
        /// Width centering will only happen if <paramref name="parentWidth"/> is provided.
        /// The same is true for <paramref name="parentHeight"/>. This allows centering on 
        /// just a width or height, or both.
        /// </remarks>
        public void Center(int parentWidth = 0, int parentHeight = 0)
        {
            if (parentWidth > 0)
                X = GetCenteredX(parentWidth);

            if (parentHeight > 0)
                Y = GetCenteredY(parentHeight);
        }


        /// <summary>
        /// Returns the X position that text would need to be centered on a parent width.
        /// </summary>
        /// <param name="text">The text whose X center is to be calcualted.</param>
        /// <param name="parentWidth">The parent width to center the text on.</param>
        /// <returns>The centered X position.</returns>
        public static int GetCenteredX(string text, int parentWidth)
        {
            return (parentWidth / 2) - (text.Length / 2);
        }


        /// <summary>
        /// Returns the Y position that text would need to be centered on a parent height.
        /// </summary>
        /// <param name="text">The text whose Y center is to be calcualted.</param>
        /// <param name="parentHeight">The parent height to center the text on.</param>
        /// <returns>The centered Y position.</returns>
        public static int GetCenteredY(string text, int parentHeight)
        {
            return (parentHeight / 2) - (1 / 2);
        }


        /// <summary>
        /// Checks if two sprites are colliding using AABB collision detection.
        /// </summary>
        /// <param name="spriteA">The first sprite in the collision check.</param>
        /// <param name="spriteB">The second sprite in the collision check.</param>
        /// <returns>True if the sprites are colliding; otherwise, false.</returns>
        public static bool Collide(Sprite spriteA, Sprite spriteB)
        {
            return 
                spriteA.X < spriteB.X + spriteB.Width &&
                spriteA.X + spriteA.Width > spriteB.X &&
                spriteA.Y < spriteB.Y + spriteB.Height &&
                spriteA.Y + spriteA.Height > spriteB.Y;
        }


        /// <summary>
        /// Calculates the X position the sprite needs in order to be centered on the
        /// <paramref name="parentWidth"/>.
        /// </summary>
        /// <param name="parentWidth">The width on which the sprite should be centered.</param>
        /// <returns>The X position the sprite needs in order to be centered on 
        /// the <paramref name="parentWidth"/>.</returns>
        public int GetCenteredX(int parentWidth)
        {
            return (parentWidth / 2) - (Width / 2);
        }


        /// <summary>
        /// Calculates the Y position the sprite needs in order to be centered on the
        /// <paramref name="parentHeight"/>.
        /// </summary>
        /// <param name="parentHeight">The height on which the sprite should be centered.</param>
        /// <returns>The Y position the sprite needs in order to be centered on the
        /// <paramref name="parentHeight"/></returns>
        public int GetCenteredY(int parentHeight)
        {
            return (parentHeight / 2) - (Height / 2);
        }


        /// <summary>
        /// Loads a sprite from a text (.txt) file.
        /// </summary>
        /// <param name="filePath">The path of the text file.</param>
        /// <param name="x">(Optional) the X position of the sprite's top left corner.</param>
        /// <param name="y">(Optional) the Y position of the sprite's top left corner.</param>
        /// <param name="useBaseDirectory">True if the file path should have 
        /// <c>AppDomain.CurrentDomain.BaseDirectory</c> appended before it; 
        /// otherwise, false.</param>
        /// <returns>A new Sprite instance initialized with the "image" from the text file with
        /// a default position of 0, 0.</returns>
        /// <remarks>
        /// <paramref name="x"/> and <paramref name="y"/> are 0 by default.
        /// </remarks>
        public static Sprite LoadFromFile(string filePath, int x = 0, int y = 0, 
            bool useBaseDirectory = true)
        {
            //Update the file path to use the base directory of the application (if enabled).
            if (useBaseDirectory)
                filePath = $"{AppDomain.CurrentDomain.BaseDirectory}{filePath}";

            //Get each line that composes the sprite.
            string[] spriteLines = File.ReadAllLines(filePath);

            //For each line:
            StringBuilder spriteBuilder = new StringBuilder();
            for (int i = 0; i < spriteLines.Length; i++)
            {
                //Add it to the texture being built.
                string newLine = i != spriteLines.Length - 1 ? "\n" : string.Empty;
                spriteBuilder.Append($"{spriteLines[i]}{newLine}");
            }

            //Return the new sprite.
            return new Sprite(spriteBuilder.ToString(), x, y);
        }

        #endregion
    }
}
