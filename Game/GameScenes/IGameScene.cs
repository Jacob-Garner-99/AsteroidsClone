using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;
using Asteroids.Game.Components.General;


namespace Asteroids.Game.GameScenes
{
    /// <summary>
    /// Defines the necessary properties and functions needed for a game scene to run, such as:
    /// update and draw methods, a reference to the GameManager, etc.
    /// </summary>
    internal interface IGameScene
    {
        #region Properties

        /// <summary>
        /// A reference to the <c>GameManager</c> running the game.
        /// </summary>
        GameManager GameManager { get; set; }

        #endregion

        
        #region Functions
        
        /// <summary>
        /// Sets this scene as the current game scene in the <c>GameManager</c>.
        /// </summary>
        void EnterScene();


        /// <summary>
        /// Exits this game scene and removes it from the <c>GameManager</c>.
        /// </summary>
        void ExitScene();


        /// <summary>
        /// Updates this game scene.
        /// </summary>
        /// <param name="deltaTime">The time in seconds since the last frame of the game.</param>
        void Update(float deltaTime);


        /// <summary>
        /// Draws this game scene to the screen.
        /// </summary>
        void Draw();

        #endregion
    }
}
