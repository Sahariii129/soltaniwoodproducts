using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.structs
{
    public class productstructure
    {
        public string productid { get; set; }
        public string categoryid { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "وارد کردن نام محصول الزامی است.")]

        public string name { get; set; }
        public string dimension { get; set; }
        public string codename { get; set; }
        public string imageproducts { get; set; }
        public string description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تعیین کلاس الزامی است.")]

        public string grade { get; set; }
        public string keywords { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تعیین حد نصاب الزامی است.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "حد نصاب باید عدد باشد.")]
        public string inventory { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تعیین قیمت خرید الزامی است.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "قیمت خرید وارده معتبر نمی باشد")]
        public string lastbuycost { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تعیین قیمت فروش الزامی است.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "قیمت فروش وارده معتبر نمی باشد")]
        public string lastcellcost { get; set; }
       
        public string status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تعیین وزن الزامی است.")]
        [RegularExpression(@"[0-9\.]+", ErrorMessage = "وزن وارده معتبر نمی باشد")]
        public string weight { get; set; }



    }

public class selectedproducts
    {
        public List<int> productlist { get; set; }
    }


}