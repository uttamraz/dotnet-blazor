using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class UserUpdateRequest
    {
        [Required, MaxLength(100), MinLength(2)]
        public string FullName { get; set; } = null!;
        [Required, MaxLength(50), MinLength(6), EmailAddress]
        public string Email { get; set; } = null!;
        [Required, MaxLength(20), MinLength(8)]
        public string Mobile { get; set; } = null!;
        [Required]
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentAddress { get; set; }
        public string? DocumentUrl { get; set; }
    }
}
