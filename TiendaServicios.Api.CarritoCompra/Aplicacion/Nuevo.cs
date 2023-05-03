using FluentValidation;
using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class PostCarritoCompraRequest : IRequest<PostCarritoCompraResponse>
        {
            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ListaProducto { get; set; }
        }

        public class PostCarritoCompraResponse
        {
            public bool recordSaved { get; set; } 
        }

        private class PostCarritoCompraRequestValidation : AbstractValidator<PostCarritoCompraRequest>
        {
            private PostCarritoCompraRequestValidation()
            {
                RuleFor(x => x.FechaCreacionSesion).NotEmpty();
                RuleFor(x => x.ListaProducto).NotEmpty();
            }
        }

        private class PostLibroCompraRequestHandler : IRequestHandler<PostCarritoCompraRequest, PostCarritoCompraResponse>
        {
            private readonly ContextoCarritoCompra _contexto;
            public PostLibroCompraRequestHandler(ContextoCarritoCompra contexto)
            {
                _contexto = contexto;
            }
            public async Task<PostCarritoCompraResponse> Handle(PostCarritoCompraRequest request, CancellationToken cancellationToken)
            {
                CarritoSesion carritoSesion = new();
                carritoSesion.FechaCreacion = request.FechaCreacionSesion;
  
                _contexto.CarritoSesion.Add(carritoSesion);
                var value = await _contexto.SaveChangesAsync();

                if (value == 0) throw new Exception("Error en la insersion");

                DateTime fechaActual = DateTime.Now;

                List<CarritoSesionDetalle> detalles = request.ListaProducto.Select(prd => new CarritoSesionDetalle
                {
                    FechaCreacion = fechaActual,
                    CarritoSesionId = carritoSesion.CarritoSesionId,
                    ProductoSeleccionado = prd
                }).ToList();

                await _contexto.CarritoSesionDetalle.AddRangeAsync(detalles);
                var values = await _contexto.SaveChangesAsync();

                if (values <= 0) throw new Exception("No se puedo insertar el detalle de carrito de compras");

                //todo: loguear error
                return new PostCarritoCompraResponse { recordSaved = values > 0 };
            }
        }
    }


}
