//---------------------------------------------------------------------------------------
// <copyright file="LevelsSettings.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiConversion.TextSettings
{
    using System.ComponentModel;

    /// <summary>
    /// Class used to show levels on a PropertyGrid
    /// </summary>
    [TypeConverterAttribute(typeof(LevelsSettingsConverter))]
    public class LevelsSettings
    {
        #region Fields

        /// <summary>
        /// The maximum value.
        /// </summary>
        private int maximum = 255;

        /// <summary>
        /// The median value.
        /// </summary>
        private float median = 0.5f;

        /// <summary>
        /// The minimum value.
        /// </summary>
        private int minimum;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelsSettings"/> class.
        /// </summary>
        public LevelsSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelsSettings"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="median">The median.</param>
        /// <param name="maximum">The maximum.</param>
        public LevelsSettings(int minimum, float median, int maximum)
        {
            this.Minimum = minimum;
            this.Median = median;
            this.Maximum = maximum;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [DefaultValueAttribute(255), NotifyParentProperty(true), DescriptionAttribute("Maximum level (0 to 255)")]
        public int Maximum
        {
            get
            {
                return this.maximum;
            }

            set
            {
                if (value > 255)
                {
                    value = 255;
                }
                else if (value < this.Minimum + 2)
                {
                    value = this.Minimum + 2;
                }

                if (this.maximum == value)
                {
                    return;
                }

                this.maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the median value.
        /// </summary>
        /// <value>The median value.</value>
        [DefaultValueAttribute(0.5f), NotifyParentProperty(true), DescriptionAttribute("Median level (0.0 to 1.0)")]
        public float Median
        {
            get
            {
                return this.median;
            }

            set
            {
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                if (this.median == value)
                {
                    return;
                }

                this.median = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [DefaultValueAttribute(0), NotifyParentProperty(true), DescriptionAttribute("Minimum level (0 to 255)")]
        public int Minimum
        {
            get
            {
                return this.minimum;
            }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > this.Maximum - 2)
                {
                    value = this.Maximum - 2;
                }

                if (this.minimum == value)
                {
                    return;
                }

                this.minimum = value;
            }
        }

        #endregion Properties
    }
}