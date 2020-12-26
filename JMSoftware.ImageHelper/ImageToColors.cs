//---------------------------------------------------------------------------------------
// <copyright file="ImageToColors.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Class to handle converting an image into an array of Colors
    /// </summary>
    public static class ImageToColors
    {
        #region Public methods

        /// <summary>
        /// Process the passed image into an outputSize array of Colors
        /// </summary>
        /// <param name="image">Source image</param>
        /// <param name="outputSize">Size of the output array</param>
        /// <param name="reduceColors">Reduce the number of colors to no more then 256?</param>
        /// <returns>An outputSize array of Colors</returns>
        public static Color[][] Convert(Image image, Size outputSize, bool reduceColors)
        {
            if (image == null)
            {
                return null;
            }

            return Convert(
                        image,
                        outputSize,
                        new Rectangle(0, 0, image.Width, image.Height),
                        reduceColors);
        }

        /// <summary>
        /// Process the specified section of the passed image into an outputSize array of Colors
        /// </summary>
        /// <param name="image">Source image</param>
        /// <param name="outputSize">Size of the output array</param>
        /// <param name="section">Section of the image to use</param>
        /// <param name="reducingColors">Reduce the number of colors to no more then 256?</param>
        /// <returns>An outputSize array of Colors</returns>
        public static Color[][] Convert(Image image, Size outputSize, Rectangle section, bool reducingColors)
        {
            Color[][] outputArray;

            // create the resized and cropped image
            using (Bitmap resizedImage = new Bitmap(outputSize.Width, outputSize.Height))
            {
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(
                            image,
                            new Rectangle(0, 0, outputSize.Width, outputSize.Height),
                            section.X,
                            section.Y,
                            section.Width,
                            section.Height,
                            GraphicsUnit.Pixel);
                }

                if (reducingColors)
                {
                    // File name for the temporary file
                    string temporaryFilename = Path.GetTempFileName();

                    // Save as a 256 color gif file
                    resizedImage.Save(temporaryFilename, ImageFormat.Gif);

                    // load into an image
                    using (Bitmap gifImage = (Bitmap)Image.FromFile(temporaryFilename))
                    {
                        outputArray = BuildOutputArray(outputSize, gifImage);
                    }

                    try
                    {
                        // remove the temporary file
                        File.Delete(temporaryFilename);
                    }
                    catch
                    {
                    }
                }
                else
                {
                    // Build the output array from the 24-bit image
                    outputArray = BuildOutputArray(outputSize, resizedImage);
                }
            }

            return outputArray;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Builds the output array.
        /// </summary>
        /// <param name="outputSize">Size of the output.</param>
        /// <param name="resizedImage">The resized image.</param>
        /// <returns>The new array of colors</returns>
        private static Color[][] BuildOutputArray(Size outputSize, Bitmap resizedImage)
        {
            Color[][] result;

            try
            {
                result = new Color[outputSize.Height][];

                for (int i = 0; i < outputSize.Height; i++)
                {
                    result[i] = new Color[outputSize.Width];
                }
            }
            catch (System.OutOfMemoryException)
            {
                return null;
            }

            BitmapData data = resizedImage.LockBits(new Rectangle(new Point(0, 0), outputSize), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int padding = data.Stride - (outputSize.Width * 3);

            unsafe
            {
                byte* pointer = (byte*)data.Scan0;

                for (int y = 0; y < outputSize.Height; y++)
                {
                    for (int x = 0; x < outputSize.Width; x++)
                    {
                        result[y][x] = Color.FromArgb(pointer[2], pointer[1], pointer[0]);

                        pointer += 3;
                    }

                    pointer += padding;
                }
            }

            return result;
        }

        #endregion Private methods
    }
}