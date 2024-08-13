// Ignore Spelling: Cli

using System.ComponentModel;
using System.Reflection;

namespace K.AudioConverter.Cli.Extensions
{
    public static class EnumExtensions
    {
        public static string Description(this Enum @enum)
        {
            if (@enum == null)
            {
                return string.Empty;
            }

            var fieldInfo = @enum.GetType().GetField(@enum.ToString());
            if (fieldInfo == null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : @enum.ToString();
        }

        public static T GetEnumValueFromDescription<T>(this string description) where T : Enum
        {
            var type = typeof(T);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && attribute.Description == description)
                {
                    return (T)field.GetValue(null)!;
                }
            }
            throw new ArgumentException($"No enum value with description '{description}' found", nameof(description));
        }
    }
}
