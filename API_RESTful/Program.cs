using API_RESTful.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// AGREGA TODOS LOS CONTROLADORES CREADOS:
builder.Services.AddControllers();

// DOCUMENTACION DE SWAGGER PARA TESTEAR LA API:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INYECCION DE LA DB:
builder.Services.AddDbContext<MyDBcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Cadena_Conexion")));

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

app.Run();
