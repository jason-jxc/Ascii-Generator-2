//---------------------------------------------------------------------------------------
// <copyright file="ValidRampCharsDialog.Designer.cs" company="Jonathan Mathews Software">
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
    /// Form to display and change the ramp
    /// </summary>
    public partial class ValidRampCharsDialog : System.Windows.Forms.Form
    {
        #region Fields

        /// <summary>
        /// The Cancel button
        /// </summary>
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// The Default button
        /// </summary>
        private System.Windows.Forms.Button btnDefault;

        /// <summary>
        /// The Ok button
        /// </summary>
        private System.Windows.Forms.Button btnOk;

        /// <summary>
        /// The characters combobox
        /// </summary>
        private System.Windows.Forms.ComboBox cmbCharacters;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// The first label
        /// </summary>
        private System.Windows.Forms.Label lblValid1;

        /// <summary>
        /// The second label
        /// </summary>
        private System.Windows.Forms.Label lblValid2;

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
            this.lblValid1 = new System.Windows.Forms.Label();
            this.lblValid2 = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.cmbCharacters = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 73);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "btnOk";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(227, 73);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "btnCancel";
            // 
            // lblValid1
            // 
            this.lblValid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValid1.Location = new System.Drawing.Point(12, 9);
            this.lblValid1.Name = "lblValid1";
            this.lblValid1.Size = new System.Drawing.Size(290, 16);
            this.lblValid1.TabIndex = 0;
            this.lblValid1.Text = "lblValid1";
            this.lblValid1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblValid2
            // 
            this.lblValid2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValid2.Location = new System.Drawing.Point(12, 54);
            this.lblValid2.Name = "lblValid2";
            this.lblValid2.Size = new System.Drawing.Size(290, 16);
            this.lblValid2.TabIndex = 2;
            this.lblValid2.Text = "lblValid2";
            this.lblValid2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDefault.Location = new System.Drawing.Point(117, 73);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "btnDefault";
            this.btnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
            // 
            // cmbCharacters
            // 
            this.cmbCharacters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCharacters.Location = new System.Drawing.Point(12, 28);
            this.cmbCharacters.Name = "cmbCharacters";
            this.cmbCharacters.Size = new System.Drawing.Size(290, 21);
            this.cmbCharacters.TabIndex = 1;
            this.cmbCharacters.DropDown += new System.EventHandler(this.CmbCharacters_DropDown);
            this.cmbCharacters.TextChanged += new System.EventHandler(this.CmbCharacters_TextChanged);
            // 
            // ValidRampCharsDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(314, 108);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.lblValid2);
            this.Controls.Add(this.lblValid1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbCharacters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 134);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(322, 134);
            this.Name = "ValidRampCharsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion Private methods
    }
}