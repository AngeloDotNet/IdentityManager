# Identity Manager

Example showing a management of users, roles, permissions, modules and licenses using .NET 8 Minimal API and Entity Framework Core

> [!IMPORTANT]
> **The MinimalApi.Identity.API library used in this sample project, is still under development of new implementations.**

### 🛠️ Installation

This example uses the MinimalApi.Identity.API library available on NuGet.

Just search for MinimalApi.Identity.API in the Package Manager GUI or run the following command in the .NET CLI:

```shell
dotnet add package MinimalApi.Identity.API
```

### 🚀 Configuration

Adding this sections in the _appsettings.json_ file:

```json
{
    "Kestrel": {
        "Limits": {
            "MaxRequestBodySize": 5242880
        }
    },
    "JwtOptions": {
        "Issuer": "[ISSUER]",
        "Audience": "[AUDIENCE]",
        "SecurityKey": "[SECURITY-KEY]",
    },
    "NetIdentityOptions": {
        "RequireUniqueEmail": true,
        "RequireDigit": true,
        "RequiredLength": 8,
        "RequireUppercase": true,
        "RequireLowercase": true,
        "RequireNonAlphanumeric": true,
        "RequiredUniqueChars": 4,
        "RequireConfirmedEmail": true,
        "MaxFailedAccessAttempts": 3,
        "AllowedForNewUsers": true,
        "DefaultLockoutTimeSpan": "00:05:00"
    },
    "SmtpOptions": {
        "Host": "smtp.example.org",
        "Port": 25,
        "Security": "StartTls",
        "Username": "Username del server SMTP",
        "Password": "Password del server SMTP",
        "Sender": "MyApplication <noreply@example.org>",
        "SaveEmailSent": false 
    },
    "UsersOptions": {
        "AssignAdminRoleOnRegistration": "admin@example.org"
    },
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=[HOSTNAME];Initial Catalog=[DATABASE];User ID=[USERNAME];Password=[PASSWORD];Encrypt=False"
    }
}
```

Registering services at _Program.cs_ file:

```csharp
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetDatabaseConnString("DefaultConnection");

//...

var jwtOptions = builder.Configuration.GetSettingsOptions<JwtOptions>(nameof(JwtOptions));
var identityOptions = builder.Configuration.GetSettingsOptions<NetIdentityOptions>(nameof(NetIdentityOptions));
var smtpOptions = builder.Configuration.GetSettingsOptions<SmtpOptions>(nameof(SmtpOptions));

builder.Services
    .AddRegisterServices<Program>(connectionString, jwtOptions, identityOptions)
    .AddAuthorization(options =>
    {
        options.AddDefaultAuthorizationPolicy(); // Adds default authorization policies
        // Here you can add additional authorization policies
    });

builder.Services
    .AddSwaggerConfiguration()
    .AddRegisterOptions(builder.Configuration);

var app = builder.Build();

//...

app.UseAuthentication();
app.UseAuthorization();

//...

app.UseMapEndpoints();
app.Run();
```

### 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

### ⭐ Give a Star

If you find this project useful, please give it a ⭐ on GitHub to show your support and help others discover it!

### 🤝 Contributing

Suggestions are always welcome! Feel free to open suggestion issues in the repository and we will implement them as possible.