//---------------------------------------------------------------------------------------
// <copyright file="JMDithering.cs" company="Jonathan Mathews Software">
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
    using JMSoftware.Interfaces;

    /// <summary>
    /// Control to handle changing the dithering levels
    /// </summary>
    public partial class JMDithering : UserControl, IDither
    {
        #region Fields

        /// <summary>
        /// The dithering amount
        /// </summary>
        private int ditherAmount = 5;

        /// <summary>
        /// The dithering random
        /// </summary>
        private int ditherRandom = 3;

        /// <summary>
        /// Is the mouse button being held down?
        /// </summary>
        private bool mouseDown;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMDithering"/> class.
        /// </summary>
        public JMDithering()
        {
            this.InitializeComponent();

            this.udDitheringAmount.Value = this.trkDithering.Value = this.DitherAmount;

            this.trkRandom.Value = this.DitherRandom;

            this.lblRandom.Enabled = this.trkRandom.Enabled = this.DitherAmount > 0;
        }

        #endregion Constructors

        /// <summary>Event raised when the dithering value changes</summary>
        public event EventHandler DitheringChanging;

        /// <summary>Event raised when the dithering value has changed</summary>
        public event EventHandler DitheringChanged;

        /// <summary>Event raised when the random value has changed</summary>
        public event EventHandler RandomChanged;

        #region Properties

        /// <summary>
        /// Gets or sets the level of dithering to be applied (0 to ?)
        /// </summary>
        /// <value>The dithering amount</value>
        [Browsable(true), DefaultValue(5)]
        public int DitherAmount
        {
            get
            {
                return this.ditherAmount;
            }

            set
            {
                if (this.ditherAmount == value || value < 0)
                {
                    return;
                }

                this.ditherAmount = value;

                this.udDitheringAmount.Value = this.trkDithering.Value = this.ditherAmount;

                this.udDitheringAmount.Refresh();

                this.lblRandom.Enabled = this.trkRandom.Enabled = this.trkDithering.Value > 0;

                if (this.mouseDown)
                {
                    this.UpdateDitheringChanging();
                }
                else
                {
                    this.UpdateDitheringChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text shown on the dithering amount label
        /// </summary>
        /// <value>The dithering label.</value>
        [DefaultValue("Dithering Amount:")]
        public string DitheringLabel
        {
            get
            {
                return this.lblDitheringAmount.Text;
            }

            set
            {
                this.lblDitheringAmount.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of randomness to be used
        /// </summary>
        /// <value>The dithering random</value>
        [Browsable(true), DefaultValue(3)]
        public int DitherRandom
        {
            get
            {
                return this.ditherRandom;
            }

            set
            {
                if (this.ditherRandom == value)
                {
                    return;
                }

                this.ditherRandom = value;

                this.trkRandom.Value = this.ditherRandom;

                if (this.RandomChanged != null)
                {
                    this.RandomChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the text shown on the random amount label
        /// </summary>
        /// <value>The random label.</value>
        [DefaultValue("Random:")]
        public string RandomLabel
        {
            get
            {
                return this.lblRandom.Text;
            }

            set
            {
                this.lblRandom.Text = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Resets the value.
        /// </summary>
        public void Reset()
        {
            this.DitherAmount = 5;
            this.DitherRandom = 3;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the MouseDown event of the trkDithering control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkDithering_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = true;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the trkDithering control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TrkDithering_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseDown = false;

                this.UpdateDitheringChanged();
            }
        }

        /// <summary>
        /// Handles the Scroll event of the trkDithering control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkDithering_Scroll(object sender, EventArgs e)
        {
            this.DitherAmount = this.trkDithering.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the trkDithering control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkDithering_ValueChanged(object sender, EventArgs e)
        {
            this.DitherAmount = this.trkDithering.Value;
        }

        /// <summary>
        /// Handles the Scroll event of the trkRandom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkRandom_Scroll(object sender, EventArgs e)
        {
            this.DitherRandom = this.trkRandom.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the trkRandom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkRandom_ValueChanged(object sender, EventArgs e)
        {
            this.DitherRandom = this.trkRandom.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the udDitheringAmount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UdDitheringAmount_ValueChanged(object sender, EventArgs e)
        {
            this.DitherAmount = (int)this.udDitheringAmount.Value;
        }

        /// <summary>
        /// Occurs when the dithering has changed.
        /// </summary>
        private void UpdateDitheringChanged()
        {
            if (this.DitheringChanged == null)
            {
                return;
            }

            this.DitheringChanged(this, new EventArgs());
        }

        /// <summary>
        /// Occurs when the dithering is changing.
        /// </summary>
        private void UpdateDitheringChanging()
        {
            if (this.DitheringChanging == null)
            {
                return;
            }

            this.DitheringChanging(this, new EventArgs());
        }

        #endregion Private methods
    }
}