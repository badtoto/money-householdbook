namespace GMoney.control
{
    partial class CategoryList
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
            this.olvCategory = new BrightIdeasSoftware.ObjectListView();
            this.olvMajor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSub = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // olvCategory
            // 
            this.olvCategory.AllColumns.Add(this.olvMajor);
            this.olvCategory.AllColumns.Add(this.olvSub);
            this.olvCategory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvMajor,
            this.olvSub});
            this.olvCategory.FullRowSelect = true;
            this.olvCategory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.olvCategory.IsSearchOnSortColumn = false;
            this.olvCategory.Location = new System.Drawing.Point(-1, 24);
            this.olvCategory.MultiSelect = false;
            this.olvCategory.Name = "olvCategory";
            this.olvCategory.ShowFilterMenuOnRightClick = false;
            this.olvCategory.ShowHeaderInAllViews = false;
            this.olvCategory.ShowSortIndicators = false;
            this.olvCategory.Size = new System.Drawing.Size(363, 123);
            this.olvCategory.TabIndex = 0;
            this.olvCategory.UseCompatibleStateImageBehavior = false;
            this.olvCategory.View = System.Windows.Forms.View.Details;
            // 
            // olvMajor
            // 
            this.olvMajor.AspectName = "MajorName";
            this.olvMajor.Text = "Major";
            this.olvMajor.Width = 92;
            // 
            // olvSub
            // 
            this.olvSub.AspectName = "SubName";
            this.olvSub.Text = "Sub";
            this.olvSub.Width = 241;
            // 
            // CategoryList
            // 
            this.AnchorSize = new System.Drawing.Size(365, 21);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.olvCategory);
            this.Name = "CategoryList";
            this.Size = new System.Drawing.Size(365, 150);
            ((System.ComponentModel.ISupportInitialize)(this.olvCategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvCategory;
        private BrightIdeasSoftware.OLVColumn olvMajor;
        private BrightIdeasSoftware.OLVColumn olvSub;
    }
}
