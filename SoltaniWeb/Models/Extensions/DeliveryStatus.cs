using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Extensions
{
    public enum DeliveryStatus
    {
        [Description("پیامک با موفقیت ارسال و توسط گوشی دریافت شده است.")]
        [Display(Name = "دریافت شد")]
        MT_DELIVERED,
        [Description("پیامک به مخابرات ارسال شده است")]
        [Display(Name = "پیامک به مخابرات ارسال شده است")]
        CHECK_OK,
        [Description("پیامک توسط سرور دریافت نشده است.")]
        [Display(Name = "پیامک توسط سرور دریافت نشده است.")]
        CHECK_ERROR,
        [Description("در ارسال پیامک خطا رخ داده است.")]
        [Display(Name = "در ارسال پیامک خطا رخ داده است.")]
        SMS_FAILED
    }
}
