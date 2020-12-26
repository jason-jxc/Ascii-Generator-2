//---------------------------------------------------------------------------------------
// <copyright file="Version.cs" company="Jonathan Mathews Software">
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
    /// <summary>
    /// Class used to store the version number
    /// </summary>
    public class Version
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        public Version()
        {
            this.SuffixString = string.Empty;

            this.DownloadUrl = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="patch">The patch version number.</param>
        /// <param name="suffix">The suffix version number.</param>
        /// <param name="suffixText">The suffix string.</param>
        public Version(int major, int minor, int patch, int suffix, string suffixText)
            : this(major, minor, patch, suffix, suffixText, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        /// <param name="patch">The patch version number.</param>
        /// <param name="suffix">The suffix version number.</param>
        /// <param name="suffixText">The suffix string.</param>
        /// <param name="downloadUrl">The download URL.</param>
        public Version(int major, int minor, int patch, int suffix, string suffixText, string downloadUrl)
        {
            this.Major = major;

            this.Minor = minor;

            this.Patch = patch;

            this.Suffix = suffix;

            this.SuffixString = suffixText;

            this.DownloadUrl = downloadUrl;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the download URL.
        /// </summary>
        /// <value>The download URL.</value>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets the major version.
        /// </summary>
        /// <value>The major version.</value>
        public int Major { get; set; }

        /// <summary>
        /// Gets or sets the minor version.
        /// </summary>
        /// <value>The minor version.</value>
        public int Minor { get; set; }

        /// <summary>
        /// Gets or sets the patch version.
        /// </summary>
        /// <value>The patch version.</value>
        public int Patch { get; set; }

        /// <summary>
        /// Gets or sets the suffix version.
        /// </summary>
        /// <value>The suffix version.</value>
        public int Suffix { get; set; }

        /// <summary>
        /// Gets or sets the suffix string.
        /// </summary>
        /// <value>The suffix string.</value>
        public string SuffixString { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="version1">The first Version object.</param>
        /// <param name="version2">The second Version object.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(Version version1, Version version2)
        {
            bool result = false;

            if (version1.Major > version2.Major)
            {
                result = true;
            }
            else if (version1.Major == version2.Major)
            {
                if (version1.Minor > version2.Minor)
                {
                    result = true;
                }
                else if (version1.Minor == version2.Minor)
                {
                    if (version1.Patch > version2.Patch)
                    {
                        result = true;
                    }
                    else if (version1.Patch == version2.Patch)
                    {
                        result = version1.Suffix > version2.Suffix;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="version1">The first Version object.</param>
        /// <param name="version2">The second Version object.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(Version version1, Version version2)
        {
            bool result = false;

            if (version1.Major < version2.Major)
            {
                result = true;
            }
            else if (version1.Major == version2.Major)
            {
                if (version1.Minor < version2.Minor)
                {
                    result = true;
                }
                else if (version1.Minor == version2.Minor)
                {
                    if (version1.Patch < version2.Patch)
                    {
                        result = true;
                    }
                    else if (version1.Patch == version2.Patch)
                    {
                        result = version1.Suffix < version2.Suffix;
                    }
                }
            }

            return result;
        }
 
        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="version1">The lh version.</param>
        /// <param name="version2">The rh version.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Version version1, Version version2)
        {
            return !(version1 == version2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="version1">The lh version.</param>
        /// <param name="version2">The rh version.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Version version1, Version version2)
        {
            if ((object)version1 == null || (object)version2 == null)
            {
                return (object)version1 == null && (object)version2 == null;
            }

            return version1.Major == version2.Major && version1.Minor == version2.Minor && version1.Patch == version2.Patch && version1.Suffix == version2.Suffix;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            return this == obj as Version;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (this.Major * 1000000) + (this.Minor * 10000) + (this.Patch * 100) + this.Suffix;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}{3}", this.Major, this.Minor, this.Patch, this.SuffixString);
        }

        #endregion Public methods
    }
}