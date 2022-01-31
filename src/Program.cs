using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    Environment.GetEnvironmentVariable("ConnectionStrings__Default")
                    ?? throw new ArgumentNullException("ConnectionStrings__Default"));
            });
    })
    .Build();

await host.RunAsync();

Console.WriteLine("Dummy change");

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    [Keyless]
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
