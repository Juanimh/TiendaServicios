using FluentValidation;
using MediatR;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;


namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class PostAutorRequest  : IRequest<PostAutorResponse> { 
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class PostAutorResponse
        {
            public string AutorLibroGuid { get; set; }
        }

        public class PostAutorRequestValidation : AbstractValidator<PostAutorRequest>
        {
            public PostAutorRequestValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }
        private class PostAutorHandler : IRequestHandler<PostAutorRequest, PostAutorResponse>
        {
            private readonly ContextoAutor _contexto;
            public PostAutorHandler(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<PostAutorResponse> Handle(PostAutorRequest request, CancellationToken cancellationToken)
            {
                AutorLibro autorLibro = new();
                autorLibro.Nombre = request.Nombre;
                autorLibro.Apellido = request.Apellido;
                autorLibro.FechaNacimiento = request.FechaNacimiento;
                autorLibro.AutorLibroGuid = Convert.ToString(Guid.NewGuid());

                _contexto.Add(autorLibro);
                int recordsSaved = await _contexto.SaveChangesAsync();

                if (recordsSaved <= 0) throw new Exception("No se pudo insertar el autor");

                return new PostAutorResponse { AutorLibroGuid = autorLibro.AutorLibroGuid };
            }
        }
    }


}
