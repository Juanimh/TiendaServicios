using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Aplicacion.Dto;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<Nuevo.PostAutorResponse>> Save([FromBody] Nuevo.PostAutorRequest data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAll()
        {
            return await _mediator.Send(new Consulta.GetAllAutorRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> Get(string id)
        {
            return await _mediator.Send(new Consulta.GetAutorRequest { AutorGuid = id});
        }
    }
}
