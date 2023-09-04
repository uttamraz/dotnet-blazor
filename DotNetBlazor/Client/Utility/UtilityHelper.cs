
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
        public static string GetDisplayName(this object obj, string propertyName)
        {
            PropertyInfo? property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
                if (displayNameAttribute != null)
                {
                    return displayNameAttribute.DisplayName;
                }
            }
            return propertyName;
        }
        public static TDestination Map<TDestination>(this object? source) where TDestination : new()
        {
            var destination = new TDestination();
            if (source != null)
            {
                PropertyInfo[] sourceProperties = source.GetType().GetProperties();
                PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties();

                foreach (var sourceProperty in sourceProperties)
                {
                    var matchingDestinationProperty = Array.Find(destinationProperties, prop =>
                        prop.Name == sourceProperty.Name && prop.PropertyType == sourceProperty.PropertyType);

                    if (matchingDestinationProperty != null)
                    {
                        var value = sourceProperty.GetValue(source);
                        matchingDestinationProperty.SetValue(destination, value);
                    }
                }
            }
            return destination;
        }
    }
}