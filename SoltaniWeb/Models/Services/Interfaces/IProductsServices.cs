using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models.Services.Interfaces
{
    public interface IProductsServices
    {
        List<tbl_products> GetAllproducts();
        List<tbl_products> GetproductsOfCategory(int CatgID = 0);
        bool GetStatuseOFCategory(int CatgID = 0);
        List<tbl_products> GetproductsOfCategoryToShow(int CatgID = 0);
        IQueryable<tbl_products> GetProductsStatusTrue(IQueryable<tbl_products> products);
        List<tbl_products> Searchproducts(string filter, int section_id = 0, int category_id = 0, int order = 0, int status = 0);
        List<tbl_listkala97> AdvancedSearchListkala(string sabtdate, int productid, int anbarid, int havalehtypeid, int usernameid, string desc);


    }
}
