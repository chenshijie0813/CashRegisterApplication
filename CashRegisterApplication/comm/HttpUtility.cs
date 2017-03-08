﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using CashRegisterApplication.model;
using System.Windows.Forms;
using System.Security.Cryptography;
using CashRegisterApplication.comm;
/*    
* 作者：周公(zhoufoxcn)    
* 日期：2011-05-08    
* 原文出处：http://blog.csdn.net/zhoufoxcn 或http://zhoufoxcn.blog.51cto.com    
* 版权说明：本文可以在保留原文出处的情况下使用于非商业用途，周公对此不作任何担保或承诺。    
* */
namespace CashRegiterApplication
{
    /// <summary>  
    /// 有关HTTP请求的辅助类  
    /// </summary>  
    public class HttpUtility
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private static readonly string CashRegistHost = "http://aladdin.chalubo.com/cashRegister/";
        private static CookieContainer gCookies = null;//全局登录态cookie
        private static readonly string DefaultUser = "york";
        private static readonly string DefaultPassword = "york";
        private static int timeOutDefault = 10000;//10秒超时
        private static string gUserName;
        private static string gPassword;
        private static readonly string LoginFunc = "login.json?";
        private static readonly string ProductCodeFunc = "getPricingByProductCode.json?productCode=";
        private static UserLogin oLoginer;
        public static bool Login(string user, string password)
        {
            gUserName = user;
            gPassword = password;
            gCookies = null;
            oLoginer = new UserLogin();

            string loginUrl = LoginFunc + "username=" + gUserName + "&password=" + CommUiltl.HEX_MD5(gPassword);
            Console.WriteLine("Debug loginUrl:" + loginUrl);
            //if ( !Get<UserLogin>("login.json?username=york&password=c3b29ce20ce560efdc6f6714612a1156", ref oLoginer))
            if (!Get<UserLogin>(loginUrl, ref oLoginer))
            {
                Console.WriteLine("ERR:Get failed");
                MessageBox.Show("登录异常:请检查网络");
                //登录异常
                if (DefaultUser == user && DefaultPassword == password)//
                {
                    MessageBox.Show("使用临时用户登录");
                    return true;
                }
                return false;
            }
            if (0 != oLoginer.errorCode)
            {
                Console.WriteLine("ERR:Get OK but errorCode:" + oLoginer.errorCode);
                MessageBox.Show("帐号密码不对");
                return false;
            }
            Console.WriteLine("ERR:Get OK oLoginer errorCode: " + oLoginer.errorCode);
            return true;
        }
        private static User GetLoginUser()
        {
            return oLoginer.data;
        }



        /// <summary>
        /// 当网络异常的时候，会返回错误
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public static bool GetProductByProductCode(string productCode,ref ProductPricingInfoResp oProductPricingInfoResp)
        {
            string funcUrl = ProductCodeFunc + productCode;
            if (!Get<ProductPricingInfoResp>(funcUrl, ref oProductPricingInfoResp))
            {
                Console.WriteLine("ERR:Get failed");
                MessageBox.Show("登录异常:请检查网络");
                return false;
            }
            return true;
        }
        public static bool Get<T>(string funcUrl, ref T returnObj)
        {
            HttpWebRequest request = WebRequest.Create(CashRegistHost + funcUrl) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            request.Timeout = timeOutDefault;
            request.CookieContainer = gCookies;
            if (gCookies == null)
            {
                request.CookieContainer = new CookieContainer();
            }

            if (!HttpResp<T>(request, ref returnObj))
            {
                Console.WriteLine("ERR:HttpResp failed");
                return false;
            }
            return true;
        }
        public static bool HttpResp<T>(HttpWebRequest request, ref T returnObj)
        {
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine("网络异常");
                HandelWEbException(e);
                return false;
            }
            Console.WriteLine("DEBUG:response.StatusCode : {0}", (int)response.StatusCode);
            StreamReader streamReader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
            string content = streamReader.ReadToEnd();
            returnObj = JsonConvert.DeserializeObject<T>(content);
            if (gCookies == null)
            {
                SetCookiet(response.Cookies);
            }
            if (response != null)
            {
                response.Close();
            }
            return true;
        }
        public static void SetCookiet(CookieCollection oCookieCollection)
        {
            gCookies = new CookieContainer();
            for (int i = 0; i < oCookieCollection.Count; ++i)
            {
                gCookies.Add(oCookieCollection[i]);
            }
        }

        public static void HandelWEbException(WebException e)
        {
            HttpWebResponse response = (HttpWebResponse)e.Response;
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                Console.WriteLine("ERR:response.StatusCode : {0}", (int)response.StatusCode);
            }
            else
            {
                Console.WriteLine("ERR:e.Status: {0}", e.Status);
            }
            if (response != null)
            {
                response.Close();
            }
        }


        public static HttpWebResponse Get(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            request.Timeout = timeOutDefault;
            request.CookieContainer = gCookies;
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="gCookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection gCookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (gCookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(gCookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="gCookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection gCookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (gCookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(gCookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }



    }
}