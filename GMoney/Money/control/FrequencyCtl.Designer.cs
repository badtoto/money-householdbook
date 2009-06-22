namespace GMoney.control
{
    partial class FrequencyCtl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbDetail = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDetail
            // 
            this.cbDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDetail.FormattingEnabled = true;
            this.cbDetail.Location = new System.Drawing.Point(102, 0);
            this.cbDetail.Name = "cbDetail";
            this.cbDetail.Size = new System.Drawing.Size(90, 22);
            this.cbDetail.TabIndex = 45;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Day",
            "Week",
            "Month",
            "Year"});
            this.cbType.Location = new System.Drawing.Point(41, 0);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(60, 22);
            this.cbType.TabIndex = 44;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // nudInterval
            // 
            this.nudInterval.Location = new System.Drawing.Point(0, 0);
            this.nudInterval.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(40, 22);
            this.nudInterval.TabIndex = 43;
            this.nudInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FrequencyCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbDetail);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.nudInterval);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FrequencyCtl";
            this.Size = new System.Drawing.Size(192, 22);
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDetail;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.NumericUpDown nudInterval;
    }
}
