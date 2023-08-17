using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Repository.ProfileRepository;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Profile;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
        public async Task<UserDetailResponseData> UpdateProfile(UserUpdateRequest request)
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

            var user = await _profileRepository.UpdateProfile(userRequest);
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

        public async Task<UserDetailResponseData> GetProfile()
        {
            int id = _contextHelper.Id();
            var user = await _profileRepository.GetProfile(id);
            if (user == null)
            {
                throw new ValidationException("Profile not found!");
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

        public async Task<ChangePasswordResponseData> ChangePassword(ChangePasswordRequest request)
        {
            int id = _contextHelper.Id();
            var user = await _profileRepository.GetProfile(id);
            if (user == null)
            {
                throw new ValidationException("Profile not found!");
            }
            // Validate Password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new ValidationException("Invalid Old Password!");
            }
            var userRequest = new UpdatePasswordRequest
            {
                UserId = id,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            var resp = await _profileRepository.UpdatePassword(userRequest);
            return new ChangePasswordResponseData
            {
                IsSuccess = resp.IsSuccess
            };
        }
    }
}