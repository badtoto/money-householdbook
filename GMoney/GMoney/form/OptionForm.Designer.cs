namespace GMoney.form
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tcOption = new System.Windows.Forms.TabControl();
            this.tpSecurity = new System.Windows.Forms.TabPage();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.btnUserNew = new System.Windows.Forms.Label();
            this.folvUser = new BrightIdeasSoftware.FastObjectListView();
            this.colUserId = new BrightIdeasSoftware.OLVColumn();
            this.colUserName = new BrightIdeasSoftware.OLVColumn();
            this.colUserCreateDate = new BrightIdeasSoftware.OLVColumn();
            this.colUserUpdateDate = new BrightIdeasSoftware.OLVColumn();
            this.colUserDeleted = new BrightIdeasSoftware.OLVColumn();
            this.labelBasicUser = new System.Windows.Forms.Label();
            this.cbUseLoginPass = new System.Windows.Forms.CheckBox();
            this.labelBasicLogin = new System.Windows.Forms.Label();
            this.tbNewPassConf = new System.Windows.Forms.TextBox();
            this.labelNewPassConf = new System.Windows.Forms.Label();
            this.tbNewPass = new System.Windows.Forms.TextBox();
            this.labelNewPass = new System.Windows.Forms.Label();
            this.tbOldPass = new System.Windows.Forms.TextBox();
            this.labelOldPass = new System.Windows.Forms.Label();
            this.tpFixed = new System.Windows.Forms.TabPage();
            this.frequencyCtl = new GMoney.control.FrequencyCtl();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.btnFixedDel = new System.Windows.Forms.Label();
            this.btnFixedNew = new System.Windows.Forms.Label();
            this.cbSub = new System.Windows.Forms.ComboBox();
            this.ctbAmount = new GMoney.control.NumericBox();
            this.folvFixed = new BrightIdeasSoftware.FastObjectListView();
            this.colFixUserName = new BrightIdeasSoftware.OLVColumn();
            this.colFixSub = new BrightIdeasSoftware.OLVColumn();
            this.colFixFrequency = new BrightIdeasSoftware.OLVColumn();
            this.colFixStartDate = new BrightIdeasSoftware.OLVColumn();
            this.colFixEndDate = new BrightIdeasSoftware.OLVColumn();
            this.colFixAmount = new BrightIdeasSoftware.OLVColumn();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelFixed = new System.Windows.Forms.Label();
            this.tpOptimal = new System.Windows.Forms.TabPage();
            this.folvOptimal = new BrightIdeasSoftware.FastObjectListView();
            this.colOptSub = new BrightIdeasSoftware.OLVColumn();
            this.colOptOptimal = new BrightIdeasSoftware.OLVColumn();
            this.colOptRange = new BrightIdeasSoftware.OLVColumn();
            this.labelOptimal = new System.Windows.Forms.Label();
            this.tcOption.SuspendLayout();
            this.tpSecurity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folvUser)).BeginInit();
            this.tpFixed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folvFixed)).BeginInit();
            this.cmsDelete.SuspendLayout();
            this.tpOptimal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folvOptimal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(199, 376);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(293, 376);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(387, 376);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tcOption
            // 
            this.tcOption.Controls.Add(this.tpSecurity);
            this.tcOption.Controls.Add(this.tpFixed);
            this.tcOption.Controls.Add(this.tpOptimal);
            this.tcOption.ItemSize = new System.Drawing.Size(75, 19);
            this.tcOption.Location = new System.Drawing.Point(6, 7);
            this.tcOption.Multiline = true;
            this.tcOption.Name = "tcOption";
            this.tcOption.SelectedIndex = 0;
            this.tcOption.Size = new System.Drawing.Size(664, 365);
            this.tcOption.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcOption.TabIndex = 0;
            this.tcOption.SelectedIndexChanged += new System.EventHandler(this.tcOption_SelectedIndexChanged);
            // 
            // tpSecurity
            // 
            this.tpSecurity.Controls.Add(this.tbUserName);
            this.tpSecurity.Controls.Add(this.btnUserNew);
            this.tpSecurity.Controls.Add(this.folvUser);
            this.tpSecurity.Controls.Add(this.labelBasicUser);
            this.tpSecurity.Controls.Add(this.cbUseLoginPass);
            this.tpSecurity.Controls.Add(this.labelBasicLogin);
            this.tpSecurity.Controls.Add(this.tbNewPassConf);
            this.tpSecurity.Controls.Add(this.labelNewPassConf);
            this.tpSecurity.Controls.Add(this.tbNewPass);
            this.tpSecurity.Controls.Add(this.labelNewPass);
            this.tpSecurity.Controls.Add(this.tbOldPass);
            this.tpSecurity.Controls.Add(this.labelOldPass);
            this.tpSecurity.Location = new System.Drawing.Point(4, 23);
            this.tpSecurity.Name = "tpSecurity";
            this.tpSecurity.Padding = new System.Windows.Forms.Padding(3);
            this.tpSecurity.Size = new System.Drawing.Size(656, 338);
            this.tpSecurity.TabIndex = 0;
            this.tpSecurity.Text = "Basic";
            this.tpSecurity.UseVisualStyleBackColor = true;
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(3, 174);
            this.tbUserName.MaxLength = 10;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(200, 22);
            this.tbUserName.TabIndex = 9;
            this.tbUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbUserName_KeyDown);
            this.tbUserName.Leave += new System.EventHandler(this.optionAcceptButtonOn);
            this.tbUserName.Enter += new System.EventHandler(this.optionAcceptButtonOff);
            // 
            // btnUserNew
            // 
            this.btnUserNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserNew.Image = global::GMoney.Properties.Resources.add_user_32;
            this.btnUserNew.Location = new System.Drawing.Point(208, 163);
            this.btnUserNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnUserNew.Name = "btnUserNew";
            this.btnUserNew.Size = new System.Drawing.Size(32, 32);
            this.btnUserNew.TabIndex = 10;
            this.btnUserNew.Click += new System.EventHandler(this.btnUserNew_Click);
            // 
            // folvUser
            // 
            this.folvUser.AllColumns.Add(this.colUserId);
            this.folvUser.AllColumns.Add(this.colUserName);
            this.folvUser.AllColumns.Add(this.colUserCreateDate);
            this.folvUser.AllColumns.Add(this.colUserUpdateDate);
            this.folvUser.AllColumns.Add(this.colUserDeleted);
            this.folvUser.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.folvUser.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.folvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUserId,
            this.colUserName,
            this.colUserCreateDate,
            this.colUserUpdateDate,
            this.colUserDeleted});
            this.folvUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.folvUser.FullRowSelect = true;
            this.folvUser.Location = new System.Drawing.Point(3, 198);
            this.folvUser.MultiSelect = false;
            this.folvUser.Name = "folvUser";
            this.folvUser.RowHeight = 22;
            this.folvUser.ShowGroups = false;
            this.folvUser.Size = new System.Drawing.Size(650, 137);
            this.folvUser.TabIndex = 11;
            this.folvUser.UseAlternatingBackColors = true;
            this.folvUser.UseCompatibleStateImageBehavior = false;
            this.folvUser.View = System.Windows.Forms.View.Details;
            this.folvUser.VirtualMode = true;
            this.folvUser.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditStarting);
            this.folvUser.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditFinishing);
            // 
            // colUserId
            // 
            this.colUserId.AspectName = "ID";
            this.colUserId.IsEditable = false;
            this.colUserId.MinimumWidth = 75;
            this.colUserId.Text = "ID";
            this.colUserId.Width = 75;
            // 
            // colUserName
            // 
            this.colUserName.AspectName = "Name";
            this.colUserName.FillsFreeSpace = true;
            this.colUserName.MinimumWidth = 180;
            this.colUserName.Text = "UserName";
            this.colUserName.Width = 180;
            // 
            // colUserCreateDate
            // 
            this.colUserCreateDate.AspectName = "CreateDate";
            this.colUserCreateDate.AspectToStringFormat = "{0:yyyy/MM/dd}";
            this.colUserCreateDate.IsEditable = false;
            this.colUserCreateDate.MinimumWidth = 100;
            this.colUserCreateDate.Text = "Create Date";
            this.colUserCreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colUserCreateDate.Width = 100;
            // 
            // colUserUpdateDate
            // 
            this.colUserUpdateDate.AspectName = "UpdateDate";
            this.colUserUpdateDate.AspectToStringFormat = "{0:yyyy/MM/dd}";
            this.colUserUpdateDate.IsEditable = false;
            this.colUserUpdateDate.MinimumWidth = 100;
            this.colUserUpdateDate.Text = "Update Date";
            this.colUserUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colUserUpdateDate.Width = 100;
            // 
            // colUserDeleted
            // 
            this.colUserDeleted.AspectName = "DeleteFlg";
            this.colUserDeleted.AspectToStringFormat = "";
            this.colUserDeleted.MinimumWidth = 100;
            this.colUserDeleted.Text = "Deleted";
            this.colUserDeleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colUserDeleted.Width = 100;
            // 
            // labelBasicUser
            // 
            this.labelBasicUser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelBasicUser.Location = new System.Drawing.Point(3, 151);
            this.labelBasicUser.Name = "labelBasicUser";
            this.labelBasicUser.Size = new System.Drawing.Size(166, 20);
            this.labelBasicUser.TabIndex = 8;
            this.labelBasicUser.Text = "User Manager";
            this.labelBasicUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbUseLoginPass
            // 
            this.cbUseLoginPass.AutoSize = true;
            this.cbUseLoginPass.Location = new System.Drawing.Point(6, 33);
            this.cbUseLoginPass.Name = "cbUseLoginPass";
            this.cbUseLoginPass.Size = new System.Drawing.Size(199, 18);
            this.cbUseLoginPass.TabIndex = 1;
            this.cbUseLoginPass.Text = "Use Login Password Protection.";
            this.cbUseLoginPass.UseVisualStyleBackColor = true;
            this.cbUseLoginPass.CheckedChanged += new System.EventHandler(this.cbUseLoginPass_CheckedChanged);
            // 
            // labelBasicLogin
            // 
            this.labelBasicLogin.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelBasicLogin.Location = new System.Drawing.Point(3, 10);
            this.labelBasicLogin.Name = "labelBasicLogin";
            this.labelBasicLogin.Size = new System.Drawing.Size(290, 20);
            this.labelBasicLogin.TabIndex = 0;
            this.labelBasicLogin.Text = "Please set your login password.";
            this.labelBasicLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbNewPassConf
            // 
            this.tbNewPassConf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbNewPassConf.Location = new System.Drawing.Point(194, 114);
            this.tbNewPassConf.MaxLength = 32;
            this.tbNewPassConf.Name = "tbNewPassConf";
            this.tbNewPassConf.Size = new System.Drawing.Size(160, 22);
            this.tbNewPassConf.TabIndex = 7;
            this.tbNewPassConf.UseSystemPasswordChar = true;
            this.tbNewPassConf.TextChanged += new System.EventHandler(this.tpSecurity_TextChanged);
            // 
            // labelNewPassConf
            // 
            this.labelNewPassConf.Location = new System.Drawing.Point(38, 114);
            this.labelNewPassConf.Name = "labelNewPassConf";
            this.labelNewPassConf.Size = new System.Drawing.Size(150, 20);
            this.labelNewPassConf.TabIndex = 6;
            this.labelNewPassConf.Text = "New Password Confirm:";
            this.labelNewPassConf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbNewPass
            // 
            this.tbNewPass.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbNewPass.Location = new System.Drawing.Point(194, 87);
            this.tbNewPass.MaxLength = 32;
            this.tbNewPass.Name = "tbNewPass";
            this.tbNewPass.Size = new System.Drawing.Size(160, 22);
            this.tbNewPass.TabIndex = 5;
            this.tbNewPass.UseSystemPasswordChar = true;
            this.tbNewPass.TextChanged += new System.EventHandler(this.tpSecurity_TextChanged);
            // 
            // labelNewPass
            // 
            this.labelNewPass.Location = new System.Drawing.Point(38, 87);
            this.labelNewPass.Name = "labelNewPass";
            this.labelNewPass.Size = new System.Drawing.Size(150, 20);
            this.labelNewPass.TabIndex = 4;
            this.labelNewPass.Text = "New Password:";
            this.labelNewPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbOldPass
            // 
            this.tbOldPass.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbOldPass.Location = new System.Drawing.Point(194, 61);
            this.tbOldPass.MaxLength = 32;
            this.tbOldPass.Name = "tbOldPass";
            this.tbOldPass.Size = new System.Drawing.Size(160, 22);
            this.tbOldPass.TabIndex = 3;
            this.tbOldPass.UseSystemPasswordChar = true;
            this.tbOldPass.TextChanged += new System.EventHandler(this.tpSecurity_TextChanged);
            // 
            // labelOldPass
            // 
            this.labelOldPass.Location = new System.Drawing.Point(38, 61);
            this.labelOldPass.Name = "labelOldPass";
            this.labelOldPass.Size = new System.Drawing.Size(150, 20);
            this.labelOldPass.TabIndex = 2;
            this.labelOldPass.Text = "Old Password:";
            this.labelOldPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tpFixed
            // 
            this.tpFixed.Controls.Add(this.frequencyCtl);
            this.tpFixed.Controls.Add(this.cbUser);
            this.tpFixed.Controls.Add(this.btnFixedDel);
            this.tpFixed.Controls.Add(this.btnFixedNew);
            this.tpFixed.Controls.Add(this.cbSub);
            this.tpFixed.Controls.Add(this.ctbAmount);
            this.tpFixed.Controls.Add(this.folvFixed);
            this.tpFixed.Controls.Add(this.labelFixed);
            this.tpFixed.Location = new System.Drawing.Point(4, 23);
            this.tpFixed.Name = "tpFixed";
            this.tpFixed.Padding = new System.Windows.Forms.Padding(3);
            this.tpFixed.Size = new System.Drawing.Size(656, 338);
            this.tpFixed.TabIndex = 1;
            this.tpFixed.Text = "Fixed";
            this.tpFixed.UseVisualStyleBackColor = true;
            // 
            // frequencyCtl
            // 
            this.frequencyCtl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frequencyCtl.Interval = 1;
            this.frequencyCtl.IntervalDetail = "";
            this.frequencyCtl.IntervalType = -1;
            this.frequencyCtl.Location = new System.Drawing.Point(310, 33);
            this.frequencyCtl.Margin = new System.Windows.Forms.Padding(0);
            this.frequencyCtl.Name = "frequencyCtl";
            this.frequencyCtl.Size = new System.Drawing.Size(192, 22);
            this.frequencyCtl.TabIndex = 3;
            // 
            // cbUser
            // 
            this.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(3, 33);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(80, 22);
            this.cbUser.TabIndex = 1;
            // 
            // btnFixedDel
            // 
            this.btnFixedDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFixedDel.Image = global::GMoney.Properties.Resources.Delete_16;
            this.btnFixedDel.Location = new System.Drawing.Point(631, 34);
            this.btnFixedDel.Margin = new System.Windows.Forms.Padding(0);
            this.btnFixedDel.Name = "btnFixedDel";
            this.btnFixedDel.Size = new System.Drawing.Size(20, 20);
            this.btnFixedDel.TabIndex = 6;
            this.btnFixedDel.Click += new System.EventHandler(this.btnFixedDel_Click);
            // 
            // btnFixedNew
            // 
            this.btnFixedNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFixedNew.Image = ((System.Drawing.Image)(resources.GetObject("btnFixedNew.Image")));
            this.btnFixedNew.Location = new System.Drawing.Point(609, 34);
            this.btnFixedNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnFixedNew.Name = "btnFixedNew";
            this.btnFixedNew.Size = new System.Drawing.Size(20, 20);
            this.btnFixedNew.TabIndex = 5;
            this.btnFixedNew.Click += new System.EventHandler(this.btnFixedNew_Click);
            // 
            // cbSub
            // 
            this.cbSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub.FormattingEnabled = true;
            this.cbSub.Location = new System.Drawing.Point(84, 33);
            this.cbSub.Name = "cbSub";
            this.cbSub.Size = new System.Drawing.Size(225, 22);
            this.cbSub.TabIndex = 2;
            // 
            // ctbAmount
            // 
            this.ctbAmount.AllowNegative = false;
            this.ctbAmount.BackColor = System.Drawing.SystemColors.Window;
            this.ctbAmount.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctbAmount.Location = new System.Drawing.Point(503, 33);
            this.ctbAmount.Minimum = 0;
            this.ctbAmount.Name = "ctbAmount";
            this.ctbAmount.Size = new System.Drawing.Size(95, 22);
            this.ctbAmount.TabIndex = 4;
            this.ctbAmount.Leave += new System.EventHandler(this.optionAcceptButtonOn);
            this.ctbAmount.Enter += new System.EventHandler(this.optionAcceptButtonOff);
            this.ctbAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctbAmount_KeyDown);
            // 
            // folvFixed
            // 
            this.folvFixed.AllColumns.Add(this.colFixUserName);
            this.folvFixed.AllColumns.Add(this.colFixSub);
            this.folvFixed.AllColumns.Add(this.colFixFrequency);
            this.folvFixed.AllColumns.Add(this.colFixStartDate);
            this.folvFixed.AllColumns.Add(this.colFixEndDate);
            this.folvFixed.AllColumns.Add(this.colFixAmount);
            this.folvFixed.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.folvFixed.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.folvFixed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFixUserName,
            this.colFixSub,
            this.colFixFrequency,
            this.colFixStartDate,
            this.colFixEndDate,
            this.colFixAmount});
            this.folvFixed.ContextMenuStrip = this.cmsDelete;
            this.folvFixed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.folvFixed.FullRowSelect = true;
            this.folvFixed.Location = new System.Drawing.Point(3, 57);
            this.folvFixed.MultiSelect = false;
            this.folvFixed.Name = "folvFixed";
            this.folvFixed.RowHeight = 22;
            this.folvFixed.ShowGroups = false;
            this.folvFixed.Size = new System.Drawing.Size(650, 278);
            this.folvFixed.TabIndex = 7;
            this.folvFixed.UseAlternatingBackColors = true;
            this.folvFixed.UseCompatibleStateImageBehavior = false;
            this.folvFixed.View = System.Windows.Forms.View.Details;
            this.folvFixed.VirtualMode = true;
            this.folvFixed.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditStarting);
            this.folvFixed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folvFixed_KeyDown);
            this.folvFixed.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditFinishing);
            // 
            // colFixUserName
            // 
            this.colFixUserName.AspectName = "UserName";
            this.colFixUserName.IsEditable = false;
            this.colFixUserName.MinimumWidth = 65;
            this.colFixUserName.Text = "Name";
            this.colFixUserName.Width = 65;
            // 
            // colFixSub
            // 
            this.colFixSub.AspectName = "Name";
            this.colFixSub.FillsFreeSpace = true;
            this.colFixSub.IsEditable = false;
            this.colFixSub.MinimumWidth = 150;
            this.colFixSub.Text = "Category";
            this.colFixSub.Width = 150;
            // 
            // colFixFrequency
            // 
            this.colFixFrequency.AspectName = "";
            this.colFixFrequency.MinimumWidth = 192;
            this.colFixFrequency.Text = "Frequency";
            this.colFixFrequency.Width = 192;
            // 
            // colFixStartDate
            // 
            this.colFixStartDate.AspectName = "StartDate";
            this.colFixStartDate.AspectToStringFormat = "{0:yyyy/MM/dd}";
            this.colFixStartDate.MinimumWidth = 80;
            this.colFixStartDate.Text = "StartDate";
            this.colFixStartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFixStartDate.Width = 80;
            // 
            // colFixEndDate
            // 
            this.colFixEndDate.AspectName = "EndDate";
            this.colFixEndDate.AspectToStringFormat = "{0:yyyy/MM/dd}";
            this.colFixEndDate.MinimumWidth = 80;
            this.colFixEndDate.Text = "EndDate";
            this.colFixEndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFixEndDate.Width = 80;
            // 
            // colFixAmount
            // 
            this.colFixAmount.AspectName = "Amount";
            this.colFixAmount.AspectToStringFormat = "{0:n0}";
            this.colFixAmount.MinimumWidth = 60;
            this.colFixAmount.Text = "Amount";
            this.colFixAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmsDelete
            // 
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(104, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::GMoney.Properties.Resources.Delete_16;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // labelFixed
            // 
            this.labelFixed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelFixed.Location = new System.Drawing.Point(3, 10);
            this.labelFixed.Name = "labelFixed";
            this.labelFixed.Size = new System.Drawing.Size(378, 20);
            this.labelFixed.TabIndex = 0;
            this.labelFixed.Text = "Please set your default Fixed Data.";
            this.labelFixed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpOptimal
            // 
            this.tpOptimal.Controls.Add(this.folvOptimal);
            this.tpOptimal.Controls.Add(this.labelOptimal);
            this.tpOptimal.Location = new System.Drawing.Point(4, 23);
            this.tpOptimal.Name = "tpOptimal";
            this.tpOptimal.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptimal.Size = new System.Drawing.Size(656, 338);
            this.tpOptimal.TabIndex = 2;
            this.tpOptimal.Text = "Optimal";
            this.tpOptimal.UseVisualStyleBackColor = true;
            // 
            // folvOptimal
            // 
            this.folvOptimal.AllColumns.Add(this.colOptSub);
            this.folvOptimal.AllColumns.Add(this.colOptOptimal);
            this.folvOptimal.AllColumns.Add(this.colOptRange);
            this.folvOptimal.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.folvOptimal.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.folvOptimal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOptSub,
            this.colOptOptimal,
            this.colOptRange});
            this.folvOptimal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.folvOptimal.FullRowSelect = true;
            this.folvOptimal.Location = new System.Drawing.Point(3, 33);
            this.folvOptimal.MultiSelect = false;
            this.folvOptimal.Name = "folvOptimal";
            this.folvOptimal.RowHeight = 22;
            this.folvOptimal.ShowGroups = false;
            this.folvOptimal.Size = new System.Drawing.Size(650, 302);
            this.folvOptimal.TabIndex = 1;
            this.folvOptimal.UseAlternatingBackColors = true;
            this.folvOptimal.UseCompatibleStateImageBehavior = false;
            this.folvOptimal.View = System.Windows.Forms.View.Details;
            this.folvOptimal.VirtualMode = true;
            this.folvOptimal.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditStarting);
            this.folvOptimal.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.option_CellEditFinishing);
            // 
            // colOptSub
            // 
            this.colOptSub.AspectName = "Name";
            this.colOptSub.FillsFreeSpace = true;
            this.colOptSub.IsEditable = false;
            this.colOptSub.MinimumWidth = 150;
            this.colOptSub.Text = "Name";
            this.colOptSub.Width = 275;
            // 
            // colOptOptimal
            // 
            this.colOptOptimal.AspectName = "Optimal";
            this.colOptOptimal.AspectToStringFormat = "{0:n0}";
            this.colOptOptimal.MinimumWidth = 100;
            this.colOptOptimal.Text = "Optimal";
            this.colOptOptimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOptOptimal.Width = 150;
            // 
            // colOptRange
            // 
            this.colOptRange.AspectName = "Range";
            this.colOptRange.AspectToStringFormat = "{0:n0}";
            this.colOptRange.MinimumWidth = 100;
            this.colOptRange.Text = "Range";
            this.colOptRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOptRange.Width = 150;
            // 
            // labelOptimal
            // 
            this.labelOptimal.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelOptimal.Location = new System.Drawing.Point(3, 10);
            this.labelOptimal.Name = "labelOptimal";
            this.labelOptimal.Size = new System.Drawing.Size(359, 20);
            this.labelOptimal.TabIndex = 0;
            this.labelOptimal.Text = "Please set your Optimal Range of the following data.";
            this.labelOptimal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(677, 406);
            this.Controls.Add(this.tcOption);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tcOption.ResumeLayout(false);
            this.tpSecurity.ResumeLayout(false);
            this.tpSecurity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folvUser)).EndInit();
            this.tpFixed.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.folvFixed)).EndInit();
            this.cmsDelete.ResumeLayout(false);
            this.tpOptimal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.folvOptimal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcOption;
        private System.Windows.Forms.TabPage tpSecurity;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbOldPass;
        private System.Windows.Forms.Label labelOldPass;
        private System.Windows.Forms.Label labelBasicLogin;
        private System.Windows.Forms.TextBox tbNewPassConf;
        private System.Windows.Forms.Label labelNewPassConf;
        private System.Windows.Forms.TextBox tbNewPass;
        private System.Windows.Forms.Label labelNewPass;
        private System.Windows.Forms.CheckBox cbUseLoginPass;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TabPage tpFixed;
        private System.Windows.Forms.Label labelFixed;
        private System.Windows.Forms.TabPage tpOptimal;
        private System.Windows.Forms.Label labelOptimal;
        private BrightIdeasSoftware.FastObjectListView folvOptimal;
        private BrightIdeasSoftware.FastObjectListView folvFixed;
        private GMoney.control.NumericBox ctbAmount;
        private System.Windows.Forms.ComboBox cbSub;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Label btnFixedDel;
        private System.Windows.Forms.Label btnFixedNew;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label labelBasicUser;
        private BrightIdeasSoftware.FastObjectListView folvUser;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label btnUserNew;
        private GMoney.control.FrequencyCtl frequencyCtl;

        private BrightIdeasSoftware.OLVColumn colUserId;
        private BrightIdeasSoftware.OLVColumn colUserName;
        private BrightIdeasSoftware.OLVColumn colUserCreateDate;
        private BrightIdeasSoftware.OLVColumn colUserUpdateDate;
        private BrightIdeasSoftware.OLVColumn colUserDeleted;
        private BrightIdeasSoftware.OLVColumn colFixSub;
        private BrightIdeasSoftware.OLVColumn colFixAmount;
        private BrightIdeasSoftware.OLVColumn colFixUserName;
        private BrightIdeasSoftware.OLVColumn colFixFrequency;
        private BrightIdeasSoftware.OLVColumn colFixStartDate;
        private BrightIdeasSoftware.OLVColumn colFixEndDate;
        private BrightIdeasSoftware.OLVColumn colOptSub;
        private BrightIdeasSoftware.OLVColumn colOptOptimal;
        private BrightIdeasSoftware.OLVColumn colOptRange;

    }
}