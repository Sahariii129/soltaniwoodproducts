using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models.repository
{
    public class ftprepository
    {
        public virtual tbl_ftp getftp(int id)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var q = db.tbl_ftp.Where(a => a.id == id).SingleOrDefault();

            return q;

        }


    }
}