//---------------------------------------------------------------------------------------
// <copyright file="ImageFunctions.cs" company="Jonathan Mathews Software">
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
    using System.Drawing.Imaging;

    /// <summary>
    /// Class containing image related functions
    /// </summary>
    public abstract class ImageFunctions
    {
        #region Public methods

        /// <summary>
        /// Get the image format for the passed extension string
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>The image format</returns>
        public static ImageFormat GetImageFormat(string extension)
        {
            Guid result;

            switch (extension)
            {
                case ".png":
                    result = ImageFormat.Png.Guid;
                    break;
                case ".jpg":
                case ".jpeg":
                case ".jpe":
                    result = ImageFormat.Jpeg.Guid;
                    break;
                case ".gif":
                    result = ImageFormat.Gif.Guid;
                    break;
                case ".tif":
                case ".tiff":
                    result = ImageFormat.Tiff.Guid;
                    break;
                default: // bmp, rle, dib
                    result = ImageFormat.Bmp.Guid;
                    break;
            }

            return new ImageFormat(result);
        }

        #endregion Public methods
    }
}