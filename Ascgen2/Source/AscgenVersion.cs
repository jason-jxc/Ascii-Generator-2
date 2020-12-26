//---------------------------------------------------------------------------------------
// <copyright file="AscgenVersion.cs" company="Jonathan Mathews Software">
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
    /// Static class containing the program details.
    /// </summary>
    public static class AscgenVersion
    {
        #region Constants

        /// <summary>Major version number.</summary>
        public const int Major = 2;

        /// <summary>Minor version number.</summary>
        public const int Minor = 0;

        /// <summary>Patch version number.</summary>
        public const int Patch = 0;

        /// <summary>The program name.</summary>
        public const string ProgramName = "ASCII Generator";

        /// <summary>Version Suffix.</summary>
        public const string Suffix = "";

        /// <summary>Version Suffix Number.</summary>
        public const int SuffixNumber = 1;

        /// <summary>The url of the remote xml file containing the latest version.</summary>
        public const string VersionUrl = "http://ascgen2.sourceforge.net/version.xml";

        #endregion Constants

        #region Public methods

        /// <summary>
        /// Build and return this.the current application version.
        /// </summary>
        /// <returns>The current version as a string.</returns>
        public static new string ToString()
        {
            return string.Format("{0}.{1}.{2}{3}", Major, Minor, Patch, Suffix);
        }

        #endregion Public methods
    }
}