namespace Customer.Application.Dtos.Common
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T? Value { get; set; }
        public static ServiceResponse<T> Success(int statusCode, T data) {
            return new ServiceResponse<T> { Value = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static ServiceResponse<T> Success(int statusCode)
        {
            return new ServiceResponse<T> {StatusCode = statusCode, IsSuccess = true };
        }
        public static ServiceResponse<T> Fail(int statusCode, string errors)
        {
            return new ServiceResponse<T> { Errors = new List<string>() { errors }, StatusCode = statusCode, IsSuccess = false };
        }
        public static ServiceResponse<T> Fail(int statusCode, List<string> errors)
        {
            return new ServiceResponse<T> { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }
    }
}
