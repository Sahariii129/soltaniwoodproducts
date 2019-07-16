using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class applicantrepfirst
    {
        public int applicant_id { get; set; }
        [Required(ErrorMessage = "ورود نام  الزامی است")]
        [Display(Name = "نام :")]
        [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]
        public string f_name { get; set; }
        [Required(ErrorMessage = "ورود نام خانوادگی الزامی است")]
        [Display(Name = "نام خانوادگی :")]
        [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]
        public string l_name { get; set; }
        [Required(ErrorMessage = "ورود کد ملی الزامی است")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی معتبر نمی باشد")]

        [Display(Name = "کد ملی :")]
        public string codemelli { get; set; }
        [Required(ErrorMessage = "ورود شماره موبایل الزامی است")]
        [RegularExpression(@"(0|\+98)?([ ]|,|-|[()]){0,2}9[1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}", ErrorMessage = "شماره موبایل معتبر نمی باشد")]


        [Display(Name = "شماره موبایل :")]
        public string cellnumber { get; set; }

        public Nullable<int> sex { get; set; }

        public Nullable<int> job_id { get; set; }

        [Required(ErrorMessage = "ورود تاریخ تولد الزامی است")]
        [RegularExpression(@"^(\d{4})/(0?[1-9]|1[012])/(0?[1-9]|[12][0-9]|3[01])$",ErrorMessage="فرمت تاریخ وارد صحیح نمی باشد")]

        public string birthday { get; set; }

        [Display(Name = "کد تائید :")]
        public Nullable<long> uniqekey { get; set; }
        public Nullable<System.DateTime> sabtdate { get; set; }


    }
}