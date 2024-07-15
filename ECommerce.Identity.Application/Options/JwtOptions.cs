namespace ECommerce.Application.Options
{
    public class JwtOptions
    {
        public const string Name = "Jwt";
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public long Expired { get; set; }
    }
}
