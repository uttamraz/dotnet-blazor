using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Server.Services.ProfileService
{
    public interface IProfileService
    {
        Task<UserDetailResponseData> UpdateUser(UserUpdateRequest request);
        Task<UserDetailResponseData> UserDetail();
        Task<ChangePasswordResponseData> ChangePassword(ChangePasswordRequest request);
    }
}
