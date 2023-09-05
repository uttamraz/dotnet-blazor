using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Shared.Models.Todo
{

    public class TodoRequest
    {
        public int Id { get; set; }
        [Required, MaxLength(150), MinLength(2)]
        public string Name { get; set; } = null!;
        [Required, MaxLength(500), MinLength(2)]
        public string Description { get; set; } = null!;
        public int Progress { get; set; }
        [Required, MaxLength(15), MinLength(2)]
        public string Status { get; set; } = null!; // To Do, In Progress, Done
    }
}