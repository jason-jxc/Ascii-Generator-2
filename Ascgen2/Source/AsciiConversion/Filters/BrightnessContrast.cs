//---------------------------------------------------------------------------------------
// <copyright file="BrightnessContrast.cs" company="Jonathan Mathews Software">
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
    using JMSoftware.Interfaces;

    /// <summary>
    /// Filter to apply Brightness/Contrast changes to the data
    /// </summary>
    public class BrightnessContrast : IBrightnessContrast, IAscgenFilter
    {
        #region Fields

        /// <summary>
        /// The brightness.
        /// </summary>
        private int brightness;

        /// <summary>
        /// Value indicating whether a value has changed since brightnessContrastLookup was last created
        /// </summary>
        private bool brightnessContrastChanged;

        /// <summary>
        /// Array to store the output value for each possible input value
        /// </summary>
        private byte[] brightnessContrastLookup;

        /// <summary>
        /// The contrast.
        /// </summary>
        private int contrast;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BrightnessContrast"/> class.
        /// </summary>
        /// <param name="brightness">Level of brightness to use (-255 to 255)</param>
        /// <param name="contrast">Level of contrast to use (-255 to 255)</param>
        public BrightnessContrast(int brightness, int contrast)
        {
            this.Brightness = brightness;
            this.Contrast = contrast;

            this.brightnessContrastChanged = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the brightness.
        /// </summary>
        /// <value>The brightness.</value>
        public int Brightness
        {
            get
            {
                return this.brightness;
            }

            set
            {
                if (this.brightness == value)
                {
                    return;
                }

                this.brightness = value;

                this.brightnessContrastChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the contrast.
        /// </summary>
        /// <value>The contrast.</value>
        public int Contrast
        {
            get
            {
                return this.contrast;
            }

            set
            {
                if (this.contrast == value)
                {
                    return;
                }

                this.contrast = value;

                this.brightnessContrastChanged = true;
            }
        }

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

            if (this.Brightness == 0 && this.Contrast == 0)
            {
                return values;
            }

            if (this.brightnessContrastChanged)
            {
                this.UpdateBrightnessContrastLookup();
            }

            int arrayWidth = values[0].Length;
            int arrayHeight = values.Length;

            byte[][] result = new byte[arrayHeight][];

            for (int i = 0; i < arrayHeight; i++)
            {
                result[i] = new byte[arrayWidth];
            }

            for (int y = 0; y < arrayHeight; y++)
            {
                for (int x = 0; x < arrayWidth; x++)
                {
                    result[y][x] = this.brightnessContrastLookup[values[y][x]];
                }
            }

            return result;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Updates the brightness contrast values lookup array.
        /// </summary>
        private void UpdateBrightnessContrastLookup()
        {
            this.brightnessContrastLookup = new byte[256];

            if (this.Brightness == 0 && this.Contrast == 0)
            {
                for (int i = 0; i < 256; i++)
                {
                    this.brightnessContrastLookup[i] = (byte)i;
                }

                return;
            }

            int minimum = 0;
            int maximum = 255;

            int offsetX = 0;

            float contrast;

            if (this.Contrast < 0)
            {
                contrast = 1f + ((float)this.Contrast / 255f);

                minimum = (int)(((255f - (255f * contrast)) / 2.0) + 0.5);
                maximum = 255 - minimum;
            }
            else
            {
                contrast = 256f / (256f - (float)this.Contrast);

                offsetX = (int)(((255f - (255f * contrast)) / 2.0) + 0.5);
            }

            int itemOffset = this.Brightness + minimum + offsetX;

            for (int i = 0; i < 256; i++)
            {
                int outputValue = (int)(((float)i * contrast) + 0.5) + itemOffset;

                if (outputValue > maximum)
                {
                    outputValue = maximum;
                }
                else
                {
                    if (outputValue < minimum)
                    {
                        outputValue = minimum;
                    }
                }

                this.brightnessContrastLookup[i] = (byte)outputValue;
            }
        }

        #endregion Private methods
    }
}