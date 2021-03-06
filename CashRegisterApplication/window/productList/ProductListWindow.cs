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
using System.Data.SQLite;
using CashRegisterApplication.window;
using CashRegisterApplication.comm;
using CashRegisterApplication.model;
using CashRegisterApplication.window.Setting;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Threading;

namespace CashRegiterApplication
{

    public partial class ProductListWindow : Form
    {
        bool gConstructEnd = false;

        public ProductListWindow()
        {
            InitializeComponent();
            InitData();
          
        }

        private void ProductListWindow_Load(object sender, EventArgs e)
        {
      
            CenterContral.Clean();

            System.Windows.Forms.Clipboard.SetText("8410376009392");
            //System.Windows.Forms.Clipboard.SetText("2100507005701");
            // System.Windows.Forms.Clipboard.SetText("9556247516480");
            //System.Windows.Forms.Clipboard.SetText("倍乐");
            this.label_defaultUser.Text = CenterContral.DefaultUserName;
            this.label_postId.Text = CenterContral.iPostId.ToString();
            SetLableTime();
        }

        internal void SetLableTime()
        {
            this.label_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        internal static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("CurrentDomain_UnhandledException:" + e.ToString());
        }

        public void CallShow()
        {
            this.Show();
            UpdateTextShow();
        }

        public void SetMemberInfo()
        {
            CommUiltl.Log("this.label_member_account.Text:"+ this.label_member_account.Text);
            this.label_member_account.Text = CenterContral.oStockOutDTO.oMember.memberAccount;
            this.label_member_balance.Text = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.oMember.balance).ToString();
            if (this.label_member_account.Text == "")
            {
                this.label_member_balance.Text = "";//避免出现0.00
            }
            this.label_member_point.Text = CenterContral.oStockOutDTO.oMember.point.ToString();
        }

        public void SetLocalSaveDataNumber()
        {
            //挂单数量
            this.label_local_save_stock_number.Text = CenterContral.oLocalSaveStock.listStock.Count.ToString();
        }
        public void  SetSerialNumber(string strSerialNumber)
        {
            //设置流水号
            this.label_serial_number.Text = strSerialNumber;
        }
        
        public void SetStoreName(string strStoreName)
        {
            //设置门店
            this.Text = "收银台-" + strStoreName;
        }

