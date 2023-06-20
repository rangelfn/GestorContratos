using GestorContratos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Carregar a configuração do arquivo appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar a string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionar o DbContext usando a string de conexão
builder.Services.AddDbContext<GestorContratosContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Adicionar os serviços ao contêiner
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar o pipeline de requisição HTTP
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