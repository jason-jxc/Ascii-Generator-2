//---------------------------------------------------------------------------------------
// <copyright file="Levels.cs" company="Jonathan Mathews Software">
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
    /// Filter to adjust the levels of the data
    /// </summary>
    public class Levels : ILevels, IAscgenFilter
    {
        #region Fields

        /// <summary>
        /// The maximum level
        /// </summary>
        private int maximum;

        /// <summary>
        /// The median level
        /// </summary>
        private float median;

        /// <summary>
        /// The minimum level
        /// </summary>
        private int minimum;

        /// <summary>
        /// Value indicating whether a value has changed since valuesLookup was last created
        /// </summary>
        private bool valueChanged;

        /// <summary>
        /// Array to store the output value for each possible input value
        /// </summary>
        private byte[] valuesLookup;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Levels"/> class.
        /// </summary>
        public Levels()
            : this(0, 255, 0.5f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Levels"/> class.
        /// </summary>
        /// <param name="minimum">Minimum level (0 to maximum - 1)</param>
        /// <param name="maximum">Maximum level (minimum + 1 to 255)</param>
        /// <param name="median">Median value (0.0 to 1.0)</param>
        public Levels(int minimum, int maximum, float median)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Median = median;

            this.valueChanged = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                if (this.maximum == value)
                {
                    return;
                }

                this.maximum = value;

                this.valueChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the median.
        /// </summary>
        /// <value>The median.</value>
        public float Median
        {
            get
            {
                return this.median;
            }

            set
            {
                if (this.median == value)
                {
                    return;
                }

                this.median = value;

                this.valueChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                if (this.minimum == value)
                {
                    return;
                }

                this.minimum = value;

                this.valueChanged = true;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Implementation of the apply function
        /// </summary>
        /// <param name="values">Input values</param>
        /// <returns>Output values</returns>
        public byte[][] Apply(byte[][] values)
        {
            if (values == null || this.Minimum < 0 || this.Maximum < 0 || this.Minimum > this.Maximum || this.Maximum > 255 || this.Minimum > 255)
            {
                return null;
            }

            if (this.Minimum == 0 && this.Maximum == 255 && this.Median == 0.5)
            {
                return values;
            }

            if (this.valueChanged)
            {
                this.UpdateValuesLookup();
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
                    result[y][x] = this.valuesLookup[values[y][x]];
                }
            }

            this.valueChanged = false;

            return result;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Updates the values lookup array.
        /// </summary>
        private void UpdateValuesLookup()
        {
            this.valuesLookup = new byte[256];

            if (this.Minimum == 0 && this.Maximum == 255 && this.Median == 0.5)
            {
                for (int i = 0; i < 256; i++)
                {
                    this.valuesLookup[i] = (byte)i;
                }

                return;
            }

            for (int x = 0; x < this.Minimum; x++)
            {
                this.valuesLookup[x] = 0;
            }

            int mid = (int)(((float)(this.Maximum - this.Minimum) * this.Median) + 0.5f) + this.Minimum;

            float ratio = 128f / (float)(mid - this.Minimum);

            for (int x = this.Minimum; x < mid; x++)
            {
                this.valuesLookup[x] = (byte)(((float)(x - this.Minimum) * ratio) + 0.5f);
            }

            ratio = 128f / (float)(this.Maximum - mid);

            for (int x = mid; x < this.Maximum; x++)
            {
                this.valuesLookup[x] = (byte)(((float)(x - mid) * ratio) + 128.5f);
            }

            for (int x = this.Maximum; x < 256; x++)
            {
                this.valuesLookup[x] = 255;
            }
        }

        #endregion Private methods
    }
}