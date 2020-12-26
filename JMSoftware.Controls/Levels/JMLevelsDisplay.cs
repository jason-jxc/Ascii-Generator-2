//---------------------------------------------------------------------------------------
// <copyright file="JMLevelsDisplay.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Class used to draw the main body of the JMLevels control
    /// </summary>
    internal class JMLevelsDisplay
    {
        #region Fields

        /// <summary>
        /// Is this enabled?
        /// </summary>
        private bool enabled;

        /// <summary>
        /// Location of the levels display
        /// </summary>
        private Point location;

        /// <summary>
        /// Size of the levels display
        /// </summary>
        private Size size;

        /// <summary>
        /// Object used to draw the graph
        /// </summary>
        private JMLevelsGraph levelsGraph;

        /// <summary>
        /// Object used to draw the sliders
        /// </summary>
        private JMLevelsSliderContainer sliders;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevelsDisplay"/> class.
        /// </summary>
        public JMLevelsDisplay()
        {
            this.ControlGap = 2;
            this.size = new Size();
            this.enabled = true;

            this.sliders = new JMLevelsSliderContainer();
            this.levelsGraph = new JMLevelsGraph();

            this.sliders.OnRefresh += this.Refresh;
            this.sliders.OnValueChanged += this.SlidersValueChanged;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Occurs when this class is refreshed</summary>
        public event RefreshEventHandler OnRefresh;

        /// <summary>Event that fires when the minimum, median, or maximum value changes</summary>
        internal event ValueChangedEventHandler OnValueChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>Gets or sets the array of values</summary>
        public int[] Array
        {
            get { return this.levelsGraph.Array; }
            set { this.levelsGraph.Array = value; }
        }

        /// <summary>Gets or sets the gap between the graph and the sliders</summary>
        public int ControlGap { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMLevelsDisplay"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.levelsGraph.Enabled = this.sliders.Enabled = this.enabled = value;
            }
        }

        /// <summary>
        /// Sets the location.
        /// </summary>
        /// <value>The location.</value>
        public Point Location
        {
            set
            {
                this.levelsGraph.Location = this.location = value;

                this.sliders.Location = new Point(
                                            this.location.X,
                                            this.location.Y + Size.Height - JMLevelsSliderContainer.Height);
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get { return this.sliders.Maximum; }
            set { this.sliders.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the median.
        /// </summary>
        /// <value>The median.</value>
        public float Median
        {
            get { return this.sliders.Median; }
            set { this.sliders.Median = value; }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get { return this.sliders.Minimum; }
            set { this.sliders.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size of the display.</value>
        public Size Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value;

                this.levelsGraph.Size = new Size(
                                            this.size.Width,
                                            this.size.Height - this.ControlGap - JMLevelsSliderContainer.Height);

                this.sliders.Size = new Size(this.size.Width, JMLevelsSliderContainer.Height);
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Process mouse button being pressed down
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public void MouseDown(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.sliders.MouseDown(e);
        }

        /// <summary>
        /// Process the mouse moving
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public void MouseMove(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.sliders.MouseMove(e);
        }

        /// <summary>
        /// Process mouse button being released
        /// </summary>
        public void MouseUp()
        {
            if (!this.Enabled)
            {
                return;
            }

            this.sliders.MouseUp();
        }

        /// <summary>
        /// Paints the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        public void Paint(PaintEventArgs e)
        {
            this.levelsGraph.Paint(e);
            this.sliders.Paint(e);
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            if (this.OnRefresh == null)
            {
                return;
            }

            this.OnRefresh();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Occurs when the sliders value has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SlidersValueChanged(object sender, EventArgs e)
        {
            if (this.OnValueChanged == null)
            {
                return;
            }

            this.OnValueChanged(sender, e);
        }

        #endregion Private methods
    }
}