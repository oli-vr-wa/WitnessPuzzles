using WitnessPuzzles.Application.Services;
using WitnessPuzzles.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var allowReactDev = "_allowReactDev";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowReactDev,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PuzzleDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=witness_puzzles.db"));

builder.Services.Scan(Scan => Scan
    .FromAssemblyOf<GridBuilderService>()
    .AddClasses()
    .AsMatchingInterface()
    .WithTransientLifetime());

// Register Infrastructure Repositories
builder.Services.Scan(scan => scan
    .FromAssemblyOf<PuzzleDbContext>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
    .AsMatchingInterface()
    .WithScopedLifetime());

var app = builder.Build();

// Auto-create SQLite database during development
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PuzzleDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(allowReactDev);
app.MapControllers();

app.Run();

