using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;


namespace Asteroids.Extensions
{
    /// <summary>
    /// Provides extension methods for the <c>array</c> reference type.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Fills the array with a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value">The value to fill the array with.</param>
        public static void Fill<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }
    }
}
