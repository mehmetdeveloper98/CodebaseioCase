namespace Customer.Application.Dtos.Common
{
    public class ServiceResponseNoContent:BaseResponse
    {
        public static ServiceResponseNoContent Success(int statusCode)
        {
            return new ServiceResponseNoContent { StatusCode = statusCode, IsSuccess = true ,Errors=new List<string>()};
        }
        public static ServiceResponseNoContent Fail(int statusCode, string errors)
        {
            return new ServiceResponseNoContent { Errors = new List<string>() { errors }, StatusCode = statusCode, IsSuccess = false };
        }
        public static ServiceResponseNoContent Fail(int statusCode, List<string> errors)
        {
            return new ServiceResponseNoContent { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }
    }
}