        //折扣信息
        public void UpdateDiscount()
        {
            //折扣额度
            this.label_discount_amount.Text = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.discountAmount);
            //折扣率
            this.label_discount_rate.Text = (CenterContral.oStockOutDTO.Base.discountRate / 100).ToString();
        }



        public System.Windows.Forms.DataGridView GetDataGridViewProduct()
        {
            return this.dataGridView_productList;
        }

        private void ProductListWindow_Shown(object sender, EventArgs e)
        {
            //当界面显示的时候加载这里
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[0].Cells[0].Value))
            {
                this.dataGridView_productList.Rows[0].Cells[0].Value = "1";
            }
            _SetCurrentCell();
            CommUiltl.Log("ok");
        }

        private void InitData()
        {
            _InitOrderMsg();
            this.ColumnIndex.ReadOnly = true;
            this.ColumnGoodsName.ReadOnly = true;
            this.ColumnRetailSpecification.ReadOnly = true;
            this.ColumnRemark.ReadOnly = true;
            this.ColumnMoney.ReadOnly = true;
            gConstructEnd = true;
          
        }
      
        private void _InitOrderMsg()
        {
            this.label_orderFee.Text = "0.00";
            this.label_receiveFee.Text = "0.00";
            this.label_changeFee.Text = "0.00";
            this.label_total_product_count.Text = "0";
           // ShowPrinterFlag();
        }
        private void _GeneraterOrder()
        {
            //跳到收钱窗口
            string strProductList = "";
            _GenerateProductListForOrder(ref strProductList);
            if (!CenterContral.GenerateOrder(strProductList))
            {
                return;
            }
        }
        /*
       * 弹出框，展示实收多少钱
       */
        private void _Windows_ShowRecieveMoeny()
        {

            _UpdateStockBaseMsg();
            _GeneraterOrder();
            CenterContral.Window_RecieveMoney.ShowByProductListWindow();
            //this.Hide();
        }

     

        private bool  _GenerateProductListForOrder(ref string strProductList)
        {
            int rowCount = CenterContral.oStockOutDTO.details.Count;
            for (int index = 0; index < rowCount; ++index)
            {
                var oStockOutDetail= CenterContral.oStockOutDTO.details[index];
                strProductList += oStockOutDetail.goodsId + ":";
                strProductList += oStockOutDetail.orderCount + ":";
                strProductList += oStockOutDetail.subtotal + "|";
            }
            return true;
        }

        public void UpdateTextShow()
        {
            //列出订单总价
            UpdateProductListWindowsMoneyLabel();

            //列出支付信息
            _SetCheckoutGrid();

            //更新会员信息
            SetMemberInfo();
            //挂单
            SetLocalSaveDataNumber();
            //折扣
            UpdateDiscount();
            //显示打印机状态
            ShowPrinterFlag();
        }
        public void ShowPrinterFlag()
        {
            if (CenterContral.oSystem.bPrinterOpen )
            {
                this.label_Sate.Text = "打印小票";
                return;
            }
            this.label_Sate.Text = "关闭打印";
            return;
        }

        internal void ShowWindows()
        {
            this.Show();
            this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.INDEX].Value = this.dataGridView_productList.RowCount;
            UpdateTextShow();
        }
        internal void EscapeShowByRecieveWindows()
        {
            this.Show();
            this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.INDEX].Value = this.dataGridView_productList.RowCount;
            UpdateTextShow();
        }
        internal void CloseOrderAndPrintOrderByCenter()
        {

            this.Show();
            
 
         
            _ShowPayTipsInProductListAndSaveOrderMsg();

            //this.dataGridView_productList.CurrentCell = this.dataGridView_productList.Rows[0].Cells[CELL_INDEX.GOODS_BARCODE];
            //this.dataGridView_productList.BeginEdit(true);
     
            _ResetAllData();
            UpdateTextShow();
        }

       

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
        private void _ShowPayTipsInProductListAndSaveOrderMsg()
        {
            if (CenterContral.oStockOutDTO.Base.ChangeFee == 0)
            {
                System.Windows.Forms.MessageBox.Show("下单成功,无需找零");
            }
            else if (CenterContral.oStockOutDTO.Base.ChangeFee > 0)
            {
                System.Windows.Forms.MessageBox.Show("下单成功,请记得找零：" + CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.ChangeFee) + " 元");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("程序出现找零异常，请联系开发看");
            }
            //把下单的商品列表给缓存
        }
        public void UpdateProductListWindowsMoneyLabel()
        {
            CommUiltl.Log("_SetDataGridViewOrderFee");
            this.label_orderFee.Text = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.orderAmount);
            this.label_receiveFee.Text = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.RealRecieveFee);
            this.label_changeFee.Text = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.ChangeFee);
            this.label_total_product_count.Text = CenterContral.oStockOutDTO.Base.totalProductCount.ToString();
        }

        private void _SetCheckoutGrid()
        {
            this.dataGridView_checkout.Rows.Clear();
            foreach (var item in CenterContral.oStockOutDTO.payments)
            {
                int i = this.dataGridView_checkout.Rows.Add();
                this.dataGridView_checkout.Rows[i].Cells[0].Value = item.payTypeDesc;
                this.dataGridView_checkout.Rows[i].Cells[1].Value = CommUiltl.CoverMoneyUnionToStrYuan(item.payAmount);
            }
            this.dataGridView_checkout.CurrentCell = null;
            this.dataGridView_checkout.ClearSelection();
        }



        private void _SetCurrentCell()
        {
            _GoProductList();
        }
        private void _GoProductList()
        {
            this.dataGridView_checkout.CurrentCell = null;
            this.dataGridView_checkout.ClearSelection();
            //默认最后一行正在编辑中
            this.dataGridView_productList.Focus();
            this.dataGridView_productList.Select();
            this.dataGridView_productList.CurrentCell = this.dataGridView_productList.Rows[this.dataGridView_productList.RowCount - 1].Cells[1];
            this.dataGridView_productList.BeginEdit(true);
        }



        private bool _ResetMoneyByRow(int rowIndex, int columnIndex)
        {
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //价钱为空，就停止
                return false;
            }
            long actualCount = 0, unitPrice = 0;

            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.GOODS_BARCODE].Value))
            {
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.GOODS_BARCODE);
                MessageBox.Show("错误条码" );
                return false;
            }
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value))
            {
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.PRODUCT_RetailDetailCount);
                MessageBox.Show("数量错误,不能为空");
                return false;
            }
            CommUiltl.Log("rowIndex:rowIndex" + rowIndex + " GOODS_BARCODE:" + this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.GOODS_BARCODE].Value.ToString());
            CommUiltl.Log("detail count:"+ CenterContral.oStockOutDTO.details.Count);
            
            string strRetailCount = this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value.ToString();
            if (!_CheckRetailAccount(CenterContral.oStockOutDTO.details[rowIndex],strRetailCount,ref actualCount))
            {
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.PRODUCT_RetailDetailCount);
                //_SetPointToResetCurrentCell(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount]);
                MessageBox.Show("错误数量" );
                return false;
            }
          
            if (!CommUiltl.ConverStrYuanToUnion(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_NORMAL_PRICE].Value, out unitPrice))
            {
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.PRODUCT_NORMAL_PRICE);
                MessageBox.Show("错误金额" );
                return false;
            }

            CommUiltl.Log("unitPrice:"+ unitPrice);
            _UpdateStockOutDtoDetailMoney(CenterContral.oStockOutDTO.details[rowIndex], strRetailCount, actualCount,unitPrice);
            this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_MONEY].Value = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.details[rowIndex].subtotal);
            _UpdateStockBaseMsg();
            return true;
        }

        private void _UpdateStockOutDtoDetailMoney(DbStockOutDetail stockOutDetail,string strRetailCount, long actualCount, long unitPrice)
        {
            stockOutDetail.unitPrice = unitPrice;
            if (stockOutDetail.isBarCodeMoneyGoods == CenterContral.IS_BARCODE_MOENY_GOODS)
            {
                //计重类数量
                long barcodeCount = 0;
                CommUiltl.ConverStrYuanToUnion(strRetailCount, out barcodeCount);
                stockOutDetail.barcodeCount = barcodeCount;
                stockOutDetail.orderCount = stockOutDetail.orderCount;//实际数量不变
                stockOutDetail.subtotal = CommUiltl.CaculateBarCodeTotalMoney(stockOutDetail.barcodeCount, stockOutDetail.unitPrice);
                return;
            }

            stockOutDetail.orderCount = actualCount;
            stockOutDetail.subtotal = stockOutDetail.orderCount* stockOutDetail.unitPrice;
        }

        private bool _CheckRetailAccount(DbStockOutDetail oStockOutDetail,string strRetailCount,ref long actualCount )
        {
            if (oStockOutDetail.isBarCodeMoneyGoods == CenterContral.IS_BARCODE_MOENY_GOODS)
            {
                //计重类数量   
                long barcodeCount = 0;
                return CommUiltl.ConverStrYuanToUnion(strRetailCount, out barcodeCount);

            }
            return CommUiltl.CoverStrToLong(strRetailCount, out actualCount);
        }

        private void _UpdateStockBaseMsg()
        {
            long rowCount = CenterContral.oStockOutDTO.details.Count;
            long orderPrice = 0, subtotalCount = 0;
            for (int index = 0; index < rowCount; ++index)
            {
                subtotalCount += CenterContral.oStockOutDTO.details[index].orderCount;
                orderPrice += CenterContral.oStockOutDTO.details[index].subtotal;
            }
            CenterContral.updateOrderAmount(orderPrice,ref CenterContral.oStockOutDTO);
            string strOrderPrice = CommUiltl.CoverMoneyUnionToStrYuan(CenterContral.oStockOutDTO.Base.orderAmount);
       
            CenterContral.oStockOutDTO.Base.totalProductCount = subtotalCount;
            UpdateTextShow();
            return;
        }

        private void GetProductInfoByBarcode(int rowIndex, int columnIndex)
        {
            DataGridViewRow currentRow = this.dataGridView_productList.Rows[rowIndex];
            CommUiltl.Log("GetProductInfoByBarcode currentRow:"+ currentRow.Index);
            if (CommUiltl.IsObjEmpty(currentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value) 
                && CommUiltl.IsObjEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //条形码为空
                return;
            }
            if ( !CommUiltl.IsObjEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value) )
            {
                //已经有商品
                return;
            }

            string strKeyWord = currentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value.ToString().Trim();
            if (strKeyWord == "")
            {
                _SetPointToResetCurrentCell(currentRow.Cells[columnIndex]);
                MessageBox.Show("当前行有问题 rowIndex:" + rowIndex + " columnIndex" + columnIndex);
                return;
            }
            //根据-商品货号-取出商品（商品货号：可能是条码，可能是商品号）
            ProductPricingInfoResp oStockOutDetailInfoResp = new ProductPricingInfoResp();
            if (!CenterContral.GetGoodsByGoodsKey(strKeyWord ,ref oStockOutDetailInfoResp))
            {
                _SetPointToResetCurrentCell(currentRow.Cells[columnIndex]);
                return;
            }
            if (oStockOutDetailInfoResp.data.list.Count == 0)
            {
                MessageBox.Show("未找到商品资料");
                _SetPointToResetCurrentCell(currentRow.Cells[columnIndex]);
                return;
            }
            //商品个数大于1
            if (oStockOutDetailInfoResp.data.list.Count >1 )
            {
                _SetPointToResetCurrentCell(currentRow.Cells[columnIndex]);
                //唤起界面，对商品进行筛选
                _CallWindowsSelectGoood(oStockOutDetailInfoResp.data.list);
                gSelectGoodsRow = currentRow;
                return;
            }
            ProductPricing  productInfo=oStockOutDetailInfoResp.data.list[0];
            productInfo.postKeyWord = strKeyWord;
            //单个商品
            _AddProducntInfoToDataGridViewProductList(currentRow,productInfo);
            //将光标移动到数量里面
            _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.PRODUCT_RetailDetailCount);
        }

        private void _CallWindowsSelectGoood(List<ProductPricing> list)
        {
            CenterContral.CallWindowsSelectGooodByProudctList( list);
        }
        DataGridViewRow gSelectGoodsRow;
        public void CallBackBySelectGoodWindow(ProductPricing productInfo)
        {
            //选中商品后回调这里
            this.Show();
            DataGridViewRow currentRow = this.dataGridView_productList.CurrentRow;
            if (!CommUiltl.IsObjEmpty(currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                CommUiltl.Log("CallBackBySelectGoodWindow CommUiltl.IsObjEmpty currentRow.Index:" + currentRow.Index);
                //已经有商品
                return;
            }
            productInfo.postKeyWord = currentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value.ToString().Trim();
            _AddProducntInfoToDataGridViewProductList(currentRow,productInfo);
            //将光标移动到数量里面
            this.dataGridView_productList.CurrentCell = currentRow.Cells[CELL_INDEX.PRODUCT_RetailDetailCount];
            this.dataGridView_productList.BeginEdit(true);
        }
        public void EecBySelectGoodWindow()
        {
            //选中商品后回调这里
            this.Show();
            DataGridViewRow currentRow = this.dataGridView_productList.CurrentRow;
            //将光标移动到数量里面
            this.dataGridView_productList.CurrentCell = currentRow.Cells[CELL_INDEX.GOODS_BARCODE];
            this.dataGridView_productList.BeginEdit(true);
        }
        public void _AddProducntInfoToDataGridViewProductList(DataGridViewRow currentRow, ProductPricing productInfo)
        {
            //单个商品
            DbStockOutDetail detail = new DbStockOutDetail();
            CenterContral.ProductTostockDetail(productInfo, ref detail);
            //设置行里面商品信息
            SetRowsByStockOutDetail(currentRow, detail);
            CenterContral.oStockOutDTO.details.Add(detail);
            CommUiltl.Log(" add Main.oStockOutDTO.details.Count:" + CenterContral.oStockOutDTO.details.Count);
            //更新订单价钱
            _UpdateStockBaseMsg();
        }
        private void SetRowsByStockOutDetail(DataGridViewRow currentRow, DbStockOutDetail detail)
        {
            currentRow.Cells[CELL_INDEX.INDEX].Value = currentRow.Index + 1;
            currentRow.Cells[CELL_INDEX.GOODS_BARCODE].ReadOnly = true;//请求到后台的条码，不允许修改，只能删除，防止误操作
           currentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value = detail.barcode;
            currentRow.Cells[CELL_INDEX.PRODUCT_NAME].Value = detail.goodsName;
            CommUiltl.Log("detail.goodsShowSpecification:"+ detail.goodsShowSpecification);
            currentRow.Cells[CELL_INDEX.PRODUCT_SPECIFICATION].Value = detail.goodsShowSpecification;

            string RetailPrice = CommUiltl.CoverMoneyUnionToStrYuan(detail.unitPrice);
            currentRow.Cells[CELL_INDEX.PRODUCT_NORMAL_PRICE].Value = RetailPrice;

            currentRow.Cells[CELL_INDEX.PRODUCT_REMARK].Value = detail.remark;
            //currentRow.Cells[CELL_INDEX.PRODUCT_JSON].Value = JsonConvert.SerializeObject(detail);
            //总价和数量
            currentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value =   CommUiltl.CoverMoneyUnionToStrYuan(detail.subtotal);
            currentRow.Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value = CenterContral.GetGoodsCount(detail);
        }



        
        private void productListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            CommUiltl.Log("row:" + e.RowIndex + " Column:" + e.ColumnIndex);
        }


        private void productListDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            CommUiltl.Log("row:" + e.RowIndex + " Column:" + e.ColumnIndex);


        }
        private bool gDeleteEventFlag=false;
        private void productListDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CommUiltl.Log("begin e.RowIndex："+ e.RowIndex + " ColumnIndex" + e.ColumnIndex);

            if (!gConstructEnd)
            {
                //未初始化行表的时候，这里是空的
                return;
            }
            if (gDeleteEventFlag)
            {
                CommUiltl.Log("dataGridView_productList_RowsRemoved row:" + e.RowIndex + " Column");
                //删除数据的时候，会走到这里，这里面行数已经删除
                gDeleteEventFlag = false;
                return;
            }
            CommUiltl.Log("row:" + this.dataGridView_productList.CurrentCell.RowIndex + " Column:" + this.dataGridView_productList.CurrentCell.ColumnIndex);
            if (this.dataGridView_productList.CurrentCell.ColumnIndex != e.ColumnIndex || this.dataGridView_productList.CurrentCell.RowIndex != e.RowIndex)
            {
                CommUiltl.Log("ColumnIndex != e.ColumnIndex");
                //非当前行
                return;
            }
            if (e.ColumnIndex == CELL_INDEX.GOODS_BARCODE)
            {
                CommUiltl.Log(" CELL_INDEX.PRODUCT_CODE");
                //MessageBox.Show("end edit code PRODUCT_CODE RowIndex :" + e.RowIndex + " CellIndex"+e.ColumnIndex   );
                GetProductInfoByBarcode(e.RowIndex, e.ColumnIndex);
                return;
            }
            if (this.dataGridView_productList.CurrentRow.IsNewRow)
            {
                //最后一行是新行不做处理，因为最后一行是新行，说明还没有数据
                CommUiltl.Log("this.dataGridView_productList.CurrentRow.IsNewRow：" + e.RowIndex + " ColumnIndex" + e.ColumnIndex);
                return;
            }
            if (e.ColumnIndex == CELL_INDEX.PRODUCT_RetailDetailCount)
            {
                //条码必须输入
                if (!_CheckIsKeyword(e.RowIndex, e.ColumnIndex))
                {
                    return;
                }
                //如果是扫码，那么就移动到下一行
                if ( !_isBarcode(e.RowIndex, e.ColumnIndex))
                {
                    return;
                }
                //将光标移动到价钱
                CommUiltl.Log(" e.ColumnIndex == CELL_INDEX.PRODUCT_RetailDetailCount and  _ResetMoneyByRow and _SetCurrentPointToRetailDetailCount  ");
                if (!_ResetMoneyByRow(e.RowIndex, e.ColumnIndex))
                {
                    return;
                }
                _SetCurrentPointToRetailDetailCount(e.RowIndex, CELL_INDEX.PRODUCT_NORMAL_PRICE);
                return;
            }
            if (e.ColumnIndex == CELL_INDEX.PRODUCT_NORMAL_PRICE && !this.dataGridView_productList.CurrentRow.IsNewRow)
            {
                CommUiltl.Log("PRODUCT_NORMAL_PRICE");
                //条码必须输入
                if (!_CheckIsKeyword(e.RowIndex, e.ColumnIndex))
                {
                    return;
                }
                if (!_ResetMoneyByRow(e.RowIndex, e.ColumnIndex))
                {
                    return;
                }
                //将光标移动到下一行条码
                _GotoNextBarcode(e.RowIndex);
                return;
            }

        }

        private bool _isBarcode(int rowIndex, int columnIndex)
        {
            CommUiltl.Log("_isBarcode");
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                //价钱为空，就停止
                return false;
            }
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value))
            {
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.PRODUCT_RetailDetailCount);
                MessageBox.Show("数量错误,不能为空");
                return false;
            }
            //判断是否是条码
            string strBarcode = this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value.ToString();
            if (strBarcode.Length  < 5)//说明是数字
            {
                return true;
            }
            //处理条码
            //恢复数量
            if (CenterContral.oStockOutDTO.details[rowIndex].isBarCodeMoneyGoods == CenterContral.IS_BARCODE_MOENY_GOODS)
            {
                //条码带有金额的商品,数量=总价/单价
                this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value= CommUiltl.CoverUnionTo3rd(CenterContral.oStockOutDTO.details[rowIndex].barcodeCount);
            }else
            {
                this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.PRODUCT_RetailDetailCount].Value = CenterContral.oStockOutDTO.details[rowIndex].orderCount;
            }
            //添加一行
            this.dataGridView_productList.Rows.Add();
            //光标移动到下一行的条码
            this.dataGridView_productList.Rows[rowIndex + 1].Cells[CELL_INDEX.GOODS_BARCODE].Value = strBarcode;
            _GotoNextBarcode(rowIndex);
            GetProductInfoByBarcode(rowIndex + 1, CELL_INDEX.GOODS_BARCODE);
            CommUiltl.Log("_isBarcode");
            return false;
        }

        private bool _CheckIsKeyword(int rowIndex, int columnIndex)
        {
            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.GOODS_BARCODE].Value))
            {
                MessageBox.Show("请输入商品货号", "操作提示");
                _SetCurrentPointToRetailDetailCount(rowIndex, CELL_INDEX.GOODS_BARCODE);
                return false;
            }
            return true;
        }
        bool gMoveToRetailDetailCountFlag = false;//用来控制，是否要移动光标到商品数量
        private void _SetCurrentPointToRetailDetailCount(int rowIndex, int columnIndex)
        {
            CommUiltl.Log("_GotoNextBarcode RowIndex:" + rowIndex + " this.dataGridView_productList.RowCount:" + this.dataGridView_productList.RowCount);
           
            if (this.dataGridView_productList.Rows[rowIndex + 1].IsNewRow)
            {
                CommUiltl.Log("_SetCurrentPointToRetailDetailCount row:" + rowIndex + " Column" + columnIndex);
                gMoveToRetailDetailCountFlag = true;
                gCurrentCell = this.dataGridView_productList.Rows[rowIndex].Cells[columnIndex];
                return;
            }
        }
        private void selectDatagrivdViewProductList()
        {
            this.dataGridView_productList.Select();
            this.dataGridView_productList.BeginEdit(true);
        }

        bool gMoveToNextBarcodeFlag = false;
        private void _GotoNextBarcode(int rowIndex)
        {
            CommUiltl.Log("_GotoNextBarcode RowIndex:"+ rowIndex + " this.dataGridView_productList.RowCount:" + this.dataGridView_productList.RowCount);
            CommUiltl.Log("_GotoNextBarcode PRODUCT_CODE:" + this.dataGridView_productList.Rows[rowIndex].Cells[CELL_INDEX.GOODS_BARCODE].Value);
            if (this.dataGridView_productList.Rows[rowIndex+1 ].IsNewRow)
            {
                CommUiltl.Log("RowIndex == this.dataGridView_productList.RowCount -2");
                gMoveToNextBarcodeFlag = true;
                gCurrentCell = this.dataGridView_productList.Rows[rowIndex + 1].Cells[CELL_INDEX.GOODS_BARCODE];
                return ;
            }

        }

        private void productListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            CommUiltl.Log("begin ");
            if (this.dataGridView_productList.CurrentCell == null)
            {
                CommUiltl.Log("this.dataGridView_productList.CurrentCell == null");
                return;
            }
            if (!gConstructEnd)
            {
                CommUiltl.Log("gConstructEnd ");
                //未初始化行表的时候，这里是空的
                return;
            }
            if (gCurrentCell ==null )
            {
                CommUiltl.Log("gCurrentCell== null ");
                return;
            }
            CommUiltl.Log("gCurrentCell:" + gCurrentCell);
            if (gCurrentCell.RowIndex<0)
            {
                CommUiltl.Log("gCurrentCell.RowIndex<0");
                return;
            }
            if (gResetRow)
            {
                CommUiltl.Log("gResetRow:" + gResetRow);
                gResetRow = false;
                this.dataGridView_productList.CurrentCell = gCurrentCell;
                this.dataGridView_productList.CurrentCell.Selected = true;
                return;
            }
            if (gMoveToRetailDetailCountFlag)
            {
                CommUiltl.Log("gMoveToRetailDetailCountFlag:" + gMoveToRetailDetailCountFlag);
              
                gMoveToRetailDetailCountFlag = false;
                this.dataGridView_productList.CurrentCell = gCurrentCell;
                this.dataGridView_productList.CurrentCell.Selected = true;
            }
            if (gMoveToNextBarcodeFlag)
            {
                CommUiltl.Log("gMoveToNextBarcodeFlag:" + gMoveToNextBarcodeFlag);
                gMoveToNextBarcodeFlag = false;
                this.dataGridView_productList.CurrentCell = gCurrentCell;
                this.dataGridView_productList.CurrentCell.Selected = true;
            }
            _GoCurrentBarcodeIfNeed();

            CommUiltl.Log("end ");
        }

        private void productListDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            CommUiltl.Log("row:" +e.RowIndex );
            this.dataGridView_productList.Rows[e.RowIndex].Cells[CELL_INDEX.INDEX].Value = e.RowIndex + 1;
        }

        private void productListDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CommUiltl.Log("dataGridView_productList_RowsRemoved row:" + e.RowIndex + " RowCount" + this.dataGridView_productList.RowCount);
            for (int i = 0, rowIndex = 0; i < this.dataGridView_productList.RowCount; ++i)
            {
                this.dataGridView_productList.Rows[i].Cells[CELL_INDEX.INDEX].Value = rowIndex + 1;
                ++rowIndex;
            }
        }

        private bool checkCharIfIsNum(Keys keyData)
        {
            return true;
            if (!this.dataGridView_productList.IsCurrentCellInEditMode)
            {
                return true;
            }
            //不允许出现字母
            if (
                 (System.Windows.Forms.Keys.A <= keyData && keyData <= System.Windows.Forms.Keys.Z)
                )
            {
                return false;
            }
            return true;
        }
        Control gDataGriviewTextChangedContral;//因为datagrivew 没有text change事件，所以就用这个变量来控制
        private void dataGridView_productList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            CommUiltl.Log("dataGridView_productList_EditingControlShowing:dataGridViewTextBox_KeyPress");
            gDataGriviewTextChangedContral = e.Control;
            gDataGriviewTextChangedContral.TextChanged -= tb_TextChanged;
            gDataGriviewTextChangedContral.TextChanged += tb_TextChanged;
            //DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            //tb.KeyPress -= new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
            //tb.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            if (this.dataGridView_productList.IsCurrentCellInEditMode
                   && this.dataGridView_productList.CurrentCell !=null
                   && (this.dataGridView_productList.CurrentCell.ColumnIndex ==CELL_INDEX.PRODUCT_RetailDetailCount||
                   this.dataGridView_productList.CurrentCell.ColumnIndex == CELL_INDEX.PRODUCT_NORMAL_PRICE
                   )
                )
            {
                CommUiltl.Log("tb_TextChanged:" + gDataGriviewTextChangedContral.Text + " current value:" + this.dataGridView_productList.CurrentCell.Value);
                this.dataGridView_productList.CurrentCell.Value = gDataGriviewTextChangedContral.Text;
                CommUiltl.Log("after tb_TextChanged:" + gDataGriviewTextChangedContral.Text + " current value:" + this.dataGridView_productList.CurrentCell.Value);
                //when i press enter,bellow code never run?

            }
        }

        private void dataGridViewTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dataGridView_productList.IsCurrentCellInEditMode)
            {
                CommUiltl.Log("dataGridViewTextBox_KeyPress:" + e.KeyChar + " current value:" + this.dataGridView_productList.CurrentCell.Value);
                //when i press enter,bellow code never run?
            }


        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            CommUiltl.Log("keyData:" + keyData);
            //因为datagridview无法捕获表格里面输入的字符，所以要捕获
            if (!checkCharIfIsNum(keyData))
            {
                return true;
            }
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Enter:
                    {
                        //MessageBox.Show("Keys.Enter: CurrentCell.RowIndex:" + this.productListDataGridView.CurrentCell.RowIndex);
                        if (this.dataGridView_productList.IsCurrentCellInEditMode)
                        {
                            CommUiltl.Log(" IsCurrentCellInEditMode ");
                            if (CommUiltl.IsObjEmpty(this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value)&& this.dataGridView_productList.CurrentRow.IsNewRow)
                            {
                                if ( CenterContral.oStockOutDTO.Base.orderAmount == 0 )
                                {
                                    //没有输入商品，不允许拉起支付方式
                                    return true;
                                }

                                //唤起收银界面
                                CommUiltl.Log(" CELL_INDEX.PRODUCT_CODE empty ");
                                this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.INDEX].Value = "";
                                _Windows_ShowRecieveMoeny();
                                return true;
                                //  return base.ProcessCmdKey(ref msg, keyData);
                            }
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    break;
                case System.Windows.Forms.Keys.Home:
                    {
                        if (CenterContral.oStockOutDTO.Base.orderAmount == 0)
                        {
                            //没有输入商品，不允许拉起支付方式
                            return true;
                        }
                        _Windows_ShowRecieveMoeny();
                        return true;
                    }
              
                case System.Windows.Forms.Keys.Tab:
                    {
                        //tabl的操作被禁止
                        return true;
                    }
                // case System.Windows.Forms.Keys.OemMinus:
                //case System.Windows.Forms.Keys.Subtract:
                case System.Windows.Forms.Keys.Delete:
                    {
                        _DeleteCurrentRow();
                        return true;
                    }
                case System.Windows.Forms.Keys.F1:
                    {
                        //重新打印小票
                        RePrintThisOrder();
                        return true;
                    }
                case System.Windows.Forms.Keys.F2:
                    {
                        //零售退货
                        CenterContral.ShowReturnSerialNumberWindow();
                        return true;
                    }
                case System.Windows.Forms.Keys.F3:
                    {
                        //折扣
                        Discount();
                        return true;
                    }
              
                case System.Windows.Forms.Keys.F4:
                    {
                        //交易挂单
                        SaveStock();
                        //  return base.ProcessCmdKey(ref msg, keyData);
                        return true;
                    }
                case System.Windows.Forms.Keys.F5:
                    {
                        //挂单恢复
                        RecoverStock();
                        return true;
                    }
                case System.Windows.Forms.Keys.F6:
                    {
                        //开钱箱
                        CenterContral.CloseMoneyBox(CenterContral.CloseMoneyBoxComm);
                        return true;
                    }
                case System.Windows.Forms.Keys.F9:
                    {
                        CenterContral.Show_MemberInfoWindow_By_ProductList();
                        //this.Hide();
                        return true;
                    }
                case System.Windows.Forms.Keys.F10:
                    {
                        //功能菜单
                        CenterContral.CallFunctionMenuWindow();
                        return true;
                    }
                case System.Windows.Forms.Keys.F11:
                    {
                        ChangePrinterStatus();
                        return true;
                    }
                //case System.Windows.Forms.Keys.End:
                //    {
                //        CenterContral.flagCallSetting = CenterContral.FLAG_PRODUCTlIST_WINDOW;
                //        CenterContral.Windows_SettingDefaultMsgWindow.ShowByCenter();
                //        return true;
                //    }

                case System.Windows.Forms.Keys.Insert:
                    {
                        //取消订单
                        CanCelOrder();
                        return true;
                    }
              
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void ChangePrinterStatus()
        {
            if (CenterContral.oSystem.bPrinterOpen)
            {
                var confirmPayApartResult = MessageBox.Show("是否要关闭打印",
                                     "打印确认",
                                     MessageBoxButtons.YesNo);
                if (confirmPayApartResult != DialogResult.Yes)
                {
                    return;
                }
                CenterContral.oSystem.bPrinterOpen = false;
            }else
            {
                var confirmPayApartResult = MessageBox.Show("是否要开启打印",
                     "打印确认",
                     MessageBoxButtons.YesNo);
                if (confirmPayApartResult != DialogResult.Yes)
                {
                    return;
                }
                CenterContral.oSystem.bPrinterOpen = true;
            }
            CenterContral.SetSystemInfoToDb();
            UpdateTextShow();
            return;
        }
        private void _DeleteCurrentRow()
        {
            //删除操作，把当前行给删除
            if (this.dataGridView_productList.CurrentCell == null || this.dataGridView_productList.CurrentCell.RowIndex
                 < 0 )
            {
                CommUiltl.Log("Keys.Delete CurrentCell ==null ");
                return;
            }
            CommUiltl.Log("Keys.Delete CurrentCell row count :" + this.dataGridView_productList.Rows.Count);
            DataGridViewRow oCurrentRow = this.dataGridView_productList.CurrentRow;

            if (oCurrentRow.IsNewRow)//最新一行
            {
                CommUiltl.Log("oCurrentRow.IsNewRowrow count :" + this.dataGridView_productList.Rows.Count);
                //最新的一行删除按钮，就自动移动光标到上一行
                if (oCurrentRow.Index >= 1)//确保能移动到上一行
                {
                    this.dataGridView_productList.CurrentCell = this.dataGridView_productList.Rows[oCurrentRow.Index - 1].Cells[CELL_INDEX.GOODS_BARCODE];
                }
                return;
            }

            //删除当前行
            CommUiltl.Log("Keys.Delete !IsNewRow oCurrentRow.Index:" + oCurrentRow.Index);
            if (!CommUiltl.IsObjEmpty(oCurrentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value))
            {
                string showTips = "是否要删除商品:" + oCurrentRow.Cells[CELL_INDEX.PRODUCT_NAME].Value;
                var confirmPayApartResult = MessageBox.Show(showTips,
                                      "取消订单操作",
                                      MessageBoxButtons.YesNo);
                if (confirmPayApartResult != DialogResult.Yes)
                {
                    return;
                }
                if (this.dataGridView_productList.CurrentCell.ColumnIndex== CELL_INDEX.PRODUCT_RetailDetailCount ||
                    this.dataGridView_productList.CurrentCell.ColumnIndex == CELL_INDEX.PRODUCT_NORMAL_PRICE
                    )
                {
                    gDeleteEventFlag = true;
                }
                //先删除数据
                CenterContral.oStockOutDTO.details.RemoveAt(oCurrentRow.Index);
                CommUiltl.Log("oCurrentRow.Index ：" + oCurrentRow.Index);
                //再删除行
                this.dataGridView_productList.Rows.RemoveAt(oCurrentRow.Index);
                CommUiltl.Log("RemoveAt.Index：" + oCurrentRow.Index);

                _UpdateStockBaseMsg();
                return;
            }
            //关键词为空，删除当前行即可
            CommUiltl.Log("Keys.Delete IsObjEmpty:" + oCurrentRow.Index);
            if (!CommUiltl.IsObjEmpty(this.dataGridView_productList.Rows[oCurrentRow.Index + 1].Cells[CELL_INDEX.INDEX].Value))
            {
                this.dataGridView_productList.Rows[oCurrentRow.Index + 1].Cells[CELL_INDEX.INDEX].Value = oCurrentRow.Index+1;//最后一行的行号要变更
            }
            this.dataGridView_productList.Rows.RemoveAt(oCurrentRow.Index);
            return;



        }
        /**********************************取消订单******************************************/
        private void CanCelOrder()
        {
            //取消订单
            string showTips = "是否要取消订单";
            var confirmPayApartResult = MessageBox.Show(showTips,
                                  "取消订单操作",
                                  MessageBoxButtons.YesNo);

            if (confirmPayApartResult != DialogResult.Yes)
            {
                return;
            }
            string strProductList = "";
            _GenerateProductListForOrder(ref strProductList);
            if (!CenterContral.CanCelOrder(CenterContral.oStockOutDTO))
            {
                return;
            }
    
            MessageBox.Show("取消成功", "取消订单操作");
            _ResetAllData();

        }
        /**********************************整单打折******************************************/
        private void Discount()
        {
            CenterContral.Window_DiscountWindows.ShowWithDiscountMsg();
        }
        /**********************************挂单**********************************************/
        private void SaveStock()
        {
            if (CenterContral.oStockOutDTO.details.Count == 0 )
            {
                MessageBox.Show("当前没有商品，无法挂单",
                                 "挂单操作");
                return;
            }
            string showTips = "是否要挂单";
            var confirmPayApartResult = MessageBox.Show(showTips,
                                  "挂单操作",
                                  MessageBoxButtons.YesNo);

            if (confirmPayApartResult != DialogResult.Yes)
            {
                return;
            }
            _UpdateStockBaseMsg();
            string strProductList = "";
            _GenerateProductListForOrder(ref strProductList);
            if (!CenterContral.SaveStock(strProductList))
            {
                return;
            }
            CommUiltl.Log("_ResetAllData");
           _ResetAllData();
        }

        private void RecoverStock()
        {
            CommUiltl.Log("Main.oSaveSotckOut.listStock.Count: "+ CenterContral.oLocalSaveStock.listStock.Count);
            if (CenterContral.oLocalSaveStock.listStock.Count==0)
            {
                MessageBox.Show("无挂单", "挂单操作");
                return;
            }

            string showTips = "本单将会挂起来，恢复上一个挂单";
            if (ProductListIsEmpty(CenterContral.oStockOutDTO))
            {
                showTips = "是否要恢复上一个订单";
            }
          
            var confirmPayApartResult = MessageBox.Show(showTips,
                                  "挂单操作",
                                  MessageBoxButtons.YesNo);

            if (confirmPayApartResult != DialogResult.Yes)
            {
                return;
            }
            //当前订单保存订单
            _UpdateStockBaseMsg();
            string strProductList = "";
            _GenerateProductListForOrder(ref strProductList);

            if ( !ProductListIsEmpty(CenterContral.oStockOutDTO))
            {
                CommUiltl.Log("ProductListIsEmpty " );
                //如果不是空的单据，那么就要保存
                if (!CenterContral.SaveStock(strProductList))
                {
                    return;
                }
            }
            _ResetAllData();
            CenterContral.GetSaveOrderToCurrentMsg();
            SetProductListWindowByStockOut(CenterContral.oStockOutDTO);

        }

        private bool ProductListIsEmpty(DbStockOutDTO oStockOutDTO)
        {
            return CenterContral.oStockOutDTO.details.Count ==0 ;
        }

        public void SetProductListWindowByStockOut(DbStockOutDTO oStockOutDTO)
        {
            //列出商品
            int rowIndex;
            for (int i=0;i< oStockOutDTO.details.Count;++i)
            {
                rowIndex=this.dataGridView_productList.Rows.Add();
                CommUiltl.Log("rowIndex "+ rowIndex);
                CommUiltl.Log("i " + i);
                CommUiltl.Log("oStockOutDTO.details[i] " + oStockOutDTO.details[i]);
                SetRowsByStockOutDetail(this.dataGridView_productList.Rows[rowIndex], oStockOutDTO.details[i]);
            }
            UpdateTextShow();
           
        }


        

        private  DataGridViewCell gCurrentCell=null;
        private bool gResetRow = false;

      
        private void _ResetAllData()
        {
            gConstructEnd = false;
            //使用clear事件，会触发行修改事件

            this.dataGridView_productList.Rows.Clear();
            this.dataGridView_checkout.Rows.Clear();
            CenterContral.Clean();
            _InitOrderMsg();
            this.dataGridView_productList.CurrentCell = this.dataGridView_productList.Rows[0].Cells[CELL_INDEX.GOODS_BARCODE];
            this.dataGridView_productList.BeginEdit(true);
            gConstructEnd = true;

        }

        private void _GoCurrentBarcodeIfNeed()
        {
            if (this.dataGridView_productList.ColumnCount < CELL_INDEX.PRODUCT_MONEY +1)
            {
                CommUiltl.Log("ColumnCount < PRODUCT_MONEY");
                //初始化阶段
                return;
            }
            if (this.dataGridView_productList.IsCurrentCellInEditMode
                &&!CommUiltl.IsObjEmpty(this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.GOODS_BARCODE].Value)
                &&CommUiltl.IsObjEmpty(this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.PRODUCT_MONEY].Value))
            {
                CommUiltl.Log("go CurrentCell ");
                //第一个
                this.dataGridView_productList.CurrentCell = this.dataGridView_productList.CurrentRow.Cells[CELL_INDEX.GOODS_BARCODE];
            }
        }



        void orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            CommUiltl.Log("begin");
            if (gResetRow)
            {
                CommUiltl.Log("gResetRow");
                gResetRow = false;
            }
        }
     
        private void _SetPointToResetCurrentCell(DataGridViewCell currentCell)
        {
            CommUiltl.Log("row:"+ currentCell.RowIndex +" Column"+ currentCell.ColumnIndex);
            gResetRow = true;
            gCurrentCell = currentCell;
        }

        void orderDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        private void ProductListWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CenterContral.oLocalSaveStock.listStock.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("请清理挂单，再退出");
                //有挂单，不能退出
                e.Cancel = true;
                return;
            }
            this.dataGridView_productList.Rows.Clear();
            CenterContral.Windows_Login.Close();
            Environment.Exit(1);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_payWay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label_connectStatus_Click(object sender, EventArgs e)
        {

        }

        private void label_defaultUser_Click(object sender, EventArgs e)
        {

        }
        //FormWindowState LastWindowState = FormWindowState.Minimized;
        private void ProductListWindow_SizeChanged(object sender, EventArgs e)
        {
            // When window state changes
            //if (WindowState != LastWindowState)
            //{
            //    LastWindowState = WindowState;


            //    if (WindowState == FormWindowState.Maximized)
            //    {
            //        CommUiltl.Log ("FormWindowState.Maximized");
            //        this.FormBorderStyle = FormBorderStyle.None;
            //        return;
            //    }
            //    this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //    if (WindowState == FormWindowState.Normal)
            //    {
            //        CommUiltl.Log(" FormWindowState.Normal");
            //        // Restored!
            //        return;
            //    }
            //    CommUiltl.Log(" other");
            //}
        }
        //***********打印事件
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            CommUiltl.Log("CenterContral.oSystem.bPrinterOpen :" + CenterContral.oSystem.bPrinterOpen);
            if (!CenterContral.oSystem.bPrinterOpen)
            {
                //打印机关闭状态
                return;
            }

            string text = null;
            // 信息头 
            System.Drawing.Font printFont = new System.Drawing.Font
            ("Arial", 8, System.Drawing.FontStyle.Regular);

            text = CenterContral.GetTicketInfo();//获取本次购物清单数据
            CommUiltl.Log("printDocument_PrintPage");
                  CommUiltl.Log(text);
            // 设置信息打印格式 
            e.Graphics.DrawString(text, printFont, System.Drawing.Brushes.Black, 0, 5);
            CommUiltl.Log("RawPrinterHelper printDocument_PrintPage end");
        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //打印结束后
         
            //删除文件
            if (File.Exists(CenterContral.strPrintFilePath))
            {
                File.Delete(CenterContral.strPrintFilePath);
            }
        }

        public void PrintOrder(DbStockOutDTO oStockOutDTO)
        {
            if (!CenterContral.oSystem.bPrinterOpen)
            {
                //打印机关闭状态
                return;
            }

            CommUiltl.Log("RawPrinterHelper PrintOrder :" + this.printDocument.PrinterSettings.IsValid);
           
             if (!this.printDocument.PrinterSettings.IsValid)
            {
                 MessageBox.Show("找不到打印机",
                               "打印机异常"
                             );
                return;
            }
            //把当前单据写入文件
            CenterContral.printOrderMsgToFile(oStockOutDTO);
        }
        public void DoPrint()
        {
            if (!CenterContral.oSystem.bPrinterOpen)
            {
                //打印机关闭状态
                return;
            }
            //关闭钱箱
            CenterContral.CloseMoneyBox(CenterContral.CloseMoneyBoxComm);
            this.printDocument.PrintController = new StandardPrintController();//使用这个，将会隐藏打印的对话框
            this.printDocument.Print();
        }
         
        //#region 读取文本文件 打印完成后 重新删除该文件
        public void RePrintThisOrder()
        {
            CenterContral.Call_PrinterHistoryWindow();
            //var confirm = MessageBox.Show("是否要重打小票",
            //                      "重打小票",
            //                      MessageBoxButtons.YesNo);

            //if (confirm != DialogResult.Yes)
            //{
            //    return;
            //}
            //if (0 == CenterContral.oStockOutDTO.Base.RecieveFee)
            //{
            //     confirm = MessageBox.Show("未有收款，确认要打小票",
            //                      "重打小票",
            //                      MessageBoxButtons.YesNo);
            //    if (confirm != DialogResult.Yes)
            //    {
            //        return;
            //    }
            //}
          
            //if (CenterContral.oStockOutDTO.Base.RecieveFee < CenterContral.oStockOutDTO.Base.orderAmount)
            //{
            //    confirm = MessageBox.Show("收款额小于订单金额，确认要打小票",
            //                     "重打小票",
            //                     MessageBoxButtons.YesNo);
            //    if (confirm != DialogResult.Yes)
            //    {
            //        return;
            //    }
            //}
            //this.PrintOrder(CenterContral.oStockOutDTO);
        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_productList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //重新定义序号
            CommUiltl.Log("dataGridView_productList_RowsRemoved row:" + e.RowIndex + " RowCount" + this.dataGridView_productList.RowCount);

            for (int i = 0, rowIndex = 0; i < this.dataGridView_productList.RowCount; ++i)
            {
                this.dataGridView_productList.Rows[i].Cells[CELL_INDEX.INDEX].Value = rowIndex + 1;
                ++rowIndex;
            }
        }



        private void dataGridView_productList_KeyDown(object sender, KeyEventArgs e)
        {
            CommUiltl.Log("dataGridView_productList_KeyDown:" + e.KeyCode);
        }
    }

    public static class CELL_INDEX
    {
        public static int INDEX = 0;
        public static int GOODS_BARCODE = 1;
        public static int PRODUCT_NAME = 2;
        public static int PRODUCT_SPECIFICATION = 3;
        public static int PRODUCT_RetailDetailCount = 4;
        public static int PRODUCT_NORMAL_PRICE = 5;

        public static int PRODUCT_MONEY = 6;//总价
        public static int PRODUCT_REMARK = 7;
        public static int PRODUCT_JSON = 8;


        public static int ORDER_COLUMN = 1;
        public static int RECIEVE_FEE_ROW = 0;
        public static int ORDER_FEE_ROW = 1;
        public static int CHANGE_FEE_ROW = 2;
    }

}
