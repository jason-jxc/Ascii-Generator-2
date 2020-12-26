//---------------------------------------------------------------------------------------
// <copyright file="JMPictureBox.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    /// <summary>
    /// Picture Box class
    /// </summary>
    public class JMPictureBox : UserControl
    {
        #region Fields

        /// <summary>Level of brightness to apply to the image, 0.0f = no change</summary>
        private float brightness;

        /// <summary>The image with Brightess/Contrast applied</summary>
        private Bitmap brightnessContrastImage;

        /// <summary>Image with Brightness/Contrast applied, size less then or equal to ImageLocation</summary>
        private Bitmap canvas;

        /// <summary>Level of contrast to apply to the image, 1.0f = no change</summary>
        private float contrast;

        /// <summary>The original image</summary>
        private Image image;

        /// <summary>Are we drawing the image?</summary>
        private bool imageIsBeingDrawn;

        /// <summary>InterpolationMode to use when drawing the image</summary>
        private InterpolationMode interpolationMode;

        /// <summary>Image resized to ImageLocation.Width and ImageLocation.Height</summary>
        private Bitmap resizedImage;

        /// <summary>Mode to use when drawing the image onto the control</summary>
        private JMPictureBoxSizeMode sizeMode;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMPictureBox"/> class.
        /// </summary>
        public JMPictureBox()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            this.sizeMode = JMPictureBoxSizeMode.Normal;

            this.interpolationMode = InterpolationMode.Default;

            this.imageIsBeingDrawn = true;

            this.brightness = 0.0f;
            this.contrast = 1.0f;
        }

        #endregion Constructors

        #region Enums

        /// <summary>New SizeMode enumerator</summary>
        public enum JMPictureBoxSizeMode
        {
            /// <summary>Picture displayed at the top left corner without any stretching</summary>
            Normal,

            /// <summary>Stretch the image to fit the control</summary>
            Stretch,

            /// <summary>Center the image in the control without resizing</summary>
            Center,

            /// <summary>Center and stretch the image to the control, keeping its aspect ratio</summary>
            FitCenter
        }

        #endregion Enums

        #region Properties

        /// <summary>Gets the image with Brightness/Contrast applied</summary>
        public Image BCImage
        {
            get
            {
                if (this.brightnessContrastImage == null)
                {
                    this.CreateCanvasFromBrightnessContrastImage();
                }

                return this.brightnessContrastImage;
            }
        }

        /// <summary>
        /// Gets or sets the brightness.
        /// </summary>
        /// <value>The brightness.</value>
        [DefaultValue(0.0f), Category("Appearance"), Description("Level of brightness to apply to the image.")]
        public float Brightness
        {
            get
            {
                return this.brightness;
            }

            set
            {
                if (this.brightness == value)
                {
                    return;
                }

                this.brightness = value;

                this.DisposeIfOnlyOneReference(this.brightnessContrastImage);
                this.brightnessContrastImage = null;

                this.DisposeIfOnlyOneReference(this.canvas);
                this.canvas = null;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the contrast.
        /// </summary>
        /// <value>The contrast.</value>
        [DefaultValue(1.0f), Category("Appearance"), Description("Level of contrast to apply to the image.")]
        public float Contrast
        {
            get
            {
                return this.contrast;
            }

            set
            {
                if (this.contrast == value)
                {
                    return;
                }

                this.contrast = value;

                this.DisposeIfOnlyOneReference(this.brightnessContrastImage);
                this.brightnessContrastImage = null;

                this.DisposeIfOnlyOneReference(this.canvas);
                this.canvas = null;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are drawing image.
        /// </summary>
        /// <value><c>true</c> if [drawing image]; otherwise, <c>false</c>.</value>
        public bool DrawingImage
        {
            get
            {
                return this.imageIsBeingDrawn;
            }

            set
            {
                if (this.imageIsBeingDrawn == value)
                {
                    return;
                }

                this.imageIsBeingDrawn = value;

                if (this.imageIsBeingDrawn)
                {
                    this.Refresh();
                }
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [DefaultValue(null), Category("Appearance"), Description("Image to be Displayed")]
        public Image Image
        {
            get
            {
                return this.image;
            }

            set
            {
                this.DisposeIfOnlyOneReference((Bitmap)this.image);
                this.image = value;

                this.DisposeIfOnlyOneReference(this.resizedImage);
                this.resizedImage = null;

                this.DisposeIfOnlyOneReference(this.brightnessContrastImage);
                this.brightnessContrastImage = null;

                this.DisposeIfOnlyOneReference(this.canvas);
                this.canvas = null;

                this.CalculateImageLocation();

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the image is loaded.
        /// </summary>
        /// <value><c>true</c> if image is loaded; otherwise, <c>false</c>.</value>
        public bool ImageIsLoaded
        {
            get
            {
                return this.Image != null;
            }
        }

        /// <summary>
        /// Gets or sets the location of the image on the control.
        /// </summary>
        /// <value>The image location.</value>
        public Rectangle ImageLocation { get; set; }

        /// <summary>
        /// Gets a value indicating whether the image is smaller then the drawing area.
        /// </summary>
        /// <value><c>true</c> if image upscaled; otherwise, <c>false</c>.</value>
        public bool ImageIsUpscaled
        {
            get
            {
                return this.image.Width < this.ImageLocation.Width || this.image.Height < this.ImageLocation.Height;
            }
        }

        /// <summary>
        /// Gets or sets the interpolation mode used for drawing the image.
        /// </summary>
        /// <value>The interpolation mode.</value>
        [DefaultValue(InterpolationMode.Default), Category("Appearance"), Description("InterpolationMode for drawing the image.")]
        public InterpolationMode InterpolationMode
        {
            get
            {
                return this.interpolationMode;
            }

            set
            {
                if (this.interpolationMode == value)
                {
                    return;
                }

                this.interpolationMode = value != InterpolationMode.Invalid ? value : InterpolationMode.Default;

                // repaint the control
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the size mode.
        /// </summary>
        /// <value>The size mode.</value>
        [DefaultValue(JMPictureBoxSizeMode.Normal), Category("Appearance"), Description("Method used to draw the image on the control")]
        public JMPictureBoxSizeMode SizeMode
        {
            get
            {
                return this.sizeMode;
            }

            set
            {
                if (this.sizeMode == value)
                {
                    return;
                }

                this.sizeMode = value;

                if (this.CalculateImageLocation())
                {
                    this.DisposeIfOnlyOneReference(this.resizedImage);
                    this.resizedImage = null;

                    this.DisposeIfOnlyOneReference(this.canvas);
                    this.canvas = null;
                }

                this.Refresh();
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Forces the control to invalidate its client area and immediately redraw itself and any child controls.
        /// </summary>
        public override void Refresh()
        {
            if (this.imageIsBeingDrawn)
            {
                base.Refresh();
            }
        }

        /// <summary>
        /// Rotate the image
        /// </summary>
        /// <param name="type">The type of rotation/flip.</param>
        public virtual void RotateImage(RotateFlipType type)
        {
            using (Image newImage = new Bitmap(Image))
            {
                newImage.RotateFlip(type);

                this.Image = new Bitmap(newImage);
            }
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Raises the System.Windows.Forms.Control.MouseDown event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.MouseEventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handle painting the control
        /// </summary>
        /// <param name="e">Provides data for the Paint event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.ImageLocation.Contains(e.ClipRectangle))
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillRectangle(brush, e.ClipRectangle);
                }
            }

            if (!this.ImageIsLoaded)
            {
                using (Font font = new Font("Microsoft Sans Serif", 8.25f))
                {
                    using (StringFormat f = new StringFormat())
                    {
                        f.Alignment = StringAlignment.Center;
                        f.LineAlignment = StringAlignment.Center;

                        e.Graphics.DrawString(this.Text, font, Brushes.DarkGray, this.ClientRectangle, f);
                    }
                }

                return;
            }

            if (!this.DrawingImage || !e.ClipRectangle.IntersectsWith(this.ImageLocation))
            {
                return;
            }

            if (this.canvas == null)
            {
                this.CreateCanvas();
            }

            if (this.ImageIsUpscaled)
            {
                e.Graphics.InterpolationMode = this.interpolationMode;

                // avoid DrawImage() bug(?) that offsets small images by -0.5 pixels in each direction
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

                e.Graphics.DrawImage(this.canvas, this.ImageLocation);
            }
            else
            {
                e.Graphics.DrawImageUnscaled(this.canvas, this.ImageLocation);
            }
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.Resize event.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if (this.CalculateImageLocation())
            {
                this.DisposeIfOnlyOneReference(this.resizedImage);
                this.resizedImage = null;

                this.CreateCanvasFromBrightnessContrastImage();
            }

            base.OnResize(e);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Calculate a new ImageLocation for the image
        /// </summary>
        /// <returns>Did the size of the rectangle change?</returns>
        private bool CalculateImageLocation()
        {
            if (!this.ImageIsLoaded)
            {
                return false;
            }

            // Store the previous rectangle size
            Size oldSize = this.ImageLocation.Size;

            Size displaySize = new Size(this.DisplayRectangle.Width - 1, this.DisplayRectangle.Height - 1);

            switch (this.sizeMode)
            {
                case JMPictureBoxSizeMode.Stretch:
                    this.ImageLocation = new Rectangle(0, 0, displaySize.Width, displaySize.Height);

                    break;

                case JMPictureBoxSizeMode.FitCenter:
                    float imageRatio = (float)this.Image.Width / (float)this.Image.Height;

                    float areaRatio = (float)displaySize.Width / (float)displaySize.Height;

                    // if the Control is wider then the image
                    if (areaRatio > imageRatio)
                    {
                        Size size = new Size(
                                (int)((imageRatio * (float)displaySize.Height) + 0.5),
                                displaySize.Height);

                        Point location = new Point(
                                (displaySize.Width - size.Width) / 2,
                                0);

                        this.ImageLocation = new Rectangle(location, size);
                    }
                    else
                    {
                        Size size = new Size(
                                displaySize.Width,
                                (int)(((float)displaySize.Width / (float)imageRatio) + 0.5));

                        Point location = new Point(
                                0,
                                (displaySize.Height - size.Height) / 2);

                        this.ImageLocation = new Rectangle(location, size);
                    }

                    break;

                case JMPictureBoxSizeMode.Center:
                    this.ImageLocation = new Rectangle(
                                                (displaySize.Width - this.Image.Width) / 2,
                                                (displaySize.Height - this.Image.Height) / 2,
                                                this.Image.Width,
                                                this.Image.Height);

                    break;

                default:
                    this.ImageLocation = new Rectangle(
                                                0,
                                                0,
                                                this.Image.Width,
                                                this.Image.Height);

                    break;
            }

            return this.ImageLocation.Size != oldSize;
        }

        /// <summary>
        /// Create canvas from brightnessContrastImage
        /// </summary>
        private void CreateCanvasFromBrightnessContrastImage()
        {
            if (!this.ImageIsLoaded || !this.imageIsBeingDrawn || this.ImageLocation.Width == 0 || this.ImageLocation.Height == 0)
            {
                return;
            }

            if (this.brightnessContrastImage == null)
            {
                this.CreateBrightnessContrastImage();
            }

            this.DisposeIfOnlyOneReference(this.canvas);

            if (this.ImageIsUpscaled)
            {
                this.canvas = this.brightnessContrastImage;
            }
            else
            {
                this.canvas = new Bitmap(this.ImageLocation.Width, this.ImageLocation.Height, PixelFormat.Format24bppRgb);

                using (Graphics g = Graphics.FromImage(this.canvas))
                {
                    g.InterpolationMode = this.interpolationMode;

                    g.DrawImage(this.brightnessContrastImage, 0, 0, this.ImageLocation.Width, this.ImageLocation.Height);
                }
            }
        }

        /// <summary>
        /// Creates the brightness contrast image.
        /// </summary>
        private void CreateBrightnessContrastImage()
        {
            this.brightnessContrastImage = new Bitmap(this.image.Width, this.image.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(this.brightnessContrastImage))
            {
                using (ImageAttributes ia = new ImageAttributes())
                {
                    ia.SetColorMatrix(Matrices.BrightnessContrast(this.brightness, this.contrast));

                    g.DrawImage(
                            this.image,
                            new Rectangle(0, 0, this.image.Width, this.image.Height),
                            0,
                            0,
                            this.image.Width,
                            this.image.Height,
                            GraphicsUnit.Pixel,
                            ia);
                }
            }
        }

        /// <summary>
        /// Create canvas from resizedImage
        /// </summary>
        private void CreateCanvas()
        {
            if (!this.ImageIsLoaded || !this.imageIsBeingDrawn || this.ImageLocation.Width == 0 || this.ImageLocation.Height == 0)
            {
                return;
            }

            if (this.ImageIsUpscaled)
            {
                this.DisposeIfOnlyOneReference(this.resizedImage);
                this.resizedImage = (Bitmap)this.image;
            }
            else
            {
                if (this.resizedImage == null)
                {
                    this.CreateResizedImage();
                }
            }

            this.DisposeIfOnlyOneReference(this.canvas);
            this.canvas = new Bitmap(this.resizedImage.Width, this.resizedImage.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(this.canvas))
            {
                using (ImageAttributes ia = new ImageAttributes())
                {
                    ia.SetColorMatrix(Matrices.BrightnessContrast(this.brightness, this.contrast));

                    g.DrawImage(
                            this.resizedImage,
                            new Rectangle(0, 0, this.resizedImage.Width, this.resizedImage.Height),
                            0,
                            0,
                            this.resizedImage.Width,
                            this.resizedImage.Height,
                            GraphicsUnit.Pixel,
                            ia);
                }
            }
        }

        /// <summary>
        /// Creates the resized image.
        /// </summary>
        private void CreateResizedImage()
        {
            this.resizedImage = new Bitmap(this.ImageLocation.Width, this.ImageLocation.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(this.resizedImage))
            {
                g.InterpolationMode = this.interpolationMode;

                g.DrawImage(this.image, 0, 0, this.ImageLocation.Width, this.ImageLocation.Height);
            }
        }

        /// <summary>
        /// Dispose the bitmap in memory if it is only referenced once
        /// </summary>
        /// <param name="bitmap">The bitmap to possibly dispose</param>
        private void DisposeIfOnlyOneReference(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return;
            }

            int count = 0;

            if (bitmap == this.image)
            {
                count++;
            }

            if (bitmap == this.brightnessContrastImage)
            {
                count++;
            }

            if (bitmap == this.resizedImage)
            {
                count++;
            }

            if (bitmap == this.canvas)
            {
                count++;
            }

            if (count == 1)
            {
                bitmap.Dispose();
            }
        }

        #endregion Private methods
    }
}