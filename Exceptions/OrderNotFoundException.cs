namespace OrderFlowApi.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid orderId) 
            : base($"Order {orderId} was not found.")
        {
        }
    }
}
