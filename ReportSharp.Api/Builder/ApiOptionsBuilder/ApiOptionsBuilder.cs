using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Api.Services.ApiAuthorizationService;
using ReportSharp.Exceptions;

namespace ReportSharp.Api.Builder.ApiOptionsBuilder
{
    public class ApiOptionsBuilder : IApiOptionsBuilder
    {
        public ApiOptionsBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public IApiOptionsBuilder UseAuthorization<TAuthorizationService>()
            where TAuthorizationService : IApiAuthorizationService
        {
            ServiceCollection.AddScoped(typeof(IApiAuthorizationService), typeof(TAuthorizationService));

            return this;
        }

        public IApiOptionsBuilder UseAuthorization(Type authorizationServiceType)
        {
            if (!typeof(IApiAuthorizationService).IsAssignableFrom(authorizationServiceType))
                throw new InvalidTypeException(authorizationServiceType, "API authorization service");

            ServiceCollection.AddScoped(typeof(IApiAuthorizationService), authorizationServiceType);

            return this;
        }
    }
}