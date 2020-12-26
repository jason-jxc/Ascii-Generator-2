//---------------------------------------------------------------------------------------
// <copyright file="ImageToValues.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.ImageHelper
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    /// <summary>
    /// Class to handle converting the image into an array of values
    /// </summary>
    public static class ImageToValues
    {
        #region Public methods

        /// <summary>
        /// Convert an Image to an OutputSize array of Byte values
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <param name="outputSize">Size of the array to be returned</param>
        /// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
        public static byte[][] Convert(Image image, Size outputSize)
        {
            if (image == null)
            {
                return null;
            }

            return Convert(image, outputSize, Matrices.Identity());
        }

        /// <summary>
        /// Modify and convert an Image to an OutputSize array of Byte values
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <param name="outputSize">Size of the array to be returned</param>
        /// <param name="matrix">ColorMatrix to be applied to the image</param>
        /// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
        public static byte[][] Convert(Image image, Size outputSize, ColorMatrix matrix)
        {
            if (image == null || matrix == null)
            {
                return null;
            }

            return Convert(image, outputSize, matrix, new Rectangle(0, 0, image.Width, image.Height));
        }

        /// <summary>
        /// Modify and convert an Image to an OutputSize array of Byte values
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <param name="outputSize">Size of the array to be returned</param>
        /// <param name="matrix">ColorMatrix to be applied to the image</param>
        /// <param name="section">The part of the image to be used</param>
        /// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
        public static byte[][] Convert(Image image, Size outputSize, ColorMatrix matrix, Rectangle section)
        {
            if (image == null || matrix == null)
            {
                return null;
            }

            byte[][] result;

            try
            {
                result = new byte[outputSize.Height][];

                for (int i = 0; i < outputSize.Height; i++)
                {
                    result[i] = new byte[outputSize.Width];
                }
            }
            catch (OutOfMemoryException)
            {
                return null;
            }

            Rectangle targetSize = new Rectangle(0, 0, outputSize.Width, outputSize.Height);

            using (Bitmap resized = new Bitmap(outputSize.Width, outputSize.Height))
            {
                // draw a resized version onto the new bitmap
                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(
                            image,
                            targetSize,
                            section.X,
                            section.Y,
                            section.Width,
                            section.Height,
                            GraphicsUnit.Pixel);
                }

                // copy the resized version onto a new bitmap, applying the matrix
                using (Bitmap target = new Bitmap(outputSize.Width, outputSize.Height))
                {
                    // create the image attributes and pass it the matrix
                    using (ImageAttributes imageAttributes = new ImageAttributes())
                    {
                        // merge the passed matrix with the matrix for converting to greyscale
                        ColorMatrix matrices = Matrices.Multiply(Matrices.Grayscale(), matrix);

                        imageAttributes.SetColorMatrix(matrices);

                        using (Graphics graphics = Graphics.FromImage(target))
                        {
                            graphics.DrawImage(
                                    resized,
                                    targetSize,
                                    0,
                                    0,
                                    outputSize.Width,
                                    outputSize.Height,
                                    GraphicsUnit.Pixel,
                                    imageAttributes);
                        }
                    }
                    
                    BitmapData data = target.LockBits(targetSize, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                    int padding = data.Stride - (outputSize.Width * 3);

                    unsafe
                    {
                        byte* pointer = (byte*)data.Scan0;

                        for (int y = 0; y < outputSize.Height; y++)
                        {
                            for (int x = 0; x < outputSize.Width; x++)
                            {
                                result[y][x] = pointer[2];
                                
                                pointer += 3;
                            }

                            pointer += padding;
                        }
                    }

                    target.UnlockBits(data);
                }
            }

            return result;
        }

        #endregion Public methods
    }
}