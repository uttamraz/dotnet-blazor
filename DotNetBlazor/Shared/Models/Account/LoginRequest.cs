
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Account
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
