using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Aplicacion.Dto;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta
    {
        #region ListaTodos
        public class GetAllAutorRequest : IRequest<List<AutorDto>>
        {
        }

        private class GetAllAutorHandler : IRequestHandler<GetAllAutorRequest, List<AutorDto>>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public GetAllAutorHandler(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<AutorDto>> Handle(GetAllAutorRequest request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.AutorLibro.ToListAsync();

                return _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
            }
        }

        #endregion ListarTodos

        #region TraerPorGuid

        public class GetAutorRequest: IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        private class GetAutorHandler : IRequestHandler<GetAutorRequest, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public GetAutorHandler(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<AutorDto> Handle(GetAutorRequest request, CancellationToken cancellationToken)
            {
                AutorLibro autor = await _contexto.AutorLibro.FirstOrDefaultAsync(a => a.AutorLibroGuid == request.AutorGuid);

                if (autor is null) throw new Exception($"No existe autor el autor");

                return _mapper.Map<AutorLibro, AutorDto>(autor);
            }
        }
        #endregion TraerPorGuid



    }
}
