namespace OrderFlowApi.Models
{
    public enum OrderStatus
    {
        received,
        rejected,
        completed,
        failed
    }

    public enum PaymentStatus
    {
        received,
        pending,
        failed
    }
}
