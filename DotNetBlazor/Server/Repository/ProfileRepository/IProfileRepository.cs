using DotNetBlazor.Server.Entities;
using DotNetBlazor.Shared.Models.Profile;

namespace DotNetBlazor.Server.Repository.ProfileRepository
{
    public interface IProfileRepository
    {
        Task<User> UpdateUser(User request);
        Task<User> UserDetail(int userId);
        Task<User> UserDetail(string email);
        Task<UpdatePasswordResponseData> UpdatePassword(UpdatePasswordRequest request);
    }
}
