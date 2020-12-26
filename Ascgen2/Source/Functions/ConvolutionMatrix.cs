//---------------------------------------------------------------------------------------
// <copyright file="ConvolutionMatrix.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware
{
    /// <summary>
    /// Basic class for 3x3 convolution matricies
    /// </summary>
    public class ConvolutionMatrix
    {
        #region Fields

        /// <summary>
        /// Matrix to be applied
        /// </summary>
        private int[] matrix;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvolutionMatrix"/> class.
        /// </summary>
        public ConvolutionMatrix()
            : this(new[] { new[] { 0, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 0 } }, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvolutionMatrix"/> class.
        /// </summary>
        /// <param name="values">3x3 array of values for the matrix</param>
        /// <param name="factor">Initial matrix factor</param>
        public ConvolutionMatrix(int[][] values, int factor)
        {
            if (values == null)
            {
                values = new[] { new[] { 0, 0, 0 }, new[] { 0, 1, 0 }, new[] { 0, 0, 0 } };
            }

            this.matrix = new int[9];
            int pos = 0;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    this.matrix[pos++] = values[row][col];
                }
            }

            this.Factor = factor;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the matrix factor - should be equal to the sum of the matrix values
        /// </summary>
        /// <value>The factor.</value>
        public int Factor { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Apply a ConvolutionMatrix to the array of values
        /// </summary>
        /// <param name="values">Values to be processed</param>
        /// <returns>Array containing the altered values</returns>
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

            int pixel;

            // variables factored out of the loops
            int maximumWidth = arrayWidth - 1;
            int maximumHeight = arrayHeight - 1;
            float factor = (float)this.Factor;
            int x, y, col, row, pos;

            for (y = 1; y < maximumHeight; y++)
            {
                for (x = 1; x < maximumWidth; x++)
                {
                    pixel = 0;
                    pos = 0;

                    for (row = -1; row < 2; row++)
                    {
                        for (col = -1; col < 2; col++)
                        {
                            // multiple the matrix value by the corresponding pixel centered around x,y
                            pixel += this.matrix[pos++] * values[y + row][x + col];
                        }
                    }

                    pixel = (int)(((float)pixel / factor) + 0.5f);

                    // if pixel is between 0 to 255
                    if ((pixel & 0xff) == pixel)
                    {
                        result[y][x] = (byte)pixel;
                    }
                    else if (pixel > 255)
                    {
                        result[y][x] = 255;
                    }
                    else
                    {
                        result[y][x] = 0;
                    }
                }
            }

            if (maximumHeight < 2 || maximumWidth < 2)
            {
                return result;
            }

            // do the edges separately to avoid min/max calc in the main loop
            // TODO: Do this but better
            int[] incrementX = { 1, maximumWidth };
            int[] incrementY = { maximumHeight, 1 };

            for (int i = 0; i < 2; i++)
            {
                for (x = 0; x < arrayWidth; x += incrementX[i])
                {
                    for (y = 0; y < arrayHeight; y += incrementY[i])
                    {
                        pixel = 0;
                        pos = 0;

                        for (row = -1; row < 2; row++)
                        {
                            for (col = -1; col < 2; col++)
                            {
                                // limit x, y to be between 0 and max
                                pixel += this.matrix[pos++] *
                                    values[y + row > maximumHeight ? maximumHeight : (y + row < 0 ? 0 : y + row)][x + col > maximumWidth ? maximumWidth : (x + col < 0 ? 0 : x + col)];
                            }
                        }

                        pixel = (int)(((float)pixel / factor) + 0.5f);

                        // if pixel is between 0 to 255
                        if ((pixel & 0xff) == pixel)
                        {
                            result[y][x] = (byte)pixel;
                        }
                        else if (pixel > 255)
                        {
                            result[y][x] = 255;
                        }
                        else
                        {
                            result[y][x] = 0;
                        }
                    }
                }
            }

            return result;
        }

        #endregion Public methods
    }
}