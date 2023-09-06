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
                data.Status = request.Status;
                data.Progress = request.Progress;
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
            var query = context.Todo.AsQueryable();
            // Apply filters if provided
            if (request.Filter != null)
            {
                query = query.Where(item =>
                    item.UserId == request.Filter.UserId &&
                    (string.IsNullOrWhiteSpace(request.Filter.Name) || item.Name.Contains(request.Filter.Name)) &&
                    (string.IsNullOrWhiteSpace(request.Filter.Description) || item.Description.Contains(request.Filter.Description)) &&
                    (string.IsNullOrWhiteSpace(request.Filter.Status) || item.Status == request.Filter.Status));
            }


            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                query = query.Where(item =>
                    item.Name.Contains(request.Query) ||
                    item.Description.Contains(request.Query) ||
                    item.Status == request.Query);
            }

            // Get the total count before pagination
            int totalCount = await query.CountAsync();
            // Apply pagination
            query = query.Skip(request.Skip()).Take(request.PerPage);
            var data = await query.ToListAsync();
            if (data != null)
            {
                response.List = data.Select(item => item.Map<TodoDetail>()).ToList();
                response.Pagination = new Pagination
                {
                    Page = request.Page,
                    PerPage = request.PerPage,
                    Total = totalCount
                };
            }
            return response;
        }
    }
}
