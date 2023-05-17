using BusinessLayer.Implementations;
using BusinessLayer;
using DataLayer.Entities;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
builder.Services.AddDbContext<Context>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("TestAppDb")));
*/
var optionsBuilder = new DbContextOptionsBuilder<Context>();
var options = optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("TestAppDb")).Options;

builder.Services.AddScoped<DataManager>(_ => new DataManager(
                                        new EFRepository<Post>(new Context(options))));

builder.Services.AddDbContext<Context>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("TestAppDb")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
