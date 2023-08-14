using System.Drawing.Printing;

namespace DotNetBlazor.Shared.Models.Common
{
    public class QueryFilter
    {
        private int perPage = 10;
        private Dictionary<string, string> filter = new();
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
        public Dictionary<string, string> Filter
        {
            get { return filter; }
            set { filter = value ?? filter; }
        }
        public Dictionary<string, string>? Order { get; set; }
    }
}
