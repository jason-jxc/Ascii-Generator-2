//---------------------------------------------------------------------------------------
// <copyright file="TextViewerRichTextBox.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.Controls
{
    using System.Drawing;
    using System.Windows.Forms;
    using JMSoftware.Interfaces;

    /// <summary>
    /// Class to give an ITextViewer interface to a RichTextBox
    /// </summary>
    public class TextViewerRichTextBox : RichTextBox, ITextViewer
    {
        #region Constructors

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Background colour
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return this.BackColor;
            }

            set
            {
                this.BackColor = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the text is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return Text.Length == 0;
            }
        }

        /// <summary>
        /// Gets or sets the lines of text in a text box control.
        /// </summary>
        public new string[] Lines
        {
            get
            {
                return base.Lines;
            }

            set
            {
                base.Lines = value;

                this.Update();
            }
        }

        /// <summary>
        /// Gets or sets the colour of the text
        /// </summary>
        public Color TextColor
        {
            get
            {
                return this.ForeColor;
            }

            set
            {
                this.ForeColor = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Selects all text in the text box.
        /// </summary>
        public new void SelectAll()
        {
            base.SelectAll();

            // Give the control focus to avoid "not looking selected" problem
            this.Select();
        }

        /// <summary>
        /// Stop selecting the text
        /// </summary>
        public void SelectNone()
        {
            this.Select(this.SelectionStart + this.SelectionLength, 0);
        }

        #endregion Public methods
    }
}