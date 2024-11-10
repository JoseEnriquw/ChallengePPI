using Core.Domain.Common;
using Core.Domain.Enums;
using FluentValidation;

namespace Core.UseCase.V1.OrderOperations.Commands.Update
{
    public class UpdateOrderStatusCommandValidation : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidation() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER, "{PropertyName}"));
            RuleFor(x => x.State)
                .Must(state => Enum.IsDefined(typeof(OrderStatusEnum), state))
                .WithMessage(ErrorMessage.STATE_VALIDATION);
        }
    }
}
