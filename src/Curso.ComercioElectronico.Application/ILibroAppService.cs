using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface ILibroAppService
{

    ICollection<LibroDto> GetAll();

    Task<LibroDto> CreateAsync(LibroCrearActualizarDto libro);

    Task UpdateAsync (int id, LibroCrearActualizarDto libro);

    Task<bool> DeleteAsync(int libroId);
}