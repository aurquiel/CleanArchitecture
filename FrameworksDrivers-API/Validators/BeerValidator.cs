using FluentValidation;
using InterfaceAdapters_Mappers.Dtos.Request;

namespace FrameworksDrivers_API.Validators
{
    public class BeerValidator  : AbstractValidator<BeerRequestDTO>
    {
        public BeerValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("La cerveza debe tener nombre");
            RuleFor(dto => dto.Style).NotEmpty().WithMessage("La cerveza debe tener un estilo");
            RuleFor(dto => dto.Alcohol).NotEmpty().WithMessage("La cerveza debe tener alcohol");
        }
    }
}
