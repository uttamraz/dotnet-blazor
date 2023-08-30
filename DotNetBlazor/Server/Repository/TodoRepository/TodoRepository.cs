using DotNetBlazor.Server.Context;
using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Todo;
using Microsoft.EntityFrameworkCore;

namespace DotNetBlazor.Server.Repository.TodoRepository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly BaseDbContext context;
        public TodoRepository(BaseDbContext baseContext)
        {
            context = baseContext;
        }

        public async Task<TodoDetail> Create(Todo request)
        {
            var response = new TodoDetail();
            await context.Todo.AddAsync(request);
            await context.SaveChangesAsync();
            // AutoMapper
            return request.Map<TodoDetail>();
        }

        public async Task<TodoDetail> Update(Todo request)
        {
            var response = new TodoDetail();
            var data = await context.Todo.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (data != null)
            {
                data.Name = request.Name;
                data.Description = request.Description;
                data.LastModifiedDate = DateTime.Now;
                await context.SaveChangesAsync();
                // AutoMapper
                return data.Map<TodoDetail>();
            }
            return response;
        }

        public async Task<DeleteTodoResponseData> Delete(DeleteTodoRequest request)
        {
            var response = new DeleteTodoResponseData();
            var data = await context.Todo.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (data != null)
            {
                context.Todo.Remove(data);
                await context.SaveChangesAsync();
                response.IsSuccess = true;
            }
            return response;
        }

        public async Task<TodoListData> List(TodoListRequest request)
        {
            var response = new TodoListData();
            var data = await context.Todo.ToListAsync();
            if (data != null)
            {
                // Assuming TodoListData has a property to hold the list of Todo items
                response.List = data.Select(item => item.Map<TodoDetail>()).ToList();
                response.pagination = new Pagination
                {
                    Page = 1,
                    PerPage = 1000,
                    Total = response.List.Count()
                };
            }
            return response;
        }
    }
}
