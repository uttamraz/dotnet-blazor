using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; } = null!;
        [Required, MinLength(4), MaxLength(10), DisplayName("New Password")]
        public string NewPassword { get; set; } = null!;
        [Required, MinLength(4), MaxLength(10), Compare("NewPassword"), DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
