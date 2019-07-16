using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.repository
{
    public class applicantrep
    {
        public int applicant_id { get; set; }
                        [Required(ErrorMessage = "ورود نام  الزامی است")]
                        [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]
        public string f_name { get; set; }
                        [Required(ErrorMessage = "ورود نام خانوادگی الزامی است")]
                        [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]

        public string l_name { get; set; }
        [Display(Name="کد ملی:")]
        public string codemelli { get; set; }
        public string shenasnamenum { get; set; }
                [Required(ErrorMessage = "ورود نام پدر الزامی است")]
                [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]

        public string fathername { get; set; }
                        [Required(ErrorMessage = "ورود شماره موبایل الزامی است")]
                        [RegularExpression(@"(0|\+98)?([ ]|,|-|[()]){0,2}9[0|1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}", ErrorMessage = "شماره موبایل معتبر نمی باشد")]
                        [Display(Name = "شماره موبایل:")]

        public string cellnumber { get; set; }
      
        public Nullable<int> sex { get; set; }
        public Nullable<int> degree_id { get; set; }
        public byte[] applicant_image { get; set; }
        public Nullable<int> job_id { get; set; }

        public Nullable<bool> Married { get; set; }
                        [Required(ErrorMessage = "ورود نام شهر محل تولد الزامی است")]
                        [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]

        public string borncity { get; set; }
     
        public  Nullable<System.DateTime> birthday { get; set; }
        public string Religion { get; set; }
        public Nullable<bool> Mentalphysicalhealth { get; set; }
        public Nullable<int> militaryservicestatus { get; set; }
        public Nullable<bool> Insurance { get; set; }
        public string Insurancepriod { get; set; }
        public string Insurancenumber { get; set; }
        public string method_introduction { get; set; }
        public string Expected_salary { get; set; }
        public Nullable<bool> workingnow { get; set; }
        public string nationality { get; set; }
        public Nullable<int> word { get; set; }
        public Nullable<int> excell { get; set; }
        public Nullable<int> typeskil { get; set; }
        public Nullable<int> en_language { get; set; }
        public Nullable<int> windows { get; set; }
        public Nullable<int> internet { get; set; }
        public Nullable<int> access { get; set; }
        public Nullable<int> powerpoint { get; set; }
        public Nullable<int> Type_cooperation { get; set; }
        public Nullable<int> number_child { get; set; }
        public Nullable<bool> status_smoking { get; set; }
        public Nullable<System.DateTime> sabtdate { get; set; }
                                [Required(ErrorMessage = "ورود رشته تحصیلی الزامی است")]
                                [RegularExpression(@"[ا-یa-zA-Z0-9\-\._\sآءئ]+", ErrorMessage = "  حاوی کاراکتر های معتبر نمی باشد")]

        public string edu_field { get; set; }
        public Nullable<bool> driverlicense { get; set; }
        public string otherskiles { get; set; }
        public Nullable<int> photoshop { get; set; }
        public Nullable<int> illustrator { get; set; }
                                [Display(Name = "کد تائید:")]

        public Nullable<long> uniqekey { get; set; }
   

      
    }
}