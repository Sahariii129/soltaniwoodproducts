using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Services
{
    public class ProductsServices : IProductsServices
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();



        public List<tbl_products> GetAllproducts()
        {
            return db.tbl_products.ToList();
        }

        public List<tbl_products> GetproductsOfCategory(int CatgID = 0)
        {
            return db.tbl_products.Where(a => a.categoryid == CatgID).OrderBy(a => a.codename).ToList();
        }

        public bool GetStatuseOFCategory(int CatgID = 0)
        {
            var category = db.tbl_category.Where(a => a.id == CatgID).SingleOrDefault();
            if (category == null)
            {
                return false;
            }
            if (category.status.HasValue)
            {
                if (category.status.Value == true)
                {
                    return true;

                }
            }

            return false;
        }

        public List<tbl_products> GetproductsOfCategoryToShow(int CatgID = 0)
        {

            if (GetStatuseOFCategory(CatgID) == true)
            {
                return GetproductsOfCategory(CatgID).Where(a => (a.status.HasValue ? a.status.Value == true : 1 == 2)).ToList();

            }
            return null;
        }

        public List<tbl_products> Searchproducts(string filter, int section_id = 0, int category_id = 0, int order = 0, int status = 0)
        {
            IQueryable<tbl_products> result = db.tbl_products;

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.Replace("ي", "ی").Replace("ك", "ک");

                result = result.Where(a => a.name.Contains(filter) || a.category.categoryname.Contains(filter) || a.description.Contains(filter) || a.keywords.Contains(filter) || a.codename.Contains(filter));
            }
            if (section_id != 0)
            {
                result = result.Where(a => a.category.section_.id == section_id);
            }
            if (category_id != 0)
            {
                result = result.Where(a => a.category.id == category_id);
            }
            switch (status)
            {
                case 0:
                    break;
                case 1:
                    result=result.Where(a=>a.tbl_listkala97.Sum(h=>h.kalanumberm)>0);
                    break;

                default:
                    break;
            }

            switch (order)
            {
                case 0:
                    result = result.OrderByDescending(a => (a.tbl_listkala97.Sum(b => b.kalanumberm)));
                    break;
                case 1:
                    result = result.OrderBy(a => a.category.categoryname);
                    break;
                case 2:
                    result = result.OrderBy(a => a.name);
                    break;
                case 3:
                    result = result.OrderBy(a => a.codename);
                    break;

                default:
                    break;
            }
            
            result = GetProductsStatusTrue(result);
          

            return result.ToList();

        }

        public IQueryable<tbl_products> GetProductsStatusTrue(IQueryable<tbl_products> products)
        {
            IQueryable<tbl_products> result = products;
            result = result.Where(a => (a.status.HasValue) ? a.status.Value == true : 1 == 2);
            result = result.Where(a => (a.category.status.HasValue) ? a.category.status.Value == true : 1 == 2);

            return result;
        }

        public List<tbl_listkala97> AdvancedSearchListkala(string sabtdate, int productid, int anbarid, int havalehtypeid, int usernameid, string desc)
        {
            IQueryable<tbl_listkala97> result = db.tbl_listkala97;
            if (sabtdate != null)
            {
                DateTime thisdate = (sabtdate == null ? DateTime.Now : shamsi.PersianDateToGregorianDate(sabtdate));
                result = result.Where(a => a.sodoordate == thisdate.Date);

            }
            if (productid!=0)
            {
                result = result.Where(a => a.productid == productid);
            }

            if (anbarid!=0)
            {
                result = result.Where(a => a.anbarid == anbarid);
            }
            if (havalehtypeid!=0)
            {
                result = result.Where(a => a.htype == havalehtypeid);
            }
            if (usernameid!=0)
            {
                result = result.Where(a => a.usernameid == usernameid);
            }
            if (!string.IsNullOrEmpty(desc))
            {
                result = result.Where(a => a.kaladescription.Contains(desc));
            }
            return result.ToList();

        }
    }
}
