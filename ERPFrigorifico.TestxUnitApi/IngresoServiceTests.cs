using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Application.Services;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using Moq;

namespace ERPFrigorifico.TestxUnitApi
{
    public class IngresoServiceTests
    {

        private readonly Mock<IIngresoRepository> _repoMock;
        private readonly Mock<IUnitOfWorkRepository> _uowMock;
        private readonly IngresoService _service;

        public IngresoServiceTests()
        {
            _repoMock = new Mock<IIngresoRepository>();
            _uowMock = new Mock<IUnitOfWorkRepository>();
            _service = new IngresoService(_repoMock.Object, _uowMock.Object);
        }

        public Task SeedBasicos()
        {
            var proveedor = new Proveedor
            {
                Id = 1,
                Nombre = "Chasinados Mercedes",
                CUIL = 2034568901,
                Direccion = "Calle Falsa 123",
                EliminadoEn = null,
            };

            var operario = new Operario
            {
                Id = 1,
                UserId = "ab1",
                Nombre = "Juan",
                Apellido = "Perez",
                DNI = "123456",
                CarnetVencido = false,
                FechaCarnetVencido = null,
                EliminadoEn = null,
            };

            var camion = new Camion
            {
                Id = 1,
                Marca = "Mercedes",
                Modelo = "Actros",
                EliminadoEn = null,
            };

            _repoMock
                .Setup(x => x.ObtenerProveedorAsync(1))
                .ReturnsAsync(proveedor);

            _repoMock
                .Setup(x => x.ObtenerOperarioAsync(1))

                .ReturnsAsync(operario);
            _repoMock
                .Setup(x => x.ObtenerCamionAsync(1))
                .ReturnsAsync(camion);

            return Task.CompletedTask;
        }


