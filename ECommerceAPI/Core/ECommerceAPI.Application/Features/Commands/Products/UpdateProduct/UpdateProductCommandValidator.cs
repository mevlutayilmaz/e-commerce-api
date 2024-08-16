using FluentValidation;

namespace ECommerceAPI.Application.Features.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Price)
                .NotNull().WithMessage("Price cannot be empty!")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be 0 or greater!");
        }
    }
}
