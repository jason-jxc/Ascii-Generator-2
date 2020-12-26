//---------------------------------------------------------------------------------------
// <copyright file="CharacterValue.cs" company="Jonathan Mathews Software">
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

    /// <summary>
    /// Class used to store values of characters for the AsciiRampCreator
    /// </summary>
    public class CharacterValue
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterValue"/> class.
        /// </summary>
        /// <param name="character">Character to use</param>
        /// <param name="font">Font to use</param>
        public CharacterValue(char character, Font font)
        {
            this.Character = character;

            this.CalculateValues(font);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the character.
        /// </summary>
        /// <value>The character.</value>
        public char Character { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        /// <remarks>This shows the deviation of the character from its Value</remarks>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// <remarks>The average value of the character's pixels</remarks>
        public int Value { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Calculate the values for the current character
        /// </summary>
        /// <param name="font">font to be used</param>
        public void CalculateValues(Font font)
        {
            // size of the shrunken character to use for the calculation
            const int Width = 4;
            const int Height = 4;
            const float Area = 16f;

            using (Bitmap bitmap = TextToImage.Convert(this.Character.ToString(), font))
            {
                using (Bitmap shrunk = new Bitmap(Width, Height))
                {
                    using (Graphics g = Graphics.FromImage(shrunk))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        g.Clear(Color.White);

                        g.DrawImage(bitmap, 0, 0, Width, Height);
                    }

                    unsafe
                    {
                        BitmapData data = shrunk.LockBits(
                                                        new Rectangle(new Point(0, 0), shrunk.Size),
                                                        ImageLockMode.ReadOnly,
                                                        PixelFormat.Format24bppRgb);

                        byte* pointer = (byte*)data.Scan0;

                        int padding = data.Stride - (shrunk.Width * 3);

                        int totalValue = 0;

                        for (int y = 0; y < shrunk.Height; y++)
                        {
                            for (int x = 0; x < shrunk.Width; x++)
                            {
                                totalValue += pointer[2];

                                pointer += 3;
                            }

                            pointer += padding;
                        }

                        // store the average value
                        this.Value = (int)(((float)totalValue / Area) + 0.5);

                        int totalDifference = 0;

                        pointer = (byte*)data.Scan0;

                        for (int y = 0; y < shrunk.Height; y++)
                        {
                            for (int x = 0; x < shrunk.Width; x++)
                            {
                                // add the difference between the average value and this pixel
                                totalDifference += this.Value > pointer[2] ? this.Value - pointer[2] : pointer[2] - this.Value;

                                pointer += 3;
                            }

                            pointer += padding;
                        }

                        // store the score for this character
                        this.Score = (int)(((float)totalDifference / Area) + 0.5);

                        shrunk.UnlockBits(data);
                    }
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return this.Character.ToString();
        }

        #endregion Public methods
    }
}
