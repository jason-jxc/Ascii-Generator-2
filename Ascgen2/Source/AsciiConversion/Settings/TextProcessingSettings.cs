//---------------------------------------------------------------------------------------
// <copyright file="TextProcessingSettings.cs" company="Jonathan Mathews Software">
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
    using System.ComponentModel;
    using System.Drawing;
    using JMSoftware.AsciiConversion.Filters;
    using JMSoftware.AsciiConversion.TextSettings;
    using JMSoftware.AsciiGeneratorDotNet;
    using JMSoftware.TextHelper;

    /// <summary>
    /// Settings used for the conversions.
    /// </summary>
    public class TextProcessingSettings
    {
        #region Fields

        /// <summary>Brightness of the text image</summary>
        private int brightness;

        /// <summary>Are we calculating the characters size?</summary>
        private bool calculateCharacterSize;

        /// <summary>Size of one character</summary>
        private Size characterSize;

        /// <summary>Contrast of the text image</summary>
        private int contrast;

        /// <summary>Dithering level</summary>
        private int dithering;

        /// <summary>Ditering random level</summary>
        private int ditheringRandom;

        /// <summary>Have the filters changed?</summary>
        private bool filterChanged;

        /// <summary>List of filters to use on the image</summary>
        private ArrayList filterList;

        /// <summary>Flip the text horizontally?</summary>
        private bool flipHorizontally;

        /// <summary>Flip the text vertically?</summary>
        private bool flipVertically;

        /// <summary>Font for the image</summary>
        private Font font;

        /// <summary>Are we using black text on a white background?</summary>
        private bool isBlackTextOnWhite;

        /// <summary>Is this font fixed-width</summary>
        private bool isFixedWidth;

        /// <summary>The levels adjustment values</summary>
        private LevelsSettings levels;

        /// <summary>Character ramp for the conversions</summary>
        private string ramp;

        /// <summary>Are we sharpening the image?</summary>
        private bool sharpen;

        /// <summary>Sharpening filter</summary>
        private Sharpen sharpenFilter;

        /// <summary>Are we stretching the image?</summary>
        private bool stretch;

        /// <summary>Stretching filter</summary>
        private Stretch stretchFilter;

        /// <summary>Are we using an unsharp mask on the image?</summary>
        private bool unsharp;

        /// <summary>Unsharp mask filter</summary>
        private UnsharpMask unsharpFilter;

        /// <summary>All possible character to use when generating the ramp</summary>
        private string validCharacters;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextProcessingSettings"/> class.
        /// </summary>
        public TextProcessingSettings()
            : this(new Size(Variables.Instance.DefaultWidth, Variables.Instance.DefaultHeight))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextProcessingSettings"/> class.
        /// </summary>
        /// <param name="size">The initial output size.</param>
        public TextProcessingSettings(Size size)
            : this(size, Variables.Instance.DefaultFont)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextProcessingSettings"/> class.
        /// </summary>
        /// <param name="size">The initial output size.</param>
        /// <param name="font">The initial font.</param>
        public TextProcessingSettings(Size size, Font font)
        {
            this.calculateCharacterSize = true;

            this.isBlackTextOnWhite = !Variables.Instance.InvertImage;

            this.isFixedWidth = true;

            this.IsGeneratedRamp = Variables.Instance.UseGeneratedRamp;

            this.ramp = "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ";

            this.sharpen = Variables.Instance.Sharpen;
            this.sharpenFilter = new Sharpen();

            this.stretch = Variables.Instance.Stretch;
            this.stretchFilter = new Stretch();

            this.unsharp = Variables.Instance.UnsharpMask;
            this.unsharpFilter = new UnsharpMask(3);

            this.filterChanged = true;

            if (Variables.Instance.CurrentSelectedValidCharacters > -1)
            {
                this.validCharacters = (string)Variables.Instance.DefaultValidCharacters[Variables.Instance.CurrentSelectedValidCharacters];
            }
            else
            {
                this.validCharacters = Variables.Instance.CurrentCharacters;
            }

            this.Size = size;

            this.Font = font;

            // TODO: Move these to ResetVariables and set widgets from events
            this.brightness = Variables.Instance.DefaultTextBrightness;
            this.contrast = Variables.Instance.DefaultTextContrast;

            this.dithering = Variables.Instance.DefaultDitheringLevel;
            this.ditheringRandom = Variables.Instance.DefaultDitheringRandom;

            this.flipHorizontally = Variables.Instance.FlipHorizontally;
            this.flipVertically = Variables.Instance.FlipVertically;

            this.levels = new LevelsSettings(Variables.Instance.DefaultMinimumLevel, Variables.Instance.DefaultMedianLevel, Variables.Instance.DefaultMaximumLevel);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Brightness of the text image.
        /// </summary>
        /// <value>The brightness.</value>
        [CategoryAttribute("Adjustment"), DefaultValueAttribute(0), DescriptionAttribute("Brightness of the output image (-200 to 200)")]
        public int Brightness
        {
            get
            {
                return this.brightness;
            }

            set
            {
                if (value > 200)
                {
                    value = 200;
                }
                else if (value < -200)
                {
                    value = -200;
                }

                if (this.brightness == value)
                {
                    return;
                }

                this.brightness = value;

                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the character size should be automatically calculated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if calculating the character size; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Calculate Character Size?"), CategoryAttribute("Output"), DefaultValueAttribute(true), DescriptionAttribute("Let the program calculate the size of a character in this font?")]
        public bool CalculateCharacterSize
        {
            get
            {
                return this.calculateCharacterSize;
            }

            set
            {
                if (this.calculateCharacterSize == value)
                {
                    return;
                }

                this.calculateCharacterSize = value;

                if (this.calculateCharacterSize)
                {
                    this.UpdateCharacterSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of one character in the font.
        /// </summary>
        /// <value>The size of the character.</value>
        [DisplayName("Character Size"), CategoryAttribute("Output"), DescriptionAttribute("The size of one character in this font")]
        public Size CharacterSize
        {
            get
            {
                return this.characterSize;
            }

            set
            {
                int width = value.Width;
                int height = value.Height;

                if (width < 1)
                {
                    width = 1;
                }
                else if (width > 100)
                {
                    width = 100;
                }

                if (height < 1)
                {
                    height = 1;
                }
                else if (height > 100)
                {
                    height = 100;
                }

                Size size = new Size(width, height);

                if (this.characterSize == size)
                {
                    return;
                }

                this.characterSize = size;
                this.CalculateCharacterSize = false;
            }
        }

        /// <summary>
        /// Gets or sets the contrast of the text image.
        /// </summary>
        /// <value>The contrast.</value>
        [CategoryAttribute("Adjustment"), DefaultValueAttribute(0), DescriptionAttribute("The contrast of the output image (-100 to 100)")]
        public int Contrast
        {
            get
            {
                return this.contrast;
            }

            set
            {
                if (value > 100)
                {
                    value = 100;
                }
                else if (value < -100)
                {
                    value = -100;
                }

                if (this.contrast == value)
                {
                    return;
                }

                this.contrast = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the level of dithering.
        /// </summary>
        /// <value>The dithering.</value>
        [DisplayName("Amount"), CategoryAttribute("Dithering"), DefaultValueAttribute(4), DescriptionAttribute("Amount of dithering to apply to the output (0 to 25)")]
        public int Dithering
        {
            get
            {
                return this.dithering;
            }

            set
            {
                if (value > 25)
                {
                    value = 25;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                if (this.dithering == value)
                {
                    return;
                }

                this.dithering = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the level of randomness for the dithering.
        /// </summary>
        /// <value>The dithering random.</value>
        [DisplayName("Random"), CategoryAttribute("Dithering"), DefaultValueAttribute(3), DescriptionAttribute("Randomness of the dithering (0 to 20)")]
        public int DitheringRandom
        {
            get
            {
                return this.ditheringRandom;
            }

            set
            {
                if (value > 20)
                {
                    value = 20;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                if (this.ditheringRandom == value)
                {
                    return;
                }

                this.ditheringRandom = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets the filter list.
        /// </summary>
        /// <value>The filter list.</value>
        [Browsable(false)]
        public ArrayList FilterList
        {
            get
            {
                if (!this.filterChanged)
                {
                    return this.filterList;
                }

                this.filterList = new System.Collections.ArrayList();

                if (this.Stretch)
                {
                    this.filterList.Add(this.stretchFilter);
                }

                if (this.Brightness != 0 || this.Contrast != 0)
                {
                    this.filterList.Add(new BrightnessContrast(
                        this.IsBlackTextOnWhite ? this.Brightness : -this.Brightness,
                        this.IsBlackTextOnWhite ? this.Contrast : -this.Contrast));
                }

                if (this.MinimumLevel != 0 || this.MaximumLevel != 255 || this.MedianLevel != 0.5f)
                {
                    this.filterList.Add(new Levels(this.MinimumLevel, this.MaximumLevel, this.MedianLevel));
                }

                if (this.Sharpen)
                {
                    this.filterList.Add(this.sharpenFilter);
                }

                if (this.Unsharp)
                {
                    this.filterList.Add(this.unsharpFilter);
                }

                if (this.FlipHorizontally || this.FlipVertically)
                {
                    this.filterList.Add(new Flip(this.FlipHorizontally, this.FlipVertically));
                }

                if (this.Dithering > 0)
                {
                    this.filterList.Add(new Dither(this.Dithering, this.DitheringRandom));
                }

                this.filterChanged = false;

                return this.filterList;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the image horizontally.
        /// </summary>
        /// <value><c>true</c> if flipping horizontally; otherwise, <c>false</c>.</value>
        [DisplayName("Flip Horizontally"), CategoryAttribute("Effects"), DefaultValueAttribute(false), DescriptionAttribute("Flip the output horizontally?")]
        public bool FlipHorizontally
        {
            get
            {
                return this.flipHorizontally;
            }

            set
            {
                if (this.flipHorizontally == value)
                {
                    return;
                }

                this.flipHorizontally = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the image vertically.
        /// </summary>
        /// <value><c>true</c> if flipping vertically; otherwise, <c>false</c>.</value>
        [DisplayName("Flip Vertically"), CategoryAttribute("Effects"), DefaultValueAttribute(false), DescriptionAttribute("Flip the image vertically?")]
        public bool FlipVertically
        {
            get
            {
                return this.flipVertically;
            }

            set
            {
                if (this.flipVertically == value)
                {
                    return;
                }

                this.flipVertically = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The current font.</value>
        [Browsable(false)]
        public Font Font
        {
            get
            {
                return this.font;
            }

            set
            {
                this.font = value;

                this.IsFixedWidth = FontFunctions.IsFixedWidth(this.font);

                if (this.CalculateCharacterSize)
                {
                    this.UpdateCharacterSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the output image.
        /// </summary>
        /// <value>The current height.</value>
        [Browsable(false)]
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is black text on a white background.
        /// </summary>
        /// <value>
        /// <c>true</c> if black text on white; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Black Text on White?"), CategoryAttribute("Output"), DefaultValueAttribute(true), DescriptionAttribute("Output is black text on a white background?")]
        public bool IsBlackTextOnWhite
        {
            get
            {
                return this.isBlackTextOnWhite;
            }

            set
            {
                if (this.isBlackTextOnWhite == value)
                {
                    return;
                }

                this.isBlackTextOnWhite = value;

                if (this.ValuesToText is ValuesToFixedWidthTextConverter)
                {
                    ((ValuesToFixedWidthTextConverter)this.ValuesToText).Ramp =
                        this.IsBlackTextOnWhite ? this.Ramp : FontFunctions.Reverse(this.Ramp);
                }
                else if (this.ValuesToText is ValuesToVariableWidthTextConverter)
                {
                    ((ValuesToVariableWidthTextConverter)this.ValuesToText).InvertOutput = !this.isBlackTextOnWhite;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether font is fixed width.
        /// </summary>
        /// <value>
        /// <c>true</c> if the font is fixed width; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsFixedWidth
        {
            get
            {
                return this.isFixedWidth;
            }

            set
            {
                this.isFixedWidth = value;

                if (this.isFixedWidth)
                {
                    this.ValuesToText = new ValuesToFixedWidthTextConverter(this.Ramp);
                }
                else
                {
                    this.ValuesToText = new ValuesToVariableWidthTextConverter(this.ValidCharacters, this.Font);

                    if (!this.IsBlackTextOnWhite)
                    {
                        ((ValuesToVariableWidthTextConverter)this.ValuesToText).InvertOutput = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ramp was automatically generated.
        /// </summary>
        /// <value>
        /// <c>true</c> if a generated ramp; otherwise, <c>false</c>.
        /// </value>
        [DisplayName("Generated Ramp?"), CategoryAttribute("Ramp"), DefaultValueAttribute(true), DescriptionAttribute("Automatically generate an ASCII ramp for this font?")]
        public bool IsGeneratedRamp { get; set; }

        /// <summary>
        /// Gets or sets the levels.
        /// </summary>
        /// <value>The levels.</value>
        [CategoryAttribute("Adjustment"), DescriptionAttribute("Minimum, median and maximum levels for the output image")]
        public LevelsSettings Levels
        {
            get
            {
                return this.levels;
            }

            set
            {
                if (this.levels == value)
                {
                    return;
                }

                this.levels = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        [Browsable(false)]
        public int MaximumLevel
        {
            get
            {
                return this.Levels.Maximum;
            }

            set
            {
                this.Levels.Maximum = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the median level.
        /// </summary>
        /// <value>The median level.</value>
        [Browsable(false)]
        public float MedianLevel
        {
            get
            {
                return this.Levels.Median;
            }

            set
            {
                this.Levels.Median = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>The minimum level.</value>
        [Browsable(false)]
        public int MinimumLevel
        {
            get
            {
                return this.Levels.Minimum;
            }

            set
            {
                this.Levels.Minimum = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the ASCII ramp used for the image (black->white).
        /// </summary>
        /// <value>The current ramp.</value>
        [CategoryAttribute("Ramp"), DefaultValueAttribute("MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       "), DescriptionAttribute("Custom ASCII ramp to use (black to white)")]
        public string Ramp
        {
            get
            {
                return this.ramp;
            }

            set
            {
                if (value == null || value.Length < 2)
                {
                    return;
                }

                this.ramp = value;

                if (this.ValuesToText is ValuesToFixedWidthTextConverter)
                {
                    ((ValuesToFixedWidthTextConverter)this.ValuesToText).Ramp =
                        this.IsBlackTextOnWhite ? this.ramp : FontFunctions.Reverse(this.ramp);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to sharpen the image.
        /// </summary>
        /// <value><c>true</c> if sharpening; otherwise, <c>false</c>.</value>
        [CategoryAttribute("Effects"), DefaultValueAttribute(false), DescriptionAttribute("Sharpen the output image?")]
        public bool Sharpen
        {
            get
            {
                return this.sharpen;
            }

            set
            {
                if (this.sharpen == value)
                {
                    return;
                }

                this.sharpen = value;
                this.filterChanged = true;

                if (this.sharpen)
                {
                    this.Unsharp = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the output image.
        /// </summary>
        /// <value>The current size.</value>
        [Browsable(false)]
        public Size Size
        {
            get
            {
                return new Size(this.Width, this.Height);
            }

            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to stretch the output image.
        /// </summary>
        /// <value><c>true</c> if stretching; otherwise, <c>false</c>.</value>
        [CategoryAttribute("Effects"), DefaultValueAttribute(true), DescriptionAttribute("Stretch the output image?")]
        public bool Stretch
        {
            get
            {
                return this.stretch;
            }

            set
            {
                if (this.stretch == value)
                {
                    return;
                }

                this.stretch = value;
                this.filterChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use an unsharp mask.
        /// </summary>
        /// <value><c>true</c> if using an unsharp mask; otherwise, <c>false</c>.</value>
        [DisplayName("Unsharp Mask"), CategoryAttribute("Effects"), DefaultValueAttribute(true), DescriptionAttribute("Apply an unsharp mask to the output image?")]
        public bool Unsharp
        {
            get
            {
                return this.unsharp;
            }

            set
            {
                if (this.unsharp == value)
                {
                    return;
                }

                this.unsharp = value;
                this.filterChanged = true;

                if (this.unsharp)
                {
                    this.Sharpen = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the characters to use for generating the ramp.
        /// </summary>
        /// <value>The valid characters.</value>
        [DisplayName("Valid Characters"), CategoryAttribute("Ramp"), DefaultValueAttribute(" #,.0123456789:;@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$"), DescriptionAttribute("Characters used when generating an ASCII ramp")]
        public string ValidCharacters
        {
            get
            {
                return this.validCharacters;
            }

            set
            {
                if (value == null || value.Length < 2)
                {
                    return;
                }

                this.validCharacters = value;

                if (this.ValuesToText is ValuesToVariableWidthTextConverter)
                {
                    ((ValuesToVariableWidthTextConverter)this.ValuesToText).ValidCharacters = this.validCharacters;
                }
                else
                {
                    if (this.IsGeneratedRamp)
                    {
                        this.Ramp = AsciiRampCreator.CreateRamp(this.Font, this.ValidCharacters);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the values to text conversion class.
        /// </summary>
        /// <value>The values to text.</value>
        [Browsable(false)]
        public ValuesToTextConverter ValuesToText { get; set; }

        /// <summary>
        /// Gets or sets the width of the output image.
        /// </summary>
        /// <value>The width.</value>
        [Browsable(false)]
        public int Width
        {
            get;
            set;
        }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Calculates the size of one character in the current font.
        /// </summary>
        private void UpdateCharacterSize()
        {
            Size size = FontFunctions.MeasureText("W", Font);

            if (!this.IsFixedWidth)
            {
                size.Width = ValuesToVariableWidthTextConverter.CharacterWidth;
            }

            this.characterSize = size;
        }

        #endregion Private methods
    }
}