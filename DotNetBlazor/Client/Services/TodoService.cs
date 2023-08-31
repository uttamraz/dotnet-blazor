using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Client.Services
{

    public interface ITodoService
    {
        Task<CreateTodoResponse> Create(CreateTodoRequest request);
        Task<UpdateTodoResponse> Update(UpdateTodoRequest request);
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

        public async Task<CreateTodoResponse> Create(CreateTodoRequest request)
        {
            return await _apiHelper.Post<CreateTodoResponse>("api/v1/todo/Create", request);
        }

        public async Task<DeleteTodoResponse> Delete(DeleteTodoRequest request)
        {
            return await _apiHelper.Post<DeleteTodoResponse>("api/v1/todo/Delete", request);
        }

        public async Task<TodoListResponse> List(TodoListRequest request)
        {
            var list = await _apiHelper.Get<TodoListResponse>("api/v1/todo/List");
            return list;
        }

        public async Task<UpdateTodoResponse> Update(UpdateTodoRequest request)
        {
            return await _apiHelper.Post<UpdateTodoResponse>("api/v1/todo/Update", request);
        }
    }
}
