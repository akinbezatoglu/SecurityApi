namespace SecurityApi.Token.Services.JwtProvider
{
    public interface IJwtProvider
    {
        string Generate(Ulid Id, string Email);
        bool Validate(string Token);
    }
}
