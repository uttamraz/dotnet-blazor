namespace DotNetBlazor.Shared.Models.Profile
{
    public class UpdatePasswordRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; } = null!;
    }
}
