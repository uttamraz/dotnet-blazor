using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DotNetBlazor.Server.Controllers;
using DotNetBlazor.Server.Services.AccountService;
using DotNetBlazor.Shared.Models.Account;
using DotNetBlazor.Server.Test.Helper;
using Xunit;

namespace DotNetBlazor.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AccountController _controller;
        private readonly RegistrationRequest _registrationRequest;

        public AccountControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);
            _registrationRequest = MockHelper.RegistrationRequestMock();

        }
        [Fact]
        public async Task Register_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            _accountServiceMock.Setup(service => service.RegisterUser(It.IsAny<RegistrationRequest>()))
                .ReturnsAsync(new RegistrationResponseData { IsSuccess = true });

            // Act
            var response = await _controller.Register(_registrationRequest);

            // Assert
            var result = Assert.IsType<OkObjectResult>(response);
            var responseData = Assert.IsType<RegistrationResponse>(result.Value);

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(StatusCodes.Status200OK, responseData.Response.Status);
            Assert.True(responseData.Data.IsSuccess);
        }

        [Fact]
        public async Task Register_InvalidRequest_ReturnsValidationFailedResult()
        {
            // Arrange
            _accountServiceMock.Setup(service => service.RegisterUser(It.IsAny<RegistrationRequest>()))
                .ReturnsAsync(new RegistrationResponseData { });

            // Act (Request Same Request Object Should Return Validation Error)
            var response = await _controller.Register(_registrationRequest);

            // Assert
            var result = Assert.IsType<UnprocessableEntityObjectResult>(response);
            var responseData = Assert.IsType<RegistrationResponse>(result.Value);

            Assert.Equal(StatusCodes.Status422UnprocessableEntity, result.StatusCode);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, responseData.Response.Status);
            Assert.NotNull(responseData.Data);
        }

        [Fact]
        public async Task Login_ValidInput_ReturnsSuccessResponse()
        {
            // Arrange
            var registrationServiceMock = new Mock<IAccountService>();
            registrationServiceMock.Setup(service => service.Login(It.IsAny<LoginRequest>()))
                .ReturnsAsync(new LoginResponseData { Token = "token" });

            var loginRequest = MockHelper.LoginRequest();

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsType<LoginResponse>(okResult.Value);

            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(StatusCodes.Status200OK, responseData.Response.Status);
            Assert.False(string.IsNullOrEmpty(responseData.Data.Token));
        }

        [Fact]
        public async Task Login_InvalidInput_ReturnsFailedResponse()
        {
            // Arrange
            _accountServiceMock.Setup(service => service.Login(It.IsAny<LoginRequest>()))
                .ReturnsAsync(new LoginResponseData { Token = "token" });

            var request = MockHelper.InvalidLoginRequest();

            // Act
            var response = await _controller.Login(request);

            // Assert
            var result = Assert.IsType<UnauthorizedObjectResult>(response);
            var responseData = Assert.IsType<LoginResponse>(result.Value);

            Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
            Assert.Equal(StatusCodes.Status401Unauthorized, responseData.Response.Status);
            Assert.True(string.IsNullOrEmpty(responseData.Data.Token));
        }
    }
}
