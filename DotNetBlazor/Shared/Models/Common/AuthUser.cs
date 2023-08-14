namespace DotNetBlazor.Shared.Models.Common
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? Email { get; set; }
    }
}
