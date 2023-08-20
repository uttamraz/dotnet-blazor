using DotNetBlazor.Server.Context;
using DotNetBlazor.Server.Entities;
using DotNetBlazor.Shared.Models.Profile;
using Microsoft.EntityFrameworkCore;

namespace DotNetBlazor.Server.Repository.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly BaseDbContext context;
        public ProfileRepository(BaseDbContext baseContext)
        {
            context = baseContext;
        }

        public async Task<User> UpdateProfile(User request)
        {
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (data != null)
            {
                data.Gender = request.Gender;
                data.DateOfBirth = request.DateOfBirth;
                data.CurrentAddress = request.CurrentAddress;
                data.DocumentUrl = request.DocumentUrl;
                data.UpdatedDate = DateTime.Now;

                //using var transaction = await context.Database.BeginTransactionAsync();
                //try
                //{
                await context.SaveChangesAsync();
                //}
                //catch (Exception)
                //{
                //    await transaction.RollbackAsync();
                //    throw;
                //}
                return data;
            }
            return request;
        }

        public async Task<User?> GetProfile(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User?> UserDetail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<UpdatePasswordResponseData> UpdatePassword(UpdatePasswordRequest request)
        {
            var response = new UpdatePasswordResponseData();
            var data = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (data != null)
            {
                data.Password = request.Password;
                data.PasswordChangeDate = DateTime.Now;
                await context.SaveChangesAsync();
                response.IsSuccess = true;
            }
            return response;
        }
    }
}
