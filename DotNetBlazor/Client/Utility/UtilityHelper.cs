
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace DotNetBlazor.Client.Utility
{
    public static class UtilityHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            throw new ArgumentException("Invalid expression");
        }

        public static object? Prop(this object obj, string propertyName)
        {
            PropertyInfo? property = obj.GetType().GetProperty(propertyName);
            return property?.GetValue(obj, null);
        }

        public static string GetDisplayName(this PropertyInfo property)
        {
            var displayAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
            return displayAttribute != null ? displayAttribute.DisplayName : property.Name;
        }
    }
}