using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_user
    {
        public tbl_user()
        {
            tbl_accesslevels = new HashSet<tbl_accesslevels>();
            tbl_actionaccessibility = new HashSet<tbl_actionaccessibility>();
            tbl_files = new HashSet<tbl_files>();
            tbl_filestosend = new HashSet<tbl_filestosend>();
            tbl_historyappconnect = new HashSet<tbl_historyappconnect>();
            tbl_listkala = new HashSet<tbl_listkala>();
            tbl_listkala97 = new HashSet<tbl_listkala97>();
            tbl_news = new HashSet<tbl_news>();
            tbl_newsimagefiles = new HashSet<tbl_newsimagefiles>();
            tbl_newsimages = new HashSet<tbl_newsimages>();
            tbl_orderdone_user = new HashSet<tbl_order>();
            tbl_orderuser = new HashSet<tbl_order>();
            tbl_purchasekart = new HashSet<tbl_purchasekart>();
            tbl_samples = new HashSet<tbl_samples>();
            tbl_searchproducts = new HashSet<tbl_searchproducts>();
            tbl_signalRmsgfrom_user = new HashSet<tbl_signalRmsg>();
            tbl_signalRmsgto_user = new HashSet<tbl_signalRmsg>();
            tbl_signalrUsers = new HashSet<tbl_signalrUsers>();
            tbl_statistics = new HashSet<tbl_statistics>();
            tbl_supporterinonlinechat = new HashSet<tbl_supporterinonlinechat>();
            tbl_tbl_onlineusers = new HashSet<tbl_tbl_onlineusers>();
            tbl_transaction = new HashSet<tbl_transaction>();
            tbl_usermoreinfo = new HashSet<tbl_usermoreinfo>();
            tbl_sentMessag = new HashSet<tbl_sentMessag>();
        }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string Lname { get; set; }
        public string email { get; set; }
        public int Access { get; set; }
        public string userdescription { get; set; }
        public bool status { get; set; }
        public int uniqkey { get; set; }
        public string Tokenstring { get; set; }
        public string image { get; set; }
        public int? ftpid { get; set; }
        public bool? online { get; set; }
        public string confirmpass { get; set; }
        public byte[] img { get; set; }
        public string cellphone { get; set; }
        public DateTime? signupdate { get; set; }
        public int? branches_id { get; set; }


        public bool? vipmember { get; set; }
        public virtual tbl_purchasecartSignalR tbl_purchasecartSignalR { get; set; }

        public virtual tbl_branches branches_ { get; set; }
        public virtual tbl_ftp ftp { get; set; }
        public virtual ICollection<tbl_accesslevels> tbl_accesslevels { get; set; }
       public virtual ICollection<tbl_actionaccessibility> tbl_actionaccessibility { get; set; }
       public virtual ICollection<tbl_files> tbl_files { get; set; }
       public virtual ICollection<tbl_filestosend> tbl_filestosend { get; set; }
       public virtual ICollection<tbl_historyappconnect> tbl_historyappconnect { get; set; }
       public virtual ICollection<tbl_listkala> tbl_listkala { get; set; }
       public virtual ICollection<tbl_listkala97> tbl_listkala97 { get; set; }
       public virtual ICollection<tbl_news> tbl_news { get; set; }
       public virtual ICollection<tbl_newsimagefiles> tbl_newsimagefiles { get; set; }
       public virtual ICollection<tbl_newsimages> tbl_newsimages { get; set; }
       public virtual ICollection<tbl_order> tbl_orderdone_user { get; set; }
       public virtual ICollection<tbl_order> tbl_orderuser { get; set; }
       public virtual ICollection<tbl_purchasekart> tbl_purchasekart { get; set; }
       public virtual ICollection<tbl_samples> tbl_samples { get; set; }
       public virtual ICollection<tbl_searchproducts> tbl_searchproducts { get; set; }
       public virtual ICollection<tbl_signalRmsg> tbl_signalRmsgfrom_user { get; set; }
       public virtual ICollection<tbl_signalRmsg> tbl_signalRmsgto_user { get; set; }
       public virtual ICollection<tbl_signalrUsers> tbl_signalrUsers { get; set; }
       public virtual ICollection<tbl_statistics> tbl_statistics { get; set; }
       public virtual ICollection<tbl_supporterinonlinechat> tbl_supporterinonlinechat { get; set; }
       public virtual ICollection<tbl_tbl_onlineusers> tbl_tbl_onlineusers { get; set; }
       public virtual ICollection<tbl_transaction> tbl_transaction { get; set; }
       public virtual ICollection<tbl_usermoreinfo> tbl_usermoreinfo { get; set; }
        public virtual ICollection<tbl_sentMessag> tbl_sentMessag { get; set; }
    }
}
