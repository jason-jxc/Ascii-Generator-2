//---------------------------------------------------------------------------------------
// <copyright file="FormSaveAs.Designer.cs" company="Jonathan Mathews Software">
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
    /// Form to show the different save as types
    /// </summary>
    partial class FormSaveAs
    {
        #region Fields

        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.Button btnOk;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layoutButtons;

        private System.Windows.Forms.TableLayoutPanel layoutBWColour;

        private System.Windows.Forms.TableLayoutPanel layoutContainer;

        private System.Windows.Forms.TableLayoutPanel layoutTextImage;

        private System.Windows.Forms.RadioButton rbBlackWhite;

        private System.Windows.Forms.RadioButton rbColour;

        private System.Windows.Forms.RadioButton rbImage;

        private System.Windows.Forms.RadioButton rbText;

        #endregion Fields

        #region Protected methods

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

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbColour = new System.Windows.Forms.RadioButton();
            this.rbBlackWhite = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbImage = new System.Windows.Forms.RadioButton();
            this.layoutContainer = new System.Windows.Forms.TableLayoutPanel();
            this.layoutBWColour = new System.Windows.Forms.TableLayoutPanel();
            this.layoutTextImage = new System.Windows.Forms.TableLayoutPanel();
            this.layoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.layoutContainer.SuspendLayout();
            this.layoutBWColour.SuspendLayout();
            this.layoutTextImage.SuspendLayout();
            this.layoutButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbColour
            // 
            this.rbColour.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbColour.Location = new System.Drawing.Point(183, 3);
            this.rbColour.Name = "rbColour";
            this.rbColour.Size = new System.Drawing.Size(174, 121);
            this.rbColour.TabIndex = 1;
            this.rbColour.Text = "rbColour";
            this.rbColour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbColour.UseVisualStyleBackColor = true;
            // 
            // rbBlackWhite
            // 
            this.rbBlackWhite.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbBlackWhite.Checked = true;
            this.rbBlackWhite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbBlackWhite.Location = new System.Drawing.Point(3, 3);
            this.rbBlackWhite.Name = "rbBlackWhite";
            this.rbBlackWhite.Size = new System.Drawing.Size(174, 121);
            this.rbBlackWhite.TabIndex = 0;
            this.rbBlackWhite.TabStop = true;
            this.rbBlackWhite.Text = "rbBlackWhite";
            this.rbBlackWhite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbBlackWhite.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(3, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rbText
            // 
            this.rbText.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbText.Checked = true;
            this.rbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbText.Location = new System.Drawing.Point(3, 3);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(174, 121);
            this.rbText.TabIndex = 0;
            this.rbText.TabStop = true;
            this.rbText.Text = "rbText";
            this.rbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // rbImage
            // 
            this.rbImage.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbImage.Location = new System.Drawing.Point(183, 3);
            this.rbImage.Name = "rbImage";
            this.rbImage.Size = new System.Drawing.Size(174, 121);
            this.rbImage.TabIndex = 1;
            this.rbImage.Text = "rbImage";
            this.rbImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbImage.UseVisualStyleBackColor = true;
            // 
            // layoutContainer
            // 
            this.layoutContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutContainer.ColumnCount = 1;
            this.layoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContainer.Controls.Add(this.layoutBWColour, 0, 0);
            this.layoutContainer.Controls.Add(this.layoutTextImage, 0, 1);
            this.layoutContainer.Controls.Add(this.layoutButtons, 0, 2);
            this.layoutContainer.Location = new System.Drawing.Point(12, 12);
            this.layoutContainer.Name = "layoutContainer";
            this.layoutContainer.RowCount = 3;
            this.layoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.layoutContainer.Size = new System.Drawing.Size(360, 290);
            this.layoutContainer.TabIndex = 4;
            // 
            // layoutBWColour
            // 
            this.layoutBWColour.ColumnCount = 2;
            this.layoutBWColour.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutBWColour.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutBWColour.Controls.Add(this.rbColour, 1, 0);
            this.layoutBWColour.Controls.Add(this.rbBlackWhite, 0, 0);
            this.layoutBWColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutBWColour.Location = new System.Drawing.Point(0, 0);
            this.layoutBWColour.Margin = new System.Windows.Forms.Padding(0);
            this.layoutBWColour.Name = "layoutBWColour";
            this.layoutBWColour.RowCount = 1;
            this.layoutBWColour.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutBWColour.Size = new System.Drawing.Size(360, 127);
            this.layoutBWColour.TabIndex = 0;
            // 
            // layoutTextImage
            // 
            this.layoutTextImage.ColumnCount = 2;
            this.layoutTextImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTextImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTextImage.Controls.Add(this.rbImage, 1, 0);
            this.layoutTextImage.Controls.Add(this.rbText, 0, 0);
            this.layoutTextImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTextImage.Location = new System.Drawing.Point(0, 127);
            this.layoutTextImage.Margin = new System.Windows.Forms.Padding(0);
            this.layoutTextImage.Name = "layoutTextImage";
            this.layoutTextImage.RowCount = 1;
            this.layoutTextImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTextImage.Size = new System.Drawing.Size(360, 127);
            this.layoutTextImage.TabIndex = 1;
            // 
            // layoutButtons
            // 
            this.layoutButtons.ColumnCount = 3;
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutButtons.Controls.Add(this.btnOk, 0, 0);
            this.layoutButtons.Controls.Add(this.btnCancel, 2, 0);
            this.layoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutButtons.Location = new System.Drawing.Point(0, 254);
            this.layoutButtons.Margin = new System.Windows.Forms.Padding(0);
            this.layoutButtons.Name = "layoutButtons";
            this.layoutButtons.RowCount = 1;
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutButtons.Size = new System.Drawing.Size(360, 36);
            this.layoutButtons.TabIndex = 2;
            // 
            // FormSaveAs
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 314);
            this.ControlBox = false;
            this.Controls.Add(this.layoutContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(275, 169);
            this.Name = "FormSaveAs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSaveAs";
            this.layoutContainer.ResumeLayout(false);
            this.layoutBWColour.ResumeLayout(false);
            this.layoutTextImage.ResumeLayout(false);
            this.layoutButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Private methods
    }
}