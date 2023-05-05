using System;
using System.Collections;
using System.Linq;

namespace Common.Utilities
{
    /// <summary>
    /// کلاس برای اعتبارسنجی پارامترهای ورودی متدها
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// اگر پارامتر ورودی Null بود یه NullExeption صادر بشه
        /// </summary>
        public static void NotNull<T>(T obj, string name, string message = null)
            where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}" , message);
        }

        public static void NotNull<T>(T? obj, string name, string message = null)
            where T : struct
        {
            if (!obj.HasValue)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);

        }
        /// <summary>
        /// اگر string هست تهی نباشه
        /// اگر لیست هست حداقل یه ایتم داخلش باشه
        /// </summary>
        public static void NotEmpty<T>(T obj, string name, string message = null, T defaultValue = null)
            where T : class
        {
            if (obj == defaultValue
                || (obj is string str && string.IsNullOrWhiteSpace(str))
                || (obj is IEnumerable list && !list.Cast<object>().Any()))
            {
                throw new ArgumentException("Argument is empty : " + message, $"{name} : {typeof(T)}");
            }
        }
    }
}
