using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Server.Services.TodoService
{
    public interface ITodoService
    {
        Task<TodoDetail> Create(TodoRequest request);
        Task<TodoDetail> Update(TodoRequest request);
        Task<DeleteTodoResponseData> Delete(DeleteTodoRequest request);
        Task<TodoListData> List(TodoListRequest request);
    }
}