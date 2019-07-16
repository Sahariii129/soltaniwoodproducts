using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
        }
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            //Debug.Assert(!string.IsNullOrEmpty(value));
            return (T)Enum.Parse(typeof(T), value, true);
        }


    }
}
