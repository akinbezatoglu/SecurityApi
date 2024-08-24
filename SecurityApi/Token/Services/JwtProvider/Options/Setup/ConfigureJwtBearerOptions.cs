using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SecurityApi.Token.Services.JwtProvider.Options.Setup
{
    public class ConfigureJwtBearerOptions(IOptions<JwtOptions> jwtOptions) : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters.ValidIssuer = _jwtOptions.Issuer;
            options.TokenValidationParameters.ValidAudience = _jwtOptions.Audience;
            options.TokenValidationParameters.IssuerSigningKey = SecurityKey(_jwtOptions.SecretKey);
        }
        public static SymmetricSecurityKey SecurityKey(string key) => new(Encoding.UTF8.GetBytes(key));
    }
}
