using FluentValidation.TestHelper;
using Core.UseCase.V1.OrderOperations.Commands.Create;

public class CreateOrderCommandValidationTests
{
    private readonly CreateOrderCommandValidation _validator;

    public CreateOrderCommandValidationTests()
    {
        _validator = new CreateOrderCommandValidation();
    }

    [Fact]
    public void Should_HaveError_When_AccountId_IsNotGreaterThanZero()
    {
        var model = new CreateOrderCommand { AccountId = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.AccountId);
    }

    [Fact]
    public void Should_NotHaveError_When_AccountId_IsGreaterThanZero()
    {
        var model = new CreateOrderCommand { AccountId = 1 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.AccountId);
    }

    [Fact]
    public void Should_HaveError_When_AssetId_IsNotGreaterThanZero()
    {
        var model = new CreateOrderCommand { AssetId = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.AssetId);
    }

    [Fact]
    public void Should_NotHaveError_When_AssetId_IsGreaterThanZero()
    {
        var model = new CreateOrderCommand { AssetId = 1 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.AssetId);
    }

    [Fact]
    public void Should_HaveError_When_Quantity_IsNotGreaterThanZero()
    {
        var model = new CreateOrderCommand { Quantity = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Should_NotHaveError_When_Quantity_IsGreaterThanZero()
    {
        var model = new CreateOrderCommand { Quantity = 10 };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void Should_HaveError_When_Price_IsLessThanZero_ForOperationCOrV(decimal price)
    {
        var model = new CreateOrderCommand { Price = price, Operation = 'C' };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void Should_NotHaveError_When_Price_IsGreaterThanOrEqualToZero_ForOperationCOrV(decimal price)
    {
        var model = new CreateOrderCommand { Price = price, Operation = 'C' };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Price);
    }

    [Theory]
    [InlineData('C')]
    [InlineData('V')]
    public void Should_NotHaveError_When_Operation_IsCOrV(char operation)
    {
        var model = new CreateOrderCommand { Operation = operation };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Operation);
    }

    [Fact]
    public void Should_HaveError_When_Operation_IsNotCOrV()
    {
        var model = new CreateOrderCommand { Operation = 'X' };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Operation);
    }
}


