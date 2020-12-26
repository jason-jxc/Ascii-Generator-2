//---------------------------------------------------------------------------------------
// <copyright file="CharacterSizeDialog.cs" company="Jonathan Mathews Software">
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

    /// <summary>
    /// Dialog used to specify the size of one character in the font
    /// </summary>
    public partial class CharacterSizeDialog
    {
        #region Fields

        /// <summary>
        /// The size of the character
        /// </summary>
        private Size characterSize = new Size(0, 0);

        /// <summary>
        /// The default character size
        /// </summary>
        private Size defaultCharacterSize = new Size(0, 0);

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterSizeDialog"/> class.
        /// </summary>
        public CharacterSizeDialog()
        {
            this.InitializeComponent();

            this.UpdateUI();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether we should automatically calculate the size?
        /// </summary>
        /// <value><c>true</c> if automatically calculating size; otherwise, <c>false</c>.</value>
        public bool AutoCalculateSize
        {
            get
            {
                return this.chkAuto.Checked;
            }

            set
            {
                this.chkAuto.Checked = value;

                this.UpdateAutoCheckbox();
            }
        }

        /// <summary>
        /// Gets or sets the size of one character in the font
        /// </summary>
        /// <value>The size of the character.</value>
        public Size CharacterSize
        {
            get
            {
                return this.characterSize;
            }

            set
            {
                this.characterSize = value;

                this.tbxWidth.Text = this.characterSize.Width.ToString(Variables.Instance.Culture);

                this.tbxHeight.Text = this.characterSize.Height.ToString(Variables.Instance.Culture);
            }
        }

        /// <summary>
        /// Gets or sets the default size of one character (for when the Auto button is pressed)
        /// </summary>
        /// <value>The default size of the character.</value>
        public Size DefaultCharacterSize
        {
            get
            {
                return this.defaultCharacterSize;
            }

            set
            {
                this.defaultCharacterSize = value;

                if (this.chkAuto.Checked)
                {
                    this.tbxWidth.Text = this.defaultCharacterSize.Width.ToString(Variables.Instance.Culture);
                    this.tbxHeight.Text = this.defaultCharacterSize.Height.ToString(Variables.Instance.Culture);
                }
            }
        }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Handles the CheckedChanged event of the chkAuto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ChkAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            this.UpdateAutoCheckbox();
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxHeight control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxHeight_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.characterSize.Height = Convert.ToInt32(this.tbxHeight.Text, Variables.Instance.Culture);
            }
            catch (FormatException)
            {
                this.characterSize.Height = -1;
            }

            this.UpdateOkButton();
        }

        /// <summary>
        /// Handles the TextChanged event of the tbxWidth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxWidth_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.characterSize.Width = Convert.ToInt32(this.tbxWidth.Text, Variables.Instance.Culture);
            }
            catch (FormatException)
            {
                this.characterSize.Width = -1;
            }

            this.UpdateOkButton();
        }

        /// <summary>
        /// Updates the auto checkbox.
        /// </summary>
        private void UpdateAutoCheckbox()
        {
            this.tbxWidth.Enabled = this.tbxHeight.Enabled = !this.chkAuto.Checked;

            if (this.chkAuto.Checked)
            {
                this.tbxWidth.Text = this.DefaultCharacterSize.Width.ToString(Variables.Instance.Culture);
                this.tbxHeight.Text = this.DefaultCharacterSize.Height.ToString(Variables.Instance.Culture);
            }
        }

        /// <summary>
        /// Updates the ok button.
        /// </summary>
        private void UpdateOkButton()
        {
            this.btnOk.Enabled = this.characterSize.Width > 0 && this.characterSize.Height > 0;
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.Text = Resource.GetString("Character Size");
            this.btnOk.Text = Resource.GetString("&Ok");
            this.btnCancel.Text = Resource.GetString("&Cancel");
            this.chkAuto.Text = Resource.GetString("Auto");
        }

        #endregion Private methods
    }
}