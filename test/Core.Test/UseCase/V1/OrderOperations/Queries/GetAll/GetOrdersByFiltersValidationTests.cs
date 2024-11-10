using Core.Domain.Enums;
using Core.UseCase.V1.OrderOperations.Queries.GetAll;
using FluentValidation.TestHelper;

namespace Core.Test.UseCase.V1.OrderOperations.Queries.GetAll
{
    public class GetOrdersByFiltersValidationTests
    {
        private readonly GetOrdersByFiltersValidation _validator;

        public GetOrdersByFiltersValidationTests()
        {
            _validator = new GetOrdersByFiltersValidation();
        }

        [Theory]
        [InlineData('C')]
        [InlineData('V')]
        [InlineData(null)]
        public void Should_NotHaveError_When_Operation_IsValid(char? operation)
        {
            var model = new GetOrdersByFilters { Operation = operation };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Operation);
        }

        [Fact]
        public void Should_HaveError_When_Operation_IsInvalid()
        {
            var model = new GetOrdersByFilters { Operation = 'X' };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Operation);
        }

        [Fact]
        public void Should_NotHaveError_When_State_IsNullOrValidEnumValue()
        {
            var model = new GetOrdersByFilters { State = (int)OrderStatusEnum.InProcess };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.State);

            model.State = null;
            result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.State);
        }

        [Fact]
        public void Should_HaveError_When_State_IsInvalid()
        {
            var model = new GetOrdersByFilters { State = 999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.State);
        }
    }
}
