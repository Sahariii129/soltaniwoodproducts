using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models.Services.Interfaces
{
   public interface IUserServices
    {
        int GetUseridByUsername(string username);
        tbl_user GetUserByUsername(string username);
        bool UserCanSeeMoreProductInfo(string username);

        bool updateaccessleveltouser(int userid);
        bool GiveAccessMenuToUser(int userid);

    }
}
