using System;
using EvolveDb;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using TrainMonitor.repository;
using MySqlConnector;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using TrainMonitor.application;
using TrainMonitor.application.Services.Accounts;
using TrainMonitor.repository.Repositories;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddScoped<IAccountService,  AccountService>();
builder.Services.AddDistributedMemoryCache();

// Add services to the container.
builder.Services.AddControllersWithViews(); 


//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
//Respositories
builder.Services.AddScoped<IAccountRepository, AccountRepository>();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

using (var connection = new MySqlConnection(connectionString))
{
    var evolve = new Evolve(connection, msg => Console.WriteLine(msg))
    {
        Locations = new[] { "/home/nicu/Documents/Trains/TrainMonitor.repository/Migrations" }, 
        IsEraseDisabled = true, 
    };

    try
    {
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database migration failed.", ex);
        throw;
    }
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
