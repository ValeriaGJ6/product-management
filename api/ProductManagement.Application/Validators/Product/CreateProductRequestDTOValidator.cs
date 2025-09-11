using ProductManagement.Application.DTOs.Requests.Product;
using FluentValidation;

namespace ProductManagement.Application.Validators.Product
{
    public class CreateProductRequestDTOValidator : AbstractValidator<CreateProductRequestDTO>
    {
        public CreateProductRequestDTOValidator()
        {
            RuleFor(x => x.Name).ProductName();
            RuleFor(x => x.Description).ProductDescription();
            RuleFor(x => x.Price).ProductPrice();
        }
    }
}
