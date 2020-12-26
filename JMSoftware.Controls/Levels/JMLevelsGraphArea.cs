//---------------------------------------------------------------------------------------
// <copyright file="JMLevelsGraphArea.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.Controls.Levels
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Class used to draw the graph lines onto the control
    /// </summary>
    internal class JMLevelsGraphArea
    {
        #region Fields

        /// <summary>
        /// The array of values
        /// </summary>
        private int[] array;

        /// <summary>
        /// The size of the graph area
        /// </summary>
        private Size size;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevelsGraphArea"/> class.
        /// </summary>
        public JMLevelsGraphArea()
        {
            this.size = new Size();
            this.GraphColor = Color.Blue;
        }

        /// <summary>
        /// Gets or sets the array.
        /// </summary>
        /// <value>The array.</value>
        public int[] Array
        {
            get
            {
                return this.array;
            }

            set
            {
                if (value == null || value.Length != Size.Width)
                {
                    return;
                }

                this.array = value;
            }
        }

        /// <summary>Gets or sets the color used to draw the graph</summary>
        public Color GraphColor { get; set; }

        /// <summary>Gets or sets Location</summary>
        public Point Location { get; set; }

        /// <summary>Gets or sets the Size</summary>
        public Size Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (this.size == value)
                {
                    return;
                }

                this.size = value;

                if (this.size.Width < 0)
                {
                    this.size.Width = 0;
                }

                if (this.size.Height < 0)
                {
                    this.size.Height = 0;
                }
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Paints the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        public void Paint(PaintEventArgs e)
        {
            if (this.array == null)
            {
                return;
            }

            using (Pen pen = new Pen(this.GraphColor))
            {
                for (int x = 0; x < this.Array.Length; x++)
                {
                    e.Graphics.DrawLine(
                                    pen,
                                    x + this.Location.X,
                                    this.Location.Y + this.Size.Height,
                                    x + this.Location.X,
                                    this.Location.Y + this.Size.Height - this.Array[x]);
                }
            }
        }

        #endregion Public methods
    }
}