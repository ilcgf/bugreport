using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Logging;

namespace BugReport.WebApi.Test;

public sealed class Gets : BaseTest
{
    private readonly ILogger<Gets> _logger;

    public Gets(ILogger<Gets> logger)
    {
        _logger = logger;
    }


    [Fact]
    public async Task Should_return_ArrayOfUsers()
    {
        _logger.LogInformation("starting");
        var content = await _http.GetFromJsonAsync<User[]>("users");

        Assert.Equal(4, content!.Length);
   
    }

    [Fact]
    public async Task Should_return_OK()
    {
        var response = await _http.GetAsync("users");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
