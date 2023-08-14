
namespace DotNetBlazor.Shared.Models.Common
{
    public class ListRequest
    {
        private int perPage = 10;
        public int Page { get; set; } = 1;
        public int PerPage
        {
            get { return perPage == -1 ? 1000000 : perPage; }
            set { perPage = value; }
        }
        public int Skip
        {
            get { return perPage == -1 ? 0 : (Page - 1) * perPage; }
        }
    }
}
