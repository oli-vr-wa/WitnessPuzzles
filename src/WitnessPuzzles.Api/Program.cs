using WitnessPuzzles.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(Scan => Scan
    .FromAssemblyOf<GridBuilderService>()
    .AddClasses()
    .AsMatchingInterface()
    .WithTransientLifetime());

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();

