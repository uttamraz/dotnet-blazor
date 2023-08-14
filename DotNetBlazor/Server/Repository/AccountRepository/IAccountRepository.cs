using DotNetBlazor.Server.Entities;

namespace DotNetBlazor.Server.Repository.RegistrationRepository
{
    public interface IAccountRepository
    {
        Task<User> RegisterUser(User request);
        Task<User> LoginUser(User request);
    }
}
