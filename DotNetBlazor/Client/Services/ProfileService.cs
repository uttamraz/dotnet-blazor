using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Client.Services
{

    public interface IProfieService
    {
        Task<UserDetailResponse> UpdateProfile(UserUpdateRequest request);
        Task<UserDetailResponse> GetProfile();
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
    }

    public class ProfileService : IProfieService
    {
        private readonly IApiHelper _apiHelper;

        public ProfileService(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<UserDetailResponse> UpdateProfile(UserUpdateRequest request)
        {
            return await _apiHelper.Post<UserDetailResponse>("api/v1/profile/UpdateProfile", request);
        }

        public async Task<UserDetailResponse> GetProfile()
        {
            return await _apiHelper.Get<UserDetailResponse>("api/v1/profile/GetProfile");
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            return await _apiHelper.Get<ChangePasswordResponse>("api/v1/profile/ChangePassword");
        }
    }
}
