using System.Net.Http.Json;
using System.Net;
using Microsoft.Extensions.Logging;

namespace BugReport.WebApi.Test;

public sealed class Gets : BaseTest
{

    [Fact]
    public async Task Should_return_ArrayOfUsers()
    {

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
