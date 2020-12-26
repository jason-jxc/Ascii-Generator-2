//---------------------------------------------------------------------------------------
// <copyright file="TextImageMagnificationDialog.Designer.cs" company="Jonathan Mathews Software">
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
    /// Form to get the text magnification level
    /// </summary>
    public partial class TextImageMagnificationDialog : System.Windows.Forms.Form
	{
		#region Fields 

		private System.Windows.Forms.Button btnCancel;

		private System.Windows.Forms.Button btnOk;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Label label1;

		private System.Windows.Forms.Label lblMagnification;

		private System.Windows.Forms.Label lblOutputSize;

		private System.Windows.Forms.PictureBox pbxOutputImage;

		private System.Windows.Forms.Panel pnlOutputImage;

		private System.Windows.Forms.TabPage tabPageInput;

		private System.Windows.Forms.TabPage tabPageOutput;

		private System.Windows.Forms.TabControl tabSample;

		private System.Windows.Forms.TextBox tbxMagnification;

		private System.Windows.Forms.TextBox tbxOutputHeight;

		private System.Windows.Forms.TextBox tbxOutputWidth;

		private System.Windows.Forms.TextBox tbxSampleText;

		private System.Windows.Forms.TrackBar trkMagnification;

		#endregion Fields 

		#region Private methods 

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbxSampleText = new System.Windows.Forms.TextBox();
			this.pnlOutputImage = new System.Windows.Forms.Panel();
			this.pbxOutputImage = new System.Windows.Forms.PictureBox();
			this.lblMagnification = new System.Windows.Forms.Label();
			this.tbxMagnification = new System.Windows.Forms.TextBox();
			this.trkMagnification = new System.Windows.Forms.TrackBar();
			this.tabSample = new System.Windows.Forms.TabControl();
			this.tabPageOutput = new System.Windows.Forms.TabPage();
			this.tabPageInput = new System.Windows.Forms.TabPage();
			this.lblOutputSize = new System.Windows.Forms.Label();
			this.tbxOutputWidth = new System.Windows.Forms.TextBox();
			this.tbxOutputHeight = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlOutputImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxOutputImage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trkMagnification)).BeginInit();
			this.tabSample.SuspendLayout();
			this.tabPageOutput.SuspendLayout();
			this.tabPageInput.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(12, 178);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 24);
			this.btnOk.TabIndex = 9;
			this.btnOk.Text = "btnOk";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(292, 178);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "btnCancel";
			// 
			// tbxSampleText
			// 
			this.tbxSampleText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbxSampleText.Location = new System.Drawing.Point(0, 0);
			this.tbxSampleText.Multiline = true;
			this.tbxSampleText.Name = "tbxSampleText";
			this.tbxSampleText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbxSampleText.Size = new System.Drawing.Size(352, 70);
			this.tbxSampleText.TabIndex = 2;
			this.tbxSampleText.Text = "tbxSampleText1\r\ntbxSampleText2\r\ntbxSampleText3";
			this.tbxSampleText.WordWrap = false;
			this.tbxSampleText.TextChanged += new System.EventHandler(this.TbxSampleText_TextChanged);
			// 
			// pnlOutputImage
			// 
			this.pnlOutputImage.AutoScroll = true;
			this.pnlOutputImage.BackColor = System.Drawing.Color.White;
			this.pnlOutputImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlOutputImage.Controls.Add(this.pbxOutputImage);
			this.pnlOutputImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlOutputImage.Location = new System.Drawing.Point(0, 0);
			this.pnlOutputImage.Name = "pnlOutputImage";
			this.pnlOutputImage.Size = new System.Drawing.Size(352, 70);
			this.pnlOutputImage.TabIndex = 4;
			// 
			// pbxOutputImage
			// 
			this.pbxOutputImage.Location = new System.Drawing.Point(1, 2);
			this.pbxOutputImage.Name = "pbxOutputImage";
			this.pbxOutputImage.Size = new System.Drawing.Size(160, 40);
			this.pbxOutputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbxOutputImage.TabIndex = 0;
			this.pbxOutputImage.TabStop = false;
			// 
			// lblMagnification
			// 
			this.lblMagnification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblMagnification.Location = new System.Drawing.Point(12, 140);
			this.lblMagnification.Name = "lblMagnification";
			this.lblMagnification.Size = new System.Drawing.Size(96, 20);
			this.lblMagnification.TabIndex = 6;
			this.lblMagnification.Text = "lblMagnification";
			this.lblMagnification.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbxMagnification
			// 
			this.tbxMagnification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbxMagnification.Location = new System.Drawing.Point(340, 141);
			this.tbxMagnification.MaxLength = 3;
			this.tbxMagnification.Name = "tbxMagnification";
			this.tbxMagnification.ReadOnly = true;
			this.tbxMagnification.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.tbxMagnification.Size = new System.Drawing.Size(32, 20);
			this.tbxMagnification.TabIndex = 8;
			// 
			// trkMagnification
			// 
			this.trkMagnification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.trkMagnification.AutoSize = false;
			this.trkMagnification.LargeChange = 10;
			this.trkMagnification.Location = new System.Drawing.Point(98, 140);
			this.trkMagnification.Maximum = 1000;
			this.trkMagnification.Minimum = 250;
			this.trkMagnification.Name = "trkMagnification";
			this.trkMagnification.Size = new System.Drawing.Size(236, 32);
			this.trkMagnification.TabIndex = 7;
			this.trkMagnification.TickFrequency = 50;
			this.trkMagnification.Value = 750;
			this.trkMagnification.ValueChanged += new System.EventHandler(this.TrkMagnification_ValueChanged);
			// 
			// tabSample
			// 
			this.tabSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabSample.Controls.Add(this.tabPageOutput);
			this.tabSample.Controls.Add(this.tabPageInput);
			this.tabSample.Location = new System.Drawing.Point(12, 12);
			this.tabSample.Name = "tabSample";
			this.tabSample.SelectedIndex = 0;
			this.tabSample.Size = new System.Drawing.Size(360, 96);
			this.tabSample.TabIndex = 0;
			// 
			// tabPageOutput
			// 
			this.tabPageOutput.Controls.Add(this.pnlOutputImage);
			this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
			this.tabPageOutput.Name = "tabPageOutput";
			this.tabPageOutput.Size = new System.Drawing.Size(352, 70);
			this.tabPageOutput.TabIndex = 0;
			this.tabPageOutput.Text = "tabPageOutput";
			// 
			// tabPageInput
			// 
			this.tabPageInput.Controls.Add(this.tbxSampleText);
			this.tabPageInput.Location = new System.Drawing.Point(4, 22);
			this.tabPageInput.Name = "tabPageInput";
			this.tabPageInput.Size = new System.Drawing.Size(352, 70);
			this.tabPageInput.TabIndex = 1;
			this.tabPageInput.Text = "tabPageInput";
			// 
			// lblOutputSize
			// 
			this.lblOutputSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblOutputSize.Location = new System.Drawing.Point(109, 113);
			this.lblOutputSize.Name = "lblOutputSize";
			this.lblOutputSize.Size = new System.Drawing.Size(72, 20);
			this.lblOutputSize.TabIndex = 1;
			this.lblOutputSize.Text = "lblOutputSize";
			this.lblOutputSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbxOutputWidth
			// 
			this.tbxOutputWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbxOutputWidth.Location = new System.Drawing.Point(187, 114);
			this.tbxOutputWidth.Name = "tbxOutputWidth";
			this.tbxOutputWidth.ReadOnly = true;
			this.tbxOutputWidth.Size = new System.Drawing.Size(40, 20);
			this.tbxOutputWidth.TabIndex = 2;
			this.tbxOutputWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tbxOutputHeight
			// 
			this.tbxOutputHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbxOutputHeight.Location = new System.Drawing.Point(237, 114);
			this.tbxOutputHeight.Name = "tbxOutputHeight";
			this.tbxOutputHeight.ReadOnly = true;
			this.tbxOutputHeight.Size = new System.Drawing.Size(40, 20);
			this.tbxOutputHeight.TabIndex = 4;
			this.tbxOutputHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(225, 113);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "x";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextImageMagnificationDialog
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(384, 216);
			this.ControlBox = false;
			this.Controls.Add(this.tbxMagnification);
			this.Controls.Add(this.trkMagnification);
			this.Controls.Add(this.tbxOutputHeight);
			this.Controls.Add(this.tbxOutputWidth);
			this.Controls.Add(this.lblOutputSize);
			this.Controls.Add(this.tabSample);
			this.Controls.Add(this.lblMagnification);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(384, 232);
			this.Name = "TextImageMagnificationDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TextImageMagnificationDialog";
			this.pnlOutputImage.ResumeLayout(false);
			this.pnlOutputImage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxOutputImage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trkMagnification)).EndInit();
			this.tabSample.ResumeLayout(false);
			this.tabPageOutput.ResumeLayout(false);
			this.tabPageInput.ResumeLayout(false);
			this.tabPageInput.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion Private methods 
	}
}