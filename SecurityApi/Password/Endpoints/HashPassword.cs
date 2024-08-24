using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using SecurityApi.Common;
using SecurityApi.Common.Extentions;
using SecurityApi.Password.Services.PasswordHasher;

namespace SecurityApi.Password.Endpoints
{
    public class HashPassword : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/hash", Handle)
            .WithRequestValidation<Request>();
        public record Request(string Password);
        public record Response(string Hash);
        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MaximumLength(128);
            }
        }
        private static Ok<Response> Handle(IPasswordHasher passwordHasher, Request request)
        {
            var response = new Response(passwordHasher.Hash(request.Password));
            return TypedResults.Ok(response);
        }
    }
}
