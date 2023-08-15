using DotNetBlazor.Shared.Models.Common;

namespace DotNetBlazor.Shared.Models.Profile
{
    public class UserDetailResponse
    {
        public UserDetailResponseData Data { get; set; }
        public Response Response { get; set; }
    }

    public class UserDetailResponseData
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentAddress { get; set; }
        public string? DocumentUrl { get; set; }
    }
}