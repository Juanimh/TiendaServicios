using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Aplicacion.Dto;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        #region ListaTodos
        public class GetAllLibroRequest : IRequest<List<LibroDto>>
        {
        }

        public class GetAllLibroHandler : IRequestHandler<GetAllLibroRequest, List<LibroDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public GetAllLibroHandler(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<LibroDto>> Handle(GetAllLibroRequest request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.LibreriaMaterial.ToListAsync();

                return _mapper.Map<List<LibreriaMaterial>, List<LibroDto>>(autores);
            }
        }

        #endregion ListarTodos

        #region TraerPorGuid

        public class GetLibroRequest : IRequest<LibroDto>
        {
            public Guid? LibroGuid { get; set; }
        }

        public class GetLibroHandler : IRequestHandler<GetLibroRequest, LibroDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public GetLibroHandler(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<LibroDto> Handle(GetLibroRequest request, CancellationToken cancellationToken)
            {
                LibreriaMaterial libro = await _contexto.LibreriaMaterial.FirstOrDefaultAsync(l => l.LibreriaMaterialId == request.LibroGuid);

                if (libro is null) throw new Exception($"No existe libro");

                return _mapper.Map<LibreriaMaterial, LibroDto>(libro);
            }
        }
        #endregion TraerPorGuid


    }
}
