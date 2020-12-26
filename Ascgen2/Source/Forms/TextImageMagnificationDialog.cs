//---------------------------------------------------------------------------------------
// <copyright file="TextImageMagnificationDialog.cs" company="Jonathan Mathews Software">
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
    using System.Drawing;
    using System.Drawing.Imaging;
    using JMSoftware.TextHelper;

    /// <summary>
    /// Form to get the text magnification level
    /// </summary>
    public partial class TextImageMagnificationDialog
    {
        #region Fields

        /// <summary>
        /// Color of the texts background
        /// </summary>
        private Color backgroundColor;

        /// <summary>
        /// Do we need to recreate the image?
        /// </summary>
        private bool updateImage;

        /// <summary>
        /// Size of the input image
        /// </summary>
        private Size inputSize;

        /// <summary>
        /// Size of the output image
        /// </summary>
        private Size outputSize;

        /// <summary>
        /// Color of the text
        /// </summary>
        private Color textColor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextImageMagnificationDialog"/> class.
        /// </summary>
        /// <param name="font">The font to be used.</param>
        public TextImageMagnificationDialog(Font font)
        {
            this.inputSize = new Size(0, 0);
            this.outputSize = new Size(0, 0);

            this.textColor = Color.Black;
            this.backgroundColor = Color.White;

            this.updateImage = true;

            this.InitializeComponent();

            this.tbxSampleText.Font = font;

            this.UpdateUI();

            this.UpdateImage();

            this.tbxMagnification.Text = ((int)this.Value).ToString(Variables.Instance.Culture);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the color used for the background
        /// </summary>
        /// <value>The color of the background.</value>
        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }

            set
            {
                this.backgroundColor = this.tbxSampleText.BackColor = value;

                this.updateImage = true;

                this.UpdateImage();
            }
        }

        /// <summary>
        /// Gets or sets the size of the input image
        /// </summary>
        /// <value>The size of the input.</value>
        public Size InputSize
        {
            get
            {
                return this.inputSize;
            }

            set
            {
                this.inputSize = value;

                this.UpdateOutputSize();
            }
        }

        /// <summary>
        /// Gets or sets the color used for the text
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get
            {
                return this.textColor;
            }

            set
            {
                this.textColor = this.tbxSampleText.ForeColor = value;

                this.updateImage = true;

                this.UpdateImage();
            }
        }

        /// <summary>
        /// Gets or sets the Font to be used
        /// </summary>
        /// <value>The text font.</value>
        public Font TextFont
        {
            get
            {
                return this.tbxSampleText.Font;
            }

            set
            {
                this.tbxSampleText.Font = value;

                this.UpdateImage();
            }
        }

        /// <summary>
        /// Gets or sets the zoom value (25 -&gt; 100)
        /// </summary>
        /// <value>The value.</value>
        public float Value
        {
            get
            {
                return (float)this.trkMagnification.Value / 10f;
            }

            set
            {
                this.trkMagnification.Value = (int)(value * 10f);
            }
        }

        #endregion Properties

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
        /// Handles the TextChanged event of the tbxSampleText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TbxSampleText_TextChanged(object sender, System.EventArgs e)
        {
            this.updateImage = true;

            this.UpdateImage();
        }

        /// <summary>
        /// Handles the ValueChanged event of the trkMagnification control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TrkMagnification_ValueChanged(object sender, System.EventArgs e)
        {
            this.updateImage = true;

            this.UpdateImage();

            this.tbxMagnification.Text = ((int)this.Value).ToString(Variables.Instance.Culture);

            this.UpdateOutputSize();
        }

        /// <summary>
        /// Create a new image for the picturebox
        /// </summary>
        private void UpdateImage()
        {
            if (!this.updateImage || this.tbxSampleText.Lines.Length == 0)
            {
                return;
            }

            using (Bitmap bmpFullSize = TextToImage.Convert(this.tbxSampleText.Text, this.tbxSampleText.Font, this.TextColor, this.BackgroundColor))
            {
                if (bmpFullSize == null)
                {
                    return;
                }

                float magnification = this.Value / 100f;

                Size newSize = new Size(
                                    (int)(((float)bmpFullSize.Width * magnification) + 0.5f),
                                    (int)(((float)bmpFullSize.Height * magnification) + 0.5f));

                this.pbxOutputImage.Image = new Bitmap(newSize.Width, newSize.Height);

                using (ImageAttributes ia = new ImageAttributes())
                {
                    ia.SetColorMatrix(JMSoftware.Matrices.Grayscale());

                    using (Graphics g = Graphics.FromImage(this.pbxOutputImage.Image))
                    {
                        // select highest quality resize mode
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        g.DrawImage(
                                bmpFullSize,
                                new Rectangle(0, 0, newSize.Width, newSize.Height),
                                0,
                                0,
                                bmpFullSize.Width,
                                bmpFullSize.Height,
                                GraphicsUnit.Pixel,
                                ia);
                    }
                }
            }

            this.updateImage = false;

            this.pbxOutputImage.Refresh();
        }

        /// <summary>
        /// Updates the size of the output.
        /// </summary>
        private void UpdateOutputSize()
        {
            if (this.InputSize.Width < 1 || this.InputSize.Height < 1)
            {
                return;
            }

            float ratio = (float)this.Value / 100f;

            this.outputSize.Width = (int)(((float)this.InputSize.Width * ratio) + 0.5f);
            this.outputSize.Height = (int)(((float)this.InputSize.Height * ratio) + 0.5f);

            this.tbxOutputWidth.Text = this.outputSize.Width.ToString(Variables.Instance.Culture);
            this.tbxOutputHeight.Text = this.outputSize.Height.ToString(Variables.Instance.Culture);

            this.tbxOutputWidth.Refresh();
            this.tbxOutputHeight.Refresh();
        }

        /// <summary>
        /// Update the form with the text strings for the current language
        /// </summary>
        private void UpdateUI()
        {
            this.Text = Resource.GetString("Choose the Magnification Level");

            this.tabPageInput.Text = Resource.GetString("Input Text Sample");
            this.tabPageOutput.Text = Resource.GetString("Output Image Sample");
            this.lblMagnification.Text = Resource.GetString("Magnification") + " (%):";
            this.btnOk.Text = Resource.GetString("&Ok");
            this.btnCancel.Text = Resource.GetString("&Cancel");
            this.lblOutputSize.Text = Resource.GetString("Output Size") + ":";

            string text = Resource.GetString("For The Best Effect This Text Should Be Readable");

            this.tbxSampleText.Lines = new[]
                                            {
                                                text,
                                                text.ToLower(Variables.Instance.Culture),
                                                text.ToUpper(Variables.Instance.Culture)
                                            };
        }

        #endregion Private methods
    }
}