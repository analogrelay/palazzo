using System.Reflection;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.OpenApi.Models;

using Palazzo.Configuration;
using Palazzo.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v0.1", new OpenApiInfo()
    {
        Version = "v0.1",
        Title = "Palazzo API",
        Description = "An API for managing Palazzo databases.",
    });
    var xmlFilename = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddPalazzoNode(builder.Configuration.GetSection("Palazzo"));
builder.Services.Configure<NodeOptions>(options =>
{
    // Resolve the storage root relative to the content root.
    var storageRoot = Path.Combine(builder.Environment.ContentRootPath, options.StorageRoot);
    options.StorageRoot = storageRoot;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v0.1/swagger.json", "v0.1"));
}

app.UseHttpsRedirection();
app.Use(async (httpContext, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex) when (ex is IProblemException problemException)
    {
        if (problemException.StatusCode is { } s)
        {
            httpContext.Response.StatusCode = s;
        }

        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails()
            {
                Type = problemException.Type,
                Title = problemException.Title,
                Detail = problemException.Detail,
                Status = problemException.StatusCode,
                Instance = problemException.Instance,
            },
            new JsonSerializerOptions(),
            "application/problem+json");
    }
});
app.UseAuthorization();
app.MapControllers();

app.Run();
