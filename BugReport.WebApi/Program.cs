using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=:memory:");
});
var app = builder.Build();

app.MapGet("users", Gets);

app.Run();


static async Task<Ok<User[]>> Gets(AppDbContext context)
{
    return TypedResults.Ok(await context.Users.ToArrayAsync());
}


public partial class Program { }

public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    public DbSet<User> Users { get; set; }
}


public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}