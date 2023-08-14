namespace DotNetBlazor.Shared.Models.Common
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }

        public JwtToken(string token, DateTime expiryTime)
        {
            Token = token;
            ExpiryTime = expiryTime;
        }
    }
}
