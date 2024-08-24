using SecurityApi.Common.Filters;

namespace SecurityApi.Common.Extentions
{
    public static class RouteHandlerBuilderValidationExtensions
    {
        public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
        {
            return builder
                .AddEndpointFilter<RequestValidationFilter<TRequest>>()
                .ProducesValidationProblem();
        }
    }
}
