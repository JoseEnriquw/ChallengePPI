using Core.Domain.Common;
using FluentValidation;

namespace Core.UseCase.V1.OrderOperations.Commands.Create
{
    public class CreateOrderCommandValidation : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidation()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER,"{PropertyName}"));

            RuleFor(x => x.AssetId)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER,"{PropertyName}"));

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage(string.Format(ErrorMessage.MUST_BE_A_POSITIVE_NUMBER,"{PropertyName}"));

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage(string.Format(ErrorMessage.GREATER_THAN_OR_EQUAL,"{PropertyName}",0))
                .When(x => x.Operation == 'C' || x.Operation == 'V') 
                .WithMessage(ErrorMessage.PRICE_VALIDATION);

            RuleFor(x => x.Operation)
                .Must(op => op == 'C' || op == 'V')
                .WithMessage(ErrorMessage.OPERATION_VALIDATION);
        }
    }
}
