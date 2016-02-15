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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TLPSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvPresentations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAll)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
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
            // LectureSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 394);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.TLPSplit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LectureSelector";
            this.Text = "LectureSelector";
            this.TLPSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvPresentations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tlvAll)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPSplit;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
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
    }
}