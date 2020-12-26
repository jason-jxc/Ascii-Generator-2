//---------------------------------------------------------------------------------------
// <copyright file="VariableWidthCharacterValue.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Drawing.Imaging;
    using JMSoftware.TextHelper;

    /// <summary>Class used to store variables width characters and their values</summary>
    public class VariableWidthCharacterValue
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableWidthCharacterValue"/> class.
        /// </summary>
        /// <param name="character">The initial character for this object</param>
        /// <param name="font">Font to use</param>
        public VariableWidthCharacterValue(char character, Font font)
        {
            this.Character = character;
            this.Value = 0;
            this.Width = 0;
            this.CalculateValue(font);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableWidthCharacterValue"/> class.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="width">The width.</param>
        /// <param name="characterValue">The characters value.</param>
        public VariableWidthCharacterValue(char character, int width, int characterValue)
        {
            this.Character = character;
            this.Width = width;
            this.Value = characterValue;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>The character.</value>
        public char Character { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the width of the character.
        /// </summary>
        /// <value>The width of the character.</value>
        public int Width { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Update Value for the passed font
        /// </summary>
        /// <param name="font">Font to be used</param>
        public void CalculateValue(Font font)
        {
            using (Bitmap bitmap = TextToImage.Convert(this.Character.ToString(), font))
            {
                if (bitmap == null)
                {
                    this.Value = -1;

                    return;
                }

                this.Width = bitmap.Width;

                int total = 0;

                unsafe
                {
                    BitmapData data = bitmap.LockBits(
                                                    new Rectangle(new Point(0, 0), bitmap.Size),
                                                    ImageLockMode.ReadOnly,
                                                    PixelFormat.Format24bppRgb);

                    byte* pointer = (byte*)data.Scan0;

                    int padding = data.Stride - (bitmap.Width * 3);

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            total += (int)(((float)(pointer[2] + pointer[1] + pointer[0]) / 3f) + 0.5);

                            pointer += 3;
                        }

                        pointer += padding;
                    }

                    bitmap.UnlockBits(data);
                }

                this.Value = (int)(((float)total / (float)(this.Width * bitmap.Height)) + 0.5);
            }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return this.Character.ToString();
        }

        #endregion Public methods
    }
}
