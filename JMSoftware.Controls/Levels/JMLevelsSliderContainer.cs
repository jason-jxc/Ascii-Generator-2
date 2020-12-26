//---------------------------------------------------------------------------------------
// <copyright file="JMLevelsSliderContainer.cs" company="Jonathan Mathews Software">
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
    /// Class used to handle drawing of the sliders onto the JMLevelsDisplay
    /// </summary>
    internal class JMLevelsSliderContainer
    {
        #region Constants

        /// <summary>Fixed height of the slider area</summary>
        public const int Height = 12;

        #endregion Constants

        #region Fields

        /// <summary>
        /// Is this slider container enabled?
        /// </summary>
        private bool enabled;

        /// <summary>
        /// The maximum sliders
        /// </summary>
        private JMLevelsSlider maximumSlider;

        /// <summary>
        /// The median level
        /// </summary>
        private float median;

        /// <summary>
        /// The median slider
        /// </summary>
        private JMLevelsSlider medianSlider;

        /// <summary>
        /// The minimum slider
        /// </summary>
        private JMLevelsSlider minimumSlider;

        /// <summary>
        /// The size of the slider container
        /// </summary>
        private Size size;

        /// <summary>Array of pointers to the sliders, in top-down order</summary>
        private JMLevelsSlider[] sliders;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevelsSliderContainer"/> class.
        /// </summary>
        public JMLevelsSliderContainer()
        {
            this.enabled = true;

            this.median = 0.5f;

            this.size = new Size();

            this.minimumSlider = new JMLevelsSlider(Color.Black, 0, this) { MinimumValue = 0, MaximumValue = 253 };
            this.minimumSlider.OnRefresh += this.Refresh;
            this.minimumSlider.OnSliderMoved += this.MinimumSliderMoved;

            this.maximumSlider = new JMLevelsSlider(Color.White, 255, this) { MinimumValue = 2, MaximumValue = 255 };
            this.maximumSlider.OnRefresh += this.Refresh;
            this.maximumSlider.OnSliderMoved += this.MaximumSliderMoved;

            this.medianSlider = new JMLevelsSlider(Color.LightGray, 128, this) { MinimumValue = 1, MaximumValue = 254 };
            this.medianSlider.OnRefresh += this.Refresh;
            this.medianSlider.OnSliderMoved += this.MedianSliderMoved;

            this.sliders = new[] { this.minimumSlider, this.maximumSlider, this.medianSlider };
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Occurs when this class is refreshed</summary>
        public event RefreshEventHandler OnRefresh;

        /// <summary>Event that fires when the minimum, median, or maximum value changes</summary>
        internal event ValueChangedEventHandler OnValueChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether we are dragging a slider.
        /// </summary>
        /// <value><c>true</c> if dragging a slider; otherwise, <c>false</c>.</value>
        public bool DraggingSlider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMLevelsSliderContainer"/> is enabled.
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
                this.enabled = value;

                for (int x = 0; x < this.sliders.Length; x++)
                {
                    this.sliders[x].Enabled = value;
                }
            }
        }

        /// <summary>Gets or sets the Location</summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get
            {
                return this.maximumSlider.Value;
            }

            set
            {
                this.maximumSlider.Value = value;

                this.minimumSlider.MaximumValue = value - 2;
                this.medianSlider.MaximumValue = value - 1;

                this.UpdateMedianSlider();
            }
        }

        /// <summary>
        /// Gets or sets the median value.
        /// </summary>
        /// <value>The median.</value>
        public float Median
        {
            get
            {
                return this.median;
            }

            set
            {
                if (value < 0f)
                {
                    this.median = 0f;
                }
                else if (value > 1f)
                {
                    this.median = 1f;
                }
                else
                {
                    this.median = value;
                }

                this.UpdateMedianSlider();
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get
            {
                return this.minimumSlider.Value;
            }

            set
            {
                this.minimumSlider.Value = value;

                this.maximumSlider.MinimumValue = value + 2;
                this.medianSlider.MinimumValue = value + 1;

                this.UpdateMedianSlider();
            }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size of the slider container.</value>
        public Size Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.size = value.Height == Height ? value : new Size(value.Width, Height);
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Bring the specified slider to the front of the array
        /// </summary>
        /// <param name="slider">slider to move</param>
        public void BringToFront(JMLevelsSlider slider)
        {
            JMLevelsSlider[] levelsSliders = new JMLevelsSlider[this.sliders.Length];

            levelsSliders[0] = slider;

            int pos = 1;

            for (int x = 0; x < this.sliders.Length; x++)
            {
                if (this.sliders[x] != slider)
                {
                    levelsSliders[pos] = this.sliders[x];
                    pos++;
                }
            }

            this.sliders = levelsSliders;
        }

        /// <summary>
        /// Process mouse button being pressed down
        /// </summary>
        /// <param name="e">Mouse data for the event</param>
        public void MouseDown(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            if (this.DraggingSlider)
            {
                return;
            }

            for (int x = 0; x < this.sliders.Length; x++)
            {
                if (this.sliders[x].Rectangle.Contains(e.Location))
                {
                    this.sliders[x].MouseDown(e);
                    break;
                }
            }
        }

        /// <summary>
        /// Process the mouse moving
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public void MouseMove(MouseEventArgs e)
        {
            if (!this.Enabled || !this.DraggingSlider)
            {
                return;
            }

            for (int x = 0; x < this.sliders.Length; x++)
            {
                this.sliders[x].MouseMove(e);
            }
        }

        /// <summary>
        /// Process mouse button being released
        /// </summary>
        public void MouseUp()
        {
            if (!this.Enabled || !this.DraggingSlider)
            {
                return;
            }

            for (int x = 0; x < this.sliders.Length; x++)
            {
                this.sliders[x].MouseUp();
            }
        }

        /// <summary>
        /// Paints the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        public void Paint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(
                            SystemPens.ControlLightLight,
                            new Point(this.Location.X, this.Location.Y + this.Size.Height - 1),
                            new Point(this.Location.X + this.Size.Width - 1, this.Location.Y + this.Size.Height - 1));

            e.Graphics.DrawLine(
                            SystemPens.ControlLightLight,
                            new Point(this.Location.X + this.Size.Width - 1, this.Location.Y),
                            new Point(this.Location.X + this.Size.Width - 1, this.Location.Y + this.Size.Height - 1));

            e.Graphics.DrawLine(
                            this.Enabled ? SystemPens.ControlDarkDark : SystemPens.GrayText,
                            this.Location,
                            new Point(this.Location.X, this.Location.Y + this.Size.Height - 1));

            e.Graphics.DrawLine(
                            this.Enabled ? SystemPens.ControlDarkDark : SystemPens.GrayText,
                            this.Location,
                            new Point(this.Location.X + this.Size.Width - 1, this.Location.Y));

            for (int x = this.sliders.Length - 1; x > -1; x--)
            {
                this.sliders[x].Paint(e);
            }
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            if (this.OnRefresh != null)
            {
                this.OnRefresh();
            }
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the maximum slider moved event.
        /// </summary>
        /// <param name="previousValue">The previous value.</param>
        private void MaximumSliderMoved(int previousValue)
        {
            if (previousValue == this.maximumSlider.Value)
            {
                return;
            }

            this.minimumSlider.MaximumValue = this.maximumSlider.Value - 2;
            this.medianSlider.MaximumValue = this.maximumSlider.Value - 1;

            this.UpdateMedianSlider();

            if (this.OnValueChanged != null)
            {
                this.OnValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the medians slider moved event.
        /// </summary>
        /// <param name="previousValue">The previous value.</param>
        private void MedianSliderMoved(int previousValue)
        {
            if (previousValue == this.medianSlider.Value)
            {
                return;
            }

            this.Median = (float)(this.medianSlider.Value - this.Minimum) / (float)(this.Maximum - this.Minimum);

            if (this.OnValueChanged != null)
            {
                this.OnValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the minimum slider moved event.
        /// </summary>
        /// <param name="previousValue">The previous value.</param>
        private void MinimumSliderMoved(int previousValue)
        {
            if (previousValue == this.minimumSlider.Value)
            {
                return;
            }

            this.maximumSlider.MinimumValue = this.minimumSlider.Value + 2;
            this.medianSlider.MinimumValue = this.minimumSlider.Value + 1;

            this.UpdateMedianSlider();

            if (this.OnValueChanged != null)
            {
                this.OnValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Update the position of the median slider
        /// </summary>
        private void UpdateMedianSlider()
        {
            this.medianSlider.Value = (int)(((float)(this.maximumSlider.Value - this.minimumSlider.Value) * this.Median) + 0.5f) + this.minimumSlider.Value;
        }

        #endregion Private methods
    }
}