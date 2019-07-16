using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class productsrepository
    {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public IQueryable<tbl_products> getproducts(string s = "")
        {

            var q = (from a in db.tbl_products
                    where ((a.name.Contains(s) || a.codename.Contains(s)) && (a.status==true))
                    select a).OrderBy(a=> a.codename);

            return q;
        }

        public virtual tbl_products getproductsbyid(int id = 0)
        {

         
            var q = db.tbl_products.Where(a => a.idproduct == id ).SingleOrDefault();

            return q;
        }

        public int Get_InventoryofProductByID (int productid = 0)
        {
            int Inventory = db.tbl_listkala97.Where(a => a.productid == productid).Sum(a => a.kalanumberm);
            return Inventory;
        }

        public List<inventoryofproduct> Get_InventoryofProductByIDineachanbar(int productid = 0)
        {
            var q = db.tbl_listkala97.Where(a => a.productid == productid).GroupBy(a => new { a.productid, a.anbarid , a.anbar.anbarname }).Select(g => new inventoryofproduct
            {
                anbarid = g.Key.anbarid,
                anbarname=g.Key.anbarname,
                inventory= g.Sum(d=>d.kalanumberm)

            }).ToList();
            return q;
        }

    }
}
