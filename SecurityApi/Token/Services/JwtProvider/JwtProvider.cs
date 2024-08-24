using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecurityApi.Token.Services.JwtProvider.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SecurityApi.Token.Services.JwtProvider
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public static SymmetricSecurityKey SecurityKey(string key) => new(Encoding.UTF8.GetBytes(key));

        public string Generate(Ulid Id, string Email)
        {
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims:
                [
                    new(JwtRegisteredClaimNames.Sub, Id.ToString()),
                    new(JwtRegisteredClaimNames.Email, Email)
                ],
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new(
                    SecurityKey(_options.SecretKey),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool Validate(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                IssuerSigningKey = SecurityKey(_options.SecretKey)
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                // Log the reason why the token is not valid
                return false;
            }
        }
    }
}
