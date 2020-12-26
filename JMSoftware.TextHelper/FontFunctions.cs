//---------------------------------------------------------------------------------------
// <copyright file="FontFunctions.cs" company="Jonathan Mathews Software">
//     ASCII Generator dotNET - Image to ASCII Art Conversion Program
//     Copyright (C) 2011 Jonathan Mathews Software. All rights reserved.
// </copyright>
// <author>Jonathan Mathews</author>
// <email>info@jmsoftware.co.uk</email>
// <email>jmsoftware@gmail.com</email>
// <website>http://www.jmsoftware.co.uk/</website>
// <website>http://ascgen2.sourceforge.net/</website>
// <license>
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the license, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/.
// </license>
//---------------------------------------------------------------------------------------
[assembly: System.CLSCompliant(true)]

namespace JMSoftware.TextHelper
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Class for all general font related functions
    /// </summary>
    public abstract class FontFunctions
    {
        #region Public methods

        /// <summary>
        /// Get the size of one character in a fixed pitch font
        /// </summary>
        /// <param name="font">Font to use</param>
        /// <returns>Size of one character, or empty size if font is not fixed pitch</returns>
        public static Size GetFixedPitchFontSize(Font font)
        {
            return IsFixedWidth(font) ? MeasureText("W", font) : Size.Empty;
        }

        /// <summary>
        /// Creates a random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>The random string</returns>
        public static string GetRandomString(int length)
        {
            string randomString = string.Empty;
            const string ValidCharacters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Random random = new Random();

            while (length-- > 0)
            {
                randomString += ValidCharacters[random.Next(ValidCharacters.Length - 1)];
            }

            return randomString;
        }

        /// <summary>
        /// Check if the font has a fixed width
        /// </summary>
        /// <param name="font">The font to be checked.</param>
        /// <returns>
        /// <c>true</c> if fixed width; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFixedWidth(Font font)
        {
            return NativeMethods.IsFixedPitch(font);
        }

        /// <summary>
        /// Measure the given text when it is drawn with the specified font
        /// </summary>
        /// <param name="text">The text to measure</param>
        /// <param name="font">The font to be used</param>
        /// <returns>The size of the text</returns>
        public static Size MeasureText(string text, Font font)
        {
            if (string.IsNullOrEmpty(text) || font == null)
            {
                return Size.Empty;
            }

            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                return TextRenderer.MeasureText(
                                            Graphics.FromImage(bitmap),
                                            text,
                                            font,
                                            new Size(1, 1),
                                            TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            }
        }

        /// <summary>
        /// Reverse and return a string
        /// </summary>
        /// <param name="input">The string to be reversed</param>
        /// <returns>The reversed string</returns>
        public static string Reverse(string input)
        {
            if (input == null)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();

            for (int i = input.Length - 1; i > -1; i--)
            {
                builder.Append(input[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Convert an array of strings to one string with newlines
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The converted array</returns>
        public static string StringArrayToString(string[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();

            int arraySize = array.GetLength(0);

            int arraySizeMinusOne = arraySize - 1;

            for (int i = 0; i < arraySize; i++)
            {
                result.Append(array[i]);

                if (i < arraySizeMinusOne)
                {
                    result.Append(Environment.NewLine);
                }
            }

            return result.ToString();
        }

        #endregion Public methods
    }
}