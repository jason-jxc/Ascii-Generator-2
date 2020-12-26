//---------------------------------------------------------------------------------------
// <copyright file="ValidRampCharsDialog.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using JMSoftware.TextHelper;

    /// <summary>
    /// Form to display and change the ramp
    /// </summary>
    public partial class ValidRampCharsDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidRampCharsDialog"/> class.
        /// </summary>
        public ValidRampCharsDialog()
        {
            this.InitializeComponent();

            this.UpdateUI();

            this.cmbCharacters.Items.Clear();
            this.cmbCharacters.Items.AddRange(Variables.Instance.DefaultValidCharacters);
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the valid characters for the textbox</summary>
        public string Characters
        {
            get
            {
                return this.cmbCharacters.Text;
            }

            set
            {
                this.cmbCharacters.Text = value;
            }
        }

        /// <summary>Gets or sets the current font for the textbox</summary>
        public new Font Font
        {
            get
            {
                return this.cmbCharacters.Font;
            }

            set
            {
                this.cmbCharacters.Font = value;

                this.MinimumSize = new Size(this.MinimumSize.Width, this.cmbCharacters.Top + this.cmbCharacters.ItemHeight + 96);
            }
        }

        #endregion Properties

        #region Protected methods

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the Click event of the btnDefault control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDefault_Click(object sender, System.EventArgs e)
        {
            this.cmbCharacters.Text = Variables.Instance.CurrentSelectedValidCharacters > -1 ?
                Variables.Instance.DefaultValidCharacters[Variables.Instance.CurrentSelectedValidCharacters] :
                Variables.Instance.CurrentCharacters;

            this.cmbCharacters.Focus();
            this.cmbCharacters.SelectAll();
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
                Size size = FontFunctions.MeasureText(characters + "  ", this.cmbCharacters.Font);

                if (size.Width > width)
                {
                    width = size.Width;
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
            this.btnOk.Enabled = this.cmbCharacters.Text.Length > 0;
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.lblValid1.Text = Resource.GetString("Enter the characters to be used for generating the ramp") + ":";
            this.lblValid2.Text = "(" + Resource.GetString("Some characters may not make it in to the ramp") + ")";

            this.btnOk.Text = Resource.GetString("&Ok");
            this.btnCancel.Text = Resource.GetString("&Cancel");
            this.btnDefault.Text = Resource.GetString("&Default");
        }

        #endregion Private methods
    }
}