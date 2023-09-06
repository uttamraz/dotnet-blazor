using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetBlazor.Server.Entities.Validation;
using System.Reflection;

namespace DotNetBlazor.Server.Entities
{
    public partial class Todo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required, MaxLength(150), MinLength(2)]
        public string Name { get; set; } = null!;
        [Required, MaxLength(500), MinLength(2)]
        public string Description { get; set; } = null!;
        [Required, Range(0, 100)]
        public int Progress { get; set; }
        [Required, MaxLength(15), MinLength(2)]
        public string Status { get; set; } = null!; // To Do, In Progress, Done
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}