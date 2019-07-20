using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoltaniWeb.Models.Extensions;

namespace SoltaniWeb.Models.structs.PersonVM
{
    public class PersonViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<string> Companys { get; set; }
        public int GroupId { get; set; }
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<string> PersonGroups { get; set; }
        public string FullNamePerson { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string CodeMeli { get; set; }
        public string Cell { get; set; }
        public string Tell { get; set; }
        public string Address { get; set; }
        public string BrancheName { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public string CreateDatetimeShamsi => CreateDatetime!= null? CreateDatetime.Value.ToPersianDate():"";
        public DateTime? Birthday { get; set; }
        public List<PersonAddressViewModel> PersonAddresses { get; set; }
        public List<PersonInformationSettingViewModel> PersonInformationSetting { get; set; }
    }

    

    public class PersonAddressViewModel
    {
        public int PersonId { get; set; }

        public int Id { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(500, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Address { get; set; }


        [Display(Name = "شماره تماس 1")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Phone { get; set; }


        [Display(Name = "شماره تماس 2")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]

        public string Phone2 { get; set; }
        [Display(Name = "شماره تماس 3")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string Phone3 { get; set; }


        [Display(Name = "کد پستی")]
        [MaxLength(11, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string PostalCode { get; set; }
    }
    public class PersonInformationSettingViewModel
    {
        public int PersonId { get; set; }
        public int Id { get; set; }
        public string PropertyName { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "{0} اجباری می باشد")]
        [MaxLength(500, ErrorMessage = "حداکثر طول فیلد {0}، {1} کاراکتر می باشد")]
        public string PropertyValue { get; set; }
    }
}
