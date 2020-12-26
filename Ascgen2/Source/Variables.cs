//---------------------------------------------------------------------------------------
// <copyright file="Variables.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiGeneratorDotNet
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Abstract class containing global variables for the program
    /// </summary>
    [Serializable]
    public sealed class Variables
    {
        #region Constants

        /// <summary>
        /// Filename for the settings
        /// </summary>
        [NonSerialized]
        private const string Filename = "settings.dat";

        #endregion Constants

        #region Fields

        /// <summary>
        /// The instance used in the singleton pattern.
        /// </summary>
        [NonSerialized]
        private static Variables instance = new Variables();

        /// <summary>
        /// Check for a new version of the program?
        /// </summary>
        private bool checkForNewVersion = true;

        /// <summary>
        /// Confirm to save the image on exit?
        /// </summary>
        private bool confirmOnClose = true;

        /// <summary>
        /// The culture used by the application
        /// </summary>
        [NonSerialized]
        private CultureInfo culture = new CultureInfo(string.Empty);

        /// <summary>
        /// The current characters.
        /// </summary>
        [NonSerialized]
        private string currentCharacters;

        /// <summary>
        /// The current ramp.
        /// </summary>
        [NonSerialized]
        private string currentRamp;

        /// <summary>
        /// The currently selected ramp.
        /// </summary>
        [NonSerialized]
        private int currentSelectedRamp;

        /// <summary>
        /// The currently selected valid characters.
        /// </summary>
        [NonSerialized]
        private int currentSelectedValidCharacters;

        /// <summary>
        /// The default dithering level.
        /// </summary>
        [NonSerialized]
        private int defaultDitheringLevel = 4;

        /// <summary>
        /// The default dithering random.
        /// </summary>
        [NonSerialized]
        private int defaultDitheringRandom = 3;

        /// <summary>
        /// The default font
        /// </summary>
        private Font defaultFont = new Font("Lucida Console", 9f);

        /// <summary>
        /// The default height
        /// </summary>
        private int defaultHeight = -1;

        /// <summary>
        /// The default maximum level.
        /// </summary>
        [NonSerialized]
        private int defaultMaxLevel = 255;

        /// <summary>
        /// The default median level.
        /// </summary>
        [NonSerialized]
        private float defaultMedianLevel = 0.5f;

        /// <summary>
        /// The default minimum level.
        /// </summary>
        [NonSerialized]
        private int defaultMinLevel;

        /// <summary>
        /// The default list of ramps.
        /// </summary>
        [NonSerialized]
        private string[] defaultRamps = new[]
        {
            "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ",
            "@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ",
            "#WMBRXVYIti+=;:,. ", "##XXxxx+++===---;;,,...    ",
            "@%#*+=-:. ",
            "#¥¥®®ØØ$$ø0oo°++=-,.    ",
            "# ",
            "01 ",
            "█▓▒░ "
        };

        /// <summary>
        /// The default text brightness.
        /// </summary>
        [NonSerialized]
        private int defaultTextBrightness;

        /// <summary>
        /// The default text contrast.
        /// </summary>
        [NonSerialized]
        private int defaultTextContrast;

        /// <summary>
        /// The different strings of valid characters.
        /// </summary>
        [NonSerialized]
        private string[] defaultValidCharacters = new[]
        {
            " #,.0123456789:;@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$",
            " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
            " 1234567890",
            "M@WB08Za2SX7r;i:;. ",
            "@#MBHAGh93X25Sisr;:, ",
            "█▓▒░ "
        };

        /// <summary>
        /// The default width
        /// </summary>
        private int defaultWidth = 150;

        /// <summary>
        /// Flip the image horizontally?
        /// </summary>
        [NonSerialized]
        private bool flipHorizontally;

        /// <summary>
        /// Flip the image vertically?
        /// </summary>
        [NonSerialized]
        private bool flipVertically;

        /// <summary>
        /// Invert the output image?
        /// </summary>
        [NonSerialized]
        private bool invertImage;

        /// <summary>
        /// Load brightness/contrast from settings?
        /// </summary>
        [NonSerialized]
        private bool loadImageBrightnessContrast;

        /// <summary>
        /// Load levels from settings?
        /// </summary>
        [NonSerialized]
        private bool loadLevels;

        /// <summary>
        /// Load image brightness/contrast from settings?
        /// </summary>
        [NonSerialized]
        private bool loadTextBrightnessContrast;

        /// <summary>
        /// The maximum height for the output.
        /// </summary>
        [NonSerialized]
        private int maximumHeight = 999;

        /// <summary>
        /// The maximum width for the output.
        /// </summary>
        [NonSerialized]
        private int maximumWidth = 999;

        /// <summary>
        /// The filename prefix for output images
        /// </summary>
        [NonSerialized]
        private string prefix = "ASCII-";

        /// <summary>
        /// The selection border color.
        /// </summary>
        [NonSerialized]
        private Color selectionBorderColor = Color.DarkBlue;

        /// <summary>
        /// The selection fill color.
        /// </summary>
        [NonSerialized]
        private Color selectionFillColor = Color.FromArgb(128, 173, 216, 230);

        /// <summary>
        /// Sharpen the output?
        /// </summary>
        [NonSerialized]
        private bool sharpen;

        /// <summary>
        /// Show the image widget?
        /// </summary>
        [NonSerialized]
        private bool showWidgetImage = true;

        /// <summary>
        /// Show the text settings widget?
        /// </summary>
        [NonSerialized]
        private bool showWidgetTextSettings = true;

        /// <summary>
        /// Stretch the output?
        /// </summary>
        [NonSerialized]
        private bool stretch = true;

        /// <summary>
        /// Use an unsharp mask?
        /// </summary>
        [NonSerialized]
        private bool unsharpMask = true;

        /// <summary>
        /// Update the output while selecting an area of the image?
        /// </summary>
        [NonSerialized]
        private bool updateWhileSelecting = true;

        /// <summary>
        /// Use a generated ramp?
        /// </summary>
        [NonSerialized]
        private bool useGeneratedRamp = true;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the singleton Variables instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Variables Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to check for a new version.
        /// </summary>
        /// <value><c>true</c> if checking for new versions; otherwise, <c>false</c>.</value>
        public bool CheckForNewVersion
        {
            get
            {
                return this.checkForNewVersion;
            }

            set
            {
                this.checkForNewVersion = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether confirm on close with an unsaved image.
        /// </summary>
        /// <value><c>true</c> if confirm on close; otherwise, <c>false</c>.</value>
        public bool ConfirmOnClose
        {
            get
            {
                return this.confirmOnClose;
            }

            set
            {
                this.confirmOnClose = value;
            }
        }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public CultureInfo Culture
        {
            get
            {
                return this.culture;
            }

            set
            {
                this.culture = value;
            }
        }

        /// <summary>
        /// Gets or sets the current characters.
        /// </summary>
        /// <value>The current characters.</value>
        public string CurrentCharacters
        {
            get
            {
                return this.currentCharacters;
            }

            set
            {
                this.currentCharacters = value;
            }
        }

        /// <summary>
        /// Gets or sets the current ramp.
        /// </summary>
        /// <value>The current ramp.</value>
        public string CurrentRamp
        {
            get
            {
                return this.currentRamp;
            }

            set
            {
                this.currentRamp = value;
            }
        }

        /// <summary>
        /// Gets or sets the currently selected ramp.
        /// </summary>
        /// <value>The currently selected ramp.</value>
        public int CurrentSelectedRamp
        {
            get
            {
                return this.currentSelectedRamp;
            }

            set
            {
                this.currentSelectedRamp = value;
            }
        }

        /// <summary>
        /// Gets or sets the currently selected valid characters.
        /// </summary>
        /// <value>The currently selected valid characters.</value>
        public int CurrentSelectedValidCharacters
        {
            get
            {
                return this.currentSelectedValidCharacters;
            }

            set
            {
                this.currentSelectedValidCharacters = value;
            }
        }

        /// <summary>
        /// Gets or sets the default dithering level.
        /// </summary>
        /// <value>The default dithering level.</value>
        public int DefaultDitheringLevel
        {
            get
            {
                return this.defaultDitheringLevel;
            }

            set
            {
                this.defaultDitheringLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>The default dithering random.</value>
        public int DefaultDitheringRandom
        {
            get
            {
                return this.defaultDitheringRandom;
            }

            set
            {
                this.defaultDitheringRandom = value;
            }
        }

        /// <summary>
        /// Gets or sets the default font.
        /// </summary>
        /// <value>The default font.</value>
        public Font DefaultFont
        {
            get
            {
                return this.defaultFont;
            }

            set
            {
                this.defaultFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the default height.
        /// </summary>
        /// <value>The default height.</value>
        public int DefaultHeight
        {
            get
            {
                return this.defaultHeight;
            }

            set
            {
                this.defaultHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the default maximum level.
        /// </summary>
        /// <value>The default maximum level.</value>
        public int DefaultMaximumLevel
        {
            get
            {
                return this.defaultMaxLevel;
            }

            set
            {
                this.defaultMaxLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the default median level.
        /// </summary>
        /// <value>The default median level.</value>
        public float DefaultMedianLevel
        {
            get
            {
                return this.defaultMedianLevel;
            }

            set
            {
                this.defaultMedianLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the default minimum level.
        /// </summary>
        /// <value>The default minimum level.</value>
        public int DefaultMinimumLevel
        {
            get
            {
                return this.defaultMinLevel;
            }

            set
            {
                this.defaultMinLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the default list of ramps
        /// </summary>
        /// <value>The default ramps.</value>
        public string[] DefaultRamps
        {
            get
            {
                return this.defaultRamps;
            }

            set
            {
                this.defaultRamps = value;
            }
        }

        /// <summary>
        /// Gets or sets the default text brightness.
        /// </summary>
        /// <value>The default text brightness.</value>
        public int DefaultTextBrightness
        {
            get
            {
                return this.defaultTextBrightness;
            }

            set
            {
                this.defaultTextBrightness = value;
            }
        }

        /// <summary>
        /// Gets or sets the default text contrast.
        /// </summary>
        /// <value>The default text contrast.</value>
        public int DefaultTextContrast
        {
            get
            {
                return this.defaultTextContrast;
            }

            set
            {
                this.defaultTextContrast = value;
            }
        }

        /// <summary>
        /// Gets or sets the default settings used for all ASCII ramp valid character strings
        /// </summary>
        /// <value>The default valid characters.</value>
        public string[] DefaultValidCharacters
        {
            get
            {
                return this.defaultValidCharacters;
            }

            set
            {
                this.defaultValidCharacters = value;
            }
        }

        /// <summary>
        /// Gets or sets the default width.
        /// </summary>
        /// <value>The default width.</value>
        public int DefaultWidth
        {
            get
            {
                return this.defaultWidth;
            }

            set
            {
                this.defaultWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be flipped horizontally
        /// </summary>
        /// <value><c>true</c> if flipping horizontally; otherwise, <c>false</c>.</value>
        public bool FlipHorizontally
        {
            get
            {
                return this.flipHorizontally;
            }

            set
            {
                this.flipHorizontally = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be flipped vertically
        /// </summary>
        /// <value><c>true</c> if flip vertically; otherwise, <c>false</c>.</value>
        public bool FlipVertically
        {
            get
            {
                return this.flipVertically;
            }

            set
            {
                this.flipVertically = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial input directory.
        /// </summary>
        /// <value>The initial input directory.</value>
        public string InitialInputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the initial output directory.
        /// </summary>
        /// <value>The initial output directory.</value>
        public string InitialOutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to invert the output image.
        /// </summary>
        /// <value><c>true</c> if inverting image; otherwise, <c>false</c>.</value>
        public bool InvertImage
        {
            get
            {
                return this.invertImage;
            }

            set
            {
                this.invertImage = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether brightness and contrast should be loaded from the settings
        /// </summary>
        /// <value>
        /// <c>true</c> if loading image brightness contrast; otherwise, <c>false</c>.
        /// </value>
        public bool LoadImageBrightnessContrast
        {
            get
            {
                return this.loadImageBrightnessContrast;
            }

            set
            {
                this.loadImageBrightnessContrast = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the levels should be loaded from the settings
        /// </summary>
        /// <value><c>true</c> if loading levels; otherwise, <c>false</c>.</value>
        public bool LoadLevels
        {
            get
            {
                return this.loadLevels;
            }

            set
            {
                this.loadLevels = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the text brightness and contrast should be loaded from the settings
        /// </summary>
        /// <value>
        /// <c>true</c> if loading text brightness contrast; otherwise, <c>false</c>.
        /// </value>
        public bool LoadTextBrightnessContrast
        {
            get
            {
                return this.loadTextBrightnessContrast;
            }

            set
            {
                this.loadTextBrightnessContrast = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum height for the output.
        /// </summary>
        /// <value>The maximum height.</value>
        public int MaximumHeight
        {
            get
            {
                return this.maximumHeight;
            }

            set
            {
                this.maximumHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum width for the output.
        /// </summary>
        /// <value>The maximum width.</value>
        public int MaximumWidth
        {
            get
            {
                return this.maximumWidth;
            }

            set
            {
                this.maximumWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the filename prefix.
        /// </summary>
        /// <value>The prefix.</value>
        public string Prefix
        {
            get
            {
                return this.prefix;
            }

            set
            {
                this.prefix = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection border.
        /// </summary>
        /// <value>The color of the selection border.</value>
        public Color SelectionBorderColor
        {
            get
            {
                return this.selectionBorderColor;
            }

            set
            {
                this.selectionBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection fill.
        /// </summary>
        /// <value>The color of the selection fill.</value>
        public Color SelectionFillColor
        {
            get
            {
                return this.selectionFillColor;
            }

            set
            {
                this.selectionFillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default value for whether the output should be sharpened
        /// </summary>
        /// <value><c>true</c> if sharpen; otherwise, <c>false</c>.</value>
        public bool Sharpen
        {
            get
            {
                return this.sharpen;
            }

            set
            {
                this.sharpen = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the image widget.
        /// </summary>
        /// <value><c>true</c> if showing the image widget; otherwise, <c>false</c>.</value>
        public bool ShowWidgetImage
        {
            get
            {
                return this.showWidgetImage;
            }

            set
            {
                this.showWidgetImage = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the text settings widget.
        /// </summary>
        /// <value><c>true</c> if showing the text settings widget; otherwise, <c>false</c>.</value>
        public bool ShowWidgetTextSettings
        {
            get
            {
                return this.showWidgetTextSettings;
            }

            set
            {
                this.showWidgetTextSettings = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to stretch the output.
        /// </summary>
        /// <value><c>true</c> if stretching; otherwise, <c>false</c>.</value>
        public bool Stretch
        {
            get
            {
                return this.stretch;
            }

            set
            {
                this.stretch = value;
            }
        }

        /// <summary>
        /// Gets or sets the translation file.
        /// </summary>
        /// <value>The translation file.</value>
        public string TranslationFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use an unsharp mask.
        /// </summary>
        /// <value><c>true</c> if using an unsharp mask; otherwise, <c>false</c>.</value>
        public bool UnsharpMask
        {
            get
            {
                return this.unsharpMask;
            }

            set
            {
                this.unsharpMask = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to update the output while selecting an area of the image.
        /// </summary>
        /// <value>
        /// <c>true</c> if update while selecting; otherwise, <c>false</c>.
        /// </value>
        public bool UpdateWhileSelecting
        {
            get
            {
                return this.updateWhileSelecting;
            }

            set
            {
                this.updateWhileSelecting = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use a generated ramp.
        /// </summary>
        /// <value><c>true</c> if using a generated ramp; otherwise, <c>false</c>.</value>
        public bool UseGeneratedRamp
        {
            get
            {
                return this.useGeneratedRamp;
            }

            set
            {
                this.useGeneratedRamp = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <returns>True if settings were loaded, otherwise false</returns>
        public static bool LoadSettings()
        {
            Variables settings = Load(Filename);

            if (settings == null)
            {
                return false;
            }

            // TODO: Validation?
            instance = new Variables
                {
                    DefaultWidth = settings.DefaultWidth,
                    DefaultHeight = settings.DefaultHeight,
                    TranslationFile = settings.TranslationFile,
                    InitialInputDirectory = settings.InitialInputDirectory,
                    InitialOutputDirectory = settings.InitialOutputDirectory,
                    DefaultFont = settings.DefaultFont,
                    ConfirmOnClose = settings.ConfirmOnClose,
                    CheckForNewVersion = settings.CheckForNewVersion
                };

            return true;
        }

        /// <summary>
        /// Save the current settings
        /// </summary>
        public void SaveSettings()
        {
            Save(this, Filename);
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Loads the settings from the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A Variables object containing the loaded settings, or null</returns>
        private static Variables Load(string filename)
        {
            Stream stream = null;

            Variables variables = null;

            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);

                IFormatter formatter = new BinaryFormatter();

                variables = (Variables)formatter.Deserialize(stream);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return variables;
        }

        /// <summary>
        /// Saves the specified Variables object to a file.
        /// </summary>
        /// <param name="variables">The variables.</param>
        /// <param name="filename">The filename to use.</param>
        private static void Save(Variables variables, string filename)
        {
            Stream stream = null;

            try
            {
                stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);

                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, variables);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        #endregion Private methods
    }
}