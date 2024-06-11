namespace BugReport.WebApi.Test;

public abstract class BaseTest : IAsyncDisposable
{
    protected readonly CustomWebApplicationFactory _factory;
    protected readonly HttpClient _http;

    protected BaseTest()
    {
        _factory = new CustomWebApplicationFactory();
        _http = _factory.CreateClient();
    }

    public virtual async ValueTask DisposeAsync()
    {
        _http.Dispose();
        await _factory.DisposeAsync();
    }
}
