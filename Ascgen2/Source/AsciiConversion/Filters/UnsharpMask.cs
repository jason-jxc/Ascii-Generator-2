//---------------------------------------------------------------------------------------
// <copyright file="UnsharpMask.cs" company="Jonathan Mathews Software">
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
    /// Filter to run a unsharp mask over the values
    /// </summary>
    public class UnsharpMask : IAscgenFilter
    {
        #region Fields

        /// <summary>Blur filter used during the unsharp process</summary>
        private Blur blur = new Blur();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsharpMask"/> class.
        /// </summary>
        public UnsharpMask()
            : this(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsharpMask"/> class.
        /// </summary>
        /// <param name="numberOfBlurs">The number of blurs to use when applying the filter</param>
        public UnsharpMask(int numberOfBlurs)
        {
            this.NumberOfBlurs = numberOfBlurs;
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the number of times the image should be blurred when applying the filter</summary>
        public int NumberOfBlurs { get; set; }

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

            byte[][] result = (byte[][])values.Clone();

            for (int i = 0; i < this.NumberOfBlurs; i++)
            {
                result = this.blur.Apply(result);
            }

            int arrayWidth = values[0].Length;
            int arrayHeight = values.Length;

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    // subtract the blurred value from the current value * 2
                    int newValue = values[y][x] + values[y][x] - result[y][x];

                    // make sure value is between 0 and 255
                    if ((newValue & 0xff) == newValue)
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