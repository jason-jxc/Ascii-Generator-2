//---------------------------------------------------------------------------------------
// <copyright file="AscgenConverter.cs" company="Jonathan Mathews Software">
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
    using System.Collections;
    using System.Drawing;
    using JMSoftware.AsciiConversion.Filters;
    using JMSoftware.ImageHelper;

    /// <summary>
    /// Static class with methods for Image to Text conversion
    /// </summary>
    public static class AscgenConverter
    {
        #region Constructors

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Convert the image into an array of strings using the passed settings
        /// </summary>
        /// <param name="image">image to convert</param>
        /// <param name="settings">the text settings to use for the output image</param>
        /// <returns>the converted text image</returns>
        public static string[] Convert(Image image, TextProcessingSettings settings)
        {
            if (image == null || settings == null || settings.Width < 1 || settings.Height < 1)
            {
                return null;
            }

            return Convert(ImageToValues.Convert(image, settings.Size), settings);
        }

        /// <summary>
        /// Convert the the passed values into an array of strings using the passed settings
        /// </summary>
        /// <param name="values">array of values to use</param>
        /// <param name="settings">the text settings to use for the output image</param>
        /// <returns>the converted text image</returns>
        public static string[] Convert(byte[][] values, TextProcessingSettings settings)
        {
            if (settings == null)
            {
                return null;
            }

            ArrayList filters = settings.FilterList;

            foreach (IAscgenFilter filter in filters)
            {
                values = filter.Apply(values);
            }

            return settings.ValuesToText.Apply(values);
        }

        #endregion Public methods
    }
}