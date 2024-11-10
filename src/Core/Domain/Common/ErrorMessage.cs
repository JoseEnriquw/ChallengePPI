
namespace Core.Domain.Common
{
    public static class ErrorMessage
    {
        public const string EMPTY_VALUE = "{0} cannot be empty";
        public const string MAXIMUM_LENGTH = "{0} cannot have more than {1} characters";
        public const string NULL_VALUE = "{0} cannot be null";
        public const string NOT_FOUND_GET_BY_ID = "The Id {0} of {1} was not found in the database";
        public const string MUST_BE_A_POSITIVE_NUMBER = "{0} must be a positive number.";
        public const string GREATER_THAN_OR_EQUAL = "{0} must be {1} or greater.";
        public const string PRICE_VALIDATION = "Price must be specified for buy/sell operations.";
        public const string OPERATION_VALIDATION = "Operation must be 'C' (Compra) or 'V' (Venta).";
        public const string STATE_VALIDATION = "Invalid estado. Must be 'InProcess', 'Executed', or 'Canceled'.";
    }

}
