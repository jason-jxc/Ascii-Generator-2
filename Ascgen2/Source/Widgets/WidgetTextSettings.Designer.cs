//---------------------------------------------------------------------------------------
// <copyright file="WidgetTextSettings.Designer.cs" company="Jonathan Mathews Software">
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
    using System.Windows.Forms;

    /// <summary>
    /// Widget class to display the text adjustment controls
    /// </summary>
    public partial class WidgetTextSettings : BaseWidget
    {
        #region Fields

        private ToolStripMenuItem cmenuBCReset;

        private ContextMenuStrip cmenuBrightnessContrast;

        private ContextMenuStrip cmenuDither;

        private ToolStripMenuItem cmenuDitherReset;

        private System.Windows.Forms.ContextMenuStrip cmenuLevels;

        private System.Windows.Forms.ToolStripMenuItem cmenuLevelsReset;

        private System.Windows.Forms.ToolStripMenuItem cmenuMaximum;

        private System.Windows.Forms.ToolStripMenuItem cmenuMedian;

        private System.Windows.Forms.ToolStripMenuItem cmenuMinimum;

        private ToolStripSeparator cmenuSep1;

        private System.ComponentModel.IContainer components = null;

        private JMSoftware.Controls.JMBrightnessContrast jmBrightnessContrast1;

        private JMSoftware.Controls.JMDithering jmDithering1;

        private JMSoftware.Controls.Levels.JMLevels jmLevels1;

        private OpenFileDialog openFileDialog1;

        private TabPage tabBrightnessContrast;

        private TabPage tabDither;

        private TabPage tabLevels;

        private TabControl tpControls;

        #endregion Fields

        #region Private methods

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.jmLevels1 = new JMSoftware.Controls.Levels.JMLevels();
            this.cmenuLevels = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuLevelsReset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuMinimum = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuMedian = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuMaximum = new System.Windows.Forms.ToolStripMenuItem();
            this.tpControls = new System.Windows.Forms.TabControl();
            this.tabLevels = new System.Windows.Forms.TabPage();
            this.tabBrightnessContrast = new System.Windows.Forms.TabPage();
            this.jmBrightnessContrast1 = new JMSoftware.Controls.JMBrightnessContrast();
            this.cmenuBrightnessContrast = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuBCReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tabDither = new System.Windows.Forms.TabPage();
            this.jmDithering1 = new JMSoftware.Controls.JMDithering();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cmenuDither = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuDitherReset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuLevels.SuspendLayout();
            this.tpControls.SuspendLayout();
            this.tabLevels.SuspendLayout();
            this.tabBrightnessContrast.SuspendLayout();
            this.cmenuBrightnessContrast.SuspendLayout();
            this.tabDither.SuspendLayout();
            this.cmenuDither.SuspendLayout();
            this.SuspendLayout();
            // 
            // jmLevels1
            // 
            this.jmLevels1.Array = null;
            this.jmLevels1.BackColor = System.Drawing.Color.White;
            this.jmLevels1.BottomMargin = 5;
            this.jmLevels1.ContextMenuStrip = this.cmenuLevels;
            this.jmLevels1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmLevels1.Enabled = false;
            this.jmLevels1.LeftMargin = 5;
            this.jmLevels1.Location = new System.Drawing.Point(3, 3);
            this.jmLevels1.Name = "jmLevels1";
            this.jmLevels1.RightMargin = 5;
            this.jmLevels1.Size = new System.Drawing.Size(173, 81);
            this.jmLevels1.Suspended = false;
            this.jmLevels1.TabIndex = 0;
            this.jmLevels1.TopMargin = 5;
            this.jmLevels1.ValueChanged += new JMSoftware.Controls.Levels.ValueChangedEventHandler(this.JmLevels1_ValueChanged);
            // 
            // cmenuLevels
            // 
            this.cmenuLevels.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuLevelsReset,
            this.cmenuSep1,
            this.cmenuMinimum,
            this.cmenuMedian,
            this.cmenuMaximum});
            this.cmenuLevels.Name = "cmenuMain";
            this.cmenuLevels.Size = new System.Drawing.Size(166, 98);
            this.cmenuLevels.Opening += new System.ComponentModel.CancelEventHandler(this.CmenuLevels_Opening);
            // 
            // cmenuLevelsReset
            // 
            this.cmenuLevelsReset.MergeIndex = 0;
            this.cmenuLevelsReset.Name = "cmenuLevelsReset";
            this.cmenuLevelsReset.Size = new System.Drawing.Size(165, 22);
            this.cmenuLevelsReset.Text = "cmenuReset";
            this.cmenuLevelsReset.Click += new System.EventHandler(this.CmenuLevelsReset_Click);
            // 
            // cmenuSep1
            // 
            this.cmenuSep1.MergeIndex = 1;
            this.cmenuSep1.Name = "cmenuSep1";
            this.cmenuSep1.Size = new System.Drawing.Size(162, 6);
            // 
            // cmenuMinimum
            // 
            this.cmenuMinimum.Enabled = false;
            this.cmenuMinimum.MergeIndex = 2;
            this.cmenuMinimum.Name = "cmenuMinimum";
            this.cmenuMinimum.Size = new System.Drawing.Size(165, 22);
            this.cmenuMinimum.Text = "cmenuMinimum";
            // 
            // cmenuMedian
            // 
            this.cmenuMedian.Enabled = false;
            this.cmenuMedian.MergeIndex = 3;
            this.cmenuMedian.Name = "cmenuMedian";
            this.cmenuMedian.Size = new System.Drawing.Size(165, 22);
            this.cmenuMedian.Text = "cmenuMedian";
            // 
            // cmenuMaximum
            // 
            this.cmenuMaximum.Enabled = false;
            this.cmenuMaximum.MergeIndex = 4;
            this.cmenuMaximum.Name = "cmenuMaximum";
            this.cmenuMaximum.Size = new System.Drawing.Size(165, 22);
            this.cmenuMaximum.Text = "cmenuMaximum";
            // 
            // tpControls
            // 
            this.tpControls.Controls.Add(this.tabLevels);
            this.tpControls.Controls.Add(this.tabBrightnessContrast);
            this.tpControls.Controls.Add(this.tabDither);
            this.tpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpControls.HotTrack = true;
            this.tpControls.Location = new System.Drawing.Point(0, 0);
            this.tpControls.Multiline = true;
            this.tpControls.Name = "tpControls";
            this.tpControls.SelectedIndex = 0;
            this.tpControls.Size = new System.Drawing.Size(187, 112);
            this.tpControls.TabIndex = 1;
            // 
            // tabLevels
            // 
            this.tabLevels.BackColor = System.Drawing.Color.White;
            this.tabLevels.Controls.Add(this.jmLevels1);
            this.tabLevels.Location = new System.Drawing.Point(4, 21);
            this.tabLevels.Name = "tabLevels";
            this.tabLevels.Padding = new System.Windows.Forms.Padding(3);
            this.tabLevels.Size = new System.Drawing.Size(179, 87);
            this.tabLevels.TabIndex = 0;
            this.tabLevels.Text = "Levels";
            // 
            // tabBrightnessContrast
            // 
            this.tabBrightnessContrast.BackColor = System.Drawing.Color.White;
            this.tabBrightnessContrast.Controls.Add(this.jmBrightnessContrast1);
            this.tabBrightnessContrast.Location = new System.Drawing.Point(4, 21);
            this.tabBrightnessContrast.Name = "tabBrightnessContrast";
            this.tabBrightnessContrast.Padding = new System.Windows.Forms.Padding(3);
            this.tabBrightnessContrast.Size = new System.Drawing.Size(179, 87);
            this.tabBrightnessContrast.TabIndex = 1;
            this.tabBrightnessContrast.Text = "Brightness/Contrast";
            // 
            // jmBrightnessContrast1
            // 
            this.jmBrightnessContrast1.BackColor = System.Drawing.Color.White;
            this.jmBrightnessContrast1.ContextMenuStrip = this.cmenuBrightnessContrast;
            this.jmBrightnessContrast1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmBrightnessContrast1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jmBrightnessContrast1.Location = new System.Drawing.Point(3, 3);
            this.jmBrightnessContrast1.MinimumSize = new System.Drawing.Size(120, 86);
            this.jmBrightnessContrast1.Name = "jmBrightnessContrast1";
            this.jmBrightnessContrast1.Size = new System.Drawing.Size(173, 86);
            this.jmBrightnessContrast1.Suspended = false;
            this.jmBrightnessContrast1.TabIndex = 0;
            this.jmBrightnessContrast1.OnBrightnessChanging += new System.EventHandler(this.JmBrightnessContrast1_BrightnessChanging);
            this.jmBrightnessContrast1.OnValueChanged += new System.EventHandler(this.JmBrightnessContrast1_ValueChanged);
            this.jmBrightnessContrast1.OnContrastChanged += new System.EventHandler(this.JmBrightnessContrast1_ContrastChanged);
            this.jmBrightnessContrast1.OnBrightnessChanged += new System.EventHandler(this.JmBrightnessContrast1_BrightnessChanged);
            this.jmBrightnessContrast1.OnContrastChanging += new System.EventHandler(this.JmBrightnessContrast1_ContrastChanging);
            this.jmBrightnessContrast1.OnValueChanging += new System.EventHandler(this.JmBrightnessContrast1_ValueChanging);
            // 
            // cmenuBrightnessContrast
            // 
            this.cmenuBrightnessContrast.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuBCReset});
            this.cmenuBrightnessContrast.Name = "cmenuBrightnessContrast";
            this.cmenuBrightnessContrast.Size = new System.Drawing.Size(155, 26);
            this.cmenuBrightnessContrast.Opening += new System.ComponentModel.CancelEventHandler(this.CmenuBrightnessContrast_Opening);
            // 
            // cmenuBCReset
            // 
            this.cmenuBCReset.Name = "cmenuBCReset";
            this.cmenuBCReset.Size = new System.Drawing.Size(154, 22);
            this.cmenuBCReset.Text = "cmenuBCReset";
            this.cmenuBCReset.Click += new System.EventHandler(this.CmenuBCReset_Click);
            // 
            // tabDither
            // 
            this.tabDither.Controls.Add(this.jmDithering1);
            this.tabDither.Location = new System.Drawing.Point(4, 21);
            this.tabDither.Name = "tabDither";
            this.tabDither.Padding = new System.Windows.Forms.Padding(3);
            this.tabDither.Size = new System.Drawing.Size(179, 87);
            this.tabDither.TabIndex = 2;
            this.tabDither.Text = "Dither";
            this.tabDither.UseVisualStyleBackColor = true;
            // 
            // jmDithering1
            // 
            this.jmDithering1.BackColor = System.Drawing.SystemColors.Control;
            this.jmDithering1.ContextMenuStrip = this.cmenuDither;
            this.jmDithering1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmDithering1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jmDithering1.Location = new System.Drawing.Point(3, 3);
            this.jmDithering1.MinimumSize = new System.Drawing.Size(147, 69);
            this.jmDithering1.Name = "jmDithering1";
            this.jmDithering1.Size = new System.Drawing.Size(173, 81);
            this.jmDithering1.TabIndex = 0;
            this.jmDithering1.DitheringChanging += new System.EventHandler(this.JmDithering1_DitheringChanging);
            this.jmDithering1.RandomChanged += new System.EventHandler(this.JmDithering1_RandomChanged);
            this.jmDithering1.DitheringChanged += new System.EventHandler(this.JmDithering1_DitheringChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cmenuDither
            // 
            this.cmenuDither.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuDitherReset});
            this.cmenuDither.Name = "cmenuDither";
            this.cmenuDither.Size = new System.Drawing.Size(172, 26);
            this.cmenuDither.Opening += new System.ComponentModel.CancelEventHandler(this.CmenuDither_Opening);
            // 
            // cmenuDitherReset
            // 
            this.cmenuDitherReset.Name = "cmenuDitherReset";
            this.cmenuDitherReset.Size = new System.Drawing.Size(171, 22);
            this.cmenuDitherReset.Text = "cmenuDitherReset";
            this.cmenuDitherReset.Click += new System.EventHandler(this.CmenuDitherResetToolStripMenuItem_Click);
            // 
            // WidgetTextSettings
            // 
            this.ClientSize = new System.Drawing.Size(187, 112);
            this.Controls.Add(this.tpControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(203, 146);
            this.Name = "WidgetTextSettings";
            this.cmenuLevels.ResumeLayout(false);
            this.tpControls.ResumeLayout(false);
            this.tabLevels.ResumeLayout(false);
            this.tabBrightnessContrast.ResumeLayout(false);
            this.cmenuBrightnessContrast.ResumeLayout(false);
            this.tabDither.ResumeLayout(false);
            this.cmenuDither.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Private methods
    }
}