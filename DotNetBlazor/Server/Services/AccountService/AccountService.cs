using DotNetBlazor.Server.Entities;
using DotNetBlazor.Server.Repository.RegistrationRepository;
using DotNetBlazor.Server.Utility.Helpers;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace DotNetBlazor.Server.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IServiceProvider _serviceProvider;

        public AccountService(IAccountRepository accountRepository, IJwtUtils jwtUtils, IServiceProvider serviceProvider)
        {
            _accountRepository = accountRepository;
            _jwtUtils = jwtUtils;
            _serviceProvider = serviceProvider;
        }

        public async Task<LoginResponseData> Login(LoginRequest request)
        {
            var response = new LoginResponseData();
            // Get User
            var user = await _accountRepository.LoginUser(new User { Email = request.Email });
            if (user == null)
            {
                throw new ValidationException("Invalid username or password!");
            }
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
            response.Token = jwtToken.Token;
            return response;
        }

        public async Task<RegistrationResponseData> RegisterUser(RegistrationRequest request)
        {
            var response = new RegistrationResponseData();
            var userRequest = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PasswordChangeDate = DateTime.Now
            };

            ValidationHelper.Validate(userRequest, _serviceProvider);

            var user = await _accountRepository.RegisterUser(userRequest);
            if (user == null)
            {
                throw new ValidationException("Registration failed!");
            }
            response.IsSuccess = true;
            return response;
        }
    }
}