using Microsoft.Extensions.Options;
using SecurityApi;
using SecurityApi.Password.Services.PasswordHasher;
using SecurityApi.Token.Services.JwtProvider;
using SecurityApi.Token.Services.JwtProvider.Options.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName?.Replace('+', '.'));
});

builder.Services.ConfigureOptions<ConfigureJwtOptions>();
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Security API");
    });
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
