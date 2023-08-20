using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class UserUpdateRequest
    {
        [MaxLength(100), MinLength(2)]
        public string FullName { get; set; } = null!;
        [MaxLength(20), MinLength(8)]
        public string Mobile { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentAddress { get; set; }
        public string? DocumentUrl { get; set; }
    }
}
