﻿using Microsoft.Extensions.Options;

namespace SecurityApi.Token.Services.JwtProvider.Options.Setup
{
    public class ConfigureJwtOptions(IConfiguration configuration) : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
