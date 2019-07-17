using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Extensions;

namespace SoltaniWeb.Models.structs.CompanyVM
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام شرکت")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(1000, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Name { get; set; }

        [Display(Name = "نوع شرکت")]
        //[Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(500, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string CompanyType { get; set; }

        [UIHint("_ClientCompanyTypes")]
        public CompanyTypeClass CompanyTypes
        {
            get;
            set;
        }
       

        [Display(Name = "شماره تماس")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Phone { get; set; }

        [Display(Name = "شماره تماس 2")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Phone2 { get; set; }

        [Display(Name = "شماره تماس 3")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Phone3 { get; set; }

        [Display(Name = "آدرس")]
        [MaxLength(2000, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Address { get; set; }

        [Display(Name = "شماره ثبت شرکت")]
        [MaxLength(50, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string CompanyRegistrationNumber { get; set; }
    }

    public class CompanyTypeClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
