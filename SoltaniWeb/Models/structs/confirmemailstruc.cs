using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models.structs
{
    public class confirmemailstruc
    {

        [RegularExpression(@"[a-zA-Z0-9\-\._\s]+", ErrorMessage = "نام  حاوی کاراکتر های معتبر نمی باشد")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "حداقل تعداد کاراکتر 3 و حداکثر تعداد کاراکتر 100 می باشد")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری حتما باید وارد شود")]
        public string username { get; set; }


        public int uniqkey { get; set; }
    }
  



}