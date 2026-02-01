namespace OrderFlowApi.Exceptions
{
    public class NoProductsException : Exception
    {
        public NoProductsException(string message)
            : base(message)
        {
        }
    }
}
