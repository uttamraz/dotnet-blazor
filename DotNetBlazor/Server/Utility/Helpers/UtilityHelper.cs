using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public static class UtilityHelper
    {
        public static Exception GetExceptionByStatusCode(int statusCode, string message)
        {
            switch (statusCode)
            {
                case 400:
                case 422:
                    return new ValidationException(message);
                case 401:
                    return new UnauthorizedAccessException(message);
                case 403:
                    return new AccessViolationException(message);
                case 404:
                    return new FileNotFoundException(message);
                case 500:
                    return new Exception(message);
                default:
                    return new Exception(message);
            }
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
    }
}
