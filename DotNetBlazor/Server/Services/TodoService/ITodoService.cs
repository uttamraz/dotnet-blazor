using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Server.Services.TodoService
{
    public interface ITodoService
    {
        Task<TodoDetail> Create(CreateTodoRequest request);
        Task<TodoDetail> Update(UpdateTodoRequest request);
        Task<DeleteTodoResponseData> Delete(DeleteTodoRequest request);
        Task<TodoListData> List(TodoListRequest request);
    }
}