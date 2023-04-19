using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventBackoffice.Backend.Data;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<BackendContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("BackendContext") ?? throw new InvalidOperationException("Connection string 'BackendContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BackendContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BackendContextSQLite") ?? throw new InvalidOperationException("Connection string 'EventBackofficeContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BackendContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
