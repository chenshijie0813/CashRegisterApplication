﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
namespace CashRegiterApplication
{

    public partial class CashRegisterWindow : Form
    {
        
        /**
         * 
         */
        public CashRegisterWindow()
        {
            
            InitializeComponent();

            this.productListDataGridView.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.productListDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            int index=0;
          

            this.orderDataGridView.RowHeadersVisible = false;
            this.orderDataGridView.ColumnHeadersVisible = false;

            index = this.orderDataGridView.Rows.Add();
            this.orderDataGridView.Rows[index].Cells[0].Value = "实收";
            this.orderDataGridView.Rows[index].Cells[CELL_INDEX.ORDER_COLUMN].Value = "0.00";
            index = this.orderDataGridView.Rows.Add();
            this.orderDataGridView.Rows[index].Cells[0].Value = "应收";
            this.orderDataGridView.Rows[index].Cells[CELL_INDEX.ORDER_COLUMN].ReadOnly = true;
            this.orderDataGridView.Rows[index].Cells[CELL_INDEX.ORDER_COLUMN].Value = "0.00";
            index = this.orderDataGridView.Rows.Add();
            this.orderDataGridView.Rows[index].Cells[0].Value = "找零";
            this.orderDataGridView.Rows[index].Cells[CELL_INDEX.ORDER_COLUMN].ReadOnly = true;
            this.orderDataGridView.Rows[index].Cells[CELL_INDEX.ORDER_COLUMN].Value = "0.00";

  

            //this.productListDataGridView.AllowUserToAddRows = false;
            this.orderDataGridView.AllowUserToAddRows = false;
            this.payWayDataGridView.AllowUserToAddRows = false;
            this.payWayDataGridView.Rows.Add();

            this.ColumnIndex.ReadOnly = true;
            this.ColumnProductName.ReadOnly = true;
            this.ColumnProductSpecification.ReadOnly = true;
            this.ColumnRemark.ReadOnly = true;
            this.ColumnMoney.ReadOnly = true;
            this.orderMsg.ReadOnly = true;

            this.productListDataGridView.Rows.Add();
            //this.productListDataGridView[CELL_INDEX.PRODUCT_CODE, 0].Value = "4891913690152";
            //this.productListDataGridView[CELL_INDEX.PRODUCT_CODE, 1].Value = "4891913691036";
            //this.productListDataGridView[CELL_INDEX.PRODUCT_CODE, 2].Value = "4891913690206";
            gConstructEnd = true;
        }
        bool gConstructEnd = false;
        static void Log(string message,
                     [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                      [CallerMemberName] string  fun= null)
        {
            Console.WriteLine("{0}:  func:{1}  line:{2} str:{3}", Path.GetFileName(file), fun,line, message);
        }

        private void CashRegisterWindow_Load(object sender, EventArgs e)
        {
          
         //  _SelectProductList();
        }

        private void CashRegisterWindow_Shown(object sender, EventArgs e)
        {
            //当界面显示的时候加载这里
            if (objIsEmpty(this.productListDataGridView.Rows[0].Cells[0].Value) )
            {
                this.productListDataGridView.Rows[0].Cells[0].Value = "1";
            }
            _SelectProductList();
            Log("ok");
        }
        private void _SelectProductList()
        {
            this.orderDataGridView.CurrentCell = null;
            this.payWayDataGridView.CurrentCell = null;
            this.payWayDataGridView.ClearSelection();
            this.orderDataGridView.ClearSelection();
            //默认第一行正在编辑中
            this.productListDataGridView.Select();
            this.productListDataGridView.CurrentCell = this.productListDataGridView.Rows[0].Cells[1];
            this.productListDataGridView.BeginEdit(true);
        }


      
        private void _ResetMoneyByRow(int rowIndex, int columnIndex)
        {
            if (objIsEmpty(this.productListDataGridView.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //价钱为空，就停止
                return;
            }
            int amout=0, price=0;

            if (objIsEmpty(this.productListDataGridView.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_CODE].Value))
            {
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                MessageBox.Show("错误条码：" + rowIndex);
                return;
            }

            if (!transferAmountToInt(this.productListDataGridView.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_AMOUNT].Value, out amout))
            {
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                MessageBox.Show("错误数量：" + rowIndex);
                return ;
            }

            if (!transferMoneyToInt(this.productListDataGridView.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_NORMAL_PRICE].Value, out price))
            {
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                MessageBox.Show("错误金额：" + rowIndex);
                return; 
            }

