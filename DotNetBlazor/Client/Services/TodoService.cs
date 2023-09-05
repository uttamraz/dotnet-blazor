using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Client.Services
{

    public interface ITodoService
    {
        Task<TodoResponse> Create(TodoRequest request);
        Task<TodoResponse> Update(TodoRequest request);
        Task<DeleteTodoResponse> Delete(DeleteTodoRequest request);
        Task<TodoListResponse> List(TodoListRequest request);
    }

    public class TodoService : ITodoService
    {
        private readonly IApiHelper _apiHelper;
        private readonly IEventHelper _eventHelper;

        public TodoService(IApiHelper apiHelper, IEventHelper eventHelper)
        {
            _apiHelper = apiHelper;
            _eventHelper = eventHelper;
        }

        public async Task<TodoResponse> Create(TodoRequest request)
        {
            return await _apiHelper.Post<TodoResponse>("api/v1/todo/Create", request);
        }

        public async Task<DeleteTodoResponse> Delete(DeleteTodoRequest request)
        {
            return await _apiHelper.Post<DeleteTodoResponse>("api/v1/todo/Delete", request);
        }

        public async Task<TodoListResponse> List(TodoListRequest request)
        {
            var list = await _apiHelper.Get<TodoListResponse>("api/v1/todo/List", request);
            return list;
        }

        public async Task<TodoResponse> Update(TodoRequest request)
        {
            return await _apiHelper.Post<TodoResponse>("api/v1/todo/Update", request);
        }
    }
}
