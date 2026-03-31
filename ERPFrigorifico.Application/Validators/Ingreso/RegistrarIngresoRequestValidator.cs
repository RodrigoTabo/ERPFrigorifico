using ERPFrigorifico.Domain.Enums;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using FluentValidation;


namespace ERPFrigorifico.Application.Validators.Ingreso
{
    public class RegistrarIngresoRequestValidator  : AbstractValidator<RegistrarIngresoRequest>
    {
        public RegistrarIngresoRequestValidator()
        {
            RuleFor(x => x.pesoTara)
                .GreaterThan(0).WithMessage("El peso tara debe ser mayor a 0.");

            RuleFor(x => x.pesoBruto)
                .GreaterThan(0).WithMessage("El peso bruto debe ser mayor a 0.");

            RuleFor(x => x)
                .Must(x => x.pesoBruto > x.pesoTara)
                .WithMessage("El peso bruto debe ser mayor al peso tara.");

            RuleFor(x => x.cantidadAnimales)
                .GreaterThan(0).WithMessage("La cantidad de animales no puede ser menor a 0.");

            RuleFor(x => x.patente)
                .NotEmpty().WithMessage("La patente es requerida.")
                .MaximumLength(30).WithMessage("La patente no puede superar los 30 caracteres.");

            When(x => x.tipoIngreso == TipoIngreso.Interno, () =>
            {
                RuleFor(x => x.camionId)
                    .NotNull().WithMessage("El camion es obligatorio.");

                RuleFor(x => x.operarioId)
                    .NotNull().WithMessage("El operario es obligatorio.");
            });

            When(x => x.tipoIngreso == TipoIngreso.Proveedor, () =>
            {
                RuleFor(x => x.proveedorId)
                    .NotNull().WithMessage("El proveedor es obligatorio.");
            });
        }
    }
}
