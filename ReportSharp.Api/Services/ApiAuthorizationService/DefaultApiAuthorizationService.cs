using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReportSharp.Api.Config;

namespace ReportSharp.Api.Services.ApiAuthorizationService
{
    public class DefaultApiAuthorizationService : IApiAuthorizationService
    {
        public DefaultApiAuthorizationService(IHttpContextAccessor contextAccessor,
            IOptions<ReportSharpApiConfig> options)
        {
            HttpContext = contextAccessor.HttpContext;
            ReportSharpApiConfig = options.Value;
        }

        public HttpContext HttpContext { get; }
        public ReportSharpApiConfig ReportSharpApiConfig { get; }

        public bool IsAuthorized()
        {
            var username = GetValueFromHeader("username");
            if (username == null) return false;

            var password = GetValueFromHeader("password");
            if (password == null) return false;

            if (username != ReportSharpApiConfig.Username) return false;

            var validPassword = CalculatePassword(username);

            return validPassword == password;
        }

        protected virtual string CalculatePassword(string username)
        {
            var now = DateTime.Now;

            var usernameCode = GetAsciiValue(username);
            var secretKeyCode = GetAsciiValue(ReportSharpApiConfig.SecretKey);
            var mergedUsernameSecretKey = long.Parse($"{usernameCode}{secretKeyCode}");

            var todayCode = long.Parse($"{now.Year:0000}{now.Month:00}{now.Day:00}");

            var validPassword = mergedUsernameSecretKey ^ todayCode;

            return validPassword.ToString();
        }

        protected virtual long GetAsciiValue(string str)
        {
            return str.ToCharArray().Aggregate(0, (current, c) => current + c);
        }

        protected virtual string GetValueFromHeader(string key)
        {
            return HttpContext.Request.Headers
                .Where(x => x.Key == key)
                .Select(x => x.Value.ToString())
                .FirstOrDefault();
        }
    }
}