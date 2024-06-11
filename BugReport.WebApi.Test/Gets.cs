using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace BugReport.WebApi.Test;

public sealed class Gets(ITestOutputHelper output) : BaseTest
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public async Task Should_return_ArrayOfUsers()
    {
        var content = await _http.GetFromJsonAsync<User[]>("users");

        Assert.Equal(4, content!.Length);
    }

    [Fact]
    public async Task Should_return_OK()
    {
        HttpResponseMessage? response = null;

        response = await _http.GetAsync("users");
        var content = await response.Content.ReadAsStringAsync();
        _logger.LogInformation(content);
        _output.WriteLine(content);
        Assert.Equal(HttpStatusCode.OK, response!.StatusCode);
    }
}
