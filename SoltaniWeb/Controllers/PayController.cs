using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SoltaniWeb;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SoltaniWeb.Controllers
{
    [getstatisticController]
    public class PayController : Controller
    {
        // GET: Pay
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();

    

        public String PaymentAction(int purchasecartid = 0)
        {
            try
            {

                transAction Model = new transAction();
                Model.factorNumber = purchasecartid.ToString();
                // تعیین مقدار قابل پرداخت

                var cartlist = db.tbl_purchasekartitemlist.Where(a => a.perchasekart_id == purchasecartid);
                decimal totalprice = 0, totalweight = 0, totalcosttransportation = 0, discount = 0;
                string totalpricestr, totalweightstr, totalcosttransportationstr, discountstr;
                foreach (var item in cartlist)
                {
                    totalprice = totalprice + (decimal)(item.number * (item.product_.lastcellcost.HasValue == true ? item.product_.lastcellcost.Value : 0));
                    totalweight = totalweight + (decimal)(item.number * (item.product_.weight.HasValue == true ? item.product_.weight.Value : 0));
                }

               

                    totalpricestr = string.Format("{0:#,##0.##}", totalprice) + " ریال";
                    if (cartlist.FirstOrDefault().perchasekart_.discount_id != null)
                    {
                        discount = Math.Floor(cartlist.FirstOrDefault().perchasekart_.discount_.percentage.Value * totalprice / 100);
                        discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    }
                    else
                    {
                        discount = 0;
                        discountstr = string.Format("{0:#,##0.##}", discount) + " ریال";
                    }
                    if (db.tbl_transportationcost.Where(a => a.cart_id == purchasecartid).Count() > 0)
                    {

                        totalcosttransportation = db.tbl_transportationcost.Where(a => a.cart_id == purchasecartid).Sum(a => a.totaltcost.Value);
                        totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
                    }
                    else
                    {
                        totalcosttransportation = 0;
                        totalcosttransportationstr = string.Format("{0:#,##0.##}", totalcosttransportation) + " ریال";
                    }
                    decimal payableprice = 0;
                    string payablepricestr = "";

                    payableprice = totalprice + totalcosttransportation - discount;
                    payablepricestr = string.Format("{0:#,##0.##}", payableprice) + " ریال";





                    //


                    Model.amount = payableprice.ToString();
                    Model.factorNumber = cartlist.FirstOrDefault().perchasekart_.id.ToString();



                    //

                    Payment ob = new Payment();
                    string result = ob.pay(Model.amount);
                    JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);

                    if (Parmeters.status == 1)
                    {
                        var thiscart = db.tbl_purchasekart.Where(a => a.id == purchasecartid).SingleOrDefault();
                        thiscart.transId = Parmeters.transId;
                        db.SaveChanges();
                        Response.Redirect("https://pay.ir/payment/gateway/" + Parmeters.transId);
                    }
                    else
                    {
                        return "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                    }
                    return "";
                
            }
            catch (Exception exp)
            {
                return "خطا در اتصال به درگاه پرداخت" + exp.Message;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult VerifyPayment(Models.VerifyResult Vresult)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["transId"]))
                {
                    Payment ob = new Payment();
                    string result = ob.verify(Request.Form["transId"].ToString());
                    JsonParameters Parmeters = JsonConvert.DeserializeObject<JsonParameters>(result);
                    string transId = Request.Form["transId"].ToString();
                    var thiscart = db.tbl_purchasekart.Where(a => a.transId == transId).SingleOrDefault();
                    if (Parmeters.status == 1)
                    {
                        thiscart.ispaid = true;
                        thiscart.purchasedateend = DateTime.Now;
                        db.SaveChanges();
                        Vresult.success = true;
                        Vresult.TransActionID += Request.Form["transId"].ToString();
                        Vresult.Amount += Parmeters.amount.ToString();

                        Vresult.SuccessMessage = "پرداخت با موفقیت انجام شد.";

                        Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                        string[] ret1 = new string[2];
                        string smstext = $"مبلغ {Vresult.Amount} ریال به حساب شما بابت خرید اینترنتی سبد شماره {thiscart.id} توسط کاربر {thiscart.user.username} در تاریخ {shamsi.ToShamsi(DateTime.Now).ToString("yyyy/MM/dd")}به شماره سند :  {Vresult.TransActionID} واریز شد.";
                        ret1 = sms_Single.SendSMS_Single(smstext,"09177017801", "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);


                        tbl_transaction t = new tbl_transaction();
                        t.cartid = thiscart.id;
                        t.amount = decimal.Parse( Vresult.Amount);
                        t.sharh = "تسویه حساب کاربری : " + thiscart.user.username;
                        t.transid = Vresult.TransActionID;
                        t.user_id = thiscart.user.id;
                        t.varizdate = DateTime.Now;
                        db.tbl_transaction.Add(t);
                        db.SaveChanges();

                    
                    }
                    else
                    {
                        Vresult.error = true;
                        Vresult.ErrorMessage = "کدخطا : " + Parmeters.errorCode + "<br />" + "پیغام خطا : " + Parmeters.errorMessage;
                    }
                }
            }
            catch (Exception)
            {
                Vresult.error = true;
                Vresult.ErrorMessage = "متاسفانه پرداخت ناموفق بوده است.";
            }

            return View(Vresult);
        }

        public class Payment
        {
            private string GatewaySend = "https://pay.ir/payment/send";
            private string GatewayResult = "https://pay.ir/payment/verify";
            string apiasl = "8970791f695563321db209644af20194";
            private string Api = "8970791f695563321db209644af20194";
            string redirectasl = "https://www.soltaniwoodproducts.com/Pay/VerifyPayment";
            private string Redirect = "https://localhost:44399/Pay/VerifyPayment";

            public string pay(String Amount)
            {
                string result = "";
                String post_string = "";
                Dictionary<string, string> post_values = new Dictionary<string, string>();
                post_values.Add("api", Api);
                post_values.Add("amount", Amount);
                post_values.Add("redirect", Redirect);

                foreach (KeyValuePair<string, string> post_value in post_values)
                {
                    post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&";
                }
                post_string = post_string.TrimEnd('&');
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(GatewaySend);

                objRequest.Method = "POST";
                objRequest.ContentLength = post_string.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";

                StreamWriter myWriter = null;
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(post_string);
                myWriter.Close();
                HttpWebResponse objResponse = (HttpWebResponse)(objRequest.GetResponse());

                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                return result;
            }
            public string verify(String TransID)
            {
                string result = "";
                String post_string = "";
                Dictionary<string, string> post_values = new Dictionary<string, string>();
                post_values.Add("api", Api);
                post_values.Add("transId", TransID);

                foreach (KeyValuePair<string, string> post_value in post_values)
                {
                    post_string += post_value.Key + "=" + HttpUtility.UrlEncode(post_value.Value) + "&";
                }
                post_string = post_string.TrimEnd('&');
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(GatewayResult);
                objRequest.Method = "POST";
                objRequest.ContentLength = post_string.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";

                StreamWriter myWriter = null;
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(post_string);
                myWriter.Close();

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                return result;
            }
        }


        public class JsonParameters
        {
            public int status { get; set; }
            public string transId { get; set; }
            public double amount { get; set; }
            public string errorCode { get; set; }
            public string errorMessage { get; set; }
            public JsonParameters()
            {

            }
        }

        public ActionResult transportationcost()
        {
            return View();
        }
    }
}