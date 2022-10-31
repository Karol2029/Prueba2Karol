using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Curso.ComercioElectronico.Infraestructure;


public class ComercioElectronicoDbContext : DbContext, IUnitOfWork
{

    //Agregar sus entidades
    public DbSet<Libro> Libros { get; set; }

    public DbSet<Editorial> Editorials { get; set; }
    
    public DbSet<Autor> Autors { get; set; }
   
    public string DbPath { get; set; }

    public ComercioElectronicoDbContext(DbContextOptions<ComercioElectronicoDbContext> options) : base(options)
    {
    } 

}



