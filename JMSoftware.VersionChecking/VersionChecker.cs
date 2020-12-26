//---------------------------------------------------------------------------------------
// <copyright file="VersionChecker.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.VersionChecking
{
    using System;
    using System.Net;
    using System.Xml;

    /// <summary>
    /// Class to handle checking for a new version
    /// </summary>
    public class VersionChecker
    {
        #region Fields

        /// <summary>
        /// WebClient used to access the remote files.
        /// </summary>
        private readonly WebClient webClient;

        /// <summary>
        /// The last version that was read.
        /// </summary>
        private Version version;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionChecker"/> class.
        /// </summary>
        public VersionChecker()
        {
            this.webClient = new WebClient { Proxy = null };

            this.webClient.DownloadStringCompleted += this.DownloadStringCompleted;
        }

        #endregion Constructors

        #region Events / Delegates

        /// <summary>
        /// Occurs when the version has been asynchronously read.
        /// </summary>
        public event EventHandler ReadAsyncCompletedEventHandler;

        /// <summary>
        /// Occurs when the asynchronous read has failed.
        /// </summary>
        public event EventHandler ReadAsyncFailedEventHandler;

        #endregion Events / Delegates

        #region Properties

        /// <summary>
        /// Gets the version that has been read.
        /// </summary>
        /// <value>The version.</value>
        /// <remarks>Equals null if not read or the read failed.</remarks>
        public Version Version
        {
            get
            {
                return this.version;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Reads the specified URL into this.Latest.
        /// </summary>
        /// <param name="uri">The Uri object.</param>
        /// <returns>Did the url load successfully?</returns>
        public bool Read(Uri uri)
        {
            string result;

            try
            {
                result = this.webClient.DownloadString(uri);
            }
            catch (WebException)
            {
                result = null;
            }
            catch (NotSupportedException)
            {
                result = null;
            }

            this.version = ProcessXml(result);

            return this.Version != null;
        }

        /// <summary>
        /// Reads the specified URL into this.Latest.
        /// </summary>
        /// <param name="url">The URL to read.</param>
        /// <returns>Did the url load successfully?</returns>
        public bool Read(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            return this.Read(new Uri(url));
        }

        /// <summary>
        /// Reads the specified URL into this.Latest.
        /// </summary>
        /// <param name="uri">The Uri object.</param>
        public void ReadAsync(Uri uri)
        {
            if (this.webClient.IsBusy)
            {
                this.webClient.CancelAsync();
            }

            try
            {
                this.webClient.DownloadStringAsync(uri);
            }
            catch (WebException)
            {
            }
        }

        /// <summary>
        /// Reads the specified URL into this.Latest asynchronously, triggering VersionReadOkEventHandler if successful.
        /// </summary>
        /// <param name="url">The URL to read.</param>
        public void ReadAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                this.ReadAsync(new Uri(url));
            }
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Processes the XML from the string into a new Version object.
        /// </summary>
        /// <param name="html">A string containing the HTML.</param>
        /// <returns>The version details taken from the html, or null.</returns>
        private static Version ProcessXml(string html)
        {
            if (html == null)
            {
                return null;
            }

            XmlDocument document = new XmlDocument();

            try
            {
                document.LoadXml(html);

                return new Version(
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/major"), 0, 100, 0),
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/minor"), 0, 100, 0),
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/patch"), 0, 100, 0),
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/suffixnum"), 0, 100, 0),
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/suffix"), string.Empty, true),
                        XmlProcessor.ReadNode(document.SelectSingleNode("version/url"), string.Empty, true));
            }
            catch (XmlException)
            {
                return null;
            }
        }

        /// <summary>
        /// Triggered when the string has been downloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                this.DownloadStringFailed(sender, e);

                return;
            }

            string result = e.Result;

            if (result.Length == 0 || !result.Substring(0, 13).Equals("<?xml version"))
            {
                this.DownloadStringFailed(sender, e);

                return;
            }

            this.version = ProcessXml(result);

            if (this.ReadAsyncCompletedEventHandler != null)
            {
                this.ReadAsyncCompletedEventHandler(sender, e);
            }
        }

        /// <summary>
        /// Called when the async download string failed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        private void DownloadStringFailed(object sender, DownloadStringCompletedEventArgs e)
        {
            this.version = null;

            if (this.ReadAsyncFailedEventHandler != null)
            {
                this.ReadAsyncFailedEventHandler(sender, e);
            }
        }

        #endregion Private methods
    }
}