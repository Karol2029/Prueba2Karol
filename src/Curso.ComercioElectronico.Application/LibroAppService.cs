using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class LibroAppService : ILibroAppService
{
    private readonly ILibroRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public LibroAppService(ILibroRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<LibroDto> CreateAsync(LibroCrearActualizarDto libroDto)
    {
        
         
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libroDto.Nombre}");
        }
 
        
        var libro = new Libro();
        libro.Nombre = libroDto.Nombre;
        libro.EditorialId = libroDto.EditorialId;
        libro.AutorId = libroDto.AutorId;
        libro.Edicion = libroDto.Edicion;
        libro = await repository.AddAsync(libro);
        await unitOfWork.SaveChangesAsync();

        
        var libroCreado1 = new LibroDto();
        libroCreado1.Nombre = libro.Nombre;
        libroCreado1.Id = libro.Id;
        libroCreado1.EditorialId = libro.EditorialId;
        libroCreado1.AutorId = libro.AutorId;
        libro.Edicion = libro.Edicion;

        return libroCreado1;
    }

    public async Task UpdateAsync(int id, LibroCrearActualizarDto libroDto)
    {
        var libro = await repository.GetByIdAsync(id);
        if (libro == null){
            throw new ArgumentException($"El libro con el id: {id}, no existe");
        }
        
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre,id);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libro.Nombre}");
        }

        //Mapeo Dto => Entidad
        libro.Nombre = libroDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(libro);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int libroId)
    {
        //Reglas Validaciones... 
        var libro = await repository.GetByIdAsync(libroId);
        if (libro == null){
            throw new ArgumentException($"El libro con el id: {libroId}, no existe");
        }

        repository.Delete(libro);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<LibroDto> GetAll()
    {
        var libroList = repository.GetAll();

        var libroListDto =  from m in libroList
                            select new LibroDto(){
                                Id = m.Id,
                                Nombre = m.Nombre,
                                Edicion = m.Edicion
                            };

        return libroListDto.ToList();
    }

    
}
 