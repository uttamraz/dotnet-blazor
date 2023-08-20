using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Server.Services.ProfileService
{
    public interface IProfileService
    {
        Task<UserDetail> UpdateProfile(UserUpdateRequest request);
        Task<UserDetail> GetProfile();
        Task<ChangePasswordResponseData> ChangePassword(ChangePasswordRequest request);
    }
}
