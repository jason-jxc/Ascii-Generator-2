namespace JMSoftware.AsciiGeneratorDotNet
{
    partial class FormBatchConversion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogInput = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarConversion = new System.Windows.Forms.ProgressBar();
            this.buttonStart = new System.Windows.Forms.Button();
            this.folderBrowserDialogOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageConversions = new System.Windows.Forms.TabPage();
            this.splitContainerConversions = new System.Windows.Forms.SplitContainer();
            this.buttonRemoveImage = new System.Windows.Forms.Button();
            this.buttonAddImage = new System.Windows.Forms.Button();
            this.buttonAddFolder = new System.Windows.Forms.Button();
            this.fileListbox1 = new JMSoftware.Controls.FileListBox();
            this.contextMenuStripFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFileAfterConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showFullPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDownImageScale = new System.Windows.Forms.NumericUpDown();
            this.labelPercent = new System.Windows.Forms.Label();
            this.buttonFont = new System.Windows.Forms.Button();
            this.comboBoxOutputFormat = new System.Windows.Forms.ComboBox();
            this.textBoxOutputDirectory = new System.Windows.Forms.TextBox();
            this.labelOutputDirectory = new System.Windows.Forms.Label();
            this.checkBoxLockRatio = new System.Windows.Forms.CheckBox();
            this.buttonOutputDirectory = new System.Windows.Forms.Button();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelOutputAs = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.comboBoxOutputType = new System.Windows.Forms.ComboBox();
            this.labelOutputSize = new System.Windows.Forms.Label();
            this.checkBoxColour = new System.Windows.Forms.CheckBox();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.propertyGridConversionSettings = new System.Windows.Forms.PropertyGrid();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.contextMenuStripLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialogOutput = new System.Windows.Forms.FontDialog();
            this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
            this.buttonStop = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageConversions.SuspendLayout();
            this.splitContainerConversions.Panel1.SuspendLayout();
            this.splitContainerConversions.Panel2.SuspendLayout();
            this.splitContainerConversions.SuspendLayout();
            this.contextMenuStripFileList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageScale)).BeginInit();
            this.tabPageAdvanced.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.contextMenuStripLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogInput
            // 
            this.openFileDialogInput.Multiselect = true;
            // 
            // folderBrowserDialogInput
            // 
            this.folderBrowserDialogInput.ShowNewFolderButton = false;
            // 
            // progressBarConversion
            // 
            this.progressBarConversion.Location = new System.Drawing.Point(71, 344);
            this.progressBarConversion.Name = "progressBarConversion";
            this.progressBarConversion.Size = new System.Drawing.Size(286, 26);
            this.progressBarConversion.TabIndex = 2;
            // 
            // buttonStart
            // 
            this.buttonStart.Image = global::AscGenDotNet.Properties.Resources.control_play_blue;
            this.buttonStart.Location = new System.Drawing.Point(12, 344);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(26, 26);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // folderBrowserDialogOutput
            // 
            this.folderBrowserDialogOutput.ShowNewFolderButton = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageConversions);
            this.tabControlMain.Controls.Add(this.tabPageAdvanced);
            this.tabControlMain.Controls.Add(this.tabPageLog);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(8);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(345, 321);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageConversions
            // 
            this.tabPageConversions.Controls.Add(this.splitContainerConversions);
            this.tabPageConversions.Location = new System.Drawing.Point(4, 22);
            this.tabPageConversions.Name = "tabPageConversions";
            this.tabPageConversions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConversions.Size = new System.Drawing.Size(337, 295);
            this.tabPageConversions.TabIndex = 0;
            this.tabPageConversions.Text = "tabPageConversions";
            this.tabPageConversions.UseVisualStyleBackColor = true;
            // 
            // splitContainerConversions
            // 
            this.splitContainerConversions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerConversions.IsSplitterFixed = true;
            this.splitContainerConversions.Location = new System.Drawing.Point(3, 3);
            this.splitContainerConversions.Name = "splitContainerConversions";
            this.splitContainerConversions.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerConversions.Panel1
            // 
            this.splitContainerConversions.Panel1.Controls.Add(this.buttonRemoveImage);
            this.splitContainerConversions.Panel1.Controls.Add(this.buttonAddImage);
            this.splitContainerConversions.Panel1.Controls.Add(this.buttonAddFolder);
            this.splitContainerConversions.Panel1.Controls.Add(this.fileListbox1);
            this.splitContainerConversions.Panel1MinSize = 180;
            // 
            // splitContainerConversions.Panel2
            // 
            this.splitContainerConversions.Panel2.Controls.Add(this.numericUpDownImageScale);
            this.splitContainerConversions.Panel2.Controls.Add(this.labelPercent);
            this.splitContainerConversions.Panel2.Controls.Add(this.buttonFont);
            this.splitContainerConversions.Panel2.Controls.Add(this.comboBoxOutputFormat);
            this.splitContainerConversions.Panel2.Controls.Add(this.textBoxOutputDirectory);
            this.splitContainerConversions.Panel2.Controls.Add(this.labelOutputDirectory);
            this.splitContainerConversions.Panel2.Controls.Add(this.checkBoxLockRatio);
            this.splitContainerConversions.Panel2.Controls.Add(this.buttonOutputDirectory);
            this.splitContainerConversions.Panel2.Controls.Add(this.textBoxHeight);
            this.splitContainerConversions.Panel2.Controls.Add(this.labelOutputAs);
            this.splitContainerConversions.Panel2.Controls.Add(this.textBoxWidth);
            this.splitContainerConversions.Panel2.Controls.Add(this.comboBoxOutputType);
            this.splitContainerConversions.Panel2.Controls.Add(this.labelOutputSize);
            this.splitContainerConversions.Panel2.Controls.Add(this.checkBoxColour);
            this.splitContainerConversions.Panel2MinSize = 100;
            this.splitContainerConversions.Size = new System.Drawing.Size(331, 289);
            this.splitContainerConversions.SplitterDistance = 180;
            this.splitContainerConversions.TabIndex = 0;
            // 
            // buttonRemoveImage
            // 
            this.buttonRemoveImage.Enabled = false;
            this.buttonRemoveImage.Image = global::AscGenDotNet.Properties.Resources.image_delete;
            this.buttonRemoveImage.Location = new System.Drawing.Point(6, 99);
            this.buttonRemoveImage.Name = "buttonRemoveImage";
            this.buttonRemoveImage.Size = new System.Drawing.Size(40, 40);
            this.buttonRemoveImage.TabIndex = 2;
            this.buttonRemoveImage.UseVisualStyleBackColor = true;
            this.buttonRemoveImage.Click += new System.EventHandler(this.ButtonRemoveImage_Click);
            // 
            // buttonAddImage
            // 
            this.buttonAddImage.Image = global::AscGenDotNet.Properties.Resources.image_add;
            this.buttonAddImage.Location = new System.Drawing.Point(6, 7);
            this.buttonAddImage.Name = "buttonAddImage";
            this.buttonAddImage.Size = new System.Drawing.Size(40, 40);
            this.buttonAddImage.TabIndex = 0;
            this.buttonAddImage.UseVisualStyleBackColor = true;
            this.buttonAddImage.Click += new System.EventHandler(this.ButtonAddImage_Click);
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Image = global::AscGenDotNet.Properties.Resources.folder_add;
            this.buttonAddFolder.Location = new System.Drawing.Point(6, 53);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(40, 40);
            this.buttonAddFolder.TabIndex = 1;
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.ButtonAddFolder_Click);
            // 
            // fileListbox1
            // 
            this.fileListbox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileListbox1.ContextMenuStrip = this.contextMenuStripFileList;
            this.fileListbox1.DisplayPath = false;
            this.fileListbox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fileListbox1.FormattingEnabled = true;
            this.fileListbox1.Location = new System.Drawing.Point(52, 7);
            this.fileListbox1.Name = "fileListbox1";
            this.fileListbox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.fileListbox1.Size = new System.Drawing.Size(268, 173);
            this.fileListbox1.TabIndex = 3;
            // 
            // contextMenuStripFileList
            // 
            this.contextMenuStripFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileToolStripMenuItem,
            this.addDirectoryToolStripMenuItem,
            this.toolStripMenuSeparator1,
            this.removeToolStripMenuItem,
            this.removeAllToolStripMenuItem,
            this.removeFileAfterConversionToolStripMenuItem,
            this.toolStripMenuSeparator2,
            this.selectAllToolStripMenuItem,
            this.selectNoneToolStripMenuItem,
            this.invertSelectionToolStripMenuItem,
            this.toolStripMenuSeparator3,
            this.showFullPathToolStripMenuItem,
            this.showExtensionToolStripMenuItem});
            this.contextMenuStripFileList.Name = "contextMenuStripFileList";
            this.contextMenuStripFileList.Size = new System.Drawing.Size(322, 242);
            this.contextMenuStripFileList.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripFileList_Opening);
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.addFileToolStripMenuItem.Text = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.AddFileToolStripMenuItem_Click);
            // 
            // addDirectoryToolStripMenuItem
            // 
            this.addDirectoryToolStripMenuItem.Name = "addDirectoryToolStripMenuItem";
            this.addDirectoryToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.addDirectoryToolStripMenuItem.Text = "addDirectoryToolStripMenuItem";
            this.addDirectoryToolStripMenuItem.Click += new System.EventHandler(this.AddDirectoryToolStripMenuItem_Click);
            // 
            // toolStripMenuSeparator1
            // 
            this.toolStripMenuSeparator1.Name = "toolStripMenuSeparator1";
            this.toolStripMenuSeparator1.Size = new System.Drawing.Size(318, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.removeToolStripMenuItem.Text = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.removeAllToolStripMenuItem.Text = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllToolStripMenuItem_Click);
            // 
            // removeFileAfterConversionToolStripMenuItem
            // 
            this.removeFileAfterConversionToolStripMenuItem.Name = "removeFileAfterConversionToolStripMenuItem";
            this.removeFileAfterConversionToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.removeFileAfterConversionToolStripMenuItem.Text = "removeFileAfterConversionToolStripMenuItem";
            this.removeFileAfterConversionToolStripMenuItem.Click += new System.EventHandler(this.RemoveFileAfterConversionToolStripMenuItem_Click);
            // 
            // toolStripMenuSeparator2
            // 
            this.toolStripMenuSeparator2.Name = "toolStripMenuSeparator2";
            this.toolStripMenuSeparator2.Size = new System.Drawing.Size(318, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.selectAllToolStripMenuItem.Text = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // selectNoneToolStripMenuItem
            // 
            this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
            this.selectNoneToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.selectNoneToolStripMenuItem.Text = "selectNoneToolStripMenuItem";
            this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.SelectNoneToolStripMenuItem_Click);
            // 
            // invertSelectionToolStripMenuItem
            // 
            this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.invertSelectionToolStripMenuItem.Text = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.InvertSelectionToolStripMenuItem_Click);
            // 
            // toolStripMenuSeparator3
            // 
            this.toolStripMenuSeparator3.Name = "toolStripMenuSeparator3";
            this.toolStripMenuSeparator3.Size = new System.Drawing.Size(318, 6);
            // 
            // showFullPathToolStripMenuItem
            // 
            this.showFullPathToolStripMenuItem.Name = "showFullPathToolStripMenuItem";
            this.showFullPathToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.showFullPathToolStripMenuItem.Text = "showFullPathToolStripMenuItem";
            this.showFullPathToolStripMenuItem.Click += new System.EventHandler(this.ShowFullPathToolStripMenuItem_Click);
            // 
            // showExtensionToolStripMenuItem
            // 
            this.showExtensionToolStripMenuItem.Name = "showExtensionToolStripMenuItem";
            this.showExtensionToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.showExtensionToolStripMenuItem.Text = "showExtensionToolStripMenuItem";
            this.showExtensionToolStripMenuItem.Click += new System.EventHandler(this.ShowExtensionToolStripMenuItem_Click);
            // 
            // numericUpDownImageScale
            // 
            this.numericUpDownImageScale.Location = new System.Drawing.Point(275, 43);
            this.numericUpDownImageScale.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownImageScale.Name = "numericUpDownImageScale";
            this.numericUpDownImageScale.ReadOnly = true;
            this.numericUpDownImageScale.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownImageScale.TabIndex = 9;
            this.numericUpDownImageScale.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelPercent
            // 
            this.labelPercent.AutoSize = true;
            this.labelPercent.Location = new System.Drawing.Point(315, 45);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(15, 13);
            this.labelPercent.TabIndex = 10;
            this.labelPercent.Text = "%";
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(245, 5);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(75, 24);
            this.buttonFont.TabIndex = 4;
            this.buttonFont.Text = "buttonFont";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.ButtonFont_Click);
            // 
            // comboBoxOutputFormat
            // 
            this.comboBoxOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputFormat.FormattingEnabled = true;
            this.comboBoxOutputFormat.Location = new System.Drawing.Point(162, 42);
            this.comboBoxOutputFormat.Name = "comboBoxOutputFormat";
            this.comboBoxOutputFormat.Size = new System.Drawing.Size(50, 21);
            this.comboBoxOutputFormat.TabIndex = 7;
            this.comboBoxOutputFormat.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOutputFormat_SelectedIndexChanged);
            // 
            // textBoxOutputDirectory
            // 
            this.textBoxOutputDirectory.Location = new System.Drawing.Point(112, 79);
            this.textBoxOutputDirectory.Name = "textBoxOutputDirectory";
            this.textBoxOutputDirectory.Size = new System.Drawing.Size(174, 20);
            this.textBoxOutputDirectory.TabIndex = 12;
            this.textBoxOutputDirectory.Leave += new System.EventHandler(this.TextBoxOutputDirectory_Leave);
            // 
            // labelOutputDirectory
            // 
            this.labelOutputDirectory.AutoSize = true;
            this.labelOutputDirectory.Location = new System.Drawing.Point(3, 82);
            this.labelOutputDirectory.Name = "labelOutputDirectory";
            this.labelOutputDirectory.Size = new System.Drawing.Size(103, 13);
            this.labelOutputDirectory.TabIndex = 11;
            this.labelOutputDirectory.Text = "labelOutputDirectory";
            // 
            // checkBoxLockRatio
            // 
            this.checkBoxLockRatio.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxLockRatio.Location = new System.Drawing.Point(127, 5);
            this.checkBoxLockRatio.Name = "checkBoxLockRatio";
            this.checkBoxLockRatio.Size = new System.Drawing.Size(24, 24);
            this.checkBoxLockRatio.TabIndex = 2;
            this.checkBoxLockRatio.Text = "X";
            this.checkBoxLockRatio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxLockRatio.UseVisualStyleBackColor = true;
            this.checkBoxLockRatio.CheckStateChanged += new System.EventHandler(this.CheckBoxLockRatio_CheckedChanged);
            // 
            // buttonOutputDirectory
            // 
            this.buttonOutputDirectory.Location = new System.Drawing.Point(292, 77);
            this.buttonOutputDirectory.Name = "buttonOutputDirectory";
            this.buttonOutputDirectory.Size = new System.Drawing.Size(28, 23);
            this.buttonOutputDirectory.TabIndex = 13;
            this.buttonOutputDirectory.Text = "...";
            this.buttonOutputDirectory.UseVisualStyleBackColor = true;
            this.buttonOutputDirectory.Click += new System.EventHandler(this.ButtonOutputDirectory_Click);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(157, 7);
            this.textBoxHeight.MaxLength = 4;
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(36, 20);
            this.textBoxHeight.TabIndex = 3;
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxHeight.Leave += new System.EventHandler(this.TextBoxHeight_Leave);
            // 
            // labelOutputAs
            // 
            this.labelOutputAs.AutoSize = true;
            this.labelOutputAs.Location = new System.Drawing.Point(3, 45);
            this.labelOutputAs.Name = "labelOutputAs";
            this.labelOutputAs.Size = new System.Drawing.Size(73, 13);
            this.labelOutputAs.TabIndex = 5;
            this.labelOutputAs.Text = "labelOutputAs";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(85, 7);
            this.textBoxWidth.MaxLength = 4;
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(36, 20);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxWidth.Leave += new System.EventHandler(this.TextBoxWidth_Leave);
            // 
            // comboBoxOutputType
            // 
            this.comboBoxOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputType.FormattingEnabled = true;
            this.comboBoxOutputType.Location = new System.Drawing.Point(82, 42);
            this.comboBoxOutputType.Name = "comboBoxOutputType";
            this.comboBoxOutputType.Size = new System.Drawing.Size(74, 21);
            this.comboBoxOutputType.TabIndex = 6;
            this.comboBoxOutputType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOutputType_SelectedIndexChanged);
            // 
            // labelOutputSize
            // 
            this.labelOutputSize.AutoSize = true;
            this.labelOutputSize.Location = new System.Drawing.Point(3, 10);
            this.labelOutputSize.Name = "labelOutputSize";
            this.labelOutputSize.Size = new System.Drawing.Size(81, 13);
            this.labelOutputSize.TabIndex = 0;
            this.labelOutputSize.Text = "labelOutputSize";
            // 
            // checkBoxColour
            // 
            this.checkBoxColour.AutoSize = true;
            this.checkBoxColour.Location = new System.Drawing.Point(218, 44);
            this.checkBoxColour.Name = "checkBoxColour";
            this.checkBoxColour.Size = new System.Drawing.Size(104, 17);
            this.checkBoxColour.TabIndex = 8;
            this.checkBoxColour.Text = "checkBoxColour";
            this.checkBoxColour.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.propertyGridConversionSettings);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(337, 295);
            this.tabPageAdvanced.TabIndex = 3;
            this.tabPageAdvanced.Text = "tabPageAdvanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // propertyGridConversionSettings
            // 
            this.propertyGridConversionSettings.Location = new System.Drawing.Point(11, 10);
            this.propertyGridConversionSettings.Name = "propertyGridConversionSettings";
            this.propertyGridConversionSettings.Size = new System.Drawing.Size(312, 276);
            this.propertyGridConversionSettings.TabIndex = 0;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(337, 295);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "tabPageLog";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.ContextMenuStrip = this.contextMenuStripLog;
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(11, 10);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(312, 276);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // contextMenuStripLog
            // 
            this.contextMenuStripLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.saveLogAsToolStripMenuItem,
            this.toolStripMenuItemSeparator1,
            this.selectAllLogToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.contextMenuStripLog.Name = "contextMenuStripLog";
            this.contextMenuStripLog.Size = new System.Drawing.Size(235, 98);
            this.contextMenuStripLog.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripLog_Opening);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.clearToolStripMenuItem.Text = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // saveLogAsToolStripMenuItem
            // 
            this.saveLogAsToolStripMenuItem.Name = "saveLogAsToolStripMenuItem";
            this.saveLogAsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.saveLogAsToolStripMenuItem.Text = "saveLogAsToolStripMenuItem";
            this.saveLogAsToolStripMenuItem.Click += new System.EventHandler(this.SaveLogAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSeparator1
            // 
            this.toolStripMenuItemSeparator1.Name = "toolStripMenuItemSeparator1";
            this.toolStripMenuItemSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // selectAllLogToolStripMenuItem
            // 
            this.selectAllLogToolStripMenuItem.Name = "selectAllLogToolStripMenuItem";
            this.selectAllLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllLogToolStripMenuItem.ShowShortcutKeys = false;
            this.selectAllLogToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.selectAllLogToolStripMenuItem.Text = "selectAllLogToolStripMenuItem";
            this.selectAllLogToolStripMenuItem.Click += new System.EventHandler(this.SelectAllLogToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.copyToolStripMenuItem.Text = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = global::AscGenDotNet.Properties.Resources.control_stop_blue;
            this.buttonStop.Location = new System.Drawing.Point(39, 344);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(26, 26);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // FormBatchConversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 382);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.progressBarConversion);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBatchConversion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormBatchConversion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBatchConversion_FormClosing);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageConversions.ResumeLayout(false);
            this.splitContainerConversions.Panel1.ResumeLayout(false);
            this.splitContainerConversions.Panel2.ResumeLayout(false);
            this.splitContainerConversions.Panel2.PerformLayout();
            this.splitContainerConversions.ResumeLayout(false);
            this.contextMenuStripFileList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageScale)).EndInit();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.contextMenuStripLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAddImage;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.Button buttonRemoveImage;
        private JMSoftware.Controls.FileListBox fileListbox1;
        private System.Windows.Forms.OpenFileDialog openFileDialogInput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogInput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ProgressBar progressBarConversion;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOutput;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageConversions;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.SplitContainer splitContainerConversions;
        private System.Windows.Forms.ComboBox comboBoxOutputFormat;
        private System.Windows.Forms.TextBox textBoxOutputDirectory;
        private System.Windows.Forms.Label labelOutputDirectory;
        private System.Windows.Forms.CheckBox checkBoxLockRatio;
        private System.Windows.Forms.Button buttonOutputDirectory;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelOutputAs;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.ComboBox comboBoxOutputType;
        private System.Windows.Forms.Label labelOutputSize;
        private System.Windows.Forms.CheckBox checkBoxColour;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.PropertyGrid propertyGridConversionSettings;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.FontDialog fontDialogOutput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLog;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
        private System.Windows.Forms.NumericUpDown numericUpDownImageScale;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFileList;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFileAfterConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showFullPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showExtensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSeparator1;
        private System.Windows.Forms.ToolStripMenuItem selectAllLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button buttonStop;
    }
}