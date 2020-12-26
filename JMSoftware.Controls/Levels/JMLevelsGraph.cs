//---------------------------------------------------------------------------------------
// <copyright file="JMLevelsGraph.cs" company="Jonathan Mathews Software">
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
    /// Class used to handle drawing of the graph onto the JMLevelsDisplay
    /// </summary>
    internal class JMLevelsGraph
    {
        #region Fields

        /// <summary>
        /// The array of values
        /// </summary>
        private int[] array;

        /// <summary>
        /// Object to draw the graph area
        /// </summary>
        private JMLevelsGraphArea graphArea;

        /// <summary>
        /// The graphs location
        /// </summary>
        private Point location;

        /// <summary>
        /// The size of the graph
        /// </summary>
        private Size size;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevelsGraph"/> class.
        /// </summary>
        public JMLevelsGraph()
        {
            this.graphArea = new JMLevelsGraphArea();
            this.Enabled = true;
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the array of values</summary>
        public int[] Array
        {
            get
            {
                return this.array;
            }

            set
            {
                if (value == null || value.Length != 256)
                {
                    return;
                }

                this.array = value;

                this.UpdateGraphAreaArray();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMLevelsGraph"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        /// <summary>Gets or sets the Location</summary>
        public Point Location
        {
            get
            {
                return this.location;
            }

            set
            {
                if (this.location == value)
                {
                    return;
                }

                this.location = value;
                this.graphArea.Location = new Point(this.location.X + 1, this.location.Y);
            }
        }

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

                this.graphArea.Size = new Size(this.size.Width - 1, this.size.Height - 2);

                this.UpdateGraphAreaArray();
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
            e.Graphics.DrawLine(
                            this.Enabled ? SystemPens.ControlText : SystemPens.GrayText,
                            this.Location,
                            new Point(this.Location.X, this.Location.Y + this.Size.Height - 1));

            e.Graphics.DrawLine(
                            this.Enabled ? SystemPens.ControlText : SystemPens.GrayText,
                            new Point(this.Location.X, this.Location.Y + this.Size.Height - 1),
                            new Point(this.Location.X + this.Size.Width - 1, this.Location.Y + this.Size.Height - 1));

            this.graphArea.Paint(e);
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Gets the maximum value from the array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>The maximum.</returns>
        private static int GetMaximumValue(int[] array)
        {
            int maximum = 0;

            for (int x = 0; x < array.Length; x++)
            {
                if (array[x] > maximum)
                {
                    maximum = array[x];
                }
            }

            return maximum;
        }

        /// <summary>
        /// Updates the graph area array.
        /// </summary>
        private void UpdateGraphAreaArray()
        {
            if (this.Array == null)
            {
                return;
            }

            int graphSize = this.graphArea.Size.Width;

            int[] newArray;

            if (this.Array.Length > graphSize)
            {
                newArray = new int[graphSize];

                float ratio = 255f / (float)graphSize;

                for (int x = 0; x < graphSize; x++)
                {
                    float startPoint = x * ratio;
                    float endPoint = startPoint + ratio;

                    float difference = 1f - (startPoint - (float)((int)startPoint));
                    float total = (float)this.Array[(int)startPoint] * difference;

                    for (int i = (int)startPoint + 1; i < (int)endPoint; i++)
                    {
                        total += (float)this.Array[i];
                    }

                    difference = endPoint - (float)((int)endPoint);
                    total += (float)this.Array[(int)endPoint] * difference;

                    newArray[x] = (int)((total / ratio) + 0.5f);
                }
            }
            else if (this.Array.Length < graphSize)
            {
                newArray = new int[graphSize];

                float ratio = 255f / (float)graphSize;

                for (int x = 0; x < graphSize; x++)
                {
                    float positionInArray = ratio * x;
                    int previousPosition = (int)positionInArray;

                    if ((float)previousPosition == positionInArray)
                    {
                        newArray[x] = this.Array[previousPosition];
                    }
                    else
                    {
                        float previousValue = (float)this.Array[previousPosition];
                        float difference = (float)this.Array[previousPosition + 1] - previousValue;

                        newArray[x] = (int)previousValue +
                            (int)((difference * (positionInArray - previousPosition)) + 0.5f);
                    }
                }
            }
            else
            {
                newArray = this.Array;
            }

            int maximum = GetMaximumValue(newArray);

            if (maximum > 0)
            {
                float heightRatio = (float)this.graphArea.Size.Height / (float)maximum;

                for (int x = 0; x < graphSize; x++)
                {
                    newArray[x] = (int)(((float)newArray[x] * heightRatio) + 0.5f);
                }
            }

            this.graphArea.Array = newArray;
        }

        #endregion Private methods
    }
}