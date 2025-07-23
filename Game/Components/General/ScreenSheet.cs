using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Extensions;
using Asteroids.Utilities;


namespace Asteroids.Game.Components.General
{
    /// <summary>
    /// A sprite manager class (sheet) which handles drawing sprites to the console screen.
    /// </summary>
    /// <remarks>
    /// The sheet is represented by a <c>char[]</c> and is therefore 1 dimensional.
    /// </remarks>
    internal class ScreenSheet
    {
        #region Properties
        
        /// <summary>
        /// The "sheet" on to which each sprite will be loaded.
        /// </summary>
        public char[] Buffer { get; internal set; } = 
            new char[Console.BufferWidth * Console.BufferHeight];

        #endregion


        #region Variables

        private GameManager gameManager;

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <c>ScreenSheet</c> class.
        /// </summary>
        /// <param name="gameManager">A reference to the <c>GameManager</c> running 
        /// the game.</param>
        public ScreenSheet(GameManager gameManager)
        {
            this.gameManager = gameManager;
            Clear();
        }

        #endregion


        #region Functions

        /// <summary>
        /// Clears the sheet of all drawn sprites.
        /// </summary>
        public void Clear()
        {
            Buffer.Fill(' ');
        }


        /// <summary>
        /// Draws the sheet to the console.
        /// </summary>
        public void Draw()
        {
            XConsole.ResetCursorPosition();
            XConsole.Write(new string(Buffer), fullLine: false);
        }


        /// <summary>
        /// Loads a sprite onto the sheet for rendering.
        /// </summary>
        /// <param name="sprite">The sprite to render.</param>
        public void LoadSprite(Sprite sprite)
        {
            //For each line of the sprite:
            for (int y = 0; y < sprite.Height; y++)
            {
                int localY = sprite.Y + y;
                if (localY < 0 || localY >= gameManager.BufferHeight)
                    continue;

                //For each character in the line:
                for (int x = 0; x < sprite.SpriteLines[y].Length; x++)
                {
                    int localX = sprite.X + x;
                    if (localX < 0 || localX >= gameManager.BufferWidth)
                        continue;

                    //Load the character into the buffer.
                    int targetIndex = localY * gameManager.BufferWidth + localX;
                    Buffer[targetIndex] = sprite.SpriteLines[y][x];
                }
            }
        }


        /// <summary>
        /// Loads text onto the sheet for rendering.
        /// </summary>
        /// <param name="text">The text to render.</param>
        /// <param name="x">The X position of the text's top left corner.</param>
        /// <param name="y">The Y position of the text's top left corner.</param>
        /// <remarks>
        /// Currently does not suppoert multi-line text rendering. Don't use new lines in 
        /// the text to be rendered.
        /// </remarks>
        public void LoadText(string text, int x, int y)
        {
            //For each character in the text:
            for (int i = 0; i < text.Length; i++)
            {
                int localX = x + i;
                if (localX < 0 || localX > gameManager.BufferWidth)
                    continue;

                //Load the character into the buffer.
                int targetIndex = y * gameManager.BufferWidth + localX;
                Buffer[targetIndex] = text[i];
            }
        }

        #endregion
    }
}
