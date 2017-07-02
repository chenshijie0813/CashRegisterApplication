﻿using CashRegisterApplication.comm;
using CashRegisterApplication.model;
using CashRegisterApplication.window;
using CashRegisterApplication.window.function;
using CashRegisterApplication.window.History;
using CashRegisterApplication.window.Printer;
using CashRegisterApplication.window.ProductList;
using CashRegisterApplication.window.Setting;
using CashRegiterApplication;
using SuperMarket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace CashRegiterApplication
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //string str;
            //str = "===============销售===============";
            //CommUiltl.Log(str+" "+ str.Length);
            //str = "名称/条码     单价    数量    金额";
            //CommUiltl.Log(str + " " + str.Length);
            //str = "名称/条码     ";
            //CommUiltl.Log(str + " " + str.Length);
            //str = "名称/条码     单价    数量";
            //CommUiltl.Log(str + " " + str.Length);
            //str = "名称/条码       单价      数量      金额";
            //CommUiltl.Log(str + " " + str.Length);
            //str = "名称/条码     单价     数量      100.32";
            //CommUiltl.Log(str + " " + str.Length);
            //   Application.Run(new FunctionMenuWindow());
           Application.Run(new LoginWindows());
            // Runs the application.
        }


        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}
