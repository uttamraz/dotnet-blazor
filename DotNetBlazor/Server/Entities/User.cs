using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetBlazor.Server.Entities.Validation;
using System.Reflection;

namespace DotNetBlazor.Server.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Mobile), IsUnique = true)]
    public partial class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50), MinLength(6), EmailAddress, CheckUniqueUser]
        public string Email { get; set; } = null!;
        [Required, DataType(DataType.Password), MaxLength(500), MinLength(6)]
        public string Password { get; set; } = null!;
        [MaxLength(100), MinLength(2)]
        public string FullName { get; set; } = null!;
        [MaxLength(20), MinLength(8), CheckUniqueUser]
        public string Mobile { get; set; } = null!;
        [MaxLength(10), MinLength(1)]
        public string? Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(100), MinLength(2)]
        public string? CurrentAddress { get; set; }
        [DataType(DataType.Url), MaxLength(1000)]
        public string? DocumentUrl { get; set; }
        public short Status { get; set; } = 1;
        public DateTime? PasswordChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = false;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }
    }
}