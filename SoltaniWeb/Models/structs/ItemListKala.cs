using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs
{
           
    public class ItemListKala
    {

        public int id { get; set; }
        public string sodoordate { get; set; }
        public string time { get; set; }
        public string htype { get; set; }
        public int anbarid { get; set; }
        public string anbarname { get; set; }
        public int productid { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int kalanumber { get; set; }
        public decimal cost { get; set; }
        public decimal totalcostkala { get; set; }
        public string kaladescription { get; set; }
        public string user { get; set; }
        public int kalanumberm { get; set; }
     

    }



    public class ItemListKalatoadd
    {

        public int? id { get; set; }

        public int productid { get; set; }


        public int kalanumber { get; set; }

        public decimal kalacost { get; set; }
        public string kaladescription { get; set; }
        public decimal totalcost { get; set; }
        public string sodoordate { get; set; }
        public string username { get; set; }
        public int htype { get; set; }
        public int kalanumberm { get; set; }
        public int anbarid { get; set; }
        public string time { get; set; }


    }

    public class inventoryofproduct
    {
        public int anbarid { get; set; }
        public string anbarname { get; set; }
        public int inventory { get; set; }
    }



    public class inventoryTable
    {
        public int id { get; set; }
        public string productname { get; set; }
        public string code { get; set; }
        public int catgid { get; set; }
        public string catgname { get; set; }
        public int inventory { get; set; }
        public int mininventory { get; set; }
        public string lastBuyPrice { get; set; }
        public string lastBuyDate { get; set; }
        public string lastSellPrice { get; set; }
        public string lastSellDate { get; set; }

    }

    public class ShowAbstractInventory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Inventory { get; set; }
    }


    public class ShowAbstracttoorder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int numberofproducttoorder { get; set; }
    }




}
