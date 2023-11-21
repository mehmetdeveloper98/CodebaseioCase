namespace Customer.Application.Dtos
{
    public class MonthlyStatisticDto
    {
        public int TotalOrder { get; set; }
        public int TotalBookCount { get; set; }
        public decimal TotalPurchasedAmount { get; set; }
    }
}
