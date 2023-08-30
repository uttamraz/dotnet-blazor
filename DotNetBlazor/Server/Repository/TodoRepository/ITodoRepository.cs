using DotNetBlazor.Server.Entities;
using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Server.Repository.TodoRepository
{
    public interface ITodoRepository
    {
        Task<TodoDetail> Create(Todo request);
        Task<TodoDetail> Update(Todo request);
        Task<DeleteTodoResponseData> Delete(DeleteTodoRequest request);
        Task<TodoListData> List(TodoListRequest request);
    }
}
