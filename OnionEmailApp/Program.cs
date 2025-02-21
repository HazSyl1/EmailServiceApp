using Microsoft.EntityFrameworkCore;
using OnionEmailApp.Domain.Interfaces;
using OnionEmailApp.Infrastructure.Repositories;
using OnionEmailApp.Application.Interfaces;
using OnionEmailApp.Application.Services;
using OnionEmailApp.Infrastructure.Services;
using OnionEmailApp.Infrastructure.Data;
using OnionEmailApp.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddScoped<IRepository<Email>, EmailRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmtpClient>(provider =>
    new SmtpClient(
        builder.Configuration["Smtp:Host"],
        int.Parse(builder.Configuration["Smtp:Port"]),
        builder.Configuration["Smtp:Username"],
        builder.Configuration["Smtp:Password"]
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Email}/{action=SendOtp}/{id?}");

app.Run();