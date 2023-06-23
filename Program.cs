using GestorContratos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Carregar a configura��o do arquivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar a string de conex�o
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionar o DbContext usando a string de conex�o
builder.Services.AddDbContext<GestorContratosContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Adicionar os servi�os ao cont�iner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisi��o HTTP
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();