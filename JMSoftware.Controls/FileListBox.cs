//---------------------------------------------------------------------------------------
// <copyright file="FileListBox.cs" company="Jonathan Mathews Software">
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
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Listbox specialized for displaying filenames
    /// </summary>
    public class FileListBox : ListBox
    {
        #region Fields

        /// <summary>
        /// Are we displaying the extension?
        /// </summary>
        private bool displayExtension = true;

        /// <summary>
        /// Are we displaying the files path?
        /// </summary>
        private bool displayPath = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileListBox"/> class.
        /// </summary>
        public FileListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether we are displaying the extension.
        /// </summary>
        /// <value><c>true</c> if display extension; otherwise, <c>false</c>.</value>
        [DefaultValue(true), Category("Behavior"), Description("Display the extensions for any files in the list?")]
        public bool DisplayExtension
        {
            get
            {
                return this.displayExtension;
            }

            set
            {
                if (this.displayExtension == value)
                {
                    return;
                }

                this.displayExtension = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display the full path names.
        /// </summary>
        /// <value><c>true</c> if [display path]; otherwise, <c>false</c>.</value>
        [DefaultValue(true), Category("Behavior"), Description("Display the full path to any files in the list?")]
        public bool DisplayPath
        {
            get
            {
                return this.displayPath;
            }

            set
            {
                if (this.displayPath == value)
                {
                    return;
                }

                this.displayPath = value;

                this.Refresh();
            }
        }

        #endregion Properties

        #region Protected methods

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> that contains the event data.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (this.Items.Count < 1 || e.Index < 0)
            {
                return;
            }

            string filename = this.Items[e.Index].ToString();

            StringBuilder builder = new StringBuilder();

            if (this.DisplayPath)
            {
                string directory = Path.GetDirectoryName(filename);

                if (!string.IsNullOrEmpty(directory))
                {
                    builder.Append(directory + Path.DirectorySeparatorChar.ToString());
                }
            }

            builder.Append(Path.GetFileNameWithoutExtension(filename));

            if (this.DisplayExtension)
            {
                builder.Append(Path.GetExtension(filename));
            }

            TextRenderer.DrawText(
                                e.Graphics,
                                builder.ToString(),
                                e.Font,
                                e.Bounds,
                                File.Exists(filename) ? Color.Black : Color.Red,
                                TextFormatFlags.Left);

            e.DrawFocusRectangle();
        }

        #endregion Protected methods
    }
}