using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class UserDetailResponse
    {
        public UserDetail Data { get; set; }
        public Response Response { get; set; }
    }

    public class UserDetail
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Mobile { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentAddress { get; set; }
        public string? DocumentUrl { get; set; }
    }
}