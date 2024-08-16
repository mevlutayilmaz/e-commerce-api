using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Product.Name)
                .NotEmpty().WithMessage("Name cannot be empty!")
                .NotNull().WithMessage("Name cannot be empty!")
                .MaximumLength(150).MinimumLength(2).WithMessage("Name must be between 2 and 150 characters!");
            RuleFor(p => p.Product.Price)
                .NotNull().WithMessage("Price cannot be empty!")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be 0 or greater!");
        }
    }
}
