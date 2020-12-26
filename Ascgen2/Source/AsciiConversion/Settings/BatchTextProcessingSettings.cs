//---------------------------------------------------------------------------------------
// <copyright file="BatchTextProcessingSettings.cs" company="Jonathan Mathews Software">
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
    using System.ComponentModel;

    /// <summary>
    /// Settings used for the batch conversions.
    /// </summary>
    public class BatchTextProcessingSettings : TextProcessingSettings
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchTextProcessingSettings"/> class.
        /// </summary>
        public BatchTextProcessingSettings()
        {
            this.Prefix = "ASCII-";
        }

        #endregion Constructors

        #region Enums

        /// <summary>
        /// List of possible suffix types
        /// </summary>
        public enum SuffixTypes
        {
            /// <summary>Use the specified string</summary>
            Custom,

            /// <summary>Use a date/time string</summary>
            DateTime,

            /// <summary>Use a random string</summary>
            Random
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        [DisplayName("Prefix"), CategoryAttribute("Output Filename"), DefaultValueAttribute("ASCII-"), DescriptionAttribute("Custom prefix for the filenames")]
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>The suffix.</value>
        [DisplayName("Suffix"), CategoryAttribute("Output Filename"), DefaultValueAttribute(""), DescriptionAttribute("Custom suffix for the filenames")]
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the type of the suffix.
        /// </summary>
        /// <value>The type of the suffix.</value>
        [DisplayName("Suffix Type"), CategoryAttribute("Output Filename"), DefaultValueAttribute(SuffixTypes.Custom), DescriptionAttribute("Type of suffix to use for the filenames")]
        public SuffixTypes SuffixType { get; set; }

        #endregion Properties
    }
}