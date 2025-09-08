using FluentValidation;

namespace ProductManagement.Application.Validators.Product
{
    public static class ProductValidationExtensions
    {
        public static IRuleBuilderOptions<T, string?> ProductName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");
        }

        public static IRuleBuilderOptions<T, string?> ProductDescription<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(255).WithMessage("La descripción no puede exceder 255 caracteres");
        }

        public static IRuleBuilderOptions<T, decimal?> ProductPrice<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("El precio es requerido")
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0")
                .PrecisionScale(10, 2, false).WithMessage("El precio debe tener máximo 8 dígitos enteros y 2 decimales");
        }

        public static IRuleBuilderOptions<T, int?> ProductId<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("El ID es requerido")
                .GreaterThan(0).WithMessage("El ID debe ser mayor que 0");
        }
    }
}