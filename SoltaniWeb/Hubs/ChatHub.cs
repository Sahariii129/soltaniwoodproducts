using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.Authorize_swp;
using SoltaniWeb.Models.structs;
using SoltaniWeb.Models.Services.Interfaces;

namespace SoltaniWeb.Hubs
{
    public class ChatHub : Hub
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
       private IPurchaseCart _cart;

        public ChatHub(IPurchaseCart cart)
        {
            _cart = cart;

        }
        public async Task SendMessage2(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task sendmessage(int from_userid, int? to_userid, string message, string messagetextpure)
        {

            string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");
            string status = "";
            string url = "";
            if (message.Contains("www.") || message.Contains(".com") || message.Contains(".org") || message.Contains(".net") || message.Contains(".ir"))
            {
                if (message.Contains("http://") || message.Contains("https://"))
                {
                    url = message;
                    message = "<a href='" + url + "'>" + message + "</a>";
                }
                else
                {
                    url = "//" + message;
                    message = "<a href='" + url + "'>" + message + "</a>";
                }

            }



            var from_user = db.tbl_signalrUsers.Where(a => a.userid == from_userid).SingleOrDefault();
            int branch = from_user.user.branches_id.HasValue ? from_user.user.branches_id.Value : 0;

            if (to_userid == null)
            {
                status = "public";
                tbl_signalRmsg msg = new tbl_signalRmsg { from_userid = from_userid, msg_text = message, msg_textpure = messagetextpure, datetime_send = DateTime.Now, visibleforall = true, visibletofrom = true, visibletoto = true };
                db.tbl_signalRmsg.Add(msg);
                db.SaveChanges();
                await Clients.All.SendAsync("addNewMessage", msg.id, from_userid, from_user.username, message, thisdatetime, status, branch, messagetextpure);

            }
            else
            {
                status = "single";
                var to_user = db.tbl_signalrUsers.Where(a => a.userid == to_userid.Value).SingleOrDefault();
                tbl_signalRmsg msg = new tbl_signalRmsg { from_userid = from_userid, to_userid = to_userid, msg_text = message, msg_textpure = messagetextpure, datetime_send = DateTime.Now, visibleforall = true, visibletofrom = true, visibletoto = true };
                db.tbl_signalRmsg.Add(msg);
                db.SaveChanges();

                if (to_user.connectionId != null)
                {
                    await Clients.Client(to_user.connectionId).SendAsync("addNewMessage", msg.id, from_userid, from_user.username, message, thisdatetime, status, branch, messagetextpure);
                }
                if (to_user.connectionId != from_user.connectionId)
                {
                    await Clients.Client(from_user.connectionId).SendAsync("addNewMessage", msg.id, from_userid, from_user.username, message, thisdatetime, status, branch, messagetextpure);

                }
            }

        }


        public async Task topin(int msgid = 0, int currentuserid = 0, string topic = "پیام مهم")
        {
            var msgpinned = db.tbl_signalRmsg.Where(a => a.pin == true && a.datetime_pin.HasValue == true);
            var currentuser = db.tbl_signalrUsers.Where(a => a.userid == currentuserid).SingleOrDefault();
            if (msgpinned.Count() >= 8)
            {
                await Clients.Client(currentuser.connectionId).SendAsync("errorinpinopperation", "بیش از 8 پیام مهم نمی توان تعریف کرد");
            }
            else
            {
                var msg = db.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();
                if (msg.pin == true)
                {
                    await Clients.Client(currentuser.connectionId).SendAsync("errorinpinopperation", "این پیام قبلاً پین شده است");
                }
                else
                {
                    msg.pin = true;
                    msg.datetime_pin = DateTime.Now;
                    msg.topic_pin = topic;
                    db.SaveChanges();
                    string persianpindate = msg.datetime_pin.Value.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm");
                    await Clients.All.SendAsync("topinmsg", msgid, persianpindate, topic);
                }

            }

        }

        public async Task register(string name, string username, string userid, string connectionid, string message)
        {
            //_4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var user = db.tbl_signalrUsers.Where(a => a.username.ToLower() == username.ToLower()).SingleOrDefault();
            if (user == null)
            {
                tbl_signalrUsers newuser = new tbl_signalrUsers { fullname = name, username = username, connectionId = connectionid, userid = int.Parse(userid) };

                db.tbl_signalrUsers.Add(newuser);

            }
            else
            {
                user.connectionId = connectionid;
                await Clients.Client(user.connectionId).SendAsync("setonline", " is Online");
                //Clients.Client(user.connectionId).setonline(" is Online");
            }
            db.SaveChanges();
            var onlineusers = db.tbl_signalrUsers.Where(x => x.connectionId != null).Select(x => x.userid).ToList();
            await Clients.All.SendAsync("addonlineusers", onlineusers);
            //Clients.All.addonlineusers(onlineusers);
            string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");

            //shamsi.ToShamsi(DateTime.Now).ToString("yyyy/MM/dd  hh:mm:ss");
            //Clients.All.SendSystemMessage(thisdatetime, user.username + " آنلاین شد.");
        }
        public async Task registerindb(int userid, int cartid, string connectionid)
        {

            var user = db.tbl_signalrUsers.Where(a => a.userid == userid).SingleOrDefault();
            if (user == null)
            {
                tbl_signalrUsers newuser = new tbl_signalrUsers { connectionId = connectionid, userid = userid };

                db.tbl_signalrUsers.Add(newuser);

            }
            else
            {
                user.connectionId = connectionid;

            }
            db.SaveChanges();


          

        }
        //public async Task sendpricebyclient(int userid, int cartid, string connectionid, String transportationRequired, string cartDesc,transportationdetailsMV t )
        public async Task sendpricebyclient(int userid, int cartid, string connectionid, String transportationRequired, string cartDesc,string location_name, string person_peygiri, string tell, string location_address)
        {

            var user = db.tbl_signalrUsers.Where(a => a.userid == userid).SingleOrDefault();
            tbl_purchasekart p = db.tbl_purchasekart.Find(cartid);
            // change purchase staus to DemandPrice
            if (cartid != 0)
            {
                p.status = (int)purchasestatus.DemandPrice;
                if (transportationRequired=="true")
                {
                    tbl_transportaiondetails t = new tbl_transportaiondetails()
                    {
                    cartid = cartid,
                    location_name=location_name,
                    person_peygiri = person_peygiri,
                    tell =tell,
                    location_address = location_address,

                    };

                    _cart.settransportaiondetails(cartid, t);
                    p.transportationisneeded = true;
                }
                else
                {
                p.transportationisneeded = false;

                }


                p.pcartDesc = cartDesc;
                db.SaveChanges();
            }

            await Clients.Client(user.connectionId).SendAsync("setDemandPriceStepInClient", p.status.Value);

            // find alireza is online
            var alireza = db.tbl_signalrUsers.Where(a => a.userid == 78).SingleOrDefault();
            if (alireza.connectionId != null && user.userid != 78)
            {



                string msg = $"یک سبد خرید توسط کاربر : {user.fullname} هم اکنون ثبت گردید. ";
                await Clients.Client(alireza.connectionId).SendAsync("showthiscartonline", msg, cartid, userid, p.status.Value);
            }

        }

        public async Task sendpricetoclient(int cartid = 0, int userid = 0)
        {
            var userclient = db.tbl_signalrUsers.Where(a => a.userid == userid).SingleOrDefault();
            var userclientconnectionid = userclient.connectionId;


            // change purchase staus to DemandPrice
            tbl_purchasekart p = db.tbl_purchasekart.Find(cartid);
            p.status = (int)purchasestatus.GetPrice;
            db.SaveChanges();
            if (userclientconnectionid != null)
            {
                await Clients.Client(userclientconnectionid).SendAsync("getPricefromServer", cartid,p.status.Value);

            }



        }
        public async Task toeditmsg(int msgid = 0, int currentuserid = 0, string msgtext = "")
        {
            var msg = db.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();
            var currentuser = db.tbl_signalrUsers.Where(a => a.userid == currentuserid).SingleOrDefault();
            var from = msg.from_userid;
            var to = msg.to_userid;
            string oldtext = msg.msg_textpure;
            string newtext = msgtext;
            int dtspan = (DateTime.Now - msg.datetime_send.Value).Days;
            if (dtspan > 1)
            {
                await Clients.Client(currentuser.connectionId).SendAsync("errorinpinopperation", "این پیام دیگر قابل ویرایش نمی باشد.");
            }
            else
            {
                msg.msg_text = msg.msg_text.Replace(oldtext, newtext);
                //msg.msg_text.Replace(msg.msg_textpure, msgtext);
                msg.msg_textpure = msgtext;
                db.SaveChanges();
                await Clients.All.SendAsync("toeditmsg", msgid, msgtext);





            }
        }


        public async Task deletepinmsg(int msgid = 0, int currentuserid = 0)
        {
            var currentuser = db.tbl_signalrUsers.Where(a => a.userid == currentuserid).SingleOrDefault();

            if (Authorize_swp.thisuserallowtodo(currentuserid, 37) == true)
            {
                var msg = db.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();
                msg.pin = false;
                msg.datetime_pin = null;
                db.SaveChanges();
                await Clients.All.SendAsync("delpinmsg", msgid);
            }
            else
            {
                await Clients.Client(currentuser.connectionId).SendAsync("errorinpinopperation", "شما مجاز به انجام این عملیات نیستید");
            }

        }


        public async Task deletemsg(int msgid = 0, int currentuserid = 0)
        {
            var msg = db.tbl_signalRmsg.Where(a => a.id == msgid).SingleOrDefault();

            var from = msg.from_userid;
            var to = msg.to_userid;
            int dtspan = (DateTime.Now - msg.datetime_send.Value).Days;
            if (from == currentuserid)
            {
                msg.visibletofrom = false;
                db.SaveChanges();
                var fromconnectionid = db.tbl_signalrUsers.Where(a => a.userid == from).SingleOrDefault().connectionId;
                await Clients.Client(fromconnectionid).SendAsync("deleteMessage", msgid);
            }
            else
            {
                msg.visibletoto = false;
                db.SaveChanges();
                if (to != null)
                {

                    var fromconnectionid = db.tbl_signalrUsers.Where(a => a.userid == to).SingleOrDefault().connectionId;
                    await Clients.Client(fromconnectionid).SendAsync("deleteMessage", msgid);
                }
                else
                {

                }
            }


            if (from == currentuserid && dtspan < 1)
            {
                msg.visibleforall = false;

                db.SaveChanges();
                var fromconnectionid = db.tbl_signalrUsers.Where(a => a.userid == from).SingleOrDefault().connectionId;

                await Clients.Client(fromconnectionid).SendAsync("deleteMessage", msgid);

                await Clients.All.SendAsync("deleteMessage", msgid);

            }



        }






        public async Task stoptypingmessageforall(int from_userid)
        {
            await Clients.All.SendAsync("stoptypingmsgforall", from_userid);

        }


        public async Task stoptypingmessage(int from_userid, int? to_userid)
        {
            var from_user = db.tbl_signalrUsers.Where(a => a.userid == from_userid).SingleOrDefault();

            if (to_userid == null)
            {
                var to_id = to_userid;
                var to_idname = "public";

                await Clients.All.SendAsync("stoptypingmsg", from_userid, from_user.username, to_id, to_idname);
            }
            else
            {
                var to_id = to_userid;
                var to_user = db.tbl_signalrUsers.Where(a => a.userid == to_userid).SingleOrDefault();
                if (to_user.connectionId != null)
                {

                    await Clients.Client(to_user.connectionId).SendAsync("stoptypingmsg", from_userid, from_user.username, to_id, to_user.username);
                }
            }

        }


        public async Task typingmessage(int from_userid, int? to_userid)
        {
            var from_user = db.tbl_signalrUsers.Where(a => a.userid == from_userid).SingleOrDefault();

            if (to_userid == null)
            {
                var to_id = to_userid;
                var to_idname = "public";

                await Clients.All.SendAsync("typingmsg", from_userid, from_user.username, to_id, to_idname);
            }
            else
            {
                var to_id = to_userid;
                var to_user = db.tbl_signalrUsers.Where(a => a.userid == to_userid).SingleOrDefault();
                if (to_user.connectionId != null)
                {
                    await Clients.Client(to_user.connectionId).SendAsync("typingmsg", from_userid, from_user.username, to_id, to_user.username);

                }
            }

        }


        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public async Task registeronlineuser(string uname, string email, string connectionid, int userid = 0)
        {

            int onlineuserid = 0;
            string usernameonline = "";
            if (userid != 0)
            {
                // اگر کاربر قبلا ثبت نام کرده باشد
                var user = db.tbl_user.Find(userid);
                uname = user.name;
                var onlinelist = db.tbl_tbl_onlineusers.Where(a => a.user_id == userid).SingleOrDefault();
                if (onlinelist == null)
                {
                    // اگر قبلا در جدول انلاین یوزیر نبوده باشد

                    tbl_tbl_onlineusers onlineuser = new tbl_tbl_onlineusers() { user_id = userid, connection_id = connectionid, nameu = user.name, email = user.email };
                    db.tbl_tbl_onlineusers.Add(onlineuser);
                    db.SaveChanges();
                    onlineuserid = onlineuser.id;
                    usernameonline = onlineuser.nameu;
                }
                else
                {
                    // اگر قبلا در جدول آنلاین یوزر بوده باشد
                    await Clients.All.SendAsync("signout", onlinelist.id);

                    onlinelist.connection_id = connectionid;
                    onlineuserid = onlinelist.id;
                    usernameonline = onlinelist.nameu;
                    db.SaveChanges();
                }
            }
            else
            {
                // اگر کاربر ثبت نام نکرده باشد.

                tbl_tbl_onlineusers onlineuser = new tbl_tbl_onlineusers() { user_id = null, connection_id = connectionid, nameu = uname, email = email };
                db.tbl_tbl_onlineusers.Add(onlineuser);
                db.SaveChanges();
                onlineuserid = onlineuser.id;
                usernameonline = onlineuser.nameu;

            }
            //  افزودن به onlinechatlist





            // مشخص کردن این مسئله از پشتیبانان کسی آنلاین هست یا خیر؟
            tbl_user suser = db.tbl_supporterinonlinechat.FirstOrDefault().user_;
            string msg;
            var onlinesupporter = db.tbl_tbl_onlineusers.Where(a => a.user_.tbl_supporterinonlinechat.Any() == true && a.connection_id != "");
            int onlinesupportercount = 0;
            if (onlinesupporter.Count() != 0)
            {
                onlinesupportercount = onlinesupporter.Count();

                //msg = "سلام " + uname + " جان ";

                tbl_tbl_onlineusers supporteronlineuser = db.tbl_tbl_onlineusers.Where(a => a.user_id == suser.id).FirstOrDefault();
                msg = " شما به " + db.tbl_supporterinonlinechat.FirstOrDefault().supporte_name + " وصل شده اید";


                await Clients.All.SendAsync("sayhello", msg, supporteronlineuser.id);

                string connection_idofsupporter = onlinesupporter.FirstOrDefault().connection_id;
                await Clients.Client(connection_idofsupporter).SendAsync("addto_onlinechatlist", onlineuserid, usernameonline);

            }
            else
            {
                // ارسال پیام خوش امد 

                msg = "سلام " + uname + " جان ";
                msg += "لطفا صبر کنید تا یکی از اعضای تیم را به شما متصل کنیم";

                await Clients.Client(connectionid).SendAsync("sayhello", msg);
                // ارسال پیامک به پشتیبان سایت
                Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                Cls_SMS.ClsSend sms_Single2 = new Cls_SMS.ClsSend();
                string[] ret1 = new string[2];
                string smstext = $"کاربری به نام {uname} منتظر جواب شماست. ///// کالای چوب سلطانی";
                string cellnumber = suser.cellphone;
                ret1 = sms_Single.SendSMS_Single(smstext, cellnumber, "100008001", "koohi8", "87g5820", "http://193.104.22.14:2055/CPSMSService/Access", "KOOHI", false);

            }




        }

        public async Task sendmessagetoserver(string msg, string connectionid, string uname, int supporter_id, int towhom)
        {
            var from = db.tbl_tbl_onlineusers.Where(a => a.connection_id == connectionid).SingleOrDefault();
            var to = db.tbl_tbl_onlineusers.Where(a => a.id == towhom).SingleOrDefault();

            if (db.tbl_supporterinonlinechat.Any(a => a.user_id == from.user_id) == true)
            {
                uname = db.tbl_supporterinonlinechat.Where(a => a.user_id == from.user_id).SingleOrDefault().supporte_name;
            }
            tbl_onlinechatmsg newmsg = new tbl_onlinechatmsg()
            {
                sendmsg_date = DateTime.Now,
                textmsg = msg,
                onlineuser_id = from.id,
                supporter_id = supporter_id,
                towhom = towhom


            };
            db.tbl_onlinechatmsg.Add(newmsg);
            db.SaveChanges();
            bool senderidsupporter = false;
            if (newmsg.onlineuser_id == newmsg.supporter_id)
            {
                senderidsupporter = true;
            }
            else
            {
                senderidsupporter = false;
            }
            await Clients.Client(from.connection_id).SendAsync("addmessage", uname, msg, newmsg.id, newmsg.sendmsg_date.ToString("HH:mm"), senderidsupporter, newmsg.onlineuser_id);
            await Clients.Client(to.connection_id).SendAsync("addmessage", uname, msg, newmsg.id, newmsg.sendmsg_date.ToString("HH:mm"), senderidsupporter, newmsg.onlineuser_id);
        }

        public async Task signoutonlineuser(string connectionid)
        {
            var user = db.tbl_tbl_onlineusers.Where(x => x.connection_id == Context.ConnectionId).SingleOrDefault();
            if (user != null)
            {
                user.connection_id = "";
                db.SaveChanges();
                await Clients.All.SendAsync("signout", user.id);
                await Clients.Client(connectionid).SendAsync("resetchat");
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = db.tbl_signalrUsers.Where(x => x.connectionId == Context.ConnectionId).SingleOrDefault();
            var user2 = db.tbl_tbl_onlineusers.Where(x => x.connection_id == Context.ConnectionId).SingleOrDefault();
            if (user2 != null)
            {
                user2.connection_id = "";
                db.SaveChanges();
                Clients.All.SendAsync("signout", user2.id);
            }
            if (user != null)
            {
                user.connectionId = null;
                db.SaveChanges();
                string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");
                Clients.All.SendAsync("SendSystemMessage", thisdatetime, user.username + " left.");
                var onlineusers = db.tbl_signalrUsers.Where(x => x.connectionId != null).Select(x => x.userid).ToList();
                Clients.All.SendAsync("addonlineusers", onlineusers);
            }
            return null;
        }


        //public async Task registerinpurchasecartsignalr(int userid, string connectionid)
        //{
        //    //_4820_soltaniwebContext db = new _4820_soltaniwebContext();
        //    var user = db.tbl_purchasecartSignalR.Where(a => a.userid.Value == userid).SingleOrDefault();
        //    if (user == null)
        //    {
        //        tbl_purchasecartSignalR newuser = new tbl_purchasecartSignalR { userid = userid, connectionid_cart = connectionid };

        //        db.tbl_purchasecartSignalR.Add(newuser);

        //    }
        //    else
        //    {
        //        user.connectionid_cart = connectionid;
        //        await Clients.Client(user.connectionid_cart).SendAsync("showtimertoclient", "please wait at least 1 hr ...");
        //        //Clients.Client(user.connectionId).setonline(" is Online");
        //    }
        //    db.SaveChanges();
        //    //var onlineusers = db.tbl_signalrUsers.Where(x => x.connectionId != null).Select(x => x.userid).ToList();
        //    //await Clients.All.SendAsync("addonlineusers", onlineusers);
        //    //Clients.All.addonlineusers(onlineusers);
        //    //string thisdatetime = DateTime.Now.ToPersianDate() + "  " + DateTime.Now.ToString("HH:mm:ss");

        //    //shamsi.ToShamsi(DateTime.Now).ToString("yyyy/MM/dd  hh:mm:ss");
        //    //Clients.All.SendSystemMessage(thisdatetime, user.username + " آنلاین شد.");
        //}
    }
}
