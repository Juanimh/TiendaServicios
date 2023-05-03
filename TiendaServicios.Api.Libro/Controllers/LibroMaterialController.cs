using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Aplicacion.Dto;


namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroMaterialController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<Nuevo.PostLibroResponse>> Save([FromBody] Nuevo.PostLibroRequest data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDto>>> GetAll()
        {
            return await _mediator.Send(new Consulta.GetAllLibroRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> Get(Guid id)
        {
            return await _mediator.Send(new Consulta.GetLibroRequest { LibroGuid = id });
        }
    }
}
