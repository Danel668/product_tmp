using FluentValidation;
using ProductGrpc;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Homework2.Validators
{
    public class GetProductsListWithFiltersRequestValidator : AbstractValidator<GetProductsListWithFiltersRequest>
    {
        public GetProductsListWithFiltersRequestValidator()
        {
            RuleFor(request => request)
                .Must(request => !request.HasCreationDate
                || (DateTime.TryParse(request.CreationDate, out var parsedDate) && parsedDate < DateTime.Now))
                .WithMessage("Некорректный формат даты");

            RuleFor(request => request)
                .Must(request => !request.HasType
                || Enum.IsDefined(typeof(ProductType), request.Type))
                .WithMessage("Неверный тип продукта.");

            RuleFor(request => request)
                .Must(request => !request.HasWarehouseId
                || request.WarehouseId > 0)
                .WithMessage("Id склада должен быть положительным числом.");

            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("Размер страницы должен быть положительным числом.");
            RuleFor(request => request.Pagination).GreaterThan(0).WithMessage("Пагинация должна быть положительным числом.");
        }
    }
}
