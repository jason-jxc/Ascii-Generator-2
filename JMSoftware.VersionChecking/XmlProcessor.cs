//---------------------------------------------------------------------------------------
// <copyright file="XmlProcessor.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Xml;

    /// <summary>
    /// Class to process xml nodes
    /// </summary>
    public static class XmlProcessor
    {
        #region Public methods

        /// <summary>
        /// Reads the node and returns the boolean value
        /// </summary>
        /// <param name="node">Node to read</param>
        /// <param name="current">Value to use if node is invalid</param>
        /// <returns>A boolean for the node</returns>
        public static bool ReadNode(XmlNode node, bool current)
        {
            if (node == null)
            {
                return current;
            }

            try
            {
                return XmlConvert.ToBoolean(node.InnerText.ToLower());
            }
            catch (System.FormatException)
            {
                return current;
            }
        }

        /// <summary>
        /// Reads the node and returns the Color value
        /// </summary>
        /// <param name="node">Node to read</param>
        /// <param name="current">Value to use if node is invalid</param>
        /// <returns>A Color for the node</returns>
        public static Color ReadNode(XmlNode node, Color current)
        {
            if (node == null || node.InnerText.Length == 0)
            {
                return current;
            }

            try
            {
                return ColorTranslator.FromHtml(node.InnerText);
            }
            catch (System.Exception)
            {
                return current;
            }
        }

        /// <summary>
        /// Reads the node and returns the string value
        /// </summary>
        /// <param name="node">Node to read</param>
        /// <param name="current">Value to use if node is invalid</param>
        /// <param name="allowEmptyString">Should we return an empty string if it is empty?</param>
        /// <returns>A string for the node</returns>
        public static string ReadNode(XmlNode node, string current, bool allowEmptyString)
        {
            if (node == null || (!allowEmptyString && node.InnerText.Length == 0))
            {
                return current;
            }

            return node.InnerText;
        }

        /// <summary>
        /// Reads the node and returns the int value
        /// </summary>
        /// <param name="node">Node to read</param>
        /// <param name="min">Lowest acceptable value</param>
        /// <param name="max">Highest acceptable value</param>
        /// <param name="current">Value to use if node is invalid</param>
        /// <returns>An integer for the node</returns>
        public static int ReadNode(XmlNode node, int min, int max, int current)
        {
            if (node == null)
            {
                return current;
            }

            int result = current;

            try
            {
                result = XmlConvert.ToInt32(node.InnerText);

                if (result < min)
                {
                    result = min;
                }

                if (result > max)
                {
                    result = max;
                }
            }
            catch (System.FormatException)
            {
            }

            return result;
        }

        /// <summary>
        /// Reads the node and returns the float value
        /// </summary>
        /// <param name="node">Node to read</param>
        /// <param name="min">Lowest acceptable value</param>
        /// <param name="max">Highest acceptable value</param>
        /// <param name="current">Value to use if node is invalid</param>
        /// <returns>A float for the node</returns>
        public static float ReadNode(XmlNode node, float min, float max, float current)
        {
            if (node == null)
            {
                return current;
            }

            float result = current;

            try
            {
                result = (float)XmlConvert.ToDouble(node.InnerText);

                if (result < min)
                {
                    result = min;
                }

                if (result > max)
                {
                    result = max;
                }
            }
            catch (System.FormatException)
            {
            }

            return result;
        }

        #endregion Public methods
    }
}