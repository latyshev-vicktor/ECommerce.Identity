namespace ECommerce.Domain.DTO
{
    public class TokenDto
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenDto(string accessToken, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
