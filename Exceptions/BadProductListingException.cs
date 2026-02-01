namespace OrderFlowApi.Exceptions
{
    public class BadProductListingException : Exception
    {
        public BadProductListingException(string message)
            : base(message)
        {
        }
    }
}
