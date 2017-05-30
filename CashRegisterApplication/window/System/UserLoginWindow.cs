﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CashRegiterApplication;
using Newtonsoft.Json;
using System.Net;
using CashRegisterApplication.comm;
using CashRegisterApplication.model;

namespace CashRegiterApplication
{


    public partial class UserLoginWindow : Form
    {

        public ProductListWindow gProductListWindow;
        public UserLoginWindow()
        {
            InitializeComponent();
            gProductListWindow = new ProductListWindow();
            this.textBox_userName.Text = "york";
            this.textBox_password.Text = "york";
           // HttpUtility.TestTimeOut();
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            CenterContral.InitWindows();
            if (0 != CenterContral.oStoreWhouse.storeWhouseId)
            {
                textBox_Shop.Text = CenterContral.oStoreWhouse.name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

     
            //登陆

            if (CenterContral.Login(this.textBox_userName.Text, this.textBox_password.Text,CenterContral.oStoreWhouse.storeWhouseId))
            {
                gProductListWindow.Show();
                this.Hide();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
