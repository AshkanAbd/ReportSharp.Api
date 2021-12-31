using System;
using ReportSharp.Api.Services.ApiAuthorizationService;

namespace ReportSharp.Api.Builder.ApiOptionsBuilder
{
    public interface IApiOptionsBuilder
    {
        public IApiOptionsBuilder UseAuthorization<TAuthorizationService>()
            where TAuthorizationService : IApiAuthorizationService;

        public IApiOptionsBuilder UseAuthorization(Type authorizationServiceType);
    }
}