using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using SecurityApi.Common;
using SecurityApi.Common.Extentions;
using SecurityApi.Password.Services.PasswordHasher;

namespace SecurityApi.Password.Endpoints
{
    public class VerifyPassword : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) => app
            .MapPost("/verify", Handle)
            .WithRequestValidation<Request>();
        public record Request(string Password, string HashedPassword);
        public record Response(bool Verified);
        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MaximumLength(128);

                RuleFor(x => x.HashedPassword)
                    .NotEmpty()
                    .MaximumLength(512);
            }
        }
        private static Ok<Response> Handle(IPasswordHasher passwordHasher, Request request)
        {
            var response = new Response(passwordHasher.Verify(request.Password, request.HashedPassword));
            return TypedResults.Ok(response);
        }
    }
}
