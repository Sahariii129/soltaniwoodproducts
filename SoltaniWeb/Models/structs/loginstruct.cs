using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SoltaniWeb.Models.structs

{
   public class loginstruct
    {

        [Display(Name = "نام کاربری : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین سه تا 100 کاراکتر باشد")]
        [RegularExpression(@"[a-zA-Z0-9\-\._\s]+", ErrorMessage = "نام کاربری  حاوی کاراکتر های معتبر نمی باشد")]
       
        public string username { get; set; }
        [StringLength(100,MinimumLength=3,ErrorMessage="حداقل تعداد کاراکتر 3 و حداکثر تعداد کاراکتر 100 می باشد")]
        [Required (AllowEmptyStrings =false ,ErrorMessage ="کلمه عبور حتما باید وارد شود")]
        public string password { get; set; }


        public bool RememberMe { get; set; }


    }

   public class email
   {
       [Display(Name="آدرس ایمیل")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل معتبر نمی باشد")]
        public string emailaddress { get; set; }
   }

    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین سه تا 100 کاراکتر باشد")]
        [RegularExpression(@"[a-zA-Z0-9\-\._\s]+", ErrorMessage = "نام کاربری  حاوی کاراکتر های معتبر نمی باشد")]
        [Remote("DuplicateUserName", "Home", ErrorMessage = "نام کاربری تکراری است", HttpMethod = "POST")]
        public string username { get; set; }
        [Display(Name = "کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]

        public string password { get; set; }

        [Display(Name = "تائید کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]
        [Compare("password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string confirmpass { get; set; }

        [Display(Name = "ایمیل : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ایمیل را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "ایمیل باید بین سه تا 100 کاراکتر باشد")]
        //[RegularExpression(@"[\w_\.\-]+[@][\w_\.\-]+[.][\w_\.]+", ErrorMessage = "قالب ایمیل را به درستی وارد نمایید")]
        [EmailAddress(ErrorMessage ="آدرس ایمیل معتبر نمی باشد")]
        [Remote("DuplicateEmail", "Home", ErrorMessage = "ایمیل تکراری است", HttpMethod = "POST")]
        public string email { get; set; }
      
        [Display(Name = "شماره موبایل :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ورود شماره موبایل الزامی است")]
        [RegularExpression(@"(0|\+98)?([ ]|,|-|[()]){0,2}9[0|1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}", ErrorMessage = "شماره موبایل معتبر نمی باشد")]
        public string cellphone { get; set; }
    }


    public class UserPanelViewModel
    {
        public int useid { get; set; }
        public string username { get; set; }
        public string actionname { get; set; }
        public string  accesscaption { get; set; }
        public string fullname { get; set; }
        public byte[] image { get; set; }
        public string controllername { get; set; }
    }


    public class userchangepasswordviewmodel
    {
        [Display(Name = "نام کاربری : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین سه تا 100 کاراکتر باشد")]
        [RegularExpression(@"[a-zA-Z0-9\-\._\s]+", ErrorMessage = "نام کاربری  حاوی کاراکتر های معتبر نمی باشد")]
        [Remote("DuplicateUserName", "Home", ErrorMessage = "نام کاربری تکراری است", HttpMethod = "POST")]
        public string username { get; set; }
        [Display(Name = "کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]

        public string password { get; set; }

        [Display(Name = "تائید کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]
        [Compare("password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string confirmpass { get; set; }
        public int userid { get; set; }
    }




    public class userinfomodelview
    {
        public int id { get; set; }
        [Display(Name = "نام کاربری : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "نام کاربری باید بین سه تا 100 کاراکتر باشد")]
        [RegularExpression(@"[a-zA-Z0-9\-\._\s]+", ErrorMessage = "نام کاربری  حاوی کاراکتر های معتبر نمی باشد")]
        [Remote("DuplicateUserName", "Home", ErrorMessage = "نام کاربری تکراری است", HttpMethod = "POST")]
        public string username { get; set; }
        [Display(Name = "کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]
        public string password { get; set; }
        [Display(Name = "تائید کلمه عبور : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه ی عبور را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "کلمه ی عبور باید بین سه تا 100 کاراکتر باشد")]
        [Compare("password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string confirmpass { get; set; }
        public string name { get; set; }
        public string Lname { get; set; }
        [Display(Name = "ایمیل : ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ایمیل را وارد نمایید")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "ایمیل باید بین سه تا 100 کاراکتر باشد")]
        //[RegularExpression(@"[\w_\.\-]+[@][\w_\.\-]+[.][\w_\.]+", ErrorMessage = "قالب ایمیل را به درستی وارد نمایید")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل معتبر نمی باشد")]
        [Remote("DuplicateEmail", "Home", ErrorMessage = "ایمیل تکراری است", HttpMethod = "POST")]
        public string email { get; set; }
        public int Access { get; set; }
        public string userdescription { get; set; }
        public bool status { get; set; }
        public int uniqkey { get; set; }
        public string Tokenstring { get; set; }
      
        public byte[] img { get; set; }
        [Display(Name = "شماره موبایل :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ورود شماره موبایل الزامی است")]
        [RegularExpression(@"(0|\+98)?([ ]|,|-|[()]){0,2}9[0|1|2|3|4]([ ]|,|-|[()]){0,2}(?:[0-9]([ ]|,|-|[()]){0,2}){8}", ErrorMessage = "شماره موبایل معتبر نمی باشد")]
        public string cellphone { get; set; }
        public DateTime? signupdate { get; set; }
        public int? branches_id { get; set; }
    }

}
