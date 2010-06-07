using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Money.db;
using System.Collections;
using Money.instance;
using Test.Properties;

namespace Test
{
    public partial class ToolsForm : Form
    {
        Random random = null;

        public ToolsForm()
        {
            InitializeComponent();
            random = new Random();

            tbDbFile.Text = Settings.Default.LastFile;
            cbDebug.Checked = Settings.Default.IsDebugDB;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TimeSpan t1 = dtpTo.Value.Subtract(dtpFrom.Value);
            if (t1.Days < 0)
            {
                MessageBox.Show("From Date is bigger than To Date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnAdd.Enabled = false;
            BaseDB db = new BaseDB();
            db.IsDebug = cbDebug.Checked;
            if (!string.IsNullOrEmpty(tbDbFile.Text))
            {
                db.SQLiteDB = tbDbFile.Text;
            }
            db.StartDate = dtpFrom.Value;
            db.Days = t1.Days;

            ArrayList userList = db.GetUserTable();
            ArrayList majorList = db.GetMajorList();
            Dictionary<int, ArrayList> subList = db.GetSubList(majorList);

            db.NewBill(userList, majorList, subList, (long)nudCount.Value);

            db = null;

            MessageBox.Show(string.Format("Add {0:n0} items!", nudCount.Value), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdd.Enabled = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = tbDbFile.Text;
            DialogResult dr = ofd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName))
                {
                    MessageBox.Show("File doesn't exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!ofd.FileName.EndsWith("money.db"))
                {
                    MessageBox.Show("Please select db file !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tbDbFile.Text = ofd.FileName;
                Settings.Default.LastFile = ofd.FileName;
                Settings.Default.Save();
            }
        }

        private void cbDebug_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.IsDebugDB = cbDebug.Checked;
            Settings.Default.Save();
        }
    }
}
