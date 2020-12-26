//---------------------------------------------------------------------------------------
// <copyright file="WidgetTextSettings.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.Widgets
{
    using System;
    using System.ComponentModel;
    using JMSoftware.AsciiGeneratorDotNet;
    using JMSoftware.Interfaces;

    /// <summary>
    /// Widget class to display the text adjustment controls
    /// </summary>
    public partial class WidgetTextSettings : IBrightnessContrast, ILevels, IDither
    {
        #region Fields

        /// <summary>
        /// Is the widget enabled?
        /// </summary>
        private bool enabled;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetTextSettings"/> class.
        /// </summary>
        public WidgetTextSettings()
        {
            this.enabled = true;

            // This call is required by the Windows Form Designer.
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Event raised when the brightness has changed</summary>
        [Browsable(true), Description("Event raised when the brightness has changed")]
        public event EventHandler BrightnessChanged;

        /// <summary>Event raised while the brightness is changing</summary>
        [Browsable(true), Description("Event raised while the brightness is changing")]
        public event EventHandler BrightnessChanging;

        /// <summary>Event raised when the contrast has changed</summary>
        [Browsable(true), Description("Event raised when the contrast has changed")]
        public event EventHandler ContrastChanged;

        /// <summary>Event raised while the contrast is changing</summary>
        [Browsable(true), Description("Event raised while the contrast is changing")]
        public event EventHandler ContrastChanging;

        /// <summary>Event raised when the dithering has changed</summary>
        [Browsable(true), Description("Event raised when the dithering has changed")]
        public event EventHandler DitheringChanged;

        /// <summary>Event raised while the dithering is changing</summary>
        [Browsable(true), Description("Event raised while the dithering is changing")]
        public event EventHandler DitheringChanging;

        /// <summary>Event raised while the random value for dithering has changed</summary>
        [Browsable(true), Description("Event raised while the random value for dithering has changed")]
        public event EventHandler DitheringRandomChanged;

        /// <summary>Event that fires when the minimum, maximum, or median levels value changes</summary>
        [Browsable(true), Description("Fires when the minimum, maximum, or median levels value changes")]
        public event EventHandler LevelsChanged;

        /// <summary>Event raised when a value has changed</summary>
        [Browsable(true), Description("Event raised when a value has changed")]
        public event EventHandler ValueChanged;

        /// <summary>Event raised while a value is changing</summary>
        [Browsable(true), Description("Event raised while a value is changing")]
        public event EventHandler ValueChanging;

        #endregion Events / Delegates

        #region Properties

        /// <summary>Gets or sets the current Brightness</summary>
        public int Brightness
        {
            get { return this.jmBrightnessContrast1.Brightness; }
            set { this.jmBrightnessContrast1.Brightness = value; }
        }

        /// <summary>Gets or sets the current Contrast</summary>
        public int Contrast
        {
            get { return this.jmBrightnessContrast1.Contrast; }
            set { this.jmBrightnessContrast1.Contrast = value; }
        }

        /// <summary>Gets or sets the current Dithering level</summary>
        public int DitherAmount
        {
            get { return this.jmDithering1.DitherAmount; }
            set { this.jmDithering1.DitherAmount = value; }
        }

        /// <summary>Gets or sets the random level for the dithering</summary>
        public int DitherRandom
        {
            get { return this.jmDithering1.DitherRandom; }
            set { this.jmDithering1.DitherRandom = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
        public new bool Enabled
        {
            get { return this.enabled; }
            set { this.jmDithering1.Enabled = this.jmLevels1.Enabled = this.jmBrightnessContrast1.Enabled = this.enabled = value; }
        }

        /// <summary>
        /// Gets or sets the array of values for the levels
        /// </summary>
        /// <value>The levels array.</value>
        public int[] LevelsArray
        {
            get { return this.jmLevels1.Array; }
            set { this.jmLevels1.Array = value; }
        }

        /// <summary>
        /// Gets or sets the Maximum value
        /// </summary>
        /// <value>The maximum level.</value>
        public int Maximum
        {
            get { return this.jmLevels1.Maximum; }
            set { this.jmLevels1.Maximum = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        /// <value>The maximum brightness.</value>
        public int MaximumBrightness
        {
            get { return this.jmBrightnessContrast1.MaximumBrightness; }
            set { this.jmBrightnessContrast1.MaximumBrightness = value; }
        }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        /// <value>The maximum contrast.</value>
        public int MaximumContrast
        {
            get { return this.jmBrightnessContrast1.MaximumContrast; }
            set { this.jmBrightnessContrast1.MaximumContrast = value; }
        }

        /// <summary>
        /// Gets or sets the median value (0.0 to 1.0)
        /// </summary>
        /// <value>The median level.</value>
        public float Median
        {
            get { return this.jmLevels1.Median; }
            set { this.jmLevels1.Median = value; }
        }

        /// <summary>
        /// Gets or sets the Minimum value
        /// </summary>
        /// <value>The minimum level.</value>
        public int Minimum
        {
            get { return this.jmLevels1.Minimum; }
            set { this.jmLevels1.Minimum = value; }
        }

        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        /// <value>The minimum brightness.</value>
        public int MinimumBrightness
        {
            get { return this.jmBrightnessContrast1.MinimumBrightness; }
            set { this.jmBrightnessContrast1.MinimumBrightness = value; }
        }

        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        /// <value>The minimum contrast.</value>
        public int MinimumContrast
        {
            get { return this.jmBrightnessContrast1.MinimumContrast; }
            set { this.jmBrightnessContrast1.MinimumContrast = value; }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        public void UpdateUI()
        {
            this.jmBrightnessContrast1.BrightnessLabel = Resource.GetString("Brightness") + ":";
            this.jmBrightnessContrast1.ContrastLabel = Resource.GetString("Contrast") + ":";
            this.jmDithering1.DitheringLabel = Resource.GetString("Dithering Amount") + ":";
            this.jmDithering1.RandomLabel = Resource.GetString("Random") + ":";

            this.cmenuDitherReset.Text = this.cmenuBCReset.Text = this.cmenuLevelsReset.Text = Resource.GetString("Reset");
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the Click event of the cmenuBCReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuBCReset_Click(object sender, EventArgs e)
        {
            this.jmBrightnessContrast1.Reset();
        }

        /// <summary>
        /// Handles the Opening event of the cmenuBrightnessContrast control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CmenuBrightnessContrast_Opening(object sender, CancelEventArgs e)
        {
            this.cmenuBCReset.Enabled = this.jmBrightnessContrast1.Brightness != 0 || this.jmBrightnessContrast1.Contrast != 0;
        }

        /// <summary>
        /// Handles the Opening event of the cmenuDither control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CmenuDither_Opening(object sender, CancelEventArgs e)
        {
            this.cmenuDither.Enabled = (this.jmDithering1.DitherAmount != 5) || (this.jmDithering1.DitherRandom != 3);
        }

        /// <summary>
        /// Handles the Click event of the cmenuDitherResetToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuDitherResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jmDithering1.Reset();
        }

        /// <summary>
        /// Handles the Opening event of the cmenuLevels control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CmenuLevels_Opening(object sender, CancelEventArgs e)
        {
            this.cmenuLevelsReset.Enabled = this.Enabled && (this.Minimum != 0 || this.Median != 0.5f || this.Maximum != 255);

            this.UpdateLevelsContextMenu();
        }

        /// <summary>
        /// Handles the Click event of the cmenuLevelsReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmenuLevelsReset_Click(object sender, System.EventArgs e)
        {
            this.jmLevels1.Reset();

            if (this.LevelsChanged != null)
            {
                this.LevelsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the BrightnessChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_BrightnessChanged(object sender, EventArgs e)
        {
            if (this.BrightnessChanged != null)
            {
                this.BrightnessChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the BrightnessChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_BrightnessChanging(object sender, EventArgs e)
        {
            if (this.BrightnessChanging != null)
            {
                this.BrightnessChanging(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ContrastChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ContrastChanged(object sender, EventArgs e)
        {
            if (this.ContrastChanged != null)
            {
                this.ContrastChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ContrastChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ContrastChanging(object sender, EventArgs e)
        {
            if (this.ContrastChanging != null)
            {
                this.ContrastChanging(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the ValueChanging event of the jmBrightnessContrast1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmBrightnessContrast1_ValueChanging(object sender, EventArgs e)
        {
            if (this.ValueChanging != null)
            {
                this.ValueChanging(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the DitheringChanged event of the jmDithering1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmDithering1_DitheringChanged(object sender, EventArgs e)
        {
            if (this.DitheringChanged != null)
            {
                this.DitheringChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the DitheringChanging event of the jmDithering1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmDithering1_DitheringChanging(object sender, EventArgs e)
        {
            if (this.DitheringChanging != null)
            {
                this.DitheringChanging(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the RandomChanged event of the jmDithering1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmDithering1_RandomChanged(object sender, EventArgs e)
        {
            if (this.DitheringRandomChanged != null)
            {
                this.DitheringRandomChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Occurs when the levels have changed
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JmLevels1_ValueChanged(object sender, EventArgs e)
        {
            if (this.LevelsChanged == null)
            {
                return;
            }

            this.LevelsChanged(sender, e);
        }

        /// <summary>
        /// Updates the levels context menu.
        /// </summary>
        private void UpdateLevelsContextMenu()
        {
            this.cmenuMinimum.Text = Resource.GetString("Minimum") + ": " + this.Minimum.ToString(Variables.Instance.Culture);
            this.cmenuMedian.Text = Resource.GetString("Median") + ": " + Math.Round(this.Median, 2).ToString(Variables.Instance.Culture);
            this.cmenuMaximum.Text = Resource.GetString("Maximum") + ": " + this.Maximum.ToString(Variables.Instance.Culture);
        }

        #endregion Private methods
    }
}