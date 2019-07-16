using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs.PersonVM
{
    public class PersonCreateViewModel
    {
        public PersonCreateViewModel()
        {
            PersonAddresses = new List<PersonAddressViewModel>();
            PersonInformationSetting = new List<PersonInformationSettingViewModel>();
        }
        public int GroupId { get; set; }
        public int Id { get; set; }
        public string GroupName { get; set; }


        [Display(Name = "نام مشتری")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(250, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string FName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(250, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string LName { get; set; }


        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(250, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Sex { get; set; }

        [Display(Name = "شعبه")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        public int BrancheId { get; set; }

        [Display(Name = "پیشوند")]
        [MaxLength(250, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Prefix { get; set; }


        [Display(Name = "کد ملی")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Codemelli { get; set; }


        [Display(Name = "شماره همراه")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Cell { get; set; }


        [Display(Name = "شماره تماس")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Tell { get; set; }


        [Display(Name = "توضیحات")]
        [MaxLength(2000, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Pdescription { get; set; }
        //[Display(Name = "کد ملی")]
        //[Required(ErrorMessage = "{0} اجباری می باشد")]
        //[MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        //public string Prefix { get; set; }
        //public string Sex { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime? Birthday { get; set; }
        public List<PersonAddressViewModel> PersonAddresses { get; set; }
        public List<PersonInformationSettingViewModel> PersonInformationSetting { get; set; }
    }
}
