//---------------------------------------------------------------------------------------
// <copyright file="FormSelectLanguage.cs" company="Jonathan Mathews Software">
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
    using System.Collections.Specialized;
    using System.Windows.Forms;

    /// <summary>
    /// Dialog to select one of the available languages from a list
    /// </summary>
    public partial class FormSelectLanguage : Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormSelectLanguage"/> class.
        /// </summary>
        /// <param name="files">The translation filenames.</param>
        public FormSelectLanguage(StringCollection files)
        {
            this.InitializeComponent();

            foreach (string file in files)
            {
                this.listBoxTranslationFiles.Items.Add(file);
            }

            this.listBoxTranslationFiles.SelectedIndex = 0;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public string SelectedItem
        {
            get
            {
                return (string)this.listBoxTranslationFiles.SelectedItem;
            }

            set
            {
                if ((string)this.listBoxTranslationFiles.SelectedItem == value)
                {
                    return;
                }

                this.listBoxTranslationFiles.SelectedItem = value;
            }
        }

        #endregion Properties
    }
}