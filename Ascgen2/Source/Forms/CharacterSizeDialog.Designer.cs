//---------------------------------------------------------------------------------------
// <copyright file="CharacterSizeDialog.Designer.cs" company="Jonathan Mathews Software">
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
	/// Dialog used to specify the size of one character in the font
	/// </summary>
	partial class CharacterSizeDialog : System.Windows.Forms.Form
	{
		#region Fields 

        /// <summary>
        /// The cancel button
        /// </summary>
		private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// The ok button
        /// </summary>
		private System.Windows.Forms.Button btnOk;

        /// <summary>
        /// The auto checkbox
        /// </summary>
		private System.Windows.Forms.CheckBox chkAuto;

        /// <summary>
        /// The x label
        /// </summary>
		private System.Windows.Forms.Label lblX;

        /// <summary>
        /// The height textbox
        /// </summary>
		private System.Windows.Forms.TextBox tbxHeight;

        /// <summary>
        /// The width textbox
        /// </summary>
		private System.Windows.Forms.TextBox tbxWidth;

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
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.tbxWidth = new System.Windows.Forms.TextBox();
            this.tbxHeight = new System.Windows.Forms.TextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(8, 36);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "btnOk";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(80, 36);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "btnCancel";
            // 
            // chkAuto
            // 
            this.chkAuto.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkAuto.Checked = true;
            this.chkAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuto.Location = new System.Drawing.Point(90, 8);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(49, 20);
            this.chkAuto.TabIndex = 3;
            this.chkAuto.Text = "chkAuto";
            this.chkAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkAuto.CheckedChanged += new System.EventHandler(this.ChkAuto_CheckedChanged);
            // 
            // tbxWidth
            // 
            this.tbxWidth.Location = new System.Drawing.Point(8, 8);
            this.tbxWidth.MaxLength = 3;
            this.tbxWidth.Name = "tbxWidth";
            this.tbxWidth.Size = new System.Drawing.Size(32, 20);
            this.tbxWidth.TabIndex = 0;
            this.tbxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxWidth.TextChanged += new System.EventHandler(this.TbxWidth_TextChanged);
            // 
            // tbxHeight
            // 
            this.tbxHeight.Location = new System.Drawing.Point(50, 8);
            this.tbxHeight.MaxLength = 3;
            this.tbxHeight.Name = "tbxHeight";
            this.tbxHeight.Size = new System.Drawing.Size(32, 20);
            this.tbxHeight.TabIndex = 2;
            this.tbxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxHeight.TextChanged += new System.EventHandler(this.TbxHeight_TextChanged);
            // 
            // lblX
            // 
            this.lblX.Location = new System.Drawing.Point(34, 5);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(24, 23);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "x";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CharacterSizeDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(148, 67);
            this.ControlBox = false;
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbxWidth);
            this.Controls.Add(this.tbxHeight);
            this.Controls.Add(this.lblX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterSizeDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CharacterSizeDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion Private methods 
	}
}