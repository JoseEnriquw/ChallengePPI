using FluentValidation.TestHelper;
using Core.UseCase.V1.OrderOperations.Commands.Delete;

namespace Core.Test.UseCase.V1.OrderOperations.Commands.Delete
{

    public class DeleteOrderCommandValidationTests
    {
        private readonly DeleteOrderCommandValidation _validator;

        public DeleteOrderCommandValidationTests()
        {
            _validator = new DeleteOrderCommandValidation();
        }

        [Fact]
        public void Should_HaveError_When_Id_IsNotGreaterThanZero()
        {
            var model = new DeleteOrderCommand { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_NotHaveError_When_Id_IsGreaterThanZero()
        {
            var model = new DeleteOrderCommand { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
