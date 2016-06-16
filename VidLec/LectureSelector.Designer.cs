﻿namespace VidLec
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
            this.spacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
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
            this.bgwLogin = new System.ComponentModel.BackgroundWorker();
            this.bgwCatalogLoader = new System.ComponentModel.BackgroundWorker();
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
            this.TLPSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPSplit.Controls.Add(this.olvPresentations, 1, 0);
            this.TLPSplit.Controls.Add(this.tlvAll, 0, 0);
            this.TLPSplit.Location = new System.Drawing.Point(12, 27);
            this.TLPSplit.Name = "TLPSplit";
            this.TLPSplit.RowCount = 1;
            this.TLPSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TLPSplit.Size = new System.Drawing.Size(838, 342);
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
            this.olvPresentations.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.olvPresentations.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.olvPresentations.Location = new System.Drawing.Point(422, 4);
            this.olvPresentations.Name = "olvPresentations";
            this.olvPresentations.Size = new System.Drawing.Size(412, 334);
            this.olvPresentations.TabIndex = 1;
            this.olvPresentations.UseCompatibleStateImageBehavior = false;
            this.olvPresentations.View = System.Windows.Forms.View.Details;
            // 
            // olvPresClmName
            // 
            this.olvPresClmName.AspectName = "Name";
            this.olvPresClmName.Text = "Name";
            // 
            // olvPresClmDate
            // 
            this.olvPresClmDate.AspectName = "FullStartDate";
            this.olvPresClmDate.Text = "Date";
            this.olvPresClmDate.Width = 102;
            // 
            // olvPresClmDuration
            // 
            this.olvPresClmDuration.AspectName = "DurationDisplay";
            this.olvPresClmDuration.Text = "Duration";
            this.olvPresClmDuration.Width = 54;
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
            this.tlvAll.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.tlvAll.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.tlvAll.Location = new System.Drawing.Point(4, 4);
            this.tlvAll.Name = "tlvAll";
            this.tlvAll.ShowGroups = false;
            this.tlvAll.Size = new System.Drawing.Size(411, 334);
            this.tlvAll.TabIndex = 2;
            this.tlvAll.UseCompatibleStateImageBehavior = false;
            this.tlvAll.View = System.Windows.Forms.View.Details;
            this.tlvAll.VirtualMode = true;
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
            this.tlvAllClmCount.Text = "Item count";
            this.tlvAllClmCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tlvAllClmCount.Width = 77;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.spacer,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 372);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(862, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // spacer
            // 
            this.spacer.Name = "spacer";
            this.spacer.Size = new System.Drawing.Size(745, 17);
            this.spacer.Spring = true;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.NetworkStatusStrip});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(862, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loggingToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.serverToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
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
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loggingToolStripMenuItem.Text = "Logging";
            // 
            // loggingDebugToolStripMenuItem
            // 
            this.loggingDebugToolStripMenuItem.Checked = true;
            this.loggingDebugToolStripMenuItem.CheckOnClick = true;
            this.loggingDebugToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loggingDebugToolStripMenuItem.Name = "loggingDebugToolStripMenuItem";
            this.loggingDebugToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loggingDebugToolStripMenuItem.Text = "Debug logging";
            this.loggingDebugToolStripMenuItem.CheckedChanged += new System.EventHandler(this.loggingDebugToolStripMenuItem_CheckedChanged);
            // 
            // loggingEnableToolStripMenuItem
            // 
            this.loggingEnableToolStripMenuItem.Checked = true;
            this.loggingEnableToolStripMenuItem.CheckOnClick = true;
            this.loggingEnableToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loggingEnableToolStripMenuItem.Name = "loggingEnableToolStripMenuItem";
            this.loggingEnableToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loggingEnableToolStripMenuItem.Text = "Enable";
            this.loggingEnableToolStripMenuItem.CheckedChanged += new System.EventHandler(this.loggingEnableToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.viewLogToolStripMenuItem.Text = "Open log file";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // openLogWindowStripMenuItem
            // 
            this.openLogWindowStripMenuItem.Name = "openLogWindowStripMenuItem";
            this.openLogWindowStripMenuItem.Size = new System.Drawing.Size(168, 22);
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
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // saveCookiesToolStripMenuItem
            // 
            this.saveCookiesToolStripMenuItem.CheckOnClick = true;
            this.saveCookiesToolStripMenuItem.Name = "saveCookiesToolStripMenuItem";
            this.saveCookiesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.saveCookiesToolStripMenuItem.Text = "Save cookies";
            this.saveCookiesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.saveCookiesToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // deleteSavedCookiesToolStripMenuItem
            // 
            this.deleteSavedCookiesToolStripMenuItem.Name = "deleteSavedCookiesToolStripMenuItem";
            this.deleteSavedCookiesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.deleteSavedCookiesToolStripMenuItem.Text = "Delete saved cookies";
            this.deleteSavedCookiesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.deleteSavedCookiesToolStripMenuItem_CheckedChanged);
            // 
            // deleteSavedCredentailsToolStripMenuItem
            // 
            this.deleteSavedCredentailsToolStripMenuItem.Name = "deleteSavedCredentailsToolStripMenuItem";
            this.deleteSavedCredentailsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.deleteSavedCredentailsToolStripMenuItem.Text = "Delete saved credentails";
            this.deleteSavedCredentailsToolStripMenuItem.Click += new System.EventHandler(this.deleteSavedCredentailsToolStripMenuItem_Click);
            // 
            // resetAllToolStripMenuItem
            // 
            this.resetAllToolStripMenuItem.Name = "resetAllToolStripMenuItem";
            this.resetAllToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.resetAllToolStripMenuItem.Text = "Reset all";
            this.resetAllToolStripMenuItem.Click += new System.EventHandler(this.resetAllToolStripMenuItem_Click);
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCatalogDetailsToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // saveCatalogDetailsToolStripMenuItem
            // 
            this.saveCatalogDetailsToolStripMenuItem.CheckOnClick = true;
            this.saveCatalogDetailsToolStripMenuItem.Name = "saveCatalogDetailsToolStripMenuItem";
            this.saveCatalogDetailsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveCatalogDetailsToolStripMenuItem.Text = "Save catalog details";
            this.saveCatalogDetailsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.saveCatalogDetailsToolStripMenuItem_CheckedChanged);
            // 
            // NetworkStatusStrip
            // 
            this.NetworkStatusStrip.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.NetworkStatusStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownSetOnline,
            this.DropDownForceOffline,
            this.toolStripSeparator3,
            this.offlineByDefaultToolStripMenuItem});
            this.NetworkStatusStrip.Name = "NetworkStatusStrip";
            this.NetworkStatusStrip.Size = new System.Drawing.Size(98, 20);
            this.NetworkStatusStrip.Text = "Network status";
            // 
            // DropDownSetOnline
            // 
            this.DropDownSetOnline.Name = "DropDownSetOnline";
            this.DropDownSetOnline.Size = new System.Drawing.Size(166, 22);
            this.DropDownSetOnline.Text = "Check online";
            this.DropDownSetOnline.Click += new System.EventHandler(this.DropDownSetOnline_Click);
            // 
            // DropDownForceOffline
            // 
            this.DropDownForceOffline.Name = "DropDownForceOffline";
            this.DropDownForceOffline.Size = new System.Drawing.Size(166, 22);
            this.DropDownForceOffline.Text = "Force offline";
            this.DropDownForceOffline.Click += new System.EventHandler(this.DropDownForceOffline_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(163, 6);
            // 
            // offlineByDefaultToolStripMenuItem
            // 
            this.offlineByDefaultToolStripMenuItem.CheckOnClick = true;
            this.offlineByDefaultToolStripMenuItem.Name = "offlineByDefaultToolStripMenuItem";
            this.offlineByDefaultToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.offlineByDefaultToolStripMenuItem.Text = "Offline by default";
            this.offlineByDefaultToolStripMenuItem.CheckedChanged += new System.EventHandler(this.offlineByDefaultToolStripMenuItem_CheckedChanged);
            // 
            // bgwLogin
            // 
            this.bgwLogin.WorkerReportsProgress = true;
            this.bgwLogin.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLogin_DoWork);
            this.bgwLogin.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwLogin_ProgressChanged);
            this.bgwLogin.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLogin_RunWorkerCompleted);
            // 
            // bgwCatalogLoader
            // 
            this.bgwCatalogLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCatalogLoader_DoWork);
            this.bgwCatalogLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCatalogLoader_RunWorkerCompleted);
            // 
            // LectureSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 394);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.TLPSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "LectureSelector";
            this.Text = "LectureSelector";
            this.Load += new System.EventHandler(this.LectureSelector_Load);
            this.Shown += new System.EventHandler(this.LectureSelector_Shown);
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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView olvPresentations;
        private BrightIdeasSoftware.OLVColumn olvPresClmName;
        private BrightIdeasSoftware.OLVColumn olvPresClmDate;
        private BrightIdeasSoftware.OLVColumn olvPresClmDuration;
        private BrightIdeasSoftware.OLVColumn olvPresClmViews;
        private BrightIdeasSoftware.OLVColumn olvPresClmDesc;
        private BrightIdeasSoftware.TreeListView tlvAll;
        private BrightIdeasSoftware.OLVColumn tlvAllClmName;
        private BrightIdeasSoftware.OLVColumn tlvAllClmCount;
        private System.Windows.Forms.ToolStripStatusLabel spacer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem NetworkStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem DropDownSetOnline;
        private System.Windows.Forms.ToolStripMenuItem DropDownForceOffline;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingEnableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogWindowStripMenuItem;
        public System.ComponentModel.BackgroundWorker bgwLogin;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveCookiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteSavedCookiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSavedCredentailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem offlineByDefaultToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwCatalogLoader;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCatalogDetailsToolStripMenuItem;
    }
}