﻿using CashRegisterApplication.model;
using CashRegisterApplication.window;
using CashRegisterApplication.window.member;
using CashRegisterApplication.window.Member;
using CashRegiterApplication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CashRegisterApplication.comm
{
    
    public static class MsgContral
    {
        
        public static bool initFlag=false;
        public static ProductListWindow Window_ProductList;//全局窗口
        public static RecieveMoneyWindow Window_RecieveMoney;//收款窗口
        public static ReceiveMoneyByCashWindow Window_ReceiveMoneyByCash;//现金收款窗口
        public static RecieveMoneyByWeixinWindow Window_RecieveMoneyByWeixin ;//微信收款窗口
        public static ReceiveMoneyByMember Window_ReceiveMoneyByMember;//会员收款窗口



        public static RechargeMoneyForMember Window_RechargeMoneyForMember;//充值会员窗口


        public static MemberInfoWindows Window_MemberInfoWindows;//输入会员弹窗



        public static StockOutDTO oStockOutDTO ;//当前单据信息
        public static StockOutDTORespone oStockOutDToRespond;
        public static HttpBaseRespone oHttpRespone;

        public static LocalSaveStock oLocalSaveStock;//挂单信息



        public static Member oMember ;//用户账户

        public static PayWay oPayWay ;//商品列表

        public const int POST_ID = 12345;

        public const int PAY_STATE_INIT = 0;
        public const  int PAY_STATE_SUCCESS = 1;
        public const int PAY_TYPE_CASH = 1;

     

        public const int STOCK_BASE_STATUS_INIT = 0;
        public const int STOCK_BASE_STATUS_NORMAL = 1;
        public const int STOCK_BASE_STATUS_OUT = 2;



        public const int CLOUD_SATE_PAY_SUCESS = 0;
        public const int CLOUD_SATE_PAY_FAILD = 0;



        public const int STOCK_BASE_DB_GENERATE_INIT = 0;
        public const int STOCK_BASE_DB_GENERATE_DONE= 1;//已经存在DB


        public const int CLOUD_SATE_PAY_GENERATE_INIT = 0;
        public const int CLOUD_SATE_PAY_GENERATE_SUCCESS = 1;
        public const int CLOUD_SATE_PAY_GENERATE_FAILED = 2;

        public const int CLOUD_SATE_PAY_UPDATE_SUCCESS = 3;


        public static int PRODUCTlIST_WINDOW = 0;//商品列表页
        public static int MEMBER_RECHAREGE_WINDOWS = 1;//支付页面
        public static int MEMBER_RECIEVE_MONEY_WINDOWS = 2;//会员收款页面


        public static int flagCallShowMember = MEMBER_RECIEVE_MONEY_WINDOWS;

        public static int flagCallShowRecharge = PRODUCTlIST_WINDOW;

        public static StoreWhouse oStoreWhouse;
        public static int store_house_selete_flag;
        public static List<StoreWhouse> oListStoreWhouse;
        public static int store_whouse_id=0;
        public const int CLOUD_SATE_PAY_UPDATE_FAILED = 4;

        public const int STORE_HOUSE_UNSET_SELETED = 0;
        public const int STORE_HOUSE_SELETED = 1;
        public static void Init()
        {
            if (initFlag)
            {
                return;
            }

            //Window_ProductList = new ProductListWindow();//全局窗口
            Window_RecieveMoney = new RecieveMoneyWindow();//收款窗口
            Window_ReceiveMoneyByCash = new ReceiveMoneyByCashWindow();//现金收款窗口
            Window_RecieveMoneyByWeixin = new RecieveMoneyByWeixinWindow();//微信收款窗口
            Window_ReceiveMoneyByMember = new ReceiveMoneyByMember();
            Window_RechargeMoneyForMember = new RechargeMoneyForMember();

            Window_MemberInfoWindows = new MemberInfoWindows();

            oStockOutDTO = new StockOutDTO();//商品列表
            oStockOutDToRespond = new StockOutDTORespone();
            oHttpRespone = new HttpBaseRespone();
            oMember = new Member();
            oPayWay = new PayWay();
            oStoreWhouse = new StoreWhouse();
            store_house_selete_flag = STORE_HOUSE_UNSET_SELETED;
            oListStoreWhouse = new List<StoreWhouse>();

            initFlag = true;

            oLocalSaveStock = new LocalSaveStock();

            Dao.ConnecSql();
            _GetSaveStock();
        }


        public static void _GetSaveStock()
        {
            //查出挂单的单据
            StockOutDTO oState = new StockOutDTO();
            oState.Base.localSaveFlag = Dao.STOCK_BASE_SAVE_FLAG_SAVING;
            List<StockOutDTO> oJsonList = new List<StockOutDTO>();
            Dao.GetCloudStateFailedStockOutList(oState, ref oJsonList);
            CommUiltl.Log("GetCloudStateFailedStockOutList：" + oJsonList.Count);
            if (0 == oJsonList.Count)
            {
                return;
            }
            foreach (var item in oJsonList)
            {
                try
                {
                    StockOutDTO oTmp = JsonConvert.DeserializeObject<StockOutDTO>(item.Base.baseDataJson);
                    oLocalSaveStock.listStock.Add(oTmp);
                }
                catch (Exception e)
                {
                    CommUiltl.Log("DeserializeObject content error ,and coanot parse:" + e + " conten:" + item.Base.baseDataJson);
                    continue;
                }
            }
        }

        /******************门店信息******************/
        internal static void GetStoreMsg()
        {
            if (HttpUtility.GetStoreMsg(ref MsgContral.oListStoreWhouse))
            {

            }
            string strLoacalJson="";
            if (Dao.GetStoreWhouseDefault(ref strLoacalJson ))
            {
                if (strLoacalJson != "" )
                {
                    
                    store_house_selete_flag = STORE_HOUSE_SELETED;
                    MsgContral.oStoreWhouse = JsonConvert.DeserializeObject<StoreWhouse>(strLoacalJson);
                }
            }
            return ;
        }
        /******************登陆**********************/
        internal static bool Login(string userName, string password)
        {
  
            //登陆之前，先去取出mac地址
            string strMac = CommUiltl.GetMacInfo();
            //if (HttpUtility.)
            //{
            //    //mac地址获取信息 根据mac地址拿到post机id

            //}
            CommUiltl.Log("mac*****:" + strMac);
            if (!HttpUtility.Login(userName, password))
            {
                return false;
            }
            //登陆成功后，更新默认门店信息
            string strStoreWhouseDefult= JsonConvert.SerializeObject(MsgContral.oStoreWhouse);

            if (store_house_selete_flag == STORE_HOUSE_SELETED)
            {
                Dao.UpdateStoreWhouseDefault(strStoreWhouseDefult);
                return true;
            }
            Dao.InsertStoreWhouseDefault(strStoreWhouseDefult);
            return true;
        }

        //****************************会员收款和充值
        //显示会员收款
        internal static void Show_MemberInfoWindow_By_RecieveMoeneyByMember()
        {
            flagCallShowMember = MEMBER_RECIEVE_MONEY_WINDOWS;
            MsgContral.Window_MemberInfoWindows.ShowWhithMember();
        }
        internal static void Show_MemberInfoWindow_By_RechargeMoeneyByMember()
        {
            flagCallShowMember = MEMBER_RECHAREGE_WINDOWS;
            MsgContral.Window_MemberInfoWindows.ShowWhithMember();
        }


        //当获取会员信息成功后进行显示页面
        internal static void ShowWindowWhenGetMemberSuccess()
        {
            if (flagCallShowMember == MEMBER_RECIEVE_MONEY_WINDOWS)
            {
                MsgContral.Window_ReceiveMoneyByMember.ShowWithMemberInfo();
                return;
            }
            if (flagCallShowMember == MEMBER_RECHAREGE_WINDOWS)
            {
                MsgContral.Window_RechargeMoneyForMember.ShowWithMemberInfo();
                return;
            }
        }
        //更新会员价
        internal static void UpdateStockOrderByMemberInfo()
        {
            //StockOutDTO
            _CaculateMemberPrice();
            MsgContral.GetGoodsStringWithoutMemberPrice();
            return;
        }
        internal static void _CaculateMemberPrice()
        {
            long totalPrice = 0;
            for (var i=0;i< MsgContral.oStockOutDTO.details.Count;++i)
            {
                //设置会员价
                if (MsgContral.oStockOutDTO.details[i].unitPrice == MsgContral.oStockOutDTO.details[i].cloudProductPricing.retailPrice)
                {
                    MsgContral.oStockOutDTO.details[i].unitPrice = MsgContral.oStockOutDTO.details[i].cloudProductPricing.memberPrice;
                }
                totalPrice += MsgContral.oStockOutDTO.details[i].unitPrice;
            }
            //
            MsgContral.oStockOutDTO.Base.orderAmount = totalPrice;
            MsgContral.Window_ProductList.SetProductListWindowByStockOut(MsgContral.oStockOutDTO);
            //更新数据库里面订单信息
            if(!Dao.updateRetailStock(MsgContral.oStockOutDTO))
            {
                return;
            }
        }
        internal static void GetGoodsStringWithoutMemberPrice()
        {
            string strTmp = "";
            for (var i = 0; i < MsgContral.oStockOutDTO.details.Count; ++i)
            {
                //设置会员价
                if (MsgContral.oStockOutDTO.details[i].unitPrice != MsgContral.oStockOutDTO.details[i].cloudProductPricing.retailPrice)
                {
                    //strTmp +="id:"+Main.oStockOutDTO.details[i].goodsId+" ";
                    strTmp += MsgContral.oStockOutDTO.details[i].goodsName;
                    strTmp += " 会员价:" + CommUiltl.CoverMoneyUnionToStrYuan(MsgContral.oStockOutDTO.details[i].cloudProductPricing.memberPrice);
                    strTmp += " 现价:" + CommUiltl.CoverMoneyUnionToStrYuan(MsgContral.oStockOutDTO.details[i].unitPrice);
                    strTmp += "\n";
                }
               
            }
            MsgContral.oMember.goodsStringWithoutMemberPrice = strTmp;

        }
        //***************************充值相关

        internal static void ShowWindows_RechargeMoneyForMember()
        {
            flagCallShowRecharge = PRODUCTlIST_WINDOW;
            Window_RechargeMoneyForMember.ShowByProductListWindow();
        }

        //充值后返回
        internal static void ControlWindowsAfterRecharge()
        {
            if (flagCallShowRecharge == PRODUCTlIST_WINDOW)
            {
                MsgContral.Window_ProductList.Show();
            }
            if (flagCallShowRecharge == MEMBER_RECIEVE_MONEY_WINDOWS)
            {
                MsgContral.Window_ProductList.Show();
            }
        }

        //当会员取消界面
        internal static void ShowWindowWhenMemberInfoCancel()
        {
           
            if (flagCallShowMember == MEMBER_RECIEVE_MONEY_WINDOWS)
            {
                MsgContral.Window_RecieveMoney.Show();
                return;
            }
            if (flagCallShowMember == MEMBER_RECHAREGE_WINDOWS)
            {
                MsgContral.Window_ProductList.Show();
                return;
            }
        }




        public static void Clean()
        {
            oStockOutDTO = new StockOutDTO();//商品列表
            oStockOutDToRespond = new StockOutDTORespone();
        }


        public static void ControlWindowsAfterPay()
        {
            CommUiltl.Log("ControlWindowsAfterPay" );
            if (MsgContral.oStockOutDTO.Base.RecieveFee < MsgContral.oStockOutDTO.Base.orderAmount)
            {
                CommUiltl.Log("Window_RecieveMoney Show");
                Window_RecieveMoney.ShowPaidMsg();
                return;
            }
            //Order.RecieveFee >= Order.orderAmount 说明已经收钱完毕
            if (!CloseOrderWhenPayAllFee())
            {
                return ;
            }
            Window_ProductList.CloseOrderByControlWindow();
        }
        //***********************************关闭订单***************************
        internal static bool CloseOrderWhenPayAllFee()
        {
            MsgContral.oStockOutDTO.Base.status = STOCK_BASE_STATUS_OUT;
            SetSaveFlag();//挂单->关单
            MsgContral.oStockOutDTO.Base.cloudCloseFlag = HttpUtility.CloseOrderWhenPayAllFee(MsgContral.oStockOutDTO, ref MsgContral.oHttpRespone);
            if (!Dao.UpdateOrderCloudState(MsgContral.oStockOutDTO))
            {
                return false;
            }
            return true;
        }

        internal static void SetSaveFlag()
        {
            if (MsgContral.oStockOutDTO.Base.localSaveFlag == Dao.STOCK_BASE_SAVE_FLAG_SAVING)
            {
                MsgContral.oStockOutDTO.Base.localSaveFlag = Dao.STOCK_BASE_SAVE_FLAG_CLOSE;
            }
        }
        //***********************************生成订单***************************
        internal static bool IsCurrentOrderInit()
        {
            return MsgContral.oStockOutDTO.Base.dbGenerateFlag == MsgContral.STOCK_BASE_DB_GENERATE_INIT;
        }
                                                                                                                                        
        internal static bool GenerateOrder(string strProductList)
        {
            if (MsgContral.oStockOutDTO.details.Count == 0)
            {
                CommUiltl.Log("Main.oStockOutDTO.details.Count == 0]");
                return true;
            }
            if (IsCurrentOrderInit())
            {
                MsgContral.oStockOutDTO.Base.ProductList = strProductList;
                CommUiltl.Log("Order.OrderCode ==  empty GenerateOrder ");
                MsgContral.oStockOutDTO.Base.generateSeariseNumber();

                MsgContral.oStockOutDTO.Base.cloudAddFlag = HttpUtility.GenerateOrder(MsgContral.oStockOutDTO, ref MsgContral.oStockOutDToRespond);
                MsgContral.oStockOutDTO.Base.cloudAddFlag = HttpUtility.CLOUD_SATE_HTTP_FAILD;

                if (MsgContral.oStockOutDTO.Base.cloudAddFlag == HttpUtility.CLOUD_SATE_HTTP_SUCESS )
                {
                    MsgContral.oStockOutDTO.Base.stockOutId = MsgContral.oStockOutDToRespond.data.Base.stockOutId;
                    SetStockDetailByHttpRespone(oStockOutDToRespond.data,ref MsgContral.oStockOutDTO );
                }
                MsgContral.oStockOutDTO.Base.baseDataJson = JsonConvert.SerializeObject(MsgContral.oStockOutDTO);
                MsgContral.oStockOutDTO.Base.dbGenerateFlag = MsgContral.STOCK_BASE_DB_GENERATE_DONE;//新增
                //插入本地数据库表
                if (!Dao.GenerateOrder(MsgContral.oStockOutDTO))
                {
                    return false;
                }
                return true;
            }
            //更新订单
            if (strProductList != null && 0 != MsgContral.oStockOutDTO.Base.ProductList.CompareTo(strProductList))
            {
                CommUiltl.Log(" strProductList is modify [" + MsgContral.oStockOutDTO.Base.ProductList + "] -> [" + strProductList + "]");
                MsgContral.oStockOutDTO.Base.ProductList = strProductList;
                MsgContral.oStockOutDTO.Base.cloudUpdateFlag = HttpUtility.updateRetailStock(MsgContral.oStockOutDTO, ref MsgContral.oStockOutDToRespond);

                if (MsgContral.oStockOutDTO.Base.cloudAddFlag == HttpUtility.CLOUD_SATE_HTTP_SUCESS)
                {
                    
                }else
                {
                    MsgContral.oStockOutDTO.Base.baseDataJson = JsonConvert.SerializeObject(MsgContral.oStockOutDTO);
                }

                if (!Dao.updateRetailStock(MsgContral.oStockOutDTO))
                {
                    return false;
                }

                return true;
            }
            CommUiltl.Log(" not modify strProductList:"+ strProductList);
            return true;
        }

        //************************挂单***********************
        internal static bool SaveStock(string strProductList)
        {
            //生成订单，状态为挂单
            MsgContral.oStockOutDTO.Base.ProductList = strProductList;
            MsgContral.oStockOutDTO.Base.localSaveFlag = Dao.STOCK_BASE_SAVE_FLAG_SAVING;
            if (!MsgContral.GenerateOrder(strProductList))
            {
                return false;
            }
            addStockToLocal(MsgContral.oStockOutDTO);
            return true;
        }
        internal static void addStockToLocal(StockOutDTO oStockOutDTO)
        {
            CommUiltl.Log("addStockToLocal Main.oSaveSotckOut.listStock.Count:" + MsgContral.oLocalSaveStock.listStock.Count);
            for (int i=0;i< MsgContral.oLocalSaveStock.listStock.Count;++i)
            {
                if (oStockOutDTO.Base.serialNumber == MsgContral.oLocalSaveStock.listStock[i].Base.serialNumber)
                {
                    CommUiltl.Log("addStockToLocal found" );
                    MsgContral.oLocalSaveStock.listStock[i] = oStockOutDTO;//如果是已经存在挂单中的订单，那么就替换下
                    return;
                }
            }
            MsgContral.oLocalSaveStock.listStock.Add(oStockOutDTO);
        }
        public static int CurrentStockIndex = -1;



        internal static void GetSaveOrderToCurrentMsg()
        {
            if (MsgContral.oLocalSaveStock.listStock.Count == 0)
            {
                return;
            }
            ++CurrentStockIndex;
            CurrentStockIndex = CurrentStockIndex % MsgContral.oLocalSaveStock.listStock.Count;
            MsgContral.oStockOutDTO = MsgContral.oLocalSaveStock.listStock[CurrentStockIndex];
        }

        internal static void SetStockDetailByHttpRespone(StockOutDTO http,ref StockOutDTO Db)
        {
            if (oStockOutDToRespond.data.details.Count != MsgContral.oStockOutDTO.details.Count)
            {
                //说明是有问题的
                CommUiltl.Log("oRespond.data.details.Count[" + oStockOutDToRespond.data.details.Count + "] != Main.oStockOutDTO.details.Count [" + MsgContral.oStockOutDTO.details.Count + "]");
                MessageBox.Show("下单异常，请联系后台同学检查下单返回[" + oStockOutDToRespond.data.details.Count + "] != Main.oStockOutDTO.details.Count [" + MsgContral.oStockOutDTO.details.Count + "]");
            }
            else
            {
                for (int i = 0; i < oStockOutDToRespond.data.details.Count; ++i)
                {
                    MsgContral.oStockOutDTO.details[i].id = oStockOutDToRespond.data.details[i].id;
                }
            }
        }
        internal static bool PayOrderByCash(long recieveFee)
        {
            MsgContral.oPayWay.payType = PAY_TYPE_CASH;
            MsgContral.oPayWay.payAmount = recieveFee;
            MsgContral.oPayWay.generatePayOrderNumber();
            MsgContral.oPayWay.serialNumber = MsgContral.oStockOutDTO.Base.serialNumber;
            MsgContral.oPayWay.payStatus=  MsgContral.PAY_STATE_SUCCESS;
            MsgContral.oPayWay.cloudState = MsgContral.CLOUD_SATE_PAY_SUCESS ;
            PayWayHttpRequet oPayWayHttpRequet = new PayWayHttpRequet();
            oPayWayHttpRequet.memberId = MsgContral.oMember.memberId;
            oPayWayHttpRequet.list.Add(MsgContral.oPayWay);
            MsgContral.oPayWay.cloudState = HttpUtility.PayOrdr(oPayWayHttpRequet);
            if (!Dao.GeneratePay(MsgContral.oPayWay))
            {
                return false;
            }
            //修改环境变量，表示这笔单支付成功
            PayWay oPayWay = new PayWay();
            MsgContral.oStockOutDTO.addPayWay(MsgContral.oPayWay);
            CommUiltl.Log("PayOrderByCash end:" + recieveFee);
            MessageBox.Show("支付" + CommUiltl.CoverMoneyUnionToStrYuan(recieveFee) + "元现金成功");
            return true;
        }
        internal static bool PayOrderByMember(long recieveFee)
        {
            MsgContral.oPayWay.payType = PAY_TYPE_CASH;
            MsgContral.oPayWay.payAmount = recieveFee;
            MsgContral.oPayWay.generatePayOrderNumber();
            MsgContral.oPayWay.serialNumber = MsgContral.oStockOutDTO.Base.serialNumber;
            MsgContral.oPayWay.payStatus = MsgContral.PAY_STATE_SUCCESS;
            MsgContral.oPayWay.cloudState = MsgContral.CLOUD_SATE_PAY_SUCESS;
            PayWayHttpRequet oPayWayHttpRequet = new PayWayHttpRequet();
            oPayWayHttpRequet.memberId = MsgContral.oMember.memberId;
            oPayWayHttpRequet.list.Add(MsgContral.oPayWay);
            MsgContral.oPayWay.cloudState = HttpUtility.PayOrdr(oPayWayHttpRequet);
            if (!Dao.GeneratePay(MsgContral.oPayWay))
            {
                return false;
            }
            //修改环境变量，表示这笔单支付成功
            PayWay oPayWay = new PayWay();
            MsgContral.oStockOutDTO.addPayWay(MsgContral.oPayWay);
            CommUiltl.Log("PayOrderByCash end:" + recieveFee);
            MessageBox.Show("支付" + CommUiltl.CoverMoneyUnionToStrYuan(recieveFee) + "元现金成功");
            return true;
        }
        //充值
        internal static bool RechargeMoneyByMember(long recieveFee)
        {
            //充值金
            Member oRechargeMember = new Member();
            oRechargeMember.memberId= MsgContral.oMember.memberId;
            oRechargeMember.memberBalance = recieveFee;
            oRechargeMember.name = MsgContral.oMember.name;
            oRechargeMember.memberAccount=MsgContral.oMember.memberAccount;
            oRechargeMember.reqJson = JsonConvert.SerializeObject(oRechargeMember);
            //请求后台充值
            oRechargeMember.cloudState = HttpUtility.memberRecharge(oRechargeMember);

            long beforeMberBalance = MsgContral.oMember.memberBalance;
            //重新拉会员信息
            MsgContral.GetMemberByMemberAccount(MsgContral.oMember.memberAccount);
            long afterMemberAccount = MsgContral.oMember.memberBalance;
            //记录流水
            Dao.memberRecharge(oRechargeMember, beforeMberBalance, afterMemberAccount, recieveFee);

            if (oRechargeMember.cloudState != HttpUtility.CLOUD_SATE_HTTP_SUCESS)
            {
                return false;
            }
            return true;
        }
        
        //************************会员信息***********************
        internal static bool GetMemberByMemberAccount(string strMemberAccount)
        {
            MemberHttpRespone oMember = new MemberHttpRespone();
            int iMemberRet=HttpUtility.GetMemberByMemberAccount(strMemberAccount,ref oMember);
            if (iMemberRet == HttpUtility.CLOUD_SATE_HTTP_SUCESS)
            {
                MsgContral.oMember = oMember.data.list[0];
                return true;
            }
            if (iMemberRet == HttpUtility.CLOUD_SATE_HTTP_FAILD)
            {
                MessageBox.Show(HttpUtility.lastErrorMsg);
                return false;
            }
            MessageBox.Show("业务错误："+HttpUtility.lastErrorMsg);
            return false;
        }

    }

}