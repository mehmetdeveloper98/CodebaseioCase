namespace Customer.Application.Exceptions
{
    public class ServerSideException : Exception
    {
        public ServerSideException(string message) : base(message) { }
    }
}
