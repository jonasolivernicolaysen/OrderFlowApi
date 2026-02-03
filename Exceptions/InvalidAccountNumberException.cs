namespace OrderFlowApi.Exceptions
{
    public class InvalidAccountNumberException : Exception
    {
        public InvalidAccountNumberException(int accountNumber)
            : base($"Invalid account number: {accountNumber}.")
        {
        }
    }
}
