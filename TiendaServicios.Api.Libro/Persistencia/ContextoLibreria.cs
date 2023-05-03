using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria() { }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }

        //Virtual es que se puede sobreescribir a futuro, necesario para los test  
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
