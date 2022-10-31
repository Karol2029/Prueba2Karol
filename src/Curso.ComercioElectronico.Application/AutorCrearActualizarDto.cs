using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

  
public class AutorCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string? Nombre {get;set;}
    public string? Apellido {get; set;}
}