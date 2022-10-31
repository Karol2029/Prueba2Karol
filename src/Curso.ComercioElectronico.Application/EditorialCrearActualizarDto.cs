using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

  
public class EditorialCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string? Nombre {get;set;}
    public int Anio {get; set;}
    public string? Mes {get; set;}
}