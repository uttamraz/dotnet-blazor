using Azure.Core;
using DotNetBlazor.Server.Context;
using DotNetBlazor.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetBlazor.Server.Repository.RegistrationRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BaseDbContext context;
        public AccountRepository(BaseDbContext baseContext)
        {
            context = baseContext;
        }

        public async Task<User> LoginUser(User request)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        }

        public async Task<User> RegisterUser(User request)
        {
            await context.Users.AddAsync(request);
            await context.SaveChangesAsync();
            return request;
        }
    }
}
