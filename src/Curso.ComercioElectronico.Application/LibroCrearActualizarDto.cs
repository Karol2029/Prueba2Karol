using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

  
public class LibroCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string? Nombre {get;set;}
    public string? Edicion {get; set;}

    public int EditorialId {get; set;}

        public int AutorId {get; set;}
}