namespace GMoney.form
{
    partial class BillForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillForm));
            this.cbMajor = new System.Windows.Forms.ComboBox();
            this.cbSub = new System.Windows.Forms.ComboBox();
            this.cbPayment = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPayment = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAnnualBudget = new System.Windows.Forms.CheckBox();
            this.labelType = new System.Windows.Forms.Label();
            this.cbBillType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSplit = new System.Windows.Forms.CheckBox();
            this.nudMonths = new System.Windows.Forms.NumericUpDown();
            this.labelSplit = new System.Windows.Forms.Label();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.tbAmount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMajor
            // 
            this.cbMajor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMajor.FormattingEnabled = true;
            this.cbMajor.Location = new System.Drawing.Point(103, 99);
            this.cbMajor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMajor.Name = "cbMajor";
            this.cbMajor.Size = new System.Drawing.Size(356, 22);
            this.cbMajor.TabIndex = 8;
            this.cbMajor.SelectedIndexChanged += new System.EventHandler(this.cbMajor_SelectedIndexChanged);
            // 
            // cbSub
            // 
            this.cbSub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSub.FormattingEnabled = true;
            this.cbSub.Location = new System.Drawing.Point(103, 128);
            this.cbSub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSub.Name = "cbSub";
            this.cbSub.Size = new System.Drawing.Size(356, 22);
            this.cbSub.TabIndex = 9;
            this.cbSub.SelectedIndexChanged += new System.EventHandler(this.cbSub_SelectedIndexChanged);
            // 
            // cbPayment
            // 
            this.cbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayment.FormattingEnabled = true;
            this.cbPayment.Location = new System.Drawing.Point(103, 218);
            this.cbPayment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbPayment.Name = "cbPayment";
            this.cbPayment.Size = new System.Drawing.Size(125, 22);
            this.cbPayment.TabIndex = 19;
            this.cbPayment.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.Location = new System.Drawing.Point(163, 244);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 20;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Category:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPayment
            // 
            this.labelPayment.BackColor = System.Drawing.Color.Transparent;
            this.labelPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPayment.Location = new System.Drawing.Point(35, 218);
            this.labelPayment.Name = "labelPayment";
            this.labelPayment.Size = new System.Drawing.Size(62, 20);
            this.labelPayment.TabIndex = 18;
            this.labelPayment.Text = "Payment:";
            this.labelPayment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelPayment.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Amount:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Remarks:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(258, 244);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // cbAnnualBudget
            // 
            this.cbAnnualBudget.AutoSize = true;
            this.cbAnnualBudget.BackColor = System.Drawing.Color.Transparent;
            this.cbAnnualBudget.Location = new System.Drawing.Point(352, 192);
            this.cbAnnualBudget.Name = "cbAnnualBudget";
            this.cbAnnualBudget.Size = new System.Drawing.Size(107, 18);
            this.cbAnnualBudget.TabIndex = 17;
            this.cbAnnualBudget.Text = "Annual Budget";
            this.cbAnnualBudget.UseVisualStyleBackColor = false;
            // 
            // labelType
            // 
            this.labelType.BackColor = System.Drawing.Color.Transparent;
            this.labelType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelType.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelType.Location = new System.Drawing.Point(37, 14);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(62, 20);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "Type:";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbBillType
            // 
            this.cbBillType.DisplayMember = "0";
            this.cbBillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBillType.FormattingEnabled = true;
            this.cbBillType.Items.AddRange(new object[] {
            "Expense",
            "Income"});
            this.cbBillType.Location = new System.Drawing.Point(103, 14);
            this.cbBillType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbBillType.Name = "cbBillType";
            this.cbBillType.Size = new System.Drawing.Size(125, 22);
            this.cbBillType.TabIndex = 1;
            this.cbBillType.SelectedIndexChanged += new System.EventHandler(this.cbBillType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(268, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy/MM/dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(334, 42);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 22);
            this.dtpDate.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(37, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "User:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbUser
            // 
            this.cbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Items.AddRange(new object[] {
            "Expense",
            "Income"});
            this.cbUser.Location = new System.Drawing.Point(103, 44);
            this.cbUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(125, 22);
            this.cbUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(45, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(407, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "---------------------------------------------------------------------------------" +
    "-------------------";
            // 
            // cbSplit
            // 
            this.cbSplit.AutoSize = true;
            this.cbSplit.BackColor = System.Drawing.Color.Transparent;
            this.cbSplit.Location = new System.Drawing.Point(234, 160);
            this.cbSplit.Name = "cbSplit";
            this.cbSplit.Size = new System.Drawing.Size(69, 18);
            this.cbSplit.TabIndex = 14;
            this.cbSplit.Text = "Split to ";
            this.cbSplit.UseVisualStyleBackColor = false;
            this.cbSplit.CheckedChanged += new System.EventHandler(this.cbSplit_CheckedChanged);
            // 
            // nudMonths
            // 
            this.nudMonths.Enabled = false;
            this.nudMonths.Location = new System.Drawing.Point(305, 158);
            this.nudMonths.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMonths.Name = "nudMonths";
            this.nudMonths.Size = new System.Drawing.Size(35, 22);
            this.nudMonths.TabIndex = 15;
            this.nudMonths.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudMonths.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelSplit
            // 
            this.labelSplit.AutoSize = true;
            this.labelSplit.BackColor = System.Drawing.Color.Transparent;
            this.labelSplit.Location = new System.Drawing.Point(346, 161);
            this.labelSplit.Name = "labelSplit";
            this.labelSplit.Size = new System.Drawing.Size(47, 14);
            this.labelSplit.TabIndex = 16;
            this.labelSplit.Text = "Months";
            // 
            // tbRemarks
            // 
            this.tbRemarks.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbRemarks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbRemarks.Location = new System.Drawing.Point(103, 188);
            this.tbRemarks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(125, 22);
            this.tbRemarks.TabIndex = 13;
            this.tbRemarks.WordWrap = false;
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(103, 158);
            this.tbAmount.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(125, 22);
            this.tbAmount.TabIndex = 11;
            this.tbAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbAmount.ThousandsSeparator = true;
            // 
            // BillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 278);
            this.Controls.Add(this.tbAmount);
            this.Controls.Add(this.labelSplit);
            this.Controls.Add(this.nudMonths);
            this.Controls.Add(this.cbSplit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.cbBillType);
            this.Controls.Add(this.cbAnnualBudget);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelPayment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbRemarks);
            this.Controls.Add(this.cbPayment);
            this.Controls.Add(this.cbSub);
            this.Controls.Add(this.cbMajor);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BillForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bill";
            ((System.ComponentModel.ISupportInitialize)(this.nudMonths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSub;
        private System.Windows.Forms.ComboBox cbPayment;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbMajor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPayment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbAnnualBudget;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox cbBillType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSplit;
        private System.Windows.Forms.NumericUpDown nudMonths;
        private System.Windows.Forms.Label labelSplit;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.NumericUpDown tbAmount;
    }
}