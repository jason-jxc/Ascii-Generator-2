//---------------------------------------------------------------------------------------
// <copyright file="JMLevelsSlider.cs" company="Jonathan Mathews Software">
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
    /// Class used to draw a slider onto the control
    /// </summary>
    internal class JMLevelsSlider
    {
        #region Fields

        /// <summary>
        /// The size of a slider
        /// </summary>
        private static Size sliderSize = new Size(11, JMLevelsSliderContainer.Height - 1);

        /// <summary>
        /// Brush used to paint the slider
        /// </summary>
        private SolidBrush brush;

        /// <summary>
        /// The sliders container
        /// </summary>
        private JMLevelsSliderContainer container;

        /// <summary>
        /// Is the mouse down and moving this slider
        /// </summary>
        private bool dragging;

        /// <summary>
        /// The maximum value for the slider
        /// </summary>
        private int maximumValue;

        /// <summary>
        /// The minimum value for the slider
        /// </summary>
        private int minimumValue;

        /// <summary>
        /// The sliders value
        /// </summary>
        private int value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevelsSlider"/> class.
        /// </summary>
        /// <param name="color">Color of the slider's body</param>
        /// <param name="value">Initial value of the slider</param>
        /// <param name="container">Object that contains this slider</param>
        public JMLevelsSlider(Color color, int value, JMLevelsSliderContainer container)
        {
            this.maximumValue = 255;

            this.Enabled = this.AcceptMouseInput = true;

            this.brush = new SolidBrush(color);

            this.Value = value;

            this.container = container;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Delegate for the OnSliderMoved event</summary>
        /// <param name="oldValue">The previous slider value</param>
        public delegate void SliderMovedEventHandler(int oldValue);

        /// <summary>Delegated method called when Value changes</summary>
        /// <param name="previousValue">Value before it changed</param>
        internal delegate void ValueChangedEventHandler(int previousValue);

        /// <summary>Occurs when this object is refreshed</summary>
        public event RefreshEventHandler OnRefresh;

        /// <summary>Event called when the slider has moved</summary>
        public event SliderMovedEventHandler OnSliderMoved;

        /// <summary>Event that occurs when Value changes</summary>
        public event ValueChangedEventHandler OnValueChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets the size of a slider
        /// </summary>
        public static Size SliderSize
        {
            get
            {
                return sliderSize;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this slider is accepting mouse input.
        /// </summary>
        /// <value><c>true</c> if accepting mouse input; otherwise, <c>false</c>.</value>
        public bool AcceptMouseInput { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMLevelsSlider"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        /// <summary>Gets or sets the highest allowed value</summary>
        public int MaximumValue
        {
            get
            {
                return this.maximumValue;
            }

            set
            {
                if (this.maximumValue == value)
                {
                    return;
                }

                this.maximumValue = value;

                if (this.Value > this.maximumValue)
                {
                    this.Value = this.maximumValue;
                }

                if (this.maximumValue < this.MinimumValue + 2)
                {
                    this.maximumValue = this.MinimumValue + 2;
                }
            }
        }

        /// <summary>Gets or sets the lowest allowed value</summary>
        public int MinimumValue
        {
            get
            {
                return this.minimumValue;
            }

            set
            {
                if (this.minimumValue == value)
                {
                    return;
                }

                this.minimumValue = value;

                if (this.Value < this.minimumValue)
                {
                    this.Value = this.minimumValue;
                }

                if (this.minimumValue > this.MaximumValue - 2)
                {
                    this.minimumValue = this.MaximumValue - 2;
                }
            }
        }

        /// <summary>Gets the rectangle containing the slider</summary>
        public Rectangle Rectangle
        {
            get
            {
                int offset =
                    (int)((((float)(this.container.Size.Width - 1) / 255f) * (float)this.Value) + 0.5 - ((float)SliderSize.Width / 2f));

                return new Rectangle(
                                new Point(this.container.Location.X + offset, this.container.Location.Y),
                                SliderSize);
            }
        }

        /// <summary>Gets or sets the value of this slider (MinimumValue->MaximumValue)</summary>
        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                int oldValue = this.value;

                if (value < this.MinimumValue)
                {
                    this.value = this.MinimumValue;
                }
                else if (value > this.MaximumValue)
                {
                    this.value = this.MaximumValue;
                }
                else
                {
                    this.value = value;
                }

                if (this.OnValueChanged != null)
                {
                    this.OnValueChanged(oldValue);
                }
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
            if (!this.Enabled || !this.AcceptMouseInput || e.Button != MouseButtons.Left || !Rectangle.Contains(e.Location))
            {
                return;
            }

            this.container.DraggingSlider = this.dragging = true;
            this.container.BringToFront(this);
        }

        /// <summary>
        /// Process the mouse moving
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public void MouseMove(MouseEventArgs e)
        {
            if (!this.Enabled || !this.dragging || !this.AcceptMouseInput)
            {
                return;
            }

            int previous = this.Value;

            this.UpdateValue(e.X);

            this.Refresh();

            if (this.OnSliderMoved != null)
            {
                this.OnSliderMoved(previous);
            }
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

            if (this.dragging && this.AcceptMouseInput)
            {
                this.container.DraggingSlider = this.dragging = false;
            }
        }

        /// <summary>
        /// Paints the specified e.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        public void Paint(PaintEventArgs e)
        {
            Rectangle rect = this.Rectangle;

            Point[] points =
                        new[]
                        {
                            new Point(rect.X + (rect.Width / 2), rect.Y),
                            new Point(rect.X + rect.Width - 1, rect.Y + (rect.Height / 2)),
                            new Point(rect.X + rect.Width - 1, rect.Y + rect.Height),
                            new Point(rect.X, rect.Y + rect.Height),
                            new Point(rect.X, rect.Y + (rect.Height / 2)),
                            new Point(rect.X + (rect.Width / 2), rect.Y)
                        };

            e.Graphics.FillPolygon(this.brush, points);

            e.Graphics.DrawLines(this.Enabled ? SystemPens.ControlText : SystemPens.GrayText, points);
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

        /// <summary>
        /// Calculate the new Value given the physical location x
        /// </summary>
        /// <param name="x">Physical x location on the control</param>
        public void UpdateValue(int x)
        {
            this.Value = (int)(((255f / (float)this.container.Size.Width) * (x - this.container.Location.X)) + 0.5);
        }

        #endregion Public methods
    }
}