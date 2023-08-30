using System.ComponentModel.DataAnnotations;
using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Todo
{
    public class UpdateTodoRequest
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required, MaxLength(150), MinLength(2)]
        public string Name { get; set; } = null!;
        [Required, MaxLength(500), MinLength(2)]
        public string Description { get; set; } = null!;
        [Required, MaxLength(15), MinLength(2)]
        public string Status { get; set; } = null!; // To Do, In Progress, Done
    }
}