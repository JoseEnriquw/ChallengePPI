using Core.Domain.Common;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.UseCase.V1.OrderOperations.Queries.GetAll
{
    public class GetOrdersByFiltersValidation : AbstractValidator<GetOrdersByFilters>
    {
        public GetOrdersByFiltersValidation()
        {
            RuleFor(x => x.Operation)
                .Must(op => op == 'C' || op == 'V' || op == null)
                .WithMessage(ErrorMessage.OPERATION_VALIDATION);

            RuleFor(x => x.State)
                .Must(state => state==null || Enum.IsDefined(typeof(OrderStatusEnum), state.Value))
                .WithMessage(ErrorMessage.STATE_VALIDATION);
        }
    }
}
