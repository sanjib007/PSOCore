namespace Utility.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }
        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception inner)
            : base(message, inner)
        {

        }
        public BaseException(string message, int statusCode = 100)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
