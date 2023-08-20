using DotNetBlazor.Client.Utility;
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
        private readonly IEventHelper _eventHelper;

        public ProfileService(IApiHelper apiHelper, IEventHelper eventHelper)
        {
            _apiHelper = apiHelper;
            _eventHelper = eventHelper;
        }

        public async Task<UserDetailResponse> UpdateProfile(UserUpdateRequest request)
        {
            return await _apiHelper.Post<UserDetailResponse>("api/v1/profile/UpdateProfile", request);
        }

        public async Task<UserDetailResponse> GetProfile()
        {
            var user = await _apiHelper.Get<UserDetailResponse>("api/v1/profile/GetProfile");
            _eventHelper.SetProfileProgress(user.Data);
            return user;
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            return await _apiHelper.Post<ChangePasswordResponse>("api/v1/profile/ChangePassword", request);
        }
    }
}
