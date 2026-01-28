namespace OrderFlowApi.Models
{
    public enum OrderStatus
    {
        Received,
        Rejected,
        Completed,
        Failed,
        Shipped
    }

    public enum PaymentStatus
    {
        Received,
        Pending,
        Failed
    }
}
