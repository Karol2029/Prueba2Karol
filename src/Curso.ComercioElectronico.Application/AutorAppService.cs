using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;



public class AutorAppService : IAutorAppService
{
    private readonly IAutorRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public AutorAppService(IAutorRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<AutorDto> CreateAsync(AutorCrearActualizarDto autorDto)
    {
        
         
        var existeNombreAutor = await repository.ExisteNombre(autorDto.Nombre);
        if (existeNombreAutor){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }
 
        
        var autor = new Autor();
        autor.Nombre = autorDto.Nombre;
        autor.Apellido = autorDto.Apellido;
        autor = await repository.AddAsync(autor);
        
        
        var autorCreado = new AutorDto();
        autorCreado.Nombre = autor.Nombre;
        autorCreado.Apellido = autor.Apellido;
        autorCreado.Id = autor.Id;

        

        return autorCreado;
    }

    public async Task UpdateAsync(int id, AutorCrearActualizarDto autorDto)
    {
        var autor = await repository.GetByIdAsync(id);
        if (autor == null){
            throw new ArgumentException($"El autor con el id: {id}, no existe");
        }
        
        var existeNombreLibro = await repository.ExisteNombre(autorDto.Nombre,id);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        autor.Nombre = autorDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(autor);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int autorId)
    {
        //Reglas Validaciones... 
        var autor = await repository.GetByIdAsync(autorId);
        if (autor == null){
            throw new ArgumentException($"La marca con el id: {autorId}, no existe");
        }

        repository.Delete(autor);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<AutorDto> GetAll()
    {
        var autorList = repository.GetAll();

        var autorListDto =  from m in autorList
                            select new AutorDto(){
                                Id = m.Id,
                                Nombre = m.Nombre,
                                Apellido = m.Apellido
                            };

        return autorListDto.ToList();
    }

   
}
 