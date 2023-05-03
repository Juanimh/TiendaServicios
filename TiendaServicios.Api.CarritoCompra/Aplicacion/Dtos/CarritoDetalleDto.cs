namespace TiendaServicios.Api.CarritoCompra.Aplicacion.Dtos
{
    public class CarritoDetalleDto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
