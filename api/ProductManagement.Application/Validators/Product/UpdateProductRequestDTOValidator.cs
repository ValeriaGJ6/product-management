using FluentValidation;
using ProductManagement.Application.DTOs.Requests.Product;

namespace ProductManagement.Application.Validators.Product
{
    public class UpdateProductRequestDTOValidator : AbstractValidator<UpdateProductRequestDTO>
    {
        public UpdateProductRequestDTOValidator()
        {
            RuleFor(x => x.Name).ProductName();
            RuleFor(x => x.Description).ProductDescription();
            RuleFor(x => x.Price).ProductPrice();
        }
    }
}
