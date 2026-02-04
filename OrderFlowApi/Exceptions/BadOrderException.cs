namespace OrderFlowApi.Exceptions
{
    public class BadOrderException : Exception
    {
        public BadOrderException(string message)
            : base(message)
        {
        }
    }
}
