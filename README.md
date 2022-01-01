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

### Available apis:

|            URL            |       Header       |      Query     |                  Action                   |
| ------------------------- | ------------------ | -------------- | ----------------------------------------- |
| ApiPrefix+"request/"      | username, password | page, pageSize | List of reported requests to database     |
| ApiPrefix+"request/{id}"  | username, password |                | Get reported request                      |
| ApiPrefix+"exception/"    | username, password | page, pageSize | List of reported exceptions to database   |
| ApiPrefix+"exception/{id}"| username, password |                | Get reported exceptions                   |
| ApiPrefix+"data/"         | username, password | page, pageSize | List of reported data to database         |
| ApiPrefix+"data/{id}"     | username, password |                | Get reported data                         |

### Default implementation for api password

```c#
public string CalculatePassword(string username)
{
    var now = DateTime.Now;

    var usernameCode = GetAsciiValue(username);
    var secretKeyCode = GetAsciiValue(ReportSharpApiConfig.SecretKey);
    var mergedUsernameSecretKey = long.Parse($"{usernameCode}{secretKeyCode}");

    var todayCode = long.Parse($"{now.Year:0000}{now.Month:00}{now.Day:00}");

    var validPassword = mergedUsernameSecretKey ^ todayCode;

    return validPassword.ToString();
}

public long GetAsciiValue(string str)
{
    return str.ToCharArray().Aggregate(0, (current, c) => current + c);
}
```

#### Notes:

1. Result of `CalculatePassword` method in above source code is password for apis.
2. Default password implementation is time based.
3. you can change default implementation by implementing `IApiAuthorizationService` interface in your `Authorization` class and enable it by `apiOptions.UseAuthorization<YourAuthorization>()`

### Donation:

#### If you like it, you can support me with `USDT`:

1) `TJ57yPBVwwK8rjWDxogkGJH1nF3TGPVq98` for `USDT TRC20`
2) `0x743379201B80dA1CB680aC08F54b058Ac01346F1` for `USDT ERC20`

