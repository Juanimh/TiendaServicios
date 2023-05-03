namespace TiendaServicios.Api.CarritoCompra.Aplicacion.Dtos
{
    public class CarritoSesionDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