        [Fact]
        public async Task RegistrarIngreso_CreaCorrectamente_ParaProveedor()
        {

            await SeedBasicos();

            _repoMock
                .Setup(x => x.ObtenerIngresoActivoPorPatenteAsync("ABC123"))
                .ReturnsAsync((Ingreso?)null);

            _repoMock
                .Setup(x => x.AddIngreso(It.IsAny<Ingreso>()))
                .Callback<Ingreso>(i => i.Id = 1);

            var request = new RegistrarIngresoRequest
                (
                camionId: null,
                operarioId: null,
                proveedorId: 1,
                pesoBruto: 300,
                pesoTara: 200,
                cantidadAnimales: 10,
                patente: "ABC123",
                tipoIngreso: TipoIngreso.Proveedor
                );

            var result = await _service.RegistrarIngresoAsync(request);

            Assert.Equal(1, result);

            _uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegistrarIngreso_CreaCorrectamente_ParaInterno()
        {

            await SeedBasicos();

            _repoMock
                .Setup(x => x.ObtenerIngresoActivoPorPatenteAsync("ABC123"))
                .ReturnsAsync((Ingreso?)null);

            _repoMock
                .Setup(x => x.AddIngreso(It.IsAny<Ingreso>()))
                .Callback<Ingreso>(i => i.Id = 1);

            var request = new RegistrarIngresoRequest
                (
                camionId: 1,
                operarioId: 1,
                proveedorId: 1,
                pesoBruto: 300,
                pesoTara: 200,
                cantidadAnimales: 10,
                patente: "ABC123",
                tipoIngreso: TipoIngreso.Interno
                );

            var result = await _service.RegistrarIngresoAsync(request);

            Assert.True(result > 0);

            _uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_InternoSinCamionId()
        {
            await SeedBasicos();

            var request = new RegistrarIngresoRequest
            (
            camionId: null,
            operarioId: 1,
            proveedorId: 1,
            pesoBruto: 300,
            pesoTara: 200,
            cantidadAnimales: 10,
            patente: "ABC123",
            tipoIngreso: TipoIngreso.Interno
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_InternoSinOperarioId()
        {
            await SeedBasicos();

            var request = new RegistrarIngresoRequest
            (
            camionId: 1,
            operarioId: null,
            proveedorId: 1,
            pesoBruto: 300,
            pesoTara: 200,
            cantidadAnimales: 10,
            patente: "ABC123",
            tipoIngreso: TipoIngreso.Interno
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_SiOperarioTieneCarnetVencido()
        {
            var operario = new Operario
            {
                Id = 1,
                CarnetVencido = true
            };

            _repoMock
                .Setup(x => x.ObtenerOperarioAsync(1))
                .ReturnsAsync(operario);

            _repoMock
                .Setup(x => x.ObtenerCamionAsync(1))
                .ReturnsAsync(new Camion());

            _repoMock
                .Setup(x => x.ObtenerIngresoActivoPorPatenteAsync("ABC123"))
                .ReturnsAsync((Ingreso?)null);

            var request = new RegistrarIngresoRequest(
                camionId: 1,
                operarioId: 1,
                proveedorId: 1,
                pesoBruto: 300,
                pesoTara: 200,
                cantidadAnimales: 10,
                patente: "ABC123",
                tipoIngreso: TipoIngreso.Interno
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_SiPesoBrutoMenorATara()
        {
            await SeedBasicos();

            var request = new RegistrarIngresoRequest
            (
            camionId: null,
            operarioId: null,
            proveedorId: 1,
            pesoBruto: 100,
            pesoTara: 200,
            cantidadAnimales: 10,
            patente: "ABC123",
            tipoIngreso: TipoIngreso.Proveedor
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_SiNoIngresoAnimales()
        {
            await SeedBasicos();

            var request = new RegistrarIngresoRequest
            (
            camionId: null,
            operarioId: null,
            proveedorId: 1,
            pesoBruto: 100,
            pesoTara: 200,
            cantidadAnimales: -55,
            patente: "ABC123",
            tipoIngreso: TipoIngreso.Proveedor
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_SiNoIngresoPatente()
        {
            await SeedBasicos();

            var request = new RegistrarIngresoRequest
            (
            camionId: null,
            operarioId: null,
            proveedorId: 1,
            pesoBruto: 100,
            pesoTara: 200,
            cantidadAnimales: 1,
            patente: null,
            tipoIngreso: TipoIngreso.Proveedor
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarIngreso_Falla_SiYaHayIngresoActivo()
        {
            await SeedBasicos();

            _repoMock
                .Setup(x => x.ObtenerIngresoActivoPorPatenteAsync("ABC123"))
                .ReturnsAsync(new Ingreso());

            var request = new RegistrarIngresoRequest
            (
            camionId: null,
            operarioId: null,
            proveedorId: 1,
            pesoBruto: 100,
            pesoTara: 200,
            cantidadAnimales: 1,
            patente: "ABC123",
            tipoIngreso: TipoIngreso.Proveedor
            );

            await Assert.ThrowsAsync<BadRequestException>(() =>
                _service.RegistrarIngresoAsync(request));
        }

        [Fact]
        public async Task RegistrarSalida_DeberiaAsignarFechaSalida()
        {
            // Arrange
            var ingreso = new Ingreso
            {
                Id = 1,
                Patente = "ABC123",
                FechaIngreso = DateTime.UtcNow
            };

            _repoMock
                .Setup(r => r.ObtenerIngresoActivoPorPatenteAsync("ABC123"))
                .ReturnsAsync(ingreso);

            var service = new IngresoService(_repoMock.Object, _uowMock.Object);

            // Act
            await service.RegistrarSalidaAsync("ABC123");

            // Assert
            Assert.NotNull(ingreso.FechaSalida);
            _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task RegistrarSalida_Falla_SiNoExisteIngreso()
        {
            _repoMock
                .Setup(x => x.ObtenerIngresoActivoPorPatenteAsync(It.IsAny<string>()))
                .ReturnsAsync((Ingreso?)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _service.RegistrarSalidaAsync("ABC123"));
        }
    }
}
