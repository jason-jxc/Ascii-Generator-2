//---------------------------------------------------------------------------------------
// <copyright file="FormConvertImage.Designer.cs" company="Jonathan Mathews Software">
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
    using System.Windows.Forms;
    using JMSoftware.Controls;

    /// <summary>
    /// Main form for the program
    /// </summary>
    partial class FormConvertImage : Form
    {
        #region Fields

        private ToolStripButton cbxLocked;

        private ToolStripButton chkGenerate;

        private ToolStripComboBox cmbRamp;

        private ToolStripSeparator cmenuHelpSep1;

        private ToolStripSeparator cmenuHelpSep2;

        private ToolStripSeparator cmenuHelpSep3;

        private ToolStripMenuItem cmenuTextCopy;

        private ToolStripMenuItem cmenuTextFont;

        private ToolStripMenuItem cmenuTextHorizontal;

        private ToolStripMenuItem cmenuTextSelectAll;

        private ToolStripMenuItem cmenuTextSelectNone;

        private ToolStripSeparator cmenuTextSeperator1;

        private ToolStripSeparator cmenuTextSeperator2;

        private ToolStripSeparator cmenuTextSeperator3;

        private ToolStripSeparator cmenuTextSeperator4;

        private ToolStripSeparator cmenuTextSeperator5;

        private ToolStripMenuItem cmenuTextSharpening;

        private ToolStripMenuItem cmenuTextSharpeningNone;

        private ToolStripMenuItem cmenuTextSharpeningSharpen;

        private ToolStripMenuItem cmenuTextSharpeningUnsharp;

        private ToolStripMenuItem cmenuTextStretch;

        private ToolStripMenuItem cmenuTextVertical;

        private System.ComponentModel.IContainer components;

        private ContextMenuStrip contextMenuText;

        private FontDialog dialogChooseFont;

        private OpenFileDialog dialogLoadImage;

        private SaveFileDialog dialogSaveColour;

        private SaveFileDialog dialogSaveImage;

        private SaveFileDialog dialogSaveText;

        private ColorDialog dialogSelectionColor;

        private ToolStripLabel lblRamp;

        private MenuStrip mainMenu1;

        private ToolStripMenuItem menuEdit;

        private ToolStripMenuItem menuEditEditSettings;

        private ToolStripMenuItem menuEditFlipHorizontal;

        private ToolStripMenuItem menuEditFlipVertical;

        private ToolStripMenuItem menuEditFonts;

        private ToolStripMenuItem menuEditFontsFont;

        private ToolStripMenuItem menuEditFontsSpecifyCharSize;

        private ToolStripMenuItem menuEditInput;

        private ToolStripMenuItem menuEditInputFlipHorizontal;

        private ToolStripMenuItem menuEditInputFlipVertical;

        private ToolStripMenuItem menuEditInputRotate180;

        private ToolStripMenuItem menuEditInputRotate270;

        private ToolStripMenuItem menuEditInputRotate90;

        private ToolStripMenuItem menuEditOutput;

        private ToolStripSeparator menuEditOutputSep1;

        private ToolStripSeparator menuEditOutputSep2;

        private ToolStripMenuItem menuEditRamps;

        private ToolStripMenuItem menuEditRampsCopyRamp;

        private ToolStripMenuItem menuEditRampsValidChars;

        private ToolStripSeparator menuEditSep2;

        private ToolStripMenuItem menuEditSharpeningMethod;

        private ToolStripMenuItem menuEditSharpeningMethodNone;

        private ToolStripMenuItem menuEditSharpeningMethodSharpen;

        private ToolStripMenuItem menuEditSharpeningMethodUnsharp;

        private ToolStripMenuItem menuEditStretch;

        private ToolStripMenuItem menuFile;

        private ToolStripMenuItem menuFileBatchConversion;

        private ToolStripMenuItem menuFileClose;

        private ToolStripMenuItem menuFileExit;

        private ToolStripMenuItem menuFileImportClipboard;

        private ToolStripMenuItem menuFileLoad;

        private ToolStripMenuItem menuFilePageSetup;

        private ToolStripMenuItem menuFilePrint;

        private ToolStripMenuItem menuFilePrintColour;

        private ToolStripMenuItem menuFilePrintPreview;

        private ToolStripMenuItem menuFilePrintPreviewColour;

        private ToolStripMenuItem menuFileSaveAs;

        private ToolStripSeparator menuFileSep1;

        private ToolStripSeparator menuFileSep2;

        private ToolStripSeparator menuFileSep3;

        private ToolStripSeparator menuFileSep5;

        private ToolStripMenuItem menuHelp;

        private ToolStripMenuItem menuHelpAbout;

        private ToolStripMenuItem menuHelpCheckForNewVersionToolStrip;

        private ToolStripMenuItem menuHelpDonate;

        private ToolStripMenuItem menuHelpReportBug;

        private ToolStripMenuItem menuHelpRequestFeature;

        private ToolStripMenuItem menuHelpTutorials;

        private ToolStripMenuItem menuView;

        private ToolStripMenuItem menuViewColourPreview;

        private ToolStripMenuItem menuViewFullScreen;

        private ToolStripSeparator menuViewSep1;

        private ToolStripSeparator menuViewSep2;

        private ToolStripMenuItem menuViewText;

        private PageSetupDialog pageSetupDialog;

        private Panel pnlMain;

        private PrintDialog printDialog;

        private System.Drawing.Printing.PrintDocument printDocument;

        private PrintPreviewDialog printPreviewDialog;

        private TextViewerRichTextBox rtbxConvertedText;

        private ToolStripTextBox tbxHeight;

        private ToolStripTextBox tbxWidth;

        private ToolStripContainer toolStripContainer1;

        private ToolStripSeparator toolStripSeparator1;

        private ToolStripButton tsbFlipHorizontally;

        private ToolStripButton tsbFlipVertically;

        private ToolStripButton tsbFont;

        private ToolStripButton tsbRotateAnticlockwise;

        private ToolStripButton tsbRotateClockwise;

        private ToolStrip toolStripRotateFlip;

        private ToolStrip toolStripFont;

        private ToolStrip toolStripCharacters;

        private ToolStrip toolStripOutputSize;

        private ToolStrip toolStripRamp;

        #endregion Fields

        #region Private methods

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConvertImage));
            this.dialogLoadImage = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmenuTextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuTextSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuTextStretch = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSeperator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuTextSharpening = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSharpeningNone = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSharpeningSharpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSharpeningUnsharp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSeperator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuTextFont = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextSeperator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmenuTextHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuTextVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogSaveText = new System.Windows.Forms.SaveFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileImportClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileBatchConversion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePrintColour = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePrintPreviewColour = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditFonts = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditFontsFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditFontsSpecifyCharSize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditRamps = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditRampsValidChars = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditRampsCopyRamp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInputRotate90 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInputRotate180 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInputRotate270 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditInputFlipHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInputFlipVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInvertOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditStretch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditOutputSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditSharpeningMethod = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSharpeningMethodNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSharpeningMethodSharpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSharpeningMethodUnsharp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditOutputSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditFlipHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditFlipVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditEditSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShowImage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuViewColourPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpTutorials = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFAQ = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpRequestFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpReportBug = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuHelpSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuHelpSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpCheckForNewVersionToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogSaveImage = new System.Windows.Forms.SaveFileDialog();
            this.dialogChooseFont = new System.Windows.Forms.FontDialog();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.rtbxConvertedText = new JMSoftware.Controls.TextViewerRichTextBox();
            this.toolStripOutputSize = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSize = new System.Windows.Forms.ToolStripLabel();
            this.tbxWidth = new System.Windows.Forms.ToolStripTextBox();
            this.cbxLocked = new System.Windows.Forms.ToolStripButton();
            this.tbxHeight = new System.Windows.Forms.ToolStripTextBox();
            this.dialogSelectionColor = new System.Windows.Forms.ColorDialog();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripRotateFlip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBlackOnWhite = new System.Windows.Forms.ToolStripButton();
            this.tsbRotateAnticlockwise = new System.Windows.Forms.ToolStripButton();
            this.tsbRotateClockwise = new System.Windows.Forms.ToolStripButton();
            this.tsbFlipHorizontally = new System.Windows.Forms.ToolStripButton();
            this.tsbFlipVertically = new System.Windows.Forms.ToolStripButton();
            this.toolStripFont = new System.Windows.Forms.ToolStrip();
            this.tsbFont = new System.Windows.Forms.ToolStripButton();
            this.toolStripWidgets = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonShowTextWidget = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowImageWidget = new System.Windows.Forms.ToolStripButton();
            this.toolStripRamp = new System.Windows.Forms.ToolStrip();
            this.lblRamp = new System.Windows.Forms.ToolStripLabel();
            this.cmbRamp = new System.Windows.Forms.ToolStripComboBox();
            this.chkGenerate = new System.Windows.Forms.ToolStripButton();
            this.toolStripDisplay = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFullScreen = new System.Windows.Forms.ToolStripButton();
            this.toolStripFile = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripCharacters = new System.Windows.Forms.ToolStrip();
            this.lblCharacters = new System.Windows.Forms.ToolStripLabel();
            this.cmbCharacters = new System.Windows.Forms.ToolStripComboBox();
            this.dialogSaveColour = new System.Windows.Forms.SaveFileDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.contextMenuText.SuspendLayout();
            this.mainMenu1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.toolStripOutputSize.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripRotateFlip.SuspendLayout();
            this.toolStripFont.SuspendLayout();
            this.toolStripWidgets.SuspendLayout();
            this.toolStripRamp.SuspendLayout();
            this.toolStripDisplay.SuspendLayout();
            this.toolStripFile.SuspendLayout();
            this.toolStripCharacters.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuText
            // 
            this.contextMenuText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuTextCopy,
            this.cmenuTextSeperator1,
            this.cmenuTextSelectAll,
            this.cmenuTextSelectNone,
            this.cmenuTextSeperator2,
            this.cmenuTextStretch,
            this.cmenuTextSeperator3,
            this.cmenuTextSharpening,
            this.cmenuTextSeperator4,
            this.cmenuTextFont,
            this.cmenuTextSeperator5,
            this.cmenuTextHorizontal,
            this.cmenuTextVertical});
            this.contextMenuText.Name = "contextMenuText";
            this.contextMenuText.Size = new System.Drawing.Size(236, 210);
            this.contextMenuText.Opened += new System.EventHandler(this.ContextMenuText_Popup);
            // 
            // cmenuTextCopy
            // 
            this.cmenuTextCopy.MergeIndex = 0;
            this.cmenuTextCopy.Name = "cmenuTextCopy";
            this.cmenuTextCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.cmenuTextCopy.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextCopy.Text = "cmenuTextCopy";
            this.cmenuTextCopy.Click += new System.EventHandler(this.CmenuCopy_Click);
            // 
            // cmenuTextSeperator1
            // 
            this.cmenuTextSeperator1.MergeIndex = 1;
            this.cmenuTextSeperator1.Name = "cmenuTextSeperator1";
            this.cmenuTextSeperator1.Size = new System.Drawing.Size(232, 6);
            // 
            // cmenuTextSelectAll
            // 
            this.cmenuTextSelectAll.MergeIndex = 2;
            this.cmenuTextSelectAll.Name = "cmenuTextSelectAll";
            this.cmenuTextSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.cmenuTextSelectAll.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextSelectAll.Text = "cmenuTextSelectAll";
            this.cmenuTextSelectAll.Click += new System.EventHandler(this.CmenuSelectAll_Click);
            // 
            // cmenuTextSelectNone
            // 
            this.cmenuTextSelectNone.MergeIndex = 3;
            this.cmenuTextSelectNone.Name = "cmenuTextSelectNone";
            this.cmenuTextSelectNone.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.cmenuTextSelectNone.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextSelectNone.Text = "cmenuTextSelectNone";
            this.cmenuTextSelectNone.Click += new System.EventHandler(this.CmenuSelectNone_Click);
            // 
            // cmenuTextSeperator2
            // 
            this.cmenuTextSeperator2.MergeIndex = 4;
            this.cmenuTextSeperator2.Name = "cmenuTextSeperator2";
            this.cmenuTextSeperator2.Size = new System.Drawing.Size(232, 6);
            // 
            // cmenuTextStretch
            // 
            this.cmenuTextStretch.MergeIndex = 5;
            this.cmenuTextStretch.Name = "cmenuTextStretch";
            this.cmenuTextStretch.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextStretch.Text = "cmenuTextStretch";
            this.cmenuTextStretch.Click += new System.EventHandler(this.CmenuTextStretch_Click);
            // 
            // cmenuTextSeperator3
            // 
            this.cmenuTextSeperator3.MergeIndex = 6;
            this.cmenuTextSeperator3.Name = "cmenuTextSeperator3";
            this.cmenuTextSeperator3.Size = new System.Drawing.Size(232, 6);
            // 
            // cmenuTextSharpening
            // 
            this.cmenuTextSharpening.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmenuTextSharpeningNone,
            this.cmenuTextSharpeningSharpen,
            this.cmenuTextSharpeningUnsharp});
            this.cmenuTextSharpening.MergeIndex = 7;
            this.cmenuTextSharpening.Name = "cmenuTextSharpening";
            this.cmenuTextSharpening.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextSharpening.Text = "cmenuTextSharpening";
            // 
            // cmenuTextSharpeningNone
            // 
            this.cmenuTextSharpeningNone.MergeIndex = 0;
            this.cmenuTextSharpeningNone.Name = "cmenuTextSharpeningNone";
            this.cmenuTextSharpeningNone.Size = new System.Drawing.Size(237, 22);
            this.cmenuTextSharpeningNone.Text = "cmenuTextSharpeningNone";
            this.cmenuTextSharpeningNone.Click += new System.EventHandler(this.CmenuTextSharpeningNone_Click);
            // 
            // cmenuTextSharpeningSharpen
            // 
            this.cmenuTextSharpeningSharpen.MergeIndex = 1;
            this.cmenuTextSharpeningSharpen.Name = "cmenuTextSharpeningSharpen";
            this.cmenuTextSharpeningSharpen.Size = new System.Drawing.Size(237, 22);
            this.cmenuTextSharpeningSharpen.Text = "cmenuTextSharpeningSharpen";
            this.cmenuTextSharpeningSharpen.Click += new System.EventHandler(this.CmenuTextSharpeningSharpen_Click);
            // 
            // cmenuTextSharpeningUnsharp
            // 
            this.cmenuTextSharpeningUnsharp.MergeIndex = 2;
            this.cmenuTextSharpeningUnsharp.Name = "cmenuTextSharpeningUnsharp";
            this.cmenuTextSharpeningUnsharp.Size = new System.Drawing.Size(237, 22);
            this.cmenuTextSharpeningUnsharp.Text = "cmenuTextSharpeningUnsharp";
            this.cmenuTextSharpeningUnsharp.Click += new System.EventHandler(this.CmenuTextSharpeningUnsharp_Click);
            // 
            // cmenuTextSeperator4
            // 
            this.cmenuTextSeperator4.MergeIndex = 8;
            this.cmenuTextSeperator4.Name = "cmenuTextSeperator4";
            this.cmenuTextSeperator4.Size = new System.Drawing.Size(232, 6);
            // 
            // cmenuTextFont
            // 
            this.cmenuTextFont.Image = global::AscGenDotNet.Properties.Resources.font;
            this.cmenuTextFont.MergeIndex = 9;
            this.cmenuTextFont.Name = "cmenuTextFont";
            this.cmenuTextFont.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextFont.Text = "cmenuTextFont";
            this.cmenuTextFont.Click += new System.EventHandler(this.CmenuTextFont_Click);
            // 
            // cmenuTextSeperator5
            // 
            this.cmenuTextSeperator5.MergeIndex = 10;
            this.cmenuTextSeperator5.Name = "cmenuTextSeperator5";
            this.cmenuTextSeperator5.Size = new System.Drawing.Size(232, 6);
            // 
            // cmenuTextHorizontal
            // 
            this.cmenuTextHorizontal.Image = global::AscGenDotNet.Properties.Resources.shape_flip_horizontal;
            this.cmenuTextHorizontal.MergeIndex = 11;
            this.cmenuTextHorizontal.Name = "cmenuTextHorizontal";
            this.cmenuTextHorizontal.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextHorizontal.Text = "cmenuTextHorizontal";
            this.cmenuTextHorizontal.Click += new System.EventHandler(this.CmenuTextHorizontal_Click);
            // 
            // cmenuTextVertical
            // 
            this.cmenuTextVertical.Image = global::AscGenDotNet.Properties.Resources.shape_flip_vertical;
            this.cmenuTextVertical.MergeIndex = 12;
            this.cmenuTextVertical.Name = "cmenuTextVertical";
            this.cmenuTextVertical.Size = new System.Drawing.Size(235, 22);
            this.cmenuTextVertical.Text = "cmenuTextVertical";
            this.cmenuTextVertical.Click += new System.EventHandler(this.CmenuTextVertical_Click);
            // 
            // dialogSaveText
            // 
            this.dialogSaveText.DefaultExt = "txt";
            // 
            // mainMenu1
            // 
            this.mainMenu1.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenu1.GripMargin = new System.Windows.Forms.Padding(2);
            this.mainMenu1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.mainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuView,
            this.menuHelp});
            this.mainMenu1.Location = new System.Drawing.Point(0, 0);
            this.mainMenu1.Name = "mainMenu1";
            this.mainMenu1.Size = new System.Drawing.Size(624, 24);
            this.mainMenu1.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileLoad,
            this.menuFileImportClipboard,
            this.menuFileClose,
            this.menuFileSep1,
            this.menuFileBatchConversion,
            this.menuFileSep2,
            this.menuFilePrint,
            this.menuFilePrintPreview,
            this.menuFilePrintColour,
            this.menuFilePrintPreviewColour,
            this.menuFilePageSetup,
            this.menuFileSep3,
            this.menuFileSaveAs,
            this.menuFileSep5,
            this.menuFileExit});
            this.menuFile.MergeIndex = 0;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(68, 20);
            this.menuFile.Text = "menuFile";
            this.menuFile.DropDownOpening += new System.EventHandler(this.MenuFile_Popup);
            // 
            // menuFileLoad
            // 
            this.menuFileLoad.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.menuFileLoad.MergeIndex = 0;
            this.menuFileLoad.Name = "menuFileLoad";
            this.menuFileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuFileLoad.Size = new System.Drawing.Size(254, 22);
            this.menuFileLoad.Text = "menuFileLoad";
            this.menuFileLoad.Click += new System.EventHandler(this.MenuFileLoad_Click);
            // 
            // menuFileImportClipboard
            // 
            this.menuFileImportClipboard.Image = global::AscGenDotNet.Properties.Resources.page_paste;
            this.menuFileImportClipboard.ImageTransparentColor = System.Drawing.Color.White;
            this.menuFileImportClipboard.MergeIndex = 1;
            this.menuFileImportClipboard.Name = "menuFileImportClipboard";
            this.menuFileImportClipboard.Size = new System.Drawing.Size(254, 22);
            this.menuFileImportClipboard.Text = "menuFileImportClipboard";
            this.menuFileImportClipboard.Click += new System.EventHandler(this.MenuFileImportClipboard_Click);
            // 
            // menuFileClose
            // 
            this.menuFileClose.Image = global::AscGenDotNet.Properties.Resources.cancel;
            this.menuFileClose.MergeIndex = 2;
            this.menuFileClose.Name = "menuFileClose";
            this.menuFileClose.Size = new System.Drawing.Size(254, 22);
            this.menuFileClose.Text = "menuFileClose";
            this.menuFileClose.Click += new System.EventHandler(this.MenuFileClose_Click);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(251, 6);
            // 
            // menuFileBatchConversion
            // 
            this.menuFileBatchConversion.Image = global::AscGenDotNet.Properties.Resources.images;
            this.menuFileBatchConversion.ImageTransparentColor = System.Drawing.Color.White;
            this.menuFileBatchConversion.MergeIndex = 4;
            this.menuFileBatchConversion.Name = "menuFileBatchConversion";
            this.menuFileBatchConversion.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuFileBatchConversion.Size = new System.Drawing.Size(254, 22);
            this.menuFileBatchConversion.Text = "menuFileBatchConversion";
            this.menuFileBatchConversion.Click += new System.EventHandler(this.MenuFileBatchConversion_Click);
            // 
            // menuFileSep2
            // 
            this.menuFileSep2.Name = "menuFileSep2";
            this.menuFileSep2.Size = new System.Drawing.Size(251, 6);
            // 
            // menuFilePrint
            // 
            this.menuFilePrint.Image = global::AscGenDotNet.Properties.Resources.printer;
            this.menuFilePrint.Name = "menuFilePrint";
            this.menuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuFilePrint.Size = new System.Drawing.Size(254, 22);
            this.menuFilePrint.Text = "menuFilePrint";
            this.menuFilePrint.Click += new System.EventHandler(this.MenuFilePrint_Click);
            // 
            // menuFilePrintPreview
            // 
            this.menuFilePrintPreview.Image = global::AscGenDotNet.Properties.Resources.page_white_magnify;
            this.menuFilePrintPreview.Name = "menuFilePrintPreview";
            this.menuFilePrintPreview.Size = new System.Drawing.Size(254, 22);
            this.menuFilePrintPreview.Text = "menuFilePrintPreview";
            this.menuFilePrintPreview.Click += new System.EventHandler(this.MenuFilePrintPreview_Click);
            // 
            // menuFilePrintColour
            // 
            this.menuFilePrintColour.Image = global::AscGenDotNet.Properties.Resources.printercolor;
            this.menuFilePrintColour.Name = "menuFilePrintColour";
            this.menuFilePrintColour.Size = new System.Drawing.Size(254, 22);
            this.menuFilePrintColour.Text = "menuFilePrintColour";
            this.menuFilePrintColour.Click += new System.EventHandler(this.MenuFilePrintColour_Click);
            // 
            // menuFilePrintPreviewColour
            // 
            this.menuFilePrintPreviewColour.Image = global::AscGenDotNet.Properties.Resources.page_magnify_color;
            this.menuFilePrintPreviewColour.Name = "menuFilePrintPreviewColour";
            this.menuFilePrintPreviewColour.Size = new System.Drawing.Size(254, 22);
            this.menuFilePrintPreviewColour.Text = "menuFilePrintPreviewColour";
            this.menuFilePrintPreviewColour.Click += new System.EventHandler(this.MenuFilePrintPreviewColour_Click);
            // 
            // menuFilePageSetup
            // 
            this.menuFilePageSetup.Image = global::AscGenDotNet.Properties.Resources.page_white_wrench;
            this.menuFilePageSetup.Name = "menuFilePageSetup";
            this.menuFilePageSetup.Size = new System.Drawing.Size(254, 22);
            this.menuFilePageSetup.Text = "menuFilePageSetup";
            this.menuFilePageSetup.Click += new System.EventHandler(this.MenuFilePageSetup_Click);
            // 
            // menuFileSep3
            // 
            this.menuFileSep3.Name = "menuFileSep3";
            this.menuFileSep3.Size = new System.Drawing.Size(251, 6);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Image = global::AscGenDotNet.Properties.Resources.disk;
            this.menuFileSaveAs.ImageTransparentColor = System.Drawing.Color.White;
            this.menuFileSaveAs.MergeIndex = 6;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSaveAs.Size = new System.Drawing.Size(254, 22);
            this.menuFileSaveAs.Text = "menuFileSaveAs";
            this.menuFileSaveAs.Click += new System.EventHandler(this.MenuFileSave_Click);
            // 
            // menuFileSep5
            // 
            this.menuFileSep5.Name = "menuFileSep5";
            this.menuFileSep5.Size = new System.Drawing.Size(251, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Image = global::AscGenDotNet.Properties.Resources.door_open;
            this.menuFileExit.MergeIndex = 9;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuFileExit.Size = new System.Drawing.Size(254, 22);
            this.menuFileExit.Text = "menuFileExit";
            this.menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditFonts,
            this.menuEditRamps,
            this.menuEditInput,
            this.menuEditOutput,
            this.menuEditSep2,
            this.menuEditEditSettings});
            this.menuEdit.MergeIndex = 1;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(70, 20);
            this.menuEdit.Text = "menuEdit";
            this.menuEdit.DropDownOpening += new System.EventHandler(this.MenuEdit_Popup);
            // 
            // menuEditFonts
            // 
            this.menuEditFonts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditFontsFont,
            this.menuEditFontsSpecifyCharSize});
            this.menuEditFonts.Name = "menuEditFonts";
            this.menuEditFonts.Size = new System.Drawing.Size(187, 22);
            this.menuEditFonts.Text = "menuEditFonts";
            // 
            // menuEditFontsFont
            // 
            this.menuEditFontsFont.Image = global::AscGenDotNet.Properties.Resources.font;
            this.menuEditFontsFont.Name = "menuEditFontsFont";
            this.menuEditFontsFont.Size = new System.Drawing.Size(237, 22);
            this.menuEditFontsFont.Text = "menuEditFontsFont";
            this.menuEditFontsFont.Click += new System.EventHandler(this.MenuEditFont_Click);
            // 
            // menuEditFontsSpecifyCharSize
            // 
            this.menuEditFontsSpecifyCharSize.Image = global::AscGenDotNet.Properties.Resources.text_uppercase;
            this.menuEditFontsSpecifyCharSize.MergeIndex = 1;
            this.menuEditFontsSpecifyCharSize.Name = "menuEditFontsSpecifyCharSize";
            this.menuEditFontsSpecifyCharSize.Size = new System.Drawing.Size(237, 22);
            this.menuEditFontsSpecifyCharSize.Text = "menuEditFontsSpecifyCharSize";
            this.menuEditFontsSpecifyCharSize.Click += new System.EventHandler(this.MenuEditSpecifyCharSize_Click);
            // 
            // menuEditRamps
            // 
            this.menuEditRamps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditRampsValidChars,
            this.menuEditRampsCopyRamp});
            this.menuEditRamps.Name = "menuEditRamps";
            this.menuEditRamps.Size = new System.Drawing.Size(187, 22);
            this.menuEditRamps.Text = "menuEditRamps";
            this.menuEditRamps.DropDownOpening += new System.EventHandler(this.MenuEditRamps_DropDownOpening);
            // 
            // menuEditRampsValidChars
            // 
            this.menuEditRampsValidChars.Image = global::AscGenDotNet.Properties.Resources.textfield;
            this.menuEditRampsValidChars.MergeIndex = 0;
            this.menuEditRampsValidChars.Name = "menuEditRampsValidChars";
            this.menuEditRampsValidChars.Size = new System.Drawing.Size(220, 22);
            this.menuEditRampsValidChars.Text = "menuEditRampsValidChars";
            this.menuEditRampsValidChars.Click += new System.EventHandler(this.MenuEditValidChars_Click);
            // 
            // menuEditRampsCopyRamp
            // 
            this.menuEditRampsCopyRamp.Image = global::AscGenDotNet.Properties.Resources.page_white_copy;
            this.menuEditRampsCopyRamp.Name = "menuEditRampsCopyRamp";
            this.menuEditRampsCopyRamp.Size = new System.Drawing.Size(220, 22);
            this.menuEditRampsCopyRamp.Text = "menuEditRampsCopyRamp";
            this.menuEditRampsCopyRamp.Click += new System.EventHandler(this.MenuEditRampCopyRamp_Click);
            // 
            // menuEditInput
            // 
            this.menuEditInput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditInputRotate90,
            this.menuEditInputRotate180,
            this.menuEditInputRotate270,
            this.toolStripSeparator1,
            this.menuEditInputFlipHorizontal,
            this.menuEditInputFlipVertical});
            this.menuEditInput.Name = "menuEditInput";
            this.menuEditInput.Size = new System.Drawing.Size(187, 22);
            this.menuEditInput.Text = "menuEditInput";
            this.menuEditInput.DropDownOpening += new System.EventHandler(this.MenuEditInput_DropDownOpening);
            // 
            // menuEditInputRotate90
            // 
            this.menuEditInputRotate90.Name = "menuEditInputRotate90";
            this.menuEditInputRotate90.Size = new System.Drawing.Size(227, 22);
            this.menuEditInputRotate90.Text = "menuEditInputRotate90";
            this.menuEditInputRotate90.Click += new System.EventHandler(this.MenuEditInputRotate90_Click);
            // 
            // menuEditInputRotate180
            // 
            this.menuEditInputRotate180.Name = "menuEditInputRotate180";
            this.menuEditInputRotate180.Size = new System.Drawing.Size(227, 22);
            this.menuEditInputRotate180.Text = "menuEditInputRotate180";
            this.menuEditInputRotate180.Click += new System.EventHandler(this.MenuEditInputRotate180_Click);
            // 
            // menuEditInputRotate270
            // 
            this.menuEditInputRotate270.Name = "menuEditInputRotate270";
            this.menuEditInputRotate270.Size = new System.Drawing.Size(227, 22);
            this.menuEditInputRotate270.Text = "menuEditInputRotate270";
            this.menuEditInputRotate270.Click += new System.EventHandler(this.MenuEditInputRotate270_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            // 
            // menuEditInputFlipHorizontal
            // 
            this.menuEditInputFlipHorizontal.Image = global::AscGenDotNet.Properties.Resources.shape_flip_horizontal;
            this.menuEditInputFlipHorizontal.Name = "menuEditInputFlipHorizontal";
            this.menuEditInputFlipHorizontal.Size = new System.Drawing.Size(227, 22);
            this.menuEditInputFlipHorizontal.Text = "menuEditInputFlipHorizontal";
            this.menuEditInputFlipHorizontal.Click += new System.EventHandler(this.MenuEditInputFlipHorizontal_Click);
            // 
            // menuEditInputFlipVertical
            // 
            this.menuEditInputFlipVertical.Image = global::AscGenDotNet.Properties.Resources.shape_flip_vertical;
            this.menuEditInputFlipVertical.Name = "menuEditInputFlipVertical";
            this.menuEditInputFlipVertical.Size = new System.Drawing.Size(227, 22);
            this.menuEditInputFlipVertical.Text = "menuEditInputFlipVertical";
            this.menuEditInputFlipVertical.Click += new System.EventHandler(this.MenuEditInputFlipVertical_Click);
            // 
            // menuEditOutput
            // 
            this.menuEditOutput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemInvertOutput,
            this.toolStripSeparator2,
            this.menuEditStretch,
            this.menuEditOutputSep1,
            this.menuEditSharpeningMethod,
            this.menuEditOutputSep2,
            this.menuEditFlipHorizontal,
            this.menuEditFlipVertical});
            this.menuEditOutput.MergeIndex = 3;
            this.menuEditOutput.Name = "menuEditOutput";
            this.menuEditOutput.Size = new System.Drawing.Size(187, 22);
            this.menuEditOutput.Text = "menuEditOutput";
            this.menuEditOutput.DropDownOpening += new System.EventHandler(this.MenuEditOutput_Popup);
            // 
            // toolStripMenuItemInvertOutput
            // 
            this.toolStripMenuItemInvertOutput.Image = global::AscGenDotNet.Properties.Resources.contrast_high;
            this.toolStripMenuItemInvertOutput.Name = "toolStripMenuItemInvertOutput";
            this.toolStripMenuItemInvertOutput.Size = new System.Drawing.Size(242, 22);
            this.toolStripMenuItemInvertOutput.Text = "toolStripMenuItemInvertOutput";
            this.toolStripMenuItemInvertOutput.Click += new System.EventHandler(this.ToolStripMenuItemInvertOutput_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
            // 
            // menuEditStretch
            // 
            this.menuEditStretch.MergeIndex = 0;
            this.menuEditStretch.Name = "menuEditStretch";
            this.menuEditStretch.Size = new System.Drawing.Size(242, 22);
            this.menuEditStretch.Text = "menuEditStretch";
            this.menuEditStretch.Click += new System.EventHandler(this.CmenuTextStretch_Click);
            // 
            // menuEditOutputSep1
            // 
            this.menuEditOutputSep1.Name = "menuEditOutputSep1";
            this.menuEditOutputSep1.Size = new System.Drawing.Size(239, 6);
            // 
            // menuEditSharpeningMethod
            // 
            this.menuEditSharpeningMethod.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditSharpeningMethodNone,
            this.menuEditSharpeningMethodSharpen,
            this.menuEditSharpeningMethodUnsharp});
            this.menuEditSharpeningMethod.MergeIndex = 2;
            this.menuEditSharpeningMethod.Name = "menuEditSharpeningMethod";
            this.menuEditSharpeningMethod.Size = new System.Drawing.Size(242, 22);
            this.menuEditSharpeningMethod.Text = "menuEditSharpeningMethod";
            this.menuEditSharpeningMethod.DropDownOpening += new System.EventHandler(this.MenuEditSharpeningMethod_Popup);
            // 
            // menuEditSharpeningMethodNone
            // 
            this.menuEditSharpeningMethodNone.MergeIndex = 0;
            this.menuEditSharpeningMethodNone.Name = "menuEditSharpeningMethodNone";
            this.menuEditSharpeningMethodNone.Size = new System.Drawing.Size(271, 22);
            this.menuEditSharpeningMethodNone.Text = "menuEditSharpeningMethodNone";
            this.menuEditSharpeningMethodNone.Click += new System.EventHandler(this.CmenuTextSharpeningNone_Click);
            // 
            // menuEditSharpeningMethodSharpen
            // 
            this.menuEditSharpeningMethodSharpen.MergeIndex = 1;
            this.menuEditSharpeningMethodSharpen.Name = "menuEditSharpeningMethodSharpen";
            this.menuEditSharpeningMethodSharpen.Size = new System.Drawing.Size(271, 22);
            this.menuEditSharpeningMethodSharpen.Text = "menuEditSharpeningMethodSharpen";
            this.menuEditSharpeningMethodSharpen.Click += new System.EventHandler(this.CmenuTextSharpeningSharpen_Click);
            // 
            // menuEditSharpeningMethodUnsharp
            // 
            this.menuEditSharpeningMethodUnsharp.MergeIndex = 2;
            this.menuEditSharpeningMethodUnsharp.Name = "menuEditSharpeningMethodUnsharp";
            this.menuEditSharpeningMethodUnsharp.Size = new System.Drawing.Size(271, 22);
            this.menuEditSharpeningMethodUnsharp.Text = "menuEditSharpeningMethodUnsharp";
            this.menuEditSharpeningMethodUnsharp.Click += new System.EventHandler(this.CmenuTextSharpeningUnsharp_Click);
            // 
            // menuEditOutputSep2
            // 
            this.menuEditOutputSep2.Name = "menuEditOutputSep2";
            this.menuEditOutputSep2.Size = new System.Drawing.Size(239, 6);
            // 
            // menuEditFlipHorizontal
            // 
            this.menuEditFlipHorizontal.Image = global::AscGenDotNet.Properties.Resources.shape_flip_horizontal;
            this.menuEditFlipHorizontal.MergeIndex = 4;
            this.menuEditFlipHorizontal.Name = "menuEditFlipHorizontal";
            this.menuEditFlipHorizontal.Size = new System.Drawing.Size(242, 22);
            this.menuEditFlipHorizontal.Text = "menuEditFlipHorizontal";
            this.menuEditFlipHorizontal.Click += new System.EventHandler(this.CmenuTextHorizontal_Click);
            // 
            // menuEditFlipVertical
            // 
            this.menuEditFlipVertical.Image = global::AscGenDotNet.Properties.Resources.shape_flip_vertical;
            this.menuEditFlipVertical.MergeIndex = 5;
            this.menuEditFlipVertical.Name = "menuEditFlipVertical";
            this.menuEditFlipVertical.Size = new System.Drawing.Size(242, 22);
            this.menuEditFlipVertical.Text = "menuEditFlipVertical";
            this.menuEditFlipVertical.Click += new System.EventHandler(this.CmenuTextVertical_Click);
            // 
            // menuEditSep2
            // 
            this.menuEditSep2.Name = "menuEditSep2";
            this.menuEditSep2.Size = new System.Drawing.Size(184, 6);
            // 
            // menuEditEditSettings
            // 
            this.menuEditEditSettings.Name = "menuEditEditSettings";
            this.menuEditEditSettings.Size = new System.Drawing.Size(187, 22);
            this.menuEditEditSettings.Text = "menuEditEditSettings";
            this.menuEditEditSettings.Click += new System.EventHandler(this.MenuEditEditSettings_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewText,
            this.toolStripMenuItemShowImage,
            this.menuViewSep1,
            this.menuViewColourPreview,
            this.menuViewSep2,
            this.menuViewFullScreen});
            this.menuView.MergeIndex = 2;
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(75, 20);
            this.menuView.Text = "menuView";
            this.menuView.DropDownOpening += new System.EventHandler(this.MenuView_Popup);
            // 
            // menuViewText
            // 
            this.menuViewText.Image = global::AscGenDotNet.Properties.Resources.text_align_justify;
            this.menuViewText.MergeIndex = 4;
            this.menuViewText.Name = "menuViewText";
            this.menuViewText.Size = new System.Drawing.Size(236, 22);
            this.menuViewText.Text = "menuViewText";
            this.menuViewText.Click += new System.EventHandler(this.MenuViewText_Click);
            // 
            // toolStripMenuItemShowImage
            // 
            this.toolStripMenuItemShowImage.Image = global::AscGenDotNet.Properties.Resources.image;
            this.toolStripMenuItemShowImage.Name = "toolStripMenuItemShowImage";
            this.toolStripMenuItemShowImage.Size = new System.Drawing.Size(236, 22);
            this.toolStripMenuItemShowImage.Text = "toolStripMenuItemShowImage";
            this.toolStripMenuItemShowImage.Click += new System.EventHandler(this.ToolStripMenuItemShowImage_Click);
            // 
            // menuViewSep1
            // 
            this.menuViewSep1.Name = "menuViewSep1";
            this.menuViewSep1.Size = new System.Drawing.Size(233, 6);
            // 
            // menuViewColourPreview
            // 
            this.menuViewColourPreview.Image = global::AscGenDotNet.Properties.Resources.page_magnify_color;
            this.menuViewColourPreview.Name = "menuViewColourPreview";
            this.menuViewColourPreview.Size = new System.Drawing.Size(236, 22);
            this.menuViewColourPreview.Text = "menuViewColourPreview";
            this.menuViewColourPreview.Click += new System.EventHandler(this.MenuViewColourPreview_Click);
            // 
            // menuViewSep2
            // 
            this.menuViewSep2.Name = "menuViewSep2";
            this.menuViewSep2.Size = new System.Drawing.Size(233, 6);
            // 
            // menuViewFullScreen
            // 
            this.menuViewFullScreen.Image = global::AscGenDotNet.Properties.Resources.monitor;
            this.menuViewFullScreen.MergeIndex = 7;
            this.menuViewFullScreen.Name = "menuViewFullScreen";
            this.menuViewFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.menuViewFullScreen.Size = new System.Drawing.Size(236, 22);
            this.menuViewFullScreen.Text = "menuViewFullScreen";
            this.menuViewFullScreen.Click += new System.EventHandler(this.MenuViewFullScreen_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpTutorials,
            this.toolStripMenuItemFAQ,
            this.cmenuHelpSep1,
            this.menuHelpRequestFeature,
            this.menuHelpReportBug,
            this.cmenuHelpSep2,
            this.menuHelpDonate,
            this.cmenuHelpSep3,
            this.menuHelpCheckForNewVersionToolStrip,
            this.menuHelpAbout});
            this.menuHelp.MergeIndex = 3;
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(75, 20);
            this.menuHelp.Text = "menuHelp";
            // 
            // menuHelpTutorials
            // 
            this.menuHelpTutorials.Image = global::AscGenDotNet.Properties.Resources.book;
            this.menuHelpTutorials.Name = "menuHelpTutorials";
            this.menuHelpTutorials.Size = new System.Drawing.Size(243, 22);
            this.menuHelpTutorials.Text = "menuHelpTutorials";
            this.menuHelpTutorials.Click += new System.EventHandler(this.MenuHelpTutorials_Click);
            // 
            // toolStripMenuItemFAQ
            // 
            this.toolStripMenuItemFAQ.Image = global::AscGenDotNet.Properties.Resources.help;
            this.toolStripMenuItemFAQ.Name = "toolStripMenuItemFAQ";
            this.toolStripMenuItemFAQ.Size = new System.Drawing.Size(243, 22);
            this.toolStripMenuItemFAQ.Text = "toolStripMenuItemFAQ";
            this.toolStripMenuItemFAQ.Click += new System.EventHandler(this.ToolStripMenuItemFAQ_Click);
            // 
            // cmenuHelpSep1
            // 
            this.cmenuHelpSep1.Name = "cmenuHelpSep1";
            this.cmenuHelpSep1.Size = new System.Drawing.Size(240, 6);
            // 
            // menuHelpRequestFeature
            // 
            this.menuHelpRequestFeature.Image = global::AscGenDotNet.Properties.Resources.user_comment;
            this.menuHelpRequestFeature.Name = "menuHelpRequestFeature";
            this.menuHelpRequestFeature.Size = new System.Drawing.Size(243, 22);
            this.menuHelpRequestFeature.Text = "menuHelpRequestFeature";
            this.menuHelpRequestFeature.Click += new System.EventHandler(this.MenuHelpRequestFeature_Click);
            // 
            // menuHelpReportBug
            // 
            this.menuHelpReportBug.Image = global::AscGenDotNet.Properties.Resources.bug;
            this.menuHelpReportBug.Name = "menuHelpReportBug";
            this.menuHelpReportBug.Size = new System.Drawing.Size(243, 22);
            this.menuHelpReportBug.Text = "menuHelpReportBug";
            this.menuHelpReportBug.Click += new System.EventHandler(this.MenuHelpReportBug_Click);
            // 
            // cmenuHelpSep2
            // 
            this.cmenuHelpSep2.Name = "cmenuHelpSep2";
            this.cmenuHelpSep2.Size = new System.Drawing.Size(240, 6);
            // 
            // menuHelpDonate
            // 
            this.menuHelpDonate.Image = global::AscGenDotNet.Properties.Resources.money;
            this.menuHelpDonate.Name = "menuHelpDonate";
            this.menuHelpDonate.Size = new System.Drawing.Size(243, 22);
            this.menuHelpDonate.Text = "menuHelpDonate";
            this.menuHelpDonate.Click += new System.EventHandler(this.MenuHelpDonate_Click);
            // 
            // cmenuHelpSep3
            // 
            this.cmenuHelpSep3.Name = "cmenuHelpSep3";
            this.cmenuHelpSep3.Size = new System.Drawing.Size(240, 6);
            // 
            // menuHelpCheckForNewVersionToolStrip
            // 
            this.menuHelpCheckForNewVersionToolStrip.Image = global::AscGenDotNet.Properties.Resources.connect;
            this.menuHelpCheckForNewVersionToolStrip.Name = "menuHelpCheckForNewVersionToolStrip";
            this.menuHelpCheckForNewVersionToolStrip.Size = new System.Drawing.Size(243, 22);
            this.menuHelpCheckForNewVersionToolStrip.Text = "menuHelpCheckForNewVersion";
            this.menuHelpCheckForNewVersionToolStrip.Click += new System.EventHandler(this.MenuHelpCheckForNewVersionToolStrip_Click);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Image = global::AscGenDotNet.Properties.Resources.help;
            this.menuHelpAbout.MergeIndex = 0;
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(243, 22);
            this.menuHelpAbout.Text = "menuHelpAbout";
            this.menuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
            // 
            // dialogChooseFont
            // 
            this.dialogChooseFont.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogChooseFont.FontMustExist = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlMain.Controls.Add(this.rtbxConvertedText);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(624, 428);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlMain_DragDrop);
            this.pnlMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlMain_DragEnter);
            this.pnlMain.Resize += new System.EventHandler(this.PnlMain_Resize);
            // 
            // rtbxConvertedText
            // 
            this.rtbxConvertedText.BackColor = System.Drawing.Color.White;
            this.rtbxConvertedText.BackgroundColor = System.Drawing.Color.White;
            this.rtbxConvertedText.ContextMenuStrip = this.contextMenuText;
            this.rtbxConvertedText.DetectUrls = false;
            this.rtbxConvertedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbxConvertedText.EnableAutoDragDrop = true;
            this.rtbxConvertedText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtbxConvertedText.Location = new System.Drawing.Point(0, 0);
            this.rtbxConvertedText.Margin = new System.Windows.Forms.Padding(0);
            this.rtbxConvertedText.Name = "rtbxConvertedText";
            this.rtbxConvertedText.ReadOnly = true;
            this.rtbxConvertedText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbxConvertedText.ShowSelectionMargin = true;
            this.rtbxConvertedText.Size = new System.Drawing.Size(624, 428);
            this.rtbxConvertedText.TabIndex = 0;
            this.rtbxConvertedText.Text = "";
            this.rtbxConvertedText.TextColor = System.Drawing.SystemColors.WindowText;
            this.rtbxConvertedText.WordWrap = false;
            // 
            // toolStripOutputSize
            // 
            this.toolStripOutputSize.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripOutputSize.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSize,
            this.tbxWidth,
            this.cbxLocked,
            this.tbxHeight});
            this.toolStripOutputSize.Location = new System.Drawing.Point(3, 24);
            this.toolStripOutputSize.Name = "toolStripOutputSize";
            this.toolStripOutputSize.Size = new System.Drawing.Size(189, 25);
            this.toolStripOutputSize.TabIndex = 1;
            this.toolStripOutputSize.Text = "toolStrip1";
            // 
            // toolStripLabelSize
            // 
            this.toolStripLabelSize.Name = "toolStripLabelSize";
            this.toolStripLabelSize.Size = new System.Drawing.Size(100, 22);
            this.toolStripLabelSize.Text = "toolStripLabelSize";
            // 
            // tbxWidth
            // 
            this.tbxWidth.MaxLength = 3;
            this.tbxWidth.Name = "tbxWidth";
            this.tbxWidth.Size = new System.Drawing.Size(25, 25);
            this.tbxWidth.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxWidth.TextChanged += new System.EventHandler(this.TbxWidth_TextChanged);
            // 
            // cbxLocked
            // 
            this.cbxLocked.Checked = true;
            this.cbxLocked.CheckOnClick = true;
            this.cbxLocked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxLocked.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cbxLocked.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cbxLocked.Name = "cbxLocked";
            this.cbxLocked.Size = new System.Drawing.Size(23, 22);
            this.cbxLocked.Text = "x";
            this.cbxLocked.ToolTipText = "Lock Aspect Ratio";
            this.cbxLocked.CheckStateChanged += new System.EventHandler(this.CbxLocked_CheckedChanged);
            // 
            // tbxHeight
            // 
            this.tbxHeight.MaxLength = 3;
            this.tbxHeight.Name = "tbxHeight";
            this.tbxHeight.Size = new System.Drawing.Size(25, 25);
            this.tbxHeight.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxHeight.TextChanged += new System.EventHandler(this.TbxHeight_TextChanged);
            // 
            // dialogSelectionColor
            // 
            this.dialogSelectionColor.FullOpen = true;
            this.dialogSelectionColor.SolidColorOnly = true;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlMain);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(624, 428);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(624, 602);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainMenu1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripOutputSize);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripRotateFlip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFont);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripWidgets);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripDisplay);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripRamp);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFile);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripCharacters);
            // 
            // toolStripRotateFlip
            // 
            this.toolStripRotateFlip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripRotateFlip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBlackOnWhite,
            this.tsbRotateAnticlockwise,
            this.tsbRotateClockwise,
            this.tsbFlipHorizontally,
            this.tsbFlipVertically});
            this.toolStripRotateFlip.Location = new System.Drawing.Point(3, 49);
            this.toolStripRotateFlip.Name = "toolStripRotateFlip";
            this.toolStripRotateFlip.Size = new System.Drawing.Size(127, 25);
            this.toolStripRotateFlip.TabIndex = 5;
            // 
            // toolStripButtonBlackOnWhite
            // 
            this.toolStripButtonBlackOnWhite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBlackOnWhite.Image = global::AscGenDotNet.Properties.Resources.contrast_high;
            this.toolStripButtonBlackOnWhite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBlackOnWhite.Name = "toolStripButtonBlackOnWhite";
            this.toolStripButtonBlackOnWhite.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonBlackOnWhite.Text = "toolStripButton1";
            this.toolStripButtonBlackOnWhite.Click += new System.EventHandler(this.ToolStripButtonBlackOnWhite_Click);
            // 
            // tsbRotateAnticlockwise
            // 
            this.tsbRotateAnticlockwise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRotateAnticlockwise.Image = global::AscGenDotNet.Properties.Resources.arrow_rotate_anticlockwise;
            this.tsbRotateAnticlockwise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRotateAnticlockwise.Name = "tsbRotateAnticlockwise";
            this.tsbRotateAnticlockwise.Size = new System.Drawing.Size(23, 22);
            this.tsbRotateAnticlockwise.Text = "toolStripButton1";
            this.tsbRotateAnticlockwise.Click += new System.EventHandler(this.TstripRotateAnticlockwise_Click);
            // 
            // tsbRotateClockwise
            // 
            this.tsbRotateClockwise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRotateClockwise.Image = global::AscGenDotNet.Properties.Resources.arrow_rotate_clockwise;
            this.tsbRotateClockwise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRotateClockwise.Name = "tsbRotateClockwise";
            this.tsbRotateClockwise.Size = new System.Drawing.Size(23, 22);
            this.tsbRotateClockwise.Text = "toolStripButton1";
            this.tsbRotateClockwise.Click += new System.EventHandler(this.TstripRotateClockwise_Click);
            // 
            // tsbFlipHorizontally
            // 
            this.tsbFlipHorizontally.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFlipHorizontally.Image = global::AscGenDotNet.Properties.Resources.shape_flip_horizontal;
            this.tsbFlipHorizontally.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFlipHorizontally.Name = "tsbFlipHorizontally";
            this.tsbFlipHorizontally.Size = new System.Drawing.Size(23, 22);
            this.tsbFlipHorizontally.Text = "toolStripButton1";
            this.tsbFlipHorizontally.Click += new System.EventHandler(this.TsbFlipHorizontally_Click);
            // 
            // tsbFlipVertically
            // 
            this.tsbFlipVertically.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFlipVertically.Image = global::AscGenDotNet.Properties.Resources.shape_flip_vertical;
            this.tsbFlipVertically.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFlipVertically.Name = "tsbFlipVertically";
            this.tsbFlipVertically.Size = new System.Drawing.Size(23, 22);
            this.tsbFlipVertically.Text = "toolStripButton1";
            this.tsbFlipVertically.Click += new System.EventHandler(this.TsbFlipVertically_Click);
            // 
            // toolStripFont
            // 
            this.toolStripFont.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripFont.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFont});
            this.toolStripFont.Location = new System.Drawing.Point(3, 74);
            this.toolStripFont.Name = "toolStripFont";
            this.toolStripFont.Size = new System.Drawing.Size(37, 25);
            this.toolStripFont.TabIndex = 4;
            // 
            // tsbFont
            // 
            this.tsbFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFont.Margin = new System.Windows.Forms.Padding(0, 1, 2, 2);
            this.tsbFont.Name = "tsbFont";
            this.tsbFont.Size = new System.Drawing.Size(23, 22);
            this.tsbFont.ToolTipText = "tsbFont";
            this.tsbFont.Click += new System.EventHandler(this.TsbFont_Click);
            // 
            // toolStripWidgets
            // 
            this.toolStripWidgets.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripWidgets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonShowTextWidget,
            this.toolStripButtonShowImageWidget});
            this.toolStripWidgets.Location = new System.Drawing.Point(40, 74);
            this.toolStripWidgets.Name = "toolStripWidgets";
            this.toolStripWidgets.Size = new System.Drawing.Size(58, 25);
            this.toolStripWidgets.TabIndex = 8;
            // 
            // toolStripButtonShowTextWidget
            // 
            this.toolStripButtonShowTextWidget.Checked = true;
            this.toolStripButtonShowTextWidget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonShowTextWidget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowTextWidget.Image = global::AscGenDotNet.Properties.Resources.text_align_justify;
            this.toolStripButtonShowTextWidget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowTextWidget.Name = "toolStripButtonShowTextWidget";
            this.toolStripButtonShowTextWidget.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowTextWidget.Text = "toolStripButtonShowTextWidget";
            this.toolStripButtonShowTextWidget.Click += new System.EventHandler(this.ToolStripButtonShowTextWidget_Click);
            // 
            // toolStripButtonShowImageWidget
            // 
            this.toolStripButtonShowImageWidget.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowImageWidget.Image = global::AscGenDotNet.Properties.Resources.image;
            this.toolStripButtonShowImageWidget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowImageWidget.Name = "toolStripButtonShowImageWidget";
            this.toolStripButtonShowImageWidget.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowImageWidget.Text = "toolStripButtonShowImageWidget";
            this.toolStripButtonShowImageWidget.Click += new System.EventHandler(this.ToolStripButtonShowImageWidget_Click);
            // 
            // toolStripRamp
            // 
            this.toolStripRamp.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripRamp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblRamp,
            this.cmbRamp,
            this.chkGenerate});
            this.toolStripRamp.Location = new System.Drawing.Point(61, 124);
            this.toolStripRamp.Name = "toolStripRamp";
            this.toolStripRamp.Size = new System.Drawing.Size(263, 25);
            this.toolStripRamp.TabIndex = 2;
            // 
            // lblRamp
            // 
            this.lblRamp.Name = "lblRamp";
            this.lblRamp.Size = new System.Drawing.Size(51, 22);
            this.lblRamp.Text = "lblRamp";
            // 
            // cmbRamp
            // 
            this.cmbRamp.Name = "cmbRamp";
            this.cmbRamp.Size = new System.Drawing.Size(121, 25);
            this.cmbRamp.DropDown += new System.EventHandler(this.CmbRamp_DropDown);
            this.cmbRamp.SelectedIndexChanged += new System.EventHandler(this.CmbRamp_SelectedIndexChanged);
            this.cmbRamp.TextChanged += new System.EventHandler(this.CmbRamp_TextChanged);
            // 
            // chkGenerate
            // 
            this.chkGenerate.CheckOnClick = true;
            this.chkGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chkGenerate.Image = ((System.Drawing.Image)(resources.GetObject("chkGenerate.Image")));
            this.chkGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chkGenerate.Name = "chkGenerate";
            this.chkGenerate.Size = new System.Drawing.Size(77, 22);
            this.chkGenerate.Text = "chkGenerate";
            this.chkGenerate.ToolTipText = "Automatically Generate a Ramp";
            this.chkGenerate.CheckedChanged += new System.EventHandler(this.ChkGenerate_CheckedChanged);
            // 
            // toolStripDisplay
            // 
            this.toolStripDisplay.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPreview,
            this.toolStripButtonFullScreen});
            this.toolStripDisplay.Location = new System.Drawing.Point(3, 124);
            this.toolStripDisplay.Name = "toolStripDisplay";
            this.toolStripDisplay.Size = new System.Drawing.Size(58, 25);
            this.toolStripDisplay.TabIndex = 6;
            // 
            // toolStripButtonPreview
            // 
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = global::AscGenDotNet.Properties.Resources.page_magnify_color;
            this.toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPreview.Text = "toolStripButtonPreview";
            this.toolStripButtonPreview.Click += new System.EventHandler(this.ToolStripButtonPreview_Click);
            // 
            // toolStripButtonFullScreen
            // 
            this.toolStripButtonFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFullScreen.Image = global::AscGenDotNet.Properties.Resources.monitor;
            this.toolStripButtonFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFullScreen.Name = "toolStripButtonFullScreen";
            this.toolStripButtonFullScreen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFullScreen.Text = "toolStripButtonFullScreen";
            this.toolStripButtonFullScreen.Click += new System.EventHandler(this.ToolStripButtonFullScreen_Click);
            // 
            // toolStripFile
            // 
            this.toolStripFile.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLoad,
            this.toolStripButtonSave});
            this.toolStripFile.Location = new System.Drawing.Point(3, 149);
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Size = new System.Drawing.Size(58, 25);
            this.toolStripFile.TabIndex = 7;
            // 
            // toolStripButtonLoad
            // 
            this.toolStripButtonLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLoad.Image = global::AscGenDotNet.Properties.Resources.folder;
            this.toolStripButtonLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoad.Name = "toolStripButtonLoad";
            this.toolStripButtonLoad.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLoad.Click += new System.EventHandler(this.ToolStripButtonLoad_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::AscGenDotNet.Properties.Resources.disk;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Click += new System.EventHandler(this.ToolStripButtonSave_Click);
            // 
            // toolStripCharacters
            // 
            this.toolStripCharacters.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripCharacters.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCharacters,
            this.cmbCharacters});
            this.toolStripCharacters.Location = new System.Drawing.Point(3, 99);
            this.toolStripCharacters.Name = "toolStripCharacters";
            this.toolStripCharacters.Size = new System.Drawing.Size(211, 25);
            this.toolStripCharacters.TabIndex = 3;
            // 
            // lblCharacters
            // 
            this.lblCharacters.Name = "lblCharacters";
            this.lblCharacters.Size = new System.Drawing.Size(76, 22);
            this.lblCharacters.Text = "lblCharacters";
            // 
            // cmbCharacters
            // 
            this.cmbCharacters.Name = "cmbCharacters";
            this.cmbCharacters.Size = new System.Drawing.Size(121, 25);
            this.cmbCharacters.DropDown += new System.EventHandler(this.CmbCharacters_DropDown);
            this.cmbCharacters.TextChanged += new System.EventHandler(this.CmbCharacters_TextChanged);
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.AllowMargins = false;
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.DocumentPrint);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.ShowIcon = false;
            this.printPreviewDialog.Visible = false;
            // 
            // FormConvertImage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(624, 602);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(472, 464);
            this.Name = "FormConvertImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASCGEN dotNET ";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormConvertImage_Closing);
            this.contextMenuText.ResumeLayout(false);
            this.mainMenu1.ResumeLayout(false);
            this.mainMenu1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.toolStripOutputSize.ResumeLayout(false);
            this.toolStripOutputSize.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripRotateFlip.ResumeLayout(false);
            this.toolStripRotateFlip.PerformLayout();
            this.toolStripFont.ResumeLayout(false);
            this.toolStripFont.PerformLayout();
            this.toolStripWidgets.ResumeLayout(false);
            this.toolStripWidgets.PerformLayout();
            this.toolStripRamp.ResumeLayout(false);
            this.toolStripRamp.PerformLayout();
            this.toolStripDisplay.ResumeLayout(false);
            this.toolStripDisplay.PerformLayout();
            this.toolStripFile.ResumeLayout(false);
            this.toolStripFile.PerformLayout();
            this.toolStripCharacters.ResumeLayout(false);
            this.toolStripCharacters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Private methods

        private ToolStripMenuItem toolStripMenuItemInvertOutput;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStrip toolStripDisplay;
        private ToolStripLabel lblCharacters;
        private ToolStripComboBox cmbCharacters;
        private ToolStripButton toolStripButtonPreview;
        private ToolStripButton toolStripButtonFullScreen;
        private ToolStripLabel toolStripLabelSize;
        private ToolStrip toolStripFile;
        private ToolStripButton toolStripButtonLoad;
        private ToolStripButton toolStripButtonSave;
        private ToolStripMenuItem toolStripMenuItemShowImage;
        private ToolStripButton toolStripButtonBlackOnWhite;
        private ToolStrip toolStripWidgets;
        private ToolStripButton toolStripButtonShowTextWidget;
        private ToolStripButton toolStripButtonShowImageWidget;
        private ToolStripMenuItem toolStripMenuItemFAQ;
    }
}