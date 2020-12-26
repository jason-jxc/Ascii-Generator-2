//---------------------------------------------------------------------------------------
// <copyright file="JMSelectablePictureBox.cs" company="Jonathan Mathews Software">
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
    using System.Windows.Forms;

    /// <summary>
    /// JMPictureBox with added area selection function
    /// </summary>
    public class JMSelectablePictureBox : JMPictureBox
    {
        #region Fields

        /// <summary>Are we calling the selection events?</summary>
        private bool callSelectionEvents;

        /// <summary>The end point of the selection in picturebox coordinates</summary>
        private Point endPoint;

        /// <summary>Fill the selection rectangle with a Color?</summary>
        private bool fillSelectionRectangle;

        /// <summary>Is the X dimension protected from changing?</summary>
        private bool isXLocked;

        /// <summary>Is the X dimension protected from changing?</summary>
        private bool isYLocked;

        /// <summary>Size and positions of the selection area modifying handles in picturebox coordinates</summary>
        private Rectangle[] modifiers;

        /// <summary>Offset of the cursor postion to _SelectedArea top left corner when moving</summary>
        private Point movingOffset;

        /// <summary>Are we dragging the selection rectangle?</summary>
        private bool movingRectangle;

        /// <summary>The last selected area</summary>
        private Rectangle previousSelectedArea;

        /// <summary>Selected area of the control</summary>
        private Rectangle selectedArea;

        /// <summary>Are we in the middle of selecting an area of the image?</summary>
        private bool selecting;

        /// <summary>Color for the selection border</summary>
        private Color selectionBorderColor;

        /// <summary>Color for the selection rectangle fill</summary>
        private Color selectionFillColor;

        /// <summary>Is the selected area locked?</summary>
        private bool selectionLocked;

        /// <summary>Has the left shift key been pressed down?</summary>
        private bool shiftIsDown;

        /// <summary>The point at which the left mouse button was pressed down in picturebox coordinates</summary>
        private Point startPoint;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JMSelectablePictureBox"/> class.
        /// </summary>
        public JMSelectablePictureBox()
        {
            this.SelectionFillColor = Color.LightBlue;
            this.SelectionBorderColor = Color.DarkBlue;

            this.modifiers = new[]
                                {
                                    new Rectangle(0, 0, 9, 9),
                                    new Rectangle(0, 0, 9, 9),
                                    new Rectangle(0, 0, 9, 9),
                                    new Rectangle(0, 0, 9, 9),
                                    new Rectangle(0, 0, 7, 7),
                                    new Rectangle(0, 0, 7, 7),
                                    new Rectangle(0, 0, 7, 7),
                                    new Rectangle(0, 0, 7, 7)
                                };

            this.callSelectionEvents = true;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>
        /// Event fired when the selected area has changed
        /// </summary>
        [Browsable(true), Description("Event raised when the selected area has changed")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Event fired when the selected area is changing
        /// </summary>
        [Browsable(true), Description("Event raised when the selected area is changing")]
        public event EventHandler SelectionChanging;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether we will fill the selection rectangle with a Color.
        /// </summary>
        /// <value>
        /// <c>true</c> if filling the selection rectangle; otherwise, <c>false</c>.
        /// </value>
        [Browsable(true), Category("Appearance"), Description("The color used to fill the selection rectangle")]
        public bool FillSelectionRectangle
        {
            get
            {
                return this.fillSelectionRectangle;
            }

            set
            {
                this.fillSelectionRectangle = value;

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [DefaultValue(null), Category("Appearance"), Description("Image to be Displayed")]
        public new Image Image
        {
            get
            {
                return base.Image;
            }

            set
            {
                base.Image = value;

                this.SelectionLocked = false;

                this.SelectNothing();
            }
        }

        /// <summary>
        /// Gets or sets the selected area.
        /// </summary>
        /// <value>The selected area.</value>
        [Browsable(false)]
        public Rectangle SelectedArea
        {
            get
            {
                return this.selectedArea;
            }

            set
            {
                if (this.selectedArea == value || !this.ImageIsLoaded)
                {
                    return;
                }

                this.selectedArea = value;

                this.NotifySelectionChange();

                this.UpdateSelectionHandles();

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color used for filling the selection rectangle
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Appearance"), Description("The color used for the selection rectangle's border")]
        public Color SelectionBorderColor
        {
            get
            {
                return this.selectionBorderColor;
            }

            set
            {
                this.selectionBorderColor = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color used for filling the selection rectangle
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("The color used to fill the selection rectangle")]
        public Color SelectionFillColor
        {
            get
            {
                return this.selectionFillColor;
            }

            set
            {
                this.selectionFillColor = value.A < 255 ? value : Color.FromArgb(128, value.R, value.G, value.B);

                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the selection is locked.
        /// </summary>
        /// <value><c>true</c> if selection locked; otherwise, <c>false</c>.</value>
        public bool SelectionLocked
        {
            get
            {
                return this.selectionLocked;
            }

            set
            {
                this.selectionLocked = value;
                this.Refresh();
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Rotate the image
        /// </summary>
        /// <param name="type">The type of rotation/flip.</param>
        public override void RotateImage(RotateFlipType type)
        {
            this.callSelectionEvents = false;

            this.SelectNothing();

            base.RotateImage(type);

            this.callSelectionEvents = true;
        }

        /// <summary>
        /// Set the selected area to nothing selected
        /// </summary>
        public void SelectNothing()
        {
            this.SelectedArea = new Rectangle(0, 0, 0, 0);
            this.SelectionLocked = false;
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        /// Raises the System.Windows.Forms.Control.KeyDown event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!this.selecting || this.movingRectangle)
            {
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.SelectedArea = this.previousSelectedArea;
                this.ReleaseMouseButton(new Point(0, 0));
                this.Refresh();
            }
            else if (e.KeyCode == Keys.ShiftKey && !this.movingRectangle)
            {
                this.shiftIsDown = true;
                this.SelectionChange();
            }
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.KeyUp event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.shiftIsDown = false;

            if (this.selecting)
            {
                this.SelectionChange();
            }

            base.OnKeyUp(e);
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.LostFocus event.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (this.movingRectangle)
            {
                Invalidate();
            }

            this.selecting = false;

            this.movingRectangle = false;
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.MouseDown event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.MouseEventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left || this.Image == null || this.SelectionLocked)
            {
                return;
            }

            this.previousSelectedArea = this.selectedArea;

            this.selecting = true;
            this.isXLocked = false;
            this.isYLocked = false;

            this.startPoint = new Point(e.X, e.Y);

            Point position = this.ConvertToImageCoordinates(this.startPoint);

            if (this.modifiers[0].Contains(this.startPoint))
            {
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.Right, this.selectedArea.Bottom);
            }
            else if (this.modifiers[1].Contains(this.startPoint))
            {
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.Left, this.selectedArea.Bottom);
            }
            else if (this.modifiers[2].Contains(this.startPoint))
            {
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.Right, this.selectedArea.Y);
            }
            else if (this.modifiers[3].Contains(this.startPoint))
            {
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.X, this.selectedArea.Y);
            }
            else if (this.modifiers[4].Contains(this.startPoint))
            {
                this.isXLocked = true;
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.Left, this.selectedArea.Bottom);
            }
            else if (this.modifiers[5].Contains(this.startPoint))
            {
                this.isXLocked = true;
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.X, this.selectedArea.Y);
            }
            else if (this.modifiers[6].Contains(this.startPoint))
            {
                this.isYLocked = true;
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.Right, this.selectedArea.Y);
            }
            else if (this.modifiers[7].Contains(this.startPoint))
            {
                this.isYLocked = true;
                this.startPoint = this.ConvertToPictureBoxCoordinates(this.selectedArea.X, this.selectedArea.Y);
            }
            else if (this.selectedArea.Contains(position))
            {
                this.movingRectangle = true;
                this.movingOffset = new Point(position.X - this.selectedArea.X, position.Y - this.selectedArea.Y);
                this.selecting = false;
            }

            this.Refresh();
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.MouseMove event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.MouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Image == null)
            {
                return;
            }

            if (this.movingRectangle)
            {
                this.MovingChange(e.Location);

                return;
            }

            if (this.selecting)
            {
                this.SelectionChange(e.Location);

                return;
            }

            // not selecting or moving
            this.UpdateMouseCursor(e.Location);
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.MouseUp event.
        /// </summary>
        /// <param name="e">A System.Windows.Forms.MouseEventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Image != null)
            {
                this.ReleaseMouseButton(e.Location);
            }

            this.Refresh();

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Handle painting the control
        /// </summary>
        /// <param name="e">Provides data for the Paint event</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.ImageIsLoaded || !this.DrawingImage || (this.SelectedArea.Width <= 0 && this.SelectedArea.Height <= 0))
            {
                return;
            }

            Rectangle rectangle = this.ConvertToPictureBoxCoordinates(this.selectedArea);

            // avoid problem caused by the DrawImage bug fix in parent's OnPaint
            if (this.ImageLocation.Width > this.Image.Width || this.ImageLocation.Width > this.Image.Width)
            {
                rectangle.Offset(1, 1);
                rectangle.Width -= 1;
                rectangle.Height -= 1;
            }

            if (this.fillSelectionRectangle)
            {
                using (SolidBrush brush = new SolidBrush(this.selectionFillColor))
                {
                    e.Graphics.FillRectangle(brush, rectangle);
                }
            }

            using (Pen borderPen = new Pen(this.selectionBorderColor, 1))
            {
                e.Graphics.DrawRectangle(borderPen, rectangle);

                if (this.movingRectangle)
                {
                    // Draw the "+" in the center of the selected area
                    int left = (rectangle.Width / 2) + rectangle.Left;
                    int top = (rectangle.Height / 2) + rectangle.Top;

                    e.Graphics.DrawLine(borderPen, left - 2 - (this.ImageIsUpscaled ? 1 : 0), top, left + 2, top);
                    e.Graphics.DrawLine(borderPen, left, top - 2 - (this.ImageIsUpscaled ? 1 : 0), left, top + 2);

                    return;
                }

                if (this.SelectionLocked)
                {
                    rectangle.Inflate(-1, -1);
                    e.Graphics.DrawRectangle(Pens.White, rectangle);
                }
                else
                {
                    e.Graphics.FillRectangles(Brushes.White, this.modifiers);
                    e.Graphics.DrawRectangles(borderPen, this.modifiers);
                }
            }
        }

        /// <summary>
        /// Raises the System.Windows.Forms.Control.Resize event.
        /// </summary>
        /// <param name="e">An System.EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Image != null)
            {
                this.UpdateSelectionHandles();
            }
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Take a point in the current picturebox and convert it to a point in Image
        /// </summary>
        /// <param name="p">A point on the picturebox</param>
        /// <returns>p converted to a point in Image</returns>
        private Point ConvertToImageCoordinates(Point p)
        {
            return this.ConvertToImageCoordinates(p.X, p.Y);
        }

        /// <summary>
        /// Take a point in the current picturebox and convert it to a point in Image
        /// </summary>
        /// <param name="x">The x coordinate of the point on the picturebox</param>
        /// <param name="y">The y coordinate of the point on the picturebox</param>
        /// <returns>(x, y) converted to a point in Image</returns>
        private Point ConvertToImageCoordinates(int x, int y)
        {
            float ratioX = (float)Image.Width / (float)this.ImageLocation.Width;
            float ratioY = (float)Image.Height / (float)this.ImageLocation.Height;

            return new Point(
                        (int)(((float)(x - this.ImageLocation.X) * ratioX) + 0.5),
                        (int)(((float)(y - this.ImageLocation.Y) * ratioY) + 0.5));
        }

        /// <summary>
        /// Take a point in the current Image and convert it to a point in the picturebox
        /// </summary>
        /// <param name="p">A point on the Image</param>
        /// <returns>p converted to a point in the PictureBox</returns>
        private Point ConvertToPictureBoxCoordinates(Point p)
        {
            return this.ConvertToPictureBoxCoordinates(p.X, p.Y);
        }

        /// <summary>
        /// Take a rectangle in the current Image and convert it to a rectangle in the picturebox
        /// </summary>
        /// <param name="r">A rectangle in Image</param>
        /// <returns>r converted to a Rectangle in the picturebox</returns>
        private Rectangle ConvertToPictureBoxCoordinates(Rectangle r)
        {
            Point start = this.ConvertToPictureBoxCoordinates(r.Location);
            Point end = this.ConvertToPictureBoxCoordinates(r.Right, r.Bottom);

            return new Rectangle(start, new Size(end.X - start.X, end.Y - start.Y));
        }

        /// <summary>
        /// Take a point in the current Image and convert it to a point in the picturebox
        /// </summary>
        /// <param name="x">The x coordinate of the point on the Image</param>
        /// <param name="y">The y coordinate of the point on the Image</param>
        /// <returns>(x, y) converted to a point in the picturebox</returns>
        private Point ConvertToPictureBoxCoordinates(int x, int y)
        {
            float ratioX = (float)this.ImageLocation.Width / (float)this.Image.Width;
            float ratioY = (float)this.ImageLocation.Height / (float)this.Image.Height;

            return new Point(
                        (int)(((float)x * ratioX) + 0.5) + this.ImageLocation.X,
                        (int)(((float)y * ratioY) + 0.5) + this.ImageLocation.Y);
        }

        /// <summary>
        /// Moving the selected area
        /// </summary>
        /// <param name="location">The new mouse position</param>
        private void MovingChange(Point location)
        {
            Point position = this.ConvertToImageCoordinates(location.X, location.Y);

            this.selectedArea.X = Math.Max(position.X - this.movingOffset.X, 0);
            this.selectedArea.Y = Math.Max(position.Y - this.movingOffset.Y, 0);

            if (this.selectedArea.Right > Image.Width)
            {
                this.selectedArea.X = this.Image.Width - this.selectedArea.Width;
            }

            if (this.selectedArea.Bottom > this.Image.Height)
            {
                this.selectedArea.Y = this.Image.Height - this.selectedArea.Height;
            }

            this.NotifySelectionChange();

            this.UpdateSelectionHandles();
            this.Refresh();
        }

        /// <summary>
        /// Handle notifing functions of a selection changed/changing event
        /// </summary>
        private void NotifySelectionChange()
        {
            if (!this.callSelectionEvents)
            {
                return;
            }

            if (this.selecting || this.movingRectangle)
            {
                if (this.SelectionChanging != null)
                {
                    this.SelectionChanging(this, EventArgs.Empty);
                }
            }
            else
            {
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Handle mouse released
        /// </summary>
        /// <param name="location">The point at which the button is released</param>
        private void ReleaseMouseButton(Point location)
        {
            bool notifyChange = false;

            if (this.movingRectangle)
            {
                this.movingRectangle = false;

                notifyChange = true;
            }
            else
            {
                this.selecting = false;

                // if the button was released at the point it was clicked
                if (this.startPoint == location)
                {
                    // clear the selection if clicked on the image but outside the current selection
                    if (this.ImageLocation.Contains(this.startPoint))
                    {
                        if (!this.selectedArea.Contains(this.ConvertToImageCoordinates(this.startPoint)))
                        {
                            this.SelectNothing();
                        }
                    }
                }
                else
                {
                    notifyChange = true;
                }
            }

            if (notifyChange && this.SelectedArea != this.previousSelectedArea)
            {
                this.NotifySelectionChange();
            }
        }

        /// <summary>
        /// Update the area being selected as _StartPoint to _EndPoint
        /// </summary>
        private void SelectionChange()
        {
            Point start = this.ConvertToImageCoordinates(this.startPoint);
            start.X = Math.Max(Math.Min(start.X, this.Image.Width), 0);
            start.Y = Math.Max(Math.Min(start.Y, this.Image.Height), 0);

            Point current = this.ConvertToImageCoordinates(this.endPoint);
            current.X = Math.Max(Math.Min(current.X, this.Image.Width), 0);
            current.Y = Math.Max(Math.Min(current.Y, this.Image.Height), 0);

            if (this.shiftIsDown && !this.isXLocked && !this.isYLocked)
            {
                int width = Math.Abs(current.X - start.X);
                int height = Math.Abs(current.Y - start.Y);

                if (width < height)
                {
                    // make height = width
                    current.Y = (current.Y > start.Y) ? start.Y + width : start.Y - width;
                }
                else
                {
                    // make width = height
                    current.X = (current.X > start.X) ? start.X + height : start.X - height;
                }
            }

            Point topleft = new Point(Math.Min(start.X, current.X), Math.Min(start.Y, current.Y));
            Point bottomright = new Point(Math.Max(start.X, current.X), Math.Max(start.Y, current.Y));

            if (this.isXLocked)
            {
                topleft.X = this.SelectedArea.X;
                bottomright.X = this.SelectedArea.Right;
            }
            else if (this.isYLocked)
            {
                topleft.Y = this.SelectedArea.Y;
                bottomright.Y = this.SelectedArea.Bottom;
            }

            this.SelectedArea = new Rectangle(topleft, new Size(bottomright.X - topleft.X, bottomright.Y - topleft.Y));
        }

        /// <summary>
        /// Update the area being selected as _StartPoint to the passed end point
        /// </summary>
        /// <param name="location">New end point for the selection in picturebox coordinates</param>
        private void SelectionChange(Point location)
        {
            this.endPoint = location;
            this.SelectionChange();
        }

        /// <summary>
        /// Update the mouse cursor for the current position
        /// </summary>
        /// <param name="location">The current position</param>
        private void UpdateMouseCursor(Point location)
        {
            if (this.selectionLocked || this.selectedArea.Width < 1 || this.selectedArea.Height < 1)
            {
                return;
            }

            int position = -1;

            for (int i = 0; i < 8; i++)
            {
                if (this.modifiers[i].Contains(location))
                {
                    position = i;
                    break;
                }
            }

            switch (position)
            {
                case 0:
                case 3:
                    this.Cursor = Cursors.SizeNWSE;
                    break;

                case 1:
                case 2:
                    this.Cursor = Cursors.SizeNESW;
                    break;

                case 4:
                case 5:
                    this.Cursor = Cursors.SizeNS;
                    break;

                case 6:
                case 7:
                    this.Cursor = Cursors.SizeWE;
                    break;

                default:
                    this.Cursor = this.selectedArea.Contains(this.ConvertToImageCoordinates(location)) ? Cursors.Hand : Cursors.Default;
                    break;
            }

            this.Refresh();
        }

        /// <summary>
        /// Move the selection modifier squares into the correct positions
        /// </summary>
        private void UpdateSelectionHandles()
        {
            Rectangle selected = this.ConvertToPictureBoxCoordinates(this.selectedArea);

            this.modifiers[0].X = selected.X - 4;
            this.modifiers[0].Y = selected.Y - 4;

            this.modifiers[1].X = selected.Right - 4;
            this.modifiers[1].Y = selected.Y - 4;

            this.modifiers[2].X = selected.X - 4;
            this.modifiers[2].Y = selected.Bottom - 4;

            this.modifiers[3].X = selected.Right - 4;
            this.modifiers[3].Y = selected.Bottom - 4;

            this.modifiers[4].X = selected.X + ((selected.Right - selected.X) / 2) - 3;
            this.modifiers[4].Y = selected.Y - 3;

            this.modifiers[5].X = selected.X + ((selected.Right - selected.X) / 2) - 3;
            this.modifiers[5].Y = selected.Bottom - 3;

            this.modifiers[6].X = selected.X - 3;
            this.modifiers[6].Y = selected.Y + ((selected.Bottom - selected.Y) / 2) - 3;

            this.modifiers[7].X = selected.Right - 3;
            this.modifiers[7].Y = selected.Y + ((selected.Bottom - selected.Y) / 2) - 3;
        }

        #endregion Private methods
    }
}