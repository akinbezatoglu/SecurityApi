namespace SecurityApi.Token.Services.JwtProvider.Options
{
    public class JwtOptions
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string SecretKey { get; init; }
    }
}
