using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReportSharp.Api.Config;
using ReportSharp.Builder.ReportSharpBuilder;

namespace ReportSharp.Api.Extensions
{
    public static class ReportSharpConfigBuilderExtension
    {
        public static ReportSharpBuilder UseApis(
            this ReportSharpBuilder reportSharpBuilder
        )
        {
            var options = reportSharpBuilder.App.ApplicationServices.GetService<IOptions<ReportSharpApiConfig>>();
            var apiConfig = options == null ? new ReportSharpApiConfig() : options.Value;

            NormalizeApiConfig(apiConfig);

            reportSharpBuilder.App
                .MapWhen(x => x
                        .Request.Path.HasValue && x.Request.Path.Value!.StartsWith(apiConfig.ApiPrefix)
                    , builder => {
                        builder.UseMvc(routes => {
                            routes.MapRoute("Index requests",
                                apiConfig.ApiPrefix + "/request",
                                new {
                                    controller = "Request",
                                    action = "Index"
                                }
                            ).MapRoute("Show request",
                                apiConfig.ApiPrefix + "/request/{id}",
                                new {
                                    controller = "Request",
                                    action = "Show"
                                }
                            ).MapRoute("Index exceptions",
                                apiConfig.ApiPrefix + "/exception",
                                new {
                                    controller = "Exception",
                                    action = "Index"
                                }
                            ).MapRoute("Show exception",
                                apiConfig.ApiPrefix + "/exception/{id}",
                                new {
                                    controller = "Exception",
                                    action = "Show"
                                }
                            ).MapRoute("Index data",
                                apiConfig.ApiPrefix + "/data",
                                new {
                                    controller = "Data",
                                    action = "Index"
                                }
                            ).MapRoute("Show data",
                                apiConfig.ApiPrefix + "/data/{id}",
                                new {
                                    controller = "Data",
                                    action = "Show"
                                }
                            );
                        });
                    });

            return reportSharpBuilder;
        }

        private static void NormalizeApiConfig(ReportSharpApiConfig apiConfig)
        {
            while (apiConfig.ApiPrefix.EndsWith("/")) {
                apiConfig.ApiPrefix = apiConfig.ApiPrefix
                    .Substring(0, apiConfig.ApiPrefix.Length - 1);
            }

            while (!apiConfig.ApiPrefix.StartsWith("/")) {
                apiConfig.ApiPrefix = $"/{apiConfig.ApiPrefix}";
            }
        }
    }
}