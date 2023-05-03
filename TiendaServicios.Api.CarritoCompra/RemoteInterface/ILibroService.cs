using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultad, LibroRemote libro, string errorMensaje)> GetLibro(Guid guidLibro);
    }
}
