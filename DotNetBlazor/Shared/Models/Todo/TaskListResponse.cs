using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Todo
{

    public class TodoListResponse
    {
        public TodoListData Data { get; set; }
        public Response Response { get; set; }
    }


    public class TodoListData
    {
        public List<TodoDetail> List { get; set; }
        public Pagination Pagination { get; set; }
    }


    public class TodoDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Progress { get; set; }
        public string Status { get; set; } = null!;
        [DisplayName("Create Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
    }
}