using FluentValidation;
using MediatR;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class PostLibroRequest : IRequest<PostLibroResponse>
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class PostLibroResponse
        {
            public bool recordSaved { get; set; } 
        }

        public class PostLibroRequestValidation : AbstractValidator<PostLibroRequest>
        {
            private PostLibroRequestValidation()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class PostLibroRequestHandler : IRequestHandler<PostLibroRequest, PostLibroResponse>
        {
            private readonly ContextoLibreria _contexto;
            public PostLibroRequestHandler(ContextoLibreria contexto)
            {
                _contexto = contexto;
            }
            public async Task<PostLibroResponse> Handle(PostLibroRequest request, CancellationToken cancellationToken)
            {
                LibreriaMaterial libro = new();
                libro.Titulo = request.Titulo;
                libro.FechaPublicacion = request.FechaPublicacion;
                libro.AutorLibro = request.AutorLibro;

                _contexto.LibreriaMaterial.Add(libro);
                var result = await _contexto.SaveChangesAsync();

                //todo: loguear error
                return new PostLibroResponse { recordSaved = result > 0 };
            }
        }
    }


}
