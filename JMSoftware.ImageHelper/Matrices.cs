//---------------------------------------------------------------------------------------
// <copyright file="Matrices.cs" company="Jonathan Mathews Software">
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
    using System.Drawing.Imaging;

    /// <summary>
    /// Class of static functions to create or process ColorMatrix objects
    /// </summary>
    public abstract class Matrices
    {
        #region Public methods

        /// <summary>
        /// Returns a ColorMatrix to alter an image by the desired Brightness and Contrast
        /// </summary>
        /// <param name="brightness">Level of brightness (0.0 = none)</param>
        /// <param name="contrast">Level of contrast (1.0 = none)</param>
        /// <returns>ColorMatrix to modify an images brightness and contrast</returns>
        public static ColorMatrix BrightnessContrast(float brightness, float contrast)
        {
            float value = (0.5f * (1f - contrast)) + brightness;

            return new ColorMatrix(
                            new[]
                            {
                                new[] { contrast, 0f, 0f, 0f, 0f },
                                new[] { 0f, contrast, 0f, 0f, 0f },
                                new[] { 0f, 0f, contrast, 0f, 0f },
                                new[] { 0f, 0f, 0f, 1, 0f },
                                new[] { value, value, value, 2f, 1f }
                            });
        }

        /// <summary>
        /// Returns a ColorMatrix to convert an image into grayscale
        /// </summary>
        /// <returns>ColorMatrix to convert an image into grayscale</returns>
        public static ColorMatrix Grayscale()
        {
            return new ColorMatrix(
                            new[]
                            {
                                new[] { 0.299f,  0.299f,  0.299f,  0f, 0f },
                                new[] { 0.587f, 0.587f, 0.587f, 0f, 0f },
                                new[] { 0.114f, 0.114f, 0.114f, 0f, 0f },
                                new[] { 0f, 0f, 0f, 1f, 0f },
                                new[] { 0f, 0f, 0f, 0f, 1f }
                            });
        }

        /// <summary>
        /// Returns the identity ColorMatrix, which has no effect on an image
        /// </summary>
        /// <returns>The Identity ColorMatrix</returns>
        public static ColorMatrix Identity()
        {
            return new ColorMatrix(
                            new[]
                            {
                                new[] { 1f, 0f, 0f, 0f, 0f },
                                new[] { 0f, 1f, 0f, 0f, 0f },
                                new[] { 0f, 0f, 1f, 0f, 0f },
                                new[] { 0f, 0f, 0f, 1f, 0f },
                                new[] { 0f, 0f, 0f, 0f, 1f }
                            });
        }

        /// <summary>
        /// Returns a ColorMatrix for the product of Matrix1 and Matrix2
        /// </summary>
        /// <param name="matrix1">The first ColorMatrix to be multiplied</param>
        /// <param name="matrix2">The second ColorMatrix to be multiplied</param>
        /// <returns>A new ColorMatrix containing the result of (Matrix1)(Matrix2)</returns>
        public static ColorMatrix Multiply(ColorMatrix matrix1, ColorMatrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
            {
                return null;
            }

            ColorMatrix result = new ColorMatrix();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    result[x, y] = 0;

                    for (int z = 0; z < 5; z++)
                    {
                        result[x, y] += matrix1[x, z] * matrix2[z, y];
                    }
                }
            }

            return result;
        }

        #endregion Public methods
    }
}