//---------------------------------------------------------------------------------------
// <copyright file="DimensionsCalculator.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.Drawing;

    /// <summary>
    /// Class used to calculate output dimensions for a specified input and character size
    /// </summary>
    public class DimensionsCalculator
    {
        #region Fields

        /// <summary>
        /// The size of a character
        /// </summary>
        private Size characterSize;

        /// <summary>
        /// Does the output image have to have the same aspect ratio as the input image?
        /// </summary>
        private bool dimensionsAreLocked = true;

        /// <summary>
        /// Size of the input image in pixels
        /// </summary>
        private Size imageSize;

        /// <summary>
        /// Size of the output image in characters
        /// </summary>
        private Size outputSize = new Size(150, 0);

        /// <summary>
        /// Was the width the last value to be changed?
        /// </summary>
        private bool widthChangedLast = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionsCalculator"/> class.
        /// </summary>
        /// <param name="imageSize">Size of the image.</param>
        /// <param name="characterSize">Size of the character.</param>
        /// <param name="width">The width of the output.</param>
        /// <param name="height">The height of the output.</param>
        public DimensionsCalculator(Size imageSize, Size characterSize, int width, int height)
        {
            this.imageSize = imageSize;

            this.characterSize = characterSize;

            this.OutputSize = new Size(width, height);
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>
        /// Occurs when the output size has changed.
        /// </summary>
        public event EventHandler OnOutputSizeChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the size of a character.
        /// </summary>
        /// <value>The size of a character.</value>
        public Size CharacterSize
        {
            get
            {
                return this.characterSize;
            }

            set
            {
                if (this.characterSize == value)
                {
                    return;
                }

                this.characterSize = value;

                if (!this.DimensionsAreLocked)
                {
                    return;
                }

                this.CalculateOtherDimension();

                this.UpdateEvents();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output dimensions are locked.
        /// </summary>
        /// <value><c>true</c> if dimensions are locked; otherwise, <c>false</c>.</value>
        public bool DimensionsAreLocked
        {
            get
            {
                return this.dimensionsAreLocked;
            }

            set
            {
                if (this.dimensionsAreLocked == value)
                {
                    return;
                }

                this.dimensionsAreLocked = value;

                if (!this.dimensionsAreLocked)
                {
                    return;
                }

                Size oldSize = this.outputSize;

                this.CalculateOtherDimension();

                if (this.outputSize != oldSize)
                {
                    this.UpdateEvents();
                }
            }
        }

        /// <summary>
        /// Gets or sets the output height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return this.outputSize.Height;
            }

            set
            {
                if (this.outputSize.Height == value)
                {
                    return;
                }

                this.outputSize.Height = value;

                this.widthChangedLast = false;

                if (this.dimensionsAreLocked)
                {
                    this.CalculateWidth();
                }

                this.UpdateEvents();
            }
        }

        /// <summary>
        /// Gets or sets the size of the image.
        /// </summary>
        /// <value>The size of the image.</value>
        public Size ImageSize
        {
            get
            {
                return this.imageSize;
            }

            set
            {
                if (this.imageSize == value)
                {
                    return;
                }

                this.imageSize = value;

                if (this.DimensionsAreLocked)
                {
                    this.CalculateOtherDimension();
                }

                this.UpdateEvents();
            }
        }

        /// <summary>
        /// Gets or sets the size of the output.
        /// </summary>
        /// <value>The size of the output.</value>
        public Size OutputSize
        {
            get
            {
                return this.outputSize;
            }

            set
            {
                if (value.Width < 1 && value.Height < 1)
                {
                    return;
                }

                this.dimensionsAreLocked = value.Width < 0 || value.Height < 0;

                this.outputSize = new Size(-1, -1);

                if (this.dimensionsAreLocked)
                {
                    if (value.Height < 0)
                    {
                        this.Width = value.Width;
                    }
                    else
                    {
                        this.Height = value.Height;
                    }
                }
                else
                {
                    this.Height = value.Height;
                    this.Width = value.Width;
                }
            }
        }

        /// <summary>
        /// Gets or sets the output width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get
            {
                return this.outputSize.Width;
            }

            set
            {
                if (this.outputSize.Width == value)
                {
                    return;
                }

                this.outputSize.Width = value;

                this.widthChangedLast = true;

                if (this.dimensionsAreLocked)
                {
                    this.CalculateHeight();
                }

                this.UpdateEvents();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the width was the last dimension to be changed.
        /// </summary>
        /// <value><c>true</c> if width changed last; otherwise, <c>false</c>.</value>
        public bool WidthChangedLast
        {
            get
            {
                return this.widthChangedLast;
            }
        }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Calculate the other dimension (b) from the known dimension (a)
        /// </summary>
        /// <remarks>i.e. if width is known pass widths for all (a) values, and heights for all (b) values</remarks>
        /// <param name="dimension">the known dimension (a)</param>
        /// <param name="imageDimension">image dimension (a)</param>
        /// <param name="otherImageDimension">image dimension (b)</param>
        /// <param name="characterDimension">character dimension (a) - dimension of one character in the font</param>
        /// <param name="otherCharacterDimension">character dimension (b) - dimension of one character in the font</param>
        /// <returns>the other dimension (b)</returns>
        private static int CalculateOtherDimension(int dimension, int imageDimension, int otherImageDimension, int characterDimension, int otherCharacterDimension)
        {
            if (dimension == 0 || imageDimension == 0 || otherImageDimension == 0 || characterDimension == 0 || otherCharacterDimension == 0)
            {
                return 0;
            }

            float result = (float)(dimension * characterDimension * otherImageDimension) / (float)(imageDimension * otherCharacterDimension);

            return (int)(result + 0.5);
        }

        /// <summary>
        /// Calculates the height.
        /// </summary>
        private void CalculateHeight()
        {
            this.outputSize.Height = CalculateOtherDimension(
                                            this.outputSize.Width,
                                            this.ImageSize.Width,
                                            this.ImageSize.Height,
                                            this.characterSize.Width,
                                            this.characterSize.Height);
        }

        /// <summary>
        /// Calculates the other dimension.
        /// </summary>
        private void CalculateOtherDimension()
        {
            if (this.widthChangedLast)
            {
                this.CalculateHeight();
            }
            else
            {
                this.CalculateWidth();
            }
        }

        /// <summary>
        /// Calculates the width.
        /// </summary>
        private void CalculateWidth()
        {
            this.outputSize.Width = CalculateOtherDimension(
                                            this.outputSize.Height,
                                            this.ImageSize.Height,
                                            this.ImageSize.Width,
                                            this.characterSize.Height,
                                            this.characterSize.Width);
        }

        /// <summary>
        /// Updates the events.
        /// </summary>
        private void UpdateEvents()
        {
            if (this.OnOutputSizeChanged != null)
            {
                this.OnOutputSizeChanged(this, new EventArgs());
            }
        }

        #endregion Private methods
    }
}