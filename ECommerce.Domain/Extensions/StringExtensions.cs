using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerce.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string GetDisplay<T>(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var type = typeof(T);
            var field = type.GetField(text);
            if (field == null) throw new InvalidOperationException($"Не удалось получить данные по атрибуту Display");

            return field.GetCustomAttribute<DisplayAttribute>()?.Name ?? string.Empty;
        }
    }
}
