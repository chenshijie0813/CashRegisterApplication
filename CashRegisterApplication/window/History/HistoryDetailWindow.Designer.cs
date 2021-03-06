﻿namespace CashRegisterApplication.window.History
{
    partial class HistoryDetailWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryDetailWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_productList = new System.Windows.Forms.DataGridView();
            this.ColumnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnGoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRetailSpecification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNormalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_searisenumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_stockOutTime = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label_orderFee = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_member_account = new System.Windows.Forms.Label();
            this.label_discount_amount = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_discount_rate = new System.Windows.Forms.Label();
            this.label_state = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_productList)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_productList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_searisenumber, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.8932F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.1068F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView_productList
            // 
            this.dataGridView_productList.AllowUserToAddRows = false;
            this.dataGridView_productList.AllowUserToDeleteRows = false;
            this.dataGridView_productList.AllowUserToOrderColumns = true;
            this.dataGridView_productList.AllowUserToResizeColumns = false;
            this.dataGridView_productList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_productList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_productList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_productList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_productList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_productList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIndex,
            this.ColumnProductCode,
            this.ColumnGoodsName,
            this.ColumnRetailSpecification,
            this.ColumnAmount,
            this.ColumnNormalPrice,
            this.ColumnMoney,
            this.ColumnRemark,
            this.ProductID});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView_productList, 4);
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.MenuText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_productList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_productList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_productList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView_productList.EnableHeadersVisualStyles = false;
            this.dataGridView_productList.GridColor = System.Drawing.SystemColors.WindowText;
            this.dataGridView_productList.Location = new System.Drawing.Point(1, 104);
            this.dataGridView_productList.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_productList.MultiSelect = false;
            this.dataGridView_productList.Name = "dataGridView_productList";
            this.dataGridView_productList.ReadOnly = true;
            this.dataGridView_productList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_productList.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_productList.RowHeadersVisible = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.dataGridView_productList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_productList.RowTemplate.Height = 23;
            this.dataGridView_productList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_productList.Size = new System.Drawing.Size(582, 257);
            this.dataGridView_productList.TabIndex = 2;
            // 
            // ColumnIndex
            // 
            this.ColumnIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnIndex.FillWeight = 10F;
            this.ColumnIndex.HeaderText = "序";
            this.ColumnIndex.Name = "ColumnIndex";
            this.ColumnIndex.ReadOnly = true;
            this.ColumnIndex.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnIndex.Width = 30;
            // 
            // ColumnProductCode
            // 
            this.ColumnProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnProductCode.FillWeight = 15F;
            this.ColumnProductCode.HeaderText = "商品货号";
            this.ColumnProductCode.Name = "ColumnProductCode";
            this.ColumnProductCode.ReadOnly = true;
            // 
            // ColumnGoodsName
            // 
            this.ColumnGoodsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnGoodsName.FillWeight = 25F;
            this.ColumnGoodsName.HeaderText = "名称";
            this.ColumnGoodsName.Name = "ColumnGoodsName";
            this.ColumnGoodsName.ReadOnly = true;
            // 
            // ColumnRetailSpecification
            // 
            this.ColumnRetailSpecification.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnRetailSpecification.FillWeight = 8F;
            this.ColumnRetailSpecification.HeaderText = "单位";
            this.ColumnRetailSpecification.Name = "ColumnRetailSpecification";
            this.ColumnRetailSpecification.ReadOnly = true;
            // 
            // ColumnAmount
            // 
            this.ColumnAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnAmount.FillWeight = 8F;
            this.ColumnAmount.HeaderText = "数量";
            this.ColumnAmount.Name = "ColumnAmount";
            this.ColumnAmount.ReadOnly = true;
            // 
            // ColumnNormalPrice
            // 
            this.ColumnNormalPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnNormalPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnNormalPrice.FillWeight = 10F;
            this.ColumnNormalPrice.HeaderText = "单价";
            this.ColumnNormalPrice.Name = "ColumnNormalPrice";
            this.ColumnNormalPrice.ReadOnly = true;
            // 
            // ColumnMoney
            // 
            this.ColumnMoney.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnMoney.FillWeight = 10F;
            this.ColumnMoney.HeaderText = "金额";
            this.ColumnMoney.Name = "ColumnMoney";
            this.ColumnMoney.ReadOnly = true;
            // 
            // ColumnRemark
            // 
            this.ColumnRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnRemark.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnRemark.FillWeight = 15F;
            this.ColumnRemark.HeaderText = "备注";
            this.ColumnRemark.Name = "ColumnRemark";
            this.ColumnRemark.ReadOnly = true;
            this.ColumnRemark.Visible = false;
            // 
            // ProductID
            // 
            this.ProductID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProductID.HeaderText = "商品ID";
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Visible = false;
            // 
            // label_searisenumber
            // 
            this.label_searisenumber.AutoSize = true;
            this.label_searisenumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_searisenumber.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_searisenumber.Location = new System.Drawing.Point(4, 1);
            this.label_searisenumber.Name = "label_searisenumber";
            this.label_searisenumber.Size = new System.Drawing.Size(576, 37);
            this.label_searisenumber.TabIndex = 1;
            this.label_searisenumber.Text = "交易明细账(流水号)";
            this.label_searisenumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.Controls.Add(this.button2, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label22, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_stockOutTime, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label26, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_orderFee, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_member_account, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_discount_amount, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.label16, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_discount_rate, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_state, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.button1, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 39);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.79365F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.20635F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(582, 64);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(1, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "状态";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Location = new System.Drawing.Point(1, 1);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 30);
            this.label22.TabIndex = 5;
            this.label22.Text = "交易时间";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_stockOutTime
            // 
            this.label_stockOutTime.AutoSize = true;
            this.label_stockOutTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_stockOutTime.Location = new System.Drawing.Point(58, 1);
            this.label_stockOutTime.Margin = new System.Windows.Forms.Padding(0);
            this.label_stockOutTime.Name = "label_stockOutTime";
            this.label_stockOutTime.Size = new System.Drawing.Size(141, 30);
            this.label_stockOutTime.TabIndex = 6;
            this.label_stockOutTime.Text = "xxxxxx";
            this.label_stockOutTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(200, 1);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 30);
            this.label26.TabIndex = 7;
            this.label26.Text = "订单金额";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_orderFee
            // 
            this.label_orderFee.AutoSize = true;
            this.label_orderFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_orderFee.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_orderFee.Location = new System.Drawing.Point(266, 1);
            this.label_orderFee.Margin = new System.Windows.Forms.Padding(0);
            this.label_orderFee.Name = "label_orderFee";
            this.label_orderFee.Size = new System.Drawing.Size(62, 30);
            this.label_orderFee.TabIndex = 8;
            this.label_orderFee.Text = "0.00";
            this.label_orderFee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(329, 1);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 30);
            this.label8.TabIndex = 9;
            this.label8.Text = "会员卡";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_member_account
            // 
            this.label_member_account.AutoSize = true;
            this.label_member_account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_member_account.Location = new System.Drawing.Point(401, 1);
            this.label_member_account.Margin = new System.Windows.Forms.Padding(0);
            this.label_member_account.Name = "label_member_account";
            this.label_member_account.Size = new System.Drawing.Size(103, 30);
            this.label_member_account.TabIndex = 10;
            this.label_member_account.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_discount_amount
            // 
            this.label_discount_amount.AutoSize = true;
            this.label_discount_amount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_discount_amount.Location = new System.Drawing.Point(401, 32);
            this.label_discount_amount.Margin = new System.Windows.Forms.Padding(0);
            this.label_discount_amount.Name = "label_discount_amount";
            this.label_discount_amount.Size = new System.Drawing.Size(103, 31);
            this.label_discount_amount.TabIndex = 11;
            this.label_discount_amount.Text = "0";
            this.label_discount_amount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(329, 32);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 31);
            this.label16.TabIndex = 12;
            this.label16.Text = "折扣额";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(200, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 31);
            this.label5.TabIndex = 13;
            this.label5.Text = "折扣率";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_discount_rate
            // 
            this.label_discount_rate.AutoSize = true;
            this.label_discount_rate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_discount_rate.Location = new System.Drawing.Point(266, 32);
            this.label_discount_rate.Margin = new System.Windows.Forms.Padding(0);
            this.label_discount_rate.Name = "label_discount_rate";
            this.label_discount_rate.Size = new System.Drawing.Size(62, 31);
            this.label_discount_rate.TabIndex = 14;
            this.label_discount_rate.Text = "100";
            this.label_discount_rate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_state
            // 
            this.label_state.AutoSize = true;
            this.label_state.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_state.Location = new System.Drawing.Point(58, 32);
            this.label_state.Margin = new System.Windows.Forms.Padding(0);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(141, 31);
            this.label_state.TabIndex = 15;
            this.label_state.Text = "xxxxxx";
            this.label_state.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(508, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(508, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "返回";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HistoryDetailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoryDetailWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "明细";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HitoryDetailWindow_FormClosing);
            this.Load += new System.EventHandler(this.HitoryDetailWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_productList)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_searisenumber;
        private System.Windows.Forms.DataGridView dataGridView_productList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnGoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRetailSpecification;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNormalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label_stockOutTime;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label_orderFee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_member_account;
        private System.Windows.Forms.Label label_discount_amount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_discount_rate;
        private System.Windows.Forms.Label label_state;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}