using Microsoft.AspNetCore.Http.HttpResults;
using SecurityApi.Common;
using SecurityApi.Common.Extentions;
using SecurityApi.Token.Services.JwtProvider;

namespace SecurityApi.Token.Endpoints
{
    public class GenerateToken : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/generate", Handle)
            .WithRequestValidation<Request>();
        public record Request(Ulid Id, string Email);
        public record Response(string Token);
        private static Ok<Response> Handle(IJwtProvider jwtProvider, Request request)
        {
            var response = new Response(jwtProvider.Generate(request.Id, request.Email));
            return TypedResults.Ok(response);
        }
    }
}
