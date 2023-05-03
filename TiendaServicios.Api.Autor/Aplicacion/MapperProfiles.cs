using AutoMapper;
using TiendaServicios.Api.Autor.Aplicacion.Dto;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
