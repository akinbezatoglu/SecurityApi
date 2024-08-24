using Microsoft.AspNetCore.Http.HttpResults;
using SecurityApi.Common;
using SecurityApi.Common.Extentions;
using SecurityApi.Token.Services.JwtProvider;

namespace SecurityApi.Token.Endpoints
{
    public class ValidateToken : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/validate", Handle)
            .WithRequestValidation<Request>();
        public record Request(string Token);
        public record Response(bool Valid);
        private static Ok<Response> Handle(IJwtProvider jwtProvider, Request request)
        {
            var response = new Response(jwtProvider.Validate(request.Token));
            return TypedResults.Ok(response);
        }
    }
}
