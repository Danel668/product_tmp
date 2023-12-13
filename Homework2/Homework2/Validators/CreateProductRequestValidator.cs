using FluentValidation;
using ProductGrpc;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Homework2.Validators
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0).WithMessage("Id должен быть положительным числом.");
            RuleFor(request => request.Name).NotEmpty().WithMessage("Имя должно содержать только русские и английские буквы.");
            RuleFor(request => request.Price).GreaterThan(0).WithMessage("Цена должна быть положительным числом.");
            RuleFor(request => request.Weight).GreaterThan(0).WithMessage("Вес должен быть положительным числом.");
            RuleFor(request => request.Type).IsInEnum().WithMessage("Неверный тип продукта.");
            RuleFor(request => request.CreationDate)
                .Must(creationDate => DateTime.TryParse(creationDate, out var parsedDate) && parsedDate < DateTime.Now).WithMessage("Некорректный формат даты");
                
            RuleFor(request => request.WarehouseId).GreaterThan(0).WithMessage("Id склада должен быть положительным числом.");
        }
    }
}
