using FluentValidation;
using ProductManagement.Application.Common;

namespace ProductManagement.Application.Validators.Common
{
    public class PaginationParamsValidator : AbstractValidator<PaginationParams>
    {
        public PaginationParamsValidator()
        {
            RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("El parámetro 'page' debe ser mayor que cero.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("El parámetro 'pageSize' debe ser mayor que cero.")
                .LessThanOrEqualTo(100).WithMessage("El parámetro 'pageSize' no puede ser mayor que 100.");
        }
    }
}
