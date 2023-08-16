using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Account
{
    public class RegistrationRequest
    {
        [Required, DisplayName("Full Name"), MaxLength(15), MinLength(6)]
        public string FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, MaxLength(32), MinLength(6)]
        public string Password { get; set; } = null!;
    }
}