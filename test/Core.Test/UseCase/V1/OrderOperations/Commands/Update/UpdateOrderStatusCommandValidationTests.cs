using FluentValidation.TestHelper;
using Core.UseCase.V1.OrderOperations.Commands.Update;
using Core.Domain.Enums;

namespace Core.Test.UseCase.V1.OrderOperations.Commands.Update
{
    public class UpdateOrderStatusCommandValidationTests
    {
        private readonly UpdateOrderStatusCommandValidation _validator;

        public UpdateOrderStatusCommandValidationTests()
        {
            _validator = new UpdateOrderStatusCommandValidation();
        }

        [Fact]
        public void Should_HaveError_When_Id_IsNotGreaterThanZero()
        {
            var model = new UpdateOrderStatusCommand { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_NotHaveError_When_Id_IsGreaterThanZero()
        {
            var model = new UpdateOrderStatusCommand { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData((int)OrderStatusEnum.InProcess)]
        [InlineData((int)OrderStatusEnum.Executed)]
        [InlineData((int)OrderStatusEnum.Canceled)]
        public void Should_NotHaveError_When_State_IsValid(int state)
        {
            var model = new UpdateOrderStatusCommand { State = state };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.State);
        }

        [Fact]
        public void Should_HaveError_When_State_IsInvalid()
        {
            var model = new UpdateOrderStatusCommand { State = -1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.State);
        }
    }
}

