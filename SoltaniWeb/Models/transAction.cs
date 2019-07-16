using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models
{

    public class transAction
    {
        //APIKey دریافتی شما از Pay.ir
        public  string api { get;set;} 

        // مبلغ تراکنش به صورت ریالی و بزرگتر یا مساوی 1000
        [DisplayName("مبلغ پرداختی")]
        public String amount { get; set; }

        //   آدرس بازگشتی به صورت urlSerialize ، که باید با آدرس درگاه پرداخت تایید شده در Pay.ir بر روی یک دامنه باشد
        public string redirect { get; set; }

        // mobile string شماره موبایل پرداخت کننده جهت نمایش لیست کارت های بانکی پرداخت کننده
        public string factorNumber { get; set; }
    }
}