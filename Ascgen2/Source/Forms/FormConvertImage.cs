//---------------------------------------------------------------------------------------
// <copyright file="FormConvertImage.cs" company="Jonathan Mathews Software">
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
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;
    using System.Windows.Forms;
    using JMSoftware.AsciiConversion;
    using JMSoftware.AsciiConversion.Filters;
    using JMSoftware.ImageHelper;
    using JMSoftware.Interfaces;
    using JMSoftware.TextHelper;
    using JMSoftware.VersionChecking;
    using JMSoftware.Widgets;
    using Version = JMSoftware.VersionChecking.Version;

    /// <summary>
    /// Main form for the program
    /// </summary>
    public partial class FormConvertImage
    {
        #region Fields

        /// <summary>Interface to the object used to get and set the brightness and contrast amounts</summary>
        private IBrightnessContrast brightnessContrast;

        /// <summary>Used for storing the size of the form</summary>
        private Size clientSize;

        /// <summary>
        /// Dialog used to get the level of image scaling
        /// </summary>
        private TextImageMagnificationDialog dialogChooseTextZoom;

        /// <summary>Object used to calculate the output dimensions</summary>
        private DimensionsCalculator dimensionsCalculator;

        /// <summary>Interface to the object used to get and set the dithering amounts</summary>
        private IDither dither;

        /// <summary>Are we doing the conversions?</summary>
        private bool doConversion;

        /// <summary>The full filename of the input image</summary>
        private string filename;

        /// <summary>
        /// The batch conversion form
        /// </summary>
        private FormBatchConversion formBatchConversion;

        /// <summary>The Save As form</summary>
        private FormSaveAs formSaveAs;

        /// <summary>Has the current image been saved?</summary>
        private bool imageSaved;

        /// <summary>Have the input settings changed so the output needs to be recalculated?</summary>
        private bool inputChanged;

        /// <summary>A value indicating whether the form is full screen.</summary>
        private bool isFullScreen;

        /// <summary>Interface to the object used to get and set the levels</summary>
        private ILevels levels;

        /// <summary>Stores the position of the selection rectangle</summary>
        private Rectangle oldSelectionPosition;

        /// <summary>Stores the position and size of the form when going to full screen</summary>
        private Rectangle previousFormPosition;

        /// <summary>Store the previous state of the window (to work around toggling problems with a maximized form)</summary>
        private FormWindowState previousWindowState;

        /// <summary>Are we printing in colour?</summary>
        private bool printColour;

        /// <summary>Used to store the settings used on the output image</summary>
        private TextProcessingSettings textSettings;

        /// <summary>Interface to the object used to display the text</summary>
        private ITextViewer textViewer;

        /// <summary>The original image converted into an array of bytes</summary>
        private byte[][] values;

        /// <summary>Handles checking for a new version</summary>
        private VersionChecker versionChecker;

        /// <summary>
        /// The image widget
        /// </summary>
        private WidgetImage widgetImage;

        /// <summary>
        /// Text settings widget
        /// </summary>
        private WidgetTextSettings widgetTextSettings;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormConvertImage"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public FormConvertImage(IList<string> arguments)
        {
            // Required for Windows Form Designer support
            this.InitializeComponent();

            this.Text = AscgenVersion.ProgramName + " v" + AscgenVersion.ToString();

            Variables.LoadSettings();

            this.filename = string.Empty;

            this.doConversion = true;

            this.AlterInputImageToolStripIsEnabled = false;

            this.dialogSaveImage.InitialDirectory =
                        this.dialogSaveText.InitialDirectory =
                        this.dialogSaveColour.InitialDirectory = Variables.Instance.InitialOutputDirectory;

            this.clientSize = this.pnlMain.ClientSize;

            this.textSettings = new TextProcessingSettings();

            this.textViewer = this.rtbxConvertedText;

            this.formSaveAs = new FormSaveAs();

            this.SetupWidgets();

            this.dimensionsCalculator = new DimensionsCalculator(this.CurrentImageSection.Size, this.CharacterSize, Variables.Instance.DefaultWidth, Variables.Instance.DefaultHeight);

            this.dimensionsCalculator.OnOutputSizeChanged += this.DimensionsCalculator_OnOutputSizeChanged;

            this.textSettings.Font = null;

            this.Font = Variables.Instance.DefaultFont;

            this.SetupControls();

            this.formBatchConversion = new FormBatchConversion();

            this.dialogChooseTextZoom = new TextImageMagnificationDialog(this.Font);

            this.versionChecker = new VersionChecker();

            this.versionChecker.ReadAsyncCompletedEventHandler += this.VersionReadAsyncCompletedEventHandler;

            if (Variables.Instance.CheckForNewVersion)
            {
                this.versionChecker.ReadAsync(AscgenVersion.VersionUrl);
            }

            // load a filename if one was passed
            if (arguments != null && arguments.Count == 1)
            {
                this.LoadImage(arguments[0]);
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether whether tstripAlterInputImage is enabled (while allowing it to be moved).
        /// </summary>
        /// <value>
        ///     <c>true</c> if tstripAlterInputImage is enabled; otherwise, <c>false</c>.
        /// </value>
        private bool AlterInputImageToolStripIsEnabled
        {
            get
            {
                return this.tsbRotateClockwise.Enabled;
            }

            set
            {
                foreach (ToolStripItem item in this.toolStripRotateFlip.Items)
                {
                    item.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether an area of the image is selected.
        /// </summary>
        /// <value><c>true</c> if an area is selected; otherwise, <c>false</c>.</value>
        private bool AreaIsSelected
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return false;
                }

                return this.widgetImage.SelectedArea.Width > 0 && this.widgetImage.SelectedArea.Height > 0;
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        private Color BackgroundColor
        {
            get
            {
                return this.IsBlackTextOnWhite ? Color.White : Color.Black;
            }
        }

        /// <summary>
        /// Gets or sets the text brightness.
        /// </summary>
        /// <value>The text brightness.</value>
        private int Brightness
        {
            get
            {
                return this.textSettings.Brightness;
            }

            set
            {
                this.textSettings.Brightness = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are calculating the character size.
        /// </summary>
        /// <value>
        /// <c>true</c> if calculating character size; otherwise, <c>false</c>.
        /// </value>
        private bool CalculatingCharacterSize
        {
            get
            {
                return this.textSettings.CalculateCharacterSize;
            }

            set
            {
                this.textSettings.CalculateCharacterSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of one character.
        /// </summary>
        /// <value>The size of one character.</value>
        private Size CharacterSize
        {
            get
            {
                return this.textSettings.CharacterSize;
            }

            set
            {
                this.dimensionsCalculator.CharacterSize = this.textSettings.CharacterSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the text contrast.
        /// </summary>
        /// <value>The text contrast.</value>
        private int Contrast
        {
            get
            {
                return this.textSettings.Contrast;
            }

            set
            {
                this.textSettings.Contrast = value;
            }
        }

        /// <summary>
        /// Gets the currently active section of the image.
        /// </summary>
        /// <value>The current image section.</value>
        private Rectangle CurrentImageSection
        {
            get
            {
                if (!this.ImageIsLoaded)
                {
                    return Rectangle.Empty;
                }

                if (!this.AreaIsSelected)
                {
                    return new Rectangle(0, 0, this.widgetImage.Image.Width, this.widgetImage.Image.Height);
                }

                return this.widgetImage.SelectedArea;
            }
        }

        /// <summary>
        /// Gets or sets the dithering amount.
        /// </summary>
        /// <value>The dithering.</value>
        private int Dithering
        {
            get
            {
                return this.textSettings.Dithering;
            }

            set
            {
                if (this.textSettings.Dithering == value)
                {
                    return;
                }

                this.textSettings.Dithering = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the dithering amount.
        /// </summary>
        /// <value>The dithering.</value>
        private int DitheringRandom
        {
            get
            {
                return this.textSettings.DitheringRandom;
            }

            set
            {
                if (this.textSettings.DitheringRandom == value)
                {
                    return;
                }

                this.textSettings.DitheringRandom = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the full filename of the image.
        /// </summary>
        /// <value>The filename.</value>
        private string Filename
        {
            get
            {
                return this.ImageIsLoaded ? this.filename : string.Empty;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.filename = value;

                this.widgetImage.Text = Path.GetFileName(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the output horizontally.
        /// </summary>
        /// <value><c>true</c> if flipping horizontally; otherwise, <c>false</c>.</value>
        private bool FlipHorizontally
        {
            get
            {
                return this.textSettings.FlipHorizontally;
            }

            set
            {
                if (this.textSettings.FlipHorizontally == value)
                {
                    return;
                }

                this.textSettings.FlipHorizontally = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to flip the output vertically.
        /// </summary>
        /// <value><c>true</c> if flipping vertically; otherwise, <c>false</c>.</value>
        private bool FlipVertically
        {
            get
            {
                return this.textSettings.FlipVertically;
            }

            set
            {
                if (this.textSettings.FlipVertically == value)
                {
                    return;
                }

                this.textSettings.FlipVertically = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets the font for the text.
        /// </summary>
        private new Font Font
        {
            get
            {
                return this.textSettings.Font;
            }

            set
            {
                // having to use Font.Equals() as == isn't catching it
                if (value == null || value.Equals(this.textSettings.Font))
                {
                    return;
                }

                this.textSettings.Font =
                                    this.textViewer.Font =
                                    this.dialogChooseFont.Font = value;

                this.toolStripRamp.Visible =
                        this.chkGenerate.Visible =
                        this.lblRamp.Visible =
                        this.cmbRamp.Visible = this.IsFixedWidth;

                Size fontSize = FontFunctions.MeasureText("W", this.textSettings.Font);

                if (this.IsFixedWidth)
                {
                    if (this.IsGeneratedRamp)
                    {
                        this.Ramp = AsciiRampCreator.CreateRamp(this.textSettings.Font, this.ValidCharacters);
                    }
                }
                else
                {
                    fontSize.Width = 7;
                    this.inputChanged = true;
                }

                this.UpdateMenus();

                if (this.textSettings.CalculateCharacterSize)
                {
                    this.CharacterSize = fontSize;
                }

                this.DoConvert();
            }
        }

        /// <summary>
        /// Gets a value indicating whether an image is loaded.
        /// </summary>
        /// <value><c>true</c> if an image is loaded; otherwise, <c>false</c>.</value>
        private bool ImageIsLoaded
        {
            get
            {
                return this.widgetImage.Image != null;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output is black text on a white background.
        /// </summary>
        /// <value>
        /// <c>true</c> if black text on white; otherwise, <c>false</c>.
        /// </value>
        private bool IsBlackTextOnWhite
        {
            get
            {
                return this.textSettings.IsBlackTextOnWhite;
            }

            set
            {
                this.toolStripButtonBlackOnWhite.Checked = !value;

                if (this.textSettings.IsBlackTextOnWhite == value)
                {
                    return;
                }

                this.textSettings.IsBlackTextOnWhite = value;

                this.textViewer.BackgroundColor = this.BackgroundColor;

                this.textViewer.TextColor = this.TextColor;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the font is fixed width.
        /// </summary>
        /// <value>
        /// <c>true</c> if fixed width; otherwise, <c>false</c>.
        /// </value>
        private bool IsFixedWidth
        {
            get
            {
                return this.textSettings.IsFixedWidth;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the form is full screen.
        /// </summary>
        /// <value><c>true</c> if full screen; otherwise, <c>false</c>.</value>
        private bool IsFullScreen
        {
            get
            {
                return this.isFullScreen;
            }

            set
            {
                if (this.isFullScreen == value)
                {
                    return;
                }

                this.isFullScreen = value;

                this.toolStripButtonFullScreen.Checked = this.IsFullScreen;

                if (this.isFullScreen)
                {
                    this.previousWindowState = this.WindowState;

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    this.previousFormPosition = new Rectangle(this.Location, this.Size);

                    this.FormBorderStyle = FormBorderStyle.None;

                    this.Location = new Point(0, 0);

                    this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                }
                else
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;

                    this.WindowState = this.previousWindowState;

                    this.Location = this.previousFormPosition.Location;

                    this.Size = this.previousFormPosition.Size;

                    this.Focus();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ramp is generated or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if generated ramp; otherwise, <c>false</c>.
        /// </value>
        private bool IsGeneratedRamp
        {
            get
            {
                return this.textSettings.IsGeneratedRamp;
            }

            set
            {
                if (this.textSettings.IsGeneratedRamp == value && this.chkGenerate.Checked == value && this.toolStripCharacters.Visible == value)
                {
                    return;
                }

                this.toolStripCharacters.Visible = this.chkGenerate.Checked = this.textSettings.IsGeneratedRamp = value;

                this.cmbRamp.Enabled = !value;

                if (!this.IsFixedWidth)
                {
                    return;
                }

                this.Ramp = value ?
                                    AsciiRampCreator.CreateRamp(this.Font, this.ValidCharacters) :
                                    this.cmbRamp.Text;

                this.inputChanged = true;

                this.imageSaved = false;
            }
        }

        /// <summary>
        /// Gets or sets the maximum level.
        /// </summary>
        /// <value>The maximum level.</value>
        private int MaximumLevel
        {
            get
            {
                return this.textSettings.MaximumLevel;
            }

            set
            {
                this.textSettings.MaximumLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the median level.
        /// </summary>
        /// <value>The median level.</value>
        private float MedianLevel
        {
            get
            {
                return this.textSettings.MedianLevel;
            }

            set
            {
                this.textSettings.MedianLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum level.
        /// </summary>
        /// <value>The minimum level.</value>
        private int MinimumLevel
        {
            get
            {
                return this.textSettings.MinimumLevel;
            }

            set
            {
                this.textSettings.MinimumLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the output text.
        /// </summary>
        /// <value>The height of the output.</value>
        private int OutputHeight
        {
            get
            {
                return this.textSettings.Height;
            }

            set
            {
                this.textSettings.Height = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the output text.
        /// </summary>
        /// <value>The width of the output.</value>
        private int OutputWidth
        {
            get
            {
                return this.textSettings.Width;
            }

            set
            {
                this.textSettings.Width = value;
            }
        }

        /// <summary>
        /// Gets or sets the ramp.
        /// </summary>
        /// <value>The ramp (black to white).</value>
        private string Ramp
        {
            get
            {
                return this.textSettings.Ramp;
            }

            set
            {
                if (this.textSettings.Ramp == value)
                {
                    return;
                }

                this.textSettings.Ramp = value;

                this.ApplyTextEffects();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to sharpen the output.
        /// </summary>
        /// <value><c>true</c> if sharpening; otherwise, <c>false</c>.</value>
        private bool Sharpen
        {
            get
            {
                return this.textSettings.Sharpen;
            }

            set
            {
                this.textSettings.Sharpen = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output is stretched.
        /// </summary>
        /// <value><c>true</c> if stretched; otherwise, <c>false</c>.</value>
        private bool Stretch
        {
            get
            {
                return this.textSettings.Stretch;
            }

            set
            {
                this.textSettings.Stretch = value;
            }
        }

        /// <summary>
        /// Gets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        private Color TextColor
        {
            get
            {
                return this.IsBlackTextOnWhite ? Color.Black : Color.White;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output uses an unsharp mask.
        /// </summary>
        /// <value><c>true</c> if unsharp mask; otherwise, <c>false</c>.</value>
        private bool Unsharp
        {
            get
            {
                return this.textSettings.Unsharp;
            }

            set
            {
                this.textSettings.Unsharp = value;
            }
        }

        /// <summary>
        /// Gets or sets the valid characters.
        /// </summary>
        /// <value>The valid characters.</value>
        private string ValidCharacters
        {
            get
            {
                return this.textSettings.ValidCharacters;
            }

            set
            {
                if (value == null || this.textSettings.ValidCharacters == value)
                {
                    return;
                }

                this.textSettings.ValidCharacters = value;

                if (this.IsGeneratedRamp || !this.IsFixedWidth)
                {
                    this.ApplyTextEffects();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the values array has been created.
        /// </summary>
        /// <value><c>true</c> if values created; otherwise, <c>false</c>.</value>
        private bool ValuesCreated
        {
            get
            {
                return this.values != null;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Get a rectangle representing the total actual area printable on the page
        /// </summary>
        /// <param name="e">The print parameters</param>
        /// <param name="centerOnPage">Make the margins equal so the area is centered on the page?</param>
        /// <param name="isPrintPreview">Is this for a print preview?</param>
        /// <returns>The printable area</returns>
        public static Rectangle GetPrintableArea(PrintPageEventArgs e, bool centerOnPage, bool isPrintPreview)
        {
            Rectangle printableArea = Rectangle.Round(e.PageSettings.PrintableArea);

            int leftMargin = printableArea.X;
            int topMargin = printableArea.Y;
            int rightMargin = e.PageBounds.Width - (e.PageSettings.Landscape ? printableArea.Bottom : printableArea.Right);
            int bottomMargin = e.PageBounds.Height - (e.PageSettings.Landscape ? printableArea.Right : printableArea.Bottom);

            if (centerOnPage)
            {
                // use the biggest values to center the image
                leftMargin = rightMargin = Math.Max(leftMargin, rightMargin);
                topMargin = bottomMargin = Math.Max(topMargin, bottomMargin);
            }

            Rectangle result = new Rectangle(
                                    leftMargin,
                                    topMargin,
                                    e.PageBounds.Width - leftMargin - rightMargin - 1,
                                    e.PageBounds.Height - topMargin - bottomMargin - 1);

            if (!isPrintPreview)
            {
                // apply physical offset for the printer
                result.Offset(-(int)e.PageSettings.HardMarginX, -(int)e.PageSettings.HardMarginY);
            }

            return result;
        }

        /// <summary>
        /// Load an image into the picturebox, and setup the form etc.
        /// </summary>
        /// <param name="image">The image to load</param>
        /// <returns>Did everything work correctly?</returns>
        public bool LoadImage(Image image)
        {
            if (!this.CloseImage())
            {
                return false;
            }

            this.doConversion = false;

            this.widgetImage.Hide();

            this.widgetImage.Image = image;

            // Note: widget borders are x=16 (8+8), and y=34 (26+8)
            if (image.Width > image.Height)
            {
                float ratio = (float)image.Height / (float)image.Width;

                int width = (ratio < 0.3) ? 380 : 280;

                // Calculate the height
                this.widgetImage.Size = new Size(width, (int)(((float)(width - 16) * ratio) + 0.5f) + 34);
            }
            else
            {
                float ratio = (float)image.Width / (float)image.Height;

                int height = (ratio < 0.3) ? 380 : 280;

                // Calculate the width
                this.widgetImage.Size = new Size((int)(((float)(height - 34) * ratio) + 0.5f) + 16, height);
            }

            this.PositionImageWidget();

            this.widgetImage.Show();

            this.widgetImage.Refresh();

            this.AlterInputImageToolStripIsEnabled = this.widgetTextSettings.Enabled = true;

            this.widgetTextSettings.Refresh();

            this.UpdateMenus();

            this.inputChanged = true;

            this.dimensionsCalculator.ImageSize = image.Size;

            this.dimensionsCalculator.OutputSize = new Size(Variables.Instance.DefaultWidth, Variables.Instance.DefaultHeight);

            this.cbxLocked.Checked = this.dimensionsCalculator.DimensionsAreLocked;

            this.UpdateTextSizeControls();

            this.inputChanged = true;

            this.doConversion = true;

            // Do the conversion first then show the textbox
            return this.rtbxConvertedText.Visible = this.DoConvert();
        }

        /// <summary>
        /// Load the specified image into the picturebox, and setup the form etc.
        /// </summary>
        /// <param name="imagePath">Path to the image</param>
        /// <returns>Did the image load correctly?</returns>
        public bool LoadImage(string imagePath)
        {
            if (!this.CloseImage())
            {
                return false;
            }

            try
            {
                Image image;

                using (Image loadedImage = Image.FromFile(imagePath))
                {
                    Size size;

                    if (loadedImage is Metafile)
                    {
                        size = new Size(1000, (int)((1000f * ((float)loadedImage.Height / (float)loadedImage.Width)) + 0.5f));
                    }
                    else
                    {
                        size = new Size(loadedImage.Width, loadedImage.Height);
                    }

                    image = new Bitmap(size.Width, size.Height);

                    using (Graphics g = Graphics.FromImage(image))
                    {
                        g.Clear(this.BackgroundColor);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(loadedImage, 0, 0, size.Width, size.Height);
                    }
                }

                this.dialogLoadImage.FileName = this.Filename = imagePath;

                return this.LoadImage(image);
            }
            catch (OutOfMemoryException)
            { // Catch any bad image files
                MessageBox.Show(
                            Resource.GetString("Unknown or Unsupported File"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                            Resource.GetString("File Not Found"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Overriden OnResize to handle widget positions
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            base.OnResize(e);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the drag over.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private static void HandleDragOver(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileNameW") && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the DragEnter event of the rtbxConvertedText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private static void RtbxConvertedText_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Applies the text brightness contrast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApplyTextBrightnessContrast(object sender, EventArgs e)
        {
            this.Brightness = this.brightnessContrast.Brightness;
            this.Contrast = this.brightnessContrast.Contrast;

            this.ApplyTextEffects();

            this.UpdateLevelsArray();
        }

        /// <summary>
        /// Applies the text effects.
        /// </summary>
        private void ApplyTextEffects()
        {
            if (!this.ValuesCreated)
            {
                return;
            }

            this.textViewer.Lines = AscgenConverter.Convert(this.values, this.textSettings);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbxLocked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CbxLocked_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dimensionsCalculator.DimensionsAreLocked = this.cbxLocked.Checked;
        }

        /// <summary>
        /// Checks if we want to close without saving.
        /// </summary>
        /// <returns>A value with whether we are closing without saving</returns>
        private bool CheckCloseWithoutSaving()
        {
            if (this.imageSaved || !this.ImageIsLoaded || !Variables.Instance.ConfirmOnClose)
            {
                return true;
            }

            switch (MessageBox.Show(
                                Resource.GetString("Save the output before closing") + "?",
                                Resource.GetString("Warning"),
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1))
            {
                case DialogResult.Yes:      // save then close if save dialog ok
                    return this.ShowSaveDialog();

                case DialogResult.No:       // close
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Update and check if the text image is valid
        /// </summary>
        /// <returns>Is it ok for the text image to be saved?</returns>
        private bool CheckIfSavable()
        {
            if (!this.DoConvert())
            {
                if (this.textViewer.IsEmpty)
                {
                    MessageBox.Show(
                                this,
                                Resource.GetString("Invalid Output Size"),
                                Resource.GetString("Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.ServiceNotification);
                }

                return false;
            }

            if (this.Ramp.Length == 0)
            {
                MessageBox.Show(
                            this,
                            Resource.GetString("Invalid ASCII Ramp"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.ServiceNotification);

                this.cmbRamp.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ChkGenerate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.IsGeneratedRamp = this.chkGenerate.Checked;
        }

        /// <summary>
        /// Closes the image.
        /// </summary>
        /// <returns>Did we close the image?</returns>
        private bool CloseImage()
        {
            if (!this.ImageIsLoaded)
            {
                return true;
            }

            if (!this.CheckCloseWithoutSaving())
            {
                return false;
            }

            this.rtbxConvertedText.Visible = false;

            this.textViewer.Clear();

            this.widgetImage.Image = null;

            this.Filename = string.Empty;

            this.values = null;

            this.brightnessContrast.Brightness = Variables.Instance.DefaultTextBrightness;

            this.brightnessContrast.Contrast = Variables.Instance.DefaultTextContrast;

            this.MinimumLevel = this.levels.Minimum = Variables.Instance.DefaultMinimumLevel;

            this.MedianLevel = this.levels.Median = Variables.Instance.DefaultMedianLevel;

            this.MaximumLevel = this.levels.Maximum = Variables.Instance.DefaultMaximumLevel;

            this.widgetTextSettings.Refresh();

            this.AlterInputImageToolStripIsEnabled = this.widgetTextSettings.Enabled = false;

            this.UpdateMenus();

            return true;
        }

        /// <summary>
        /// Handles the DropDown event of the cmbCharacters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbCharacters_DropDown(object sender, System.EventArgs e)
        {
            int width = this.cmbCharacters.Width;

            foreach (string characters in this.cmbCharacters.Items)
            {
                Size textSize = FontFunctions.MeasureText(characters + "  ", this.cmbCharacters.Font);

                if (textSize.Width > width)
                {
                    width = textSize.Width;
                }
            }

            this.cmbCharacters.DropDownWidth = width;
        }

        /// <summary>
        /// Handles the TextChanged event of the cmbCharacters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbCharacters_TextChanged(object sender, System.EventArgs e)
        {
            if (!this.cmbCharacters.Visible || this.cmbCharacters.Text.Length < 1)
            {
                return;
            }

            this.ValidCharacters = this.cmbCharacters.Text;
        }

        /// <summary>
        /// Handles the DropDown event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_DropDown(object sender, System.EventArgs e)
        {
            int width = this.cmbRamp.Width;

            foreach (string ramp in this.cmbRamp.Items)
            {
                Size size = FontFunctions.MeasureText(ramp + "  ", this.cmbRamp.Font);

                if (size.Width > width)
                {
                    width = size.Width;
                }
            }

            this.cmbRamp.DropDownWidth = width;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.Ramp = this.cmbRamp.Text;
        }

        /// <summary>
        /// Handles the TextChanged event of the cmbRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmbRamp_TextChanged(object sender, System.EventArgs e)
        {
            this.Ramp = this.cmbRamp.Text;
        }

        /// <summary>
        /// Handles the Click event of the cmenuCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuCopy_Click(object sender, System.EventArgs e)
        {
            this.textViewer.Copy();
        }

        /// <summary>
        /// Handles the Click event of the cmenuSelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuSelectAll_Click(object sender, System.EventArgs e)
        {
            this.textViewer.SelectAll();
        }

        /// <summary>
        /// Handles the Click event of the cmenuSelectNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuSelectNone_Click(object sender, System.EventArgs e)
        {
            this.textViewer.SelectNone();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextFont_Click(object sender, System.EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextHorizontal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextHorizontal_Click(object sender, System.EventArgs e)
        {
            this.FlipHorizontally = !this.FlipHorizontally;
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningNone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningNone_Click(object sender, System.EventArgs e)
        {
            this.Unsharp = this.Sharpen = false;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningSharpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningSharpen_Click(object sender, System.EventArgs e)
        {
            this.Sharpen = true;
            this.Unsharp = false;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextSharpeningUnsharp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextSharpeningUnsharp_Click(object sender, System.EventArgs e)
        {
            this.Sharpen = false;
            this.Unsharp = true;
            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextStretch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextStretch_Click(object sender, System.EventArgs e)
        {
            this.Stretch = !this.Stretch;
            this.ApplyTextEffects();
            this.UpdateLevelsArray();
        }

        /// <summary>
        /// Handles the Click event of the cmenuTextVertical control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuTextVertical_Click(object sender, System.EventArgs e)
        {
            this.FlipVertically = !this.FlipVertically;
        }

        /// <summary>
        /// Handles the Popup event of the contextMenuText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContextMenuText_Popup(object sender, System.EventArgs e)
        {
            this.cmenuTextFont.Enabled = this.cmenuTextSelectAll.Enabled = !this.textViewer.IsEmpty;

            this.cmenuTextCopy.Enabled = this.cmenuTextSelectNone.Enabled = this.textViewer.SelectionLength > 0;

            this.cmenuTextStretch.Checked = this.Stretch;

            this.cmenuTextStretch.Enabled =
                    this.cmenuTextSharpening.Enabled =
                    this.cmenuTextSharpeningNone.Enabled =
                    this.cmenuTextSharpeningSharpen.Enabled =
                    this.cmenuTextSharpeningUnsharp.Enabled =
                    this.cmenuTextHorizontal.Enabled =
                    this.cmenuTextVertical.Enabled = this.ImageIsLoaded;

            this.cmenuTextSharpeningNone.Checked = !this.Sharpen && !this.Unsharp;
            this.cmenuTextSharpeningSharpen.Checked = this.Sharpen;
            this.cmenuTextSharpeningUnsharp.Checked = this.Unsharp;

            this.cmenuTextHorizontal.Checked = this.FlipHorizontally;
            this.cmenuTextVertical.Checked = this.FlipVertically;
        }

        /// <summary>
        /// Creates the colour image.
        /// </summary>
        /// <param name="zoom">The zoom level to use.</param>
        /// <returns>The new colour image</returns>
        private Image CreateColourImage(float zoom)
        {
            Color[][] colors = ImageToColors.Convert(
                                    this.widgetImage.Image,
                                    new Size(this.OutputWidth, this.OutputHeight),
                                    this.CurrentImageSection,
                                    (this.dialogSaveColour.FilterIndex == 1 || this.dialogSaveColour.FilterIndex == 2));

            Image image = TextToColorImage.Convert(
                                    this.textViewer.Lines,
                                    this.Font,
                                    colors,
                                    this.BackgroundColor,
                                    zoom);

            return image;
        }

        /// <summary>
        /// Handles the OnOutputSizeChanged event of the dimensionsCalculator object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DimensionsCalculator_OnOutputSizeChanged(object sender, EventArgs e)
        {
            this.UpdateTextSizeControls();

            this.inputChanged = true;

            this.OutputWidth = this.dimensionsCalculator.Width;
            this.OutputHeight = this.dimensionsCalculator.Height;

            this.DoConvert();
        }

        /// <summary>
        /// Raised when the dithering is changing
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DitheringChanging(object sender, EventArgs e)
        {
            this.Dithering = this.dither.DitherAmount;
        }

        /// <summary>
        /// Raised when the dithering random has changed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DitheringRandomChanged(object sender, EventArgs e)
        {
            this.DitheringRandom = this.dither.DitherRandom;
        }

        /// <summary>
        /// Process the conversion
        /// </summary>
        /// <returns>Did the conversion succeed?</returns>
        private bool DoConvert()
        {
            if (!this.doConversion || !this.ImageIsLoaded)
            {
                return false;
            }

            if (!this.inputChanged)
            {
                return true;
            }

            if (this.OutputWidth < 1 || this.OutputHeight < 1)
            {
                this.textViewer.Clear();

                this.tbxWidth.Focus();

                return false;
            }

            // convert the image into values
            this.values = ImageToValues.Convert(
                              this.widgetImage.Image,
                              new Size(this.OutputWidth, this.OutputHeight),
                              JMSoftware.Matrices.Identity(),
                              this.CurrentImageSection);

            if (!this.ValuesCreated)
            {
                this.textViewer.Clear();

                MessageBox.Show(
                            Resource.GetString("Out of Memory, Could not convert the image"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                return false;
            }

            this.UpdateLevelsArray();

            this.ApplyTextEffects();

            this.inputChanged = false;
            this.imageSaved = false;

            return true;
        }

        /// <summary>
        /// Function called by the PrintDocument to print the image
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        private void DocumentPrint(object sender, PrintPageEventArgs e)
        {
            if (!this.DoConvert())
            {
                return;
            }

            ImageAttributes ia = new ImageAttributes();

            Rectangle printarea = GetPrintableArea(e, true, this.printDocument.PrintController.IsPreview);

            Bitmap canvas;

            if (this.printColour)
            {
                Color[][] colors = ImageToColors.Convert(
                                    this.widgetImage.Image,
                                    new Size(this.OutputWidth, this.OutputHeight),
                                    this.CurrentImageSection,
                                    false);

                canvas = (Bitmap)TextToColorImage.Convert(
                                     this.textViewer.Lines,
                                     this.Font,
                                     colors,
                                     this.BackgroundColor,
                                     100f);
            }
            else
            {
                canvas = TextToImage.Convert(
                                     this.textViewer.Text,
                                     this.Font,
                                     this.TextColor,
                                     this.BackgroundColor);

                // convert to greyscale to avoid cleartype colours problem
                ia.SetColorMatrix(JMSoftware.Matrices.Grayscale());
            }

            this.printDocument.DocumentName = "ASCII-" + Path.GetFileNameWithoutExtension(this.Filename);

            // calculate the area that the image will cover
            float printRatio = (float)printarea.Width / (float)printarea.Height;
            float imageRatio = (float)canvas.Width / (float)canvas.Height;

            Rectangle targetLocation = new Rectangle(printarea.X, printarea.Y, printarea.Width, printarea.Height);

            if (printRatio > imageRatio)
            {
                targetLocation.Width = (int)((imageRatio * (float)printarea.Height) + 0.5);
                targetLocation.X += (printarea.Width - targetLocation.Width) / 2;
            }
            else
            {
                targetLocation.Height = (int)(((float)printarea.Width / imageRatio) + 0.5);
                targetLocation.Y += (printarea.Height - targetLocation.Height) / 2;
            }

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            e.Graphics.DrawImage(canvas, targetLocation, 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, ia);

            // Draw a border
            e.Graphics.DrawRectangle(Pens.Black, targetLocation);

            ia.Dispose();
        }

        /// <summary>
        /// Process the print request
        /// </summary>
        /// <param name="preview">Are we just showing a print preview?</param>
        /// <param name="useColor">Print with colour?</param>
        private void DoPrint(bool preview, bool useColor)
        {
            this.printColour = useColor;

            try
            {
                if (preview)
                {
                    this.printPreviewDialog.ShowDialog();
                }
                else
                {
                    this.printDocument.Print();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                        Resource.GetString("Print Error") + ": " + ex.Message,
                        Resource.GetString("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Apply the rotate/flip on the image.
        /// </summary>
        /// <param name="type">The type of rotation/flip.</param>
        private void DoRotateFlip(RotateFlipType type)
        {
            this.widgetImage.DoRotateFlip(type, this, new EventArgs());
        }

        /// <summary>
        /// Handles the Closing event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void FormConvertImage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.ImageIsLoaded)
            {
                return;
            }

            e.Cancel = !this.CheckCloseWithoutSaving();
        }

        /// <summary>
        /// Handles the drag drop.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void HandleDragDrop(DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (fileNames.Length == 1)
            {
                this.LoadImage(fileNames[0]);
            }
        }

        /// <summary>
        /// Occurs when the levels values have changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LevelsChanged(object sender, EventArgs e)
        {
            if (this.MinimumLevel == this.levels.Minimum &&
                this.MedianLevel == this.levels.Median &&
                this.MaximumLevel == this.levels.Maximum)
            {
                return;
            }

            this.MinimumLevel = this.levels.Minimum;
            this.MedianLevel = this.levels.Median;
            this.MaximumLevel = this.levels.Maximum;

            this.ApplyTextEffects();
        }

        /// <summary>
        /// Handles the Popup event of the menuEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEdit_Popup(object sender, System.EventArgs e)
        {
            this.menuEditInput.Enabled = this.menuEditOutput.Enabled = this.ImageIsLoaded;
        }

        /// <summary>
        /// Handles the Click event of the menuEditEditSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditEditSettings_Click(object sender, EventArgs e)
        {
            using (FormEditSettings settingsDialog = new FormEditSettings())
            {
                settingsDialog.InputDirectory = Variables.Instance.InitialInputDirectory;

                settingsDialog.OutputDirectory = Variables.Instance.InitialOutputDirectory;

                settingsDialog.OutputSize = new System.Drawing.Size(Variables.Instance.DefaultWidth, Variables.Instance.DefaultHeight);

                settingsDialog.DefaultFont = Variables.Instance.DefaultFont;

                settingsDialog.ConfirmOnClose = Variables.Instance.ConfirmOnClose;

                settingsDialog.CheckForNewVersions = Variables.Instance.CheckForNewVersion;

                if (settingsDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Variables.Instance.InitialInputDirectory = settingsDialog.InputDirectory;

                Variables.Instance.InitialOutputDirectory = settingsDialog.OutputDirectory;

                Variables.Instance.DefaultWidth = settingsDialog.OutputSize.Width;

                Variables.Instance.DefaultHeight = settingsDialog.OutputSize.Height;

                Variables.Instance.DefaultFont = settingsDialog.DefaultFont;

                Variables.Instance.ConfirmOnClose = settingsDialog.ConfirmOnClose;

                Variables.Instance.CheckForNewVersion = settingsDialog.CheckForNewVersions;
            }

            Variables.Instance.SaveSettings();
        }

        /// <summary>
        /// Handles the Click event of the menuEditFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditFont_Click(object sender, EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the DropDownOpening event of the menuEditInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInput_DropDownOpening(object sender, EventArgs e)
        {
            this.menuEditInput.DropDown.Enabled = this.menuEditInput.Enabled;
        }

        /// <summary>
        /// Handles the Click event of the menuEditInputFlipHorizontal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInputFlipHorizontal_Click(object sender, EventArgs e)
        {
            this.widgetImage.DoRotateFlip(RotateFlipType.RotateNoneFlipX, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menuEditInputFlipVertical control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInputFlipVertical_Click(object sender, EventArgs e)
        {
            this.widgetImage.DoRotateFlip(RotateFlipType.RotateNoneFlipY, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menuEditInputRotate180 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInputRotate180_Click(object sender, EventArgs e)
        {
            this.widgetImage.DoRotateFlip(RotateFlipType.Rotate180FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menuEditInputRotate270 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInputRotate270_Click(object sender, EventArgs e)
        {
            this.widgetImage.DoRotateFlip(RotateFlipType.Rotate270FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menuEditInputRotate90 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditInputRotate90_Click(object sender, EventArgs e)
        {
            this.widgetImage.DoRotateFlip(RotateFlipType.Rotate90FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Popup event of the menuEditOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditOutput_Popup(object sender, System.EventArgs e)
        {
            this.toolStripMenuItemInvertOutput.Checked = !this.IsBlackTextOnWhite;

            this.menuEditStretch.Checked = this.Stretch;

            this.menuEditFlipHorizontal.Checked = this.FlipHorizontally;

            this.menuEditFlipVertical.Checked = this.FlipVertically;

            this.menuEditOutput.DropDown.Enabled = this.menuEditOutput.Enabled;
        }

        /// <summary>
        /// Handles the Click event of the menuEditRampCopyRamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditRampCopyRamp_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Ramp, true);
        }

        /// <summary>
        /// Handles the DropDownOpening event of the menuEditRamps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditRamps_DropDownOpening(object sender, EventArgs e)
        {
            this.menuEditRampsCopyRamp.Enabled = this.IsFixedWidth;
        }

        /// <summary>
        /// Handles the Popup event of the menuEditSharpeningMethod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditSharpeningMethod_Popup(object sender, System.EventArgs e)
        {
            this.menuEditSharpeningMethodNone.Checked = !this.Sharpen && !this.Unsharp;

            this.menuEditSharpeningMethodSharpen.Checked = this.Sharpen;

            this.menuEditSharpeningMethodUnsharp.Checked = this.Unsharp;
        }

        /// <summary>
        /// Handles the Click event of the menuEditSpecifyCharSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditSpecifyCharSize_Click(object sender, EventArgs e)
        {
            using (CharacterSizeDialog characterDialog = new CharacterSizeDialog())
            {
                characterDialog.AutoCalculateSize = this.CalculatingCharacterSize;

                characterDialog.CharacterSize = this.CharacterSize;

                if (this.IsFixedWidth)
                {
                    characterDialog.DefaultCharacterSize = this.CalculatingCharacterSize ? this.CharacterSize : FontFunctions.GetFixedPitchFontSize(this.Font);
                }
                else
                {
                    characterDialog.DefaultCharacterSize = new Size(
                                                            ValuesToVariableWidthTextConverter.CharacterWidth,
                                                            FontFunctions.MeasureText("W", this.Font).Height);
                }

                if (characterDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.CalculatingCharacterSize = characterDialog.AutoCalculateSize;

                this.CharacterSize = characterDialog.CharacterSize;
            }
        }

        /// <summary>
        /// Handles the Click event of the menuEditValidChars control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuEditValidChars_Click(object sender, System.EventArgs e)
        {
            using (ValidRampCharsDialog validCharactersDialog = new ValidRampCharsDialog())
            {
                validCharactersDialog.Font = this.Font;

                validCharactersDialog.Characters = this.ValidCharacters;

                if (validCharactersDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.ValidCharacters = validCharactersDialog.Characters;

                this.cmbCharacters.Text = validCharactersDialog.Characters;
            }
        }

        /// <summary>
        /// Handles the Popup event of the menuFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFile_Popup(object sender, System.EventArgs e)
        {
            // enable the import item if an image is on the clipboard
            IDataObject data = Clipboard.GetDataObject();

            this.menuFileImportClipboard.Enabled = data.GetDataPresent(DataFormats.Bitmap, true);
        }

        /// <summary>
        /// Handles the Click event of the menuFileBatchConversion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileBatchConversion_Click(object sender, EventArgs e)
        {
            this.formBatchConversion.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuFileClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileClose_Click(object sender, System.EventArgs e)
        {
            if (!this.CloseImage())
            {
                return;
            }

            this.widgetTextSettings.LevelsArray = new int[256];
        }

        /// <summary>
        /// Handles the Click event of the menuFileExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the menuFileImportClipboard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileImportClipboard_Click(object sender, System.EventArgs e)
        {
            IDataObject data = Clipboard.GetDataObject();

            if (data == null || !data.GetDataPresent(DataFormats.Bitmap) || !this.CloseImage())
            {
                return;
            }

            this.Filename = string.Format(
                                            Variables.Instance.Culture,
                                            "Clipboard{0:yyyyMMddHHmmss}",
                                            System.DateTime.Now);

            this.LoadImage((Bitmap)data.GetData(DataFormats.Bitmap, true));
        }

        /// <summary>
        /// Handles the Click event of the menuFileLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileLoad_Click(object sender, System.EventArgs e)
        {
            this.ShowLoadImageDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuFilePageSetup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                this.pageSetupDialog.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(
                    Resource.GetString("Print Error") + ": " + ex.Message,
                    Resource.GetString("Error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrint_Click(object sender, EventArgs e)
        {
            if (this.printDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.DoPrint(false, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintColour control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintColour_Click(object sender, EventArgs e)
        {
            if (this.printDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.DoPrint(false, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintPreview_Click(object sender, EventArgs e)
        {
            this.DoPrint(true, false);
        }

        /// <summary>
        /// Handles the Click event of the menuFilePrintPreviewColour control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFilePrintPreviewColour_Click(object sender, EventArgs e)
        {
            this.DoPrint(true, true);
        }

        /// <summary>
        /// Handles the Click event of the menuFileSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuFileSave_Click(object sender, System.EventArgs e)
        {
            this.ShowSaveDialog();
        }

        /// <summary>
        /// Handles the Click event of the menuHelpAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpAbout_Click(object sender, System.EventArgs e)
        {
            using (AboutDialog aboutDialog = new AboutDialog())
            {
                aboutDialog.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of the menuHelpCheckForNewVersionToolStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpCheckForNewVersionToolStrip_Click(object sender, EventArgs e)
        {
            if (!this.versionChecker.Read(AscgenVersion.VersionUrl))
            {
                MessageBox.Show(Resource.GetString("Error"));

                return;
            }

            this.ShowNewVersionDialog(true);
        }

        /// <summary>
        /// Handles the Click event of the menuHelpDonate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpDonate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=58GBZPNY3YYHA&lc=GB&item_name=Ascii%20Generator%20Tip%20Jar&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHostedGuest");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpReportBug control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpReportBug_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sourceforge.net/tracker/?func=add&group_id=133786&atid=728164");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpRequestFeature control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpRequestFeature_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sourceforge.net/tracker/?func=add&group_id=133786&atid=728167");
        }

        /// <summary>
        /// Handles the Click event of the menuHelpTutorials control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuHelpTutorials_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ascgendotnet.jmsoftware.co.uk/tutorials");
        }

        /// <summary>
        /// Handles the Popup event of the menuView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuView_Popup(object sender, System.EventArgs e)
        {
            this.menuViewText.Checked = this.widgetTextSettings.Visible;

            this.toolStripMenuItemShowImage.Checked = this.widgetImage.Visible;

            this.menuViewFullScreen.Checked = this.IsFullScreen;
        }

        /// <summary>
        /// Handles the Click event of the menuViewColourPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewColourPreview_Click(object sender, EventArgs e)
        {
            this.ShowColourPreview();
        }

        /// <summary>
        /// Handles the Click event of the menuViewFullScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewFullScreen_Click(object sender, System.EventArgs e)
        {
            this.IsFullScreen = !this.IsFullScreen;
        }

        /// <summary>
        /// Handles the Click event of the menuViewText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MenuViewText_Click(object sender, System.EventArgs e)
        {
            this.ToggleTextWidget();
        }

        /// <summary>
        /// Handles the DragDrop event of the pnlMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void PnlMain_DragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the PnlMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void PnlMain_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Handles the Resize event of the pnlMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PnlMain_Resize(object sender, EventArgs e)
        {
            this.RearrangeWidgets();
        }

        /// <summary>
        /// Positions the image widget.
        /// </summary>
        private void PositionImageWidget()
        {
            this.widgetImage.Top = this.pnlMain.Height - this.widgetImage.Height - 10;

            this.widgetImage.Left = this.pnlMain.Width - this.widgetImage.Width - 10;
        }

        /// <summary>
        /// Positions the text widget.
        /// </summary>
        private void PositionTextWidget()
        {
            this.widgetTextSettings.Top = this.pnlMain.Height - this.widgetTextSettings.Height - 10;

            this.widgetTextSettings.Left = 4;
        }

        /// <summary>
        /// Updates the selection area if needed.
        /// </summary>
        private void ProcessSelectionAreaChange()
        {
            if (this.oldSelectionPosition == this.widgetImage.SelectedArea)
            {
                return;
            }

            if (this.oldSelectionPosition.Size == this.widgetImage.SelectedArea.Size)
            {
                this.inputChanged = true;

                this.DoConvert();
            }
            else
            {
                this.dimensionsCalculator.ImageSize = this.CurrentImageSection.Size;

                this.UpdateTextSizeControls();
            }

            this.oldSelectionPosition = this.widgetImage.SelectedArea;
        }

        /// <summary>
        /// Update the positions of the widgets
        /// </summary>
        private void RearrangeWidgets()
        {
            this.SuspendLayout();

            foreach (Control control in this.pnlMain.Controls)
            {
                if (control as BaseWidget == null)
                {
                    continue;
                }

                if (control.Left > (this.clientSize.Width - control.Right))
                {
                    control.Left = this.pnlMain.ClientSize.Width - (this.clientSize.Width - control.Left);
                }

                if (control.Top > (this.clientSize.Height - control.Bottom))
                {
                    control.Top = this.pnlMain.ClientSize.Height - (this.clientSize.Height - control.Top);
                }

                control.Refresh();
            }

            this.ResumeLayout();

            this.clientSize = this.pnlMain.ClientSize;
        }

        /// <summary>
        /// Handles the DragDrop event of the rtbxConvertedText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void RtbxConvertedText_DragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Show and process the dialog to save as an image
        /// </summary>
        /// <param name="file">Default filename for the output</param>
        /// <param name="useColour">Output as colour instead of black and white?</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveAsImage(string file, bool useColour)
        {
            if (useColour && !this.IsFixedWidth)
            {
                throw new ArgumentException("Cannot use colour with variable width conversions");
            }

            if (!this.CheckIfSavable())
            {
                return false;
            }

            this.dialogChooseTextZoom.TextFont = this.Font;

            this.dialogChooseTextZoom.TextColor = this.TextColor;

            this.dialogChooseTextZoom.BackgroundColor = this.BackgroundColor;

            this.dialogChooseTextZoom.InputSize =
                FontFunctions.MeasureText(this.textViewer.Text, this.Font);

            if (this.dialogChooseTextZoom.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            this.dialogSaveImage.FileName = file;

            if (this.dialogSaveImage.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string filename = this.dialogSaveImage.FileName;

            string extension = Path.GetExtension(filename).ToLower(Variables.Instance.Culture);

            switch (this.dialogSaveImage.FilterIndex)
            {
                case 1:
                    if (extension != ".bmp" && extension != ".rle" && extension != ".dib")
                    {
                        filename += ".bmp";
                    }

                    break;

                case 2:
                    if (extension != ".gif")
                    {
                        filename += ".gif";
                    }

                    break;

                case 3:
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".jpe")
                    {
                        filename += ".jpg";
                    }

                    break;

                case 4:
                    if (extension != ".png")
                    {
                        filename += ".png";
                    }

                    break;

                case 5:
                    if (extension != ".tif")
                    {
                        filename += ".tif";
                    }

                    break;
            }

            if (useColour)
            {
                using (Image image = this.CreateColourImage(this.dialogChooseTextZoom.Value))
                {
                    image.Save(filename, ImageFunctions.GetImageFormat(extension));
                }

                this.imageSaved = true;
            }
            else
            {
                this.imageSaved = TextToImage.Save(
                                    this.textViewer.Text,
                                    filename,
                                    this.Font,
                                    this.dialogChooseTextZoom.TextColor,
                                    this.dialogChooseTextZoom.BackgroundColor,
                                    this.dialogChooseTextZoom.Value,
                                    true);
            }

            return this.imageSaved;
        }

        /// <summary>
        /// Show and process the dialog to save as colour text
        /// </summary>
        /// <param name="filename">Default filename for the output</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveColourTextDialog(string filename)
        {
            if (!this.IsFixedWidth)
            {
                throw new InvalidOperationException("Cannot use colour with variable width conversions");
            }

            if (!(this.CheckIfSavable() && this.IsFixedWidth))
            {
                return false;
            }

            this.dialogSaveColour.FileName = filename;

            if (this.dialogSaveColour.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            // create the array of Colors
            Color[][] colors = ImageToColors.Convert(
                                this.widgetImage.Image,
                                new Size(this.OutputWidth, this.OutputHeight),
                                this.CurrentImageSection,
                                (this.dialogSaveColour.FilterIndex == 1 || this.dialogSaveColour.FilterIndex == 2));

            string[] strings = AscgenConverter.Convert(this.values, this.textSettings);

            string output;

            System.Text.Encoding encoding;

            OutputCreator outputCreator = new OutputCreator(strings, this.textSettings, colors);

            switch (this.dialogSaveColour.FilterIndex)
            {
                case 2: // rtf 256 color
                case 4: // rtf 24-bit
                    output = outputCreator.CreateRtf();

                    encoding = System.Text.Encoding.ASCII;

                    break;

                default: // html 24-bit /  256 color
                    outputCreator.Title = Path.GetFileNameWithoutExtension(this.Filename);

                    output = outputCreator.CreateHtml();

                    encoding = System.Text.Encoding.UTF8;

                    break;
            }

            using (StreamWriter writer = new StreamWriter(this.dialogSaveColour.FileName, false, encoding))
            {
                writer.Write(output);
            }

            this.imageSaved = true;

            return true;
        }

        /// <summary>
        /// Show and process the dialog to save as text
        /// </summary>
        /// <param name="filename">Default filename for the output</param>
        /// <returns>Was the file saved?</returns>
        private bool SaveTextDialog(string filename)
        {
            if (!this.CheckIfSavable())
            {
                return false;
            }

            this.dialogSaveText.FileName = filename;

            if (this.dialogSaveText.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            RichTextBoxStreamType streamType = RichTextBoxStreamType.PlainText;

            bool saved = false;

            switch (this.dialogSaveText.FilterIndex)
            {
                case 1: // plain text
                    streamType = RichTextBoxStreamType.PlainText;
                    break;

                case 2: // plain text (unicode)
                case 3: // nfo
                    streamType = RichTextBoxStreamType.UnicodePlainText;
                    break;

                case 4: // rich text
                    streamType = RichTextBoxStreamType.RichText;
                    break;

                case 5: // XHTML
                    using (StreamWriter writer = new StreamWriter(this.dialogSaveText.FileName))
                    {
                        OutputCreator outputCreator = new OutputCreator(this.textViewer.Lines, this.textSettings)
                            {
                                Title = Path.GetFileNameWithoutExtension(this.Filename)
                            };

                        writer.Write(outputCreator.CreateHtml());
                    }

                    saved = true;
                    break;
            }

            if (!saved)
            {
                this.rtbxConvertedText.SaveFile(this.dialogSaveText.FileName, streamType);
            }

            this.imageSaved = true;

            return true;
        }

        /// <summary>
        /// Sets up the forms controls.
        /// </summary>
        private void SetupControls()
        {
            this.tbxWidth.MaxLength = Variables.Instance.MaximumWidth.ToString(Variables.Instance.Culture).Length;

            this.tbxHeight.MaxLength = Variables.Instance.MaximumHeight.ToString(Variables.Instance.Culture).Length;

            this.cbxLocked.Checked = Variables.Instance.DefaultWidth < 1 || Variables.Instance.DefaultHeight < 1;

            this.rtbxConvertedText.AllowDrop = true;
            this.rtbxConvertedText.DragDrop += this.RtbxConvertedText_DragDrop;
            this.rtbxConvertedText.DragEnter += RtbxConvertedText_DragEnter;

            this.rtbxConvertedText.Visible = false;

            this.pnlMain.AllowDrop = true;

            this.textViewer.BackgroundColor = this.BackgroundColor;
            this.textViewer.TextColor = this.TextColor;

            this.SetupToolstrip();

            this.cmbRamp.Items.Clear();
            this.cmbRamp.Items.AddRange(Variables.Instance.DefaultRamps);

            if (Variables.Instance.CurrentSelectedRamp == -1)
            {
                this.cmbRamp.Text = Variables.Instance.CurrentRamp;
            }
            else
            {
                this.cmbRamp.SelectedIndex = Variables.Instance.CurrentSelectedRamp;
            }

            this.cmbRamp.Select(0, 0);

            this.IsGeneratedRamp = Variables.Instance.UseGeneratedRamp;

            this.cmbCharacters.Items.Clear();
            this.cmbCharacters.Items.AddRange(Variables.Instance.DefaultValidCharacters);

            if (Variables.Instance.CurrentSelectedValidCharacters == -1)
            {
                this.cmbCharacters.Text = Variables.Instance.CurrentCharacters;
            }
            else
            {
                this.cmbCharacters.SelectedIndex = Variables.Instance.CurrentSelectedValidCharacters;
            }

            // make sure the text isn't selected
            this.cmbCharacters.Select(0, 0);

            this.UpdateUI();

            this.UpdateMenus();
        }

        /// <summary>
        /// Empties and readds the toolstrips to get the desired layout (it adds from the bottom up).
        /// </summary>
        private void SetupToolstrip()
        {
            this.toolStripContainer1.TopToolStripPanel.Controls.Clear();

            this.toolStripContainer1.TopToolStripPanel.Join(this.mainMenu1);

            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripDisplay, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripWidgets, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripRotateFlip, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripFont, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripOutputSize, 1);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripFile, 1);

            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripCharacters, 2);
            this.toolStripContainer1.TopToolStripPanel.Join(this.toolStripRamp, 2);

            foreach (ToolStrip strip in this.toolStripContainer1.TopToolStripPanel.Controls)
            {
                strip.BackColor = this.toolStripContainer1.TopToolStripPanel.BackColor;
                strip.GripMargin = new Padding(0);
                strip.GripStyle = ToolStripGripStyle.Hidden;
            }
        }

        /// <summary>
        /// Create and set up the widgets.
        /// </summary>
        private void SetupWidgets()
        {
            this.widgetTextSettings = new WidgetTextSettings
                {
                    MaximumBrightness = 200,
                    MinimumBrightness = -200,
                    MaximumContrast = 100,
                    MinimumContrast = -100,
                    Brightness = Variables.Instance.DefaultTextBrightness,
                    Contrast = Variables.Instance.DefaultTextContrast,
                    Minimum = Variables.Instance.DefaultMinimumLevel,
                    Maximum = Variables.Instance.DefaultMaximumLevel,
                    Median = Variables.Instance.DefaultMedianLevel,
                    DitherAmount = Variables.Instance.DefaultDitheringLevel,
                    DitherRandom = Variables.Instance.DefaultDitheringRandom,
                    Enabled = false,
                    Visible = Variables.Instance.ShowWidgetTextSettings
                };

            this.PositionTextWidget();

            this.widgetTextSettings.ValueChanging += this.ApplyTextBrightnessContrast;

            this.widgetTextSettings.ValueChanged += this.ApplyTextBrightnessContrast;

            this.widgetTextSettings.LevelsChanged += this.LevelsChanged;

            this.widgetTextSettings.DitheringChanging += this.DitheringChanging;

            this.widgetTextSettings.DitheringChanged += this.DitheringChanging;

            this.widgetTextSettings.DitheringRandomChanged += this.DitheringRandomChanged;

            this.brightnessContrast = this.widgetTextSettings;

            this.levels = this.widgetTextSettings;

            this.dither = this.widgetTextSettings;

            this.widgetImage = new WidgetImage
                {
                    Visible = Variables.Instance.ShowWidgetImage,
                    SelectionBorderColor = Variables.Instance.SelectionBorderColor,
                    SelectionFillColor = Variables.Instance.SelectionFillColor
                };

            this.PositionImageWidget();

            this.widgetImage.SelectionChanging += this.WidgetImage_SelectionChanging;

            this.widgetImage.SelectionChanged += this.WidgetImage_SelectionChanged;

            this.widgetImage.DoubleClick += this.WidgetImage_DoubleClick;

            this.widgetImage.OnDragDrop += this.WidgetImage_OnDragDrop;

            this.widgetImage.LoadImage += this.WidgetImage_LoadImage;

            this.widgetImage.ImageUpdated += this.WidgetImage_ImageUpdated;

            this.pnlMain.Controls.AddRange(new Control[] { this.widgetTextSettings, this.widgetImage });

            foreach (Control control in this.pnlMain.Controls)
            {
                if (control is BaseWidget)
                {
                    control.BringToFront();
                }
            }
        }

        /// <summary>
        /// Shows the colour preview.
        /// </summary>
        private void ShowColourPreview()
        {
            if (!this.IsFixedWidth)
            {
                throw new InvalidOperationException("Cannot use colour with variable width conversions");
            }

            using (FormColourPreview preview = new FormColourPreview())
            {
                preview.Image = this.CreateColourImage(100);

                preview.ShowDialog(this);
            }
        }

        /// <summary>
        /// Display the font dialog and process the result
        /// </summary>
        private void ShowFontDialog()
        {
            try
            {
                if (this.dialogChooseFont.ShowDialog() == DialogResult.OK)
                {
                    this.Font = this.dialogChooseFont.Font;
                }
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show(
                            Resource.GetString("Unable to select this font"),
                            Resource.GetString("Error"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.ServiceNotification);
            }
        }

        /// <summary>
        /// Shows the load image dialog, and processess its result
        /// </summary>
        private void ShowLoadImageDialog()
        {
            if (this.dialogLoadImage.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.LoadImage(this.dialogLoadImage.FileName);
        }

        /// <summary>
        /// Shows the new version dialog.
        /// </summary>
        /// <param name="showVersionUpToDateDialog">if set to <c>true</c>, shows the version is up to date dialog.</param>
        private void ShowNewVersionDialog(bool showVersionUpToDateDialog)
        {
            Version latest = this.versionChecker.Version;

            if (latest == null)
            {
                return;
            }

            Version current = new Version(
                                        AscgenVersion.Major,
                                        AscgenVersion.Minor,
                                        AscgenVersion.Patch,
                                        AscgenVersion.SuffixNumber,
                                        AscgenVersion.Suffix);

            if (latest > current)
            {
                string text = string.Format(Resource.GetString("Version {0} is available"), this.versionChecker.Version);

                if (MessageBox.Show(Resource.GetString("Open the download page") + "?", text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(latest.DownloadUrl);
                }
            }
            else if (showVersionUpToDateDialog)
            {
                MessageBox.Show(Resource.GetString("This is the latest version"), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Shows the save dialog.
        /// </summary>
        /// <returns>Did we save the image?</returns>
        private bool ShowSaveDialog()
        {
            this.formSaveAs.IsFixedWidth = this.IsFixedWidth;

            bool saved = false;

            bool isText = this.formSaveAs.IsText;

            bool isColour = this.formSaveAs.IsColour;

            Size size = this.formSaveAs.Size;

            if (this.formSaveAs.ShowDialog() == DialogResult.OK)
            {
                string filename = Variables.Instance.Prefix + Path.GetFileNameWithoutExtension(this.Filename);

                if (this.formSaveAs.IsText)
                {
                    saved = this.formSaveAs.IsColour ? this.SaveColourTextDialog(filename) : this.SaveTextDialog(filename);
                }
                else
                {
                    saved = this.SaveAsImage(filename, this.formSaveAs.IsColour);
                }
            }

            if (!saved)
            {
                this.formSaveAs.IsText = isText;
                this.formSaveAs.IsColour = isColour;
                this.formSaveAs.Size = size;
            }

            return saved;
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxHeight_TextChanged(object sender, System.EventArgs e)
        {
            if (this.tbxHeight.Text == this.dimensionsCalculator.Height.ToString())
            {
                return;
            }

            try
            {
                this.dimensionsCalculator.Height = Convert.ToInt32(this.tbxHeight.Text, Variables.Instance.Culture);
            }
            catch (FormatException)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxWidth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxWidth_TextChanged(object sender, System.EventArgs e)
        {
            if (this.tbxWidth.Text == this.dimensionsCalculator.Width.ToString())
            {
                return;
            }

            try
            {
                this.dimensionsCalculator.Width = Convert.ToInt32(this.tbxWidth.Text, Variables.Instance.Culture);
            }
            catch (FormatException)
            {
                return;
            }
        }

        /// <summary>
        /// Toggles the image widget.
        /// </summary>
        private void ToggleImageWidget()
        {
            this.toolStripButtonShowImageWidget.Checked = this.widgetImage.Visible = !this.widgetImage.Visible;
        }

        /// <summary>
        /// Toggles the text widget.
        /// </summary>
        private void ToggleTextWidget()
        {
            this.toolStripButtonShowTextWidget.Checked = this.widgetTextSettings.Visible = !this.widgetTextSettings.Visible;

            this.widgetTextSettings.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonBlackOnWhite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonBlackOnWhite_Click(object sender, EventArgs e)
        {
            this.IsBlackTextOnWhite = !this.IsBlackTextOnWhite;
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonFullScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonFullScreen_Click(object sender, EventArgs e)
        {
            this.IsFullScreen = !this.IsFullScreen;
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonLoad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonLoad_Click(object sender, EventArgs e)
        {
            this.ShowLoadImageDialog();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonPreview_Click(object sender, EventArgs e)
        {
            this.ShowColourPreview();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.ShowSaveDialog();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonShowImageWidget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonShowImageWidget_Click(object sender, EventArgs e)
        {
            this.ToggleImageWidget();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonShowTextWidget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripButtonShowTextWidget_Click(object sender, EventArgs e)
        {
            this.ToggleTextWidget();
        }

        /// <summary>
        /// Handles the Click event of the ToolStripMenuItemFAQ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripMenuItemFAQ_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ascgendotnet.jmsoftware.co.uk/help/faq");
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItemInvertOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripMenuItemInvertOutput_Click(object sender, EventArgs e)
        {
            this.IsBlackTextOnWhite = !this.IsBlackTextOnWhite;
        }

        /// <summary>
        /// Handles the Click event of the toolStripMenuItemShowImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ToolStripMenuItemShowImage_Click(object sender, EventArgs e)
        {
            this.ToggleImageWidget();
        }

        /// <summary>
        /// Handles the Click event of the tsbFlipHorizontally control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFlipHorizontally_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        /// <summary>
        /// Handles the Click event of the tsbFlipVertically control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFlipVertically_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        /// <summary>
        /// Handles the Click event of the tsbFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TsbFont_Click(object sender, System.EventArgs e)
        {
            this.ShowFontDialog();
        }

        /// <summary>
        /// Handles the Click event of the tstripRotateAnticlockwise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TstripRotateAnticlockwise_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        /// <summary>
        /// Handles the Click event of the tstripRotateClockwise control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TstripRotateClockwise_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        /// <summary>
        /// Updates the levels array.
        /// </summary>
        private void UpdateLevelsArray()
        {
            if (!this.ValuesCreated)
            {
                return;
            }

            byte[][] bytes = this.values;

            if (this.Brightness != 0 || this.Contrast != 0)
            {
                BrightnessContrast filter = new BrightnessContrast(
                    this.IsBlackTextOnWhite ? this.Brightness : -this.Brightness,
                    this.IsBlackTextOnWhite ? this.Contrast : -this.Contrast);

                bytes = filter.Apply(this.values);
            }

            if (this.Stretch)
            {
                Stretch filter = new Stretch();
                bytes = filter.Apply(bytes);
            }

            // Update the levels graph
            int[] levels = new int[256];

            for (int y = 0; y < this.OutputHeight; y++)
            {
                for (int x = 0; x < this.OutputWidth; x++)
                {
                    levels[(int)bytes[y][x]]++;
                }
            }

            this.widgetTextSettings.LevelsArray = levels;
        }

        /// <summary>
        /// Set enabled status for the menus
        /// </summary>
        private void UpdateMenus()
        {
            this.menuFileSaveAs.Enabled =
                this.menuFileClose.Enabled =
                this.menuFilePrint.Enabled =
                this.menuFilePrintPreview.Enabled =
                this.toolStripButtonSave.Enabled =
                    this.ImageIsLoaded;

            this.toolStripButtonPreview.Enabled =
                this.menuViewColourPreview.Enabled =
                this.menuFilePrintColour.Enabled =
                this.menuFilePrintPreviewColour.Enabled =
                    this.IsFixedWidth && this.ImageIsLoaded;
        }

        /// <summary>
        /// Updates the text size controls.
        /// </summary>
        private void UpdateTextSizeControls()
        {
            this.tbxWidth.Text = this.dimensionsCalculator.Width.ToString();
            this.tbxHeight.Text = this.dimensionsCalculator.Height.ToString();
            this.toolStripOutputSize.Refresh();
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.menuFile.Text = Resource.GetString("&File");
            this.menuFileLoad.Text = Resource.GetString("&Load Image") + "...";
            this.menuFileClose.Text = Resource.GetString("&Close");
            this.menuFileSaveAs.Text = "&" + Resource.GetString("Save As") + "...";
            this.menuFileExit.Text = Resource.GetString("E&xit");
            this.menuFileImportClipboard.Text = Resource.GetString("I&mport from Clipboard");
            this.menuFileBatchConversion.Text = Resource.GetString("Batch Conversion") + "...";
            this.menuFilePrint.Text = Resource.GetString("Print") + "...";
            this.menuFilePrintPreview.Text = Resource.GetString("Print Preview") + "...";
            this.menuFilePrintColour.Text = Resource.GetString("Print Colour") + "...";
            this.menuFilePrintPreviewColour.Text = Resource.GetString("Colour Print Preview") + "...";
            this.menuFilePageSetup.Text = Resource.GetString("Page Setup") + "...";

            this.menuEdit.Text = "&" + Resource.GetString("Edit");
            this.menuEditFlipHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.menuEditFlipVertical.Text = Resource.GetString("Flip Vertically");
            this.menuEditInput.Text = Resource.GetString("Input");
            this.menuEditInputRotate90.Text = Resource.GetString("Rotate") + " 90°";
            this.menuEditInputRotate180.Text = Resource.GetString("Rotate") + " 180°";
            this.menuEditInputRotate270.Text = Resource.GetString("Rotate") + " 270°";
            this.menuEditInputFlipHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.menuEditInputFlipVertical.Text = Resource.GetString("Flip Vertically");
            this.menuEditOutput.Text = Resource.GetString("Output");
            this.menuEditSharpeningMethod.Text = Resource.GetString("Sharpening Method");
            this.menuEditSharpeningMethodNone.Text = Resource.GetString("None");
            this.menuEditSharpeningMethodSharpen.Text = Resource.GetString("Sharpen");
            this.menuEditSharpeningMethodUnsharp.Text = Resource.GetString("Unsharp Mask");
            this.menuEditFontsSpecifyCharSize.Text = Resource.GetString("Specify Character Size") + "...";
            this.menuEditStretch.Text = Resource.GetString("Stretch");
            this.menuEditRamps.Text = Resource.GetString("Ramps");
            this.menuEditRampsValidChars.Text = Resource.GetString("Valid Characters") + "...";
            this.menuEditRampsCopyRamp.Text = Resource.GetString("Copy Ramp to Clipboard");
            this.menuEditFontsFont.Text = Resource.GetString("Font") + "...";
            this.menuEditFonts.Text = Resource.GetString("Fonts");
            this.menuEditEditSettings.Text = Resource.GetString("Edit Settings") + "...";

            this.menuView.Text = Resource.GetString("&View");
            this.menuViewColourPreview.Text = Resource.GetString("Colour Preview") + "...";

            this.menuViewText.Text = Resource.GetString("Text Settings");
            this.toolStripMenuItemShowImage.Text = Resource.GetString("Input Image");
            this.menuViewFullScreen.Text = Resource.GetString("&Full Screen");

            this.menuHelp.Text = Resource.GetString("&Help");
            this.menuHelpTutorials.Text = Resource.GetString("Tutorials") + "...";
            this.toolStripMenuItemFAQ.Text = Resource.GetString("FAQ") + "...";
            this.menuHelpRequestFeature.Text = Resource.GetString("Request a Feature") + "...";
            this.menuHelpReportBug.Text = Resource.GetString("Report a Bug") + "...";
            this.menuHelpDonate.Text = Resource.GetString("&Donate") + "...";
            this.menuHelpCheckForNewVersionToolStrip.Text = Resource.GetString("Check for a New Version") + "...";
            this.menuHelpAbout.Text = Resource.GetString("&About") + "...";

            this.toolStripLabelSize.Text = Resource.GetString("Size") + ":";
            this.lblRamp.Text = Resource.GetString("Ramp") + ":";
            this.tsbFont.Text = Resource.GetString("Font") + "...";
            this.chkGenerate.Text = Resource.GetString("Auto");

            this.lblCharacters.Text = Resource.GetString("Characters") + ":";

            this.cmenuTextCopy.Text = Resource.GetString("Copy");
            this.cmenuTextSelectAll.Text = Resource.GetString("Select All");
            this.cmenuTextSelectNone.Text = Resource.GetString("Select None");

            this.toolStripMenuItemInvertOutput.Text = Resource.GetString("Invert the Output");
            this.cmenuTextStretch.Text = Resource.GetString("Stretch");
            this.cmenuTextSharpening.Text = Resource.GetString("Sharpening Method");
            this.cmenuTextSharpeningNone.Text = Resource.GetString("None");
            this.cmenuTextSharpeningSharpen.Text = Resource.GetString("Sharpen");
            this.cmenuTextSharpeningUnsharp.Text = Resource.GetString("Unsharp Mask");
            this.cmenuTextFont.Text = Resource.GetString("Font") + "...";
            this.cmenuTextVertical.Text = Resource.GetString("Flip Vertically");
            this.cmenuTextHorizontal.Text = Resource.GetString("Flip Horizontally");
            this.toolStripButtonShowTextWidget.ToolTipText = Resource.GetString("Text Settings");
            this.toolStripButtonShowImageWidget.ToolTipText = Resource.GetString("Input Image");

            this.toolStripButtonLoad.ToolTipText = Resource.GetString("&Load Image").Replace("&", string.Empty);
            this.toolStripButtonSave.ToolTipText = Resource.GetString("Save");

            this.tsbFont.ToolTipText = Resource.GetString("Choose the Font");

            this.tsbRotateClockwise.ToolTipText = Resource.GetString("Rotate Clockwise");
            this.tsbRotateAnticlockwise.ToolTipText = Resource.GetString("Rotate Anticlockwise");
            this.tsbFlipHorizontally.ToolTipText = Resource.GetString("Flip Horizontally");
            this.tsbFlipVertically.ToolTipText = Resource.GetString("Flip Vertically");

            this.toolStripButtonBlackOnWhite.ToolTipText = Resource.GetString("Invert the Output");
            this.toolStripButtonPreview.ToolTipText = Resource.GetString("Colour Preview");
            this.toolStripButtonFullScreen.ToolTipText = Resource.GetString("Full Screen") + " (F11)";

            this.dialogSaveText.Title = Resource.GetString("Save to a Text File") + "...";

            this.widgetImage.DisplayText = Resource.GetString("Doubleclick to load an image, or drag and drop here.") +
                Environment.NewLine + Environment.NewLine + Resource.GetString("Click and drag on an image to select an area");

            this.dialogLoadImage.Filter =
                Resource.GetString("Image Files") + "|*.bmp;*.rle;*.dib;*.exif;*.gif;*.jpg;*.jpeg;*.jpe;*.png;*.tif;*.tiff;*.wmf;*.emf|" +
                Resource.GetString("Bitmap Images") + " (*.bmp, *.rle, *.dib)|*.bmp;*.rle;*.dib|" +
                Resource.GetString("Exchangeable Image Files") + " (*.exif)|*.exif|" +
                Resource.GetString("GIF Images") + " (*.gif)|*.gif|" +
                Resource.GetString("JPEG Images") + " (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;*.jpg|" +
                Resource.GetString("Portable Network Graphics Images") + " (*.png)|*.png|" +
                Resource.GetString("TIF Images") + " (*.tif, *.tiff)|*.tif;*.tiff|" +
                Resource.GetString("Windows Metafile Images") + " (*.emf, *.wmf)|*.emf;*.wmf|" +
                Resource.GetString("All Files") + " (*.*)|*.*";

            this.dialogSaveImage.Filter =
                Resource.GetString("Bitmap Images") + " (*.bmp, *.rle, *.dib)|*.bmp;*.rle;*.dib|" +
                Resource.GetString("GIF Images") + " (*.gif)|*.gif|" +
                Resource.GetString("JPEG Images") + " (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;*.jpg|" +
                Resource.GetString("Portable Network Graphics Images") + " (*.png)|*.png|" +
                Resource.GetString("TIF Images") + " (*.tif, *.tiff)|*.tif;*.tiff|" +
                Resource.GetString("All Files") + " (*.*)|*.*";

            this.dialogSaveImage.FilterIndex = 2; // gif = smallest

            this.dialogSaveText.Filter = Resource.GetString("Plain Text") + "|*.txt|" +
                Resource.GetString("Plain Text") + " (Unicode)|*.txt|" +
                "NFO|*.nfo|" +
                Resource.GetString("Rich Text") + "|*.rtf|" +
                "XHTML 1.1|*.html|" +
                Resource.GetString("All Files") + "|*.*";
            this.dialogSaveText.FilterIndex = 1;

            this.dialogSaveColour.Filter = "XHTML 1.1 (8-bit)|*.html|" +
                Resource.GetString("Rich Text") + " (8-bit)|*.rtf|" +
                "XHTML 1.1 (24-bit)|*.html|" +
                Resource.GetString("Rich Text") + " (24-bit)|*.rtf";
            this.dialogSaveColour.FilterIndex = 1;

            this.widgetTextSettings.UpdateUI();

            this.widgetImage.UpdateUI();

            this.formSaveAs.UpdateUI();
        }

        /// <summary>
        /// Called when the asynchronous version read has completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VersionReadAsyncCompletedEventHandler(object sender, EventArgs e)
        {
            this.ShowNewVersionDialog(false);
        }

        /// <summary>
        /// Handles the OnDoubleClick event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_DoubleClick(object sender, EventArgs e)
        {
            this.ShowLoadImageDialog();
        }

        /// <summary>
        /// Handles the ImageUpdated event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_ImageUpdated(object sender, EventArgs e)
        {
            this.inputChanged = true;

            this.dimensionsCalculator.ImageSize = this.CurrentImageSection.Size;

            this.DoConvert();
        }

        /// <summary>
        /// Handles the LoadImage event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_LoadImage(object sender, EventArgs e)
        {
            this.ShowLoadImageDialog();
        }

        /// <summary>
        /// Handles the OnDragDrop event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_OnDragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_SelectionChanged(object sender, EventArgs e)
        {
            this.ProcessSelectionAreaChange();
        }

        /// <summary>
        /// Handles the SelectionChanging event of the widgetImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WidgetImage_SelectionChanging(object sender, EventArgs e)
        {
            if (!Variables.Instance.UpdateWhileSelecting)
            {
                return;
            }

            this.ProcessSelectionAreaChange();
        }

        #endregion Private methods
    }
}