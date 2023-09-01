using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Todo
{
    public class TodoListRequest : ListRequest
    {
        public TodoFilter? Filter { get; set; }
    }

    public class TodoFilter
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}