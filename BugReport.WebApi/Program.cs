using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=Database.db");
});
var app = builder.Build();

app.MapGet("users", Gets);

//var provider = builder.Services.BuildServiceProvider();

//PopulateDatabase(provider);
app.Run();


static async Task<Ok<User[]>> Gets(AppDbContext context)
{
    return TypedResults.Ok(await context.Users.ToArrayAsync());
}

static void PopulateDatabase(IServiceProvider provider)
{
    using var scope = provider.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    User[] users =
    [
        new(){Id=1,Name="Anton"},
            new(){Id=2,Name="Bob"},
            new(){Id=3,Name="Charlie"},
            new(){Id=4,Name="David"},
        ];
    context.Users.AddRange(users);
    context.SaveChanges();
}


public partial class Program { }

public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);
        mb.Entity<User>().HasData([
                new(){Id=1,Name="Anton"},
                new(){Id=2,Name="Bob"},
                new(){Id=3,Name="Charlie"},
                new(){Id=4,Name="David"}
            ]);

    }
}


public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}