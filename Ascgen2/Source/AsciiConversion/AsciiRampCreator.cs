//---------------------------------------------------------------------------------------
// <copyright file="AsciiRampCreator.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiConversion
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class to handle creation of ASCII Ramps
    /// </summary>
    public abstract class AsciiRampCreator
    {
        #region Public methods

        /// <summary>
        /// Create an ASCII Ramp with from the given font and characters
        /// </summary>
        /// <param name="font">Font to be used</param>
        /// <param name="characters">The characters to be used for the ramp</param>
        /// <returns>A new ASCII ramp</returns>
        public static string CreateRamp(System.Drawing.Font font, string characters)
        {
            if (string.IsNullOrEmpty(characters))
            {
                return null;
            }

            if (characters.Length == 1)
            {
                return characters;
            }

            string characterString = string.Empty;

            // Remove duplicate characters
            foreach (char c in characters)
            {
                if (characterString.IndexOf(c) == -1)
                {
                    characterString += c.ToString();
                }
            }

            SortedList<int, CharacterValue> list = new SortedList<int, CharacterValue>();

            int minimum = 255;
            int maximum = 0;

            for (int i = 0; i < characterString.Length; i++)
            {
                CharacterValue characterValue = new CharacterValue(characterString[i], font);

                if (list.ContainsKey(characterValue.Value))
                {
                    if (characterValue.Score < list[characterValue.Value].Score)
                    {
                        list[characterValue.Value] = characterValue;
                    }
                }
                else
                {
                    list.Add(characterValue.Value, characterValue);

                    if (minimum > characterValue.Value)
                    {
                        minimum = characterValue.Value;
                    }

                    if (maximum < characterValue.Value)
                    {
                        maximum = characterValue.Value;
                    }
                }
            }

            StringBuilder result = new StringBuilder();

            IEnumerator<KeyValuePair<int, CharacterValue>> enumerator = list.GetEnumerator();

            enumerator.MoveNext();

            int current = enumerator.Current.Key;

            // loop through and fill in the gaps
            while (enumerator.MoveNext())
            {
                int next = enumerator.Current.Key;

                int middle = current + (int)(((float)(next - current) / 2f) + 0.5f);

                for (int i = current; i < middle; i++)
                {
                    result.Append(list[current]);
                }

                for (int i = middle; i < next; i++)
                {
                    result.Append(list[next]);
                }

                current = next;
            }

            result.Append(list[current]);

            return result.ToString();
        }

        #endregion Public methods
    }
}