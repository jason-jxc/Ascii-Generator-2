//---------------------------------------------------------------------------------------
// <copyright file="Stretch.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiConversion.Filters
{
    /// <summary>
    /// Filter to stretch the values so min...max = 0...255
    /// </summary>
    public class Stretch : IAscgenFilter
    {
        #region Public methods

        /// <summary>
        /// Apply the filter to the values
        /// </summary>
        /// <param name="values">Input values</param>
        /// <returns>Output values</returns>
        public byte[][] Apply(byte[][] values)
        {
            if (values == null)
            {
                return null;
            }

            int arrayWidth = values[0].Length;
            int arrayHeight = values.Length;

            byte[][] result = new byte[arrayHeight][];

            for (int i = 0; i < arrayHeight; i++)
            {
                result[i] = new byte[arrayWidth];
            }

            int minimum = 255, maximum = 0;

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    if ((int)values[y][x] < minimum)
                    {
                        minimum = (int)values[y][x];
                    }

                    if ((int)values[y][x] > maximum)
                    {
                        maximum = (int)values[y][x];
                    }
                }
            }

            if (minimum == 0 && maximum == 255)
            {
                return values;
            }

            float range = (float)(maximum - minimum);

            byte[] valueLookup = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                valueLookup[i] = (byte)(((float)((i - minimum) * 255) / range) + 0.5);
            }

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    result[y][x] = valueLookup[values[y][x]];
                }
            }

            return result;
        }

        #endregion Public methods
    }
}