using Core.Domain.Common;
using FluentValidation;

namespace Core.UseCase.V1.OrderOperations.Queries.GetById
{
    public class GetOrderByIdValidation: AbstractValidator<GetOrderById>
    {
        public GetOrderByIdValidation()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER, "{PropertyName}"));
        }
    }
}
