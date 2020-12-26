//---------------------------------------------------------------------------------------
// <copyright file="JMBrightnessContrast.Designer.cs" company="Jonathan Mathews Software">
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
    /// <summary>
    /// Control to receive user input for brightness and contrast levels
    /// </summary>
    public partial class JMBrightnessContrast
    {
        #region Fields

        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Label for the brightness text</summary>
        private System.Windows.Forms.Label lblBrightness;

        /// <summary>Label for the contrast text</summary>
        private System.Windows.Forms.Label lblContrast;

        /// <summary>Brightness TrackBar</summary>
        private System.Windows.Forms.TrackBar trkBrightness;

        /// <summary>Contrast TrackBar</summary>
        private System.Windows.Forms.TrackBar trkContrast;

        /// <summary>Brightness NumericUpDown</summary>
        private System.Windows.Forms.NumericUpDown nudBrightness;

        /// <summary>Contrast NumericUpDown</summary>
        private System.Windows.Forms.NumericUpDown nudContrast;

        #endregion Fields

        #region Protected methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion Protected methods

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblContrast = new System.Windows.Forms.Label();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.trkBrightness = new System.Windows.Forms.TrackBar();
            this.trkContrast = new System.Windows.Forms.TrackBar();
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.nudContrast = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).BeginInit();
            this.SuspendLayout();
            // 
            // lblContrast
            // 
            this.lblContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblContrast.Location = new System.Drawing.Point(3, 46);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(64, 16);
            this.lblContrast.TabIndex = 21;
            this.lblContrast.Text = "Contrast:";
            this.lblContrast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBrightness
            // 
            this.lblBrightness.Location = new System.Drawing.Point(3, 2);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(64, 16);
            this.lblBrightness.TabIndex = 18;
            this.lblBrightness.Text = "Brightness:";
            this.lblBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trkBrightness
            // 
            this.trkBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trkBrightness.AutoSize = false;
            this.trkBrightness.LargeChange = 20;
            this.trkBrightness.Location = new System.Drawing.Point(-5, 19);
            this.trkBrightness.Maximum = 150;
            this.trkBrightness.Minimum = -150;
            this.trkBrightness.Name = "trkBrightness";
            this.trkBrightness.Size = new System.Drawing.Size(131, 24);
            this.trkBrightness.TabIndex = 20;
            this.trkBrightness.TickFrequency = 15;
            this.trkBrightness.Scroll += new System.EventHandler(this.TrkBrightness_Scroll);
            this.trkBrightness.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrkBrightness_MouseDown);
            this.trkBrightness.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrkBrightness_MouseUp);
            // 
            // trkContrast
            // 
            this.trkContrast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trkContrast.AutoSize = false;
            this.trkContrast.LargeChange = 10;
            this.trkContrast.Location = new System.Drawing.Point(-5, 63);
            this.trkContrast.Maximum = 100;
            this.trkContrast.Minimum = -100;
            this.trkContrast.Name = "trkContrast";
            this.trkContrast.Size = new System.Drawing.Size(131, 24);
            this.trkContrast.TabIndex = 23;
            this.trkContrast.TickFrequency = 10;
            this.trkContrast.Scroll += new System.EventHandler(this.TrkContrast_Scroll);
            this.trkContrast.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrkContrast_MouseDown);
            this.trkContrast.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrkContrast_MouseUp);
            // 
            // nudBrightness
            // 
            this.nudBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudBrightness.Location = new System.Drawing.Point(64, 2);
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(50, 20);
            this.nudBrightness.TabIndex = 24;
            this.nudBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBrightness.ValueChanged += new System.EventHandler(this.NudBrightness_ValueChanged);
            // 
            // nudContrast
            // 
            this.nudContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nudContrast.Location = new System.Drawing.Point(64, 46);
            this.nudContrast.Name = "nudContrast";
            this.nudContrast.Size = new System.Drawing.Size(50, 20);
            this.nudContrast.TabIndex = 25;
            this.nudContrast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudContrast.ValueChanged += new System.EventHandler(this.NudContrast_ValueChanged);
            // 
            // JMBrightnessContrast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudContrast);
            this.Controls.Add(this.nudBrightness);
            this.Controls.Add(this.lblContrast);
            this.Controls.Add(this.lblBrightness);
            this.Controls.Add(this.trkBrightness);
            this.Controls.Add(this.trkContrast);
            this.MinimumSize = new System.Drawing.Size(120, 86);
            this.Name = "JMBrightnessContrast";
            this.Size = new System.Drawing.Size(120, 86);
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudContrast)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
