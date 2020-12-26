//---------------------------------------------------------------------------------------
// <copyright file="ValuesToFixedWidthTextConverter.cs" company="Jonathan Mathews Software">
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
    using System.Text;

    /// <summary>
    /// Class to convert a fixed size array into strings using the specified ASCII Ramp
    /// </summary>
    public class ValuesToFixedWidthTextConverter : ValuesToTextConverter
    {
        #region Fields

        /// <summary>
        /// The ASCII Ramp (Lighest character to darkest)
        /// </summary>
        private string ramp;

        /// <summary>
        /// Array mapping the value x onto the matching character from the ramp
        /// </summary>
        private char[] valueCharacters;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesToFixedWidthTextConverter"/> class.
        /// </summary>
        /// <param name="ramp">Ramp used to create the output image</param>
        public ValuesToFixedWidthTextConverter(string ramp)
        {
            this.Ramp = ramp;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the ASCII Ramp (Lighest character to darkest)
        /// </summary>
        /// <value>The ASCII ramp.</value>
        public string Ramp
        {
            get
            {
                return this.ramp;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || this.ramp == value)
                {
                    return;
                }

                this.ramp = value;

                this.valueCharacters = new char[256];

                float length = (float)(this.Ramp.Length - 1);

                for (int x = 0; x < 256; x++)
                {
                    this.valueCharacters[x] = this.ramp[(int)((((float)x / 255f) * length) + 0.5f)];
                }
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Convert 2d array of byte values into character strings
        /// </summary>
        /// <param name="values">2d array of values that represent the image</param>
        /// <returns>Array of strings containing the text image</returns>
        public override string[] Apply(byte[][] values)
        {
            if (values == null)
            {
                return null;
            }

            int numberOfColumns = values[0].Length;
            int numberOfRows = values.Length;

            string[] result = new string[numberOfRows];

            for (int y = 0; y < numberOfRows; y++)
            {
                StringBuilder builder = new StringBuilder();

                for (int x = 0; x < numberOfColumns; x++)
                {
                    builder.Append(this.valueCharacters[values[y][x]]);
                }

                result[y] = builder.ToString();
            }

            return result;
        }

        #endregion Public methods
    }
}