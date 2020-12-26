//---------------------------------------------------------------------------------------
// <copyright file="ITextViewer.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.Interfaces
{
    using System.Drawing;

    /// <summary>
    /// Interface to an object that will display text
    /// </summary>
    public interface ITextViewer
    {
        #region Properties

        /// <summary>Gets or sets the Background colour</summary>
        Color BackgroundColor { get; set; }

        /// <summary>Gets or sets the text</summary>
        Font Font { get; set; }

        /// <summary>Gets a value indicating whether the text is empty.</summary>
        bool IsEmpty { get; }

        /// <summary>Gets or sets the lines of text</summary>
        string[] Lines { get; set; }

        /// <summary>Gets the number of characters selected</summary>
        int SelectionLength { get; }

        /// <summary>Gets or sets the text</summary>
        string Text { get; set; }

        /// <summary>Gets or sets the colour of the text</summary>
        Color TextColor { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>Remove all of the text</summary>
        void Clear();

        /// <summary>Copy the currently selected text to the clipboard</summary>
        void Copy();

        /// <summary>Select all of the text</summary>
        void SelectAll();

        /// <summary>Stop selecting the text</summary>
        void SelectNone();

        #endregion Methods
    }
}