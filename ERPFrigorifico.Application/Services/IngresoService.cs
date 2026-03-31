using ERPFrigorifico.Application.Exceptions;
using ERPFrigorifico.Application.Interfaces;
using ERPFrigorifico.Application.Interfaces.Ingresos;
using ERPFrigorifico.Domain.Entities;
using ERPFrigorifico.Domain.Enums;
using ERPFrigorifico.Shared.DTOs.Ingresos;

namespace ERPFrigorifico.Application.Services
{
    public class IngresoService(IIngresoRepository ingresoRepository,
        IUnitOfWorkRepository unitOfWorkRepository) : IIngresoService
    {

        private readonly IIngresoRepository _ingresoRepository = ingresoRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        public async Task<int> RegistrarIngresoAsync(RegistrarIngresoRequest request)
        {
            //voy pensando en el log, pero lo dejo comentado por ahora.
            //_logger.LogInformation("Iniciando registro de ingreso de materia prima");

            //Validaciones generales para ambos tipos de ingreso.
            if (request.cantidadAnimales <= 0)
                throw new BadRequestException("Debes asignar la cantidad de animales a ingresar.");

            if (string.IsNullOrWhiteSpace(request.patente))
                throw new BadRequestException("La patente es obligatoria.");

            if (request.pesoBruto <= request.pesoTara)
                throw new BadRequestException("El peso bruto debe ser mayor al peso tara.");

            ValidarPesos(request.pesoTara, request.pesoBruto);

            var pesoNeto = request.pesoBruto - request.pesoTara;

            await ValidarIngresoActivoPorPatente(request.patente);


            //Dependiendo el tipo de ingreso se validan los datos correspondientes.
            if (request.tipoIngreso == TipoIngreso.Interno)
            {

                if (request.operarioId == null)
                    throw new BadRequestException("El operario es obligatorio.");
                if (request.camionId == null)
                    throw new BadRequestException("El camion es obligatorio.");

                await ValidarCamionAsync(request.camionId);
                await ValidarOperarioAsync(request.operarioId);
            }
            else if (request.tipoIngreso == TipoIngreso.Proveedor)
            {

                if (request.proveedorId == null)
                    throw new BadRequestException("El proveedor es obligatorio.");

                await ValidarProveedorAsync(request.proveedorId);

            }

            var ingreso = new Ingreso
            {
                TipoIngreso = request.tipoIngreso,
                ProveedorId = request.proveedorId,
                CamionId = request.camionId,
                OperarioId = request.operarioId,
                PesoBruto = request.pesoBruto,
                PesoTara = request.pesoTara,
                PesoNeto = pesoNeto,
                Patente = request.patente,
                FechaIngreso = DateTime.UtcNow,
                CantidadAnimales = request.cantidadAnimales,
            };

            _ingresoRepository.AddIngreso(ingreso);
            await _unitOfWorkRepository.SaveChangesAsync();

            return ingreso.Id;
        }


        public async Task RegistrarSalidaAsync(string patente)
        {
            var ingreso = await _ingresoRepository.ObtenerIngresoActivoPorPatenteAsync(patente);

            if (ingreso == null)
                throw new NotFoundException("No existe un ingreso activo para esta patente.");

            ingreso.FechaSalida = DateTime.UtcNow;

            await _unitOfWorkRepository.SaveChangesAsync();
        }


        //Metodos privados para validaciones especificas de cada entidad.
        private async Task ValidarProveedorAsync(int? proveedorId)
        {
            var proveedor = await _ingresoRepository.ObtenerProveedorAsync(proveedorId);

            if (proveedor == null)
                throw new NotFoundException("El proveedor no esta registrado.");
            if (proveedor.EliminadoEn != null)
                throw new BadRequestException("El proveedor esta eliminado de los registros. No puede ingresar.");
        }

        private async Task ValidarCamionAsync(int? camionId)
        {
            var camion = await _ingresoRepository.ObtenerCamionAsync(camionId);

            if (camion == null)
                throw new NotFoundException("El camion no esta registrado.");
            if (camion.EliminadoEn != null)
                throw new BadRequestException("El camion esta eliminado de los registros. No puede ingresar.");
        }

        private async Task ValidarOperarioAsync(int? operarioId)
        {
            var operario = await _ingresoRepository.ObtenerOperarioAsync(operarioId);

            if (operario == null)
                throw new NotFoundException("El operario no esta registrado.");
            if (operario.CarnetVencido)
                throw new BadRequestException("El carnet del operario esta vencido.");
            if (operario.EliminadoEn != null)
                throw new BadRequestException("El operario esta eliminado de los registros.");
        }

        private async Task ValidarIngresoActivoPorPatente(string patente)
        {
            var ingresoActivo = await _ingresoRepository.ObtenerIngresoActivoPorPatenteAsync(patente);

            if (ingresoActivo != null)
                throw new BadRequestException("Ya existe un ingreso activo con esa patente.");
        }

        private void ValidarPesos(decimal pesoTara, decimal pesoBruto)
        {
            if (pesoTara <= 0) throw new BadRequestException("El peso de la tara es obligatorio.");
            if (pesoBruto <= 0) throw new BadRequestException("El peso bruto es obligatorio.");
        }

    }
}