//---------------------------------------------------------------------------------------
// <copyright file="ValuesToVariableWidthTextConverter.cs" company="Jonathan Mathews Software">
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
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// Class to convert a fixed size array into strings in a specific font using the characters set
    /// </summary>
    public class ValuesToVariableWidthTextConverter : ValuesToTextConverter
    {
        #region Fields

        /// <summary>Array of (0 to 256 * this.maximumCharacterWidth + 1) / (0 to this.maximumCharacterWidth + 1)</summary>
        private int[][] averageArray;

        /// <summary>Stores the best character for every possible value and width (0->255, 0->this.maximumCharacterWidth)</summary>
        private VariableWidthCharacterValue[][] bestCharacter;

        /// <summary>Difference between the matching charactervalue.value in VariableWidthCharacterValue[,] and the actual value</summary>
        private int[][] bestCharacterDifference;

        /// <summary>Array of VariableWidthCharacterValue objects used to create the output</summary>
        private VariableWidthCharacterValue[] characters;

        /// <summary>Font used for this object</summary>
        private Font font;

        /// <summary>Are we inverting the output?</summary>
        private bool invertOutput;

        /// <summary>Largest size of character width</summary>
        private int maximumCharacterWidth;

        /// <summary>String of possible characaters to use</summary>
        private string validCharacters;

        /// <summary>Has this character width (0 to this.maximumCharacterWidth) already been used?</summary>
        private bool[] widthHasBeenUsed;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesToVariableWidthTextConverter"/> class.
        /// </summary>
        /// <param name="validCharacters">Set initial string of characters used to create the output</param>
        /// <param name="font">Font to be used</param>
        public ValuesToVariableWidthTextConverter(string validCharacters, Font font)
        {
            this.font = font;

            this.validCharacters = validCharacters;

            this.CreateArrays();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the width of one character
        /// </summary>
        /// <value>The width of the character.</value>
        public static int CharacterWidth
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to invert the output.
        /// </summary>
        /// <value><c>true</c> if inverting the output; otherwise, <c>false</c>.</value>
        public bool InvertOutput
        {
            get
            {
                return this.invertOutput;
            }

            set
            {
                if (this.invertOutput == value)
                {
                    return;
                }

                this.invertOutput = value;

                this.CreateBestCharactersArrays();
            }
        }

        /// <summary>
        /// Gets or sets the string of characters to create the output (Length > 1)
        /// </summary>
        /// <value>The valid characters.</value>
        public string ValidCharacters
        {
            get
            {
                return this.validCharacters;
            }

            set
            {
                string uniqueCharacters = string.Empty;

                for (int x = 0; x < value.Length; x++)
                {
                    if (uniqueCharacters.IndexOf(value[x]) == -1)
                    {
                        uniqueCharacters += value[x];
                    }
                }

                if (uniqueCharacters.Length < 2 || uniqueCharacters == this.validCharacters)
                {
                    return;
                }

                this.validCharacters = uniqueCharacters;

                this.CreateArrays();
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Convert 2d array of byte values into character strings
        /// </summary>
        /// <param name="values">2d array of values that represent the image</param>
        /// <returns>Array of strings containing the text image</returns>
        public override string[] Apply(byte[][] values)
        {
            if (values == null)
            {
                return null;
            }

            int numberOfColumns = values[0].Length;
            int numberOfRows = values.Length;

            if (numberOfColumns < 1 || numberOfRows < 1)
            {
                return null;
            }

            int targetWidth = numberOfColumns * CharacterWidth;

            // the corresponding character position for every output pixel
            int[] pixelToCharacterPosition = new int[targetWidth + this.maximumCharacterWidth];

            for (int characterPosition = 0, pixelPosition = 0; characterPosition < numberOfColumns; characterPosition++)
            {
                for (int i = 0; i < CharacterWidth; i++)
                {
                    pixelToCharacterPosition[pixelPosition++] = characterPosition;
                }
            }

            // set the overlapping pixels to the last character
            for (int x = targetWidth; x < targetWidth + this.maximumCharacterWidth; x++)
            {
                pixelToCharacterPosition[x] = numberOfColumns - 1;
            }

            string[] result = new string[numberOfRows];
            int characterWidthArraySize = this.maximumCharacterWidth + 1;
            int[] postionValues = new int[characterWidthArraySize];

            for (int row = 0; row < numberOfRows; row++)
            {
                int position = 0;

                StringBuilder builder = new StringBuilder();

                while (position < targetWidth)
                {
                    for (int width = 1, total = 0; width < characterWidthArraySize; width++)
                    {
                        total += values[row][pixelToCharacterPosition[position + width]];

                        if (!this.widthHasBeenUsed[width])
                        {
                            continue;
                        }

                        postionValues[width] = this.averageArray[total][width];
                    }

                    VariableWidthCharacterValue bestFit = this.bestCharacter[postionValues[this.maximumCharacterWidth]][this.maximumCharacterWidth];

                    int bestDifference = this.bestCharacterDifference[postionValues[this.maximumCharacterWidth]][this.maximumCharacterWidth];

                    for (int x = 1; x < characterWidthArraySize; x++)
                    {
                        if (!this.widthHasBeenUsed[x] || this.bestCharacterDifference[postionValues[x]][x] >= bestDifference)
                        {
                            continue;
                        }

                        bestFit = this.bestCharacter[postionValues[x]][x];

                        bestDifference = this.bestCharacterDifference[postionValues[x]][x];
                    }

                    builder.Append(bestFit.Character);

                    position += bestFit.Width;
                }

                result[row] = builder.ToString();
            }

            return result;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Create all of the arrays.
        /// </summary>
        private void CreateArrays()
        {
            this.CreateCharacterArray();

            this.CreateAverageArray();

            this.CreateBestCharactersArrays();
        }

        /// <summary>
        /// Creates the array of averages.
        /// </summary>
        private void CreateAverageArray()
        {
            int width = this.maximumCharacterWidth + 1;

            int arrayLength = 256 * width;

            this.averageArray = new int[arrayLength][];

            for (int x = 0; x < arrayLength; x++)
            {
                this.averageArray[x] = new int[width];

                for (int y = 1; y < width; y++)
                {
                    this.averageArray[x][y] = (int)(((float)x / (float)y) + 0.5f);
                }
            }
        }

        /// <summary>
        /// Create the arrays of best VariableWidthCharacterValues
        /// </summary>
        private void CreateBestCharactersArrays()
        {
            int maximumWidth = this.maximumCharacterWidth + 1;

            this.bestCharacter = new VariableWidthCharacterValue[256][];
            this.bestCharacterDifference = new int[256][];

            for (int currentValue = 0; currentValue < 256; currentValue++)
            {
                this.bestCharacter[currentValue] = new VariableWidthCharacterValue[maximumWidth];
                this.bestCharacterDifference[currentValue] = new int[maximumWidth];

                foreach (VariableWidthCharacterValue character in this.characters)
                {
                    int difference = Math.Abs(currentValue - character.Value);

                    if (this.bestCharacter[currentValue][character.Width] == null || difference < this.bestCharacterDifference[currentValue][character.Width])
                    {
                        this.bestCharacter[currentValue][character.Width] = character;
                        this.bestCharacterDifference[currentValue][character.Width] = difference;
                    }
                }
            }
        }

        /// <summary>
        /// Update the arrays for the current Font
        /// </summary>
        private void CreateCharacterArray()
        {
            int minimumValue = 255;
            int maximumValue = 0;
            this.maximumCharacterWidth = 0;

            VariableWidthCharacterValue[] characterValues = new VariableWidthCharacterValue[this.ValidCharacters.Length];

            for (int i = 0; i < this.ValidCharacters.Length; i++)
            {
                characterValues[i] = new VariableWidthCharacterValue(this.ValidCharacters[i], this.font);

                if (maximumValue < characterValues[i].Value)
                {
                    maximumValue = characterValues[i].Value;
                }

                if (minimumValue > characterValues[i].Value)
                {
                    minimumValue = characterValues[i].Value;
                }

                if (characterValues[i].Width > this.maximumCharacterWidth)
                {
                    this.maximumCharacterWidth = characterValues[i].Width;
                }
            }

            float ratio = 255f / (float)(maximumValue - minimumValue);

            this.widthHasBeenUsed = new bool[this.maximumCharacterWidth + 1];

            List<VariableWidthCharacterValue> list = new List<VariableWidthCharacterValue>();

            for (int i = 0; i < characterValues.Length; i++)
            {
                // stretch the characters value to range from 0 to 255
                characterValues[i].Value = (int)(((float)(characterValues[i].Value - minimumValue) * ratio) + 0.5f);

                this.widthHasBeenUsed[characterValues[i].Width] = true;

                bool uniqueCharacter = true;

                foreach (VariableWidthCharacterValue value in list)
                {
                    // dont add the character if one with that width and value is already in the list
                    if (characterValues[i].Width == value.Width && characterValues[i].Value == value.Value)
                    {
                        uniqueCharacter = false;
                        break;
                    }
                }

                if (uniqueCharacter)
                {
                    list.Add(characterValues[i]);
                }
            }

            this.characters = list.ToArray();
        }

        #endregion Private methods
    }
}