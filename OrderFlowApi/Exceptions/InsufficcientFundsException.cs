namespace OrderFlowApi.Exceptions
{
    public class InsufficcientFundsException : Exception
    {
            public InsufficcientFundsException()
                : base("Insufficcient funds")
            {
            }
    }
}
