using MinimalApi.Identity.API.Extensions;
using MinimalApi.Identity.API.Options;

namespace IdentityManager.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetDatabaseConnString("DefaultConnection");
        var jwtOptions = builder.Configuration.GetSettingsOptions<JwtOptions>(nameof(JwtOptions));

        var identityOptions = builder.Configuration.GetSettingsOptions<NetIdentityOptions>(nameof(NetIdentityOptions));
        var smtpOptions = builder.Configuration.GetSettingsOptions<SmtpOptions>(nameof(SmtpOptions));

        var namingCors = "cors";

        builder.Services.AddCors(options => options.AddPolicy(namingCors, builder
            => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", app.Environment.ApplicationName);
            });
        }

        app.UseCors(namingCors);

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMapEndpoints();
        app.Run();
    }
}