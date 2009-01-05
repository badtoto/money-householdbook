namespace Test
{
    partial class ToolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolsForm));
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDbFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(108, 148);
            this.nudCount.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.nudCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(200, 22);
            this.nudCount.TabIndex = 2;
            this.nudCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCount.ThousandsSeparator = true;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(136, 176);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data Count:";
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDebug.Location = new System.Drawing.Point(20, 53);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(102, 18);
            this.cbDebug.TabIndex = 6;
            this.cbDebug.Text = "Is Debug DB :";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.CheckedChanged += new System.EventHandler(this.cbDebug_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "DataBase:";
            // 
            // tbDbFile
            // 
            this.tbDbFile.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbDbFile.Location = new System.Drawing.Point(108, 18);
            this.tbDbFile.Name = "tbDbFile";
            this.tbDbFile.ReadOnly = true;
            this.tbDbFile.Size = new System.Drawing.Size(135, 22);
            this.tbDbFile.TabIndex = 8;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(248, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(108, 84);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 22);
            this.dtpFrom.TabIndex = 10;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(108, 115);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 22);
            this.dtpTo.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "From:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "To:";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 216);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbDbFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.nudCount);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestForm";
            this.Text = "Add Test Data";
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDbFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

