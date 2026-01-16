using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ultitity.Extensions
{
    public static class Enums
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())[0]
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? enumValue.ToString();
        }
    }
}
