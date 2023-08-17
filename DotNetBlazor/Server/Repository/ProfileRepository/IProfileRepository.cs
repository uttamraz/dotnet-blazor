using DotNetBlazor.Server.Entities;
using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Server.Repository.ProfileRepository
{
    public interface IProfileRepository
    {
        Task<User> UpdateProfile(User request);
        Task<User> GetProfile(int userId);
        Task<User> UserDetail(string email);
        Task<UpdatePasswordResponseData> UpdatePassword(UpdatePasswordRequest request);
    }
}
