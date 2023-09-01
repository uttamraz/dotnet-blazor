
namespace DotNetBlazor.Shared.Models.Common
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPage()
        {
            return (int)Math.Ceiling((double)Total / PerPage);
        }
    }
}