            int orderPrice = amout*price;
            string strOrderPrice = _CoverMoneyToString(orderPrice);
            this.productListDataGridView.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_MONEY].Value = strOrderPrice;
            _SetOrderPrice();
        }


        private void _SetOrderPrice()
        {
            int rowCount = this.productListDataGridView.RowCount;
            int orderPrice = 0,money=0;
            for (int index = 0; index < rowCount; ++index)
            {
                if (objIsEmpty(this.productListDataGridView.Rows[index].Cells[CELL_INDEX.PRODUCT_MONEY].Value))
                {
                    continue;
                }

                if ( !transferMoneyToInt(this.productListDataGridView.Rows[index].Cells[CELL_INDEX.PRODUCT_MONEY].Value,out money) )
                {
                    MessageBox.Show("错误行："+ index);
                    return;
                }

                orderPrice += money;
            }
            string strOrderPrice = _CoverMoneyToString(orderPrice);
            this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.ORDER_FEE_ROW].Value = strOrderPrice;
            this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.RECIEVE_FEE_ROW].Value = strOrderPrice;
            this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.CHANGE_FEE_ROW].Value = "0.00";
            return;
        }

        private void GetProductByProductCode( int rowIndex, int columnIndex)
        {
            DataGridViewRow currentRow = this.productListDataGridView.Rows[rowIndex];
            //if (!objIsEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            //{
            //    //说明数量被修改了，那么这个商品不再做处理
            //    return;
            //}
           
            if (objIsEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value)&& objIsEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //空行
                //_GoOrderDataGrid();//跳到总价
                return;
            }

            string productCode = currentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value.ToString().Trim();
            if (productCode == "")
            {
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                MessageBox.Show("当前行有问题 rowIndex:" + rowIndex + " columnIndex"+ columnIndex);
                return;
            }

            string tagUrl = "http://aladdin.chalubo.com/cashRegister/getPricingByProductCode.json?productCode=" + productCode;
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);

            StreamReader streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
            string content = streamReader.ReadToEnd();
          
            //将Json字符串转化成对象
            ProductPricingInfoResp productResp = JsonConvert.DeserializeObject<ProductPricingInfoResp>(content);
            if (productResp.errorCode != 0 )
            {
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                MessageBox.Show("后台返回商品失败 productCode:" + productCode + " ;content:" + content);
                return ;
            }

            ProductPricing productInfo = productResp.data.info;
            if (objIsEmpty(productInfo.ProductCode)||
               objIsEmpty(productInfo.NormalPrice) ||
                objIsEmpty(productInfo.ProductName) 
                )
            {
                MessageBox.Show("productInfo.ProductCode:" + productInfo.ProductCode);
                MessageBox.Show("productInfo.NormalPrice:" + productInfo.NormalPrice);
                MessageBox.Show("productInfo.ProductName:" + productInfo.ProductName);
                MessageBox.Show("后台返回 有问题商品:" + content);
                _SetPointToResetCurrentCell(this.productListDataGridView.Rows[rowIndex].Cells[columnIndex]);
                return;
            }
  
            currentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value = productInfo.ProductCode;
            currentRow.Cells[CELL_INDEX.PRODUCT_CODE].ReadOnly = true;//请求到后台的条码，不允许修改，只能删除，防止误操作
            currentRow.Cells[CELL_INDEX.PRODUCT_NAME].Value = productInfo.ProductName;
            currentRow.Cells[CELL_INDEX.PRODUCT_SPECIFICATION].Value = productInfo.ProductSpecification;
            string normalPrice= _CoverMoneyToString(productInfo.NormalPrice);
            currentRow.Cells[CELL_INDEX.PRODUCT_NORMAL_PRICE].Value = normalPrice;
            currentRow.Cells[CELL_INDEX.PRODUCT_AMOUNT].Value = 1;
            currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value = normalPrice;
           
            //更新订单价钱
            _SetOrderPrice();
           // _GoNextTab();

        }



        //private void _GoNextTab()
        //{
        //    //下一行处于编辑状态
        //    DataGridViewRow currentRow = this.productListDataGridView.CurrentRow;
        //    if (objIsEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value))
        //    {
        //       // //跳到总价
        //        return;
        //    }
        //    //this.productListDataGridView.CurrentCell = this.productListDataGridView[CELL_INDEX.PRODUCT_CODE, currentRow.Index + 1];
        //    //this.productListDataGridView.BeginEdit(true);
        //}

        private string _CoverMoneyToString(int money)
        {
          
            //保留小数点后两位
            return Convert.ToDecimal((double)money / 100).ToString("0.00");
        }


        private bool objIsEmpty(object value)
        {
            if (value == null || value.ToString() == "" )
            {
                return true;
            }
            return false;
        }
        private bool transferMoneyToInt(object value, out int number)
        {
            number = 0;
            if (objIsEmpty(value))
            {
                return false;
            }
            decimal decimalNumber = 0;
            bool isNumber = decimal.TryParse(value.ToString(), out decimalNumber);
            if (!isNumber) return false;
            number=Convert.ToInt32(decimalNumber * 100);
            return true;
           
        }
        private bool transferAmountToInt(object value, out int number)
        {
            number = 0;
            if (objIsEmpty(value))
            {
                return false;
            }
            return int.TryParse(value.ToString(), out number);
        }

        private void _GoOrderDataGrid()
        {
            //MessageBox.Show("_GoOrderDataGrid " + this.productListDataGridView.CurrentRow.Index + " index" + this.productListDataGridView.CurrentCell.ColumnIndex);
            //跳转到总价的金额编辑
            this.productListDataGridView.CurrentRow.Selected = false;
            this.payWayDataGridView.ClearSelection();
        
            this.orderDataGridView.CurrentCell = this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.RECIEVE_FEE_ROW];
            this.orderDataGridView.BeginEdit(true);
        }
        private void productListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void productListDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //int RowIndex=this.productListDataGridView.CurrentCell.RowIndex;
            ////MessageBox.Show("Begin edit RowIndex:" + RowIndex);
            //if (RowIndex >= 1)
            //{
            //    if (objIsEmpty( this.productListDataGridView.Rows[RowIndex].Cells[CELL_INDEX.PRODUCT_CODE].Value)
            //        && objIsEmpty(this.productListDataGridView.Rows[RowIndex-1].Cells[CELL_INDEX.PRODUCT_CODE].Value))
            //    {
            //        _SetOrderPrice();
            //        _GoOrderDataGrid();
            //    }
            //}
           
        }

        private void productListDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("CellEndEdit  RowIndex:" + this.productListDataGridView.CurrentCell.RowIndex);
            //MessageBox.Show("CellEndEdit");
            if (!gConstructEnd)
            {
                //未初始化行表的时候，这里是空的
                return;
            }
            if (this.productListDataGridView.CurrentCell.ColumnIndex != e.ColumnIndex || this.productListDataGridView.CurrentCell.RowIndex != e.RowIndex)
            {
                //非当前行
                return;
            }
            if (e.ColumnIndex == CELL_INDEX.PRODUCT_CODE)
            {
                //MessageBox.Show("end edit code PRODUCT_CODE RowIndex :" + e.RowIndex + " CellIndex"+e.ColumnIndex   );
                GetProductByProductCode(e.RowIndex, e.ColumnIndex);
            }
            else if (e.ColumnIndex == CELL_INDEX.PRODUCT_AMOUNT || e.ColumnIndex == CELL_INDEX.PRODUCT_NORMAL_PRICE)
            {
                _ResetMoneyByRow(e.RowIndex,e.ColumnIndex);
            }
        }
        
        private void productListDataGridView_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        

        private void productListDataGridView_Enter(object sender, EventArgs e)
        {
            if (gResetRow)
            {
                gResetRow = false;
                this.productListDataGridView.CurrentCell = gCurrentCell;
            }
            //MessageBox.Show("Enter:" + e.ToString());
        }

        private void productListDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("KeyDown:" + e.ToString());
        }

        private void productListDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.productListDataGridView.Rows[e.RowIndex].Cells[CELL_INDEX.INDEX].Value = e.RowIndex + 1;
        }

        private void productListDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.productListDataGridView.Rows[e.RowIndex].Cells[CELL_INDEX.INDEX].Value = e.RowIndex+1;
        }


        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Enter:
                    {
                        //MessageBox.Show("Keys.Enter: CurrentCell.RowIndex:" + this.productListDataGridView.CurrentCell.RowIndex);
                        if (this.productListDataGridView.IsCurrentCellInEditMode)
                        {
                            if (objIsEmpty(this.productListDataGridView.CurrentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value))
                            {
                                //MessageBox.Show("Keys.Enter Value empty  RowIndex:" + this.productListDataGridView.CurrentCell.RowIndex);
                                if (this.productListDataGridView.CurrentRow.IsNewRow)
                                {
                                    //删除当前行
                                    //this.productListDataGridView.Rows.Remove(this.productListDataGridView.CurrentRow);
                                    this.productListDataGridView.CurrentRow.Cells[CELL_INDEX.INDEX].Value = "";
                                    _SetOrderPrice();
                                    _GoOrderDataGrid();
                                }
                                return base.ProcessCmdKey(ref msg, keyData);
                            }

                        }
                        if (this.orderDataGridView.IsCurrentCellInEditMode)
                        {
                            //this.orderDataGridView.BeginEdit(false);
                            //this.orderDataGridView.CurrentCell = null;
                            //if (this.orderDataGridView.CurrentCell.RowIndex == CELL_INDEX.ORDER_FEE_ROW)
                            //{
                               
                            //}
                            //return true;
                        }

                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private  DataGridViewCell gCurrentCell;
        private bool gResetRow = false;

        private void orderDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int orderFee = 0, recieveFee = 0, changeFee = 0;
            if (!transferMoneyToInt(this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.ORDER_FEE_ROW].Value, out orderFee))
            {
                MessageBox.Show("应收错误:" + this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.ORDER_FEE_ROW].Value);
                return;
            }
            if (!transferMoneyToInt(this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.RECIEVE_FEE_ROW].Value, out recieveFee))
            {
                MessageBox.Show("实收错误:" + this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.RECIEVE_FEE_ROW].Value);
                return;
            }

            changeFee = recieveFee - orderFee;
            this.orderDataGridView[CELL_INDEX.ORDER_COLUMN, CELL_INDEX.CHANGE_FEE_ROW].Value = _CoverMoneyToString(changeFee);
            if (changeFee < 0)
            {
                _SetPointToResetCurrentCell(orderDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                MessageBox.Show("实收价钱小于应收" );
                return;
            }
            MessageBox.Show("生成订单成功");
        }
        private void _GoCurrentProductCodeIfNeed()
        {
            if (this.productListDataGridView.ColumnCount < CELL_INDEX.PRODUCT_MONEY +1)
            {
                //初始化阶段
                return;
            }
            if (!objIsEmpty(this.productListDataGridView.CurrentRow.Cells[CELL_INDEX.PRODUCT_CODE].Value)
                &&objIsEmpty(this.productListDataGridView.CurrentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //第一个
                this.productListDataGridView.CurrentCell = this.productListDataGridView.CurrentRow.Cells[CELL_INDEX.PRODUCT_CODE];
            }
        }
        private void productListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Log("productListDataGridView_SelectionChanged row:"+ this.productListDataGridView.CurrentCell.RowIndex +" Column:"+ this.productListDataGridView.CurrentCell.ColumnIndex);
            if (gResetRow)
            {
                gResetRow = false;
                this.productListDataGridView.CurrentCell = gCurrentCell;
                return;
            }
            _GoCurrentProductCodeIfNeed();
           
        }
        private void productListDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Log("productListDataGridView_CellEnter row:" + this.productListDataGridView.CurrentCell.RowIndex + " Column:" + this.productListDataGridView.CurrentCell.ColumnIndex);
            //_GoCurrentProductCodeIfNeed();
        }


        void orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (gResetRow)
            {
                gResetRow = false;
                this.orderDataGridView.CurrentCell = gCurrentCell;
            }
        }
     
        private void _SetPointToResetCurrentCell(DataGridViewCell currentCell)
        {
            Log("row:"+ currentCell.RowIndex +" Column"+ currentCell.ColumnIndex);
            gResetRow = true;
            gCurrentCell = currentCell;
        }

        void orderDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.orderDataGridView.SelectionChanged += new EventHandler(orderDataGridView_SelectionChanged);
        }

    }

    public static class CELL_INDEX
    {
        public static int INDEX = 0;
        public static int PRODUCT_CODE = 1;
        public static int PRODUCT_NAME = 2;
        public static int PRODUCT_SPECIFICATION = 3;
        public static int PRODUCT_NORMAL_PRICE = 4;
        public static int PRODUCT_AMOUNT = 5;
        public static int PRODUCT_MONEY = 6;

        public static int ORDER_COLUMN = 1;
        public static int RECIEVE_FEE_ROW = 0;
        public static int ORDER_FEE_ROW = 1;
        public static int CHANGE_FEE_ROW = 2;
    }

}
