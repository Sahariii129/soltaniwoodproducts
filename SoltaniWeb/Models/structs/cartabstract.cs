using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SoltaniWeb.Models.structs

{
   public class cartabstract

    {
        public int userid { get; set; }

      public int number  { get; set; }

      public int numberitemexist { get; set; }


    }
 


   public class editnumberiteminpurchasecart
   {
       public string newprice { get; set; }
       public string totalpriceitem { get; set; }
       public string totalprice { get; set; }
       public string discount { get; set; }
       public string payableprice { get; set; }
       public string totalnumber { get; set; }
       public string totalweight { get; set; }
       public string totalcosttransportation { get; set; }



   }


   public class transportation
   {

       public string  cost { get; set; }
       public decimal number { get; set; }
       public string vehicletype { get; set; }
       public int vehicletypeid { get; set; }
       public string totalcost { get; set; }
       public string address { get; set; }
       public string distance { get; set; }
   }

   public class latlng
   {
       public string lat { get; set; }
       public string lng { get; set; }
   }

    public class imageslider
    {
        public int? height { get; set; }
        public int? width { get; set; }
    }

    public class productcategory
    {
        public int id { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
        public string imagecategory { get; set; }
        public string shortname { get; set; }
        public string keywords { get; set; }
        public int section_id { get; set; }
        public string actionname { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<bool> showprice { get; set; }
        public string fulldescription { get; set; }
        public Nullable<bool> purchaseonline { get; set; }
        public Nullable<int> weight { get; set; }
        
    }

    public class report_obj
    {
        public string Report_Name { get; set; }
        public string Report_Date { get; set; }
    }
    public class systemmsg
    {
        public int id { get; set; }
        public string systemmsg_text { get; set; }
        public string changedatetime { get; set; }
    }
    public class myorderlist
    {
        public int itemid { get; set; }
        public int itemnumber { get; set; }
        public string pdesc { get; set; }
        public string itemname { get; set; }
        public int row { get; set; }

    }

    public class myorder
    {
        public Nullable<int> orderid { get; set; }
        public string orderdate { get; set; }
        public int frombranch { get; set; }
        public int tobranch { get; set; }
        public int forperson { get; set; }
        public string orderdesc { get; set; }
        public string cellphone { get; set; }
        public List<myorderlist> itemlist { get; set; }


    }

    public class deliverorderinfo
    {
        public int orderid { get; set; }
        public int? userid { get; set; }
        public string username { get; set; }
        public string deliverdate { get; set; }

        public string done { get; set; }
        public string deliverorderdesc { get; set; }

    }


      

}



