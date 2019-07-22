using SoltaniWeb.Models.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models.Services.Interfaces
{
  public  interface IPurchaseCart
    {
        tbl_purchasekart findcartbyid(int cartid);
        int itemnumberincart(int userid = 0);
        bool ISPriceValid(int cartid = 0);
        cartabstractinfo GetCartAbstractinfo(int cartid = 0);
        int opencartid(int username);
        void settransportaiondetails(int cartid , tbl_transportaiondetails tcartdetails);
        
        





    }
}
