using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Common.Utilities
{
    public static class EnumExtensions
    {
        //var seasonDisplayName = Season.GetAttribute<DisplayAttribute>();
        //Console.WriteLine("Which season is it?");
        //Console.WriteLine(seasonDisplayName.Name);
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
        public static string GetDisplayName_Razor(this Enum enumValue)
        {
            return enumValue != null ? enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName() : "";
        }
        public static string GetDisplayName_Razor(this Enum enumValue, Type enumtype)
        {
            if (enumtype == null)
                return "";
            else
            {
                var attributes = enumtype.GetMember(enumValue.ToString())[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                return ((DisplayAttribute)attributes[0]).Name;
            }
        }
        public static string GetDisplayName(this string enumValue, Type enumtype)
        {
            if (enumtype == null)
                return "";
            else
            {
                var attributes = enumtype.GetMember(enumValue)[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                return ((DisplayAttribute)attributes[0]).Name;
            }
        }
        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }
        /// <summary>
        /// برای نمایش مقادار نمایش یک Enum
        /// </summary>
        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }
        /// <summary>
        /// نمایش مقدار نمایشی و عددی یک Enum
        /// </summary>
        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
