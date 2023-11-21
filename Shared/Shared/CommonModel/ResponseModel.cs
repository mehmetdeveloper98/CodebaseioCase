using System.Text.Json.Serialization;

namespace Shared.CommonModel
{
    public class ResponseModel<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }

        public static ResponseModel<T> Success(int statusCode, T data)
        {
            return new ResponseModel<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static ResponseModel<T> Success(int statusCode)
        {
            return new ResponseModel<T> { StatusCode = statusCode, IsSuccess = true };
        }
        public static ResponseModel<T> Fail(int statusCode, string errors)
        {
            return new ResponseModel<T> { Errors = new List<string>() { errors }, StatusCode = statusCode, IsSuccess = false };
        }
        public static ResponseModel<T> Fail(int statusCode, List<string> errors)
        {
            return new ResponseModel<T> { Errors = errors, StatusCode = statusCode, IsSuccess = false };
        }
    }

}
