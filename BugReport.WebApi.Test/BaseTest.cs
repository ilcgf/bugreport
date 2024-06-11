using Microsoft.Extensions.Logging;

namespace BugReport.WebApi.Test;

public abstract class BaseTest : IAsyncDisposable
{
    protected readonly CustomWebApplicationFactory _factory;
    protected readonly HttpClient _http;
    protected readonly ILogger _logger;

    protected BaseTest()
    {
        _factory = new CustomWebApplicationFactory();
        _http = _factory.CreateClient();
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Debug);
        });

        _logger = loggerFactory.CreateLogger("IntegrationTests");
    }

    public virtual async ValueTask DisposeAsync()
    {
        _http.Dispose();
        await _factory.DisposeAsync();
    }
}
