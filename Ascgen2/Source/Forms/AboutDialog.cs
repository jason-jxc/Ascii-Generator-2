//---------------------------------------------------------------------------------------
// <copyright file="AboutDialog.cs" company="Jonathan Mathews Software">
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
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// The about form
    /// </summary>
    public partial class AboutDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutDialog"/> class.
        /// </summary>
        public AboutDialog()
        {
            this.InitializeComponent();

            this.UpdateUI();
        }

        #endregion Constructors

        #region Protected methods

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Handles the LinkClicked event of the lblJMSoftwareLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LblJMSoftwareLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lblJMSoftwareLink.LinkVisited = true;

            Process.Start("http://ascgendotnet.jmsoftware.co.uk/");
        }

        /// <summary>
        /// Handles the LinkClicked event of the lblSourceForgeLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LblSourceForgeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lblSourceForgeLink.LinkVisited = true;

            Process.Start("http://ascgen2.sourceforge.net/");
        }

        /// <summary>
        /// Handles the Resize event of the tbxLicence control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxLicence_Resize(object sender, System.EventArgs e)
        {
            this.tbxLicence.Invalidate();
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.lblVersion.Text = string.Format(
                                                "{0} {1} {2}{3}{4}: {5}",
                                                AscgenVersion.ProgramName,
                                                Resource.GetString("Version"),
                                                AscgenVersion.ToString(),
                                                Environment.NewLine,
                                                Resource.GetString("Language"),
                                                Resource.GetString("British English"));

            this.lblTitle.Text = AscgenVersion.ProgramName;

            this.btnOk.Text = Resource.GetString("&Ok");
        }

        #endregion Private methods
    }
}