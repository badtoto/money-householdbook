﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GMoney.util;

namespace GMoney.control
{
    [DesignTimeVisible(true), DefaultEvent("ValueChanged")]
    public partial class FrequencyCtl : UserControl
    {
        #region Constructor
        public FrequencyCtl()
        {
            InitializeComponent();

            SetIntervalTypeItems(cbType);

            this.nudInterval.ValueChanged += new EventHandler(nudInterval_ValueChanged);
            this.cbDetail.SelectedIndexChanged += new EventHandler(cbDetail_SelectedIndexChanged);
        }
        #endregion

        #region Methods
        private void SetIntervalTypeItems(ComboBox cb)
        {
            // TODO:hard coding for IntervalType Names
            cb.Items.Clear();
            cb.Items.Add("Day");
            cb.Items.Add("Week");
            cb.Items.Add("Month");
            cb.Items.Add("Year");
        }
        #endregion

        #region Events
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDetail.Items.Clear();
            cbDetail.Enabled = true;
            switch (cbType.SelectedIndex)
            {
                case (int)CommonUtils.IntervalType.EveryDay:// day
                    cbDetail.Enabled = false;
                    break;
                case (int)CommonUtils.IntervalType.EveryWeek:// week
                    foreach (string s in CommonUtils.DAY_NAMES)
                        cbDetail.Items.Add(s);
                    cbDetail.SelectedIndex = 0;
                    break;
                case (int)CommonUtils.IntervalType.EveryMonth:// month
                    for (int i = 0; i < 28; i++)
                        cbDetail.Items.Add((i + 1).ToString());
                    cbDetail.SelectedIndex = 0;
                    break;
                case (int)CommonUtils.IntervalType.EveryYear:// year
                    cbDetail.Enabled = false;
                    break;
                default: break;
            }

            if (FrequencyCtlValueChanged != null)
            {
                FrequencyCtlValueChanged(this, EventArgs.Empty);
            }
        }

        void cbDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FrequencyCtlValueChanged != null)
            {
                FrequencyCtlValueChanged(this, EventArgs.Empty);
            }
        }

        void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            if (FrequencyCtlValueChanged != null)
            {
                FrequencyCtlValueChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Properties
        public event EventHandler FrequencyCtlValueChanged;

        public int IntervalType { get { return cbType.SelectedIndex; } set { cbType.SelectedIndex = value; } }

        public int Interval { get { return (int)nudInterval.Value; } set { nudInterval.Value = value; } }

        public string IntervalDetail { get { return cbDetail.Text; } set { cbDetail.Text = value; } }
        #endregion
    }
}
