namespace VidLec
{
    partial class LectureSelector
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
            this.TLPSplit = new System.Windows.Forms.TableLayoutPanel();
            this.olvPresentations = new BrightIdeasSoftware.ObjectListView();
            this.olvPresClmName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPresClmDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPresClmDuration = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPresClmViews = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPresClmDesc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlvAll = new BrightIdeasSoftware.TreeListView();
            this.tlvAllClmName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tlvAllClmCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bgwLogin = new System.ComponentModel.BackgroundWorker();
            this.bgwCatalogLoader = new System.ComponentModel.BackgroundWorker();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingEnableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogWindowStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCookiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSavedCookiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSavedCredentailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCatalogDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NetworkStatusStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownSetOnline = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownForceOffline = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.offlineByDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.TLPSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvPresentations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAll)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPSplit
            // 
            this.TLPSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TLPSplit.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.TLPSplit.ColumnCount = 2;
            this.TLPSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.15385F));
            this.TLPSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.84615F));
            this.TLPSplit.Controls.Add(this.olvPresentations, 1, 0);
            this.TLPSplit.Controls.Add(this.tlvAll, 0, 0);
            this.TLPSplit.Location = new System.Drawing.Point(12, 72);
            this.TLPSplit.Name = "TLPSplit";
            this.TLPSplit.RowCount = 1;
            this.TLPSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPSplit.Size = new System.Drawing.Size(937, 344);
            this.TLPSplit.TabIndex = 0;
            // 
            // olvPresentations
            // 
            this.olvPresentations.AllColumns.Add(this.olvPresClmName);
            this.olvPresentations.AllColumns.Add(this.olvPresClmDate);
            this.olvPresentations.AllColumns.Add(this.olvPresClmDuration);
            this.olvPresentations.AllColumns.Add(this.olvPresClmViews);
            this.olvPresentations.AllColumns.Add(this.olvPresClmDesc);
            this.olvPresentations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvPresentations.CellEditUseWholeCell = false;
            this.olvPresentations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvPresClmName,
            this.olvPresClmDate,
            this.olvPresClmDuration,
            this.olvPresClmViews,
            this.olvPresClmDesc});
            this.olvPresentations.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvPresentations.Location = new System.Drawing.Point(436, 4);
            this.olvPresentations.Name = "olvPresentations";
            this.olvPresentations.Size = new System.Drawing.Size(497, 336);
            this.olvPresentations.TabIndex = 1;
            this.olvPresentations.UseCompatibleStateImageBehavior = false;
            this.olvPresentations.View = System.Windows.Forms.View.Details;
            this.olvPresentations.ItemActivate += new System.EventHandler(this.olvPresentations_ItemActivate);
            // 
            // olvPresClmName
            // 
            this.olvPresClmName.AspectName = "Name";
            this.olvPresClmName.Groupable = false;
            this.olvPresClmName.Text = "Name";
            this.olvPresClmName.Width = 180;
            // 
            // olvPresClmDate
            // 
            this.olvPresClmDate.AspectName = "FullStartDate";
            this.olvPresClmDate.Text = "Date";
            this.olvPresClmDate.Width = 80;
            // 
            // olvPresClmDuration
            // 
            this.olvPresClmDuration.AspectName = "DurationDisplay";
            this.olvPresClmDuration.Text = "Duration";
            this.olvPresClmDuration.Width = 55;
            // 
            // olvPresClmViews
            // 
            this.olvPresClmViews.AspectName = "Views";
            this.olvPresClmViews.Text = "Views";
            // 
            // olvPresClmDesc
            // 
            this.olvPresClmDesc.AspectName = "Description";
            this.olvPresClmDesc.Text = "Description";
            this.olvPresClmDesc.Width = 102;
            // 
            // tlvAll
            // 
            this.tlvAll.AllColumns.Add(this.tlvAllClmName);
            this.tlvAll.AllColumns.Add(this.tlvAllClmCount);
            this.tlvAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlvAll.CellEditUseWholeCell = false;
            this.tlvAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tlvAllClmName,
            this.tlvAllClmCount});
            this.tlvAll.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvAll.Location = new System.Drawing.Point(4, 4);
            this.tlvAll.Name = "tlvAll";
            this.tlvAll.ShowGroups = false;
            this.tlvAll.Size = new System.Drawing.Size(425, 336);
            this.tlvAll.TabIndex = 2;
            this.tlvAll.UseCompatibleStateImageBehavior = false;
            this.tlvAll.UseFiltering = true;
            this.tlvAll.View = System.Windows.Forms.View.Details;
            this.tlvAll.VirtualMode = true;
            this.tlvAll.ItemActivate += new System.EventHandler(this.tlvAll_ItemActivate);
            // 
            // tlvAllClmName
            // 
            this.tlvAllClmName.AspectName = "Name";
            this.tlvAllClmName.Text = "Name";
            this.tlvAllClmName.Width = 280;
            // 
            // tlvAllClmCount
            // 
            this.tlvAllClmCount.AspectName = "Count";
            this.tlvAllClmCount.Searchable = false;
            this.tlvAllClmCount.Text = "Item count";
            this.tlvAllClmCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tlvAllClmCount.Width = 77;
            // 
            // statusStrip
            // 
            this.statusStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(920, 442);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(119, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.serverToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingDebugToolStripMenuItem,
            this.loggingEnableToolStripMenuItem,
            this.toolStripSeparator2,
            this.viewLogToolStripMenuItem,
            this.openLogWindowStripMenuItem});
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.loggingToolStripMenuItem.Text = "Logging";
            // 
            // loggingDebugToolStripMenuItem
            // 
            this.loggingDebugToolStripMenuItem.Checked = true;
            this.loggingDebugToolStripMenuItem.CheckOnClick = true;
            this.loggingDebugToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loggingDebugToolStripMenuItem.Name = "loggingDebugToolStripMenuItem";
            this.loggingDebugToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loggingDebugToolStripMenuItem.Text = "Debug logging";
            this.loggingDebugToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // loggingEnableToolStripMenuItem
            // 
            this.loggingEnableToolStripMenuItem.Checked = true;
            this.loggingEnableToolStripMenuItem.CheckOnClick = true;
            this.loggingEnableToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loggingEnableToolStripMenuItem.Name = "loggingEnableToolStripMenuItem";
            this.loggingEnableToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loggingEnableToolStripMenuItem.Text = "Enable";
            this.loggingEnableToolStripMenuItem.CheckedChanged += new System.EventHandler(this.loggingEnableToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(176, 6);
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.viewLogToolStripMenuItem.Text = "Open log file";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // openLogWindowStripMenuItem
            // 
            this.openLogWindowStripMenuItem.Name = "openLogWindowStripMenuItem";
            this.openLogWindowStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openLogWindowStripMenuItem.Text = "Open log window";
            this.openLogWindowStripMenuItem.Click += new System.EventHandler(this.openLogWindowStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCookiesToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteSavedCookiesToolStripMenuItem,
            this.deleteSavedCredentailsToolStripMenuItem,
            this.resetAllToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // saveCookiesToolStripMenuItem
            // 
            this.saveCookiesToolStripMenuItem.CheckOnClick = true;
            this.saveCookiesToolStripMenuItem.Name = "saveCookiesToolStripMenuItem";
            this.saveCookiesToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.saveCookiesToolStripMenuItem.Text = "Save cookies";
            this.saveCookiesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // deleteSavedCookiesToolStripMenuItem
            // 
            this.deleteSavedCookiesToolStripMenuItem.Name = "deleteSavedCookiesToolStripMenuItem";
            this.deleteSavedCookiesToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.deleteSavedCookiesToolStripMenuItem.Text = "Delete saved cookies";
            this.deleteSavedCookiesToolStripMenuItem.Click += new System.EventHandler(this.deleteSavedCookiesToolStripMenuItem_Click);
            // 
            // deleteSavedCredentailsToolStripMenuItem
            // 
            this.deleteSavedCredentailsToolStripMenuItem.Name = "deleteSavedCredentailsToolStripMenuItem";
            this.deleteSavedCredentailsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.deleteSavedCredentailsToolStripMenuItem.Text = "Delete saved credentails";
            this.deleteSavedCredentailsToolStripMenuItem.Click += new System.EventHandler(this.deleteSavedCredentailsToolStripMenuItem_Click);
            // 
            // resetAllToolStripMenuItem
            // 
            this.resetAllToolStripMenuItem.Name = "resetAllToolStripMenuItem";
            this.resetAllToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.resetAllToolStripMenuItem.Text = "Reset all";
            this.resetAllToolStripMenuItem.Click += new System.EventHandler(this.resetAllToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCatalogDetailsToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // saveCatalogDetailsToolStripMenuItem
            // 
            this.saveCatalogDetailsToolStripMenuItem.CheckOnClick = true;
            this.saveCatalogDetailsToolStripMenuItem.Name = "saveCatalogDetailsToolStripMenuItem";
            this.saveCatalogDetailsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.saveCatalogDetailsToolStripMenuItem.Text = "Save catalog details";
            this.saveCatalogDetailsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // NetworkStatusStrip
            // 
            this.NetworkStatusStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownSetOnline,
            this.DropDownForceOffline,
            this.toolStripSeparator3,
            this.offlineByDefaultToolStripMenuItem});
            this.NetworkStatusStrip.Name = "NetworkStatusStrip";
            this.NetworkStatusStrip.Size = new System.Drawing.Size(107, 21);
            this.NetworkStatusStrip.Text = "Network status";
            // 
            // DropDownSetOnline
            // 
            this.DropDownSetOnline.Name = "DropDownSetOnline";
            this.DropDownSetOnline.Size = new System.Drawing.Size(176, 22);
            this.DropDownSetOnline.Text = "Check online";
            this.DropDownSetOnline.Click += new System.EventHandler(this.DropDownSetOnline_Click);
            // 
            // DropDownForceOffline
            // 
            this.DropDownForceOffline.Name = "DropDownForceOffline";
            this.DropDownForceOffline.Size = new System.Drawing.Size(176, 22);
            this.DropDownForceOffline.Text = "Force offline";
            this.DropDownForceOffline.Click += new System.EventHandler(this.DropDownForceOffline_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // offlineByDefaultToolStripMenuItem
            // 
            this.offlineByDefaultToolStripMenuItem.CheckOnClick = true;
            this.offlineByDefaultToolStripMenuItem.Name = "offlineByDefaultToolStripMenuItem";
            this.offlineByDefaultToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.offlineByDefaultToolStripMenuItem.Text = "Offline by default";
            this.offlineByDefaultToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // menuStrip
            // 
            this.menuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.NetworkStatusStrip});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(771, 30);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(181, 25);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearch.Location = new System.Drawing.Point(62, 421);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 424);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search:";
            // 
            // LectureSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 445);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.TLPSplit);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "LectureSelector";
            this.Text = "LectureSelector";
            this.Load += new System.EventHandler(this.LectureSelector_Load);
            this.TLPSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvPresentations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAll)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPSplit;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private BrightIdeasSoftware.ObjectListView olvPresentations;
        private BrightIdeasSoftware.OLVColumn olvPresClmName;
        private BrightIdeasSoftware.OLVColumn olvPresClmDate;
        private BrightIdeasSoftware.OLVColumn olvPresClmDuration;
        private BrightIdeasSoftware.OLVColumn olvPresClmViews;
        private BrightIdeasSoftware.OLVColumn olvPresClmDesc;
        private BrightIdeasSoftware.TreeListView tlvAll;
        private BrightIdeasSoftware.OLVColumn tlvAllClmName;
        private BrightIdeasSoftware.OLVColumn tlvAllClmCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        public System.ComponentModel.BackgroundWorker bgwLogin;
        private System.ComponentModel.BackgroundWorker bgwCatalogLoader;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogWindowStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCookiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteSavedCookiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSavedCredentailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCatalogDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NetworkStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem DropDownSetOnline;
        private System.Windows.Forms.ToolStripMenuItem DropDownForceOffline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem offlineByDefaultToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}