using Bogus;
using DotNetBlazor.Shared.Models.Account;

namespace DotNetBlazor.Server.Test.Helper
{
    public static class MockHelper
    {
        public static RegistrationRequest RegistrationRequestMock()
        {
            var faker = new Faker<RegistrationRequest>()
                .RuleFor(r => r.FullName, f => f.Name.FullName())
                .RuleFor(r => r.Email, f => f.Internet.Email())
                .RuleFor(r => r.Mobile, f => f.Phone.PhoneNumber())
                .RuleFor(r => r.Password, f => f.Random.Replace("??????#?#?#?#?#?#?#?#?#?#?#?#?#?#?#?#?#"));

            return faker.Generate();
        }
        public static RegistrationRequest RegistrationRequest()
        {
            return new RegistrationRequest
            {
                Email = "user@example.com",
                FullName = "Full Name",
                Mobile = "9876543210",
                Password = "password"
            };
        }
        public static LoginRequest LoginRequest()
        {
            return new LoginRequest
            {
                Email = "user@example.com",
                Password = "password"
            };
        }
        public static LoginRequest InvalidLoginRequest()
        {
            return new LoginRequest
            {
                Email = "user@example.com",
                Password = "pass"
            };
        }
    }
}
