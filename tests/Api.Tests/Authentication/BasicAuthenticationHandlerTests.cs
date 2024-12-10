using System.Text.Encodings.Web;
using Defra.PhaImportNotifications.Api.Authentication;
using Defra.PhaImportNotifications.Api.Configuration;
using Defra.PhaImportNotifications.Api.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Defra.PhaImportNotifications.Api.Tests.Authentication;

public class BasicAuthenticationHandlerTests
{
    private static async Task<BasicAuthenticationHandler> CreateHandler(TestHandlerOptions options)
    {
        var authSchemeOptions = new AuthenticationSchemeOptions { TimeProvider = Substitute.For<TimeProvider>() };

        var mockSchemeOptions = Substitute.For<IOptionsMonitor<AuthenticationSchemeOptions>>();
        mockSchemeOptions.Get("Basic").Returns(authSchemeOptions);

        var mockBasicAuthOptions = Substitute.For<IOptionsMonitor<BasicAuthOptions>>();
        mockBasicAuthOptions.CurrentValue.Returns(
            new BasicAuthOptions
            {
                Enabled = options.AuthEnabled,
                Password = options.Password,
                Username = options.Username,
            }
        );

        var handler = new BasicAuthenticationHandler(
            mockSchemeOptions,
            mockBasicAuthOptions,
            Substitute.For<ILoggerFactory>(),
            Substitute.For<UrlEncoder>()
        );

        await handler.InitializeAsync(
            new AuthenticationScheme("Basic", null, typeof(BasicAuthenticationHandler)),
            options.HttpContext
        );

        return handler;
    }

    [Fact]
    public async Task HandleAuthenticateAsync_WhenDisabled_ReturnsSuccess()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization = "";

        var handler = await CreateHandler(new TestHandlerOptions { AuthEnabled = false, HttpContext = context });

        var result = await handler.AuthenticateAsync();
        result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleAuthenticateAsync_WithNoAuthorizationHeader_FailsWithInvalidAuthorizationHeader()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization = "";

        var handler = await CreateHandler(new TestHandlerOptions { HttpContext = context });

        var result = await handler.AuthenticateAsync();
        result.Failure?.Message.Should().Be("Invalid Authorization Header");
    }

    [Theory]
    [InlineData("Basic ")] // username:
    [InlineData("Basic dXNlcm5hbWVwYXNzd29yZA==")] // usernamepassword
    [InlineData("Basic blob")] // blob
    [InlineData("Bearer blob")] // also blob
    public async Task HandleAuthenticateAsync_WithAnInvalidHeader_FailsWithInvalidAuthorizationHeader(
        string authorizationHeader
    )
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization = authorizationHeader;

        var handler = await CreateHandler(new TestHandlerOptions { HttpContext = context });

        var result = await handler.AuthenticateAsync();
        result.Failure?.Message.Should().Be("Invalid Authorization Header");
    }

    [Fact]
    public async Task HandleAuthenticateAsync_WithIncorrectUsernameOrPassword_FailsWithInvalidUsernameOrPassword()
    {
        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization = BasicAuthHelper.CreateBasicAuthHeader(
            "invalid-username",
            "invalid-password"
        );

        var handler = await CreateHandler(new TestHandlerOptions { HttpContext = context });

        var result = await handler.AuthenticateAsync();
        result.Failure?.Message.Should().Be("Invalid Username or Password");
    }

    [Fact]
    public async Task HandleAuthenticateAsync_WithCorrectUsernameAndPassword_ReturnsSuccess()
    {
        var context = new DefaultHttpContext();

        const string password = "real-password";
        const string username = "real-username";

        context.Request.Headers.Authorization = BasicAuthHelper.CreateBasicAuthHeader(username, password);

        var handler = await CreateHandler(
            new TestHandlerOptions
            {
                HttpContext = context,
                Password = password,
                Username = username,
            }
        );

        var result = await handler.AuthenticateAsync();
        result.Succeeded.Should().BeTrue();
    }

    private class TestHandlerOptions
    {
        public bool AuthEnabled { get; init; } = true;
        public required HttpContext HttpContext { get; init; }
        public string Password { get; init; } = "password";
        public string Username { get; init; } = "username";
    }
}
