using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Extensions
{
    public enum PersonInformationSetting
    {
        [Description("شماره همراه")]
        [Display(Name = "شماره همراه")]
        Mobile,
        [Description("ایمیل")]
        [Display(Name = "ایمیل")]
        Email,
        [Description("شماره حساب")]
        [Display(Name = "شماره حساب")]
        AccountNumber,
        [Description("حساب شبکه های اجتماعی")]
        [Display(Name = "حساب شبکه های اجتماعی")]
        SocialNetworkingAccount,
    }

    public enum CompanyType
    {
        [Description("شرکت")]
        [Display(Name = "شرکت")]
        Company,

        [Description("موسسه")]
        [Display(Name = "موسسه")]
        Institution,

        [Description("مجتمع")]
        [Display(Name = "مجتمع")]
        CommercialComplex,

        [Description("کارخانه")]
        [Display(Name = "کارخانه")]
        Factory,

    }
    public enum MessageType
    {
        Success = 0,
        Danger = 1,
        Warning = 2,
        Info = 3,
    }
}
