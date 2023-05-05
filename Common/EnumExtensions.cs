using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    public static class EnumExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        //public static string GetDisplayName(this Enum enu)
        //{
        //    var attr = GetDisplayAttribute(enu);
        //    return attr != null ? attr.Name : enu.ToString();
        //}
        public static string GetDisplayName(this Enum value)
        {
            //var v = value.GetType().GetField(value.ToString());
            //var attribute = (DisplayNameAttribute)v
            //       .GetCustomAttributes(false)
            //       .Where(a => a is DisplayNameAttribute)
            //       .FirstOrDefault();
            if (value != null)
            {
                var attr = GetDisplayAttribute(value);
                return attr != null ? attr.Name : value.ToString();
                //var attribute = (DisplayNameAttribute)value.GetType()
                //    .GetField(value.ToString())
                //    .GetCustomAttributes(false)
                //    .Where(a => a is DisplayNameAttribute)
                //    .FirstOrDefault();
                //return attribute != null ? attribute.DisplayName : "";
            }
            else
                return "";
        }
        public static string GetDescription(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr != null ? attr.Description : enu.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString());
            return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
        }
    }
}