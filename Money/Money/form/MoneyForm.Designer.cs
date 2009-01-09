namespace Money.form
{
    partial class MoneyForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoneyForm));
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.tlpHome = new System.Windows.Forms.TableLayoutPanel();
            this.gbBalance = new System.Windows.Forms.GroupBox();
            this.treeListView = new Money.control.MyTreeListView();
            this.colCategory = new BrightIdeasSoftware.OLVColumn();
            this.colUserName = new BrightIdeasSoftware.OLVColumn();
            this.colDate = new BrightIdeasSoftware.OLVColumn();
            this.colAmount = new BrightIdeasSoftware.OLVColumn();
            this.colRemarks = new BrightIdeasSoftware.OLVColumn();
            this.colAB = new BrightIdeasSoftware.OLVColumn();
            this.cmsHomeTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.tlpSta = new System.Windows.Forms.TableLayoutPanel();
            this.pieChart = new Nexus.Windows.Forms.PieChart();
            this.barChart = new Money.control.HBarChart();
            this.tpChart = new System.Windows.Forms.TabPage();
            this.zgcExpense = new ZedGraph.ZedGraphControl();
            this.imageListMainTab = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnMerge = new System.Windows.Forms.ToolStripButton();
            this.btnDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExpand = new System.Windows.Forms.ToolStripButton();
            this.btnCollapse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVacuum = new System.Windows.Forms.ToolStripButton();
            this.btnBackup = new System.Windows.Forms.ToolStripButton();
            this.btnOption = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.btnShowDate = new System.Windows.Forms.ToolStripButton();
            this.btnMonth = new System.Windows.Forms.ToolStripButton();
            this.btnPrev = new System.Windows.Forms.ToolStripButton();
            this.tcbDate = new System.Windows.Forms.ToolStripComboBox();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tcbType = new System.Windows.Forms.ToolStripComboBox();
            this.tcbChartStartYear = new System.Windows.Forms.ToolStripComboBox();
            this.tslTo = new System.Windows.Forms.ToolStripLabel();
            this.tcbChartEndYear = new System.Windows.Forms.ToolStripComboBox();
            this.tcbChart = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tcMain.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.tlpHome.SuspendLayout();
            this.gbBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.cmsHomeTab.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.tlpSta.SuspendLayout();
            this.tpChart.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpHome);
            this.tcMain.Controls.Add(this.tpChart);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.ImageList = this.imageListMainTab;
            this.tcMain.ItemSize = new System.Drawing.Size(90, 22);
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(792, 519);
            this.tcMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.tlpHome);
            this.tpHome.ImageIndex = 0;
            this.tpHome.Location = new System.Drawing.Point(4, 26);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(1);
            this.tpHome.Size = new System.Drawing.Size(784, 489);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "Home";
            this.tpHome.UseVisualStyleBackColor = true;
            // 
            // tlpHome
            // 
            this.tlpHome.ColumnCount = 2;
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.5F));
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5F));
            this.tlpHome.Controls.Add(this.gbBalance, 0, 0);
            this.tlpHome.Controls.Add(this.gbStatistics, 1, 0);
            this.tlpHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHome.Location = new System.Drawing.Point(1, 1);
            this.tlpHome.Name = "tlpHome";
            this.tlpHome.RowCount = 1;
            this.tlpHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHome.Size = new System.Drawing.Size(782, 487);
            this.tlpHome.TabIndex = 1;
            // 
            // gbBalance
            // 
            this.gbBalance.Controls.Add(this.treeListView);
            this.gbBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBalance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gbBalance.Location = new System.Drawing.Point(3, 3);
            this.gbBalance.Name = "gbBalance";
            this.gbBalance.Size = new System.Drawing.Size(560, 481);
            this.gbBalance.TabIndex = 0;
            this.gbBalance.TabStop = false;
            this.gbBalance.Text = "Balance";
            // 
            // treeListView
            // 
            this.treeListView.AllColumns.Add(this.colCategory);
            this.treeListView.AllColumns.Add(this.colUserName);
            this.treeListView.AllColumns.Add(this.colDate);
            this.treeListView.AllColumns.Add(this.colAmount);
            this.treeListView.AllColumns.Add(this.colRemarks);
            this.treeListView.AllColumns.Add(this.colAB);
            this.treeListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.treeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCategory,
            this.colUserName,
            this.colDate,
            this.colAmount,
            this.colRemarks,
            this.colAB});
            this.treeListView.ContextMenuStrip = this.cmsHomeTab;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.FullRowSelect = true;
            this.treeListView.HideSelection = false;
            this.treeListView.Location = new System.Drawing.Point(3, 19);
            this.treeListView.Name = "treeListView";
            this.treeListView.OwnerDraw = true;
            this.treeListView.RowHeight = 18;
            this.treeListView.ShowGroups = false;
            this.treeListView.Size = new System.Drawing.Size(554, 459);
            this.treeListView.TabIndex = 4;
            this.treeListView.UseAlternatingBackColors = true;
            this.treeListView.UseCompatibleStateImageBehavior = false;
            this.treeListView.View = System.Windows.Forms.View.Details;
            this.treeListView.VirtualMode = true;
            this.treeListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.List_KeyDown);
            this.treeListView.ItemActivate += new System.EventHandler(this.treeListView_ItemActivate);
            this.treeListView.SelectionChanged += new System.EventHandler(this.treeListView_SelectionChanged);
            this.treeListView.DoubleClick += new System.EventHandler(this.List_DoubleClick);
            // 
            // colCategory
            // 
            this.colCategory.FillsFreeSpace = true;
            this.colCategory.MinimumWidth = 180;
            this.colCategory.Text = "Category";
            this.colCategory.Width = 180;
            // 
            // colUserName
            // 
            this.colUserName.FillsFreeSpace = true;
            this.colUserName.MaximumWidth = 120;
            this.colUserName.MinimumWidth = 90;
            this.colUserName.Text = "Name";
            this.colUserName.Width = 90;
            // 
            // colDate
            // 
            this.colDate.FillsFreeSpace = true;
            this.colDate.IsEditable = false;
            this.colDate.MaximumWidth = 120;
            this.colDate.MinimumWidth = 80;
            this.colDate.Text = "Date";
            this.colDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colDate.Width = 80;
            // 
            // colAmount
            // 
            this.colAmount.FillsFreeSpace = true;
            this.colAmount.IsEditable = false;
            this.colAmount.MaximumWidth = 150;
            this.colAmount.MinimumWidth = 75;
            this.colAmount.Text = "Amount";
            this.colAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAmount.Width = 75;
            // 
            // colRemarks
            // 
            this.colRemarks.FillsFreeSpace = true;
            this.colRemarks.IsEditable = false;
            this.colRemarks.MinimumWidth = 80;
            this.colRemarks.Text = "Remarks";
            this.colRemarks.Width = 80;
            // 
            // colAB
            // 
            this.colAB.MaximumWidth = 24;
            this.colAB.MinimumWidth = 24;
            this.colAB.Text = "Annual";
            this.colAB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAB.Width = 24;
            // 
            // cmsHomeTab
            // 
            this.cmsHomeTab.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmsHomeTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.mergeToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.expandToolStripMenuItem,
            this.collapseToolStripMenuItem});
            this.cmsHomeTab.Name = "cmsHomeTab";
            this.cmsHomeTab.Size = new System.Drawing.Size(118, 120);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::Money.Properties.Resources.Edit_16;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Image = global::Money.Properties.Resources.Merge_16;
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.mergeToolStripMenuItem.Text = "&Merge";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Money.Properties.Resources.Delete_16;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(114, 6);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("expandToolStripMenuItem.Image")));
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.expandToolStripMenuItem.Text = "Ex&pand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("collapseToolStripMenuItem.Image")));
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // gbStatistics
            // 
            this.gbStatistics.Controls.Add(this.tlpSta);
            this.gbStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStatistics.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.gbStatistics.Location = new System.Drawing.Point(569, 3);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(210, 481);
            this.gbStatistics.TabIndex = 0;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
            // 
            // tlpSta
            // 
            this.tlpSta.ColumnCount = 1;
            this.tlpSta.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSta.Controls.Add(this.pieChart, 0, 0);
            this.tlpSta.Controls.Add(this.barChart, 0, 1);
            this.tlpSta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSta.Location = new System.Drawing.Point(3, 19);
            this.tlpSta.Name = "tlpSta";
            this.tlpSta.RowCount = 2;
            this.tlpSta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tlpSta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tlpSta.Size = new System.Drawing.Size(204, 459);
            this.tlpSta.TabIndex = 0;
            // 
            // pieChart
            // 
            this.pieChart.AutoSizePie = true;
            this.pieChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pieChart.Location = new System.Drawing.Point(3, 3);
            this.pieChart.Name = "pieChart";
            this.pieChart.Radius = 200F;
            this.pieChart.Size = new System.Drawing.Size(198, 140);
            this.pieChart.TabIndex = 0;
            this.pieChart.Thickness = 30F;
            this.pieChart.ItemClicked += new Nexus.Windows.Forms.PieChartItemEventHandler(this.pieChart_ItemClicked);
            // 
            // barChart
            // 
            this.barChart.Background.GradientColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.barChart.Background.GradientColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.barChart.Background.PaintingMode = Money.control.CBackgroundProperty.PaintingModes.RadialGradient;
            this.barChart.Background.SolidColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(30)))), ((int)(((byte)(90)))));
            this.barChart.BarOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.barChart.Border.BoundRect = ((System.Drawing.RectangleF)(resources.GetObject("resource.BoundRect")));
            this.barChart.Border.Color = System.Drawing.Color.White;
            this.barChart.Border.Visible = true;
            this.barChart.Border.Width = 1;
            this.barChart.Description.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.barChart.Description.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.barChart.Description.FontDefaultSize = 12F;
            this.barChart.Description.Text = "Detail";
            this.barChart.Description.Visible = false;
            this.barChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barChart.Items.BarOrientation = System.Windows.Forms.Orientation.Horizontal;
            this.barChart.Items.DefaultWidth = 0;
            this.barChart.Items.DrawingMode = Money.control.HBarItems.DrawingModes.Glass;
            this.barChart.Items.ShouldReCalculate = false;
            this.barChart.Label.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.barChart.Label.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.barChart.Label.FontDefaultSize = 8F;
            this.barChart.Label.Visible = true;
            this.barChart.Location = new System.Drawing.Point(3, 149);
            this.barChart.Name = "barChart";
            this.barChart.Shadow.ColorInner = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.barChart.Shadow.ColorOuter = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.barChart.Shadow.Mode = Money.control.CShadowProperty.Modes.Inner;
            this.barChart.Shadow.WidthInner = 5;
            this.barChart.Shadow.WidthOuter = 5;
            this.barChart.Size = new System.Drawing.Size(198, 307);
            this.barChart.SizingMode = Money.control.HBarChart.BarSizingMode.AutoScale;
            this.barChart.TabIndex = 1;
            this.barChart.Values.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.barChart.Values.Font = new System.Drawing.Font("Tahoma", 7F);
            this.barChart.Values.FontDefaultSize = 7F;
            this.barChart.Values.Mode = Money.control.CValueProperty.ValueMode.Percent;
            this.barChart.Values.Visible = true;
            // 
            // tpChart
            // 
            this.tpChart.Controls.Add(this.zgcExpense);
            this.tpChart.ImageIndex = 1;
            this.tpChart.Location = new System.Drawing.Point(4, 26);
            this.tpChart.Name = "tpChart";
            this.tpChart.Padding = new System.Windows.Forms.Padding(3);
            this.tpChart.Size = new System.Drawing.Size(784, 488);
            this.tpChart.TabIndex = 1;
            this.tpChart.Text = "Chart";
            this.tpChart.UseVisualStyleBackColor = true;
            // 
            // zgcExpense
            // 
            this.zgcExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgcExpense.IsShowPointValues = true;
            this.zgcExpense.Location = new System.Drawing.Point(3, 3);
            this.zgcExpense.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.zgcExpense.Name = "zgcExpense";
            this.zgcExpense.ScrollGrace = 0;
            this.zgcExpense.ScrollMaxX = 0;
            this.zgcExpense.ScrollMaxY = 0;
            this.zgcExpense.ScrollMaxY2 = 0;
            this.zgcExpense.ScrollMinX = 0;
            this.zgcExpense.ScrollMinY = 0;
            this.zgcExpense.ScrollMinY2 = 0;
            this.zgcExpense.Size = new System.Drawing.Size(778, 482);
            this.zgcExpense.TabIndex = 8;
            // 
            // imageListMainTab
            // 
            this.imageListMainTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMainTab.ImageStream")));
            this.imageListMainTab.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMainTab.Images.SetKeyName(0, "Home_16.png");
            this.imageListMainTab.Images.SetKeyName(1, "Status_16.png");
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 104;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(116, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnEdit,
            this.btnMerge,
            this.btnDel,
            this.toolStripSeparator5,
            this.btnExpand,
            this.btnCollapse,
            this.toolStripSeparator6,
            this.btnVacuum,
            this.btnBackup,
            this.btnOption,
            this.toolStripSeparator7,
            this.btnExit,
            this.btnAbout,
            this.btnShowDate,
            this.btnMonth,
            this.btnPrev,
            this.tcbDate,
            this.btnNext,
            this.toolStripSeparator2,
            this.tcbType,
            this.tcbChartStartYear,
            this.tslTo,
            this.tcbChartEndYear,
            this.tcbChart});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(792, 25);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 105;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Money.Properties.Resources.Add_16;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNewBill_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::Money.Properties.Resources.Edit_16;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(23, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMerge.Image = global::Money.Properties.Resources.Merge_16;
            this.btnMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(23, 22);
            this.btnMerge.Text = "Merge";
            this.btnMerge.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnDel
            // 
            this.btnDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDel.Image = global::Money.Properties.Resources.Delete_16;
            this.btnDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(23, 22);
            this.btnDel.Text = "Delete";
            this.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExpand
            // 
            this.btnExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
            this.btnExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(23, 22);
            this.btnExpand.Text = "Expand";
            this.btnExpand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExpand.ToolTipText = "Expand Group";
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
            // 
            // btnCollapse
            // 
            this.btnCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapse.Image")));
            this.btnCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(23, 22);
            this.btnCollapse.Text = "Collapse";
            this.btnCollapse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCollapse.ToolTipText = "Collapse Group";
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnVacuum
            // 
            this.btnVacuum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVacuum.Image = global::Money.Properties.Resources.Optimize_16;
            this.btnVacuum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVacuum.Name = "btnVacuum";
            this.btnVacuum.Size = new System.Drawing.Size(23, 22);
            this.btnVacuum.Text = "Vacuum";
            this.btnVacuum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVacuum.ToolTipText = "Database Vacuum";
            this.btnVacuum.Click += new System.EventHandler(this.btnVacuum_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackup.Image = global::Money.Properties.Resources.Database_16;
            this.btnBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(23, 22);
            this.btnBackup.Text = "Backup";
            this.btnBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBackup.ToolTipText = "Database Backup";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnOption
            // 
            this.btnOption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOption.Image = global::Money.Properties.Resources.Tools_16;
            this.btnOption.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(23, 22);
            this.btnOption.Text = "Options";
            this.btnOption.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExit
            // 
            this.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = global::Money.Properties.Resources.Exit_16;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(23, 23);
            this.btnExit.Text = "Exit";
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbout.Image = global::Money.Properties.Resources.Info_16;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(23, 22);
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnShowDate
            // 
            this.btnShowDate.Checked = true;
            this.btnShowDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowDate.Image = global::Money.Properties.Resources.Calendar_16;
            this.btnShowDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowDate.Name = "btnShowDate";
            this.btnShowDate.Size = new System.Drawing.Size(23, 22);
            this.btnShowDate.Text = "Show Date";
            this.btnShowDate.Click += new System.EventHandler(this.btnShowDate_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Checked = true;
            this.btnMonth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnMonth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMonth.Image = ((System.Drawing.Image)(resources.GetObject("btnMonth.Image")));
            this.btnMonth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(46, 22);
            this.btnMonth.Text = "Month";
            this.btnMonth.ToolTipText = "Show with Month";
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrev.Image = global::Money.Properties.Resources.Back_16;
            this.btnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(23, 22);
            this.btnPrev.Text = "Previous";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // tcbDate
            // 
            this.tcbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcbDate.Name = "tcbDate";
            this.tcbDate.Size = new System.Drawing.Size(75, 25);
            this.tcbDate.SelectedIndexChanged += new System.EventHandler(this.tcbDate_SelectedIndexChanged);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::Money.Properties.Resources.Forward_16;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 22);
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tcbType
            // 
            this.tcbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tcbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tcbType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcbType.Items.AddRange(new object[] {
            "Expense",
            "Income",
            "Annual"});
            this.tcbType.Name = "tcbType";
            this.tcbType.Size = new System.Drawing.Size(200, 25);
            this.tcbType.SelectedIndexChanged += new System.EventHandler(this.tcbType_SelectedIndexChanged);
            this.tcbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tcbType_KeyDown);
            // 
            // tcbChartStartYear
            // 
            this.tcbChartStartYear.AutoSize = false;
            this.tcbChartStartYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbChartStartYear.DropDownWidth = 55;
            this.tcbChartStartYear.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tcbChartStartYear.Name = "tcbChartStartYear";
            this.tcbChartStartYear.Size = new System.Drawing.Size(55, 22);
            this.tcbChartStartYear.Visible = false;
            this.tcbChartStartYear.SelectedIndexChanged += new System.EventHandler(this.tcbChartYear_SelectedIndexChanged);
            // 
            // tslTo
            // 
            this.tslTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslTo.Name = "tslTo";
            this.tslTo.Size = new System.Drawing.Size(19, 23);
            this.tslTo.Text = " - ";
            this.tslTo.Visible = false;
            // 
            // tcbChartEndYear
            // 
            this.tcbChartEndYear.AutoSize = false;
            this.tcbChartEndYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbChartEndYear.DropDownWidth = 55;
            this.tcbChartEndYear.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tcbChartEndYear.Name = "tcbChartEndYear";
            this.tcbChartEndYear.Size = new System.Drawing.Size(55, 22);
            this.tcbChartEndYear.Visible = false;
            this.tcbChartEndYear.SelectedIndexChanged += new System.EventHandler(this.tcbChartYear_SelectedIndexChanged);
            // 
            // tcbChart
            // 
            this.tcbChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tcbChart.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcbChart.Name = "tcbChart";
            this.tcbChart.Size = new System.Drawing.Size(240, 22);
            this.tcbChart.Visible = false;
            this.tcbChart.SelectedIndexChanged += new System.EventHandler(this.tcbChart_SelectedIndexChanged);
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tcMain);
            this.toolStripContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(792, 519);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(792, 566);
            this.toolStripContainer.TabIndex = 106;
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
            // 
            // MoneyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.toolStripContainer);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoneyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoneyForm";
            this.tcMain.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tlpHome.ResumeLayout(false);
            this.gbBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.cmsHomeTab.ResumeLayout(false);
            this.gbStatistics.ResumeLayout(false);
            this.tlpSta.ResumeLayout(false);
            this.tpChart.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.TabPage tpChart;
        private ZedGraph.ZedGraphControl zgcExpense;
        private System.Windows.Forms.ContextMenuStrip cmsHomeTab;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnMerge;
        private System.Windows.Forms.ToolStripButton btnDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnExpand;
        private System.Windows.Forms.ToolStripButton btnCollapse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnOption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnVacuum;
        private System.Windows.Forms.ToolStripButton btnBackup;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton btnShowDate;
        private System.Windows.Forms.ToolStripButton btnPrev;
        private System.Windows.Forms.ToolStripComboBox tcbDate;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnMonth;
        private System.Windows.Forms.ToolStripComboBox tcbType;
        private System.Windows.Forms.ToolStripComboBox tcbChart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList imageListMainTab;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private Nexus.Windows.Forms.PieChart pieChart;
        private Money.control.HBarChart barChart;
        private System.Windows.Forms.GroupBox gbBalance;
        private System.Windows.Forms.GroupBox gbStatistics;
        private Money.control.MyTreeListView treeListView;
        private BrightIdeasSoftware.OLVColumn colCategory;
        private BrightIdeasSoftware.OLVColumn colUserName;
        private BrightIdeasSoftware.OLVColumn colDate;
        private BrightIdeasSoftware.OLVColumn colAmount;
        private BrightIdeasSoftware.OLVColumn colRemarks;
        private BrightIdeasSoftware.OLVColumn colAB;
        private System.Windows.Forms.TableLayoutPanel tlpHome;
        private System.Windows.Forms.TableLayoutPanel tlpSta;
        private System.Windows.Forms.ToolStripComboBox tcbChartStartYear;
        private System.Windows.Forms.ToolStripLabel tslTo;
        private System.Windows.Forms.ToolStripComboBox tcbChartEndYear;
    }
}