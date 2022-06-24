using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data;
using TodoWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoDb"));
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("TodoSqlLiteConnection")))

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
