using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Repository.RegistrationRepository;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Server.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _cache;

        public AccountService(IAccountRepository accountRepository, IJwtUtils jwtUtils, IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _accountRepository = accountRepository;
            _jwtUtils = jwtUtils;
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        public async Task<LoginResponseData> Login(LoginRequest request)
        {
            var response = new LoginResponseData();
            // Get User
            var user = await _accountRepository.LoginUser(new User { Email = request.Email }) ?? throw new ValidationException("Invalid username or password!");
            // Validate Password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new ValidationException("Invalid username or password!");
            }
            // Create Auth TOken
            var auth = new AuthUser
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };

            var jwtToken = _jwtUtils.GenerateJwtToken(auth);
            if (!string.IsNullOrEmpty(jwtToken.Token))
            {
                response.Token = jwtToken.Token;
                await _cache.SetRecordAsync(jwtToken.Token, auth, jwtToken.ExpiryTime.Subtract(DateTime.Now));
            }
            return response;
        }

        public async Task<RegistrationResponseData> RegisterUser(RegistrationRequest request)
        {
            var response = new RegistrationResponseData();
            var userRequest = new User
            {
                FullName = request.FullName,
                Mobile = request.Mobile,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PasswordChangeDate = DateTime.Now
            };

            await Task.Run(() => _serviceProvider.Validate(userRequest));

            var user = await _accountRepository.RegisterUser(userRequest) ?? throw new ValidationException("Registration failed!");
            response.IsSuccess = true;
            return response;
        }
    }
}