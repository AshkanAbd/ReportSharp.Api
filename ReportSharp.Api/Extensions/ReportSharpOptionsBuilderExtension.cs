using System;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Api.Builder.ApiOptionsBuilder;
using ReportSharp.Builder.ReportSharpOptionsBuilder;

namespace ReportSharp.Api.Extensions
{
    public static class ReportSharpOptionsBuilderExtension
    {
        public static IReportSharpOptionsBuilder AddApis(this IReportSharpOptionsBuilder optionsBuilder,
            Action<IApiOptionsBuilder> apiOptionsBuilder)
        {
            optionsBuilder.ServiceCollection.AddMvc(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false
            );

            apiOptionsBuilder(new ApiOptionsBuilder(optionsBuilder.ServiceCollection));

            return optionsBuilder;
        }
    }
}