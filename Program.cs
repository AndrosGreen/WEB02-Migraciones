using ContosoPizza.Services;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

// Additional using declarations

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the PizzaContext
var connectionString = "server=localhost;port=3306;database=pizza;uid=root;password=root";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

builder.Services.AddDbContext<PizzaContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
);

// Add the PromotionsContext

builder.Services.AddScoped<PizzaService>();

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

// Add the CreateDbInNotExists method call
app.CreateDbIfNotExists();

app.Run();
