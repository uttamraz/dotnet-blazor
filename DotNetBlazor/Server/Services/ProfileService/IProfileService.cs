using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Server.Services.ProfileService
{
    public interface IProfileService
    {
        Task<UserDetailResponseData> UpdateProfile(UserUpdateRequest request);
        Task<UserDetailResponseData> GetProfile();
        Task<ChangePasswordResponseData> ChangePassword(ChangePasswordRequest request);
    }
}
