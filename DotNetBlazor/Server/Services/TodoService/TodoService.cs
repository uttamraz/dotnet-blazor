using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Repository.TodoRepository;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Todo;

namespace DotNetBlazor.Server.Services.TodoService
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IContextHelper _contextHelper;

        public TodoService(ITodoRepository todoRepository, IContextHelper contextHelper)

        {
            _todoRepository = todoRepository;
            _contextHelper = contextHelper;
        }

        public async Task<TodoDetail> Create(CreateTodoRequest request)
        {
            var todo = request.Map<Todo>();
            todo.UserId = _contextHelper.Id();
            return await _todoRepository.Create(todo);
        }

        public async Task<DeleteTodoResponseData> Delete(DeleteTodoRequest request)
        {
            return await _todoRepository.Delete(request);
        }

        public async Task<TodoListData> List(TodoListRequest request)
        {
            return await _todoRepository.List(request);
        }

        public async Task<TodoDetail> Update(UpdateTodoRequest request)
        {
            var todo = request.Map<Todo>();
            todo.UserId = _contextHelper.Id();
            return await _todoRepository.Update(todo);
        }
    }
}