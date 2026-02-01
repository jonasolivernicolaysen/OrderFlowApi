namespace OrderFlowApi.Exceptions
{
    public class BadProductlistingException : Exception
    {
        public BadProductlistingException(string message)
            : base(message)
        {
        }
    }
}
