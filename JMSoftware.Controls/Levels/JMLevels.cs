//---------------------------------------------------------------------------------------
// <copyright file="JMLevels.cs" company="Jonathan Mathews Software">
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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>Delegate used for ValueChanged events</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    public delegate void ValueChangedEventHandler(object sender, EventArgs e);

    /// <summary>Delegate used for OnRefresh events</summary>
    internal delegate void RefreshEventHandler();

    /// <summary>
    /// Control for displaying and adjusting a levels histogram
    /// </summary>
    public class JMLevels : UserControl
    {
        #region Fields

        /// <summary>
        /// Is the control enabled?
        /// </summary>
        private bool enabled = true;

        /// <summary>
        /// Object to draw the levels
        /// </summary>
        private JMLevelsDisplay levelsDisplay;

        /// <summary>
        /// Is the control suspended?
        /// </summary>
        private bool suspended;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMLevels"/> class.
        /// </summary>
        public JMLevels()
        {
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint,
                true);

            this.TopMargin = 5;
            this.BottomMargin = 5;
            this.LeftMargin = 5;
            this.RightMargin = 5;

            this.levelsDisplay = new JMLevelsDisplay();
            this.levelsDisplay.OnRefresh += this.LevelsDisplay_OnRefresh;
            this.levelsDisplay.OnValueChanged += this.DisplayValueChanged;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>Event that fires when the minimum, median, or maximum value changes</summary>
        [Browsable(true), Description("Occurs when the minimum, maximum, or median value changes")]
        public event ValueChangedEventHandler ValueChanged;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets the array of values
        /// </summary>
        /// <value>The array.</value>
        [Browsable(false)]
        public int[] Array
        {
            get
            {
                return this.levelsDisplay.Array;
            }

            set
            {
                this.levelsDisplay.Array = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets BottomMargin
        /// </summary>
        /// <value>The bottom margin.</value>
        [Browsable(false)]
        public int BottomMargin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value></value>
        /// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
        /// <PermissionSet>
        ///     <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ///     <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ///     <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        ///     <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        [DefaultValue(true)]
        public new bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.levelsDisplay.Enabled = this.enabled = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the LeftMargin
        /// </summary>
        /// <value>The left margin.</value>
        [Browsable(false)]
        public int LeftMargin { get; set; }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        /// <value>The maximum.</value>
        [DefaultValue(255)]
        public int Maximum
        {
            get
            {
                return this.levelsDisplay.Maximum;
            }

            set
            {
                if (this.levelsDisplay.Maximum == value)
                {
                    return;
                }

                this.levelsDisplay.Maximum = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the median value (0.0 to 1.0)
        /// </summary>
        /// <value>The median.</value>
        [DefaultValue(0.5f)]
        public float Median
        {
            get
            {
                return this.levelsDisplay.Median;
            }

            set
            {
                if (this.levelsDisplay.Median == value)
                {
                    return;
                }

                this.levelsDisplay.Median = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        /// <value>The minimum.</value>
        [DefaultValue(0)]
        public int Minimum
        {
            get
            {
                return this.levelsDisplay.Minimum;
            }

            set
            {
                if (this.levelsDisplay.Minimum == value)
                {
                    return;
                }

                this.levelsDisplay.Minimum = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the RightMargin
        /// </summary>
        /// <value>The right margin.</value>
        [Browsable(false)]
        public int RightMargin { get; set; }

        /// <summary>
        /// Gets or sets the height and width of the control.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Drawing.Size"/> that represents the height and width of the control in pixels.</returns>
        /// <PermissionSet>
        ///     <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ///     <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        ///     <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
        ///     <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
        /// </PermissionSet>
        public new Size Size
        {
            get
            {
                return base.Size;
            }

            set
            {
                base.Size = value;

                this.levelsDisplay.Size = new Size(
                                                this.ClientRectangle.Width - this.LeftMargin - this.RightMargin,
                                                this.ClientRectangle.Height - this.TopMargin - this.BottomMargin);

                this.levelsDisplay.Location = new Point(this.LeftMargin, this.TopMargin);

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="JMLevels"/> is suspended.
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
                this.suspended = value;

                if (!this.suspended)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets TopMargin
        /// </summary>
        /// <value>The top margin.</value>
        [Browsable(false)]
        public int TopMargin { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Reset the levels to the default values
        /// </summary>
        public void Reset()
        {
            this.Suspended = true;

            this.Minimum = 0;
            this.Maximum = 255;
            this.Median = 0.5f;

            this.Suspended = false;
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Method called when a mouse button is pressed down
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.levelsDisplay.MouseDown(e);

            this.Focus();
        }

        /// <summary>
        /// Method called when the mouse moves
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.levelsDisplay.MouseMove(e);
        }

        /// <summary>
        /// Method called when a mouse button is released
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!this.Enabled)
            {
                return;
            }

            this.levelsDisplay.MouseUp();
        }

        /// <summary>
        /// Method called when the control is painted
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Suspended)
            {
                return;
            }

            e.Graphics.Clear(BackColor);

            this.levelsDisplay.Paint(e);
        }

        /// <summary>
        /// Method called when the control is resized
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            this.Size = new Size(Width, Height);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Occurs when the display value has changed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DisplayValueChanged(object sender, EventArgs e)
        {
            if (this.ValueChanged == null)
            {
                return;
            }

            this.ValueChanged(sender, e);
        }

        /// <summary>
        /// Handles the OnRefresh event of the levelsDisplay
        /// </summary>
        private void LevelsDisplay_OnRefresh()
        {
            this.Refresh();
        }

        #endregion Private methods
    }
}