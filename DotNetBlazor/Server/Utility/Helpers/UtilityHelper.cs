using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public static class UtilityHelper
    {
        public static Exception GetExceptionByStatusCode(int statusCode, string message)
        {
            return statusCode switch
            {
                400 or 422 => new ValidationException(message),
                401 => new UnauthorizedAccessException(message),
                403 => new AccessViolationException(message),
                404 => new FileNotFoundException(message),
                500 => new Exception(message),
                _ => new Exception(message),
            };
        }

        public static string MaskString(string str, int maskChar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.Length > maskChar ? string.Concat(str.AsSpan(0, str.Length - maskChar), new string('*', maskChar)) : new string('*', maskChar);
        }
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
