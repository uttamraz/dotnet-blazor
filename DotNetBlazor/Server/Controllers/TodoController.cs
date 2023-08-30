using DotNetBlazor.Server.Services.TodoService;
using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Todo;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBlazor.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(CreateTodoResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Create(CreateTodoRequest request)
        {
            var data = await _todoService.Create(request);
            if (data != null)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Bad Request!"));
        }

        [HttpPost("Update")]
        [ProducesResponseType(typeof(UpdateTodoResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Update(UpdateTodoRequest request)
        {
            var data = await _todoService.Update(request);
            if (data != null)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Password change failed!"));
        }

        [HttpPost("Delete")]
        [ProducesResponseType(typeof(DeleteTodoResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Delete(DeleteTodoRequest request)
        {
            var data = await _todoService.Delete(request);
            if (data != null && data.IsSuccess)
            {
                return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
            }
            return BadRequest(new CommonResponse(null, StatusCodes.Status400BadRequest, "Password change failed!"));
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(TodoListResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> List(TodoListRequest request)
        {
            var data = await _todoService.List(request);
            return Ok(new CommonResponse(data, StatusCodes.Status200OK, "Success!"));
        }
    }
}
