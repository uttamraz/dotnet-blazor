using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class ResetPasswordRequest
    {

        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
        [Required, MinLength(6), MaxLength(32)]
        public string Password { get; set; } = null!;
    }
}
