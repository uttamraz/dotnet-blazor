using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Todo
{
    public class DeleteTodoResponse
    {
        public DeleteTodoResponseData Data { get; set; }
        public Response Response { get; set; }
    }

    public class DeleteTodoResponseData
    {
        public bool IsSuccess { get; set; }
    }
}