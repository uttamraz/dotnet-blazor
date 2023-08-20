using DotNetBlazor.Shared.Models.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetBlazor.Server.Utility.Helpers
{
    public interface IJwtUtils
    {
        public JwtToken GenerateJwtToken(AuthUser request);
        public string ValidateJwtToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly string _jwtKey;
        private readonly string _expiryTime;
        public JwtUtils(IConfiguration configuration)
        {
            _jwtKey = configuration["Jwt:Key"] ?? throw new Exception("Jwt:Key is not configured");
            _expiryTime = configuration["Jwt:ExpiryInSeconds"] ?? throw new Exception("Jwt:ExpiryInSeconds is not configured"); ;
        }

        public JwtToken GenerateJwtToken(AuthUser request)
        {
            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", request.Id.ToString()) }),
                Expires = DateTime.Now.AddSeconds(Convert.ToInt32(_expiryTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtToken(tokenHandler.WriteToken(token), tokenDescriptor.Expires.Value);
        }

        public string ValidateJwtToken(string token)
        {
            if (token == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var id = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return user id from JWT token if validation successful
                return id;
            }
            catch (Exception)
            {
                // return null if validation fails
                return string.Empty;
            }
        }
    }
}