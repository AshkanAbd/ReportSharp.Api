## ReportSharp.Api-1.0.5:

### Description:

Api for [ReportSharp.DatabaseReporter](https://www.nuget.org/packages/ReportSharp.DatabaseReporter/) of ReportSharp package

### Dependencies:

ReportSharp.DatabaseReporter: 1.0.5

Dotnet Core 3.1 or later

### Usage:

#### Note:

You need to install and configure [ReportSharp](https://www.nuget.org/packages/ReportSharp/) and [ReportSharp.DatabaseReporter](https://www.nuget.org/packages/ReportSharp.DatabaseReporter/) `1.0.5` or later to use this package.

#### Dotnet 5 or below:

1) Add following lines to `ConfigureServices` method in `Startup` class:

```c#
services.AddReportSharp(options => {
    options.ConfigReportSharp(routerOptions =>
        routerOptions.SetApiPrefix("/")
            .SetUsername("Username")
            .SetSecretKey("SecretKey")
    );
    options.AddApis(apiOptions =>
        apiOptions.UseAuthorization<DefaultApiAuthorizationService>()
    );
});
```

2) Add following lines to `Configure` method in `Startup` class:

```c#
app.UseReportSharp(configure => {
    configure.UseApis();
});
```

### Dotnet 6 or later:

1) Add following lines to `services` section, before `builder.Build()` line:

```c#
services.AddReportSharp(options => {
    options.ConfigReportSharp(routerOptions =>
        routerOptions.SetApiPrefix("/")
            .SetUsername("Username")
            .SetSecretKey("SecretKey")
    );
    options.AddApis(apiOptions =>
        apiOptions.UseAuthorization<DefaultApiAuthorizationService>()
    );
});
```

2) Add following lines to `Configure` section, after `builder.Build()` line:

```c#
app.UseReportSharp(configure => {
    configure.UseApis();
});
```
### Donation:
#### If you like it, you can support me with `USDT`:
1) `TJ57yPBVwwK8rjWDxogkGJH1nF3TGPVq98` for `USDT TRC20`
2) `0x743379201B80dA1CB680aC08F54b058Ac01346F1` for `USDT ERC20`

