//---------------------------------------------------------------------------------------
// <copyright file="TextToImage.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.TextHelper
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Class to handle text to image conversion
    /// </summary>
    public static class TextToImage
    {
        #region Public methods

        /// <summary>
        /// Draw the text onto a new bitmap, with the specified font
        /// </summary>
        /// <param name="text">The text to be drawn</param>
        /// <param name="font">The font to use</param>
        /// <returns>A new bitmap containing the text</returns>
        public static Bitmap Convert(string text, Font font)
        {
            return Convert(text, font, Color.Black, Color.White);
        }

        /// <summary>
        /// Draw the text onto a new bitmap, with the specified font and text colours
        /// </summary>
        /// <param name="text">The text to be drawn</param>
        /// <param name="font">The font to use</param>
        /// <param name="textColor">The colour of the text</param>
        /// <param name="backgroundColor">The colour of the background</param>
        /// <returns>A new bitmap containing the text</returns>
        public static Bitmap Convert(string text, Font font, Color textColor, Color backgroundColor)
        {
            return Convert(text, font, textColor, backgroundColor, new Point(0, 0));
        }

        /// <summary>
        /// Draw the text onto a new bitmap at the specified point, with the specified font and text colours
        /// </summary>
        /// <param name="text">The text to be drawn</param>
        /// <param name="font">The font to use</param>
        /// <param name="textColor">The colour of the text</param>
        /// <param name="backgroundColor">The colour of the background</param>
        /// <param name="offset">The amount by which to offset the text</param>
        /// <returns>A new bitmap containing the text</returns>
        public static Bitmap Convert(string text, Font font, Color textColor, Color backgroundColor, Point offset)
        {
            Size size = FontFunctions.MeasureText(text, font);

            Bitmap result = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(backgroundColor);

                TextRenderer.DrawText(
                                    g,
                                    text,
                                    font,
                                    offset,
                                    textColor,
                                    backgroundColor,
                                    TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            }

            return result;
        }

        /// <summary>
        /// Save the text as an image file, will overwrite it if the file already exists
        /// </summary>
        /// <param name="text">The text to save</param>
        /// <param name="filename">Filename to save</param>
        /// <param name="font">Font to use for the text</param>
        /// <param name="textColor">Color of the text</param>
        /// <param name="backgroundColor">Color of the background</param>
        /// <param name="scale">Percentage scale of the image, 1.0-100.0</param>
        /// <param name="greyscale">Save the image as greyscale?</param>
        /// <returns>Did the file save without errors?</returns>
        public static bool Save(string text, string filename, Font font, Color textColor, Color backgroundColor, float scale, bool greyscale)
        {
            using (Bitmap fullSizeBitmap = Convert(text, font, textColor, backgroundColor))
            {
                if (scale == 100f)
                {
                    fullSizeBitmap.Save(
                                    filename,
                                    GetImageFormat(Path.GetExtension(filename).ToUpperInvariant()));

                    return true;
                }

                float magnification = scale / 100f;

                Size newSize = new Size(
                                    (int)((fullSizeBitmap.Width * magnification) + 0.5),
                                    (int)((fullSizeBitmap.Height * magnification) + 0.5));

                newSize.Width = Math.Max(newSize.Width, 1);
                newSize.Height = Math.Max(newSize.Height, 1);

                using (Bitmap outputBitmap = new Bitmap(newSize.Width, newSize.Height))
                {
                    using (ImageAttributes ia = new ImageAttributes())
                    {
                        if (greyscale)
                        {
                            ia.SetColorMatrix(Grayscale());
                        }

                        using (Graphics g = Graphics.FromImage(outputBitmap))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                            g.DrawImage(
                                    fullSizeBitmap,
                                    new Rectangle(0, 0, newSize.Width, newSize.Height),
                                    0,
                                    0,
                                    fullSizeBitmap.Width,
                                    fullSizeBitmap.Height,
                                    GraphicsUnit.Pixel,
                                    ia);
                        }
                    }

                    outputBitmap.Save(
                                    filename,
                                    GetImageFormat(Path.GetExtension(filename).ToUpperInvariant()));
                }
            }

            return true;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Get the image format for the passed extension string
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>The image format</returns>
        private static ImageFormat GetImageFormat(string extension)
        {
            Guid result;

            switch (extension)
            {
                case ".PNG":
                    result = ImageFormat.Png.Guid;
                    break;
                case ".JPG":
                case ".JPEG":
                case ".JPE":
                    result = ImageFormat.Jpeg.Guid;
                    break;
                case ".GIF":
                    result = ImageFormat.Gif.Guid;
                    break;
                case ".TIF":
                case ".TIFF":
                    result = ImageFormat.Tiff.Guid;
                    break;
                default: // bmp, rle, dib
                    result = ImageFormat.Bmp.Guid;
                    break;
            }

            return new ImageFormat(result);
        }

        /// <summary>
        /// Returns a ColorMatrix to convert an image into grayscale
        /// </summary>
        /// <returns>ColorMatrix to convert an image into grayscale</returns>
        private static ColorMatrix Grayscale()
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

        #endregion Private methods
    }
}