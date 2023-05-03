using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarritoCompra.Aplicacion;
using TiendaServicios.Api.CarritoCompra.Aplicacion.Dtos;

namespace TiendaServicios.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoComprasController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<Nuevo.PostCarritoCompraResponse>> Save([FromBody] Nuevo.PostCarritoCompraRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoSesionDto>> Get(int id)
        {
            return await _mediator.Send(new Consulta.GetCarritoCompraRequest { CarritoSesionId = id });
        }
    }
}
