using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventBackofficeBackend.Data;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<EventBackofficeBackendContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("EventBackofficeBackendContext") ?? throw new InvalidOperationException("Connection string 'EventBackofficeBackendContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EventBackofficeBackendContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EventBackofficeBackendContextSQLite") ?? throw new InvalidOperationException("Connection string 'EventBackofficeContext' not found.")));

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

    var context = services.GetRequiredService<EventBackofficeBackendContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
