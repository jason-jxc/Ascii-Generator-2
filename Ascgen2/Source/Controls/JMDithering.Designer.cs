//---------------------------------------------------------------------------------------
// <copyright file="JMDithering.Designer.cs" company="Jonathan Mathews Software">
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
    /// Control to handle changing the dithering levels
    /// </summary>
    public partial class JMDithering
    {
        #region Fields

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The Dithering Amount label
        /// </summary>
        private System.Windows.Forms.Label lblDitheringAmount;

        /// <summary>
        /// The Dithering Random label
        /// </summary>
        private System.Windows.Forms.Label lblRandom;

        /// <summary>
        /// The Dithering trackbar
        /// </summary>
        private System.Windows.Forms.TrackBar trkDithering;

        /// <summary>
        /// The Dithering Random TrackBar
        /// </summary>
        private System.Windows.Forms.TrackBar trkRandom;

        /// <summary>
        /// The Dithering amount numberic up down
        /// </summary>
        private System.Windows.Forms.NumericUpDown udDitheringAmount;

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

        #region Private methods

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trkDithering = new System.Windows.Forms.TrackBar();
            this.trkRandom = new System.Windows.Forms.TrackBar();
            this.lblDitheringAmount = new System.Windows.Forms.Label();
            this.udDitheringAmount = new System.Windows.Forms.NumericUpDown();
            this.lblRandom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkDithering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRandom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDitheringAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // trkDithering
            // 
            this.trkDithering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trkDithering.AutoSize = false;
            this.trkDithering.Location = new System.Drawing.Point(-1, 23);
            this.trkDithering.Maximum = 25;
            this.trkDithering.Name = "trkDithering";
            this.trkDithering.Size = new System.Drawing.Size(193, 25);
            this.trkDithering.TabIndex = 2;
            this.trkDithering.ValueChanged += new System.EventHandler(this.TrkDithering_ValueChanged);
            this.trkDithering.Scroll += new System.EventHandler(this.TrkDithering_Scroll);
            this.trkDithering.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrkDithering_MouseDown);
            this.trkDithering.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrkDithering_MouseUp);
            // 
            // trkRandom
            // 
            this.trkRandom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trkRandom.AutoSize = false;
            this.trkRandom.Location = new System.Drawing.Point(59, 58);
            this.trkRandom.Maximum = 20;
            this.trkRandom.Name = "trkRandom";
            this.trkRandom.Size = new System.Drawing.Size(130, 25);
            this.trkRandom.TabIndex = 3;
            this.trkRandom.ValueChanged += new System.EventHandler(this.TrkRandom_ValueChanged);
            this.trkRandom.Scroll += new System.EventHandler(this.TrkRandom_Scroll);
            // 
            // lblDitheringAmount
            // 
            this.lblDitheringAmount.AutoSize = true;
            this.lblDitheringAmount.Location = new System.Drawing.Point(3, 5);
            this.lblDitheringAmount.Name = "lblDitheringAmount";
            this.lblDitheringAmount.Size = new System.Drawing.Size(91, 13);
            this.lblDitheringAmount.TabIndex = 0;
            this.lblDitheringAmount.Text = "Dithering Amount:";
            // 
            // udDitheringAmount
            // 
            this.udDitheringAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.udDitheringAmount.Location = new System.Drawing.Point(135, 3);
            this.udDitheringAmount.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.udDitheringAmount.Name = "udDitheringAmount";
            this.udDitheringAmount.Size = new System.Drawing.Size(50, 20);
            this.udDitheringAmount.TabIndex = 1;
            this.udDitheringAmount.ValueChanged += new System.EventHandler(this.UdDitheringAmount_ValueChanged);
            // 
            // lblRandom
            // 
            this.lblRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRandom.AutoSize = true;
            this.lblRandom.Location = new System.Drawing.Point(3, 62);
            this.lblRandom.Name = "lblRandom";
            this.lblRandom.Size = new System.Drawing.Size(50, 13);
            this.lblRandom.TabIndex = 4;
            this.lblRandom.Text = "Random:";
            // 
            // JMDithering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblRandom);
            this.Controls.Add(this.udDitheringAmount);
            this.Controls.Add(this.lblDitheringAmount);
            this.Controls.Add(this.trkRandom);
            this.Controls.Add(this.trkDithering);
            this.MinimumSize = new System.Drawing.Size(147, 69);
            this.Name = "JMDithering";
            this.Size = new System.Drawing.Size(188, 86);
            ((System.ComponentModel.ISupportInitialize)(this.trkDithering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRandom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDitheringAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Private methods
    }
}
