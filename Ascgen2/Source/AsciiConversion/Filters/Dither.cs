//---------------------------------------------------------------------------------------
// <copyright file="Dither.cs" company="Jonathan Mathews Software">
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
    using System;
    using JMSoftware.Interfaces;

    /// <summary>
    /// Filter to apply a dithering pattern to the values
    /// </summary>
    public class Dither : IDither, IAscgenFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dither"/> class.
        /// </summary>
        public Dither()
            : this(5, 3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dither"/> class.
        /// </summary>
        /// <param name="amount">Level of dithering</param>
        /// <param name="randomness">Level of randomness in the dither</param>
        public Dither(int amount, int randomness)
        {
            this.DitherAmount = amount;
            this.DitherRandom = randomness;
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the level of dithering to perform (0 = none)</summary>
        public int DitherAmount { get; set; }

        /// <summary>Gets or sets the level of randomness to use in the dither (0 = none)</summary>
        public int DitherRandom { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Implementation of the apply function
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

            Random rand = new Random();

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    int randomValue = rand.Next(-this.DitherRandom, this.DitherRandom);

                    int newValue = values[y][x] +
                        ((x + y) % 2 == 1 ? this.DitherAmount + randomValue : -this.DitherAmount - randomValue);

                    // limit the new value to between 0 and 255
                    if ((newValue & 255) == newValue)
                    {
                        result[y][x] = (byte)newValue;
                    }
                    else if (newValue > 255)
                    {
                        result[y][x] = 255;
                    }
                    else
                    {
                        result[y][x] = 0;
                    }
                }
            }

            return result;
        }

        #endregion Public methods
    }
}