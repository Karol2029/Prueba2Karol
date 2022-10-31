using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

 
public class Editorial
{
    [Required]
    public int Id {get;set;}

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string? Nombre {get;set;}
    public int Anio {get; set;}
    public string? Mes {get; set;}
}