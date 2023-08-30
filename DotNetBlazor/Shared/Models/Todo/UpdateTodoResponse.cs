using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Todo
{
    public class UpdateTodoResponse
    {
        public TodoDetail Data { get; set; }
        public Response Response { get; set; }
    }
}