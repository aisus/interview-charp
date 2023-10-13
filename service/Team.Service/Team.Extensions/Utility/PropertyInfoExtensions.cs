using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Team.Extensions.Utility
{
    public static class PropertyInfoExtensions
    {
        public static string GetDescription(this PropertyInfo prop)
        {
            return prop.GetCustomAttribute<DescriptionAttribute>()?.Description;
        }

        public static (string Name, string Description, int? Order) GetDisplayData(this PropertyInfo prop)
        {
            var data = prop.GetCustomAttribute<DisplayAttribute>();
            return (Name: data.GetName(), Description: data.GetDescription(), Order: data.GetOrder());
        }
    }
}