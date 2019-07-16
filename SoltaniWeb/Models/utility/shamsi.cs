using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

using SoltaniWeb.Models.Extensions;




    public static class shamsi
    {

        public static DateTime ToShamsi(DateTime dt)
        {
            try
            {
                PersianCalendar pDate = new PersianCalendar();




                return new DateTime(pDate.GetYear(dt), pDate.GetMonth(dt), pDate.GetDayOfMonth(dt), pDate.GetHour(dt), pDate.GetMinute(dt), pDate.GetSecond(dt), new GregorianCalendar());

            }
            catch (Exception ex)
            {
                //PersianCalendar pDate = new PersianCalendar();
                //DateTime t1 = new DateTime();
                //t1 = DateTime.Parse("2017-07-22");
                ////MessageBox.Show(ex.Message + "---" + ex.InnerException);
                //return new DateTime(pDate.GetYear(dt), pDate.GetMonth(dt), pDate.GetHour(t1), pDate.GetHour(dt), pDate.GetMinute(dt), pDate.GetSecond(dt));
                return new DateTime();
            }



        }

        public static string ToShamsinull(DateTime? dt)
        {
            if (dt.HasValue==true)
            {
                PersianCalendar pDate = new PersianCalendar();
                //return new DateTime ()
                DateTime dt1 = dt.Value;
                string shamsidate = pDate.GetYear(dt1).ToString() + "/" + pDate.GetMonth(dt1).ToString() + "/" + pDate.GetDayOfMonth(dt1).ToString();
                return shamsidate;

            }
            else
            {
                return "";

            }
         


        }
        public static DateTime ToMiladi(DateTime dt)
        {
            PersianCalendar pDate = new PersianCalendar();

            return pDate.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        public static string GetStringDate(DateTime dt)
        {
            DateTime DtShamsi = ToShamsi(dt);
            //DateTime DtShamsi = PersianDateConverter.ToPersianDate(dt);
            //DateTime DtShamsi = DateTime.Parse( dt.ToPersianDate());
            int Year = DtShamsi.Year;
            int Month = DtShamsi.Month;
            int Day = DtShamsi.Day;

            string[] Months = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "ابان", "اذر", "دی", "بهمن", "اسفند" };

            return Day + " " + Months[Month - 1] + " " + Year.ToString().Substring(2, 2);

        }

        public static string GetTimeStatestice(TimeSpan t)
        {
            string Time = "";
            //Time = t.Days + " روز و";
            //Time += t.Hours + " ساعت و ";
            Time += t.Minutes + " دقیقه و ";
            Time += t.Seconds + " ثانیه ";
            return Time;
        }

    

    public static DateTime PersianDateToGregorianDate(string pDate)
{
    var dateParts = pDate.Split(new[] { '/' }).Select(d => int.Parse(d)).ToArray();
    var hour = 0;
    var min = 0;
    var seconds = 0;
    return new DateTime(dateParts[0], dateParts[1], dateParts[2],
                        hour, min, seconds, new PersianCalendar());
}





    }
