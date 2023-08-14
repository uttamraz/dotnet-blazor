using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Repository.ProfileRepository;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Profile;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Server.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IContextHelper _contextHelper;


        public ProfileService(IProfileRepository profileRepository, IContextHelper contextHelper)

        {
            _profileRepository = profileRepository;
            _contextHelper = contextHelper;
        }
        public async Task<UserDetailResponseData> UpdateUser(UserUpdateRequest request)
        {
            var userRequest = new User
            {
                Id = _contextHelper.Id(),
                FullName = request.FullName,
                Mobile = request.Mobile,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                CurrentAddress = request.CurrentAddress,
                DocumentUrl = request.DocumentUrl,
                UpdatedDate = DateTime.Now
            };

            var user = await _profileRepository.UpdateUser(userRequest);
            if (user == null)
            {
                throw new ValidationException("Profile update failed!");
            }
            return new UserDetailResponseData
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.Mobile,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                CurrentAddress = user.CurrentAddress,
                DocumentUrl = user.DocumentUrl
            };
        }

        public async Task<UserDetailResponseData> UserDetail()
        {
            int id = _contextHelper.Id();
            var user = await _profileRepository.UserDetail(id);
            if (user == null)
            {
                throw new ValidationException("User not found!");
            }
            return new UserDetailResponseData
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.Mobile,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                CurrentAddress = user.CurrentAddress,
                DocumentUrl = user.DocumentUrl
            };
        }

        public Task<ChangePasswordResponseData> ChangePassword(ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}