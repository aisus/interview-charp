using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Team.Extensions.Utility
{
    public static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var member = type.GetMember(enumVal.ToString());
            var attributes = member[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }

        public static string GetDescription(this Enum enumVal)
        {
            return enumVal.GetAttribute<DescriptionAttribute>().Description;
        }

        public static string GetMemberName(this Enum enumVal)
        {
            return enumVal.GetAttribute<EnumMemberAttribute>()?.Value ?? enumVal.ToString();
        }

        public static string ToUpperSnakeCase(this Enum enumVal)
        {
            return string.Concat(enumVal
                .ToString()
                .Select((x, i) => i > 0 && char.IsUpper(x)
                    ? "_" + x.ToString()
                    : char.ToUpper(x).ToString()));
        }
    }
}