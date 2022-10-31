using System.Diagnostics;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Infraestructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Configuraciones de Dependencias
//Configurar DBContext
builder.Services.AddDbContext<ComercioElectronicoDbContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = Path.Join(path, builder.Configuration.GetConnectionString("ComercioElectronico"));
    Debug.WriteLine($"dbPath: {dbPath}");

    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddTransient<ILibroAppService, LibroAppService>();
builder.Services.AddTransient<ILibroRepository, LibroRepository>();
builder.Services.AddTransient<IAutorAppService, AutorAppService>();
builder.Services.AddTransient<IAutorRepository, AutorRepository>();

builder.Services.AddTransient<IEditorialAppService, EditorialAppService>();
builder.Services.AddTransient<IEditorialRepository, EditorialRepository>();

//Utilizar una factoria
builder.Services.AddScoped<IUnitOfWork>(provider => 
{
    var instance = provider.GetService<ComercioElectronicoDbContext>();
    return instance;
});

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
