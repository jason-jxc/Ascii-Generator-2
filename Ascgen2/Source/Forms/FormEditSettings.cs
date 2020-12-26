//---------------------------------------------------------------------------------------
// <copyright file="FormEditSettings.cs" company="Jonathan Mathews Software">
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
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Form to edit the settings
    /// </summary>
    public partial class FormEditSettings : Form
    {
        #region Fields

        /// <summary>
        /// The default font
        /// </summary>
        private Font defaultFont;

        /// <summary>
        /// The textboxes that contain directory strings
        /// </summary>
        private TextBox[] directories;

        /// <summary>
        /// The size of the output
        /// </summary>
        private Size outputSize;

        /// <summary>
        /// The dimension which has not been set
        /// </summary>
        private TextBox textBoxOtherDimension;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormEditSettings"/> class.
        /// </summary>
        public FormEditSettings()
        {
            this.InitializeComponent();

            this.UpdateUI();

            this.directories = new[] { this.textBoxInputDirectory, this.textBoxOutputDirectory };

            this.textBoxOtherDimension = this.textBoxHeight;

            this.ResetSettings();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether to check for new versions.
        /// </summary>
        /// <value>
        /// <c>true</c> if checking for new versions; otherwise, <c>false</c>.
        /// </value>
        public bool CheckForNewVersions
        {
            get
            {
                return this.checkBoxConfirmVersionCheck.Checked;
            }

            set
            {
                this.checkBoxConfirmVersionCheck.Checked = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to confirm on close.
        /// </summary>
        /// <value>
        /// <c>true</c> if confirm on close; otherwise, <c>false</c>.
        /// </value>
        public bool ConfirmOnClose
        {
            get
            {
                return this.checkBoxConfirmClose.Checked;
            }

            set
            {
                this.checkBoxConfirmClose.Checked = value;
            }
        }

        /// <summary>
        /// Gets or sets the default font.
        /// </summary>
        /// <value>The default font.</value>
        public new Font DefaultFont
        {
            get
            {
                return this.defaultFont;
            }

            set
            {
                this.defaultFont = value;

                this.UpdateFont();
            }
        }

        /// <summary>
        /// Gets or sets the input directory.
        /// </summary>
        /// <value>The input directory.</value>
        public string InputDirectory
        {
            get
            {
                return this.textBoxInputDirectory.Text;
            }

            set
            {
                if (Directory.Exists(value))
                {
                    this.textBoxInputDirectory.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the output directory.
        /// </summary>
        /// <value>The output directory.</value>
        public string OutputDirectory
        {
            get
            {
                return this.textBoxOutputDirectory.Text;
            }

            set
            {
                if (Directory.Exists(value))
                {
                    this.textBoxOutputDirectory.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the output.
        /// </summary>
        /// <value>The size of the output.</value>
        public Size OutputSize
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

        #region Protected methods

        /// <summary>
        /// Raise the System.Windows.Forms.Form.Closing event
        /// </summary>
        /// <param name="e">CancelEventArgs containing the event data</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // Catch ok button press
            if (this.DialogResult != DialogResult.OK)
            {
                return;
            }

            this.errorProvider1.Clear();

            foreach (TextBox textbox in this.directories)
            {
                // the directory must exist if not empty
                if (textbox.Text.Length > 0 && !System.IO.Directory.Exists(textbox.Text))
                {
                    this.errorProvider1.SetError(textbox, Resource.GetString("Invalid Directory"));

                    e.Cancel = true;
                }
            }

            int width;

            if (this.textBoxWidth.Text.Length > 0)
            {
                if (!int.TryParse(this.textBoxWidth.Text, out width) || width < 1)
                {
                    this.errorProvider1.SetError(this.textBoxHeight, Resource.GetString("Invalid Output Size"));

                    e.Cancel = true;
                }
            }
            else
            {
                width = -1;
            }

            int height;

            if (this.textBoxHeight.Text.Length > 0)
            {
                if (!int.TryParse(this.textBoxHeight.Text, out height) || height < 1)
                {
                    this.errorProvider1.SetError(this.textBoxHeight, Resource.GetString("Invalid Output Size"));

                    e.Cancel = true;
                }
            }
            else
            {
                height = -1;
            }

            if (width == -1 && height == -1)
            {
                this.errorProvider1.SetError(this.textBoxHeight, Resource.GetString("Invalid Output Size"));

                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                this.OutputSize = new Size(width, height);
            }
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the Click event of the buttonDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonDefault_Click(object sender, EventArgs e)
        {
            this.ResetSettings();
        }

        /// <summary>
        /// Handles the Click event of the buttonFont control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonFont_Click(object sender, EventArgs e)
        {
            this.fontDialog1.Font = this.DefaultFont;

            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DefaultFont = this.fontDialog1.Font;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonInputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonInputDirectory_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = Resource.GetString("Input Directory");
            this.folderBrowserDialog1.SelectedPath = this.textBoxInputDirectory.Text;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInputDirectory.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonOutputDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOutputDirectory_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = Resource.GetString("Output Directory");
            this.folderBrowserDialog1.SelectedPath = this.textBoxOutputDirectory.Text;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutputDirectory.Text = this.folderBrowserDialog1.SelectedPath;
            }
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
        /// Handles the Load event of the FormEditSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FormEditSettings_Load(object sender, EventArgs e)
        {
            this.textBoxWidth.Text = this.outputSize.Width.ToString();
            this.textBoxHeight.Text = this.outputSize.Height.ToString();

            this.checkBoxLockRatio.Checked = false;

            if (this.outputSize.Height > -1 && this.outputSize.Width > -1)
            {
                this.textBoxOtherDimension = this.textBoxHeight;
                return;
            }

            if (this.outputSize.Height == -1)
            {
                this.textBoxOtherDimension = this.textBoxHeight;
            }

            if (this.outputSize.Width == -1)
            {
                this.textBoxOtherDimension = this.textBoxWidth;
            }

            this.textBoxOtherDimension.Text = string.Empty;
            this.checkBoxLockRatio.Checked = true;
        }

        /// <summary>
        /// Resets the settings to the defaults.
        /// </summary>
        private void ResetSettings()
        {
            this.InputDirectory = string.Empty;

            this.OutputDirectory = string.Empty;

            this.OutputSize = new Size(150, -1);

            this.textBoxWidth.Text = "150";

            this.textBoxHeight.Text = string.Empty;

            this.textBoxHeight.Enabled = false;

            this.checkBoxLockRatio.Checked = true;

            this.textBoxOtherDimension = this.textBoxHeight;

            this.DefaultFont = new Font("Lucida Console", 9f);

            this.ConfirmOnClose = true;

            this.CheckForNewVersions = true;
        }

        /// <summary>
        /// Handles the Leave event of the textBoxHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            this.textBoxOtherDimension = this.textBoxWidth;
        }

        /// <summary>
        /// Handles the Leave event of the textBoxWidth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            this.textBoxOtherDimension = this.textBoxHeight;
        }

        /// <summary>
        /// Updates the font.
        /// </summary>
        private void UpdateFont()
        {
            this.textBoxFont.Text = this.defaultFont.Name + string.Format(Variables.Instance.Culture, " {0}pt", this.defaultFont.Size) +
                (this.defaultFont.Bold ? ", bold" : string.Empty) + (this.defaultFont.Italic ? ", italic" : string.Empty) +
                (this.defaultFont.Underline ? ", underline" : string.Empty) +
                (this.defaultFont.Strikeout ? ", strikeout" : string.Empty) + ".";
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.Text = Resource.GetString("Edit Settings");

            this.buttonOk.Text = Resource.GetString("&Ok");
            this.buttonDefault.Text = Resource.GetString("&Default");
            this.buttonCancel.Text = Resource.GetString("&Cancel");

            this.checkBoxConfirmClose.Text = Resource.GetString("Confirm close if unsaved");
            this.checkBoxConfirmVersionCheck.Text = Resource.GetString("New version check");

            this.labelInputDirectory.Text = Resource.GetString("Input Directory") + ":";
            this.labelOutputDirectory.Text = Resource.GetString("Output Directory") + ":";
            this.labelOutputSize.Text = Resource.GetString("Output Size") + ":";

            this.buttonFont.Text = Resource.GetString("Font") + "...";
        }

        #endregion Private methods
    }
}