using Core.UseCase.V1.OrderOperations.Queries.GetById;
using FluentValidation.TestHelper;

namespace Core.Test.UseCase.V1.OrderOperations.Queries.GetById
{
    public class GetOrderByIdValidationTests
    {
        private readonly GetOrderByIdValidation _validator;

        public GetOrderByIdValidationTests()
        {
            _validator = new GetOrderByIdValidation();
        }

        [Fact]
        public void Should_HaveError_When_Id_IsNotGreaterThanZero()
        {
            var model = new GetOrderById { Id = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_NotHaveError_When_Id_IsGreaterThanZero()
        {
            var model = new GetOrderById { Id = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
