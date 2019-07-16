using System;
using System.Globalization;

namespace SoltaniWeb.Models.Extensions
{
    public static class PersianDateUtils
    {
        public static string ToPersianDate (this DateTime date)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day, new GregorianCalendar());
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(dateTime) + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00");
        }
    }
}