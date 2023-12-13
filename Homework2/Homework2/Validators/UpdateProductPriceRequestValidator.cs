using FluentValidation;
using ProductGrpc;

namespace Homework2.Validators
{
    public class UpdateProductPriceRequestValidator : AbstractValidator<UpdateProductPriceRequest>
    {
        public UpdateProductPriceRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0).WithMessage("Id должен быть положительным числом.");
            RuleFor(request => request.Price).GreaterThan(0).WithMessage("Цена должна быть положительным числом.");
        }
    }
}
