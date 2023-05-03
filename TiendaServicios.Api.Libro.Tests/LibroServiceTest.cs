using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Aplicacion.Dto;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibroServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObteberLibrosPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPrueba = ObteberLibrosPrueba().AsQueryable();
            //indicar que el sorce de la data, ya no es sql server. Configurar dbset
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new ASyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibro()
        {
            var mockContexto = CrearContexto();
            //emular mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingTest>();
            });

            var mapper = mapperConfig.CreateMapper();

            Consulta.GetLibroHandler getLibroHandler = new(mockContexto.Object, mapper);

            Consulta.GetLibroRequest request = new() { LibroGuid = Guid.Empty };

            var libroDto = await getLibroHandler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libroDto);
            Assert.True(libroDto.LibreriaMaterialId == Guid.Empty);
        }


        [Fact]
        public async void GetLibros()
        {
            //System.Diagnostics.Debugger.Launch();
            //emular contexto
            var mockContexto = CrearContexto();
            //emular mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingTest>();
            });

            var mapper = mapperConfig.CreateMapper();

            Consulta.GetAllLibroHandler handler = new(mockContexto.Object, mapper);

            Consulta.GetAllLibroRequest request = new ();

            var listaLibros = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(listaLibros.Any());

        }

        [Fact]
        public async void SaveLibro()
        {
            //Test para guardar libro
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                                .UseInMemoryDatabase(databaseName: "MemoryDBLibro")
                                .Options;

            var contexto = new ContextoLibreria(options);

            Nuevo.PostLibroRequest request = new() { Titulo = "Libro unit test", AutorLibro = Guid.Empty, FechaPublicacion = DateTime.Now };

            Nuevo.PostLibroRequestHandler postLibroHandler = new(contexto);

            var response = await postLibroHandler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(response is not null);



        }
    }
}
