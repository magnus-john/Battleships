using System.ComponentModel;

namespace Battleships.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var defaultValue = $"{enumValue}";
            var field = enumValue.GetType().GetField(defaultValue);

            if (field == null)
                return defaultValue;

            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                return attribute.Description;

            return defaultValue;
        }
    }
}
