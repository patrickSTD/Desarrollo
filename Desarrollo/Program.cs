using DB.Data;
using Desarrollo.Repositorys;
using Desarrollo.Repositorys.RepositorysImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TrabajadoresContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrabajadoresConnection"));
});

builder.Services.AddScoped<TrabajadoresRepository, TrabajadoresRepositoryImpl>();
builder.Services.AddScoped<DepartamentoRepository, DepartamentoRepositoryImpl>();
builder.Services.AddScoped<ProvinciaRepository, ProvinciaRepositoryImpl>();
builder.Services.AddScoped<DistritoRepository, DistritoRepositoryImpl>();
builder.Services.AddAutoMapper(typeof(Program));


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
