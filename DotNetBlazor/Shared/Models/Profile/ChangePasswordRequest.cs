using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class ChangePasswordRequest
    {
        [Required, MinLength(4), MaxLength(10)]
        public string Password { get; set; } = null!;
        [Required, MinLength(4), MaxLength(10)]
        public string NewPassword { get; set; } = null!;
        [Required, MinLength(4), MaxLength(10), Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
