//---------------------------------------------------------------------------------------
// <copyright file="WidgetImage.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Windows.Forms;
    using JMSoftware.AsciiGeneratorDotNet;

    /// <summary>
    /// Widget to display a jmSelectablePictureBox
    /// </summary>
    public partial class WidgetImage : BaseWidget
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetImage"/> class.
        /// </summary>
        public WidgetImage()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>
        /// Event fired when the selected area has changed
        /// </summary>
        [Browsable(true), Description("Event raised when the image has been double clicked")]
        public new event EventHandler DoubleClick;

        /// <summary>
        /// Occurs when image has been updated by the widget.
        /// </summary>
        [Browsable(true), Description("Occurs when image has been updated by the widget.")]
        public event EventHandler ImageUpdated;

        /// <summary>
        /// Occurs when the user wants to load an image.
        /// </summary>
        [Browsable(true), Description("Occurs when the user wants to load an image.")]
        public event EventHandler LoadImage;

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        [Browsable(true), Description("Occurs when a drag-and-drop operation is completed.")]
        public new event DragEventHandler OnDragDrop;

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
        /// Gets or sets the text displayed when no image is shown.
        /// </summary>
        /// <value>The display text.</value>
        public string DisplayText
        {
            get
            {
                return this.jmSelectablePictureBox1.Text;
            }

            set
            {
                this.jmSelectablePictureBox1.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                return this.jmSelectablePictureBox1.Image;
            }

            set
            {
                this.jmSelectablePictureBox1.Image = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected area.
        /// </summary>
        /// <value>The selected area.</value>
        public Rectangle SelectedArea
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectedArea;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectedArea = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection border.
        /// </summary>
        /// <value>The color of the selection border.</value>
        public Color SelectionBorderColor
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectionBorderColor;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectionBorderColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the selection fill.
        /// </summary>
        /// <value>The color of the selection fill.</value>
        public Color SelectionFillColor
        {
            get
            {
                return this.jmSelectablePictureBox1.SelectionFillColor;
            }

            set
            {
                this.jmSelectablePictureBox1.SelectionFillColor = value;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Rotates/flips the image and calls the event.
        /// </summary>
        /// <param name="type">The type of rotation/flip.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void DoRotateFlip(RotateFlipType type, object sender, EventArgs e)
        {
            this.jmSelectablePictureBox1.RotateImage(type);

            if (this.ImageUpdated != null)
            {
                this.ImageUpdated(sender, e);
            }
        }

        /// <summary>
        /// Updates the UI from the resource file.
        /// </summary>
        public void UpdateUI()
        {
            this.loadImageToolStripMenuItem.Text = Resource.GetString("&Load Image") + "...";

            this.rotate90ToolStripMenuItem.Text = Resource.GetString("Rotate") + " 90°";
            this.rotate180ToolStripMenuItem.Text = Resource.GetString("Rotate") + " 180°";
            this.rotate270ToolStripMenuItem.Text = Resource.GetString("Rotate") + " 270°";

            this.flipHorizontallyToolStripMenuItem.Text = Resource.GetString("Flip Horizontally");
            this.flipVerticallyToolStripMenuItem.Text = Resource.GetString("Flip Vertically");

            this.removeSelectionToolStripMenuItem.Text = Resource.GetString("Remove Selection");
            this.lockSelectedAreaToolStripMenuItem.Text = Resource.GetString("Lock Selected Area");
            this.fillSelectedAreaToolStripMenuItem.Text = Resource.GetString("Fill Selected Area");

            this.selectionAreaFillColourToolStripMenuItem.Text = Resource.GetString("Selection Area Fill Colour") + "...";
            this.selectionAreaBorderColourToolStripMenuItem.Text = Resource.GetString("Selection Area Border Colour") + "...";

            this.updateWhileSelectionChangesToolStripMenuItem.Text = Resource.GetString("Update while Selection Changes");
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Handles the drag over.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private static void HandleDragOver(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileNameW") && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStrip1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.rotate90ToolStripMenuItem.Enabled =
                this.rotate180ToolStripMenuItem.Enabled =
                this.rotate270ToolStripMenuItem.Enabled =
                this.flipHorizontallyToolStripMenuItem.Enabled =
                this.flipVerticallyToolStripMenuItem.Enabled =
                    this.jmSelectablePictureBox1.Image != null;

            this.removeSelectionToolStripMenuItem.Enabled =
                this.lockSelectedAreaToolStripMenuItem.Enabled =
                    (this.jmSelectablePictureBox1.Image != null) &&
                    (this.jmSelectablePictureBox1.SelectedArea.Width > 0 && this.jmSelectablePictureBox1.SelectedArea.Height > 0);

            this.lockSelectedAreaToolStripMenuItem.Checked = this.jmSelectablePictureBox1.SelectionLocked;

            this.fillSelectedAreaToolStripMenuItem.Checked = this.jmSelectablePictureBox1.FillSelectionRectangle;

            this.updateWhileSelectionChangesToolStripMenuItem.Checked = Variables.Instance.UpdateWhileSelecting;
        }

        /// <summary>
        /// Handles the Click event of the fillSelectedAreaToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FillSelectedAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jmSelectablePictureBox1.FillSelectionRectangle = !this.jmSelectablePictureBox1.FillSelectionRectangle;
        }

        /// <summary>
        /// Handles the Click event of the flipHorizontallyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FlipHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipX, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the flipVerticallyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FlipVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.RotateNoneFlipY, sender, e);
        }

        /// <summary>
        /// Handles the drag drop.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void HandleDragDrop(DragEventArgs e)
        {
            if (this.OnDragDrop != null)
            {
                this.OnDragDrop(this, e);
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.DoubleClick != null)
            {
                this.DoubleClick(sender, e);
            }
        }

        /// <summary>
        /// Handles the DragDrop event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_SelectionChanged(object sender, EventArgs e)
        {
            this.SelectionChanged(sender, e);
        }

        /// <summary>
        /// Handles the SelectionChanging event of the jmSelectablePictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JMSelectablePictureBox1_SelectionChanging(object sender, EventArgs e)
        {
            this.SelectionChanging(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the loadImageToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LoadImage != null)
            {
                this.LoadImage(this, e);
            }
        }

        /// <summary>
        /// Handles the Click event of the lockSelectedAreaToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LockSelectedAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jmSelectablePictureBox1.SelectionLocked = !this.jmSelectablePictureBox1.SelectionLocked;
        }

        /// <summary>
        /// Handles the Click event of the removeSelectionToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jmSelectablePictureBox1.SelectNothing();
        }

        /// <summary>
        /// Handles the Click event of the rotate180ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Rotate180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate180FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the rotate270ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Rotate270ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate270FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the rotate90ToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Rotate90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoRotateFlip(RotateFlipType.Rotate90FlipNone, sender, e);
        }

        /// <summary>
        /// Handles the Click event of the selectionAreaBorderColourToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectionAreaBorderColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.jmSelectablePictureBox1.SelectionBorderColor;

            if (this.colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            this.jmSelectablePictureBox1.SelectionBorderColor = this.colorDialog1.Color;
        }

        /// <summary>
        /// Handles the Click event of the selectionAreaFillColourToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectionAreaFillColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.jmSelectablePictureBox1.SelectionFillColor;

            if (this.colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            this.jmSelectablePictureBox1.SelectionFillColor = this.colorDialog1.Color;
        }

        /// <summary>
        /// Handles the Click event of the updateWhileSelectionChangesToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UpdateWhileSelectionChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Variables.Instance.UpdateWhileSelecting = !Variables.Instance.UpdateWhileSelecting;
        }

        /// <summary>
        /// Handles the DragDrop event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void WidgetImage_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            this.HandleDragDrop(e);
        }

        /// <summary>
        /// Handles the DragEnter event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void WidgetImage_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragOver(e);
        }

        #endregion Private methods
    }
}