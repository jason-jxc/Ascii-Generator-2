//---------------------------------------------------------------------------------------
// <copyright file="FormColourPreview.Designer.cs" company="Jonathan Mathews Software">
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
    /// <summary>
    /// Class to show a preview of the output in colour
    /// </summary>
    public partial class FormColourPreview
    {
        #region Fields

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private JMSoftware.Controls.JMPictureBox jmPictureBox1;

        private System.Windows.Forms.Panel pnlImage;

        #endregion Fields

        #region Protected methods

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
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
            this.pnlImage = new System.Windows.Forms.Panel();
            this.jmPictureBox1 = new JMSoftware.Controls.JMPictureBox();
            this.pnlImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImage
            // 
            this.pnlImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImage.AutoScroll = true;
            this.pnlImage.Controls.Add(this.jmPictureBox1);
            this.pnlImage.Location = new System.Drawing.Point(12, 12);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(660, 542);
            this.pnlImage.TabIndex = 1;
            this.pnlImage.SizeChanged += new System.EventHandler(this.PnlImage_SizeChanged);
            // 
            // jmPictureBox1
            // 
            this.jmPictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.jmPictureBox1.DrawingImage = true;
            this.jmPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.jmPictureBox1.MinimumSize = new System.Drawing.Size(50, 50);
            this.jmPictureBox1.Name = "jmPictureBox1";
            this.jmPictureBox1.Size = new System.Drawing.Size(313, 241);
            this.jmPictureBox1.SizeMode = JMSoftware.Controls.JMPictureBox.JMPictureBoxSizeMode.Stretch;
            this.jmPictureBox1.TabIndex = 0;
            // 
            // FormColourPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(684, 566);
            this.Controls.Add(this.pnlImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormColourPreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormColourPreview";
            this.pnlImage.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Private methods
    }
}