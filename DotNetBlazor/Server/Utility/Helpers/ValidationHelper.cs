using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public static class ValidationHelper
    {
        public static void Validate(this IServiceProvider serviceProvider, object instance)
        {
            var ctx = new ValidationContext(instance, serviceProvider, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(instance, ctx, errors, true);
            if (!isValid)
            {
                var list = new List<ValidationError>();
                foreach (var error in errors)
                {
                    list.Add(new ValidationError(error?.MemberNames?.FirstOrDefault(), error?.ErrorMessage));
                }
                throw new CustomValidationException("Validation Error!", list);
            }
        }
    }
}
