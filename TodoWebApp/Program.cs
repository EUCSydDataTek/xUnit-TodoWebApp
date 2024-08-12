using Microsoft.EntityFrameworkCore;
using System.Net;
using TodoWebApp.Data;
using TodoWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoDb"));
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("TodoSqlLiteConnection")))

//CA In Code
builder.WebHost.ConfigureKestrel(config =>
{
    config.Listen(IPAddress.Parse("127.0.0.1"), 5001, options =>
    {
        options.UseHttps("Server.pfx", "P@ssw0rd");
    });
});

builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
