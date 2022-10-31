using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

 
public class Libro
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string? Nombre {get;set;}
    public string Edicion {get; set;}

    [Required]
    
    public int EditorialId {get;set;}

    public virtual Editorial Editorial {get; set; }


    [Required]
    public int AutorId {get;set;}

    public virtual Autor Autor {get;set;}
}