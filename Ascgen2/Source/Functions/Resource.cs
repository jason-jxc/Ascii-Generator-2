//---------------------------------------------------------------------------------------
// <copyright file="Resource.cs" company="Jonathan Mathews Software">
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
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Resources;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Class to handle accessing of the programs resources
    /// </summary>
    public abstract class Resource
    {
        #region Constants

        /// <summary>
        /// The root location of the localization resources.
        /// </summary>
        private const string Location = "AscGenDotNet.Resources.Localization.Localization";

        #endregion Constants

        #region Fields

        /// <summary>
        /// The translation file.
        /// </summary>
        private static string translationFile;

        /// <summary>
        /// Have we attempted to load the resource file?
        /// </summary>
        private static bool translationFileChecked;

        /// <summary>
        /// The translations
        /// </summary>
        private static Dictionary<string, string> translations;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the translation file.
        /// </summary>
        /// <value>The translation file.</value>
        public static string TranslationFile
        {
            get
            {
                if (translationFile != null)
                {
                    return translationFile;
                }

                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

                FileInfo[] files = dir.GetFiles("translation.*.xml");

                StringCollection strings = new StringCollection();

                foreach (FileInfo file in files)
                {
                    strings.Add(file.ToString());
                }

                translationFile = string.Empty;

                switch (strings.Count)
                {
                    case 0:
                        break;

                    case 1:
                        translationFile = strings[0];
                        break;

                    default:
                        using (FormSelectLanguage formSelectLanguage = new FormSelectLanguage(strings))
                        {
                            formSelectLanguage.SelectedItem = Variables.Instance.TranslationFile;

                            if (formSelectLanguage.ShowDialog() == DialogResult.OK)
                            {
                                translationFile = formSelectLanguage.SelectedItem;
                            }
                        }

                        break;
                }

                return translationFile;
            }
        }

        /// <summary>Gets the translated strings</summary>
        public static Dictionary<string, string> Translations
        {
            get
            {
                if (translations != null)
                {
                    return translations;
                }

                translations = new Dictionary<string, string>();

                if (translationFileChecked)
                {
                    return translations;
                }

                XmlDocument doc = new XmlDocument();

                if (TranslationFile.Length == 0)
                {
                    return translations;
                }

                try
                {
                    doc.Load(TranslationFile);

                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        translations.Add(node.Attributes[0].InnerText, node.InnerText);
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(ex.Message, string.Format(GetString("Error with settings file '{0}'"), TranslationFile), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.IO.FileNotFoundException)
                {
                    if (TranslationFile != "translation.xml")
                    {
                        MessageBox.Show(
                                    string.Format(
                                            GetString("Could not load translation file '{0}'"), TranslationFile),
                                            GetString("Error"),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                    }
                }
                catch (ArgumentException)
                {
                }
                finally
                {
                    translationFileChecked = true;
                }

                return translations;
            }
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Get the string named 'key' from the resource file
        /// </summary>
        /// <param name="key">Name of the string to return</param>
        /// <returns>Specified string value from the resource file</returns>
        public static string GetString(string key)
        {
            if (Translations.ContainsKey(key))
            {
                return Translations[key];
            }

            ResourceManager resourceManager = new ResourceManager(
                                            Location,
                                            System.Reflection.Assembly.GetExecutingAssembly());

            string value = resourceManager.GetString(key, Variables.Instance.Culture);

            if (string.IsNullOrEmpty(value))
            {
                // TODO: Log the missing value
                value = key;
            }

            return value;
        }

        #endregion Public methods
    }
}