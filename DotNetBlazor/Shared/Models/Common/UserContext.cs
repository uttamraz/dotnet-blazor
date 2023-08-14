namespace DotNetBlazor.Shared.Models.Common
{
    public class UserContext
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
