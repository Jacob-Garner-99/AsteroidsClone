using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;


namespace Asteroids.Utilities
{
    /// <summary>
    /// A wrapper class that extends the functionality of the <c>System.Console</c> class.
    /// </summary>
    internal static class XConsole
    {
        /// <summary>
        /// Waits for the user to press enter (this is just an aesthetic wrap for 
        /// <c>Console.ReadLine()</c>).
        /// </summary>
        public static void Pause()
        {
            Console.ReadLine();
        }


        /// <summary>
        /// Displays a program ended messages and waits for the user to press enter.
        /// </summary>
        public static void PauseOnFinish()
        {
            Write("\n(Program finished. Press enter to exit.)", ConsoleColor.DarkGray);
            Pause();
        }


        /// <summary>
        /// Resets the console's cursor position to 0, 0 (top left).
        /// </summary>
        public static void ResetCursorPosition()
        {
            Console.SetCursorPosition(0, 0);
        }


        /// <summary>
        /// Writes text to the console.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="textColor">The color of the written text.</param>
        /// <param name="centered">Indicates if the text should be centered.</param>
        /// <param name="fullLine">Indicates if the text should be followed by a 
        /// line terminator (new line).</param>
        public static void Write(string text, ConsoleColor textColor = ConsoleColor.Gray,
            bool centered = false, bool fullLine = true)
        {
            //Set the console text color.
            Console.ForegroundColor = textColor;

            //Center the text.
            if (centered)
            {
                int newlineCount = text.Count(c => c == '\n');

                Console.SetCursorPosition(
                     (Console.BufferWidth / 2) - ((text.Length - newlineCount) / 2),
                     Console.CursorTop);
            }

            //Write the text.
            if (fullLine)
                Console.WriteLine(text);

            else
                Console.Write(text);

            //Reset the console text color.
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
