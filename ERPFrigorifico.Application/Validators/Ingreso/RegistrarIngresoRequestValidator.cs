using ERPFrigorifico.Shared.Enums;
using ERPFrigorifico.Shared.DTOs.Ingresos;
using FluentValidation;


namespace ERPFrigorifico.Application.Validators.Ingreso
{
    public class RegistrarIngresoRequestValidator  : AbstractValidator<RegistrarIngresoRequest>
    {
        public RegistrarIngresoRequestValidator()
        {
            RuleFor(x => x.PesoTara)
                .GreaterThan(0).WithMessage("El peso tara debe ser mayor a 0.");

            RuleFor(x => x.PesoBruto)
                .GreaterThan(0).WithMessage("El peso bruto debe ser mayor a 0.");

            RuleFor(x => x)
                .Must(x => x.PesoBruto > x.PesoTara)
                .WithMessage("El peso bruto debe ser mayor al peso tara.");

            RuleFor(x => x.CantidadAnimales)
                .GreaterThan(0).WithMessage("La cantidad de animales no puede ser menor a 0.");

            RuleFor(x => x.Patente)
                .NotEmpty().WithMessage("La patente es requerida.")
                .MaximumLength(30).WithMessage("La patente no puede superar los 30 caracteres.");

            When(x => x.tipoIngreso == TipoIngreso.Interno, () =>
            {
                RuleFor(x => x.CamionId)
                    .NotNull().WithMessage("El camion es obligatorio.");

                RuleFor(x => x.OperarioId)
                    .NotNull().WithMessage("El operario es obligatorio.");
            });

            When(x => x.tipoIngreso == TipoIngreso.Proveedor, () =>
            {
                RuleFor(x => x.ProveedorId)
                    .NotNull().WithMessage("El proveedor es obligatorio.");
            });
        }
    }
}
