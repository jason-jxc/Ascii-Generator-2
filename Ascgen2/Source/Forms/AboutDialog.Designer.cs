//---------------------------------------------------------------------------------------
// <copyright file="AboutDialog.Designer.cs" company="Jonathan Mathews Software">
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
    using System;
    using System.Drawing;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// The about form
    /// </summary>
    public partial class AboutDialog : Form
    {
		#region Fields 

        /// <summary>
        /// The ok button
        /// </summary>
        private Button btnOk;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        /// <summary>
        /// The link label to jmsoftware.co.uk
        /// </summary>
        private LinkLabel lblJMSoftwareLink;

        /// <summary>
        /// The link label to sourceforge
        /// </summary>
        private LinkLabel lblSourceForgeLink;

        /// <summary>
        /// The program title label
        /// </summary>
        private Label lblTitle;

        /// <summary>
        /// The version label
        /// </summary>
        private Label lblVersion;

        /// <summary>
        /// The program icon display box
        /// </summary>
        private PictureBox pbxIcon;

        /// <summary>
        /// The licence textbox
        /// </summary>
        private TextBox tbxLicence;

		#endregion Fields 

		#region Private methods 

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.tbxLicence = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.pbxIcon = new System.Windows.Forms.PictureBox();
            this.lblJMSoftwareLink = new System.Windows.Forms.LinkLabel();
            this.lblSourceForgeLink = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxLicence
            // 
            this.tbxLicence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxLicence.BackColor = System.Drawing.Color.White;
            this.tbxLicence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxLicence.Location = new System.Drawing.Point(12, 88);
            this.tbxLicence.Multiline = true;
            this.tbxLicence.Name = "tbxLicence";
            this.tbxLicence.ReadOnly = true;
            this.tbxLicence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxLicence.Size = new System.Drawing.Size(360, 83);
            this.tbxLicence.TabIndex = 0;
            this.tbxLicence.TabStop = false;
            this.tbxLicence.Text = resources.GetString("tbxLicence.Text");
            this.tbxLicence.Resize += new System.EventHandler(this.TbxLicence_Resize);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Impact", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(88, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(284, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "lblTitle";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(271, 179);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 32);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "btnOk";
            this.btnOk.UseVisualStyleBackColor = false;
            // 
            // pbxIcon
            // 
            this.pbxIcon.BackColor = System.Drawing.Color.White;
            this.pbxIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbxIcon.Image")));
            this.pbxIcon.Location = new System.Drawing.Point(12, 12);
            this.pbxIcon.Name = "pbxIcon";
            this.pbxIcon.Padding = new System.Windows.Forms.Padding(3);
            this.pbxIcon.Size = new System.Drawing.Size(70, 70);
            this.pbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxIcon.TabIndex = 3;
            this.pbxIcon.TabStop = false;
            // 
            // lblJMSoftwareLink
            // 
            this.lblJMSoftwareLink.Location = new System.Drawing.Point(91, 60);
            this.lblJMSoftwareLink.Name = "lblJMSoftwareLink";
            this.lblJMSoftwareLink.Size = new System.Drawing.Size(120, 16);
            this.lblJMSoftwareLink.TabIndex = 4;
            this.lblJMSoftwareLink.TabStop = true;
            this.lblJMSoftwareLink.Text = "www.jmsoftware.co.uk";
            this.lblJMSoftwareLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblJMSoftwareLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblJMSoftwareLink_LinkClicked);
            // 
            // lblSourceForgeLink
            // 
            this.lblSourceForgeLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSourceForgeLink.Location = new System.Drawing.Point(242, 60);
            this.lblSourceForgeLink.Name = "lblSourceForgeLink";
            this.lblSourceForgeLink.Size = new System.Drawing.Size(130, 16);
            this.lblSourceForgeLink.TabIndex = 5;
            this.lblSourceForgeLink.TabStop = true;
            this.lblSourceForgeLink.Text = "ascgen2.sourceforge.net";
            this.lblSourceForgeLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSourceForgeLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblSourceForgeLink_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.Location = new System.Drawing.Point(20, 177);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(245, 36);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "lblVersion";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(384, 222);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblJMSoftwareLink);
            this.Controls.Add(this.pbxIcon);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbxLicence);
            this.Controls.Add(this.lblSourceForgeLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(376, 208);
            this.Name = "AboutDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.pbxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion Private methods 
    }
}