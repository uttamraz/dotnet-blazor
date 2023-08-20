using DotNetBlazor.Server.Context;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Server.Entities.Validation
{
    public class CheckUniqueUserAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string idKey = "id";
            int idValue = 0;
            var property = validationContext.ObjectType.GetProperty(idKey);
            if (validationContext.ObjectType.GetProperty(idKey) != null)
            {
                idValue = Convert.ToInt32(property?.GetValue(validationContext?.ObjectInstance, null));
            }
            var _context = (BaseDbContext)validationContext?.GetService(typeof(BaseDbContext));
            var entity = new User();
            entity = (idValue != 0) ?
                 _context?.Set<User>().AsQueryable().FirstOrDefault(x => x.Id != idValue && x[validationContext.MemberName] == value)
                : _context?.Set<User>().AsQueryable().FirstOrDefault(x => x[validationContext.MemberName] == value);

            if (entity != null)
            {
                string errorMessage = $"{validationContext?.DisplayName} already exists!";
                return new ValidationResult(errorMessage, new[] { validationContext.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}