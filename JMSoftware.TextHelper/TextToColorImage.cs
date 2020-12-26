//---------------------------------------------------------------------------------------
// <copyright file="TextToColorImage.cs" company="Jonathan Mathews Software">
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

    /// <summary>
    /// Class to handle text to color image conversions
    /// </summary>
    public static class TextToColorImage
    {
        #region Public methods

        /// <summary>
        /// Draw the text onto a new bitmap with the specified font and colours
        /// </summary>
        /// <param name="text">The text to be drawn</param>
        /// <param name="font">Font to use</param>
        /// <param name="textColors">2d array of colours to use (must have one for each character)</param>
        /// <param name="backgroundColor">Color to use behind the text</param>
        /// <param name="scale">Percentage scale of the image, 1.0-100.0</param>
        /// <returns>Image containing the coloured text</returns>
        public static Image Convert(string[] text, Font font, Color[][] textColors, Color backgroundColor, float scale)
        {
            if (!FontFunctions.IsFixedWidth(font) || text == null || textColors == null)
            {
                return null;
            }

            // size the output image must be to fit the text
            Size size = FontFunctions.MeasureText(FontFunctions.StringArrayToString(text), font);

            // size of one character in the font
            Size characterSize = FontFunctions.GetFixedPitchFontSize(font);

            Bitmap fullSize = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(fullSize))
            {
                g.Clear(backgroundColor);

                int width = textColors[0].Length;
                int height = textColors.Length;

                for (int y = 0; y < height; y++)
                {
                    string line = text[y];

                    for (int x = 0; x < width; x++)
                    {
                        if (textColors[y][x] == backgroundColor)
                        {
                            continue;
                        }

                        Point offset = new Point(x * characterSize.Width, y * characterSize.Height);

                        using (SolidBrush brush = new SolidBrush(textColors[y][x]))
                        {
                            g.DrawString(
                                        line[x].ToString(),
                                        font,
                                        brush,
                                        offset,
                                        StringFormat.GenericTypographic);
                        }
                    }
                }
            }

            if (scale == 100f)
            {
                return fullSize;
            }

            float magnification = scale / 100f;

            Size newSize = new Size(
                                (int)((fullSize.Width * magnification) + 0.5),
                                (int)((fullSize.Height * magnification) + 0.5));

            newSize.Width = Math.Max(newSize.Width, 1);
            newSize.Height = Math.Max(newSize.Height, 1);

            Bitmap resized = new Bitmap(newSize.Width, newSize.Height);

            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(fullSize, new Rectangle(0, 0, newSize.Width, newSize.Height));
            }

            fullSize.Dispose();

            return resized;
        }

        #endregion Public methods
    }
}