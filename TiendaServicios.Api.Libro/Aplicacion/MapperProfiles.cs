using AutoMapper;
using TiendaServicios.Api.Libro.Aplicacion.Dto;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<LibreriaMaterial, LibroDto>();
        }
    }
}
