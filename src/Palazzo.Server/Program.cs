using Microsoft.Framework.DependencyInjection;

using Palazzo.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
