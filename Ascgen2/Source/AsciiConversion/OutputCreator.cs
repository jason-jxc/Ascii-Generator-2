//---------------------------------------------------------------------------------------
// <copyright file="OutputCreator.cs" company="Jonathan Mathews Software">
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
namespace JMSoftware.AsciiConversion
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;
    using JMSoftware.AsciiGeneratorDotNet;

    /// <summary>
    /// Creates the custom output text files
    /// </summary>
    public class OutputCreator
    {
        #region Fields

        /// <summary>
        /// Array that stores the list id for each position of the color in uniqueColors
        /// </summary>
        private int[][] characterToColor;

        /// <summary>
        /// The 2d array of colors.
        /// </summary>
        private Color[][] colors;

        /// <summary>
        /// The strings containing the ASCII image.
        /// </summary>
        private string[] strings;

        /// <summary>
        /// The text processing settings.
        /// </summary>
        private TextProcessingSettings textProcessingSettings;

        /// <summary>
        /// List of unique colors.
        /// </summary>
        private ArrayList uniqueColors;

        /// <summary>
        /// Are we using Colors in the output?
        /// </summary>
        private bool useColor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputCreator"/> class.
        /// </summary>
        /// <param name="imageArray">The strings containing the ASCII image.</param>
        /// <param name="textProcessingSettings">The text processing settings.</param>
        /// <param name="colors">The 2d array of colors.</param>
        public OutputCreator(string[] imageArray, TextProcessingSettings textProcessingSettings, Color[][] colors = null)
        {
            this.strings = imageArray;

            this.textProcessingSettings = textProcessingSettings;

            this.useColor = colors != null;

            if (this.useColor && textProcessingSettings.IsFixedWidth)
            {
                this.colors = colors;

                this.SetupColorArrays();
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Creates the HTML.
        /// </summary>
        /// <returns>A string containing the HTML file.</returns>
        public string CreateHtml()
        {
            Color backgroundColor = this.textProcessingSettings.IsBlackTextOnWhite ? Color.White : Color.Black;

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            builder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\">");
            builder.AppendLine(string.Empty);
            builder.AppendLine("<head>");
            builder.AppendLine(string.Empty);

            if (this.Title.Length > 0)
            {
                builder.Append("<title>");
                builder.Append(this.Title);
                builder.Append("</title>");
                builder.Append(Environment.NewLine);

                builder.AppendLine(string.Empty);
            }

            builder.Append("<meta name=\"generator\" content=\"Ascgen dotNET ");
            builder.Append(AscgenVersion.ToString());
            builder.Append("\" />");
            builder.Append(Environment.NewLine);

            builder.AppendLine(string.Empty);

            builder.AppendLine("<style type=\"text/css\">");
            builder.AppendLine("<!--");

            builder.AppendLine("#ascgen-image pre {");
            builder.Append("	font-family: \"");
            builder.Append(this.textProcessingSettings.Font.Name);
            builder.Append("\", monospace;");
            builder.Append(Environment.NewLine);

            builder.Append("	font-size: ");
            builder.Append(this.textProcessingSettings.Font.Size);
            builder.Append("pt;");
            builder.Append(Environment.NewLine);

            builder.Append("	background-color: #");
            builder.Append(backgroundColor.R.ToString("X2", null));
            builder.Append(backgroundColor.G.ToString("X2", null));
            builder.Append(backgroundColor.B.ToString("X2", null));
            builder.Append(";");
            builder.Append(Environment.NewLine);

            Color forecolor = Color.FromArgb(
                                        (byte)(~backgroundColor.R),
                                        (byte)(~backgroundColor.G),
                                        (byte)(~backgroundColor.B));

            builder.Append("	color: #");
            builder.Append(forecolor.R.ToString("X2", null));
            builder.Append(forecolor.G.ToString("X2", null));
            builder.Append(forecolor.B.ToString("X2", null));
            builder.Append(";");
            builder.Append(Environment.NewLine);

            builder.AppendLine("	float: left;");     // avoids firefox problem with scrolling horizontally
            builder.Append("	line-height: ");  // fixes firefox problem with extra space between lines
            builder.Append(this.textProcessingSettings.CharacterSize.Height);
            builder.Append("px;");
            builder.Append(Environment.NewLine);

            builder.AppendLine("	border: 1px solid #000000;");

            builder.AppendLine("}");

            if (this.useColor)
            {
                builder.Append(Environment.NewLine);

                int count = 0;

                foreach (Color c in this.uniqueColors)
                {
                    builder.Append(".c");
                    builder.Append(count++);
                    builder.Append(" { color: #");
                    builder.Append(c.R.ToString("X2", null));
                    builder.Append(c.G.ToString("X2", null));
                    builder.Append(c.B.ToString("X2", null));
                    builder.Append("; }");
                    builder.Append(Environment.NewLine);
                }
            }

            builder.AppendLine("-->");
            builder.AppendLine("</style>");
            builder.AppendLine(string.Empty);
            builder.AppendLine("</head>");
            builder.AppendLine(string.Empty);
            builder.AppendLine("<body>");
            builder.AppendLine(string.Empty);
            builder.AppendLine("<div id=\"ascgen-image\">");
            builder.Append("<pre>");

            bool spanIsOpen = false;

            // the text
            if (this.textProcessingSettings.IsFixedWidth)
            {
                for (int y = 0; y < this.textProcessingSettings.Height; y++)
                {
                    for (int x = 0; x < this.textProcessingSettings.Width; x++)
                    {
                        if (this.useColor && this.characterToColor[y][x] != -1)
                        {
                            if (spanIsOpen)
                            {
                                builder.Append("</span>");
                            }

                            builder.Append("<span class=\"c");
                            builder.Append(this.characterToColor[y][x]);
                            builder.Append("\">");
                            spanIsOpen = true;
                        }

                        builder.Append(this.strings[y][x]);
                    }

                    if (y < this.textProcessingSettings.Height - 1)
                    {
                        builder.Append(Environment.NewLine);
                    }
                }
            }
            else
            {
                foreach (string s in this.strings)
                {
                    builder.Append(s);
                    builder.Append(Environment.NewLine);
                }
            }

            if (this.useColor)
            {
                builder.Append("</span>");
            }

            builder.Append("</pre>");
            builder.Append(Environment.NewLine);
            builder.AppendLine("</div>");
            builder.AppendLine(string.Empty);
            builder.AppendLine("</body>");
            builder.AppendLine(string.Empty);
            builder.Append("</html>");

            return builder.ToString();
        }

        /// <summary>
        /// Creates the RTF.
        /// </summary>
        /// <returns>a string containing the RTF file.</returns>
        public string CreateRtf()
        {
            StringBuilder builder = new StringBuilder();

            // the rtf header
            builder.Append(@"{\rtf1\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 ");
            builder.Append(this.textProcessingSettings.Font.Name);
            builder.Append(";}}");
            builder.Append(Environment.NewLine);

            if (this.useColor)
            {
                // the rtf colortbl
                builder.Append(@"{\colortbl ;");

                foreach (Color c in this.uniqueColors)
                {
                    builder.Append(@"\red");
                    builder.Append(c.R);
                    builder.Append(@"\green");
                    builder.Append(c.G);
                    builder.Append(@"\blue");
                    builder.Append(c.B);
                    builder.Append(";");
                }

                builder.Append("}");
                builder.Append(Environment.NewLine);
            }

            builder.Append(@"{\*\generator ");
            builder.Append(AscgenVersion.ProgramName);
            builder.Append(" ");
            builder.Append(AscgenVersion.ToString());
            builder.Append(";}");

            // the font settings
            builder.Append(@"\viewkind4\uc1\pard\lang2057\f0");

            if (this.textProcessingSettings.Font.Bold)
            {
                builder.Append(@"\b");
            }

            if (this.textProcessingSettings.Font.Italic)
            {
                builder.Append(@"\i");
            }

            if (this.textProcessingSettings.Font.Underline)
            {
                builder.Append(@"\ul");
            }

            if (this.textProcessingSettings.Font.Strikeout)
            {
                builder.Append(@"\strike");
            }

            builder.Append(@"\fs");
            builder.Append((int)(this.textProcessingSettings.Font.Size * 2));
            builder.Append(Environment.NewLine);

            // the text
            for (int y = 0; y < this.textProcessingSettings.Height; y++)
            {
                for (int x = 0; x < this.textProcessingSettings.Width; x++)
                {
                    if (this.useColor && this.characterToColor[y][x] != -1)
                    {
                        builder.Append(@"\cf");
                        builder.Append(this.characterToColor[y][x] + 1);
                        builder.Append(" ");
                    }

                    builder.Append(this.strings[y][x]);
                }

                builder.Append(@"\par");
                builder.Append(Environment.NewLine);
            }

            builder.Append("}");

            return builder.ToString();
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Sets up the color arrays.
        /// </summary>
        private void SetupColorArrays()
        {
            if (this.colors == null)
            {
                return;
            }

            this.uniqueColors = new ArrayList();

            this.characterToColor = new int[this.colors.Length][];

            int previousColorId = -1;

            for (int y = 0; y < this.colors.Length; y++)
            {
                this.characterToColor[y] = new int[this.colors[0].Length];

                for (int x = 0; x < this.colors[0].Length; x++)
                {
                    // check if the colour is already in the unique array
                    int colorId = this.uniqueColors.IndexOf(this.colors[y][x]);

                    if (colorId > -1)
                    {
                        // if color is the same as the previous one
                        if (colorId == previousColorId)
                        {
                            this.characterToColor[y][x] = -1;
                        }
                        else
                        {
                            // save the id of the color to the array
                            previousColorId = this.characterToColor[y][x] = colorId;
                        }
                    }
                    else
                    {
                        // Add the new Color and save the id
                        previousColorId = this.characterToColor[y][x] = this.uniqueColors.Add(this.colors[y][x]);
                    }
                }
            }
        }

        #endregion Private methods
    }
}