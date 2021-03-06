﻿using CashRegisterApplication.comm;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashRegisterApplication.model
{
    public class MemberHttpRespone
    {
        public int errorCode { get; set; }
        public string msg { get; set; }
        public Member data;
    }
    public class MemberData
    {
        public List<Member> list;
    }
    public class Member
    {
        public long memberId { get; set; }

        public String memberAccount{ get; set; }

        public Byte memberType{ get; set; }

        public String phone{ get; set; }

        public String name{ get; set; }

        public Byte gender{ get; set; }

        public String birthday{ get; set; }

        public String address{ get; set; }

        public long consumePoint{ get; set; }


        public long point { get; set; }

        public long balance { get; set; }

        public String password{ get; set; }

        public String salt{ get; set; }

        public String invalidTime { get; set; }

        public long storeId{ get; set; }

        public String storeName{ get; set; }

        public Byte status{ get; set; }
        public Byte isDeleted{ get; set; }
        public String createTime { get; set; }
        public String updateTime { get; set; }
        public String goodsStringWithoutMemberPrice { get; set; }

        public int cloudState { get; set; }
        public String reqRechargeJson { get; set; }

    }
    public class HttpBaseResponeDbPayment
    {
        public int errorCode { get; set; }
        public string msg { get; set; }
        public long data;
    }


}
