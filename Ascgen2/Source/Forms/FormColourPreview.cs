//---------------------------------------------------------------------------------------
// <copyright file="FormColourPreview.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiGeneratorDotNet
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using JMSoftware.Interfaces;
    using JMSoftware.Widgets;

    /// <summary>
    /// Class to show a preview of the output in colour
    /// </summary>
    public partial class FormColourPreview : Form
    {
        #region Fields

        /// <summary>Widget used to display the zoom in/out and close buttons</summary>
        private WidgetPreview widget = new WidgetPreview();

        /// <summary>Interface to the object that holds the current zoom level</summary>
        private IZoomLevel zoom;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormColourPreview"/> class.
        /// </summary>
        public FormColourPreview()
        {
            this.InitializeComponent();

            this.zoom = this.widget;

            this.Controls.AddRange(new Control[] { this.widget });

            this.widget.Top = 4;
            this.widget.Left = 4;
            this.widget.BringToFront();
            this.widget.CloseForm += this.Widget_CloseForm;
            this.widget.ZoomChanged += this.Widget_zoomChanged;

            this.UpdateUI();
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets or sets the displayed image</summary>
        public Image Image
        {
            get
            {
                return this.jmPictureBox1.Image;
            }

            set
            {
                this.jmPictureBox1.Image = value;

                this.UpdateImageSize();
            }
        }

        #endregion Properties

        #region Private methods

        /// <summary>
        /// Handles the SizeChanged event of the pnlImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PnlImage_SizeChanged(object sender, EventArgs e)
        {
            this.PositionImage();
        }

        /// <summary>
        /// Move the image into the center if it is smaller then the panel, else put at 0,0
        /// </summary>
        private void PositionImage()
        {
            this.jmPictureBox1.Left = (this.jmPictureBox1.Width < this.pnlImage.Width) ?
                (this.pnlImage.Width - this.jmPictureBox1.Width) / 2 : 0;

            this.jmPictureBox1.Top = (this.jmPictureBox1.Height < this.pnlImage.Height) ?
                (this.pnlImage.Height - this.jmPictureBox1.Height) / 2 : 0;
        }

        /// <summary>
        /// Update the controls for the current image size and output options
        /// </summary>
        private void UpdateImageSize()
        {
            this.jmPictureBox1.Size = new Size(
                (int)(((float)this.jmPictureBox1.Image.Width * this.zoom.Amount) + 0.5),
                (int)(((float)this.jmPictureBox1.Image.Height * this.zoom.Amount) + 0.5));

            this.PositionImage();
        }

        /// <summary>
        /// Updates the title.
        /// </summary>
        private void UpdateTitle()
        {
            int amount = (int)(((float)this.zoom.Amount * 100f) + 0.5);
            this.Text = Resource.GetString("Colour Preview") + " (" + amount + "%)";
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.UpdateTitle();

            this.widget.CloseText = Resource.GetString("Close");
            this.widget.ZoomInText = Resource.GetString("Zoom In");
            this.widget.ZoomOutText = Resource.GetString("Zoom Out");
        }

        /// <summary>
        /// Handles the CloseForm event of the Widget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Widget_CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the zoomChanged event of the Widget control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Widget_zoomChanged(object sender, EventArgs e)
        {
            this.UpdateImageSize();

            this.UpdateTitle();
        }

        #endregion Private methods
    }
}