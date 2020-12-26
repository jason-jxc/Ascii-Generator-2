namespace JMSoftware.Widgets
{
    partial class WidgetImage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.jmSelectablePictureBox1 = new JMSoftware.Controls.JMSelectablePictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.rotate90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate180ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotate270ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.flipHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockSelectedAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillSelectedAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectionAreaBorderColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionAreaFillColourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.updateWhileSelectionChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // jmSelectablePictureBox1
            // 
            this.jmSelectablePictureBox1.AllowDrop = true;
            this.jmSelectablePictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.jmSelectablePictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmSelectablePictureBox1.DrawingImage = true;
            this.jmSelectablePictureBox1.FillSelectionRectangle = true;
            this.jmSelectablePictureBox1.ImageLocation = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.jmSelectablePictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.jmSelectablePictureBox1.Location = new System.Drawing.Point(0, 0);
            this.jmSelectablePictureBox1.Name = "jmSelectablePictureBox1";
            this.jmSelectablePictureBox1.SelectedArea = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.jmSelectablePictureBox1.SelectionBorderColor = System.Drawing.Color.DarkBlue;
            this.jmSelectablePictureBox1.SelectionFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.jmSelectablePictureBox1.SelectionLocked = false;
            this.jmSelectablePictureBox1.Size = new System.Drawing.Size(234, 166);
            this.jmSelectablePictureBox1.SizeMode = JMSoftware.Controls.JMPictureBox.JMPictureBoxSizeMode.FitCenter;
            this.jmSelectablePictureBox1.TabIndex = 0;
            this.jmSelectablePictureBox1.SelectionChanged += new System.EventHandler(this.JMSelectablePictureBox1_SelectionChanged);
            this.jmSelectablePictureBox1.SelectionChanging += new System.EventHandler(this.JMSelectablePictureBox1_SelectionChanging);
            this.jmSelectablePictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.JMSelectablePictureBox1_DragDrop);
            this.jmSelectablePictureBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.JMSelectablePictureBox1_DragEnter);
            this.jmSelectablePictureBox1.DoubleClick += new System.EventHandler(this.JMSelectablePictureBox1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.toolStripMenuItemSep1,
            this.rotate90ToolStripMenuItem,
            this.rotate180ToolStripMenuItem,
            this.rotate270ToolStripMenuItem,
            this.toolStripMenuItemSep2,
            this.flipHorizontallyToolStripMenuItem,
            this.flipVerticallyToolStripMenuItem,
            this.toolStripMenuItemSep3,
            this.removeSelectionToolStripMenuItem,
            this.lockSelectedAreaToolStripMenuItem,
            this.fillSelectedAreaToolStripMenuItem,
            this.toolStripMenuItemSep4,
            this.selectionAreaBorderColourToolStripMenuItem,
            this.selectionAreaFillColourToolStripMenuItem,
            this.toolStripMenuItemSep5,
            this.updateWhileSelectionChangesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(339, 320);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.loadImageToolStripMenuItem.Text = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.LoadImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep1
            // 
            this.toolStripMenuItemSep1.Name = "toolStripMenuItemSep1";
            this.toolStripMenuItemSep1.Size = new System.Drawing.Size(335, 6);
            // 
            // rotate90ToolStripMenuItem
            // 
            this.rotate90ToolStripMenuItem.Name = "rotate90ToolStripMenuItem";
            this.rotate90ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.rotate90ToolStripMenuItem.Text = "rotate90ToolStripMenuItem";
            this.rotate90ToolStripMenuItem.Click += new System.EventHandler(this.Rotate90ToolStripMenuItem_Click);
            // 
            // rotate180ToolStripMenuItem
            // 
            this.rotate180ToolStripMenuItem.Name = "rotate180ToolStripMenuItem";
            this.rotate180ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.rotate180ToolStripMenuItem.Text = "rotate180ToolStripMenuItem";
            this.rotate180ToolStripMenuItem.Click += new System.EventHandler(this.Rotate180ToolStripMenuItem_Click);
            // 
            // rotate270ToolStripMenuItem
            // 
            this.rotate270ToolStripMenuItem.Name = "rotate270ToolStripMenuItem";
            this.rotate270ToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.rotate270ToolStripMenuItem.Text = "rotate270ToolStripMenuItem";
            this.rotate270ToolStripMenuItem.Click += new System.EventHandler(this.Rotate270ToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep2
            // 
            this.toolStripMenuItemSep2.Name = "toolStripMenuItemSep2";
            this.toolStripMenuItemSep2.Size = new System.Drawing.Size(335, 6);
            // 
            // flipHorizontallyToolStripMenuItem
            // 
            this.flipHorizontallyToolStripMenuItem.Image = global::AscGenDotNet.Properties.Resources.shape_flip_horizontal;
            this.flipHorizontallyToolStripMenuItem.Name = "flipHorizontallyToolStripMenuItem";
            this.flipHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.flipHorizontallyToolStripMenuItem.Text = "flipHorizontallyToolStripMenuItem";
            this.flipHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.FlipHorizontallyToolStripMenuItem_Click);
            // 
            // flipVerticallyToolStripMenuItem
            // 
            this.flipVerticallyToolStripMenuItem.Image = global::AscGenDotNet.Properties.Resources.shape_flip_vertical;
            this.flipVerticallyToolStripMenuItem.Name = "flipVerticallyToolStripMenuItem";
            this.flipVerticallyToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.flipVerticallyToolStripMenuItem.Text = "flipVerticallyToolStripMenuItem";
            this.flipVerticallyToolStripMenuItem.Click += new System.EventHandler(this.FlipVerticallyToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep3
            // 
            this.toolStripMenuItemSep3.Name = "toolStripMenuItemSep3";
            this.toolStripMenuItemSep3.Size = new System.Drawing.Size(335, 6);
            // 
            // removeSelectionToolStripMenuItem
            // 
            this.removeSelectionToolStripMenuItem.Name = "removeSelectionToolStripMenuItem";
            this.removeSelectionToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.removeSelectionToolStripMenuItem.Text = "removeSelectionToolStripMenuItem";
            this.removeSelectionToolStripMenuItem.Click += new System.EventHandler(this.RemoveSelectionToolStripMenuItem_Click);
            // 
            // lockSelectedAreaToolStripMenuItem
            // 
            this.lockSelectedAreaToolStripMenuItem.Name = "lockSelectedAreaToolStripMenuItem";
            this.lockSelectedAreaToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.lockSelectedAreaToolStripMenuItem.Text = "lockSelectedAreaToolStripMenuItem";
            this.lockSelectedAreaToolStripMenuItem.Click += new System.EventHandler(this.LockSelectedAreaToolStripMenuItem_Click);
            // 
            // fillSelectedAreaToolStripMenuItem
            // 
            this.fillSelectedAreaToolStripMenuItem.Name = "fillSelectedAreaToolStripMenuItem";
            this.fillSelectedAreaToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.fillSelectedAreaToolStripMenuItem.Text = "fillSelectedAreaToolStripMenuItem";
            this.fillSelectedAreaToolStripMenuItem.Click += new System.EventHandler(this.FillSelectedAreaToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep4
            // 
            this.toolStripMenuItemSep4.Name = "toolStripMenuItemSep4";
            this.toolStripMenuItemSep4.Size = new System.Drawing.Size(335, 6);
            // 
            // selectionAreaBorderColourToolStripMenuItem
            // 
            this.selectionAreaBorderColourToolStripMenuItem.Image = global::AscGenDotNet.Properties.Resources.color_swatch;
            this.selectionAreaBorderColourToolStripMenuItem.Name = "selectionAreaBorderColourToolStripMenuItem";
            this.selectionAreaBorderColourToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.selectionAreaBorderColourToolStripMenuItem.Text = "selectionAreaBorderColourToolStripMenuItem";
            this.selectionAreaBorderColourToolStripMenuItem.Click += new System.EventHandler(this.SelectionAreaBorderColourToolStripMenuItem_Click);
            // 
            // selectionAreaFillColourToolStripMenuItem
            // 
            this.selectionAreaFillColourToolStripMenuItem.Image = global::AscGenDotNet.Properties.Resources.color_swatch;
            this.selectionAreaFillColourToolStripMenuItem.Name = "selectionAreaFillColourToolStripMenuItem";
            this.selectionAreaFillColourToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.selectionAreaFillColourToolStripMenuItem.Text = "selectionAreaFillColourToolStripMenuItem";
            this.selectionAreaFillColourToolStripMenuItem.Click += new System.EventHandler(this.SelectionAreaFillColourToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep5
            // 
            this.toolStripMenuItemSep5.Name = "toolStripMenuItemSep5";
            this.toolStripMenuItemSep5.Size = new System.Drawing.Size(335, 6);
            // 
            // updateWhileSelectionChangesToolStripMenuItem
            // 
            this.updateWhileSelectionChangesToolStripMenuItem.Name = "updateWhileSelectionChangesToolStripMenuItem";
            this.updateWhileSelectionChangesToolStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.updateWhileSelectionChangesToolStripMenuItem.Text = "updateWhileSelectionChangesToolStripMenuItem";
            this.updateWhileSelectionChangesToolStripMenuItem.Click += new System.EventHandler(this.UpdateWhileSelectionChangesToolStripMenuItem_Click);
            // 
            // WidgetImage
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 166);
            this.Controls.Add(this.jmSelectablePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "WidgetImage";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.WidgetImage_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.WidgetImage_DragEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.JMSelectablePictureBox jmSelectablePictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep1;
        private System.Windows.Forms.ToolStripMenuItem rotate90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate180ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotate270ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep2;
        private System.Windows.Forms.ToolStripMenuItem flipHorizontallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipVerticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep3;
        private System.Windows.Forms.ToolStripMenuItem removeSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockSelectedAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillSelectedAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep4;
        private System.Windows.Forms.ToolStripMenuItem selectionAreaFillColourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionAreaBorderColourToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep5;
        private System.Windows.Forms.ToolStripMenuItem updateWhileSelectionChangesToolStripMenuItem;
    }
}