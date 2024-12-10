using Microsoft.Extensions.Options;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class BasicAuthOptionsValidator : IValidateOptions<BasicAuthOptions>
{
    public ValidateOptionsResult Validate(string? name, BasicAuthOptions options)
    {
        if (!options.Enabled)
            return ValidateOptionsResult.Success;

        if (string.IsNullOrEmpty(options.Username) || string.IsNullOrEmpty(options.Password))
            return ValidateOptionsResult.Fail("Basic Auth is enabled but the Username or Password is empty.");

        return ValidateOptionsResult.Success;
    }
}
