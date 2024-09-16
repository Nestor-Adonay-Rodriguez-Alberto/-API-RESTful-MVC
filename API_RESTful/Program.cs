var builder = WebApplication.CreateBuilder(args);

// AGREGA TODOS LOS CONTROLADORES CREADOS:
builder.Services.AddControllers();

// DOCUMENTACION DE SWAGGER PARA TESTEAR LA API:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
