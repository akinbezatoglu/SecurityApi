using SecurityApi.Common;
using SecurityApi.Common.Filters;
using SecurityApi.Password.Endpoints;
using SecurityApi.Token.Endpoints;

namespace SecurityApi
{
    public static class Endpoints
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("")
                .AddEndpointFilter<RequestLoggingFilter>()
                .WithOpenApi();

            endpoints.MapPasswordEndpoints();
            endpoints.MapTokenEndpoints();
        }

        private static void MapPasswordEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/pwd")
                .WithTags("Password");

            endpoints.MapPublicGroup()
                .MapEndpoint<HashPassword>()
                .MapEndpoint<VerifyPassword>();
        }

        private static void MapTokenEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("/token")
                .WithTags("Token");

            endpoints.MapPublicGroup()
                .MapEndpoint<GenerateToken>()
                .MapEndpoint<ValidateToken>();
        }

        private static RouteGroupBuilder MapPublicGroup(this IEndpointRouteBuilder app, string? prefix = null)
        {
            return app.MapGroup(prefix ?? string.Empty)
                .AllowAnonymous();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
