using Defra.PhaImportNotifications.Api.Authentication;
using Defra.PhaImportNotifications.Api.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.Extensions;

public static class BasicAuthenticationExtensions
{
    public static void AddBasicAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IValidateOptions<BasicAuthOptions>, BasicAuthOptionsValidator>();
        services.AddOptions<BasicAuthOptions>().BindConfiguration("BasicAuth").ValidateOptions();

        services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
        services.AddAuthorization();
    }
}
