using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Aplicacion.Dtos;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class GetCarritoCompraRequest : IRequest<CarritoSesionDto> 
        {
            public int CarritoSesionId { get; set; }

        }

        public class GetCarritoCompraRequestHandler : IRequestHandler<GetCarritoCompraRequest, CarritoSesionDto>
        {
            private readonly ContextoCarritoCompra _contexto;
            private readonly ILibroService _libroService;
            public GetCarritoCompraRequestHandler(ContextoCarritoCompra contexto, ILibroService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }
            public async Task<CarritoSesionDto> Handle(GetCarritoCompraRequest request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(cs => cs.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(csd => csd.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                List<CarritoDetalleDto> listaCarritoDto = new();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                    if (response.resultad)
                    {
                        var objLibro = response.libro;
                        listaCarritoDto.Add(new CarritoDetalleDto {
                            FechaPublicacion = objLibro.FechaPublicacion,
                            LibroId = objLibro.LibreriaMaterialId,
                            TituloLibro = objLibro.Titulo
                        });
                    }
                }

                return new CarritoSesionDto { CarritoId = carritoSesion.CarritoSesionId, FechaCreacionSesion = carritoSesion.FechaCreacion, ListaProductos = listaCarritoDto };

            }
        }

    }
}
