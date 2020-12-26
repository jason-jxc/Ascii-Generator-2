//---------------------------------------------------------------------------------------
// <copyright file="FormBatchConversion.cs" company="Jonathan Mathews Software">
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
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using JMSoftware.AsciiConversion;
    using JMSoftware.ImageHelper;
    using JMSoftware.TextHelper;

    /// <summary>
    /// Form to handle batch conversions
    /// </summary>
    public partial class FormBatchConversion : Form
    {
        #region Fields

        /// <summary>
        /// Dimension calculator for the output sizes
        /// </summary>
        private DimensionsCalculator dimensionsCalculator;

        /// <summary>
        /// The output directory
        /// </summary>
        private string outputDirectory;

        /// <summary>
        /// The size of the output
        /// </summary>
        private Size outputSize;

        /// <summary>
        /// Stores the previously selected output image extension
        /// </summary>
        private int previousImageExtension;

        /// <summary>
        /// Stores the previously selected output text extension
        /// </summary>
        private int previousTextExtension;

        /// <summary>
        /// Stores the previous output type
        /// </summary>
        private int previousType = -1;

        /// <summary>
        /// The dimension which has not been set
        /// </summary>
        private TextBox textBoxOtherDimension;

        /// <summary>
        /// The settings used for the conversions
        /// </summary>
        private BatchTextProcessingSettings textProcessingSettings;

        /// <summary>
        /// The conversion thread
        /// </summary>
        private Thread thread;

        /// <summary>
        /// List of valid file extensions
        /// </summary>
        private List<string> validFiletypes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormBatchConversion"/> class.
        /// </summary>
        public FormBatchConversion()
        {
            this.InitializeComponent();

            this.UpdateUI();

            this.OutputLogHeader();

            this.validFiletypes = new List<string>(new[] { ".bmp", ".rle", ".dib", ".exif", ".gif", ".jpg", ".jpeg", ".jpe", ".png", ".tif", ".tiff", ".wmf", ".emf" });

            this.comboBoxOutputFormat.DataSource = Enum.GetValues(typeof(TextTypes));

            this.numericUpDownImageScale.Enabled = false;

            this.fileListbox1.AllowDrop = true;
            this.fileListbox1.DragEnter += this.FileListbox1_DragEnter;
            this.fileListbox1.DragDrop += this.FileListbox1_DragDrop;

            this.textProcessingSettings = new BatchTextProcessingSettings { Font = Variables.Instance.DefaultFont };

            this.folderBrowserDialogInput.SelectedPath = this.openFileDialogInput.InitialDirectory = Variables.Instance.InitialInputDirectory;

            this.OutputDirectory = Variables.Instance.InitialOutputDirectory;

            if (string.IsNullOrEmpty(this.OutputDirectory))
            {
                this.OutputDirectory = Directory.GetCurrentDirectory();
            }

            this.SetupOutputSize();

            this.removeFileAfterConversionToolStripMenuItem.Checked = true;

            this.buttonStop.Enabled = false;

            this.propertyGridConversionSettings.SelectedObject = this.textProcessingSettings;
        }

        #endregion Constructors

        #region Enums

        /// <summary>
        /// Types of text files
        /// </summary>
        private enum TextTypes
        {
            /// <summary>Text files</summary>
            txt,

            /// <summary>.html files</summary>
            html,

            /// <summary>Richtext files</summary>
            rtf
        }

        /// <summary>
        /// Types of image files
        /// </summary>
        private enum ImageTypes
        {
            /// <summary>Bitmap files</summary>
            bmp,

            /// <summary>.gif files</summary>
            gif,

            /// <summary>.jpg files</summary>
            jpg,

            /// <summary>.png files</summary>
            png,

            /// <summary>.tif files</summary>
            tif
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the controls are enabled.
        /// </summary>
        /// <value><c>true</c> if controls enabled; otherwise, <c>false</c>.</value>
        public bool ControlsEnabled
        {
            get
            {
                return this.buttonStart.Enabled && this.splitContainerConversions.Enabled && this.propertyGridConversionSettings.Enabled;
            }

            set
            {
                this.buttonStart.Enabled = this.splitContainerConversions.Enabled = this.propertyGridConversionSettings.Enabled = value;
                this.buttonStop.Enabled = !value;
            }
        }

        /// <summary>
        /// Gets or sets the directory for the files to be saved into.
        /// </summary>
        /// <value>The output directory.</value>
        public string OutputDirectory
        {
            get
            {
                return this.outputDirectory;
            }

            set
            {
                if (this.outputDirectory == value || value.Length == 0)
                {
                    return;
                }

                this.outputDirectory = value;

                if (!Directory.Exists(this.outputDirectory))
                {
                    this.outputDirectory = string.Empty;
                    return;
                }

                if (this.outputDirectory[this.outputDirectory.Length - 1] != Path.DirectorySeparatorChar)
                {
                    this.outputDirectory += Path.DirectorySeparatorChar;
                }

                this.folderBrowserDialogOutput.SelectedPath = this.textBoxOutputDirectory.Text = this.outputDirectory;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the output format is HTML.
        /// </summary>
        /// <value><c>true</c> if output is HTML; otherwise, <c>false</c>.</value>
        private bool OutputIsHtml
        {
            get
            {
                return this.OutputIsText && this.comboBoxOutputFormat.SelectedIndex == 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the output type is image files.
        /// </summary>
        /// <value><c>true</c> if output is image; otherwise, <c>false</c>.</value>
        private bool OutputIsImage
        {
            get
            {
                return this.comboBoxOutputType.SelectedIndex == 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the output format is RTF.
        /// </summary>
        /// <value><c>true</c> if output is RTF; otherwise, <c>false</c>.</value>
        private bool OutputIsRtf
        {
            get
            {
                return this.OutputIsText && this.comboBoxOutputFormat.SelectedIndex == 2;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the output type is text files.
        /// </summary>
        /// <value><c>true</c> if output is text; otherwise, <c>false</c>.</value>
        private bool OutputIsText
        {
            get
            {
                return this.comboBoxOutputType.SelectedIndex == 0;
            }
        }

        /// <summary>
        /// Gets or sets the size of the output.
        /// </summary>
        /// <value>The size of the output.</value>
        private Size OutputSize
        {
            get
            {
                // Width and Height specified
                if (!this.checkBoxLockRatio.Checked)
                {
                    return this.outputSize;
                }

                // Width specified
                if (this.textBoxOtherDimension == this.textBoxHeight)
                {
                    return new Size(this.outputSize.Width, -1);
                }

                // Height specified
                return new Size(-1, this.outputSize.Height);
            }

            set
            {
                this.outputSize = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        public void UpdateUI()
        {
            this.Text = Resource.GetString("Batch Conversion");

            this.tabPageConversions.Text = Resource.GetString("Convert");
            this.tabPageAdvanced.Text = Resource.GetString("Advanced");
            this.tabPageLog.Text = Resource.GetString("Log");

            this.toolTip1.SetToolTip(this.buttonAddImage, Resource.GetString("Add File"));
            this.toolTip1.SetToolTip(this.buttonRemoveImage, Resource.GetString("Remove"));
            this.toolTip1.SetToolTip(this.buttonAddFolder, Resource.GetString("Add Directory"));
            this.toolTip1.SetToolTip(this.buttonStart, Resource.GetString("Convert"));
            this.toolTip1.SetToolTip(this.fileListbox1, Resource.GetString("Files to be converted"));

            this.openFileDialogInput.Filter =
                Resource.GetString("Image Files") + "|*.bmp;*.rle;*.dib;*.exif;*.gif;*.jpg;*.jpeg;*.jpe;*.png;*.tif;*.tiff;*.wmf;*.emf|" +
                Resource.GetString("Bitmap Images") + " (*.bmp, *.rle, *.dib)|*.bmp;*.rle;*.dib|" +
                Resource.GetString("Exchangeable Image Files") + " (*.exif)|*.exif|" +
                Resource.GetString("GIF Images") + " (*.gif)|*.gif|" +
                Resource.GetString("JPEG Images") + " (*.jpg, *.jpeg, *.jpe)|*.jpg;*.jpeg;*.jpg|" +
                Resource.GetString("Portable Network Graphics Images") + " (*.png)|*.png|" +
                Resource.GetString("TIF Images") + " (*.tif, *.tiff)|*.tif;*.tiff|" +
                Resource.GetString("Windows Metafile Images") + " (*.emf, *.wmf)|*.emf;*.wmf|" +
                Resource.GetString("Image Files") + " (*.*)|*.*";

            const int BorderSize = 6;

            this.labelOutputDirectory.Text = Resource.GetString("Output Directory") + ":";
            this.textBoxOutputDirectory.Left = this.labelOutputDirectory.Right + BorderSize;
            this.textBoxOutputDirectory.Width = this.buttonOutputDirectory.Left - BorderSize - this.textBoxOutputDirectory.Left;

            this.buttonFont.Text = Resource.GetString("Font") + "...";

            this.folderBrowserDialogInput.Description = Resource.GetString("Select a directory from which to import image files");
            this.folderBrowserDialogOutput.Description = Resource.GetString("Select the directory in which to save the converted images");

            this.labelOutputAs.Text = Resource.GetString("Output as");
            this.comboBoxOutputType.Left = this.labelOutputAs.Right + BorderSize;
            this.comboBoxOutputType.DataSource = new List<string>(new[] { Resource.GetString("Text"), Resource.GetString("Image") });

            int maximumOutputSizeSize = 0;

            foreach (string s in this.comboBoxOutputType.Items)
            {
                int width = TextRenderer.MeasureText(s, this.comboBoxOutputType.Font).Width;

                if (width > maximumOutputSizeSize)
                {
                    maximumOutputSizeSize = width;
                }
            }

            this.comboBoxOutputType.Width = maximumOutputSizeSize + 24;

            this.comboBoxOutputFormat.Left = this.comboBoxOutputType.Right + BorderSize;

            this.checkBoxColour.Left = this.comboBoxOutputFormat.Right + BorderSize;
            this.checkBoxColour.Text = Resource.GetString("Colour");

            this.labelOutputSize.Text = Resource.GetString("Output Size") + ":";
            this.textBoxWidth.Left = this.labelOutputSize.Right + BorderSize;
            this.checkBoxLockRatio.Left = this.textBoxWidth.Right + BorderSize;
            this.textBoxHeight.Left = this.checkBoxLockRatio.Right + BorderSize;

            this.clearToolStripMenuItem.Text = Resource.GetString("Clear");
            this.saveLogAsToolStripMenuItem.Text = Resource.GetString("Save Log As") + "...";

            this.saveFileDialogLog.Filter = Resource.GetString("Plain Text") + " (*.txt)|*.txt|" + Resource.GetString("All Files") + " (*.*)|*.*";

            this.addFileToolStripMenuItem.Text = Resource.GetString("Add File") + "...";
            this.addDirectoryToolStripMenuItem.Text = Resource.GetString("Add Directory") + "...";

            this.removeToolStripMenuItem.Text = Resource.GetString("Remove");
            this.removeAllToolStripMenuItem.Text = Resource.GetString("Remove All");
            this.removeFileAfterConversionToolStripMenuItem.Text = Resource.GetString("Remove File After Conversion");

            this.selectAllLogToolStripMenuItem.Text = this.selectAllToolStripMenuItem.Text = Resource.GetString("Select All");
            this.selectNoneToolStripMenuItem.Text = Resource.GetString("Select None");
            this.invertSelectionToolStripMenuItem.Text = Resource.GetString("Invert Selection");

            this.showFullPathToolStripMenuItem.Text = Resource.GetString("Show Full Path");
            this.showExtensionToolStripMenuItem.Text = Resource.GetString("Show Extension");

            this.copyToolStripMenuItem.Text = Resource.GetString("Copy");
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Adds the images in the directory to the list.
        /// </summary>
        /// <param name="directory">The directory.</param>
        private void AddDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory);

            this.fileListbox1.SuspendLayout();

            foreach (string filename in files)
            {
                string extension = Path.GetExtension(filename).ToLower(Variables.Instance.Culture);

                if (this.validFiletypes.Contains(extension) && !this.fileListbox1.Items.Contains(filename))
                {
                    this.fileListbox1.Items.Add(filename);
                }
            }

            this.fileListbox1.ResumeLayout();

            this.UpdateButtonRemoveImage();
        }

        /// <summary>
        /// Show the add directory dialog and handles adding the images.
        /// </summary>
        private void AddDirectoryDialog()
        {
            if (this.folderBrowserDialogInput.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.AddDirectory(this.folderBrowserDialogInput.SelectedPath);
        }

        /// <summary>
        /// Handles the Click event of the addDirectoryToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddDirectoryDialog();
        }

        /// <summary>
        /// Handles the Click event of the addFileToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddImageDialog();
        }

        /// <summary>
        /// Adds the image to the list.
        /// </summary>
        /// <param name="filename">The filename of the image.</param>
        private void AddImage(string filename)
        {
            string extension = Path.GetExtension(filename).ToLower(Variables.Instance.Culture);

            if (!this.validFiletypes.Contains(extension) || this.fileListbox1.Items.Contains(filename))
            {
                return;
            }

            this.fileListbox1.Items.Add(filename);

            this.UpdateButtonRemoveImage();
        }

        /// <summary>
        /// Show the add image dialog and handles adding the images.
        /// </summary>
        private void AddImageDialog()
        {
            if (this.openFileDialogInput.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (string filename in this.openFileDialogInput.FileNames)
            {
                this.AddImage(filename);
            }
        }

        /// <summary>
        /// Adds the separator line to the log
        /// </summary>
        private void AddLogSeparator()
        {
            this.AddLogString("-----------------------------------------------------------");
        }

        /// <summary>
        /// Adds the string to the log.
        /// </summary>
        /// <param name="text">The text to log.</param>
        private void AddLogString(string text)
        {
            this.textBoxLog.AppendText(text + Environment.NewLine);
        }

        /// <summary>
        /// Handles the Click event of the ButtonAddFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonAddFolder_Click(object sender, EventArgs e)
        {
            this.AddDirectoryDialog();
        }

        /// <summary>
        /// Handles the Click event of the buttonAddImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonAddImage_Click(object sender, EventArgs e)
        {
            this.AddImageDialog();
        }

        /// <summary>
        /// Handles the Click event of the buttonFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonFont_Click(object sender, EventArgs e)
        {
            this.fontDialogOutput.Font = this.textProcessingSettings.Font;

            if (this.fontDialogOutput.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.textProcessingSettings.Font = this.fontDialogOutput.Font;
        }

        /// <summary>
        /// Handles the Click event of the buttonOutputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOutputDirectory_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialogOutput.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            this.OutputDirectory = this.folderBrowserDialogOutput.SelectedPath;
        }

        /// <summary>
        /// Handles the Click event of the ButtonRemoveImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonRemoveImage_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedImages();
        }

        /// <summary>
        /// Handles the Click event of the buttonStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (this.fileListbox1.Items.Count == 0 || (this.thread != null && this.thread.IsAlive))
            {
                return;
            }

            this.tabControlMain.SelectedTab = this.tabPageLog;

            this.thread = new Thread(this.DoConversions)
                {
                    Name = string.Format(Variables.Instance.Culture, "BatchConversionThread{0:HHmmss}", DateTime.Now)
                };

            this.thread.Start();
        }

        /// <summary>
        /// Handles the Click event of the buttonStop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonStop_Click(object sender, EventArgs e)
        {
            this.StopConversions();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the checkBoxLockRatio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckBoxLockRatio_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxOtherDimension.Enabled = !this.checkBoxLockRatio.Checked;
        }

        /// <summary>
        /// Handles the Click event of the clearToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxLog.Clear();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxOutputFormat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBoxOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboboxColour();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxOutputType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBoxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboboxColour();

            if (this.comboBoxOutputType.SelectedIndex == this.previousType || this.comboBoxOutputFormat.SelectedIndex == -1)
            {
                return;
            }

            this.previousType = this.comboBoxOutputType.SelectedIndex;

            if (this.OutputIsImage)
            {
                this.previousTextExtension = this.comboBoxOutputFormat.SelectedIndex;
                this.comboBoxOutputFormat.DataSource = Enum.GetValues(typeof(ImageTypes));
                this.comboBoxOutputFormat.SelectedIndex = this.previousImageExtension;
                this.numericUpDownImageScale.Enabled = true;
            }
            else
            {
                this.previousImageExtension = this.comboBoxOutputFormat.SelectedIndex;
                this.comboBoxOutputFormat.DataSource = Enum.GetValues(typeof(TextTypes));
                this.comboBoxOutputFormat.SelectedIndex = this.previousTextExtension;
                this.numericUpDownImageScale.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStripFileList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ContextMenuStripFileList_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.removeToolStripMenuItem.Enabled = this.fileListbox1.Items.Count > 0 && this.fileListbox1.SelectedItems.Count > 0;

            this.invertSelectionToolStripMenuItem.Enabled =
                this.selectAllToolStripMenuItem.Enabled =
                this.removeAllToolStripMenuItem.Enabled =
                                                    this.fileListbox1.Items.Count > 0;

            this.selectNoneToolStripMenuItem.Enabled = this.fileListbox1.SelectedItems.Count > 0;

            this.showFullPathToolStripMenuItem.Checked = this.fileListbox1.DisplayPath;

            this.showExtensionToolStripMenuItem.Checked = this.fileListbox1.DisplayExtension;
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStripLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ContextMenuStripLog_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.copyToolStripMenuItem.Enabled = this.textBoxLog.SelectionLength > 0;
        }

        /// <summary>
        /// Converts the specified filename.
        /// </summary>
        /// <param name="filename">The filename of the input.</param>
        /// <param name="outputFilename">The filename for the output.</param>
        /// <returns>Was the conversion successful?</returns>
        private bool Convert(string filename, string outputFilename)
        {
            string[] convertedText;

            Color[][] colors = null;

            try
            {
                using (Image image = Image.FromFile(filename))
                {
                    this.dimensionsCalculator.ImageSize = image.Size;

                    this.textProcessingSettings.Size = this.dimensionsCalculator.OutputSize;

                    convertedText = AscgenConverter.Convert(image, this.textProcessingSettings);

                    if (this.checkBoxColour.Checked)
                    {
                        // TODO: reduceColors
                        colors = ImageToColors.Convert(
                                                image,
                                                new Size(this.textProcessingSettings.Width, this.textProcessingSettings.Height),
                                                true);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                this.AddLogString(Resource.GetString("Error") + ": " + Resource.GetString("File Not Found") + ".");
                return false;
            }
            catch (OutOfMemoryException)
            {
                this.AddLogString(Resource.GetString("Error") + ": " + Resource.GetString("Invalid or unsupported file") + ".");
                return false;
            }

            if (convertedText == null || convertedText.Length == 0)
            {
                this.AddLogString(Resource.GetString("Error converting the image"));
                return false;
            }

            return this.SaveOutputImage(convertedText, colors, outputFilename);
        }

        /// <summary>
        /// Handles the Click event of the copyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxLog.Copy();
        }

        /// <summary>
        /// Processes and converts the list of images.
        /// </summary>
        private void DoConversions()
        {
            if (!Directory.Exists(this.textBoxOutputDirectory.Text))
            {
                this.tabControlMain.SelectedTab = this.tabPageConversions;

                MessageBox.Show(
                        Resource.GetString("Invalid Output Directory"),
                        Resource.GetString("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.ServiceNotification);

                this.textBoxOutputDirectory.Focus();

                return;
            }

            CheckForIllegalCrossThreadCalls = false;

            this.ControlsEnabled = false;

            this.progressBarConversion.Value = 0;
            this.progressBarConversion.Maximum = this.fileListbox1.Items.Count;

            this.AddLogString(string.Empty);

            this.AddLogSeparator();

            this.AddLogString(string.Format(
                                    Variables.Instance.Culture,
                                    Resource.GetString("Batch conversion started, {0} file(s) to process"),
                                    this.fileListbox1.Items.Count));

            string outputDirectory = this.textBoxOutputDirectory.Text;

            this.AddLogString(string.Format(
                                    Variables.Instance.Culture,
                                    Resource.GetString("Output Size: {0}x{1} characters"),
                                    this.OutputSize.Width == -1 ? "??" : this.OutputSize.Width.ToString(),
                                    this.OutputSize.Height == -1 ? "??" : this.OutputSize.Height.ToString()));

            this.AddLogString(string.Format(
                                    Variables.Instance.Culture,
                                    Resource.GetString("Target Font: {0} {1}pt{2}{3}{4}{5}") + ".",
                                    this.textProcessingSettings.Font.Name,
                                    this.textProcessingSettings.Font.Size.ToString(Variables.Instance.Culture),
                                    this.textProcessingSettings.Font.Bold ? ", " + Resource.GetString("bold") : string.Empty,
                                    this.textProcessingSettings.Font.Italic ? ", " + Resource.GetString("italic") : string.Empty,
                                    this.textProcessingSettings.Font.Underline ? ", " + Resource.GetString("underline") : string.Empty,
                                    this.textProcessingSettings.Font.Strikeout ? ", " + Resource.GetString("strikeout") : string.Empty));

            this.AddLogString(string.Format(
                                    Variables.Instance.Culture,
                                    Resource.GetString("Target Character Size: {0}x{1} pixels{2}"),
                                    this.textProcessingSettings.CharacterSize.Width.ToString(Variables.Instance.Culture),
                                    this.textProcessingSettings.CharacterSize.Height.ToString(Variables.Instance.Culture),
                                    (this.textProcessingSettings.CalculateCharacterSize ? " (" + Resource.GetString("automatically calculated") + ")." : ".")));

            this.AddLogString(Resource.GetString("Saving output as") + " ." + this.comboBoxOutputFormat.Text + (this.OutputIsImage ? " (" + this.numericUpDownImageScale.Value.ToString() + "%)" : string.Empty));

            this.AddLogString(Resource.GetString("Output Directory") + ": " + outputDirectory);

            this.dimensionsCalculator = new DimensionsCalculator(new Size(100, 100), this.textProcessingSettings.CharacterSize, this.OutputSize.Width, this.OutputSize.Height);

            this.AddLogSeparator();

            int count = 0;
            int errors = 0;

            for (int i = this.fileListbox1.Items.Count - 1; i > -1; i--)
            {
                string filename = this.fileListbox1.Items[i].ToString();

                this.AddLogString(filename);

                string suffix = this.GetSuffix();

                string outputFilename = this.textProcessingSettings.Prefix +
                                        Path.GetFileNameWithoutExtension(filename) + suffix +
                                        "." + this.comboBoxOutputFormat.SelectedItem;

                if (!this.Convert(filename, outputDirectory + outputFilename))
                {
                    errors++;
                    continue;
                }

                count++;

                this.progressBarConversion.Value++;

                this.AddLogString(string.Format(
                                        Variables.Instance.Culture,
                                        " -> {0} ({1}x{2}), Ok.",
                                        outputFilename,
                                        this.dimensionsCalculator.OutputSize.Width.ToString(),
                                        this.dimensionsCalculator.OutputSize.Height.ToString()));

                if (this.removeFileAfterConversionToolStripMenuItem.Checked)
                {
                    this.fileListbox1.Items.RemoveAt(i);
                }
            }

            this.AddLogSeparator();

            this.AddLogString(string.Format(Variables.Instance.Culture, Resource.GetString("{0} file(s) converted, {1} error(s)"), count, errors));

            this.ControlsEnabled = true;

            CheckForIllegalCrossThreadCalls = true;
        }

        /// <summary>
        /// Handles the DragDrop event of the fileListbox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void FileListbox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string filename in filenames)
            {
                if (File.Exists(filename))
                {
                    this.AddImage(filename);
                }
                else if (Directory.Exists(filename))
                {
                    this.AddDirectory(filename);
                }
            }
        }

        /// <summary>
        /// Handles the DragEnter event of the fileListbox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void FileListbox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        /// <summary>
        /// Handles the FormClosing event of the FormBatchConversion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void FormBatchConversion_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopConversions();
        }

        /// <summary>
        /// Gets the currently specified suffix.
        /// </summary>
        /// <returns>The current suffix</returns>
        private string GetSuffix()
        {
            string suffix;

            switch (this.textProcessingSettings.SuffixType)
            {
                default:
                    suffix = this.textProcessingSettings.Suffix;
                    break;

                case BatchTextProcessingSettings.SuffixTypes.DateTime:
                    suffix = string.Format(Variables.Instance.Culture, "-" + Resource.GetString("{0:yyyyMMddHHmmss}"), DateTime.Now);
                    break;

                case BatchTextProcessingSettings.SuffixTypes.Random:
                    suffix = "-" + FontFunctions.GetRandomString(6);
                    break;
            }

            return suffix;
        }

        /// <summary>
        /// Handles the Click event of the invertSelectionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InvertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.fileListbox1.Items.Count; i++)
            {
                this.fileListbox1.SetSelected(i, !this.fileListbox1.GetSelected(i));
            }
        }

        /// <summary>
        /// Outputs the header text for the log.
        /// </summary>
        private void OutputLogHeader()
        {
            this.AddLogString(AscgenVersion.ProgramName + " " + AscgenVersion.ToString() +
                " " + Resource.GetString("Batch Conversion") + " " + Resource.GetString("Log"));
        }

        /// <summary>
        /// Handles the Click event of the removeAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListbox1.Items.Clear();

            this.UpdateButtonRemoveImage();
        }

        /// <summary>
        /// Handles the Click event of the removeFileAfterConversionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveFileAfterConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.removeFileAfterConversionToolStripMenuItem.Checked = !this.removeFileAfterConversionToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Removes the selected images from the list.
        /// </summary>
        private void RemoveSelectedImages()
        {
            if (this.fileListbox1.SelectedItems.Count == 0)
            {
                return;
            }

            while (this.fileListbox1.SelectedItems.Count > 0)
            {
                this.fileListbox1.Items.Remove(this.fileListbox1.SelectedItem);
            }

            this.UpdateButtonRemoveImage();
        }

        /// <summary>
        /// Handles the Click event of the removeToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RemoveSelectedImages();
        }

        /// <summary>
        /// Saves as an image.
        /// </summary>
        /// <param name="convertedText">The converted text.</param>
        /// <param name="colors">The colors (null if black and white).</param>
        /// <param name="outputFilename">The output filename.</param>
        /// <returns>Was the image saved?</returns>
        private bool SaveAsImage(string[] convertedText, Color[][] colors, string outputFilename)
        {
            float imageScale = (float)this.numericUpDownImageScale.Value;

            if (colors == null)
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < convertedText.Length; i++)
                {
                    builder.Append(convertedText[i]);

                    if (i < convertedText.Length - 1)
                    {
                        builder.Append(Environment.NewLine);
                    }
                }

                return TextToImage.Save(
                                    builder.ToString(),
                                    outputFilename,
                                    this.textProcessingSettings.Font,
                                    this.textProcessingSettings.IsBlackTextOnWhite ? Color.Black : Color.White,
                                    this.textProcessingSettings.IsBlackTextOnWhite ? Color.White : Color.Black,
                                    imageScale,
                                    true);
            }

            using (Image outputimage = TextToColorImage.Convert(
                convertedText,
                this.textProcessingSettings.Font,
                colors,
                this.textProcessingSettings.IsBlackTextOnWhite ? Color.White : Color.Black,
                imageScale))
            {
                outputimage.Save(
                    outputFilename,
                    ImageFunctions.GetImageFormat(Path.GetExtension(outputFilename).ToLower()));
            }

            return true;
        }

        /// <summary>
        /// Saves as a text file.
        /// </summary>
        /// <param name="convertedText">The converted text.</param>
        /// <param name="colors">The colors (null if black and white).</param>
        /// <param name="outputFilename">The output filename.</param>
        /// <returns>Was the image saved?</returns>
        private bool SaveAsText(string[] convertedText, Color[][] colors, string outputFilename)
        {
            if (!this.OutputIsText)
            {
                return false;
            }

            using (StreamWriter writer = new StreamWriter(outputFilename))
            {
                if (this.OutputIsHtml || this.OutputIsRtf)
                {
                    OutputCreator outputCreator = new OutputCreator(convertedText, this.textProcessingSettings, colors)
                        {
                            Title = Path.GetFileNameWithoutExtension(outputFilename)
                        };

                    writer.Write(this.OutputIsHtml ? outputCreator.CreateHtml() : outputCreator.CreateRtf());
                }
                else
                {
                    if (colors != null)
                    {
                        return false;
                    }

                    foreach (string line in convertedText)
                    {
                        writer.WriteLine(line);
                    }
                }

                writer.Close();
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the saveLogAsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveLogAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialogLog.FileName = string.Format(Variables.Instance.Culture, "Log{0:yyyyMMddHHmmss}", DateTime.Now);

            if (this.saveFileDialogLog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (StreamWriter streamWriter = new StreamWriter(this.saveFileDialogLog.FileName))
            {
                streamWriter.Write(this.textBoxLog.Text);
            }
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="convertedText">The converted text to be saved.</param>
        /// <param name="colors">The array of colors to use (null if black and white).</param>
        /// <param name="outputFilename">The filename for the output.</param>
        /// <returns>Was the file successfully saved?</returns>
        private bool SaveOutputImage(string[] convertedText, Color[][] colors, string outputFilename)
        {
            if (convertedText == null)
            {
                return false;
            }

            return this.OutputIsText ?
                    this.SaveAsText(convertedText, colors, outputFilename) :
                    this.SaveAsImage(convertedText, colors, outputFilename);
        }

        /// <summary>
        /// Handles the Click event of the selectAllLogToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxLog.SelectAll();
        }

        /// <summary>
        /// Handles the Click event of the selectAllToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.fileListbox1.Items.Count; i++)
            {
                this.fileListbox1.SetSelected(i, true);
            }
        }

        /// <summary>
        /// Handles the Click event of the selectNoneToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListbox1.ClearSelected();
        }

        /// <summary>
        /// Sets up the size of the output from the defaults.
        /// </summary>
        private void SetupOutputSize()
        {
            Size newSize = new Size(-1, -1);

            this.checkBoxLockRatio.Checked = false;
            this.textBoxOtherDimension = this.textBoxHeight;

            if (Variables.Instance.DefaultHeight > -1)
            {
                newSize.Height = Variables.Instance.DefaultHeight;
                this.textBoxOtherDimension = this.textBoxWidth;
            }

            if (Variables.Instance.DefaultWidth > -1)
            {
                newSize.Width = Variables.Instance.DefaultWidth;
            }

            if (newSize.Width == -1 && newSize.Height == -1)
            {
                newSize.Width = 150;
            }

            this.OutputSize = newSize;

            this.textBoxWidth.Text = this.OutputSize.Width.ToString();
            this.textBoxHeight.Text = this.OutputSize.Height.ToString();

            if (this.OutputSize.Width == -1 || this.OutputSize.Height == -1)
            {
                this.textBoxOtherDimension.Text = "150";
                this.checkBoxLockRatio.Checked = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the showExtensionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListbox1.DisplayExtension = !this.fileListbox1.DisplayExtension;
        }

        /// <summary>
        /// Handles the Click event of the showFullPathToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowFullPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListbox1.DisplayPath = !this.fileListbox1.DisplayPath;
        }

        /// <summary>
        /// Stops the conversion.
        /// </summary>
        private void StopConversions()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                return;
            }

            this.thread.Abort();

            this.ControlsEnabled = true;

            this.AddLogString(Resource.GetString("Batch conversion cancelled"));
        }

        /// <summary>
        /// Handles the Leave event of the textBoxHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            this.textBoxOtherDimension = this.textBoxWidth;

            int height;

            if (int.TryParse(this.textBoxHeight.Text, out height))
            {
                this.OutputSize = new Size(this.OutputSize.Width, height);
                return;
            }

            if (this.tabControlMain.SelectedTab == this.tabPageConversions)
            {
                MessageBox.Show(Resource.GetString("Invalid Output Size"), Resource.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.tabControlMain.SelectTab(this.tabPageConversions.Name);
            }

            this.textBoxHeight.SelectAll();
            this.textBoxHeight.Focus();
        }

        /// <summary>
        /// Handles the Leave event of the textBoxOutputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxOutputDirectory_Leave(object sender, EventArgs e)
        {
            this.OutputDirectory = this.textBoxOutputDirectory.Text;
        }

        /// <summary>
        /// Handles the Leave event of the textBoxWidth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            this.textBoxOtherDimension = this.textBoxHeight;

            int width;

            if (int.TryParse(this.textBoxWidth.Text, out width))
            {
                this.OutputSize = new Size(width, this.OutputSize.Height);
                return;
            }

            if (this.tabControlMain.SelectedTab == this.tabPageConversions)
            {
                MessageBox.Show(Resource.GetString("Invalid Output Size"), Resource.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.tabControlMain.SelectTab(this.tabPageConversions.Name);
            }

            this.textBoxWidth.SelectAll();
            this.textBoxWidth.Focus();
        }

        /// <summary>
        /// Updates the enabled status of buttonRemoveImage
        /// </summary>
        private void UpdateButtonRemoveImage()
        {
            this.buttonRemoveImage.Enabled = this.fileListbox1.Items.Count > 0;
        }

        /// <summary>
        /// Updates the enabled state of the colour combobox.
        /// </summary>
        private void UpdateComboboxColour()
        {
            this.checkBoxColour.Enabled = this.OutputIsImage || this.OutputIsHtml || this.OutputIsRtf;
        }

        #endregion Private methods
    }
}