namespace Customer.Application.Dtos.Common
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
