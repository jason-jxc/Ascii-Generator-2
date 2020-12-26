//---------------------------------------------------------------------------------------
// <copyright file="JMBrightnessContrast.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// Control to handle brightness and contrast input
    /// </summary>
    public partial class JMBrightnessContrast : UserControl
    {
        #region Fields

        /// <summary>The brightness value.</summary>
        private int brightness;

        /// <summary>The contrast value.</summary>
        private int contrast;

        /// <summary>The maximum brightness value.</summary>
        private int maximumBrightness;

        /// <summary>The maximum contrast value.</summary>
        private int maximumContrast;

        /// <summary>The minimum brightness value.</summary>
        private int minimumBrightness;

        /// <summary>The minimum contrast value.</summary>
        private int minimumContrast;

        /// <summary>Is the mouse button being held down?</summary>
        private bool mouseDown;

        /// <summary>Used to store old brightness/contrast value when its trackbar is changing</summary>
        private int startValue;

        /// <summary>Is this control suspended?</summary>
        private bool suspended;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMBrightnessContrast"/> class.
        /// </summary>
        public JMBrightnessContrast()
        {
            this.InitializeComponent();

            this.nudBrightness.Minimum = this.trkBrightness.Minimum = this.minimumBrightness = -200;
            this.nudBrightness.Maximum = this.trkBrightness.Maximum = this.maximumBrightness = 200;

            this.nudContrast.Minimum = this.trkContrast.Minimum = this.minimumContrast = -100;
            this.nudContrast.Maximum = this.trkContrast.Maximum = this.maximumContrast = 100;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Event raised when the brightness value changes</summary>
        public event EventHandler OnBrightnessChanged;

        /// <summary>Event raised when the brightness value is changing</summary>
        public event EventHandler OnBrightnessChanging;

        /// <summary>Event raised when the contrast value changes</summary>
        public event EventHandler OnContrastChanged;

        /// <summary>Event raised when the contrast value is changing</summary>
        public event EventHandler OnContrastChanging;

        /// <summary>Event raised when a value changes</summary>
        public event EventHandler OnValueChanged;

        /// <summary>Event raised when a value is changing</summary>
        public event EventHandler OnValueChanging;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the brightness.
        /// </summary>
        /// <value>The current brightness.</value>
        [DefaultValue(0)]
        public int Brightness
        {
            get
            {
                return this.brightness;
            }

            set
            {
                value = Math.Max(Math.Min(value, this.MaximumBrightness), this.MinimumBrightness);

                if (this.brightness == value)
                {
                    return;
                }

                this.nudBrightness.Value =
                    this.trkBrightness.Value =
                    this.brightness = value;

                if (this.Suspended)
                {
                    return;
                }

                this.nudBrightness.Refresh();

                if (this.mouseDown)
                {
                    this.NotifyBrightnessChanging();
                }
                else
                {
                    this.NotifyBrightnessChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text on the brightness label.
        /// </summary>
        /// <value>The text on the brightness label.</value>
        [DefaultValue("Brightness:")]
        public string BrightnessLabel
        {
            get
            {
                return this.lblBrightness.Text;
            }

            set
            {
                if (this.lblBrightness.Text == value)
                {
                    return;
                }

                this.lblBrightness.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the contrast.
        /// </summary>
        /// <value>The current contrast.</value>
        [DefaultValue(0)]
        public int Contrast
        {
            get
            {
                return this.contrast;
            }

            set
            {
                value = Math.Max(Math.Min(value, this.MaximumContrast), this.MinimumContrast);

                if (this.contrast == value)
                {
                    return;
                }

                this.nudContrast.Value =
                    this.trkContrast.Value =
                    this.contrast = value;

                if (this.Suspended)
                {
                    return;
                }

                this.nudContrast.Refresh();

                if (this.mouseDown)
                {
                    this.NotifyContrastChanging();
                }
                else
                {
                    this.NotifyContrastChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text on the contrast label.
        /// </summary>
        /// <value>The contrast label.</value>
        [DefaultValue("Contrast:")]
        public string ContrastLabel
        {
            get
            {
                return this.lblContrast.Text;
            }

            set
            {
                if (this.lblContrast.Text == value)
                {
                    return;
                }

                this.lblContrast.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum brightness.
        /// </summary>
        /// <value>The maximum brightness.</value>
        [DefaultValue(200)]
        public int MaximumBrightness
        {
            get
            {
                return this.maximumBrightness;
            }

            set
            {
                this.nudBrightness.Maximum =
                        this.trkBrightness.Maximum =
                        this.maximumBrightness = value;

                this.trkBrightness.TickFrequency = (this.maximumBrightness - this.minimumBrightness) / 20;
            }
        }

        /// <summary>
        /// Gets or sets the maximum contrast.
        /// </summary>
        /// <value>The maximum contrast.</value>
        [DefaultValue(100)]
        public int MaximumContrast
        {
            get
            {
                return this.maximumContrast;
            }

            set
            {
                this.nudContrast.Maximum =
                        this.trkContrast.Maximum =
                        this.maximumContrast = value;

                this.trkContrast.TickFrequency = (this.maximumContrast - this.minimumContrast) / 20;
            }
        }

        /// <summary>
        /// Gets or sets the minimum brightness.
        /// </summary>
        /// <value>The minimum brightness.</value>
        [DefaultValue(-200)]
        public int MinimumBrightness
        {
            get
            {
                return this.minimumBrightness;
            }

            set
            {
                this.nudBrightness.Minimum =
                        this.trkBrightness.Minimum =
                        this.minimumBrightness = value;

                this.trkBrightness.TickFrequency = (this.maximumBrightness - this.minimumBrightness) / 20;
            }
        }

        /// <summary>
        /// Gets or sets the minimum contrast.
        /// </summary>
        /// <value>The minimum contrast.</value>
        [DefaultValue(-100)]
        public int MinimumContrast
        {
            get
            {
                return this.minimumContrast;
            }

            set
            {
                this.nudContrast.Minimum =
                        this.trkContrast.Minimum =
                        this.minimumContrast = value;

                this.trkContrast.TickFrequency = (this.maximumContrast - this.minimumContrast) / 20;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMBrightnessContrast"/> is suspended.
        /// </summary>
        /// <value><c>true</c> if suspended; otherwise, <c>false</c>.</value>
        public bool Suspended
        {
            get
            {
                return this.suspended;
            }

            set
            {
                if (this.suspended == value)
                {
                    return;
                }

                this.suspended = value;

                if (!this.suspended)
                {
                    this.Refresh();
                }
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Resets the values to their defaults.
        /// </summary>
        public void Reset()
        {
            this.Suspended = true;

            bool brightnessUpdated = false;
            bool contrastUpdated = false;

            if (this.Brightness != 0)
            {
                this.Brightness = 0;

                brightnessUpdated = true;
            }

            if (this.Contrast != 0)
            {
                this.Contrast = 0;

                contrastUpdated = true;
            }

            this.Suspended = false;

            if (brightnessUpdated)
            {
                this.NotifyBrightnessChanged();
            }
            else
            {
                if (contrastUpdated)
                {
                    this.NotifyContrastChanged();
                }
            }
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Updates the brightness changed events.
        /// </summary>
        private void NotifyBrightnessChanged()
        {
            if (this.OnBrightnessChanged != null)
            {
                this.OnBrightnessChanged(this, new EventArgs());
            }

            if (this.OnValueChanged != null)
            {
                this.OnValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Updates the brightness changing events.
        /// </summary>
        private void NotifyBrightnessChanging()
        {
            if (this.OnBrightnessChanging != null)
            {
                this.OnBrightnessChanging(this, new EventArgs());
            }

            if (this.OnValueChanging != null)
            {
                this.OnValueChanging(this, new EventArgs());
            }
        }

        /// <summary>
        /// Updates the contrast changed events.
        /// </summary>
        private void NotifyContrastChanged()
        {
            if (this.OnContrastChanged != null)
            {
                this.OnContrastChanged(this, new EventArgs());
            }

            if (this.OnValueChanged != null)
            {
                this.OnValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Updates the contrast changing events.
        /// </summary>
        private void NotifyContrastChanging()
        {
            if (this.OnContrastChanging != null)
            {
                this.OnContrastChanging(this, new EventArgs());
            }

            if (this.OnValueChanging != null)
            {
                this.OnValueChanging(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the udBrightness control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NudBrightness_ValueChanged(object sender, EventArgs e)
        {
            this.Brightness = (int)this.nudBrightness.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the udContrast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NudContrast_ValueChanged(object sender, EventArgs e)
        {
            this.Contrast = (int)this.nudContrast.Value;
        }

        /// <summary>
        /// Handles the MouseDown event of the trkBrightness control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkBrightness_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = true;
                this.startValue = this.Brightness;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the trkBrightness control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = false;

                if (this.Brightness != this.startValue)
                {
                    this.NotifyBrightnessChanged();
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event of the trkBrightness control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkBrightness_Scroll(object sender, EventArgs e)
        {
            this.Brightness = this.trkBrightness.Value;
        }

        /// <summary>
        /// Handles the MouseDown event of the trkContrast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkContrast_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = true;
                this.startValue = this.Contrast;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the trkContrast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkContrast_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = false;

                if (this.Contrast != this.startValue)
                {
                    this.NotifyContrastChanged();
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event of the trkContrast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkContrast_Scroll(object sender, EventArgs e)
        {
            this.Contrast = this.trkContrast.Value;
        }

        #endregion Private methods
    }
}