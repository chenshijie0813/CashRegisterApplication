﻿using CashRegisterApplication.comm;
using CashRegisterApplication.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CashRegisterApplication.window.History
{
    public partial class HistoryListWindow : Form
    {
        public HistoryListWindow()
        {
            InitializeComponent();
            CenterContral.Init();
        }

        public void ShowByCenterControl()
        {
            this.Show();
            RefreshData();
        }

        List<DbStockOutDTO> gListStockOutDTO = new List<DbStockOutDTO>();

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void HistoryWindow_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Today;
            this.dataGridView_HistoryData.Focus();
            this.ActiveControl = this.button1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RefreshData()
        {
            
            DateTime oDate = this.dateTimePicker1.Value;
            gListStockOutDTO = new List<DbStockOutDTO>();
            if (!CenterContral.GetStockOutMsgByDate(oDate, ref gListStockOutDTO))
            {
                return;
            }
            ShowDataGrivewByStoctOutMsg();
        }

        private void ShowDataGrivewByStoctOutMsg()
        {
            this.dataGridView_HistoryData.Rows.Clear();

            for (int i=0 ; i< gListStockOutDTO.Count ; i++)
            {
                this.dataGridView_HistoryData.Rows.Add();
                SetRowsByStockOut(this.dataGridView_HistoryData.Rows[i], gListStockOutDTO[i]) ;
            }
            label_total_count.Text = "一共有" + gListStockOutDTO.Count + "笔交易";
            //默认选中最后一笔交易
            if (gListStockOutDTO.Count > 0)
            {
                this.dataGridView_HistoryData.CurrentCell = this.dataGridView_HistoryData.Rows[0].Cells[0];
             }
        }

        private int CELL_SERIAL_NUMBER = 0;
        private int CELL_CREATETIME = 1;
        private int CELL_ORDER_AMOUNT = 2;
        private int CELL_TOTAL_COUNT = 3;
        private int CELL_CREATOR = 4;
        private int CELL_STATE = 5;
        private void SetRowsByStockOut(DataGridViewRow dataGridViewRow, DbStockOutDTO oStockOut)
        {
            dataGridViewRow.Cells[CELL_SERIAL_NUMBER].Value = oStockOut.Base.serialNumber;
            dataGridViewRow.Cells[CELL_CREATETIME].Value = oStockOut.Base.stockOutTime.Substring(0,19) ;
            dataGridViewRow.Cells[CELL_ORDER_AMOUNT].Value = CommUiltl.CoverMoneyUnionToStrYuan(oStockOut.Base.orderAmount);
            dataGridViewRow.Cells[CELL_TOTAL_COUNT].Value = oStockOut.Base.totalProductCount;
            dataGridViewRow.Cells[CELL_CREATOR].Value = oStockOut.Base.creator;
            dataGridViewRow.Cells[CELL_STATE].Value = CenterContral.GetStateDscByStockOutBase(oStockOut);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_HistoryData.CurrentCell == null || this.dataGridView_HistoryData.CurrentRow == null)
            {
                MessageBox.Show("请选中打印的订单");
                return;
            }
            PrinteStockoutMsgRow(this.dataGridView_HistoryData.CurrentRow.Index);
        }

        private void PrinteStockoutMsgRow(int rowIndex)
        {
            if (rowIndex < -1 || rowIndex > gListStockOutDTO.Count)
            {
                MessageBox.Show("异常订单");
                return;
            }
            var confirmPayApartResult = MessageBox.Show("是否要打印",
              "打印确认",
               MessageBoxButtons.YesNo);

            if (confirmPayApartResult != DialogResult.Yes)
            {
                return;
            }
            CenterContral.PrintOrder(gListStockOutDTO[rowIndex]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CenterContral.Window_HistoryListWindow = new HistoryListWindow();
            escapeToPreWindows();
        }

        private void escapeToPreWindows()
        {
            CenterContral.Window_ProductList.Show();
            this.Hide();
        }

        private void HistoryWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            CenterContral.Window_HistoryListWindow = new HistoryListWindow();
            e.Cancel = false;
            escapeToPreWindows();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CommUiltl.Log("row:" + e.RowIndex);
            ShowDetailWindow(e.RowIndex);
        }

        private void ShowDetailWindow(int rowIndex)
        {
            if (rowIndex < -1 || rowIndex > gListStockOutDTO.Count)
            {
                MessageBox.Show("未知行");
                return;
            }
            DbStockOutDTO oStock = gListStockOutDTO[rowIndex];
            CenterContral.Window_HistoryDetailWindow.ShowDetailStockOut(oStock);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView_HistoryData.CurrentCell == null || this.dataGridView_HistoryData.CurrentRow == null)
            {
                MessageBox.Show("请选中订单");
                return;
            }
            ShowDetailWindow(this.dataGridView_HistoryData.CurrentRow.Index );
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            CommUiltl.Log("Keys:" + keyData);
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Enter:
                case System.Windows.Forms.Keys.D1:
                case System.Windows.Forms.Keys.NumPad1:
                case System.Windows.Forms.Keys.Oem1:
                    {
                        RefreshData();
                        break;
                    }
                case System.Windows.Forms.Keys.D2:
                case System.Windows.Forms.Keys.NumPad2:
                case System.Windows.Forms.Keys.Oem2:
                    {
                        if (this.dataGridView_HistoryData.CurrentCell == null || this.dataGridView_HistoryData.CurrentRow == null)
                        {
                            MessageBox.Show("请选中订单");
                            return true;
                        }
                        ShowDetailWindow(this.dataGridView_HistoryData.CurrentRow.Index);
                        break;
                    }
                case System.Windows.Forms.Keys.Escape:
                    {
                        escapeToPreWindows();
                        break;
                    }
              
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
