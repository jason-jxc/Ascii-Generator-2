//---------------------------------------------------------------------------------------
// <copyright file="FormSaveAs.cs" company="Jonathan Mathews Software">
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
    /// <summary>
    /// Form to show the different save as types
    /// </summary>
    public partial class FormSaveAs : System.Windows.Forms.Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormSaveAs"/> class.
        /// </summary>
        public FormSaveAs()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the output is in colour rather then black and white
        /// </summary>
        public bool IsColour
        {
            get
            {
                return this.rbColour.Checked;
            }

            set
            {
                this.rbColour.Checked = value;
                this.rbBlackWhite.Checked = !value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output is going to be fixed width
        /// </summary>
        public bool IsFixedWidth
        {
            get
            {
                return this.rbColour.Enabled;
            }

            set
            {
                this.rbColour.Enabled = value;

                if (!value)
                {
                    this.rbBlackWhite.Checked = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the output will be text rather an image
        /// </summary>
        public bool IsText
        {
            get 
            {
                return this.rbText.Checked;
            }

            set
            {
                this.rbText.Checked = value;
                this.rbImage.Checked = !value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        public void UpdateUI()
        {
            this.Text = Resource.GetString("Save As");

            this.rbBlackWhite.Text = Resource.GetString("Black and White");
            this.rbColour.Text = Resource.GetString("Colour");

            this.rbText.Text = Resource.GetString("Text");
            this.rbImage.Text = Resource.GetString("Image");

            this.btnOk.Text = Resource.GetString("&Ok");
            this.btnCancel.Text = Resource.GetString("&Cancel");
        }

        #endregion Public methods
    }
}