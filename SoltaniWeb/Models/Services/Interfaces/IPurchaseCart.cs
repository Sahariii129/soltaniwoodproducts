using SoltaniWeb.Models.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Services.Interfaces
{
  public  interface IPurchaseCart
    {

        int itemnumberincart(int userid = 0);
        bool ISPriceValid(int cartid = 0);
        cartabstractinfo GetCartAbstractinfo(int cartid = 0);
        int opencartid(int username);




    }
}
