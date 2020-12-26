//---------------------------------------------------------------------------------------
// <copyright file="WidgetPreview.cs" company="Jonathan Mathews Software">
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
    using JMSoftware.Interfaces;

    /// <summary>
    /// Widget for displaying the colour preview controls
    /// </summary>
    public partial class WidgetPreview : BaseWidget, IZoomLevel
    {
        #region Fields

        /// <summary>
        /// The zoom amount
        /// </summary>
        private float amount;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetPreview"/> class.
        /// </summary>
        public WidgetPreview()
        {
            this.amount = 1.0f;

            this.InitializeComponent();
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Event raised when the close button has been pressed</summary>
        [Browsable(true), Description("Event raised when the close button has been pressed")]
        public event EventHandler CloseForm;

        /// <summary>Event raised when the zoom amount has changed</summary>
        [Browsable(true), Description("Event raised when the zoom amount has changed")]
        public event EventHandler ZoomChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the current Zoom amount
        /// </summary>
        public float Amount
        {
            get
            {
                return this.amount;
            }

            set
            {
                if (value == this.amount || value <= 0.0 || value >= 3.0)
                {
                    return;
                }

                this.amount = value;

                this.btnZoomIn.Enabled = this.Amount < 2.9;
                this.btnZoomOut.Enabled = this.Amount > 0.1;

                if (this.ZoomChanged != null)
                {
                    this.ZoomChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the text for the Close button
        /// </summary>
        [Browsable(true), Description("Text for the Close button")]
        public string CloseText
        {
            get
            {
                return this.btnClose.Text;
            }

            set
            {
                this.btnClose.Text = value;
                this.toolTip1.SetToolTip(this.btnClose, value);
            }
        }

        /// <summary>
        /// Gets or sets the text for the Zoom In button
        /// </summary>
        [Browsable(true), Description("Text for the Zoom In button")]
        public string ZoomInText
        {
            get
            {
                return this.toolTip1.GetToolTip(this.btnZoomIn);
            }

            set
            {
                this.toolTip1.SetToolTip(this.btnZoomIn, value);
            }
        }

        /// <summary>
        /// Gets or sets the text for the Zoom Out button
        /// </summary>
        [Browsable(true), Description("Text for the Zoom Out button")]
        public string ZoomOutText
        {
            get
            {
                return this.toolTip1.GetToolTip(this.btnZoomOut);
            }

            set
            {
                this.toolTip1.SetToolTip(this.btnZoomOut, value);
            }
        }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (this.CloseForm != null)
            {
                this.CloseForm(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the Click event of the btnZoomIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            this.Amount += 0.1f;
        }

        /// <summary>
        /// Handles the Click event of the btnZoomOut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            this.Amount -= 0.1f;
        }

        #endregion Private methods
    }
}