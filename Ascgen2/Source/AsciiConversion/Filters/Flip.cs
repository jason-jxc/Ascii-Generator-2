//---------------------------------------------------------------------------------------
// <copyright file="Flip.cs" company="Jonathan Mathews Software">
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
    /// Filter to flip the values Horizontally and/or Vertically
    /// </summary>
    public class Flip : IFlip
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Flip"/> class.
        /// </summary>
        public Flip()
            : this(false, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Flip"/> class.
        /// </summary>
        /// <param name="horizontal">Flip horizontally?</param>
        /// <param name="vertical">Flip vertically?</param>
        public Flip(bool horizontal, bool vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to flip Horizontally
        /// </summary>
        /// <value></value>
        public bool Horizontal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to flip Vertically
        /// </summary>
        /// <value></value>
        public bool Vertical { get; set; }

        #endregion Properties

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

            if (!this.Horizontal && !this.Vertical)
            {
                return values;
            }

            int arrayWidth = values[0].Length;
            int arrayHeight = values.Length;

            byte[][] result = new byte[arrayHeight][];

            for (int i = 0; i < arrayHeight; i++)
            {
                result[i] = new byte[arrayWidth];
            }

            for (int y = 0, ypos = this.Vertical ? arrayHeight - 1 : 0; y < arrayHeight; y++, ypos += this.Vertical ? -1 : 1)
            {
                for (int x = 0, xpos = this.Horizontal ? arrayWidth - 1 : 0; x < arrayWidth; x++, xpos += this.Horizontal ? -1 : 1)
                {
                    result[y][x] = values[ypos][xpos];
                }
            }

            return result;
        }

        #endregion Public methods
    }
}