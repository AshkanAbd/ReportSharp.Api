using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Api.Config;
using ReportSharp.Builder.ReportSharpConfigBuilder;

namespace ReportSharp.Api.Extensions
{
    public static class ReportSharpConfigOptionsBuilderExtension
    {
        public static IReportSharpConfigBuilder SetApiPrefix(
            this IReportSharpConfigBuilder builder, string apiPrefix
        )
        {
            if (apiPrefix == null) throw new ArgumentNullException(nameof(apiPrefix));

            builder.ServiceCollection.Configure<ReportSharpApiConfig>(config =>
                config.ApiPrefix = apiPrefix
            );

            return builder;
        }

        public static IReportSharpConfigBuilder SetUsername(
            this IReportSharpConfigBuilder builder, string username
        )
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            builder.ServiceCollection.Configure<ReportSharpApiConfig>(config =>
                config.Username = username
            );

            return builder;
        }

        public static IReportSharpConfigBuilder SetSecretKey(
            this IReportSharpConfigBuilder builder, string secretKey
        )
        {
            if (secretKey == null) throw new ArgumentNullException(nameof(secretKey));

            builder.ServiceCollection.Configure<ReportSharpApiConfig>(config =>
                config.SecretKey = secretKey
            );

            return builder;
        }
    }
}