using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection") ?? 
                       throw new InvalidOperationException("Connection string 'PostgresConnection'" +
                                                    " not found.");

builder.Services.AddDbContext<ResolutionDbContext>(opt =>
    opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("WebApi")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// GET Minimal APIs
app.MapGet("/resolutions", async (ResolutionDbContext db) => await db.Resolutions.ToListAsync());

app.Run();
