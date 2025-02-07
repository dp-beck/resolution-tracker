using System.Reflection;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Dtos;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection") ?? 
                       throw new InvalidOperationException("Connection string 'PostgresConnection'" +
                                                    " not found.");

builder.Services.AddDbContext<ResolutionDbContext>(opt =>
    opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("WebApi")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IResolutionRepository, ResolutionRepository>();
builder.Services.AddScoped<IResolutionCategoryRepository, ResolutionCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Register Endpoints
app.RegisterResolutionEndpoints();

app.Run();
