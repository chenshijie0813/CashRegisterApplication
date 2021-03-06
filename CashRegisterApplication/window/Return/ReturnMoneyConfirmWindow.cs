﻿using CashRegisterApplication.comm;
using CashRegiterApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CashRegisterApplication.window
{
    public partial class ReturnMoneyConfirmWindow : Form
    {
        public ReturnMoneyConfirmWindow()
        {
            InitializeComponent();
           
        }

        private void ReceiveMoneyByCash_Load(object sender, EventArgs e)
        {
         
        }
        private void ReceiveMoneyByCash_Shown(object sender, EventArgs e)
        {
        }
        internal void CallByReturnDetailWindow(long orderAmount)
        {
            ShowByReturnMoneyWindow(orderAmount);
        }
        public void ShowByReturnMoneyWindow(long returnMoney)
        {
            this.Show();
            this.Text= "退货-"+CenterContral.oCheckout.payTypeDesc;
            //this.textBox_payType.Text = CenterContral.oCheckout.payTypeDesc;
            this.textBox_ReceiveFee.Text = CommUiltl.CoverMoneyUnionToStrYuan(returnMoney);
            this.textBox_SupportFee.Text = this.textBox_ReceiveFee.Text;
            //this.textBox_ChangeFee.Text = CommUiltl.CoverMoneyUnionToStrYuan(0);
            _SelectRecieve();
        }

        private void _SelectRecieve()
        {
            this.textBox_ReceiveFee.SelectionStart = 0;
            this.textBox_ReceiveFee.SelectionLength = this.textBox_ReceiveFee.Text.Length;
        }

        private void ReceiveMoneyByCashWindow_Enter(object sender, EventArgs e)
        {
            CommUiltl.Log("ReceiveMoneyByCashWindow_Enter");
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            CommUiltl.Log("Keys:" + keyData);
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Enter:
                    {
                        buttonConfirm_Click(null, null);
                        break;
                    }
                case System.Windows.Forms.Keys.Escape:
                    {
                        returnPreventWindows();
                        break;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    

        private void textBox_ReceiveFee_TextChanged(object sender, EventArgs e)
        {
            //检查小数点后两位
            string word = this.textBox_ReceiveFee.Text.Trim();
            string[] wordArr = word.Split('.');
            if (wordArr.Length > 1)
            {
                string afterDot = wordArr[1];
                if (afterDot.Length > 2)
                {
                    this.textBox_ReceiveFee.Text = wordArr[0] + "." + afterDot.Substring(0, 2);
                    this.textBox_ReceiveFee.SelectionStart = this.textBox_ReceiveFee.Text.Length;
                }
            }
           if( CommUiltl.IsObjEmpty(this.textBox_ReceiveFee.Text))
            {
                return;
            }
        }

        private void textBox_ReceiveFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommUiltl.Log("Keys:" + e.KeyChar);

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                //非数字，非控制，非.都认为不允许输入
                CommUiltl.Log("1 true:" + e.KeyChar);
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                // only allow one decimal point
                //只允许一个小数点
                CommUiltl.Log("2 true:" + e.KeyChar);
                e.Handled = true;
            }

        }


        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            long recieveFee = 0;
            if (!CommUiltl.ConverStrYuanToUnion(this.textBox_ReceiveFee.Text, out recieveFee))
            {
                MessageBox.Show("退货金额错误:" + this.textBox_ReceiveFee.Text);
                return;
            }
            long change = recieveFee + CenterContral.oStockOutDTO.Base.ChangeFee ;
            string showTips = "确认退货金额："  + this.textBox_ReceiveFee.Text + "元";

            var confirmPayApartResult = MessageBox.Show(showTips,
                                "确认退货金额",
                                  MessageBoxButtons.YesNo);

            if (confirmPayApartResult != DialogResult.Yes)
            {
                //确认支付
                CommUiltl.Log("DialogResult.No recieveFee:" + recieveFee);
                _SelectRecieve();
                return;
            }
         
            CenterContral.Window_ReturnDetailWindow.ShowByComfirWindow(recieveFee);
            this.Hide();
            return;
        }
        private void returnPreventWindows()
        {
            CenterContral.Window_ReturnDetailWindow.ShowByContral();
            this.Hide();
        }

        private void ReceiveMoneyByCashWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            returnPreventWindows();
        }


        private void ReturnMoneyConfirmWindow_Load(object sender, EventArgs e)
        {

        }

       
    }
}
