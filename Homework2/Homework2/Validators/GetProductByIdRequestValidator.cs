using FluentValidation;
using ProductGrpc;

namespace Homework2.Validators
{
    public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0).WithMessage("Id должен быть положительным числом.");
        }
    }
}
