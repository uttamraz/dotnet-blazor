using DotNetBlazor.Client.Utility;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Client.Services
{

    public interface IProfieService : IDisposable
    {
        Task<UserDetailResponse> UpdateProfile(UserUpdateRequest request);
        Task<UserDetailResponse> GetProfile();
        Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);

        event Action<List<Error>> ValidationError;
    }

    public class ProfileService : IProfieService
    {
        private readonly IApiHelper _apiHelper;
        private readonly ICacheHelper _cacheHelper;
        public event Action<List<Error>>? ValidationError;

        public ProfileService(IApiHelper apiHelper, ICacheHelper cacheHelper)
        {
            _apiHelper = apiHelper;
            _cacheHelper = cacheHelper;
            _apiHelper.ValidationError += SetValidationError;
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

        private void SetValidationError(List<Error> list)
        {
            ValidationError?.Invoke(list);
        }

        public void Dispose()
        {
            _apiHelper.ValidationError -= SetValidationError;
        }
    }
}
