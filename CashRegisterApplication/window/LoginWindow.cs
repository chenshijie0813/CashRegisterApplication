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

namespace CashRegiterApplication
{


    public partial class LoginWindows : Form
    {

        public ProductListWindow gProductListWindow;
        public LoginWindows()
        {
            InitializeComponent();
            gProductListWindow = new ProductListWindow();
            this.textBox_userName.Text = "york";
            this.textBox_password.Text = "york";
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (HttpUtility.Login(this.textBox_userName.Text, this.textBox_password.Text))
            {
                this.Hide();
                gProductListWindow.Show();
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